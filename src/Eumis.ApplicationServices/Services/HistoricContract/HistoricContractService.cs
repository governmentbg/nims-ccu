using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.HistoricContracts;
using Eumis.Domain.HistoricContracts.DataObjects;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Eumis.ApplicationServices.Services.HistoricContract
{
    public class HistoricContractService : IHistoricContractService
    {
        private static readonly string[] CompanyTypeCodes = new string[]
        {
            "DARJAVNO",
            "OOD",
            "ET",
            "AD",
            "SD",
            "EAD",
            "KD",
            "KDA",
            "KOOP",
            "KPS",
            "KPF",
            "KPP",
            "MKPP",
            "EOOD",
            "NA",
            "BUDGET",
            "GORSKO",
            "LOVNO",
            "UL_SDR",
            "NAR_CH",
            "REL_ORG",
            "ZNP",
            "FZNZ",
            "ZP",
            "DZZD",
            "CHU",
            "FON",
        };

        private IUnitOfWork unitOfWork;
        private IProceduresRepository proceduresRepository;
        private IEntityCodeNomsRepository<Country, EntityCodeNomVO> countryNomsRepository;
        private INuts1NomsRepository nuts1NomsRepository;
        private INuts2NomsRepository nuts2NomsRepository;
        private IDistrictNomsRepository districtNomsRepository;
        private IMunicipalityNomsRepository municipalityNomsRepository;
        private ISettlementNomsRepository settlementNomsRepository;
        private IProtectedZoneNomsRepository protectedZoneNomsRepository;

        private List<HistoricContractErrorDO> errorList;
        private IList<Domain.HistoricContracts.HistoricContractRequest> newHistoricContractRequest;

        public HistoricContractService(
            IProceduresRepository proceduresRepository,
            IEntityCodeNomsRepository<Country, EntityCodeNomVO> countryNomsRepository,
            INuts1NomsRepository nuts1NomsRepository,
            INuts2NomsRepository nuts2NomsRepository,
            IDistrictNomsRepository districtNomsRepository,
            IMunicipalityNomsRepository municipalityNomsRepository,
            ISettlementNomsRepository settlementNomsRepository,
            IProtectedZoneNomsRepository protectedZoneNomsRepository,
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.proceduresRepository = proceduresRepository;
            this.countryNomsRepository = countryNomsRepository;
            this.nuts1NomsRepository = nuts1NomsRepository;
            this.nuts2NomsRepository = nuts2NomsRepository;
            this.districtNomsRepository = districtNomsRepository;
            this.municipalityNomsRepository = municipalityNomsRepository;
            this.settlementNomsRepository = settlementNomsRepository;
            this.protectedZoneNomsRepository = protectedZoneNomsRepository;
        }

        public IList<HistoricContractErrorDO> UpdateHistoricContracts(List<HistoricContractDO> historicContracts)
        {
            DateTime startDate = DateTime.Now;

            this.CanUpdate(historicContracts);

            if (this.errorList.Any())
            {
                this.newHistoricContractRequest = new List<HistoricContractRequest> { new HistoricContractRequest(startDate, DateTime.Now, HttpStatusCode.BadRequest.ToString(), JsonConvert.SerializeObject(this.errorList), 0, JsonConvert.SerializeObject(historicContracts)) };
            }
            else
            {
                this.EditHistoricContracts(historicContracts);

                this.newHistoricContractRequest = new List<HistoricContractRequest> { new HistoricContractRequest(startDate, DateTime.Now, HttpStatusCode.OK.ToString(), string.Empty, historicContracts.Count, JsonConvert.SerializeObject(historicContracts)) };
            }

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                this.unitOfWork.BulkInsert<Eumis.Domain.HistoricContracts.HistoricContractRequest>(this.newHistoricContractRequest);
                transaction.Commit();
            }

            return this.errorList;
        }

        public void CanUpdate(List<HistoricContractDO> historicContracts)
        {
            this.errorList = new List<HistoricContractErrorDO>();
            var contractCount = 0;

            foreach (var historicContract in historicContracts)
            {
                contractCount++;

                if (string.IsNullOrWhiteSpace(historicContract.RegNumber))
                {
                    this.errorList.Add(new HistoricContractErrorDO("INVALID_REG_NUMBER", $"RegNumber is not valid in contract {contractCount}"));
                }

                if (!historicContract.ModifyDate.HasValue)
                {
                    this.errorList.Add(new HistoricContractErrorDO("INVALID_MODIFY_DATE", $"ModifyDate is null in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                }

                if (string.IsNullOrWhiteSpace(historicContract.ProcedureCode))
                {
                    this.errorList.Add(new HistoricContractErrorDO("INVALID_PROCEDURE", $"ProcedureCode is not valid in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                }
                else
                {
                    try
                    {
                        historicContract.ProcedureId = this.proceduresRepository.FindByCode(historicContract.ProcedureCode).ProcedureId;
                    }
                    catch
                    {
                        this.errorList.Add(new HistoricContractErrorDO("INVALID_PROCEDURE", $"Not found procedure with code {historicContract.ProcedureCode} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                    }
                }

                if (string.IsNullOrWhiteSpace(historicContract.CompanyUin))
                {
                    this.errorList.Add(new HistoricContractErrorDO("INVALID_COMPANY_UIN", $"CompanyUin is not valid in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                }

                if (!historicContract.CompanyUinType.HasValue || !Enum.IsDefined(typeof(UinType), historicContract.CompanyUinType))
                {
                    this.errorList.Add(new HistoricContractErrorDO("INVALID_COMPANY_UIN_TYPE", $"CompanyUinType is not a valid value in contract {contractCount} (with registration number {historicContract.RegNumber}). Please choose among: 0 - UIC; 1 - BULSTAT; 2 - BULSTAT for Liberal Professions (PIN); 3 - Foreign companies."));
                }

                if (!CompanyTypeCodes.Any(e => string.Equals(e, historicContract.CompanyTypeCode, StringComparison.OrdinalIgnoreCase)))
                {
                    this.errorList.Add(new HistoricContractErrorDO("INVALID_COMPANY_TYPE_CODE", $"CompanyTypeCode is not a valid value in contract {contractCount} (with registration number {historicContract.RegNumber}). Please choose among: {string.Join(", ", CompanyTypeCodes)}."));
                }

                if (string.IsNullOrWhiteSpace(historicContract.SeatCountryCode))
                {
                    this.errorList.Add(new HistoricContractErrorDO("INVALID_SEAT_COUNTRY_CODE", $"SeatCountryCode is null or empty in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                }

                if (historicContract.ExecutionStatus.HasValue && !Enum.IsDefined(typeof(HistoricContractExecutionStatus), historicContract.ExecutionStatus))
                {
                    this.errorList.Add(new HistoricContractErrorDO("INVALID_EXECUTION_STATUS", $"ExecutionStatus is not a valid value in contract {contractCount} (with registration number {historicContract.RegNumber}). Please choose among 1, 2, 3, 4, 5, 6, 7."));
                }

                if (historicContract.NutsLevel.HasValue && !Enum.IsDefined(typeof(NutsLevel), historicContract.NutsLevel))
                {
                    this.errorList.Add(new HistoricContractErrorDO("INVALID_NUTS_LEVEL", $"NutsLevel is not a valid value in contract {contractCount} (with registration number {historicContract.RegNumber}). Please choose among 1, 2, 3, 4, 5, 6, 7."));
                    continue;
                }

                for (int i = 0; i < historicContract.Locations.Count; i++)
                {
                    var location = historicContract.Locations[i];

                    switch (historicContract.NutsLevel)
                    {
                        case NutsLevel.Country:
                            if (string.IsNullOrWhiteSpace(location.CountryCode))
                            {
                                this.errorList.Add(new HistoricContractErrorDO("INVALID_LOCATION", $"Country code is required for location {i + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                            }

                            break;

                        case NutsLevel.ProtectedZone:
                            if (string.IsNullOrWhiteSpace(location.CountryCode) ||
                                string.IsNullOrWhiteSpace(location.ProtectedZoneCode))
                            {
                                this.errorList.Add(new HistoricContractErrorDO("INVALID_LOCATION", $"Country and protected zone codes are required for location {i + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                            }

                            break;

                        case NutsLevel.RegionNUTS1:
                            if (string.IsNullOrWhiteSpace(location.CountryCode) ||
                                string.IsNullOrWhiteSpace(location.Nuts1Code))
                            {
                                this.errorList.Add(new HistoricContractErrorDO("INVALID_LOCATION", $"Country and nuts1 codes are required for location {i + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                            }

                            break;

                        case NutsLevel.RegionNUTS2:
                            if (string.IsNullOrWhiteSpace(location.CountryCode) ||
                                string.IsNullOrWhiteSpace(location.Nuts1Code) ||
                                string.IsNullOrWhiteSpace(location.Nuts2Code))
                            {
                                this.errorList.Add(new HistoricContractErrorDO("INVALID_LOCATION", $"Country, nuts1 and nuts2 codes are required for location {i + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                            }

                            break;

                        case NutsLevel.District:
                            if (string.IsNullOrWhiteSpace(location.CountryCode) ||
                                string.IsNullOrWhiteSpace(location.Nuts1Code) ||
                                string.IsNullOrWhiteSpace(location.Nuts2Code) ||
                                string.IsNullOrWhiteSpace(location.DistrictCode))
                            {
                                this.errorList.Add(new HistoricContractErrorDO("INVALID_LOCATION", $"Country, nuts1, nuts2 and district codes are required for location {i + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                            }

                            break;

                        case NutsLevel.Municipality:
                            if (string.IsNullOrWhiteSpace(location.CountryCode) ||
                                string.IsNullOrWhiteSpace(location.Nuts1Code) ||
                                string.IsNullOrWhiteSpace(location.Nuts2Code) ||
                                string.IsNullOrWhiteSpace(location.DistrictCode) ||
                                string.IsNullOrWhiteSpace(location.MunicipalityCode))
                            {
                                this.errorList.Add(new HistoricContractErrorDO("INVALID_LOCATION", $"Country, nuts1, nuts2, district and municipality codes are required for location {i + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                            }

                            break;

                        case NutsLevel.Settlement:
                            if (string.IsNullOrWhiteSpace(location.CountryCode) ||
                                string.IsNullOrWhiteSpace(location.Nuts1Code) ||
                                string.IsNullOrWhiteSpace(location.Nuts2Code) ||
                                string.IsNullOrWhiteSpace(location.DistrictCode) ||
                                string.IsNullOrWhiteSpace(location.MunicipalityCode) ||
                                string.IsNullOrWhiteSpace(location.SettlementCode))
                            {
                                this.errorList.Add(new HistoricContractErrorDO("INVALID_LOCATION", $"Country, nuts1, nuts2, district, municipality and settlement codes are required for location {i + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                            }

                            break;

                        default:
                            this.errorList.Add(new HistoricContractErrorDO("INVALID_LOCATION", $"Uknown nuts level in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                            break;
                    }
                }

                for (int i = 0; i < historicContract.Activities.Count; i++)
                {
                    var activity = historicContract.Activities[i];

                    if (string.IsNullOrWhiteSpace(activity.Activity))
                    {
                        this.errorList.Add(new HistoricContractErrorDO("INVALID_ACTIVITY", $"Activity is not valid for activity {i + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                    }
                }

                for (int i = 0; i < historicContract.Partners.Count; i++)
                {
                    var partner = historicContract.Partners[i];

                    if (!partner.PartnerType.HasValue || !Enum.IsDefined(typeof(HistoricContractPartnerType), partner.PartnerType))
                    {
                        this.errorList.Add(new HistoricContractErrorDO("INVALID_PARTNER", $"PartnerType is not valid for partner {i + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                    }

                    if (string.IsNullOrWhiteSpace(partner.PartnerName))
                    {
                        this.errorList.Add(new HistoricContractErrorDO("INVALID_PARTNER", $"PartnerName is not valid for partner {i + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                    }

                    if (string.IsNullOrWhiteSpace(partner.PartnerNameEn))
                    {
                        this.errorList.Add(new HistoricContractErrorDO("INVALID_PARTNER", $"PartnerNameEn is not valid for partner {i + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                    }

                    if (string.IsNullOrWhiteSpace(partner.PartnerUin))
                    {
                        this.errorList.Add(new HistoricContractErrorDO("INVALID_PARTNER", $"PartnerUin is not valid for partner {i + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                    }

                    if (!partner.PartnerUinType.HasValue || !Enum.IsDefined(typeof(UinType), partner.PartnerUinType))
                    {
                        this.errorList.Add(new HistoricContractErrorDO("INVALID_PARTNER", $"PartnerUinType is not valid for partner {i + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                    }

                    if (string.IsNullOrWhiteSpace(partner.SeatCountryCode))
                    {
                        this.errorList.Add(new HistoricContractErrorDO("INVALID_PARTNER", $"SeatCountryCode is not valid for partner {i + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                    }

                    if (string.IsNullOrWhiteSpace(partner.SeatSettlementCode))
                    {
                        this.errorList.Add(new HistoricContractErrorDO("INVALID_PARTNER", $"SeatSettlementCode is not valid for partner {i + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                    }
                }

                for (int k = 0; k < historicContract.ProcurementPlans.Count; k++)
                {
                    var procurementPlan = historicContract.ProcurementPlans[k];

                    if (string.IsNullOrWhiteSpace(procurementPlan.ProcurementPlanName))
                    {
                        this.errorList.Add(new HistoricContractErrorDO("INVALID_PROCUREMENT_PLAN", $"ProcurementPlanName is not valid for procurement plan {k + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                    }

                    if (!procurementPlan.Amount.HasValue)
                    {
                        this.errorList.Add(new HistoricContractErrorDO("INVALID_PROCUREMENT_PLAN", $"Amount is null for procurement plan {k + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                    }

                    for (int i = 0; i < procurementPlan.PositionNames.Count; i++)
                    {
                        var positionName = procurementPlan.PositionNames[i];

                        if (string.IsNullOrWhiteSpace(positionName.PositionName))
                        {
                            this.errorList.Add(new HistoricContractErrorDO("INVALID_PROCUREMENT_PLAN_POSITION_NAME", $"PositionName is not valid for position {i + 1} for procurement plan {k + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                        }
                    }
                }

                for (int i = 0; i < historicContract.ContractedAmounts.Count; i++)
                {
                    var amount = historicContract.ContractedAmounts[i];

                    if (!amount.ContractedDate.HasValue)
                    {
                        this.errorList.Add(new HistoricContractErrorDO("INVALID_CONTRACTED_AMOUNT", $"ContractedDate is null for ContractedAmount {i + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                    }
                }

                for (int i = 0; i < historicContract.ActuallyPaidAmounts.Count; i++)
                {
                    var apAmount = historicContract.ActuallyPaidAmounts[i];

                    if (!apAmount.PaymentDate.HasValue)
                    {
                        this.errorList.Add(new HistoricContractErrorDO("INVALID_ACTUALLY_PAID_AMOUNT", $"PaymentDate is null for ActuallyPaidAmount {i + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                    }
                }

                for (int i = 0; i < historicContract.ReimbursedAmounts.Count; i++)
                {
                    var reimbursed = historicContract.ReimbursedAmounts[i];

                    if (!reimbursed.ReimbursementDate.HasValue)
                    {
                        this.errorList.Add(new HistoricContractErrorDO("INVALID_REIMBURSED_AMOUNT", $"ReimbursementDate is null for ReimbursedAmount {i + 1} in contract {contractCount} (with registration number {historicContract.RegNumber})"));
                    }
                }
            }
        }

        public void EditHistoricContracts(List<HistoricContractDO> historicContracts)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                this.unitOfWork.BulkDelete<Domain.HistoricContracts.HistoricContractActivity>(i => true);
                this.unitOfWork.BulkDelete<Domain.HistoricContracts.HistoricContractLocation>(i => true);
                this.unitOfWork.BulkDelete<Domain.HistoricContracts.HistoricContractPartner>(i => true);
                this.unitOfWork.BulkDelete<Domain.HistoricContracts.HistoricContractProcurementPlanPosition>(i => true);
                this.unitOfWork.BulkDelete<Domain.HistoricContracts.HistoricContractProcurementPlan>(i => true);
                this.unitOfWork.BulkDelete<Domain.HistoricContracts.HistoricContractContractedAmount>(i => true);
                this.unitOfWork.BulkDelete<Domain.HistoricContracts.HistoricContractActuallyPaidAmount>(i => true);
                this.unitOfWork.BulkDelete<Domain.HistoricContracts.HistoricContractReimbursedAmount>(i => true);
                this.unitOfWork.BulkDelete<Domain.HistoricContracts.HistoricContract>(i => true);

                IList<Domain.HistoricContracts.HistoricContract> newHistoricContracts = new List<Eumis.Domain.HistoricContracts.HistoricContract>();
                IList<Domain.HistoricContracts.HistoricContractActivity> newHistoricContractActivities = new List<Eumis.Domain.HistoricContracts.HistoricContractActivity>();
                IList<Domain.HistoricContracts.HistoricContractLocation> newHistoricContractLocations = new List<Eumis.Domain.HistoricContracts.HistoricContractLocation>();
                IList<Domain.HistoricContracts.HistoricContractPartner> newHistoricContractPartners = new List<Eumis.Domain.HistoricContracts.HistoricContractPartner>();
                IList<Domain.HistoricContracts.HistoricContractProcurementPlan> newHistoricContractProcurementPlans = new List<Eumis.Domain.HistoricContracts.HistoricContractProcurementPlan>();
                IList<Domain.HistoricContracts.HistoricContractProcurementPlanPosition> newHistoricContractProcurementPlanPositions = new List<Eumis.Domain.HistoricContracts.HistoricContractProcurementPlanPosition>();
                IList<Domain.HistoricContracts.HistoricContractContractedAmount> newHistoricContractContractedAmounts = new List<Eumis.Domain.HistoricContracts.HistoricContractContractedAmount>();
                IList<Domain.HistoricContracts.HistoricContractActuallyPaidAmount> newHistoricContractActuallyPaidAmounts = new List<Eumis.Domain.HistoricContracts.HistoricContractActuallyPaidAmount>();
                IList<Domain.HistoricContracts.HistoricContractReimbursedAmount> newHistoricContractReimbursedAmounts = new List<Eumis.Domain.HistoricContracts.HistoricContractReimbursedAmount>();

                foreach (var historicContract in historicContracts)
                {
                    switch (historicContract.CompanyTypeCode.ToUpper())
                    {
                        case "UL_SDR":
                            historicContract.CompanyTypeId = 1;
                            historicContract.CompanyLegalTypeId = 16;
                            break;
                        case "FON":
                            historicContract.CompanyTypeId = 1;
                            historicContract.CompanyLegalTypeId = 18;
                            break;
                        case "NAR_CH":
                            historicContract.CompanyTypeId = 1;
                            historicContract.CompanyLegalTypeId = 20;
                            break;
                        /*case "REL_ORG":
                            historicContract.CompanyTypeId = 1;
                            historicContract.CompanyLegalTypeId = ;
                            break;
                        case "CHU":
                            historicContract.CompanyTypeId = 2;
                            historicContract.CompanyLegalTypeId = ;
                            break;*/
                        case "BUDGET":
                            historicContract.CompanyTypeId = 3;
                            historicContract.CompanyLegalTypeId = 6;
                            break;
                        case "DARJAVNO":
                        case "KOOP":
                        case "KPS":
                        case "KPF":
                        case "KPP":
                        case "MKPP":
                        case "GORSKO":
                        case "LOVNO":
                        case "ZNP":
                        case "DZZD":
                            historicContract.CompanyTypeId = 4;
                            historicContract.CompanyLegalTypeId = 15;
                            break;
                        case "ET":
                            historicContract.CompanyTypeId = 4;
                            historicContract.CompanyLegalTypeId = 11;
                            break;
                        case "AD":
                            historicContract.CompanyTypeId = 4;
                            historicContract.CompanyLegalTypeId = 7;
                            break;
                        case "EAD":
                            historicContract.CompanyTypeId = 4;
                            historicContract.CompanyLegalTypeId = 8;
                            break;
                        case "OOD":
                            historicContract.CompanyTypeId = 4;
                            historicContract.CompanyLegalTypeId = 9;
                            break;
                        case "EOOD":
                            historicContract.CompanyTypeId = 4;
                            historicContract.CompanyLegalTypeId = 10;
                            break;
                        case "KD":
                            historicContract.CompanyTypeId = 4;
                            historicContract.CompanyLegalTypeId = 12;
                            break;
                        case "KDA":
                            historicContract.CompanyTypeId = 4;
                            historicContract.CompanyLegalTypeId = 13;
                            break;
                        case "SD":
                            historicContract.CompanyTypeId = 4;
                            historicContract.CompanyLegalTypeId = 14;
                            break;
                        case "FZNZ":
                        case "ZP":
                            historicContract.CompanyTypeId = 6;
                            historicContract.CompanyLegalTypeId = 31;
                            break;
                        case "NA":
                            historicContract.CompanyTypeId = 6;
                            historicContract.CompanyLegalTypeId = 33;
                            break;
                        default:
                            historicContract.CompanyTypeId = 4;
                            historicContract.CompanyLegalTypeId = 15;
                            break;
                    }

                    var newHistoricContract = new Eumis.Domain.HistoricContracts.HistoricContract(
                        historicContract.ProcedureId,
                        historicContract.ModifyDate.Value,
                        historicContract.RegNumber,
                        historicContract.Name,
                        historicContract.NameEN,
                        historicContract.Description,
                        historicContract.DescriptionEN,
                        historicContract.CompanyName,
                        historicContract.CompanyNameEn,
                        historicContract.CompanyUin,
                        historicContract.CompanyUinType.Value,
                        historicContract.CompanyTypeId,
                        historicContract.CompanyLegalTypeId,
                        historicContract.SeatCountryCode,
                        historicContract.SeatSettlementCode,
                        historicContract.SeatPostCode,
                        historicContract.SeatStreet,
                        historicContract.SeatAddress,
                        historicContract.ContractDate,
                        historicContract.StartDate,
                        historicContract.CompletionDate,
                        historicContract.ExecutionStatus,
                        historicContract.NutsLevel);

                    newHistoricContracts.Add(newHistoricContract);

                    foreach (var activity in historicContract.Activities)
                    {
                        var newHistoricContractActivity = new Eumis.Domain.HistoricContracts.HistoricContractActivity(
                            newHistoricContract.HistoricContractId,
                            activity.Activity);
                        newHistoricContractActivities.Add(newHistoricContractActivity);
                    }

                    foreach (var location in historicContract.Locations)
                    {
                        string fullPath = string.Empty;
                        string fullPathName = string.Empty;

                        switch (historicContract.NutsLevel)
                        {
                            case NutsLevel.Country:
                                fullPath = location.CountryCode.Trim();
                                fullPathName = this.countryNomsRepository.GetNom(this.countryNomsRepository.GetNomIdByCode(location.CountryCode.Trim())).Name;
                                break;
                            case NutsLevel.ProtectedZone:
                                var protectedZone = this.protectedZoneNomsRepository.GetNom(this.protectedZoneNomsRepository.GetNomIdByCode(location.ProtectedZoneCode.Trim()));
                                fullPath = protectedZone.FullPath;
                                fullPathName = protectedZone.FullPathName;
                                break;
                            case NutsLevel.RegionNUTS1:
                                var nuts1 = this.nuts1NomsRepository.GetNom(this.nuts1NomsRepository.GetNomIdByCode(location.Nuts1Code.Trim()));
                                fullPath = nuts1.FullPath;
                                fullPathName = nuts1.FullPathName;
                                break;
                            case NutsLevel.RegionNUTS2:
                                var nuts2 = this.nuts2NomsRepository.GetNom(this.nuts2NomsRepository.GetNomIdByCode(location.Nuts2Code.Trim()));
                                fullPath = nuts2.FullPath;
                                fullPathName = nuts2.FullPathName;
                                break;
                            case NutsLevel.District:
                                var district = this.districtNomsRepository.GetNom(this.districtNomsRepository.GetNomIdByCode(location.DistrictCode.Trim()));
                                fullPath = district.FullPath;
                                fullPathName = district.FullPathName;
                                break;
                            case NutsLevel.Municipality:
                                var municipality = this.municipalityNomsRepository.GetNom(this.municipalityNomsRepository.GetNomIdByCode(location.MunicipalityCode.Trim()));
                                fullPath = municipality.FullPath;
                                fullPathName = municipality.FullPathName;
                                break;
                            case NutsLevel.Settlement:
                                var settlement = this.settlementNomsRepository.GetNom(this.settlementNomsRepository.GetNomIdByCode(location.SettlementCode.Trim()));
                                fullPath = settlement.FullPath;
                                fullPathName = settlement.FullPathName;
                                break;
                        }

                        var newHistoricContractLocation = new Eumis.Domain.HistoricContracts.HistoricContractLocation(
                            newHistoricContract.HistoricContractId,
                            location.CountryCode,
                            location.ProtectedZoneCode,
                            location.Nuts1Code,
                            location.Nuts2Code,
                            location.DistrictCode,
                            location.MunicipalityCode,
                            location.SettlementCode,
                            fullPath,
                            fullPathName);
                        newHistoricContractLocations.Add(newHistoricContractLocation);
                    }

                    foreach (var partner in historicContract.Partners)
                    {
                        var newHistoricContractPartner = new Eumis.Domain.HistoricContracts.HistoricContractPartner(
                            newHistoricContract.HistoricContractId,
                            partner.PartnerType.Value,
                            partner.PartnerName,
                            partner.PartnerNameEn,
                            partner.PartnerUin,
                            partner.PartnerUinType.Value,
                            4,
                            15,
                            partner.SeatCountryCode,
                            partner.SeatSettlementCode,
                            partner.SeatPostCode,
                            partner.SeatStreet,
                            partner.SeatAddress);
                        newHistoricContractPartners.Add(newHistoricContractPartner);
                    }

                    foreach (var procurementPlan in historicContract.ProcurementPlans)
                    {
                        var newHistoricContractProcurementPlan = new Eumis.Domain.HistoricContracts.HistoricContractProcurementPlan(
                            newHistoricContract.HistoricContractId,
                            procurementPlan.ProcurementPlanName,
                            procurementPlan.Amount.Value);
                        newHistoricContractProcurementPlans.Add(newHistoricContractProcurementPlan);

                        foreach (var name in procurementPlan.PositionNames)
                        {
                            var newHistoricContractProcurementPlanPosition = new Eumis.Domain.HistoricContracts.HistoricContractProcurementPlanPosition(
                                newHistoricContractProcurementPlan.HistoricContractProcurementPlanId,
                                name.PositionName);
                            newHistoricContractProcurementPlanPositions.Add(newHistoricContractProcurementPlanPosition);
                        }
                    }

                    foreach (var contractedAmount in historicContract.ContractedAmounts)
                    {
                        if (historicContract.ContractedAmounts.IndexOf(contractedAmount) == historicContract.ContractedAmounts.Count - 1)
                        {
                            var newHistoricContractContractedAmount = new Eumis.Domain.HistoricContracts.HistoricContractContractedAmount(
                                newHistoricContract.HistoricContractId,
                                contractedAmount.ContractedDate.Value,
                                contractedAmount.ContractedEuAmount,
                                contractedAmount.ContractedBgAmount,
                                contractedAmount.ContractedSeftAmount,
                                true);
                            newHistoricContractContractedAmounts.Add(newHistoricContractContractedAmount);
                        }
                        else
                        {
                            var newHistoricContractContractedAmount = new Eumis.Domain.HistoricContracts.HistoricContractContractedAmount(
                                newHistoricContract.HistoricContractId,
                                contractedAmount.ContractedDate.Value,
                                contractedAmount.ContractedEuAmount,
                                contractedAmount.ContractedBgAmount,
                                contractedAmount.ContractedSeftAmount,
                                false);
                            newHistoricContractContractedAmounts.Add(newHistoricContractContractedAmount);
                        }
                    }

                    foreach (var actuallyPaidAmount in historicContract.ActuallyPaidAmounts)
                    {
                        var newHistoricContractActuallyPaidAmount = new Eumis.Domain.HistoricContracts.HistoricContractActuallyPaidAmount(
                            newHistoricContract.HistoricContractId,
                            actuallyPaidAmount.PaymentDate.Value,
                            actuallyPaidAmount.PaidEuAmount,
                            actuallyPaidAmount.PaidBgAmount);
                        newHistoricContractActuallyPaidAmounts.Add(newHistoricContractActuallyPaidAmount);
                    }

                    foreach (var reimbursedAmount in historicContract.ReimbursedAmounts)
                    {
                        var newHistoricContractReimbursedAmount = new Eumis.Domain.HistoricContracts.HistoricContractReimbursedAmount(
                            newHistoricContract.HistoricContractId,
                            reimbursedAmount.ReimbursementDate.Value,
                            reimbursedAmount.ReimbursedPrincipalEuAmount,
                            reimbursedAmount.ReimbursedPrincipalBgAmount);
                        newHistoricContractReimbursedAmounts.Add(newHistoricContractReimbursedAmount);
                    }
                }

                this.unitOfWork.BulkInsert<Eumis.Domain.HistoricContracts.HistoricContract>(newHistoricContracts);
                this.unitOfWork.BulkInsert<Eumis.Domain.HistoricContracts.HistoricContractActivity>(newHistoricContractActivities);
                this.unitOfWork.BulkInsert<Eumis.Domain.HistoricContracts.HistoricContractLocation>(newHistoricContractLocations);
                this.unitOfWork.BulkInsert<Eumis.Domain.HistoricContracts.HistoricContractPartner>(newHistoricContractPartners);
                this.unitOfWork.BulkInsert<Eumis.Domain.HistoricContracts.HistoricContractProcurementPlan>(newHistoricContractProcurementPlans);
                this.unitOfWork.BulkInsert<Eumis.Domain.HistoricContracts.HistoricContractProcurementPlanPosition>(newHistoricContractProcurementPlanPositions);
                this.unitOfWork.BulkInsert<Eumis.Domain.HistoricContracts.HistoricContractContractedAmount>(newHistoricContractContractedAmounts);
                this.unitOfWork.BulkInsert<Eumis.Domain.HistoricContracts.HistoricContractActuallyPaidAmount>(newHistoricContractActuallyPaidAmounts);
                this.unitOfWork.BulkInsert<Eumis.Domain.HistoricContracts.HistoricContractReimbursedAmount>(newHistoricContractReimbursedAmounts);

                transaction.Commit();
            }
        }
    }
}
