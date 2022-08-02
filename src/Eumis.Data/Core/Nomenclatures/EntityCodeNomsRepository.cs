using Eumis.Common.Db;
using Eumis.Data.Linq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Core.Nomenclatures
{
    internal class EntityCodeNomsRepository<TEntity, TQuery, TCodeNomVO> : EntityNomsRepository<TEntity, TQuery, TCodeNomVO>, IEntityCodeNomsRepository<TEntity, TCodeNomVO>
        where TEntity : class
    {
        private Expression<Func<TQuery, string>> codeSelector;

        public EntityCodeNomsRepository(
            IUnitOfWork unitOfWork,
            Expression<Func<TQuery, int>> keySelector,
            Expression<Func<TQuery, string>> nameSelector,
            Expression<Func<TQuery, string>> codeSelector,
            Expression<Func<TQuery, TCodeNomVO>> voSelector,
            Expression<Func<TQuery, decimal>> orderBySelector = null)
            : base(unitOfWork, keySelector, nameSelector, voSelector, orderBySelector)
        {
            this.codeSelector = codeSelector;
        }

        public virtual int GetNomIdByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException(nameof(code));
            }

            var predicate =
                PredicateBuilder.True<TQuery>()
                .AndPropertyEquals(this.codeSelector, code);

            return this.GetQuery()
                .Where(predicate)
                .Select(this.keySelector)
                .Single();
        }
    }

    [SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "This class is derivative of the one above.")]
    internal class EntityCodeNomsRepository<TEntity, TCodeNomVO> : EntityCodeNomsRepository<TEntity, TEntity, TCodeNomVO>
        where TEntity : class
    {
        public EntityCodeNomsRepository(
            IUnitOfWork unitOfWork,
            Expression<Func<TEntity, int>> keySelector,
            Expression<Func<TEntity, string>> nameSelector,
            Expression<Func<TEntity, string>> codeSelector,
            Expression<Func<TEntity, TCodeNomVO>> voSelector,
            Expression<Func<TEntity, decimal>> orderBySelector = null)
            : base(unitOfWork, keySelector, nameSelector, codeSelector, voSelector, orderBySelector)
        {
        }
    }
}
