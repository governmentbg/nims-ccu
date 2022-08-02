using Eumis.Common.Db;
using Eumis.Data.Declarations.ViewObjects;
using Eumis.Data.Linq;
using Eumis.Domain;
using Eumis.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Declarations.Repositories
{
    internal class DeclarationsRepository : AggregateRepository<Declaration>, IDeclarationsRepository
    {
        public DeclarationsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<Declaration, object>>[] Includes
        {
            get
            {
                return new Expression<Func<Declaration, object>>[]
                {
                    d => d.DeclarationFiles,
                };
            }
        }

        public IList<DeclarationVO> GetDeclarations(
            DateTime? activationDate = null,
            DeclarationStatus? status = null)
        {
            var predicate = PredicateBuilder.True<Declaration>();

            predicate = predicate
                .AndEquals(d => d.ActivationDate, activationDate)
                .AndEquals(d => d.Status, status);

            return (from d in this.unitOfWork.DbContext.Set<Declaration>().Where(predicate)
                    join u in this.unitOfWork.DbContext.Set<User>() on d.CreatedByUserId equals u.UserId
                    orderby d.CreateDate descending
                    select new DeclarationVO()
                    {
                        DeclarationId = d.DeclarationId,
                        Status = d.Status,
                        CreateDate = d.CreateDate,
                        NameBg = d.Name,
                        NameEn = d.NameAlt,
                        Creator = u.Fullname + "(" + u.Username + ")",
                        ActivationDate = d.Status == DeclarationStatus.Draft ? null : d.ActivationDate,
                    })
                    .ToList();
        }

        public bool IsDeclarationFileAccessible(int declarationId, Guid fileKey)
        {
            return (from d in this.unitOfWork.DbContext.Set<Declaration>()
                    join df in this.unitOfWork.DbContext.Set<DeclarationFile>() on d.DeclarationId equals df.DeclarationId
                    where d.DeclarationId == declarationId && df.BlobKey == fileKey && d.Status == DeclarationStatus.Published
                    select df.DeclarationFileId).Any();
        }

        public new void Remove(Declaration declaration)
        {
            if (declaration.Status != DeclarationStatus.Draft)
            {
                throw new InvalidOperationException("Cannot delete nondraft declaration.");
            }

            base.Remove(declaration);
        }
    }
}
