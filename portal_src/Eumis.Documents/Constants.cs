using Eumis.Common.Config;
using Newtonsoft.Json;
using System;
using System.Configuration;

namespace Eumis.Documents
{
    public class Constants
    {
        [JsonProperty]
        public const string BulgariaId = "BG";
        [JsonProperty]
        public const string BulgariaName = "България";
        [JsonProperty]
        public const string BulgariaNameEN = "Bulgaria";
        [JsonProperty]
        public const string CultureBG = "bg";
        [JsonProperty]
        public const string CultureEN = "en";
        [JsonProperty]
        public const string EUCode = "EU";

        [JsonProperty]
        public const int ProjectBasicDataNameLength = 400;
        [JsonProperty]
        public const int ProjectBasicDataNameEnLength = 400;
        [JsonProperty]
        public const int ProjectBasicDataDescriptionLength = 2000;
        [JsonProperty]
        public const int ProjectBasicDataDescriptionEnLength = 2000;
        [JsonProperty]
        public const int ProjectBasicDataPurposeLength = 3000;
        [JsonProperty]
        public const int ProjectBasicDataAdditionalDescriptionLength = 2000;

        [JsonProperty]
        public const int BulstatFieldMaxLength = 50;
        [JsonProperty]
        public const int CandidateNameLength = 200;
        [JsonProperty]
        public const int CandidateNameEnLength = 200;
        [JsonProperty]
        public const int CandidateStreetLength = 300;
        [JsonProperty]
        public const int CandidatePhoneLength = 50;
        [JsonProperty]
        public const int CandidateEmailLength = 100;
        [JsonProperty]
        public const int ContractorNameLength = 200;
        [JsonProperty]
        public const int ContractorNameEnLength = 200;
        [JsonProperty]
        public const int ContactPersonLength = 100;
        [JsonProperty]
        public const int CompanyRepresentativePersonLength = 100;

        [JsonProperty]
        public const int BudgetExpenseLength = 600;

        [JsonProperty]
        public const int ContractActivityCodeLength = 400;
        [JsonProperty]
        public const int ContractActivityNameLength = 4000;
        [JsonProperty]
        public const int ContractActivityExecutionMethodLength = 3000;
        [JsonProperty]
        public const int ContractActivityResultLength = 3000;

        [JsonProperty]
        public const int IndicatorDescriptionLength = 1000;

        [JsonProperty]
        public const int ContractTeamNameLength = 100;
        [JsonProperty]
        public const int ContractTeamPositionLength = 200;
        [JsonProperty]
        public const int ContractTeamResponsibilitiesLength = 3000;
        [JsonProperty]
        public const int ContractTeamPhoneLength = 50;
        [JsonProperty]
        public const int ContractTeamEmailLength = 100;

        [JsonProperty]
        public const int ProjectErrandNameLength = 1000;
        [JsonProperty]
        public const int ProjectErrandDescriptionLength = 4000;

        [JsonProperty]
        public const int PaperDocumentsDescriptionLength = 200;

        [JsonProperty]
        public const int AttachedDocumentsDescriptionLength = 200;

        [JsonProperty]
        public const int MessageContentLength = 35000;
        [JsonProperty]
        public const int MessageReplyLength = 35000;
        [JsonProperty]
        public const int ProjectDeclarationApprovalLength = 500;

        [JsonProperty]
        public const int PartnersMaxCount = 55;
        [JsonProperty]
        public const int BudgetMaxLevel3Items = 50;
        [JsonProperty]
        public const int BudgetMaxLevel3ItemsTotal = 200;
        [JsonProperty]
        public const int ContractActivitiesMaxCount = 50;
        [JsonProperty]
        public const int ContractTeamsMaxCount = 60;
        [JsonProperty]
        public const int ContractDirectionsMaxCount = 45;
        [JsonProperty]
        public const int ProjectErrandsMaxCount = 50;
        [JsonProperty]
        public const int ProcurementPlansMaxCount = 100;
        [JsonProperty]
        public const string ProcurementPlansErrandLegalActPmsGid = "7e9b44e8-742b-45e5-b967-7b7feec6e18a";
        [JsonProperty]
        public const int ContractorsMaxCount = 300;
        [JsonProperty]
        public const int ContractContractorsMaxCount = 150;
        [JsonProperty]
        public const int PaperDocumentsMaxCount = 100;
        [JsonProperty]
        public const int AttachedDocumentsMaxCount = 1000;

        [JsonProperty]
        public const int PreparerPhoneLength = 50;
        [JsonProperty]
        public const int PreparerEmailLength = 100;

        [JsonProperty]
        public static readonly int AttachedDocumentMaxSize = Int32.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Documents:AttachedDocumentMaxSizeInBytes"));

        // Gid на Физическо лице
        [JsonProperty]
        public const string CompanyLegalTypePhysicalGid = "3e8a3a2a-b211-4d82-89de-5c074e5deb94";

        [JsonProperty]
        public const string ProcedureAliasFof = "fof";
        [JsonProperty]
        public const string ProcedureAliasAgents = "agents";
        [JsonProperty]
        public const string ProcedureAliasEndusers = "endusers";

        public const string CommunicationTemplateXmlKey = "ab2fa5d7-d570-4ab8-a369-4d0d57c9637e";

        public const string ProjectMassCommunicationTemplateXmlKey = "ed80cbb8-d582-492f-93f6-fb1c322cd3a4";

        [JsonProperty]
        public const string ReCaptchaPublicKey = "6LfASMUUAAAAAAPe5OsNlS_bE2CrWsFh_AhQbN9k";
    }
}