using Autofac.Extras.Attributed;
using Eumis.Public.Common.Localization;
using Eumis.Public.Data.ContractContracts.ViewObjects;
using Eumis.Public.Data.Core;
using Eumis.Public.Data.Linq;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.Contracts;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System;
using System.Linq;

namespace Eumis.Public.Data.ContractContracts.Repositories
{
    internal class ContractContractsRepository : Repository, IContractContractsRepository
    {
        public ContractContractsRepository([WithKey(DbKey.Umis)]IUnitOfWork uow)
            : base(uow)
        {
        }

        public PageVO<ContractContractVO> GetContractContracts(
            int? programmeId = null,
            string beneficary = null,
            string companyUin = null,
            int? errandLegalActId = null,
            int offset = 0,
            int? limit = null)
        {
            var contractPredicate = PredicateBuilder.True<Contract>();

            if (programmeId != null)
            {
                contractPredicate = contractPredicate.AndEquals(c => c.ProgrammeId, programmeId);
            }

            if (!string.IsNullOrWhiteSpace(companyUin))
            {
                contractPredicate = contractPredicate.And(c => c.CompanyUin.Contains(companyUin));
            }

            if (!string.IsNullOrWhiteSpace(beneficary))
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.Bulgarian)
                {
                    contractPredicate = contractPredicate.And(c => c.CompanyName.Contains(beneficary));
                }
                else
                {
                    contractPredicate = contractPredicate.And(c => c.CompanyNameAlt.Contains(beneficary));
                }
            }

            var errandLegalActPredicate = PredicateBuilder.True<ErrandLegalAct>();

            if (errandLegalActId != null)
            {
                errandLegalActPredicate = errandLegalActPredicate.AndEquals(c => c.ErrandLegalActId, errandLegalActId);
            }

            var oldTimeout = this.unitOfWork.DbContext.Database.CommandTimeout;
            this.unitOfWork.DbContext.Database.CommandTimeout = 60 * 5;

            var contractContracts = (from cc in this.unitOfWork.DbContext.Set<ContractContract>()

                                     join c in this.unitOfWork.DbContext.Set<Contract>().Where(contractPredicate) on cc.ContractId equals c.ContractId
                                     join ct in this.unitOfWork.DbContext.Set<CompanyType>() on c.CompanyTypeId equals ct.CompanyTypeId

                                     join cdp in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>() on cc.ContractContractId equals cdp.ContractContractId
                                     join cpp in this.unitOfWork.DbContext.Set<ContractProcurementPlan>() on cdp.ContractProcurementPlanId equals cpp.ContractProcurementPlanId
                                     join ea in this.unitOfWork.DbContext.Set<ErrandArea>() on cpp.ErrandAreaId equals ea.ErrandAreaId
                                     join la in this.unitOfWork.DbContext.Set<ErrandLegalAct>().Where(errandLegalActPredicate) on cpp.ErrandLegalActId equals la.ErrandLegalActId
                                     join et in this.unitOfWork.DbContext.Set<ErrandType>() on cpp.ErrandTypeId equals et.ErrandTypeId

                                     join ccr in this.unitOfWork.DbContext.Set<ContractContractor>() on cc.ContractContractorId equals ccr.ContractContractorId
                                     group new
                                     {
                                         ContractDifferentiatedPositionName = cdp.Name,
                                         ErrandAreaName = ea.Name,
                                         ErrandLegalActName = la.Name,
                                         ErrandTypeName = et.Name,
                                     }
                                     by new
                                     {
                                         ContractContractNumber = cc.Number,
                                         ContractProcurementPlanName = cpp.Name,
                                         ContractName = c.Name,
                                         ContractNameAlt = c.NameEN,
                                         ContractId = c.ContractId,
                                         CompanyName = c.CompanyName,
                                         CompanyNameAlt = c.CompanyNameAlt,
                                         CompanyUin = c.CompanyUinType == UinType.PersonalBulstat ? string.Empty : c.CompanyUin,
                                         CompanyUinType = c.CompanyUinType,
                                         CompanyTypeName = ct.Name,
                                         CompanyTypeNameAlt = ct.NameAlt,
                                         ContractContractorName = ccr.Name,
                                         ContractContractorNameAlt = ccr.NameAlt,
                                         ContractContractorUin = ccr.UinType == UinType.PersonalBulstat ? string.Empty : ccr.Uin,
                                         ContractContractorUinType = ccr.UinType,
                                         TotalFundedValue = cc.TotalFundedValue,
                                         ContractContractEndDate = cc.EndDate,
                                     }
                                     into g
                                     select new
                                     {
                                         ContractContractNumber = g.Key.ContractContractNumber,
                                         ContractProcurementPlanName = g.Key.ContractProcurementPlanName,
                                         ContractDifferentiatedPositions = g.Select(x => x.ContractDifferentiatedPositionName),
                                         ContractName = g.Key.ContractName,
                                         ContractNameAlt = g.Key.ContractNameAlt,
                                         ContractId = g.Key.ContractId,
                                         CompanyName = g.Key.CompanyName,
                                         CompanyNameAlt = g.Key.CompanyNameAlt,
                                         CompanyUin = g.Key.CompanyUin,
                                         CompanyUinType = g.Key.CompanyUinType,
                                         CompanyTypeName = g.Key.CompanyTypeName,
                                         CompanyTypeNameAlt = g.Key.CompanyTypeNameAlt,
                                         ContractContractorName = g.Key.ContractContractorName,
                                         ContractContractorNameAlt = g.Key.ContractContractorNameAlt,
                                         ContractContractorUin = g.Key.ContractContractorUin,
                                         ContractContractorUinType = g.Key.ContractContractorUinType,
                                         TotalFundedValue = g.Key.TotalFundedValue,
                                         ErrandAreaName = g.Select(x => x.ErrandAreaName).Min(),
                                         ErrandLegalActName = g.Select(x => x.ErrandLegalActName).Min(),
                                         ErrandTypeName = g.Select(x => x.ErrandTypeName).Min(),
                                         ContractContractEndDate = g.Key.ContractContractEndDate,
                                     })
                                     .ToList()
                                     .Select(x => new ContractContractVO()
                                     {
                                         ContractContractNumber = x.ContractContractNumber,
                                         ContractProcurementPlanName = x.ContractProcurementPlanName,
                                         ContractDifferentiatedPositions = string.Join(", ", x.ContractDifferentiatedPositions),
                                         ContractName = x.ContractName,
                                         ContractNameAlt = x.ContractNameAlt,
                                         ContractId = x.ContractId,
                                         CompanyName = x.CompanyName,
                                         CompanyNameAlt = x.CompanyNameAlt,
                                         CompanyUin = x.CompanyUin,
                                         CompanyUinType = x.CompanyUinType,
                                         CompanyTypeName = x.CompanyTypeName,
                                         CompanyTypeNameAlt = x.CompanyTypeNameAlt,
                                         ContractContractorName = x.ContractContractorName,
                                         ContractContractorNameAlt = x.ContractContractorNameAlt,
                                         ContractContractorUin = x.ContractContractorUin,
                                         ContractContractorUinType = x.ContractContractorUinType,
                                         TotalFundedValue = x.TotalFundedValue,
                                         ErrandAreaName = x.ErrandAreaName,
                                         ErrandLegalActName = x.ErrandLegalActName,
                                         ErrandTypeName = x.ErrandTypeName,
                                         ContractContractEndDate = x.ContractContractEndDate,
                                     })
                                     .ToList();

            this.unitOfWork.DbContext.Database.CommandTimeout = oldTimeout;
            var contractContractsWithOffsetAndLimit = contractContracts
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            var result = new PageVO<ContractContractVO>() { Count = contractContracts.Count(), Results = contractContractsWithOffsetAndLimit };

            return result;
        }
    }
}
