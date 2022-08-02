using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Eumis.Public.Data.Linq;

namespace Eumis.Public.Data.Core.Nomenclatures
{
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.")]
    internal class EntityCodeNomsRepository<TEntity, TQuery, TCodeNomVO> : EntityNomsRepository<TEntity, TQuery, TCodeNomVO>, IEntityCodeNomsRepository<TEntity, TCodeNomVO>
        where TEntity : class
    {
        protected Expression<Func<TQuery, string>> codeSelector;

        public EntityCodeNomsRepository(
            IUnitOfWork unitOfWork,
            Expression<Func<TQuery, Guid>> keySelector,
            Expression<Func<TQuery, string>> nameSelector,
            Expression<Func<TQuery, string>> codeSelector,
            Expression<Func<TQuery, TCodeNomVO>> voSelector,
            Expression<Func<TQuery, decimal>> orderBySelector = null)
            : base(unitOfWork, keySelector, nameSelector, voSelector, orderBySelector)
        {
            this.codeSelector = codeSelector;
        }

        public virtual Guid GetNomIdByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException("code");
            }

            var predicate =
                PredicateBuilder.True<TQuery>()
                .AndPropertyEquals(this.codeSelector, code);

            return this.GetQuery()
                .Where(predicate)
                .Select(this.keySelector)
                .Single();
        }

        public virtual bool HasCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException("code");
            }

            var predicate =
                PredicateBuilder.True<TQuery>()
                .AndPropertyEquals(this.codeSelector, code);

            return this.GetQuery().Any(predicate);
        }
    }

    [SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "This class is derivative of the one above.")]
    internal class EntityCodeNomsRepository<TEntity, TCodeNomVO> : EntityCodeNomsRepository<TEntity, TEntity, TCodeNomVO>
        where TEntity : class
    {
        public EntityCodeNomsRepository(
            IUnitOfWork unitOfWork,
            Expression<Func<TEntity, Guid>> keySelector,
            Expression<Func<TEntity, string>> nameSelector,
            Expression<Func<TEntity, string>> codeSelector,
            Expression<Func<TEntity, TCodeNomVO>> voSelector,
            Expression<Func<TEntity, decimal>> orderBySelector = null)
            : base(unitOfWork, keySelector, nameSelector, codeSelector, voSelector, orderBySelector)
        {
        }
    }
}
