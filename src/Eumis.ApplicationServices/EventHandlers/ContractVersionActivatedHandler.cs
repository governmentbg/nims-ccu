using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eumis.ApplicationServices.Services.Company;
using Eumis.Common.Email;
using Eumis.Data.Companies.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Emails.Repositories;
using Eumis.Data.Indicators.Repositories;
using Eumis.Data.OperationalMap.Directions.Repositories;
using Eumis.Data.OperationalMap.MapNodes.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Companies;
using Eumis.Domain.Contracts;
using Eumis.Domain.Emails;
using Eumis.Domain.Events;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Services;
using Eumis.Rio;
using Newtonsoft.Json.Linq;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class ContractVersionActivatedHandler : Eumis.Domain.Core.EventHandler<ContractVersionActivatedEvent>
    {
        private IContractsRepository contractsRepository;
        private IContractVersionsRepository contractVersionsRepository;
        private ICompaniesRepository companiesRepository;
        private INomenclatureDomainService nomenclaturesDomainService;
        private ICompanyCreationService companyCreationService;
        private IEmailsRepository emailsRepository;
        private IProceduresRepository proceduresRepository;
        private IIndicatorsRepository indicatorsRepository;
        private IMapNodesRepository mapNodesRepository;
        private IDirectionsRepository directionsRepository;

        public ContractVersionActivatedHandler(
            IContractsRepository contractsRepository,
            IContractVersionsRepository contractVersionsRepository,
            ICompaniesRepository companiesRepository,
            INomenclatureDomainService nomenclaturesDomainService,
            ICompanyCreationService companyCreationService,
            IEmailsRepository emailsRepository,
            IProceduresRepository proceduresRepository,
            IIndicatorsRepository indicatorsRepository,
            IMapNodesRepository mapNodesRepository,
            IDirectionsRepository directionsRepository)
        {
            this.contractsRepository = contractsRepository;
            this.contractVersionsRepository = contractVersionsRepository;
            this.companiesRepository = companiesRepository;
            this.nomenclaturesDomainService = nomenclaturesDomainService;
            this.companyCreationService = companyCreationService;
            this.emailsRepository = emailsRepository;
            this.proceduresRepository = proceduresRepository;
            this.indicatorsRepository = indicatorsRepository;
            this.mapNodesRepository = mapNodesRepository;
            this.directionsRepository = directionsRepository;
        }

        public override void Handle(ContractVersionActivatedEvent e)
        {
            var contractVersion = this.contractVersionsRepository.FindWithIncludedAmounts(e.ContractVersionId);
            var versionDoc = contractVersion.GetDocument();

            var beneficiaryUin = versionDoc.Beneficiary.Uin;
            var beneficiaryUinType = versionDoc.Beneficiary.GetEnum<Rio.Company, UinType>(d => d.UinType.Id).Value;

            Eumis.Domain.Companies.Company newCompany = null;
            if (this.companiesRepository.IsUniqueUin(beneficiaryUin, beneficiaryUinType))
            {
                newCompany = this.companyCreationService.CreateFromRioCompany(versionDoc.Beneficiary);
                this.companiesRepository.Add(newCompany);
            }

            var nutsLevel = versionDoc.BFPContractBasicData.GetEnum<Rio.BFPContractBasicData, NutsLevel>(c => c.NutsAddress.NutsLevel.Id);

            var contract = this.contractsRepository.Find(contractVersion.ContractId);
            var procedure = this.proceduresRepository.FindWithoutIncludes(contract.ProcedureId);

            ContractExecutionStatus? contractStatus = ContractExecutionStatus.Active;
            if (versionDoc.BFPContractBasicData.CompletionStatus != null)
            {
                contractStatus = versionDoc.BFPContractBasicData.GetEnum<Rio.BFPContractBasicData, ContractExecutionStatus>(c => c.CompletionStatus.Id);
            }

            contract.UpdateContractData(
                contractVersion.RegNumber,
                newCompany == null ? contract.CompanyId : newCompany.CompanyId,
                versionDoc.Beneficiary.Name,
                versionDoc.Beneficiary.NameEN,
                beneficiaryUinType,
                beneficiaryUin,
                versionDoc.Beneficiary.GetPrivateNomId(d => d.CompanyType, this.nomenclaturesDomainService.GetCompanyTypeNomIdByGid).Value,
                versionDoc.Beneficiary.GetPrivateNomId(d => d.CompanyLegalType, this.nomenclaturesDomainService.GetCompanyLegalTypeNomIdByGid).Value,
                versionDoc.Beneficiary.CompanyContactPersonEmail,
                versionDoc.BFPContractBasicData.Name,
                versionDoc.BFPContractBasicData.NameEN,
                versionDoc.BFPContractBasicData.Description,
                versionDoc.BFPContractBasicData.DescriptionEN,
                contractStatus,
                contractVersion.VersionType == ContractVersionType.NewContract ? versionDoc.BFPContractBasicData.ContractDate : contract.ContractDate,
                versionDoc.BFPContractBasicData.StartDate,
                versionDoc.BFPContractBasicData.StartCondition,
                versionDoc.BFPContractBasicData.CompletionDate,
                versionDoc.BFPContractBasicData.TerminationDate,
                versionDoc.BFPContractBasicData.TerminationReason,
                nutsLevel,
                int.Parse(string.IsNullOrEmpty(versionDoc.BFPContractBasicData.Duration) ? "0" : versionDoc.BFPContractBasicData.Duration),
                versionDoc.Beneficiary.GetPublicNomId(d => d.KidCodeProject, this.nomenclaturesDomainService.GetKidCodeNomIdByCode),
                this.GetLocations(nutsLevel, versionDoc.BFPContractBasicData.NutsAddress.NutsAddressContentCollection),
                versionDoc.Beneficiary.GetPublicNomId(d => d.Seat.Country, this.nomenclaturesDomainService.GetCountryNomIdByCode),
                versionDoc.Beneficiary.GetPublicNomId(d => d.Seat.Settlement, this.nomenclaturesDomainService.GetSettlementNomIdByCode),
                versionDoc.Beneficiary.Seat.PostCode,
                versionDoc.Beneficiary.Seat.Street,
                versionDoc.Beneficiary.Seat.FullAddress,
                versionDoc.Beneficiary.GetPublicNomId(d => d.Correspondence.Country, this.nomenclaturesDomainService.GetCountryNomIdByCode),
                versionDoc.Beneficiary.GetPublicNomId(d => d.Correspondence.Settlement, this.nomenclaturesDomainService.GetSettlementNomIdByCode),
                versionDoc.Beneficiary.Correspondence.PostCode,
                versionDoc.Beneficiary.Correspondence.Street,
                versionDoc.Beneficiary.Correspondence.FullAddress,
                versionDoc.BFPContractDirectionsBudgetContract.BFPContractBudget.EUAmount,
                versionDoc.BFPContractDirectionsBudgetContract.BFPContractBudget.TotalAmount,
                versionDoc.BFPContractDirectionsBudgetContract.BFPContractBudget.TotalAmount,
                versionDoc.BFPContractDirectionsBudgetContract.BFPContractBudget.SelfAmount,
                versionDoc.BFPContractDirectionsBudgetContract.BFPContractBudget.TotalAmount);

            // update the partners
            if (versionDoc.Partners != null && versionDoc.Partners.PartnerCollection != null)
            {
                foreach (var partner in versionDoc.Partners.PartnerCollection)
                {
                    var c = this.companyCreationService.CreateFromRioCompany(partner);
                    contract.AddOrUpdatePartner(
                        Guid.Parse(partner.gid),
                        partner.isActive,
                        partner.FinancialContribution,
                        c.Uin,
                        c.UinType,
                        c.CompanyTypeId,
                        c.CompanyLegalTypeId,
                        c.Name,
                        c.NameAlt,
                        c.SeatCountryId,
                        c.SeatSettlementId,
                        c.SeatPostCode,
                        c.SeatStreet,
                        c.SeatAddress,
                        c.CorrCountryId,
                        c.CorrSettlementId,
                        c.CorrPostCode,
                        c.CorrStreet,
                        c.CorrAddress,
                        c.Representative,
                        c.Phone1,
                        c.Phone2,
                        c.Fax,
                        c.Email,
                        c.ContactName,
                        c.ContactPhone,
                        c.ContactEmail);
                }
            }

            // update the budget items
            var budgetTree = this.proceduresRepository.GetExpenseBudgetTree(contract.ProcedureId);
            var programmeBudgetTree = budgetTree.Programmes.Where(p => p.ProgrammeId == contract.ProgrammeId).Single();

            Dictionary<Guid, Tuple<int, string>> budgetLevel2Info = new Dictionary<Guid, Tuple<int, string>>();

            var l1s = programmeBudgetTree.Level1Items;
            int l2Index = 0;
            for (int l1Index = 0; l1Index < l1s.Count; l1Index++)
            {
                string l1Roman = this.Romanize(l1Index + 1);
                foreach (var l2 in l1s[l1Index].Level2Items)
                {
                    string l2Code = l1Roman + "." + (l2Index + 1);
                    budgetLevel2Info.Add(l2.Gid, Tuple.Create(l2.ProcedureBudgetLevel2Id, l2Code));

                    l2Index++;
                }
            }

            var level2BudgetItems = versionDoc
                .BFPContractDirectionsBudgetContract
                .BFPContractBudget
                .BFPContractProgrammeBudgetCollection
                .SelectMany(t => t.BFPContractProgrammeExpenseBudgetCollection);

            foreach (var level2BudgetItem in level2BudgetItems)
            {
                foreach (var level3BudgetItem in level2BudgetItem.BFPContractProgrammeDetailsExpenseBudgetCollection)
                {
                    int? directionId, subDirectionId;
                    directionId = subDirectionId = (int?)null;

                    if (level3BudgetItem.Direction?.DirectionItem?.Id != null)
                    {
                        directionId = this.directionsRepository.GetDirectionByGid(Guid.Parse(level3BudgetItem.Direction.DirectionItem.Id)).DirectionId;
                    }

                    if (level3BudgetItem.Direction?.SubDirection?.Id != null)
                    {
                        subDirectionId = this.directionsRepository.GetSubDirectionByGid(Guid.Parse(level3BudgetItem.Direction.SubDirection.Id)).SubDirectionId;
                    }

                    contract.AddOrUpdateBudgetItem(
                        budgetLevel2Info[Guid.Parse(level2BudgetItem.gid)].Item1,
                        int.Parse(level3BudgetItem.OrderNum),
                        Guid.Parse(level3BudgetItem.gid),
                        level3BudgetItem.isActive,
                        budgetLevel2Info[Guid.Parse(level2BudgetItem.gid)].Item2 + "." + level3BudgetItem.OrderNum,
                        level3BudgetItem.Name,
                        level3BudgetItem.GrandAmount,
                        level3BudgetItem.SelfAmount,
                        level3BudgetItem.Nuts.Code,
                        level3BudgetItem.Nuts.Name,
                        level3BudgetItem.Nuts.FullPath,
                        level3BudgetItem.Nuts.FullPathName,
                        directionId,
                        subDirectionId);

                    contractVersion.AddOrUpdateAmount(
                        contractVersion.ContractId,
                        budgetLevel2Info[Guid.Parse(level2BudgetItem.gid)].Item1,
                        int.Parse(level3BudgetItem.OrderNum),
                        Guid.Parse(level3BudgetItem.gid),
                        level3BudgetItem.isActive,
                        level3BudgetItem.Name,
                        level3BudgetItem.GrandAmount,
                        level3BudgetItem.SelfAmount,
                        level3BudgetItem.Nuts.Code,
                        level3BudgetItem.Nuts.Name,
                        level3BudgetItem.Nuts.FullPath,
                        level3BudgetItem.Nuts.FullPathName);
                }
            }

            // update the activities
            foreach (var activity in versionDoc.BFPContractContractActivities.BFPContractContractActivityCollection)
            {
                var companies = activity.CompanyCollection.Select(c => this.LookupCompanyData(versionDoc, c.Id)).ToList();

                contract.AddOrUpdateActivity(
                    Guid.Parse(activity.gid),
                    activity.isActive,
                    activity.Code,
                    activity.Name,
                    activity.ExecutionMethod,
                    activity.Result,
                    int.Parse(activity.StartMonth),
                    int.Parse(activity.Duration),
                    activity.Amount,
                    companies);
            }

            // update the indicators
            foreach (var indicator in versionDoc.BFPContractIndicators.BFPContractIndicatorCollection)
            {
                contract.AddOrUpdateIndicator(
                    Guid.Parse(indicator.gid),
                    this.indicatorsRepository.GetIndicatorIdByGid(Guid.Parse(indicator.SelectedIndicator.Id)),
                    indicator.isActive,
                    indicator.BaseTotal,
                    indicator.BaseMen,
                    indicator.BaseWomen,
                    indicator.TargetTotal,
                    indicator.TargetMen,
                    indicator.TargetWomen,
                    indicator.Description,
                    indicator.GetPrivateNomId(d => d.ProgrammePriority, this.mapNodesRepository.GetMapNodeIdByGid),
                    indicator.GetPrivateNomId(d => d.InvestmentPriority, this.mapNodesRepository.GetMapNodeIdByGid),
                    indicator.GetPrivateNomId(d => d.SpecificTarget, this.mapNodesRepository.GetMapNodeIdByGid));
            }

            if (this.contractVersionsRepository.GetContractVersions(contractVersion.ContractId).Count != 1)
            {
                Email email = new Email(
                    versionDoc.Beneficiary.Email,
                    EmailTemplate.ContractVersionActivatedMessage,
                    JObject.FromObject(
                        new
                        {
                            contractRegNumber = contract.RegNumber,
                            contractName = contract.Name,
                            contractCompanyName = contract.CompanyName,
                            contractCompanyUin = contract.CompanyUin,
                            procedureCode = versionDoc.BFPContractBasicData.Procedure.Code,
                            procedureName = versionDoc.BFPContractBasicData.Procedure.Name,
                            programmeCode = versionDoc.BFPContractBasicData.ProgrammeBasicData.Programme.Code,
                            programmeName = versionDoc.BFPContractBasicData.ProgrammeBasicData.Programme.Name,
                            isAnnex = contractVersion.VersionType == ContractVersionType.Annex ? true : false,
                        }));

                this.emailsRepository.Add(email);
            }
        }

        private IList<Tuple<string, string, string, string, string, string>> GetLocations(NutsLevel? nutsLevel, IList<NutsAddressContent> addresses)
        {
            if (!nutsLevel.HasValue)
            {
                return null;
            }

            IList<Tuple<string, string, string, string, string, string>> locations = null;

            switch (nutsLevel)
            {
                case NutsLevel.Country:
                    locations = addresses.Select(a => Tuple.Create(a.Country.Code, a.Country.Name, a.Country.FullPath, a.Country.FullPathName, a.Country.NameEN, a.Country.FullPathNameEN)).ToList();
                    break;
                case NutsLevel.RegionNUTS1:
                    locations = addresses.Select(a => Tuple.Create(a.Nuts1.Code, a.Nuts1.Name, a.Nuts1.FullPath, a.Nuts1.FullPathName, a.Nuts1.NameEN, a.Nuts1.FullPathNameEN)).ToList();
                    break;
                case NutsLevel.RegionNUTS2:
                    locations = addresses.Select(a => Tuple.Create(a.Nuts2.Code, a.Nuts2.Name, a.Nuts2.FullPath, a.Nuts2.FullPathName, a.Nuts2.NameEN, a.Nuts2.FullPathNameEN)).ToList();
                    break;
                case NutsLevel.District:
                    locations = addresses.Select(a => Tuple.Create(a.District.Code, a.District.Name, a.District.FullPath, a.District.FullPathName, a.District.NameEN, a.District.FullPathNameEN)).ToList();
                    break;
                case NutsLevel.Municipality:
                    locations = addresses.Select(a => Tuple.Create(a.Municipality.Code, a.Municipality.Name, a.Municipality.FullPath, a.Municipality.FullPathName, a.Municipality.NameEN, a.Municipality.FullPathNameEN)).ToList();
                    break;
                case NutsLevel.Settlement:
                    locations = addresses.Select(a => Tuple.Create(a.Settlement.Code, a.Settlement.Name, a.Settlement.FullPath, a.Settlement.FullPathName, a.Settlement.NameEN, a.Settlement.FullPathNameEN)).ToList();
                    break;
                case NutsLevel.ProtectedZone:
                    locations = addresses.Select(a => Tuple.Create(a.ProtectedZone.Code, a.ProtectedZone.Name, a.ProtectedZone.FullPath, a.ProtectedZone.FullPathName, a.ProtectedZone.NameEN, a.ProtectedZone.FullPathNameEN)).ToList();
                    break;
            }

            return locations;
        }

        private string GetFullPaths(NutsLevel? nutsLevel, IList<NutsAddressContent> addresses)
        {
            if (!nutsLevel.HasValue)
            {
                return null;
            }

            IList<string> codes = null;

            switch (nutsLevel)
            {
                case NutsLevel.Country:
                    codes = addresses.Select(a => a.Country.FullPath).ToList();
                    break;
                case NutsLevel.RegionNUTS1:
                    codes = addresses.Select(a => a.Nuts1.FullPath).ToList();
                    break;
                case NutsLevel.RegionNUTS2:
                    codes = addresses.Select(a => a.Nuts2.FullPath).ToList();
                    break;
                case NutsLevel.District:
                    codes = addresses.Select(a => a.District.FullPath).ToList();
                    break;
                case NutsLevel.Municipality:
                    codes = addresses.Select(a => a.Municipality.FullPath).ToList();
                    break;
                case NutsLevel.Settlement:
                    codes = addresses.Select(a => a.Settlement.FullPath).ToList();
                    break;
                case NutsLevel.ProtectedZone:
                    codes = addresses.Select(a => a.ProtectedZone.FullPath).ToList();
                    break;
            }

            return string.Join(";", codes);
        }

        private Tuple<string, UinType, string> LookupCompanyData(BFPContract doc, string uin)
        {
            // lookup the company in the beneficiary or partners to get more details
            string companyUin = null;
            UinType companyUinType = default(UinType);
            string companyName = null;

            if (doc.Beneficiary.Uin == uin)
            {
                companyUin = doc.Beneficiary.Uin;
                companyUinType = doc.Beneficiary.GetEnum<Rio.Company, UinType>(c => c.UinType.Id).Value;
                companyName = doc.Beneficiary.Name;
            }
            else if (doc.Partners != null && doc.Partners.PartnerCollection != null)
            {
                foreach (var partner in doc.Partners.PartnerCollection)
                {
                    if (partner.Uin == uin)
                    {
                        companyUin = partner.Uin;
                        companyUinType = partner.GetEnum<Rio.Company, UinType>(c => c.UinType.Id).Value;
                        companyName = partner.Name;
                        break;
                    }
                }
            }

            if (companyUin == null)
            {
                throw new Exception("Cannot find company data by uin");
            }

            return Tuple.Create(companyUin, companyUinType, companyName);
        }

        public string Romanize(int number)
        {
            var retVal = new StringBuilder(5);
            var valueMap = new SortedDictionary<int, string>
                               {
                                   { 1, "I" },
                                   { 4, "IV" },
                                   { 5, "V" },
                                   { 9, "IX" },
                                   { 10, "X" },
                                   { 40, "XL" },
                                   { 50, "L" },
                                   { 90, "XC" },
                                   { 100, "C" },
                                   { 400, "CD" },
                                   { 500, "D" },
                                   { 900, "CM" },
                                   { 1000, "M" },
                               };

            foreach (var kvp in valueMap.Reverse())
            {
                while (number >= kvp.Key)
                {
                    number -= kvp.Key;
                    retVal.Append(kvp.Value);
                }
            }

            return retVal.ToString();
        }
    }
}
