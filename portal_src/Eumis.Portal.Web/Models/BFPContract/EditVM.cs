using Eumis.Common.Validation;
using Eumis.Documents.Enums;
using Eumis.Documents.Mappers;
using Eumis.Documents.Validation;
using Eumis.Portal.Web.Helpers;
using R_09989;
using R_09999;
using R_10036;
using R_10040;
using System;
using System.Collections.Generic;
using System.Linq;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Models.BFPContract
{
    public class EditVM : BaseVM, IEditVM<R_10040.BFPContract>, IEngineValidatable, IRemoteValidatable
    {
        public R_09988.BFPContractTypeNomenclature type { get; set; }

        public R_10031.BFPContractBasicData BFPContractBasicData { get; set; }

        public R_10004.Company Beneficiary { get; set; }

        public R_10040.BFPContractPartners Partners { get; set; }

        public R_10040.BFPContractDirectionsBudgetContract BFPContractDirectionsBudgetContract { get; set; }

        public R_10040.BFPContractContractActivities BFPContractContractActivities { get; set; }

        public R_10040.BFPContractIndicators BFPContractIndicators { get; set; }

        public R_10040.BFPContractContractTeams BFPContractContractTeams { get; set; }

        public R_10040.BFPContractPlans BFPContractPlans { get; set; }

        public R_10040.BFPContractProjectSpecFields ProjectSpecFields { get; set; }

        public bool HasProjectSpecFields { get; set; }

        public R_10040.BFPContractAttachedDocuments AttachedDocuments { get; set; }

        public R_10093.DirectionSection Directions { get; set; }

        public R_10040.BFPContractElectronicDeclarations ElectronicDeclarations { get; set; }

        public List<string> RemoteValidationErrors { get; set; }

        public List<string> RemoteValidationWarnings { get; set; }

        public List<ApplicationSection> ApplicationSections { get; set; }

        public bool IsPartialReadOnly { get; set; }

        public ProcedureKind ProcedureKind { get; set; }

        #region Get Set

        public EditVM() { }

        public EditVM(R_10040.BFPContract contract)
        {
            this.type = contract.type;

            this.BFPContractBasicData = contract.BFPContractBasicData;

            this.Beneficiary = contract.Beneficiary;

            this.Partners = contract.Partners;

            this.BFPContractDirectionsBudgetContract = contract.BFPContractDirectionsBudgetContract;

            this.BFPContractContractActivities = contract.BFPContractContractActivities;

            this.BFPContractIndicators = contract.BFPContractIndicators;

            this.BFPContractContractTeams = contract.BFPContractContractTeams;

            this.BFPContractPlans = contract.BFPContractPlans;

            this.ProjectSpecFields = contract.ProjectSpecFields;

            this.HasProjectSpecFields = contract.HasProjectSpecFields;

            this.AttachedDocuments = contract.AttachedDocuments;

            this.Directions = contract.BFPContractDirectionsBudgetContract.Directions;

            this.ElectronicDeclarations = contract.ElectronicDeclarations;

            this.IsPartialReadOnly = contract.IsPartialReadOnly;

            #region Set Nomenclatures

            this.ApplicationSections = contract.ApplicationSections;

            this.ProcedureKind = contract.ProcedureKind;

            #endregion
        }

        public R_10040.BFPContract Set(R_10040.BFPContract contract)
        {
            return contract;
        }

        public R_10040.BFPContract SetAsync()
        {
            var contract = (R_10040.BFPContract)AppContext.Current.Document;

            if (!(contract.BFPContractBasicData != null && contract.BFPContractBasicData.isLocked))
            {
                if (this.BFPContractBasicData != null && contract.BFPContractBasicData != null)
                {
                    this.BFPContractBasicData.ProcedureIdentifier = contract.BFPContractBasicData.ProcedureIdentifier;

                    this.BFPContractBasicData.ProgrammeBasicData = contract.BFPContractBasicData.ProgrammeBasicData;
                    this.BFPContractBasicData.Procedure = contract.BFPContractBasicData.Procedure;

                    this.BFPContractBasicData.id = contract.BFPContractBasicData.id;
                    this.BFPContractBasicData.isLocked = contract.BFPContractBasicData.isLocked;
                    this.BFPContractBasicData.IsPartialReadOnly = contract.BFPContractBasicData.IsPartialReadOnly;

                    this.BFPContractBasicData.RegistrationNumber = contract.BFPContractBasicData.RegistrationNumber;
                    this.BFPContractBasicData.Version = contract.BFPContractBasicData.Version;
                    this.BFPContractBasicData.SubVersion = contract.BFPContractBasicData.SubVersion;

                    this.BFPContractBasicData.NutsLevel = contract.BFPContractBasicData.NutsLevel;
                    this.BFPContractBasicData.Locations = contract.BFPContractBasicData.Locations;
                    this.BFPContractBasicData.MaxDuration = contract.BFPContractBasicData.MaxDuration;

                    for (int i = 0; i < this.BFPContractBasicData.NutsAddress.NutsAddressContentCollection.Count; i++)
                    {
                        if (!this.BFPContractBasicData.NutsAddress.NutsLevel.Id.Equals("country"))
                            this.BFPContractBasicData.NutsAddress.NutsAddressContentCollection[i].Country = null;

                        if (!this.BFPContractBasicData.NutsAddress.NutsLevel.Id.Equals("protectedZone"))
                            this.BFPContractBasicData.NutsAddress.NutsAddressContentCollection[i].ProtectedZone = null;

                        if (!this.BFPContractBasicData.NutsAddress.NutsLevel.Id.Equals("regionNUTS1"))
                            this.BFPContractBasicData.NutsAddress.NutsAddressContentCollection[i].Nuts1 = null;

                        if (!this.BFPContractBasicData.NutsAddress.NutsLevel.Id.Equals("regionNUTS2"))
                            this.BFPContractBasicData.NutsAddress.NutsAddressContentCollection[i].Nuts2 = null;

                        if (!this.BFPContractBasicData.NutsAddress.NutsLevel.Id.Equals("district"))
                            this.BFPContractBasicData.NutsAddress.NutsAddressContentCollection[i].District = null;

                        if (!this.BFPContractBasicData.NutsAddress.NutsLevel.Id.Equals("municipality"))
                            this.BFPContractBasicData.NutsAddress.NutsAddressContentCollection[i].Municipality = null;

                        if (!this.BFPContractBasicData.NutsAddress.NutsLevel.Id.Equals("settlement"))
                            this.BFPContractBasicData.NutsAddress.NutsAddressContentCollection[i].Settlement = null;

                        if (contract.IsPartialReadOnly)
                        {
                            var oldLocation = GetLocationFromNutsAddressContent(contract.BFPContractBasicData.NutsAddress.NutsAddressContentCollection[i], contract.BFPContractBasicData.NutsAddress.NutsLevel.Id);
                            var newLocation = GetLocationFromNutsAddressContent(this.BFPContractBasicData.NutsAddress.NutsAddressContentCollection[i], this.BFPContractBasicData.NutsAddress.NutsLevel.Id);

                            if (newLocation.Code != oldLocation.Code)
                            {
                                ChangeBudgetLocation(contract.BFPContractDirectionsBudgetContract.BFPContractBudget, oldLocation, newLocation);
                            }
                        }
                    }
                }

                contract.BFPContractBasicData = this.BFPContractBasicData;
            }

            if (!(contract.Beneficiary != null && contract.Beneficiary.isLocked))
            {
                if (this.Beneficiary != null && contract.Beneficiary != null)
                {
                    this.Beneficiary.id = contract.Beneficiary.id;
                    this.Beneficiary.isLocked = contract.Beneficiary.isLocked;
                    this.Beneficiary.IsPartialReadOnly = contract.Beneficiary.IsPartialReadOnly;

                    if (contract.IsPartialReadOnly)
                    {
                        this.Beneficiary.Uin = contract.Beneficiary.Uin;
                        this.Beneficiary.Name = contract.Beneficiary.Name;
                        this.Beneficiary.NameEN = contract.Beneficiary.NameEN;
                    }
                }

                contract.Beneficiary = this.Beneficiary;
            }

            if (!(contract.Partners != null && contract.Partners.isLocked) && !contract.IsPartialReadOnly)
            {
                if (this.Partners == null || this.Partners.PartnerCollection == null)
                    contract.Partners.PartnerCollection = new CompanyCollection();
                else
                {
                    // Check if all activated elements are still existing
                    foreach (var activatedPartner in this.Partners.PartnerCollection.Where(e => e.isActivated))
                    {
                        var foundActivatedPartner = contract.Partners.PartnerCollection.FirstOrDefault(e => e.gid == activatedPartner.gid && e.isActivated);
                        if (foundActivatedPartner == null)
                            throw new Exception("Activated Partner removed");
                    }

                    for (int i = 0; i < this.Partners.PartnerCollection.Count; i++)
                    {
                        if (String.IsNullOrWhiteSpace(this.Partners.PartnerCollection[i].gid))
                        {
                            throw new Exception("Partner does not have gid");
                        }
                    }

                    var activatedGids = this.Partners.PartnerCollection.Where(e => e.isActivated).Select(e => e.gid);
                    if (contract.Partners.PartnerCollection.Any(e => !activatedGids.Contains(e.gid)
                        && (e.isActivated || !e.isActive)))
                        throw new Exception("Partner set to Activated or not active irregularly");

                    contract.Partners.PartnerCollection = this.Partners.PartnerCollection;
                }
            }

            if (!contract.BFPContractDirectionsBudgetContract.isLocked && !contract.IsPartialReadOnly)
            {
                contract.BFPContractDirectionsBudgetContract.Directions =
                    this.BFPContractDirectionsBudgetContract.Directions;

                if (contract.BFPContractDirectionsBudgetContract.BFPContractBudget != null
                    && this.BFPContractDirectionsBudgetContract.BFPContractBudget != null)
                {
                    contract.BFPContractDirectionsBudgetContract.BFPContractBudget.Load(this.BFPContractDirectionsBudgetContract.BFPContractBudget, contract.type == R_09988.BFPContractTypeNomenclature.Initial);
                }

                contract.BFPContractDirectionsBudgetContract.Contract = this.BFPContractDirectionsBudgetContract.Contract;
            }

            if (!(contract.BFPContractContractActivities != null && contract.BFPContractContractActivities.isLocked) && !contract.IsPartialReadOnly)
            {
                if (this.BFPContractContractActivities == null || this.BFPContractContractActivities.BFPContractContractActivityCollection == null)
                    contract.BFPContractContractActivities.BFPContractContractActivityCollection = new R_10040.BFPContractContractActivityCollection();
                else
                {
                    // Check if all activated elements are still existing
                    foreach (var activatedContractActivity in this.BFPContractContractActivities.BFPContractContractActivityCollection.Where(e => e.isActivated))
                    {
                        var foundActivatedContractActivity = contract.BFPContractContractActivities.BFPContractContractActivityCollection.FirstOrDefault(e => e.gid == activatedContractActivity.gid && e.isActivated);
                        if (foundActivatedContractActivity == null)
                            throw new Exception("Activated ContractActivity removed");
                    }

                    for (int i = 0; i < this.BFPContractContractActivities.BFPContractContractActivityCollection.Count; i++)
                    {
                        if (String.IsNullOrWhiteSpace(this.BFPContractContractActivities.BFPContractContractActivityCollection[i].gid))
                        {
                            throw new Exception("ContractActivity does not have gid");
                        }
                    }

                    var activatedGids = this.BFPContractContractActivities.BFPContractContractActivityCollection.Where(e => e.isActivated).Select(e => e.gid);
                    if (contract.BFPContractContractActivities.BFPContractContractActivityCollection.Any(e => !activatedGids.Contains(e.gid)
                        && (e.isActivated || !e.isActive)))
                        throw new Exception("ContractActivity set to Activated or not active irregularly");

                    contract.BFPContractContractActivities.BFPContractContractActivityCollection = this.BFPContractContractActivities.BFPContractContractActivityCollection;
                }
            }

            if (!(contract.BFPContractIndicators != null && contract.BFPContractIndicators.isLocked) && !contract.IsPartialReadOnly)
            {
                if (this.BFPContractIndicators == null || this.BFPContractIndicators.BFPContractIndicatorCollection == null)
                    contract.BFPContractIndicators.BFPContractIndicatorCollection = new R_10040.BFPContractIndicatorCollection();
                else
                {
                    // Check if all activated elements are still existing
                    foreach (var activatedIndicator in this.BFPContractIndicators.BFPContractIndicatorCollection.Where(e => e.isActivated))
                    {
                        var foundActivatedIndicator = contract.BFPContractIndicators.BFPContractIndicatorCollection.FirstOrDefault(e => e.gid == activatedIndicator.gid && e.isActivated);
                        if (foundActivatedIndicator == null)
                            throw new Exception("Activated indicator removed");
                    }

                    for (int i = 0; i < this.BFPContractIndicators.BFPContractIndicatorCollection.Count; i++)
                    {
                        if (String.IsNullOrWhiteSpace(this.BFPContractIndicators.BFPContractIndicatorCollection[i].gid))
                        {
                            throw new Exception("Indicator does not have gid");
                        }
                    }

                    var activatedGids = this.BFPContractIndicators.BFPContractIndicatorCollection.Where(e => e.isActivated).Select(e => e.gid);
                    if (contract.BFPContractIndicators.BFPContractIndicatorCollection.Any(e => !activatedGids.Contains(e.gid)
                        && (e.isActivated || !e.isActive)))
                        throw new Exception("Indicator set to Activated or not active irregularly");

                    contract.BFPContractIndicators.BFPContractIndicatorCollection = this.BFPContractIndicators.BFPContractIndicatorCollection;
                }
            }

            if (!(contract.BFPContractContractTeams != null && contract.BFPContractContractTeams.isLocked))
            {
                if (this.BFPContractContractTeams == null)
                    contract.BFPContractContractTeams.BFPContractContractTeamCollection = new R_10040.BFPContractContractTeamCollection();
                else
                    contract.BFPContractContractTeams.BFPContractContractTeamCollection = this.BFPContractContractTeams.BFPContractContractTeamCollection;
            }

            if (!(contract.BFPContractPlans != null && contract.BFPContractPlans.isLocked))
            {
                if (this.BFPContractPlans == null || this.BFPContractPlans.BFPContractPlanCollection == null)
                    contract.BFPContractPlans.BFPContractPlanCollection = new R_10040.BFPContractPlanCollection();
                else
                    contract.BFPContractPlans.BFPContractPlanCollection = this.BFPContractPlans.BFPContractPlanCollection;
            }

            if (!(contract.AttachedDocuments != null && contract.AttachedDocuments.isLocked))
            {
                if (this.AttachedDocuments == null)
                    contract.AttachedDocuments.AttachedDocumentCollection = new R_10040.AttachedDocumentCollection();
                else
                    contract.AttachedDocuments.AttachedDocumentCollection = this.AttachedDocuments.AttachedDocumentCollection;
            }

            return contract;
        }

        #endregion

        private static Location GetLocationFromNutsAddressContent(NutsAddressContent nutsAddressContent, string id)
        {
            Location location;

            switch (id)
            {
                case "country":
                    location = nutsAddressContent.Country;
                    break;

                case "protectedZone":
                    location = nutsAddressContent.ProtectedZone;
                    break;

                case "regionNUTS1":
                    location = nutsAddressContent.Nuts1;
                    break;

                case "regionNUTS2":
                    location = nutsAddressContent.Nuts2;
                    break;

                case "district":
                    location = nutsAddressContent.District;
                    break;

                case "municipality":
                    location = nutsAddressContent.Municipality;
                    break;

                case "settlement":
                    location = nutsAddressContent.Settlement;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(id), "Invalid level id");
            }

            return location;
        }

        private static void ChangeBudgetLocation(BFPContractBudget budget, Location oldLocation, Location newLocation)
        {
            foreach (var programme in budget.BFPContractProgrammeBudgetCollection)
            {
                foreach (var expense in programme.BFPContractProgrammeExpenseBudgetCollection)
                {
                    foreach (var detail in expense.BFPContractProgrammeDetailsExpenseBudgetCollection)
                    {
                        if (detail.Nuts.Code == oldLocation.Code)
                        {
                            detail.Nuts = newLocation;
                        }
                    }
                }
            }
        }
    }
}
