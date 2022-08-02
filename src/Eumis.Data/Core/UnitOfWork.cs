using EntityFramework.Utilities;
using Eumis.Common.Config;
using Eumis.Common.Db;
using Eumis.Common.Log;
using Eumis.Data.Core;
using Eumis.Data.Core.Interception;
using Eumis.Data.Linq;
using Eumis.Domain.Core;
using Medallion.Threading.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data
{
    internal class UnitOfWork : IUnitOfWork, IUnitOfWorkHidden
    {
        private static readonly TimeSpan ACQUIRE_TIMEOUT = TimeSpan.FromSeconds(5);

        public const string ContextName = "DbContext";

        private static readonly Func<EntityReference, EntityKey> RelatedEndCachedValueAccessor = ExpressionHelper.GetFieldAccessor<EntityReference, EntityKey>("_cachedForeignKey");

        private static object syncRoot = new object();

        private static DbCompiledModel compiledModel = null;

        private static string connectionString = null;

        private bool disposed = false;

        private DbContext context = null;

        private ILogger logger;
        private IEnumerable<IDbContextInitializer> contextInitializers;
        private IEnumerable<IEumisDbCommandInterceptor> interceptors;

        private Lazy<IEnumerable<IEventHandler>> eventHandlers;
        private Lazy<IEnumerable<INotificationEventHandler>> notificationEventHandlers;

        public UnitOfWork(
            ILogger logger,
            IEnumerable<IDbConfiguration> configurations,
            IEnumerable<IDbContextInitializer> contextInitializers,
            IEnumerable<IEumisDbCommandInterceptor> interceptors,
            Lazy<IEnumerable<IEventHandler>> eventHandlers,
            Lazy<IEnumerable<INotificationEventHandler>> notificationEventHandlers)
        {
            this.contextInitializers = contextInitializers;
            this.interceptors = interceptors;
            this.eventHandlers = eventHandlers;
            this.notificationEventHandlers = notificationEventHandlers;
            this.logger = logger;

            Initialize(configurations);
        }

        private static string ConnectionString
        {
            get
            {
                if (connectionString == null)
                {
                    connectionString = ConfigurationManager.ConnectionStrings[ContextName].ConnectionString.ExpandEnv();
                }

                return connectionString;
            }
        }

        public DbContext DbContext
        {
            get
            {
                if (this.context == null)
                {
                    this.context = new BridgingDbContext(this.interceptors, UnitOfWork.ConnectionString, compiledModel);
                    this.context.Configuration.LazyLoadingEnabled = false;
                    this.context.Configuration.ProxyCreationEnabled = false;
                    this.context.Configuration.UseDatabaseNullSemantics = true;

                    foreach (IDbContextInitializer contextInitializer in this.contextInitializers)
                    {
                        contextInitializer.InitializeContext(this.context);
                    }

#if DEBUG
                    this.context.Database.Log = (s) => Debug.WriteLine(s);
#endif
                }

                return this.context;
            }
        }

        private static void Initialize(IEnumerable<IDbConfiguration> configurations)
        {
            if (compiledModel == null)
            {
                lock (syncRoot)
                {
                    if (compiledModel == null)
                    {
                        Database.SetInitializer<DbContext>(null);

                        DbModelBuilder modelBuilder = new DbModelBuilder();

                        modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
                        modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

                        foreach (IDbConfiguration configuration in configurations)
                        {
                            configuration.AddConfiguration(modelBuilder);
                        }

                        using (SqlConnection connection = new SqlConnection(UnitOfWork.ConnectionString))
                        {
                            compiledModel = modelBuilder.Build(connection).Compile();
                        }
                    }
                }
            }
        }

        public void Save()
        {
            try
            {
                this.DispatchEvents();
                this.DispatchNotificationEvents();

                this.context.ChangeTracker.DetectChanges();

                // we evaluate the entries in advance because some of them may become detached
                // as others before them are deleted (in case they have dependent key)
                var addedOrModifiedEntityEntries =
                    ((IObjectContextAdapter)this.context).ObjectContext.ObjectStateManager
                    .GetObjectStateEntries(EntityState.Added | EntityState.Modified)
                    .Where(e => !e.IsRelationship)
                    .ToList();

                foreach (ObjectStateEntry entry in addedOrModifiedEntityEntries)
                {
                    // skip if a parent entity was deleted and a dependent entity becomes detached
                    if (entry.State == EntityState.Detached)
                    {
                        continue;
                    }

                    if (entry.RelationshipManager.GetAllRelatedEnds()
                        .Any(re =>
                            re is EntityReference &&
                            re.RelationshipSet.ElementType.RelationshipEndMembers
                                .Any(rem => rem.Name == re.TargetRoleName &&
                                    rem.DeleteBehavior == OperationAction.Cascade) &&
                            RelatedEndCachedValueAccessor((EntityReference)re).EntityContainerName.Contains("EntityHasNullForeignKey")))
                    {
                        ((IObjectContextAdapter)this.context).ObjectContext.DeleteObject(entry.Entity);
                    }
                }

                this.context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DataUpdateConcurrencyException();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder validationErrorsText = new StringBuilder();
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        validationErrorsText.AppendFormat(
                            "Class: {0}, Property: {1}, Error: {2}\n\n",
                            validationErrors.Entry.Entity.GetType().FullName,
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }

                this.logger.Log(LogLevel.Error, "DbEntityValidationException\n" + validationErrorsText.ToString());

                throw;
            }
        }

        public async Task SaveAsync(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                this.DispatchEvents();
                this.DispatchNotificationEvents();

                this.context.ChangeTracker.DetectChanges();

                // we evaluate the entries in advance because some of them may become detached
                // as others before them are deleted (in case they have dependent key)
                var addedOrModifiedEntityEntries =
                    ((IObjectContextAdapter)this.context).ObjectContext.ObjectStateManager
                    .GetObjectStateEntries(EntityState.Added | EntityState.Modified)
                    .Where(e => !e.IsRelationship)
                    .ToList();

                foreach (ObjectStateEntry entry in addedOrModifiedEntityEntries)
                {
                    // skip if a parent entity was deleted and a dependent entity becomes detached
                    if (entry.State == EntityState.Detached)
                    {
                        continue;
                    }

                    if (entry.RelationshipManager.GetAllRelatedEnds()
                        .Any(re =>
                            re is EntityReference &&
                            re.RelationshipSet.ElementType.RelationshipEndMembers
                                .Any(rem => rem.Name == re.TargetRoleName &&
                                    rem.DeleteBehavior == OperationAction.Cascade) &&
                            RelatedEndCachedValueAccessor((EntityReference)re).EntityContainerName.Contains("EntityHasNullForeignKey")))
                    {
                        ((IObjectContextAdapter)this.context).ObjectContext.DeleteObject(entry.Entity);
                    }
                }

                await this.context.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DataUpdateConcurrencyException();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder validationErrorsText = new StringBuilder();
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        validationErrorsText.AppendFormat(
                            "Class: {0}, Property: {1}, Error: {2}\n\n",
                            validationErrors.Entry.Entity.GetType().FullName,
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }

                this.logger.Log(LogLevel.Error, "DbEntityValidationException\n" + validationErrorsText.ToString());

                throw;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public DbContextTransaction BeginTransaction()
        {
            return this.DbContext.Database.BeginTransaction();
        }

        public DbContextTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return this.DbContext.Database.BeginTransaction(isolationLevel);
        }

        public Task<IDisposable> AcquireLockAsync(string lockName, CancellationToken cancellationToken)
        {
            var currentTransaction = this.DbContext.Database.CurrentTransaction;
            if (currentTransaction == null)
            {
                throw new Exception($"A transaction is required for {nameof(this.AcquireLockAsync)} to work properly, wrap the call in a transaction.");
            }

            var myLock = new SqlDistributedLock(lockName, currentTransaction.UnderlyingTransaction);

            return myLock.AcquireAsync(ACQUIRE_TIMEOUT, cancellationToken);
        }

        // Note: the entities in the items should not be added in the context graph
        void IUnitOfWork.BulkInsert<TEntity>(IEnumerable<TEntity> items)
        {
            var transaction = this.context.Database.CurrentTransaction;
            if (transaction == null)
            {
                throw new Exception("Transaction is required for BulkInsert. Wrap the call to BulkInsert in a transaction.");
            }

            EFBatchOperation.For(this.DbContext, this.DbContext.Set<TEntity>()).InsertAll(items, transaction: transaction.UnderlyingTransaction);
        }

        void IUnitOfWork.BulkUpdate<TEntity>(IEnumerable<TEntity> items, params Expression<Func<TEntity, object>>[] properties)
        {
            var transaction = this.context.Database.CurrentTransaction;
            if (transaction == null)
            {
                throw new Exception("Transaction is required for BulkUpdate. Wrap the call to BulkUpdate in a transaction.");
            }

            EFBatchOperation.For(this.DbContext, this.DbContext.Set<TEntity>()).UpdateAll(items, x => x.ColumnsToUpdate(properties), transaction: transaction.UnderlyingTransaction);

            // make sure the updated entities are marked as Unchanged and will not be sent to the DB on subsequent SaveChanges
            var objectStateManager = ((IObjectContextAdapter)this.context).ObjectContext.ObjectStateManager;

            this.context.ChangeTracker.DetectChanges();

            foreach (var item in items)
            {
                objectStateManager.GetObjectStateEntry(item).AcceptChanges();
            }
        }

        void IUnitOfWork.BulkDelete<TEntity>(Expression<Func<TEntity, bool>> predicate)
        {
            if (this.context.Database.CurrentTransaction == null)
            {
                throw new Exception("Transaction is required for BulkDelete. Wrap the call to BulkDelete in a transaction.");
            }

            // we do not pass the transaction, because Delete uses ExecuteStoreCommand, which
            // is executed in the context of the current transaction, if a current transaction exists
            EFBatchOperation.For(this.DbContext, this.DbContext.Set<TEntity>()).Where(predicate).Delete();
        }

        void IUnitOfWorkHidden.BulkInsert<TEntity>(IEnumerable<TEntity> items, string tableNameOverride)
        {
            var transaction = this.context.Database.CurrentTransaction;
            if (transaction == null)
            {
                throw new Exception("Transaction is required for BulkInsert. Wrap the call to BulkInsert in a transaction.");
            }

            EFBatchOperation.For(this.DbContext, this.DbContext.Set<TEntity>()).InsertAll(items, transaction: transaction.UnderlyingTransaction, tableNameOverride: tableNameOverride);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing && this.context != null)
                {
                    this.context.Dispose();
                }

                this.context = null;
                this.disposed = true;
            }
        }

        private void DispatchEvents()
        {
            var domainEventEntities = this.context.ChangeTracker.Entries<IEventEmitter>()
                .Select(po => po.Entity)
                .Where(po => po.Events.Any())
                .ToArray();

            foreach (var entity in domainEventEntities)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach (var domainEvent in events)
                {
                    foreach (var eventHandler in this.eventHandlers.Value)
                    {
                        eventHandler.Handle(domainEvent);
                    }
                }
            }
        }

        private void DispatchNotificationEvents()
        {
            var notificationEventEntities = this.context.ChangeTracker.Entries<INotificationEventEmitter>()
                .Select(po => po.Entity)
                .Where(po => po.NotificationEvents.Any())
                .ToArray();

            foreach (var entity in notificationEventEntities)
            {
                var events = entity.NotificationEvents.ToArray();
                entity.NotificationEvents.Clear();
                foreach (var notificationEvent in events)
                {
                    foreach (var eventHandler in this.notificationEventHandlers.Value)
                    {
                        eventHandler.Handle(notificationEvent);
                    }
                }
            }
        }
    }
}
