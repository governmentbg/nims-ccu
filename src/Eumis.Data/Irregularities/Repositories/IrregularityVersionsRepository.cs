using Eumis.Common.Db;
using Eumis.Data.Irregularities.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.Core;
using Eumis.Domain.Irregularities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Irregularities.Repositories
{
    internal class IrregularityVersionsRepository : AggregateRepository<IrregularityVersion>, IIrregularityVersionsRepository
    {
        public IrregularityVersionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<IrregularityVersion, object>>[] Includes
        {
            get
            {
                return new Expression<Func<IrregularityVersion, object>>[]
                {
                    i => i.Documents,
                    i => i.InvolvedPersons,
                };
            }
        }

        public IList<IrregularityVersionVO> GetVersions(int irregularityId)
        {
            return (from iv in this.unitOfWork.DbContext.Set<IrregularityVersion>()
                    where iv.IrregularityId == irregularityId
                    orderby iv.OrderNum descending
                    select new IrregularityVersionVO
                    {
                        VersionId = iv.IrregularityVersionId,
                        Status = iv.Status,
                        ModifyDate = iv.ModifyDate,
                        ReportYear = iv.ReportYear,
                        ReportQuarter = iv.ReportQuarter,
                        OrderNum = iv.OrderNum,
                    }).ToList();
        }

        public IList<IrregularityInvolvedPersonVO> GetInvolvedPersons(int versionId)
        {
            return (from iip in this.unitOfWork.DbContext.Set<IrregularityVersionInvolvedPerson>()
                    where iip.IrregularityVersionId == versionId
                    orderby iip.IrregularityVersionInvolvedPersonId descending
                    select new
                    {
                        iip.IrregularityVersionInvolvedPersonId,
                        iip.LegalType,
                        iip.Uin,
                        iip.UinType,
                        iip.FirstName,
                        iip.MiddleName,
                        iip.LastName,
                        iip.CompanyName,
                        iip.TradeName,
                        iip.HoldingName,
                    }).ToList()
                    .Select(o => new IrregularityInvolvedPersonVO
                    {
                        PersonId = o.IrregularityVersionInvolvedPersonId,
                        Uin = o.Uin,
                        UinType = o.UinType,
                        LegalType = o.LegalType,
                        Name = o.LegalType == InvolvedPersonLegalType.Person ?
                            string.Format("{0} {1} {2}", o.FirstName, o.MiddleName, o.LastName) :
                            null,
                        CompanyName = o.CompanyName,
                        TradeName = o.TradeName,
                        HoldingName = o.HoldingName,
                    }).ToList();
        }

        public IList<IrregularityDocVO> GetDocuments(int versionId)
        {
            return (from isd in this.unitOfWork.DbContext.Set<IrregularityVersionDoc>()
                    where isd.IrregularityVersionId == versionId
                    orderby isd.IrregularityVersionDocId descending
                    select new IrregularityDocVO
                    {
                        DocumentId = isd.IrregularityVersionDocId,
                        Description = isd.Description,
                        File = new FileVO
                        {
                            Key = isd.FileKey,
                            Name = isd.FileName,
                        },
                    }).ToList();
        }

        public IrregularityVersion GetActiveVersion(int irregularityId)
        {
            return this.Set()
                .Where(iv => iv.IrregularityId == irregularityId && iv.Status == IrregularityVersionStatus.Active)
                .SingleOrDefault();
        }

        public IrregularityStatus GetIrregularityStatus(int versionId)
        {
            return (from iv in this.unitOfWork.DbContext.Set<IrregularityVersion>()
                    join irr in this.unitOfWork.DbContext.Set<Irregularity>() on iv.IrregularityId equals irr.IrregularityId
                    where iv.IrregularityVersionId == versionId
                    select irr.Status).Single();
        }

        public void RemoveByIrregularityId(int irregularityId)
        {
            var version = this.Set()
                .Single(iv => iv.IrregularityId == irregularityId);

            if (version.Status != IrregularityVersionStatus.Draft)
            {
                throw new InvalidOperationException("Cannot delete nondraft version");
            }

            base.Remove(version);
        }

        public new void Remove(IrregularityVersion version)
        {
            if (version.Status != IrregularityVersionStatus.Draft || version.OrderNum == 1)
            {
                throw new DomainValidationException("Cannot delete nondraft irregularity version or the first version.");
            }

            base.Remove(version);
        }

        public bool HasDraftVersions(int irregularityId)
        {
            return (from iv in this.unitOfWork.DbContext.Set<IrregularityVersion>()
                    where iv.IrregularityId == irregularityId && iv.Status == IrregularityVersionStatus.Draft
                    select iv.IrregularityVersionId).Any();
        }

        public bool HasNonDraftVersions(int irregularityId)
        {
            return (from iv in this.unitOfWork.DbContext.Set<IrregularityVersion>()
                    where iv.IrregularityId == irregularityId && iv.Status != IrregularityVersionStatus.Draft
                    select iv.IrregularityVersionId).Any();
        }

        public int GetProgrammeId(int versionId)
        {
            return (from iv in this.unitOfWork.DbContext.Set<IrregularityVersion>()
                    join i in this.unitOfWork.DbContext.Set<Irregularity>() on iv.IrregularityId equals i.IrregularityId
                    where iv.IrregularityVersionId == versionId
                    select i.ProgrammeId).Single();
        }

        public int? GetContractId(int versionId)
        {
            return (from iv in this.unitOfWork.DbContext.Set<IrregularityVersion>()
                    join i in this.unitOfWork.DbContext.Set<Irregularity>() on iv.IrregularityId equals i.IrregularityId
                    where iv.IrregularityVersionId == versionId
                    select i.ContractId).Single();
        }

        public int GetIrregularityId(int versionId)
        {
            return (from iv in this.unitOfWork.DbContext.Set<IrregularityVersion>()
                    where iv.IrregularityVersionId == versionId
                    select iv.IrregularityId).Single();
        }
    }
}
