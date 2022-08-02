using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Eumis.Public.Data.Linq;

namespace Eumis.Public.Data.Core.Nomenclatures
{
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.")]
    internal class EntityNomsRepository<TEntity, TQuery, TNomVO> : Repository, IEntityNomsRepository<TEntity, TNomVO>
        where TEntity : class
    {
        protected Expression<Func<TQuery, Guid>> keySelector;
        protected Expression<Func<TQuery, string>> nameSelector;
        protected Expression<Func<TQuery, TNomVO>> voSelector;
        protected Expression<Func<TQuery, decimal>> orderBySelector;

        public EntityNomsRepository(
            IUnitOfWork unitOfWork,
            Expression<Func<TQuery, Guid>> keySelector,
            Expression<Func<TQuery, string>> nameSelector,
            Expression<Func<TQuery, TNomVO>> voSelector,
            Expression<Func<TQuery, decimal>> orderBySelector = null)
            : base(unitOfWork)
        {
            this.keySelector = keySelector;
            this.nameSelector = nameSelector;
            this.voSelector = voSelector;
            this.orderBySelector = orderBySelector;
        }

        public virtual TNomVO GetNom(Guid nomValueId)
        {
            if (nomValueId == default(Guid))
            {
                throw new ArgumentException("Filtering by the default value for nomValueId is not allowed.");
            }

            var predicate =
                PredicateBuilder.True<TQuery>()
                .AndPropertyEquals(this.keySelector, nomValueId);

            return this.GetQuery()
                .Where(predicate)
                .Select(this.voSelector)
                .SingleOrDefault();
        }

        public virtual IEnumerable<TNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            var query = this.GetNameFilteredQuery(term);

            query = this.orderBySelector == null ?
                query.OrderBy(this.nameSelector) :
                query.OrderBy(this.orderBySelector);

            return query
                .WithOffsetAndLimit(offset, limit)
                .Select(this.voSelector)
                .ToList();
        }

        protected virtual IQueryable<TQuery> GetQuery()
        {
            return this.unitOfWork.DbContext.Set<TEntity>().Cast<TQuery>();
        }

        protected virtual IQueryable<TQuery> GetNameFilteredQuery(string term)
        {
            var predicate =
               PredicateBuilder.True<TQuery>()
               .AndStringContains(this.nameSelector, term);

            return this.GetQuery().Where(predicate);
        }
    }

    [SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "This class is derivative of the one above.")]
    internal class EntityNomsRepository<TEntity, TNomVO> : EntityNomsRepository<TEntity, TEntity, TNomVO>
        where TEntity : class
    {
        public EntityNomsRepository(
            IUnitOfWork unitOfWork,
            Expression<Func<TEntity, Guid>> keySelector,
            Expression<Func<TEntity, string>> nameSelector,
            Expression<Func<TEntity, TNomVO>> voSelector)
            : base(
                unitOfWork,
                keySelector,
                nameSelector,
                voSelector)
        {
        }
    }
}
