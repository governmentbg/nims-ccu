using System.Reflection;
using Eumis.Common.Validation;
using Eumis.Components.Web;
using Eumis.Documents.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eumis.Documents.Contracts;
using Eumis.Documents.Validation;
using Eumis.Portal.Web.Helpers;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Models.Project
{
    public class EditVM : BaseVM, IEditVM<R_10019.Project>, IEngineValidatable, IRemoteValidatable
    {
        public Eumis.Documents.Enums.RegistrationTypeNomenclature RegistrationType { get; set; }

        public Eumis.Documents.Contracts.ContractProjectHeader ProjectHeader { get; set; }

        public R_10002.ProjectBasicData ProjectBasicData { get; set; }
        public R_10004.Company Candidate { get; set; }

        public R_10019.Partners Partners { get; set; }

        public List<R_09998.DirectionsBudgetContract> DirectionsBudgetContractCollection { get; set; }

        public List<R_09995.ProgrammeContractActivities> ProgrammeContractActivitiesCollection { get; set; }
        public List<R_10014.ProgrammeIndicators> ProgrammeIndicatorsCollection { get; set; }
        public R_10019.ContractTeams ContractTeams { get; set; }
        public R_10019.ProjectErrands ProjectErrands { get; set; }

        public R_10019.ProjectSpecFields ProjectSpecFields { get; set; }
        public R_10019.AttachedDocuments AttachedDocuments { get; set; }

        public List<ApplicationSection> ApplicationSections { get; set; }

        public R_10019.ElectronicDeclarations ElectronicDeclarations { get; set; }

        public bool HasProjectSpecFields { get; set; }
        public bool HasPaperAttachedDocuments { get; set; }
        public bool HasAttachedDocuments { get; set; }
        public bool HasElectronicDeclarations { get; set; }

        public List<string> RemoteValidationErrors { get; set; }
        public List<string> RemoteValidationWarnings { get; set; }

        public DateTime? EndingDate { get; set; }

        public bool IsFinalRecipients { get; set; }

        public bool IsFinancialIntermediaries { get; set; }

        public Guid ProjectGid { get; set; }

        public bool IsSubmitted { get; set; }

        #region Get Set

        public EditVM() { }

        public EditVM(R_10019.Project project)
        {
            this.RegistrationType = project.RegistrationType;

            this.ProjectHeader = project.ProjectHeader;

            this.ProjectBasicData = project.ProjectBasicData;

            this.ProjectGid = project.ProjectGid;

            this.IsSubmitted = project.IsSubmitted;

            this.Candidate = project.Candidate;
            if (HttpContext.Current.User != null)
            {
                var ci = HttpContext.Current.User.Identity as System.Security.Claims.ClaimsIdentity;
                var user = EumisUserManager.LoadUser(ci);
                if (!user.IsPrivate)
                {
                    this.Candidate.Email = user.Email;
                }
            }

            this.Partners = project.Partners;

            this.DirectionsBudgetContractCollection = project.DirectionsBudgetContractCollection;
            if (project.DirectionsBudgetContractCollection != null && project.DirectionsBudgetContractCollection.Count > 0)
            {
                for (int i = 0; i < project.DirectionsBudgetContractCollection.Count; i++)
                {
                    if (project.DirectionsBudgetContractCollection[i].Directions == null)
                        project.DirectionsBudgetContractCollection[i].Directions = new R_10093.DirectionSection();
                }
            }
            this.ProgrammeContractActivitiesCollection = project.ProgrammeContractActivitiesCollection;
            this.ProgrammeIndicatorsCollection = project.ProgrammeIndicatorsCollection;
            this.ContractTeams = project.ContractTeams;
            this.ProjectErrands = project.ProjectErrands;

            this.ProjectSpecFields = project.ProjectSpecFields;
            this.AttachedDocuments = project.AttachedDocuments;

            // Set attached documents IsSignatureRequired
            if (this.AttachedDocuments != null && this.AttachedDocuments.AttachedDocumentCollection != null)
            {
                for (int i = 0; i < this.AttachedDocuments.AttachedDocumentCollection.Count; i++)
                {
                    if (this.AttachedDocuments.AttachedDocumentCollection[i].Type != null && !String.IsNullOrWhiteSpace(this.AttachedDocuments.AttachedDocumentCollection[i].Type.Id))
                    {
                        this.AttachedDocuments.AttachedDocumentCollection[i].Type.isSignatureRequired
                            = AppContext.Current.Nomenclature(NomenclatureType.AttachedDocumentType)
                            .Any(e => e.Value == this.AttachedDocuments.AttachedDocumentCollection[i].Type.Id && e.IsSignatureRequired.HasValue && e.IsSignatureRequired.Value);
                    }
                }
            }

            // lock documents added in previous project version
            if (project.VersionCreateDate.HasValue &&
                this.AttachedDocuments != null &&
                this.AttachedDocuments.AttachedDocumentCollection != null &&
                this.AttachedDocuments.AttachedDocumentCollection.Count > 0)
            {
                foreach (var document in this.AttachedDocuments.AttachedDocumentCollection)
                {
                    if ((!document.ActivationDate.HasValue || (document.ActivationDate.HasValue && document.ActivationDate <= project.VersionCreateDate.Value))
                        && !string.IsNullOrWhiteSpace(document.AttachedDocumentContent?.BlobContentId))
                    {
                        document.IsLocked = true;
                    }
                }
            }

            if (project.ElectronicDeclarations != null &&
                project.ElectronicDeclarations.ElectronicDeclarationCollection != null &&
                project.ElectronicDeclarations.ElectronicDeclarationCollection.Count > 0)
            {
                this.ElectronicDeclarations = project.ElectronicDeclarations;

                foreach (var vmDeclaration in this.ElectronicDeclarations.ElectronicDeclarationCollection)
                {
                    var projectDeclaration = project.ElectronicDeclarations
                        .ElectronicDeclarationCollection
                        .Where(d => d.Gid == vmDeclaration.Gid)
                        .SingleOrDefault();

                    vmDeclaration.SetFieldValuesForEdit(projectDeclaration);
                }
            }

            // Calculate values
            // this.Contract.TotalEligibleCostsPublicFunding = 0;
            // this.Contract.TotalEligibleCosts = 0;
            // this.Contract.RatioRequestedFundingTotalEligibleCosts = 0;
            // this.Contract.TotalProjectCost = 0;

            this.ApplicationSections = project.ApplicationSections;

            this.HasProjectSpecFields = project.HasProjectSpecFields;
            this.HasPaperAttachedDocuments = project.HasPaperAttachedDocuments;
            this.HasAttachedDocuments = project.HasAttachedDocuments;
            this.HasElectronicDeclarations = project.HasElectronicDeclarations;

            this.EndingDate = project.EndingDate;

            project.PassFormTypeInfo();
            this.IsFinalRecipients = project.IsFinalRecipients;
            this.IsFinancialIntermediaries = project.IsFinancialIntermediaries;
        }

        public R_10019.Project Set(R_10019.Project project)
        {
            project.modificationDate = DateTime.Now;

            if (!(project.ProjectBasicData != null && project.ProjectBasicData.isLocked))
            {
                if (this.ProjectBasicData != null && project.ProjectBasicData != null)
                {
                    this.ProjectBasicData.ProgrammeBasicDataCollection = project.ProjectBasicData.ProgrammeBasicDataCollection;
                    this.ProjectBasicData.Procedure = project.ProjectBasicData.Procedure;
                    this.ProjectBasicData.id = project.ProjectBasicData.id;
                    this.ProjectBasicData.isLocked = project.ProjectBasicData.isLocked;
                    this.ProjectBasicData.ApplicationFormType = project.ProjectBasicData.ApplicationFormType;
                    this.ProjectBasicData.AllowedRegistrationType = project.ProjectBasicData.AllowedRegistrationType;

                    this.ProjectBasicData.MaxDuration = project.ProjectBasicData.MaxDuration;
                    this.ProjectBasicData.Locations = project.ProjectBasicData.Locations;
                }

                project.ProjectBasicData = this.ProjectBasicData;
            }

            var uinType = project.Candidate.UinType;
            var uin = project.Candidate.Uin;
            var email = project.Candidate.Email;

            if (!(project.Candidate != null && project.Candidate.isLocked))
            {
                if (this.Candidate != null && project.Candidate != null)
                {
                    this.Candidate.id = project.Candidate.id;
                    this.Candidate.isLocked = project.Candidate.isLocked;
                }

                project.Candidate = this.Candidate;
            }

            if (HttpContext.Current.User != null)
            {
                var ci = HttpContext.Current.User.Identity as System.Security.Claims.ClaimsIdentity;
                var user = EumisUserManager.LoadUser(ci);
                if (!user.IsPrivate && project.Candidate != null)
                {
                    project.Candidate.Email = user.Email;
                }

                if (user.IsPrivate || AppContext.Current.Document is R_10020.Message)
                {
                    project.Candidate.Email = email;
                    project.Candidate.UinType = uinType;
                    project.Candidate.Uin = uin;
                }
            }


            #region Standard

            //if (!(project.Partners != null && project.Partners.isLocked))
            //{
            //    if (this.Partners == null)
            //        project.Partners.PartnerCollection = new R_10019.CompanyCollection();
            //    else
            //        project.Partners.PartnerCollection = this.Partners.PartnerCollection;
            //}

            if (project.DirectionsBudgetContractCollection != null
                && this.DirectionsBudgetContractCollection != null
                && this.DirectionsBudgetContractCollection.Count == project.DirectionsBudgetContractCollection.Count)
            {
                for (int i = 0; i < project.DirectionsBudgetContractCollection.Count; i++)
                {
                    if (!project.DirectionsBudgetContractCollection[i].isLocked)
                    {
                        if (this.DirectionsBudgetContractCollection[i].Directions?.DirectionCollection != null)
                        {
                            project.DirectionsBudgetContractCollection[i].Directions.DirectionCollection =
                            this.DirectionsBudgetContractCollection[i].Directions.DirectionCollection;
                        }
                        else 
                        {
                            project.DirectionsBudgetContractCollection[i].Directions.DirectionCollection = new R_10093.DirectionCollection();
                        }

                        //if (project.DimensionsBudgetContractCollection[i].Budget.IsActive)
                        //{
                        //    if (project.DimensionsBudgetContractCollection[i].Budget != null
                        //        && this.DimensionsBudgetContractCollection[i].Budget != null)
                        //    {
                        //        project.DimensionsBudgetContractCollection[i].Budget.Load(this.DimensionsBudgetContractCollection[i].Budget);
                        //    }
                        //}

                        project.DirectionsBudgetContractCollection[i].Contract = this.DirectionsBudgetContractCollection[i].Contract;
                    }
                }
            }

            //if (project.ProgrammeContractActivitiesCollection != null
            //    && this.ProgrammeContractActivitiesCollection != null
            //    && this.ProgrammeContractActivitiesCollection.Count == project.ProgrammeContractActivitiesCollection.Count)
            //{
            //    for (int i = 0; i < project.ProgrammeContractActivitiesCollection.Count; i++)
            //    {
            //        if (!project.ProgrammeContractActivitiesCollection[i].isLocked)
            //        {
            //            project.ProgrammeContractActivitiesCollection[i].ContractActivityCollection = this.ProgrammeContractActivitiesCollection[i].ContractActivityCollection;
            //        }
            //    }
            //}

            if (project.ProgrammeIndicatorsCollection != null && this.ProgrammeIndicatorsCollection != null
                && this.ProgrammeIndicatorsCollection.Count == project.ProgrammeIndicatorsCollection.Count)
            {
                for (int i = 0; i < project.ProgrammeIndicatorsCollection.Count; i++)
                {
                    if (!project.ProgrammeIndicatorsCollection[i].isLocked)
                    {
                        project.ProgrammeIndicatorsCollection[i].IndicatorCollection = this.ProgrammeIndicatorsCollection[i].IndicatorCollection;
                    }
                }
            }

            //if (!(project.ContractTeams != null && project.ContractTeams.isLocked))
            //{
            //    project.ContractTeams.ContractTeamCollection = this.ContractTeams.ContractTeamCollection;
            //}

            //if (!(project.ProjectErrands != null && project.ProjectErrands.isLocked))
            //{
            //    project.ProjectErrands.ProjectErrandCollection = this.ProjectErrands.ProjectErrandCollection;
            //}

            #endregion


            if (!(project.ProjectSpecFields != null && project.ProjectSpecFields.isLocked))
            {
                for (int i = 0; i < project.ProjectSpecFields.ProjectSpecFieldCollection.Count; i++)
                {
                    var current = project.ProjectSpecFields.ProjectSpecFieldCollection[i];

                    var foundSpecField = this.ProjectSpecFields.ProjectSpecFieldCollection.FirstOrDefault(e => e.Id == current.Id);

                    if (foundSpecField == null)
                    {
                        if (!current.IsDeactivated)
                            throw new Exception("Collection modified manually.");
                    }
                    else
                    {
                        foundSpecField.Description = current.Description;
                        foundSpecField.IsDeactivated = current.IsDeactivated;
                        foundSpecField.IsRequired = current.IsRequired;
                        foundSpecField.Title = current.Title;

                        if (current.IsDeactivated)
                            foundSpecField.Value = current.Value;
                    }
                }

                if (this.ProjectSpecFields != null)
                {
                    project.ProjectSpecFields.ProjectSpecFieldCollection = this.ProjectSpecFields.ProjectSpecFieldCollection;
                }
            }

            if (!(project.AttachedDocuments != null && project.AttachedDocuments.isLocked))
            {
                if (project.AttachedDocuments != null && this.AttachedDocuments != null)
                {
                    project.AttachedDocuments.AttachedDocumentCollection = this.AttachedDocuments.AttachedDocumentCollection;
                }
                else
                {
                    if (project.AttachedDocuments != null)
                        project.AttachedDocuments.AttachedDocumentCollection = new R_10019.AttachedDocumentCollection();
                }
            }

            if (!(project.ElectronicDeclarations != null && project.ElectronicDeclarations.isLocked))
            {
                if (project.ElectronicDeclarations != null && project.ElectronicDeclarations.ElectronicDeclarationCollection != null &&
                    this.ElectronicDeclarations != null && this.ElectronicDeclarations.ElectronicDeclarationCollection != null)
                {
                    foreach (var projectDeclaration in project.ElectronicDeclarations.ElectronicDeclarationCollection)
                    {
                        var vmDeclaration = this.ElectronicDeclarations.ElectronicDeclarationCollection
                            .Where(d => d.Gid == projectDeclaration.Gid)
                            .SingleOrDefault();

                        projectDeclaration.SetFieldValues(vmDeclaration);
                    }
                }
            }

            /*if (!(project.Directions != null && project.Directions.isLocked))
            {
                if (this.Directions == null)
                    project.Directions.DirectionCollection = new R_10093.DirectionCollection();
                else
                    project.Directions.DirectionCollection = this.Directions.DirectionCollection;
            }*/

            return project;
        }

        #endregion
    }
}