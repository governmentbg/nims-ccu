using Eumis.Common;
using Eumis.Common.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Documents.Enums
{
    [Serializable]
    public class ProjectCommunicationSubjectNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.ProjectCommunicationSubjectNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.Bulgarian));
            }
        }

        public string NameEN
        {
            get
            {
                return App_LocalResources.ProjectCommunicationSubjectNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.English));
            }
        }

        public string DisplayName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.Name, this.NameEN);
            }
        }

        public string Id { get; set; }

        public string ResourceKey { get; set; }

        public static readonly ProjectCommunicationSubjectNomenclature ProjectProposalWithdrawal = new ProjectCommunicationSubjectNomenclature { ResourceKey = "ProjectProposalWithdrawal", Id = "projectProposalWithdrawal" };
        public static readonly ProjectCommunicationSubjectNomenclature Complaint = new ProjectCommunicationSubjectNomenclature { ResourceKey = "Complaint", Id = "complaint" };
        public static readonly ProjectCommunicationSubjectNomenclature ContractConclusionDocuments = new ProjectCommunicationSubjectNomenclature { ResourceKey = "ContractConclusionDocuments", Id = "contractConclusionDocuments" };
        public static readonly ProjectCommunicationSubjectNomenclature ChangesAndCircumstances = new ProjectCommunicationSubjectNomenclature { ResourceKey = "ChangesAndCircumstances", Id = "changesAndCircumstances" };
        public static readonly ProjectCommunicationSubjectNomenclature Message = new ProjectCommunicationSubjectNomenclature { ResourceKey = "Message", Id = "message" };
        public static readonly ProjectCommunicationSubjectNomenclature TourismMinistryReport = new ProjectCommunicationSubjectNomenclature { ResourceKey = "TourismMinistryReport", Id = "tourismMinistryReport" };
        public static readonly ProjectCommunicationSubjectNomenclature ManagingAuthorityReport = new ProjectCommunicationSubjectNomenclature { ResourceKey = "ManagingAuthorityReport", Id = "managingAuthorityReport" };

        public IEnumerable<LocalizedSelectListItem> GetBeneficaryItems()
        {
            return new List<ProjectCommunicationSubjectNomenclature>() {
                ProjectProposalWithdrawal,
                Complaint,
                ChangesAndCircumstances,
                TourismMinistryReport,
                ManagingAuthorityReport
            }.Select(e => new LocalizedSelectListItem() { Value = e.Id, Name = e.Name, NameEN = e.NameEN });
        }

        public IEnumerable<LocalizedSelectListItem> GetManagingAuthorityItems()
        {
            return new List<ProjectCommunicationSubjectNomenclature>() {
                ContractConclusionDocuments,
                ChangesAndCircumstances,
                Message,
                TourismMinistryReport
            }
            .Select(e => new LocalizedSelectListItem()
            {
                Value = e.Id,
                Name = e.Name,
                NameEN = e.NameEN
            });
        }
    }
}
