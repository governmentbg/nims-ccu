using Eumis.Common.Db;
using Eumis.Data.Linq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Core.Nomenclatures
{
    internal class EntityGidNomsRepository<TEntity, TQuery, TNomVO> : EntityNomsRepository<TEntity, TQuery, TNomVO>, IEntityGidNomsRepository<TEntity, TNomVO>
        where TEntity : class
    {
        private Expression<Func<TQuery, Guid>> gidSelector;

        public EntityGidNomsRepository(
            IUnitOfWork unitOfWork,
            Expression<Func<TQuery, int>> keySelector,
            Expression<Func<TQuery, string>> nameSelector,
            Expression<Func<TQuery, Guid>> gidSelector,
            Expression<Func<TQuery, TNomVO>> voSelector,
            Expression<Func<TQuery, decimal>> orderBySelector = null)
            : base(unitOfWork, keySelector, nameSelector, voSelector, orderBySelector)
        {
            this.gidSelector = gidSelector;
        }

        public virtual int GetNomIdByGid(Guid gid)
        {
            var predicate =
                PredicateBuilder.True<TQuery>()
                .AndPropertyEquals(this.gidSelector, gid);

            return this.GetQuery()
                .Where(predicate)
                .Select(this.keySelector)
                .Single();
        }
    }

    [SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "This class is derivative of the one above.")]
    internal class EntityGidNomsRepository<TEntity, TNomVO> : EntityGidNomsRepository<TEntity, TEntity, TNomVO>
        where TEntity : class
    {
        public EntityGidNomsRepository(
            IUnitOfWork unitOfWork,
            Expression<Func<TEntity, int>> keySelector,
            Expression<Func<TEntity, string>> nameSelector,
            Expression<Func<TEntity, Guid>> gidSelector,
            Expression<Func<TEntity, TNomVO>> voSelector,
            Expression<Func<TEntity, decimal>> orderBySelector = null)
            : base(unitOfWork, keySelector, nameSelector, gidSelector, voSelector, orderBySelector)
        {
        }
    }
}
