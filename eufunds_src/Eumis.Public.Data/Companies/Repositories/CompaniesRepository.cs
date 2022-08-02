using Autofac.Extras.Attributed;
using Eumis.Public.Data.Companies.ViewObjects;
using Eumis.Public.Data.Core;
using Eumis.Public.Data.Linq;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.Contracts;
using Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl;
using Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Public.Model.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Public.Data.Companies.Repositories
{
    internal class CompaniesRepository : Repository, ICompaniesRepository
    {
        public CompaniesRepository([WithKey(DbKey.Umis)]IUnitOfWork uow)
           : base(uow)
        {
        }

        public CompanyProjectsVO GetBeneficaryProjects(string companyUin)
        {
            var predicateBeneficary = PredicateBuilder.True<Contract>();

            predicateBeneficary = predicateBeneficary
                .And(c => c.CompanyUin == companyUin)
                .And(c => c.ContractStatus == ContractStatus.Entered);

            var reimbursedAmounts = from ra in this.unitOfWork.DbContext.Set<ReimbursedAmount>().Where(cra => cra.Status == ReimbursedAmountStatus.Entered && UmisRepository.ReportsReimbursements.Contains(cra.Reimbursement))
                                    group new
                                    {
                                        ra.PrincipalBfp.EuAmount,
                                        ra.PrincipalBfp.BgAmount,
                                    }
                                    by ra.ContractId into g
                                    select new
                                    {
                                        ContractId = g.Key,
                                        EuAmount = g.Sum(i => i.EuAmount),
                                        BgAmount = g.Sum(i => i.BgAmount),
                                    };

            var actuallyPaidAmounts = from pa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>().Where(pa => pa.Status == ActuallyPaidAmountStatus.Entered)
                                      group new
                                      {
                                          PaidEuAmount = pa.PaidBfpEuAmount,
                                          PaidBgAmount = pa.PaidBfpBgAmount,
                                      }
                                      by pa.ContractId into g
                                      select new
                                      {
                                          ContractId = g.Key,
                                          PaidEuAmount = g.Sum(i => i.PaidEuAmount),
                                          PaidBgAmount = g.Sum(i => i.PaidBgAmount),
                                      };

            var contractBudgetLevel3Amounts = from ba in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>().Where(ba => ba.IsActive)
                                              group new
                                              {
                                                  ContractedEuAmount = (decimal?)ba.CurrentEuAmount,
                                                  ContractedBgAmount = (decimal?)ba.CurrentBgAmount,
                                                  ContractedSelfAmount = (decimal?)ba.CurrentSelfAmount,
                                              }
                                              by ba.ContractId into g
                                              select new
                                              {
                                                  ContractId = g.Key,
                                                  ContractedEuAmount = g.Sum(i => i.ContractedEuAmount),
                                                  ContractedBgAmount = g.Sum(i => i.ContractedBgAmount),
                                                  ContractedSelfAmount = g.Sum(i => i.ContractedSelfAmount),
                                              };

            var groupedResult = from c in this.unitOfWork.DbContext.Set<Contract>().Where(predicateBeneficary)

                                join ra in reimbursedAmounts on c.ContractId equals ra.ContractId into g1
                                from ra in g1.DefaultIfEmpty()

                                join pa in actuallyPaidAmounts on c.ContractId equals pa.ContractId into g2
                                from pa in g2.DefaultIfEmpty()

                                join ba in contractBudgetLevel3Amounts on c.ContractId equals ba.ContractId into g3
                                from ba in g3.DefaultIfEmpty()

                                where c.ContractStatus == ContractStatus.Entered
                                select new
                                {
                                    ContractId = c.ContractId,
                                    ContractedEuAmount = ba.ContractedEuAmount,
                                    ContractedBgAmount = ba.ContractedBgAmount,
                                    ContractedSelfAmount = ba.ContractedSelfAmount,
                                    EuAmount = ra.EuAmount,
                                    BgAmount = ra.BgAmount,
                                    PaidEuAmount = pa.PaidEuAmount ?? 0 - ra.EuAmount ?? 0,
                                    PaidBgAmount = pa.PaidBgAmount ?? 0 - ra.BgAmount ?? 0,
                                };

            var result = (from bc in groupedResult
                          join c in this.unitOfWork.DbContext.Set<Contract>() on bc.ContractId equals c.ContractId
                          select new ContractVO
                          {
                              ContractId = c.ContractId,
                              Name = c.Name,
                              NameEN = c.NameEN,
                              CompanyName = c.CompanyName,
                              CompanyNameAlt = c.CompanyNameAlt,
                              CompanyUin = c.CompanyUin,
                              CompanyUinType = c.CompanyUinType,
                              StartDate = c.StartDate,
                              CompletionDate = c.CompletionDate,
                              MonthsDuration = c.Duration,
                              BeneficiarySeatCountryId = c.BeneficiarySeatCountryId,
                              BeneficiarySeatAddress = c.BeneficiarySeatAddress,
                              BeneficiarySeatPostCode = c.BeneficiarySeatPostCode,
                              BeneficiarySeatStreet = c.BeneficiarySeatStreet,
                              NutsLevel = c.NutsLevel,
                              RegNumber = c.RegNumber,
                              ExecutionStatus = c.ExecutionStatus,
                              ContractedEuAmount = bc.ContractedEuAmount ?? 0m,
                              ContractedBgAmount = bc.ContractedBgAmount ?? 0m,
                              ContractedSelfAmount = bc.ContractedSelfAmount ?? 0m,
                              PaidEuAmount = bc.PaidEuAmount,
                              PaidBgAmount = bc.PaidBgAmount,
                              IsHistoric = false,
                          })
                    .OrderBy(x => x.StartDate)
                    .ToList();

            return new CompanyProjectsVO()
            {
                BeneficaryProjects = result,
                ContractorContractsCount = this.GetCompanyContractContractsCount(companyUin),
                PartnerContractsCount = this.GetCompanyContractPartnersCount(companyUin),
                SubcontractorContractsCount = this.GetcompanyContractContractsCount(companyUin, ContractSubcontractType.Subcontractor),
                MemberContractsCount = this.GetcompanyContractContractsCount(companyUin, ContractSubcontractType.Subcontractor),
            };
        }

        public CompanyProjectsVO GetContractorProjects(string uin)
        {
            var contractorProjects = from ccr in this.unitOfWork.DbContext.Set<ContractContractor>().Where(x => x.Uin == uin)
                                     join cc in this.unitOfWork.DbContext.Set<ContractContract>() on ccr.ContractContractorId equals cc.ContractContractorId
                                     join c in this.unitOfWork.DbContext.Set<Contract>().Where(x => x.ContractStatus == ContractStatus.Entered) on cc.ContractId equals c.ContractId
                                     group cc.TotalFundedValue by new { c.ContractId, c.RegNumber, c.Name, c.NameEN } into g
                                     select new ProjectDetailsVO
                                     {
                                         ContractId = g.Key.ContractId,
                                         RegNumber = g.Key.RegNumber,
                                         Name = g.Key.Name,
                                         NameEN = g.Key.NameEN,
                                         ContractAmount = g.Sum(),
                                         ContractCount = g.Count(),
                                     };

            return new CompanyProjectsVO()
            {
                BeneficaryContractsCount = this.GetCompanyContractBeneficariesCount(uin),
                ContractorProjects = contractorProjects.ToList(),
                PartnerContractsCount = this.GetCompanyContractPartnersCount(uin),
                SubcontractorContractsCount = this.GetcompanyContractContractsCount(uin, ContractSubcontractType.Subcontractor),
                MemberContractsCount = this.GetcompanyContractContractsCount(uin, ContractSubcontractType.Subcontractor),
            };
        }

        public CompanyProjectsVO GetPartnerProjects(string uin)
        {
            var partnerProjects = from pc in this.unitOfWork.DbContext.Set<ContractPartner>().Where(x => x.Uin == uin && x.IsActive)
                                  join c in this.unitOfWork.DbContext.Set<Contract>() on pc.ContractId equals c.ContractId
                                  select new PartnerProjectsVO
                                  {
                                      ContractId = c.ContractId,
                                      Name = c.Name,
                                      NameEN = c.NameEN,
                                      StartDate = c.StartDate,
                                      CompletionDate = c.CompletionDate,
                                      RegNumber = c.RegNumber,
                                      ExecutionStatus = c.ExecutionStatus,
                                      IsHistoric = false,
                                  };

            return new CompanyProjectsVO()
            {
                BeneficaryContractsCount = this.GetCompanyContractBeneficariesCount(uin),
                ContractorContractsCount = this.GetCompanyContractContractsCount(uin),
                PartnerProjects = partnerProjects.ToList(),
                SubcontractorContractsCount = this.GetcompanyContractContractsCount(uin, ContractSubcontractType.Subcontractor),
                MemberContractsCount = this.GetcompanyContractContractsCount(uin, ContractSubcontractType.Subcontractor),
            };
        }

        public CompanyProjectsVO GetSubcontractorProjects(string uin)
        {
            return new CompanyProjectsVO()
            {
                BeneficaryContractsCount = this.GetCompanyContractBeneficariesCount(uin),
                ContractorContractsCount = this.GetCompanyContractContractsCount(uin),
                PartnerContractsCount = this.GetCompanyContractPartnersCount(uin),
                SubcontractorProjects = this.GetContractSucontractProjects(uin, ContractSubcontractType.Subcontractor),
                MemberContractsCount = this.GetcompanyContractContractsCount(uin, ContractSubcontractType.Subcontractor),
            };
        }

        public CompanyProjectsVO GetMemberProjects(string uin)
        {
            return new CompanyProjectsVO()
            {
                BeneficaryContractsCount = this.GetCompanyContractBeneficariesCount(uin),
                ContractorContractsCount = this.GetCompanyContractContractsCount(uin),
                PartnerContractsCount = this.GetCompanyContractPartnersCount(uin),
                SubcontractorContractsCount = this.GetcompanyContractContractsCount(uin, ContractSubcontractType.Subcontractor),
                MemberProjects = this.GetContractSucontractProjects(uin, ContractSubcontractType.Subcontractor),
            };
        }

        private IList<ProjectDetailsVO> GetContractSucontractProjects(string uin, ContractSubcontractType type)
        {
            return (from cs in this.unitOfWork.DbContext.Set<ContractSubcontract>().Where(x => x.Type == type)
                    join cc in this.unitOfWork.DbContext.Set<ContractContractor>().Where(x => x.Uin == uin && x.IsActive) on cs.ContractContractorId equals cc.ContractContractorId
                    join c in this.unitOfWork.DbContext.Set<Contract>().Where(x => x.ContractStatus == ContractStatus.Entered) on cc.ContractId equals c.ContractId
                    group cs.Amount by new { c.ContractId, c.RegNumber, c.Name, c.NameEN } into g
                    select new ProjectDetailsVO
                    {
                        ContractId = g.Key.ContractId,
                        RegNumber = g.Key.RegNumber,
                        Name = g.Key.Name,
                        NameEN = g.Key.NameEN,
                        ContractAmount = g.Sum(),
                        ContractCount = g.Count(),
                    }).ToList();
        }

        private int GetCompanyContractBeneficariesCount(string uin)
        {
            return this.unitOfWork.DbContext.Set<Contract>()
                .Where(x => x.CompanyUin == uin && x.ContractStatus == ContractStatus.Entered)
                .Count();
        }

        private int GetCompanyContractContractsCount(string uin)
        {
            return (from cc in this.unitOfWork.DbContext.Set<ContractContractor>().Where(x => x.Uin == uin && x.IsActive)
                    join ccc in this.unitOfWork.DbContext.Set<ContractContract>() on cc.ContractContractorId equals ccc.ContractContractorId
                    select ccc.ContractId).Count();
        }

        private int GetCompanyContractPartnersCount(string uin)
        {
            return this.unitOfWork.DbContext.Set<ContractPartner>()
                .Where(x => x.Uin == uin && x.IsActive)
                .Count();
        }

        private int GetcompanyContractContractsCount(string uin, ContractSubcontractType type)
        {
            return (from cc in this.unitOfWork.DbContext.Set<ContractContractor>().Where(x => x.Uin == uin && x.IsActive)
                    join ccc in this.unitOfWork.DbContext.Set<ContractSubcontract>().Where(x => x.Type == type) on cc.ContractContractorId equals ccc.ContractContractorId
                    select cc.ContractId).Count();
        }
    }
}
