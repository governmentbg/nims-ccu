using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Data.Registrations.PortalViewObjects;
using Eumis.Data.Registrations.ViewObjects;
using Eumis.Domain.Contracts;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Registrations;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Registrations.Repositories
{
    internal class RegOfferXmlsRepository : AggregateRepository<RegOfferXml>, IRegOfferXmlsRepository
    {
        public RegOfferXmlsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<RegOfferXml, object>>[] Includes
        {
            get
            {
                return new Expression<Func<RegOfferXml, object>>[]
                {
                    c => c.Files,
                };
            }
        }

        public RegOfferXml Find(int registrationId, Guid offerGid)
        {
            var regOfferXml = this.Set()
                .Where(r => r.Gid == offerGid && r.RegistrationId == registrationId)
                .SingleOrDefault();

            if (regOfferXml == null)
            {
                throw new DataObjectNotFoundException("RegOffertXml", offerGid);
            }

            return regOfferXml;
        }

        public RegOfferXml FindForUpdate(int registrationId, Guid offerGid, byte[] version)
        {
            RegOfferXml regOfferXml = this.Find(registrationId, offerGid);

            this.CheckVersion(regOfferXml.Version, version);

            return regOfferXml;
        }

        public RegOfferXml FindActive(Guid offerGid)
        {
            var regOfferXml = (from rox in this.Set()
                               join cdf in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>() on rox.ContractDifferentiatedPositionId equals cdf.ContractDifferentiatedPositionId
                               join cpp in this.unitOfWork.DbContext.Set<ContractProcurementPlan>() on cdf.ContractProcurementPlanId equals cpp.ContractProcurementPlanId
                               where rox.Gid == offerGid && DbFunctions.DiffDays(cpp.OffersDeadlineDate, DateTime.Now) > 0
                               select rox).FirstOrDefault();

            if (regOfferXml == null)
            {
                throw new DataObjectNotFoundException("RegOffertXml", offerGid);
            }

            return regOfferXml;
        }

        public IList<ContractOfferVO> GetAllForContract(int contractId)
        {
            return (from rox in this.Set()
                    join cdf in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>() on rox.ContractDifferentiatedPositionId equals cdf.ContractDifferentiatedPositionId
                    join cpp in this.unitOfWork.DbContext.Set<ContractProcurementPlan>() on cdf.ContractProcurementPlanId equals cpp.ContractProcurementPlanId
                    where cpp.ContractId == contractId
                    orderby rox.CreateDate descending
                    select new ContractOfferVO
                    {
                        Id = rox.RegOfferXmlId,
                        Gid = rox.Gid,
                        SubmitDate = rox.CreateDate,
                        CanBeDisplayed = (DbFunctions.DiffDays(cpp.OffersDeadlineDate, DateTime.Now) > 0 && !cpp.TerminatedDate.HasValue) || (cpp.TerminatedDate.HasValue && cpp.OffersDeadlineDate < cpp.TerminatedDate.Value),
                        ProcurementPlanExpectedAmount = cpp.ExpectedAmount,
                        ProcurementPlanSubject = cpp.Name,
                    }).ToList();
        }

        public ContractOfferVO GetOfferDetails(int offerId)
        {
            return (from rox in this.Set()
                    join cdf in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>() on rox.ContractDifferentiatedPositionId equals cdf.ContractDifferentiatedPositionId
                    join cpp in this.unitOfWork.DbContext.Set<ContractProcurementPlan>() on cdf.ContractProcurementPlanId equals cpp.ContractProcurementPlanId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cpp.ContractId equals c.ContractId
                    join ea in this.unitOfWork.DbContext.Set<ErrandArea>() on cpp.ErrandAreaId equals ea.ErrandAreaId
                    where (rox.RegOfferXmlId == offerId && DbFunctions.DiffDays(cpp.OffersDeadlineDate, DateTime.Now) > 0 && !cpp.TerminatedDate.HasValue)
                    || (rox.RegOfferXmlId == offerId && cpp.TerminatedDate.HasValue && cpp.TerminatedDate > cpp.OffersDeadlineDate)
                    select new ContractOfferVO
                    {
                        CompanyName = c.CompanyName,
                        CompanyUinType = c.CompanyUinType,
                        CompanyUin = c.CompanyUin,
                        CanBeDisplayed = true, // because of the WHERE clause
                        Description = cpp.Description,
                        Gid = rox.Gid,
                        Id = rox.RegOfferXmlId,
                        ProcurementPlanExpectedAmount = cpp.ExpectedAmount,
                        ProcurementPlanObject = ea.Name,
                        ProcurementPlanSubject = cpp.Name,
                        SubmitDate = rox.CreateDate,
                    }).FirstOrDefault();
        }

        public int GetSubmittedCount(int contractDifferentiatedPositionId)
        {
            return this.unitOfWork.DbContext.Set<RegOfferXml>()
                        .Where(rox => rox.ContractDifferentiatedPositionId == contractDifferentiatedPositionId)
                        .Where(rox => rox.Status == RegOfferStatus.Submitted)
                        .Count();
        }

        public RegOfferXmlPVO GetInfoForRegistration(int registrationId, Guid offerGid)
        {
            return this.GetRegOffersInternal(registrationId, new[] { RegOfferStatus.Submitted, RegOfferStatus.Withdrawn }, offerGid: offerGid).Item1.Single();
        }

        public PagePVO<RegOfferXmlPVO> GetSubmittedForRegistration(
            int registrationId,
            int offset = 0,
            int? limit = null,
            string dpName = null,
            string name = null,
            string companyName = null,
            DateTime? submitDate = null,
            string sortBy = null,
            SortOrder? sortOrder = null)
        {
            var results = this.GetRegOffersInternal(registrationId, new[] { RegOfferStatus.Submitted, RegOfferStatus.Withdrawn }, offset, limit, dpName, name, companyName, submitDate, sortBy: sortBy, sortOrder: sortOrder);
            return new PagePVO<RegOfferXmlPVO>
            {
                Results = results.Item1,
                Count = results.Item2,
            };
        }

        public PagePVO<RegOfferXmlPVO> GetDraftsForRegistration(
            int registrationId,
            int offset = 0,
            int? limit = null,
            string dpName = null,
            string name = null,
            string companyName = null)
        {
            var results = this.GetRegOffersInternal(registrationId, new[] { RegOfferStatus.Draft }, offset, limit, dpName, name, companyName);
            return new PagePVO<RegOfferXmlPVO>
            {
                Results = results.Item1,
                Count = results.Item2,
            };
        }

        private Tuple<IList<RegOfferXmlPVO>, int> GetRegOffersInternal(
            int registrationId,
            RegOfferStatus[] statuses,
            int offset = 0,
            int? limit = null,
            string dpName = null,
            string name = null,
            string companyName = null,
            DateTime? submitDate = null,
            Guid? offerGid = null,
            string sortBy = null,
            SortOrder? sortOrder = null)
        {
            var roxPredicate = PredicateBuilder.True<RegOfferXml>()
                .AndEquals(c => c.Gid, offerGid)
                .AndDateTimeGreaterThanOrEqual(r => r.SubmitDate, submitDate?.Date)
                .AndDateTimeLessThanOrEqual(r => r.SubmitDate, submitDate?.Date.AddDays(1).AddMilliseconds(-1));

            var dpPredicate = PredicateBuilder.True<ContractDifferentiatedPosition>()
                .AndStringContains(t => t.Name, dpName);

            var cppPredicate = PredicateBuilder.True<ContractProcurementPlan>()
                .AndStringContains(t => t.Name, name);

            var cPredicate = PredicateBuilder.True<Contract>()
                .AndStringContains(t => t.CompanyName, companyName);

            var query = (from rox in this.unitOfWork.DbContext.Set<RegOfferXml>().Where(roxPredicate)
                         join cdf in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>().Where(dpPredicate) on rox.ContractDifferentiatedPositionId equals cdf.ContractDifferentiatedPositionId
                         join cpp in this.unitOfWork.DbContext.Set<ContractProcurementPlan>().Where(cppPredicate) on cdf.ContractProcurementPlanId equals cpp.ContractProcurementPlanId
                         join ea in this.unitOfWork.DbContext.Set<ErrandArea>() on cpp.ErrandAreaId equals ea.ErrandAreaId
                         join ela in this.unitOfWork.DbContext.Set<ErrandLegalAct>() on cpp.ErrandLegalActId equals ela.ErrandLegalActId
                         join et in this.unitOfWork.DbContext.Set<ErrandType>() on cpp.ErrandTypeId equals et.ErrandTypeId
                         join c in this.unitOfWork.DbContext.Set<Contract>().Where(cPredicate) on cpp.ContractId equals c.ContractId

                         join cc in this.unitOfWork.DbContext.Set<ContractContract>() on cdf.ContractContractId equals cc.ContractContractId into g2
                         from cc in g2.DefaultIfEmpty()

                         join cctor in this.unitOfWork.DbContext.Set<ContractContractor>() on cc.ContractContractorId equals cctor.ContractContractorId into g3
                         from cctor in g3.DefaultIfEmpty()

                         where rox.RegistrationId == registrationId && statuses.Contains(rox.Status)
                         orderby rox.CreateDate descending

                         select new
                         {
                             OfferGid = rox.Gid,
                             OfferSubmitDate = rox.SubmitDate,
                             OfferStatus = rox.Status,
                             OfferVersion = rox.Version,

                             cdf.ContractDifferentiatedPositionId,
                             c.CompanyName,
                             c.CompanyUin,
                             c.CompanyUinType,
                             cpp.Name,
                             ErrandAreaCode = ea.Code,
                             ErrandAreaName = ea.Name,
                             ErrandLegalActGid = ela.Gid,
                             ErrandLegalActName = ela.Name,
                             ErrandTypeCode = et.Code,
                             ErrandTypeName = et.Name,
                             cpp.ExpectedAmount,
                             cpp.NoticeDate,
                             cpp.OffersDeadlineDate,
                             cpp.Description,

                             DpGid = cdf.Gid,
                             DpName = cdf.Name,
                             DpSubmittedOffersCount = ela.Gid == ErrandLegalAct.PmsGid ?
                                (cpp.OffersDeadlineDate.HasValue && cpp.OffersDeadlineDate < DateTime.Now ?
                                    cdf.SubmittedOffersCount : null) : cdf.SubmittedOffersCount,
                             DpRankedOffersCount = cdf.RankedOffersCount,
                             DpContractContractCompanyName = cctor.Name,
                             DpContractContractCompanyUin = cctor.Uin,
                             DpContractContractCompanyUinType = (UinType?)cctor.UinType,
                             DpContractContractContractNumber = cc.Number,
                         })
                    .ToList()
                    .GroupBy(t => new
                    {
                        t.OfferGid,
                        t.OfferSubmitDate,
                        t.OfferStatus,
                        t.OfferVersion,

                        t.ContractDifferentiatedPositionId,
                        t.CompanyName,
                        t.CompanyUin,
                        t.CompanyUinType,
                        t.Name,
                        t.ErrandAreaCode,
                        t.ErrandAreaName,
                        t.ErrandLegalActGid,
                        t.ErrandLegalActName,
                        t.ErrandTypeCode,
                        t.ErrandTypeName,
                        t.ExpectedAmount,
                        t.NoticeDate,
                        t.OffersDeadlineDate,
                        t.Description,

                        t.DpGid,
                        t.DpName,
                        t.DpSubmittedOffersCount,
                        t.DpRankedOffersCount,
                        t.DpContractContractCompanyName,
                        t.DpContractContractCompanyUin,
                        t.DpContractContractCompanyUinType,
                        t.DpContractContractContractNumber,
                    });

            var count = query.Count();
            var results = query
                    .Select(t => new RegOfferXmlPVO
                    {
                        CompanyName = t.Key.CompanyName,
                        CompanyUin = t.Key.CompanyUin,
                        CompanyUinType = new EnumPVO<UinType>()
                        {
                            Description = t.Key.CompanyUinType,
                            Value = t.Key.CompanyUinType,
                        },
                        Name = t.Key.Name,
                        ErrandArea = new EntityCodeNomVO()
                        {
                            Code = t.Key.ErrandAreaCode,
                            Name = t.Key.ErrandAreaName,
                        },
                        ErrandLegalAct = new EntityGidNomVO()
                        {
                            Gid = t.Key.ErrandLegalActGid,
                            Name = t.Key.ErrandLegalActName,
                        },
                        ErrandType = new EntityCodeNomVO
                        {
                            Code = t.Key.ErrandTypeCode,
                            Name = t.Key.ErrandTypeName,
                        },
                        ExpectedAmount = t.Key.ExpectedAmount,
                        NoticeDate = t.Key.NoticeDate,
                        OffersDeadlineDate = t.Key.OffersDeadlineDate,
                        Description = t.Key.Description,

                        DpGid = t.Key.DpGid,
                        DpName = t.Key.DpName,

                        DpSubmittedOffersCount = t.Key.DpSubmittedOffersCount,
                        DpRankedOffersCount = t.Key.DpRankedOffersCount,
                        DpContractContractCompanyName = t.Key.DpContractContractCompanyName,
                        DpContractContractCompanyUin = t.Key.DpContractContractCompanyUin,
                        DpContractContractCompanyUinType = t.Key.DpContractContractCompanyUinType.HasValue ?
                            new EnumPVO<UinType>()
                            {
                                Description = t.Key.DpContractContractCompanyUinType.Value,
                                Value = t.Key.DpContractContractCompanyUinType.Value,
                            }
                            : null,
                        DpContractContractContractNumber = t.Key.DpContractContractContractNumber,

                        OfferGid = t.Key.OfferGid,
                        OfferSubmitDate = t.Key.OfferSubmitDate,
                        OfferIsWithdrawn = t.Key.OfferStatus == RegOfferStatus.Withdrawn,
                        OfferVersion = t.Key.OfferVersion,
                    })
                    .WithSort(sortBy, sortOrder)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();

            return new Tuple<IList<RegOfferXmlPVO>, int>(results, count);
        }

        public RegOfferXml Find(Guid contractGid, Guid offerGid)
        {
            var regOfferXml = (from rox in this.unitOfWork.DbContext.Set<RegOfferXml>()
                               join cdf in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>() on rox.ContractDifferentiatedPositionId equals cdf.ContractDifferentiatedPositionId
                               join cpp in this.unitOfWork.DbContext.Set<ContractProcurementPlan>() on cdf.ContractProcurementPlanId equals cpp.ContractProcurementPlanId
                               join c in this.unitOfWork.DbContext.Set<Contract>() on cpp.ContractId equals c.ContractId

                               where rox.Gid == offerGid && c.Gid == contractGid
                               select rox)
                        .SingleOrDefault();

            if (regOfferXml == null)
            {
                throw new DataObjectNotFoundException("RegOffertXml", offerGid);
            }

            return regOfferXml;
        }

        public ContractRegOfferXmlPVO GetInfoForContractRegistration(Guid contractGid, Guid offerGid)
        {
            return this.GetContractRegOffersInternal(contractGid, offerGid: offerGid).Single();
        }

        public PagePVO<ContractRegOfferXmlPVO> GetAllForContractRegistration(Guid contractGid, int offset = 0, int? limit = null)
        {
            var results = this.GetContractRegOffersInternal(contractGid, offset, limit);
            return new PagePVO<ContractRegOfferXmlPVO>
            {
                Results = results,
                Count = results.Count(),
            };
        }

        public int GetProgrammeId(int offerId)
        {
            return (from rox in this.unitOfWork.DbContext.Set<RegOfferXml>()
                    join cdf in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>() on rox.ContractDifferentiatedPositionId equals cdf.ContractDifferentiatedPositionId
                    join cpp in this.unitOfWork.DbContext.Set<ContractProcurementPlan>() on cdf.ContractProcurementPlanId equals cpp.ContractProcurementPlanId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cpp.ContractId equals c.ContractId
                    where rox.RegOfferXmlId == offerId
                    select c.ProgrammeId).Single();
        }

        public int GetProjectId(int offerId)
        {
            return (from rox in this.unitOfWork.DbContext.Set<RegOfferXml>()
                    join cdf in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>() on rox.ContractDifferentiatedPositionId equals cdf.ContractDifferentiatedPositionId
                    join cpp in this.unitOfWork.DbContext.Set<ContractProcurementPlan>() on cdf.ContractProcurementPlanId equals cpp.ContractProcurementPlanId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cpp.ContractId equals c.ContractId
                    where rox.RegOfferXmlId == offerId
                    select c.ProjectId).Single();
        }

        public int GetContractId(int offerId)
        {
            return (from rox in this.unitOfWork.DbContext.Set<RegOfferXml>()
                    join cdf in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>() on rox.ContractDifferentiatedPositionId equals cdf.ContractDifferentiatedPositionId
                    join cpp in this.unitOfWork.DbContext.Set<ContractProcurementPlan>() on cdf.ContractProcurementPlanId equals cpp.ContractProcurementPlanId
                    where rox.RegOfferXmlId == offerId
                    select cpp.ContractId).Single();
        }

        private IList<ContractRegOfferXmlPVO> GetContractRegOffersInternal(Guid contractGid, int offset = 0, int? limit = null, Guid? offerGid = null)
        {
            var predicate = PredicateBuilder.True<RegOfferXml>()
                .AndEquals(c => c.Gid, offerGid)
                .And(r => r.Status == RegOfferStatus.Submitted || r.Status == RegOfferStatus.Withdrawn);

            return (from rox in this.unitOfWork.DbContext.Set<RegOfferXml>().Where(predicate)
                    join cdf in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>() on rox.ContractDifferentiatedPositionId equals cdf.ContractDifferentiatedPositionId
                    join cpp in this.unitOfWork.DbContext.Set<ContractProcurementPlan>() on cdf.ContractProcurementPlanId equals cpp.ContractProcurementPlanId
                    join ea in this.unitOfWork.DbContext.Set<ErrandArea>() on cpp.ErrandAreaId equals ea.ErrandAreaId
                    join ela in this.unitOfWork.DbContext.Set<ErrandLegalAct>() on cpp.ErrandLegalActId equals ela.ErrandLegalActId
                    join et in this.unitOfWork.DbContext.Set<ErrandType>() on cpp.ErrandTypeId equals et.ErrandTypeId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cpp.ContractId equals c.ContractId

                    where c.Gid == contractGid
                    orderby rox.CreateDate descending
                    select new
                    {
                        OfferGid = rox.Gid,
                        OfferSubmitDate = rox.SubmitDate,
                        OfferStatus = rox.Status,

                        cdf.ContractDifferentiatedPositionId,
                        c.CompanyName,
                        c.CompanyUin,
                        c.CompanyUinType,
                        TendererEmail = rox.Email,
                        TendererName = rox.Tenderer,
                        TendererUin = rox.Uin,
                        TendererUinType = rox.UinType,
                        cpp.Name,
                        ErrandAreaCode = ea.Code,
                        ErrandAreaName = ea.Name,
                        ErrandLegalActGid = ela.Gid,
                        ErrandLegalActName = ela.Name,
                        ErrandTypeCode = et.Code,
                        ErrandTypeName = et.Name,
                        cpp.ExpectedAmount,
                        cpp.NoticeDate,
                        cpp.OffersDeadlineDate,
                        cpp.TerminatedDate,
                        cpp.Description,

                        DifferentiatedPositionGid = cdf.Gid,
                        ProcurementPlanGid = cpp.Gid,
                    })
                    .ToList()
                    .GroupBy(t => new
                    {
                        t.OfferGid,
                        t.OfferSubmitDate,
                        t.OfferStatus,

                        t.ContractDifferentiatedPositionId,
                        t.CompanyName,
                        t.CompanyUin,
                        t.CompanyUinType,
                        t.Name,
                        t.TendererName,
                        t.TendererEmail,
                        t.TendererUin,
                        t.TendererUinType,
                        t.ErrandAreaCode,
                        t.ErrandAreaName,
                        t.ErrandLegalActGid,
                        t.ErrandLegalActName,
                        t.ErrandTypeCode,
                        t.ErrandTypeName,
                        t.ExpectedAmount,
                        t.NoticeDate,
                        t.OffersDeadlineDate,
                        t.TerminatedDate,
                        t.Description,

                        t.DifferentiatedPositionGid,
                        t.ProcurementPlanGid,
                    })
                    .WithOffsetAndLimit(offset, limit)
                    .Select(t => new ContractRegOfferXmlPVO
                    {
                        CompanyName = t.Key.CompanyName,
                        CompanyUin = t.Key.CompanyUin,
                        CompanyUinType = new EnumPVO<UinType>()
                        {
                            Description = t.Key.CompanyUinType,
                            Value = t.Key.CompanyUinType,
                        },
                        Name = t.Key.Name,
                        ErrandArea = new EntityCodeNomVO()
                        {
                            Code = t.Key.ErrandAreaCode,
                            Name = t.Key.ErrandAreaName,
                        },
                        ErrandLegalAct = new EntityGidNomVO()
                        {
                            Gid = t.Key.ErrandLegalActGid,
                            Name = t.Key.ErrandLegalActName,
                        },
                        ErrandType = new EntityCodeNomVO
                        {
                            Code = t.Key.ErrandTypeCode,
                            Name = t.Key.ErrandTypeName,
                        },
                        TendererName = t.Key.TendererName,
                        TendererEmail = t.Key.TendererEmail,
                        TendererUin = t.Key.TendererUin,
                        TendererUinType = new EnumPVO<UinType>()
                        {
                            Description = t.Key.CompanyUinType,
                            Value = t.Key.CompanyUinType,
                        },
                        ExpectedAmount = t.Key.ExpectedAmount,
                        NoticeDate = t.Key.NoticeDate,
                        OffersDeadlineDate = t.Key.OffersDeadlineDate,
                        ProcurementTerminatedDate = t.Key.TerminatedDate,
                        Description = t.Key.Description,

                        OfferGid = t.Key.OfferGid,
                        OfferSubmitDate = t.Key.OfferSubmitDate.Value,
                        OfferIsWithdrawn = t.Key.OfferStatus == RegOfferStatus.Withdrawn,
                        DifferentiatedPositionGid = t.Key.DifferentiatedPositionGid,
                        ProcurementPlanGid = t.Key.ProcurementPlanGid,
                    })
                    .ToList();
        }
    }
}
