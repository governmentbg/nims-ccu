using Eumis.Common.Db;
using Eumis.Data.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Core.Nomenclatures
{
    [SuppressMessage("", "SA1401:FieldsMustBePrivate", Justification = "TODO: review later")]
    internal class DependentEntityNomsRepository<TEntity, TQuery, TNomVO> : EntityNomsRepository<TEntity, TQuery, TNomVO>, IDependentEntityNomsRepository<TEntity, TNomVO>
        where TEntity : class
    {
        private Expression<Func<TQuery, int>> parentSelector;

        public DependentEntityNomsRepository(
            IUnitOfWork unitOfWork,
            Expression<Func<TQuery, int>> keySelector,
            Expression<Func<TQuery, int>> parentSelector,
            Expression<Func<TQuery, string>> nameSelector,
            Expression<Func<TQuery, TNomVO>> voSelector,
            Expression<Func<TQuery, decimal>> orderBySelector = null)
            : this(
                  unitOfWork,
                  keySelector,
                  parentSelector,
                  nameSelector,
                  null,
                  voSelector,
                  orderBySelector)
        {
        }

        public DependentEntityNomsRepository(
            IUnitOfWork unitOfWork,
            Expression<Func<TQuery, int>> keySelector,
            Expression<Func<TQuery, int>> parentSelector,
            Expression<Func<TQuery, string>> nameSelector,
            Expression<Func<TQuery, string>> nameAltSelector,
            Expression<Func<TQuery, TNomVO>> voSelector,
            Expression<Func<TQuery, decimal>> orderBySelector = null)
            : base(
                  unitOfWork,
                  keySelector,
                  nameSelector,
                  nameAltSelector,
                  voSelector,
                  orderBySelector)
        {
            this.parentSelector = parentSelector;
        }

        public override IEnumerable<TNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            return new List<TNomVO>();
        }

        public IEnumerable<TNomVO> GetNoms(int parentId, string term, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<TQuery>()
                .AndPropertyEquals(this.parentSelector, parentId);

            return base.GetQuery()
                .Where(predicate)
                .Select(this.voSelector)
                .ToList();
        }

        protected override IQueryable<TQuery> GetQuery()
        {
            return base.GetQuery();
        }
    }

    [SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "This class is derivative of the one above.")]
    internal class DependentEntityNomsRepository<TEntity, TNomVO> : DependentEntityNomsRepository<TEntity, TEntity, TNomVO>
        where TEntity : class
    {
        public DependentEntityNomsRepository(
            IUnitOfWork unitOfWork,
            Expression<Func<TEntity, int>> keySelector,
            Expression<Func<TEntity, int>> parentSelector,
            Expression<Func<TEntity, string>> nameSelector,
            Expression<Func<TEntity, TNomVO>> voSelector)
            : base(
                unitOfWork,
                keySelector,
                parentSelector,
                nameSelector,
                null,
                voSelector)
        {
        }

        public DependentEntityNomsRepository(
            IUnitOfWork unitOfWork,
            Expression<Func<TEntity, int>> keySelector,
            Expression<Func<TEntity, int>> parentSelector,
            Expression<Func<TEntity, string>> nameSelector,
            Expression<Func<TEntity, string>> nameAltSelector,
            Expression<Func<TEntity, TNomVO>> voSelector)
            : base(
                unitOfWork,
                keySelector,
                parentSelector,
                nameSelector,
                nameAltSelector,
                voSelector)
        {
        }
    }
}
