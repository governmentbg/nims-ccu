using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Linq;
using Eumis.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data
{
    internal class AggregateRepository<TEntity, TBaseEntity> : Repository, IAggregateRepository<TEntity>
        where TEntity : class, IAggregateRoot
        where TBaseEntity : class, IAggregateRoot
    {
        public AggregateRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this.unitOfWork = (UnitOfWork)unitOfWork;
        }

        protected virtual Expression<Func<TEntity, object>>[] Includes
        {
            get
            {
                return Array.Empty<Expression<Func<TEntity, object>>>();
            }
        }

        public virtual TEntity Find(int id)
        {
            return this.FindEntity(id, this.Includes);
        }

        public async virtual Task<TEntity> FindAsync(int id, CancellationToken ct)
        {
            return await this.FindEntityAsync(id, ct, this.Includes);
        }

        public virtual TEntity FindForUpdate(int id, byte[] version)
        {
            var entity = this.Find(id);
            this.CheckVersion(entity.Version, version);

            return entity;
        }

        public virtual TEntity FindForUpdate(int id, string version)
        {
            byte[] vers = System.Convert.FromBase64String(version);
            return this.FindForUpdate(id, vers);
        }

        public async virtual Task<TEntity> FindForUpdateAsync(int id, byte[] version, CancellationToken ct)
        {
            var entity = await this.FindAsync(id, ct);
            this.CheckVersion(entity.Version, version);

            return entity;
        }

        public TEntity FindWithoutIncludes(int id)
        {
            return this.FindEntity(id, Array.Empty<Expression<Func<TEntity, object>>>());
        }

        public async Task<TEntity> FindWithoutIncludesAsync(int id, CancellationToken ct)
        {
            return await this.FindEntityAsync(id, ct, Array.Empty<Expression<Func<TEntity, object>>>());
        }

        public byte[] GetVersion(int id)
        {
            return this.FindWithoutIncludes(id).Version;
        }

        public void Add(TEntity entity)
        {
            this.unitOfWork.DbContext.Set<TEntity>().Add(entity);
        }

        public void Remove(TEntity entity)
        {
            this.unitOfWork.DbContext.Set<TEntity>().Remove(entity);
        }

        protected IQueryable<TEntity> Set()
        {
            return this.unitOfWork.DbContext.Set<TBaseEntity>().OfType<TEntity>().Include(this.Includes);
        }

        protected void CheckVersion(byte[] version1, byte[] version2)
        {
            if (!Enumerable.SequenceEqual(version1, version2))
            {
                throw new DataUpdateConcurrencyException();
            }
        }

        private TEntity FindEntity(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            object[] keyValues = new object[] { id };

            var e = this.FindInStore(keyValues, includes);

            if (e == null)
            {
                throw new DataObjectNotFoundException(typeof(TEntity).Name, id);
            }

            return e;
        }

        private async Task<TEntity> FindEntityAsync(int id, CancellationToken ct, params Expression<Func<TEntity, object>>[] includes)
        {
            object[] keyValues = new object[] { id };

            var e = await this.FindInStoreAsync(keyValues, ct, includes);

            if (e == null)
            {
                throw new DataObjectNotFoundException(typeof(TEntity).Name, id);
            }

            return e;
        }

        private TEntity FindInStore(object[] keyValues, params Expression<Func<TEntity, object>>[] includes)
        {
            ObjectContext objectContext = ((IObjectContextAdapter)this.unitOfWork.DbContext).ObjectContext;
            ObjectSet<TBaseEntity> objectSet = objectContext.CreateObjectSet<TBaseEntity>();

            string quotedEntitySetName = string.Format(
                CultureInfo.InvariantCulture,
                "{0}.{1}",
                this.QuoteIdentifier(objectSet.EntitySet.EntityContainer.Name),
                this.QuoteIdentifier(objectSet.EntitySet.Name));

            var queryBuilder = new StringBuilder();
            queryBuilder.AppendFormat("SELECT VALUE X FROM {0} AS X WHERE ", quotedEntitySetName);

            var entityKeyValues = this.CreateEntityKey(objectSet.EntitySet, keyValues).EntityKeyValues;
            var parameters = new ObjectParameter[entityKeyValues.Length];

            for (var i = 0; i < entityKeyValues.Length; i++)
            {
                if (i > 0)
                {
                    queryBuilder.Append(" AND ");
                }

                var name = string.Format(CultureInfo.InvariantCulture, "gc{0}", i.ToString(CultureInfo.InvariantCulture));
                queryBuilder.AppendFormat("X.{0} = @{1}", this.QuoteIdentifier(entityKeyValues[i].Key), name);
                parameters[i] = new ObjectParameter(name, entityKeyValues[i].Value);
            }

            IQueryable<TEntity> query = objectContext.CreateQuery<TBaseEntity>(queryBuilder.ToString(), parameters).OfType<TEntity>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.SingleOrDefault();
        }

        private async Task<TEntity> FindInStoreAsync(object[] keyValues, CancellationToken ct, params Expression<Func<TEntity, object>>[] includes)
        {
            ObjectContext objectContext = ((IObjectContextAdapter)this.unitOfWork.DbContext).ObjectContext;
            ObjectSet<TBaseEntity> objectSet = objectContext.CreateObjectSet<TBaseEntity>();

            string quotedEntitySetName = string.Format(
                CultureInfo.InvariantCulture,
                "{0}.{1}",
                this.QuoteIdentifier(objectSet.EntitySet.EntityContainer.Name),
                this.QuoteIdentifier(objectSet.EntitySet.Name));

            var queryBuilder = new StringBuilder();
            queryBuilder.AppendFormat("SELECT VALUE X FROM {0} AS X WHERE ", quotedEntitySetName);

            var entityKeyValues = this.CreateEntityKey(objectSet.EntitySet, keyValues).EntityKeyValues;
            var parameters = new ObjectParameter[entityKeyValues.Length];

            for (var i = 0; i < entityKeyValues.Length; i++)
            {
                if (i > 0)
                {
                    queryBuilder.Append(" AND ");
                }

                var name = string.Format(CultureInfo.InvariantCulture, "gc{0}", i.ToString(CultureInfo.InvariantCulture));
                queryBuilder.AppendFormat("X.{0} = @{1}", this.QuoteIdentifier(entityKeyValues[i].Key), name);
                parameters[i] = new ObjectParameter(name, entityKeyValues[i].Value);
            }

            IQueryable<TEntity> query = objectContext.CreateQuery<TBaseEntity>(queryBuilder.ToString(), parameters).OfType<TEntity>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.SingleOrDefaultAsync(ct);
        }

        private string QuoteIdentifier(string identifier)
        {
            return "[" + identifier.Replace("]", "]]") + "]";
        }

        private EntityKey CreateEntityKey(System.Data.Entity.Core.Metadata.Edm.EntitySet entitySet, object[] keyValues)
        {
            if (keyValues == null || !keyValues.Any() || keyValues.Any(v => v == null))
            {
                throw new ArgumentException("Parameter keyValues cannot be empty or contain nulls.");
            }

            var keyNames = entitySet.ElementType.KeyMembers.Select(m => m.Name).ToList();
            if (keyNames.Count != keyValues.Length)
            {
                throw new ArgumentException("Invalid number of key values.");
            }

            return new System.Data.Entity.Core.EntityKey(entitySet.EntityContainer.Name + "." + entitySet.Name, keyNames.Zip(keyValues, (name, value) => new KeyValuePair<string, object>(name, value)));
        }
    }

    [SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "This class is derivative of the one above.")]
    internal class AggregateRepository<TEntity> : AggregateRepository<TEntity, TEntity>
        where TEntity : class, IAggregateRoot
    {
        public AggregateRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
