using System.Diagnostics.CodeAnalysis;

namespace Eumis.Web.Host.Nancy.Models
{
    [SuppressMessage("", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Urls are used by the js app")]
    public class HomeModel : LayoutModel
    {
        public string BlobServerLocation { get; set; }

        public string MaxBlobSize { get; set; }

        public string PortalProcedureEvalTableViewUrl { get; set; }

        public string PortalProcedureEvalTableEditUrl { get; set; }

        public string PortalApplicationViewUrl { get; set; }

        public string PortalEvalSessionSheetViewUrl { get; set; }

        public string PortalEvalSessionSheetEditUrl { get; set; }

        public string PortalEvalSessionStandpointViewUrl { get; set; }

        public string PortalEvalSessionStandpointEditUrl { get; set; }

        public string PortalProjectViewUrl { get; set; }

        public string PortalProjectEditUrl { get; set; }

        public string PortalProjectCommunicationViewUrl { get; set; }

        public string PortalProjectCommunicationAnswerViewUrl { get; set; }

        public string PortalProjectCommunicationEditUrl { get; set; }

        public string PortalProjectManagingAuthorityCommunicationViewUrl { get; set; }

        public string PortalProjectManagingAuthorityCommunicationEditUrl { get; set; }

        public string PortalProjectManagingAuthorityCommunicationAnswerViewUrl { get; set; }

        public string PortalProjectManagingAuthorityCommunicationAnswerEditUrl { get; set; }

        public string PortalContractViewUrl { get; set; }

        public string PortalContractEditUrl { get; set; }

        public string PortalContractEditPartialUrl { get; set; }

        public string PortalContractOfferViewUrl { get; set; }

        public string PortalContractProcurementViewUrl { get; set; }

        public string PortalContractProcurementEditUrl { get; set; }

        public string PortalContractCommunicationViewUrl { get; set; }

        public string PortalContractCommunicationEditUrl { get; set; }

        public string PortalContractSpendingPlanViewUrl { get; set; }

        public string PortalContractSpendingPlanEditUrl { get; set; }

        public string PortalContractReportTechnicalViewUrl { get; set; }

        public string PortalContractReportTechnicalEditUrl { get; set; }

        public string PortalContractReportFinancialViewUrl { get; set; }

        public string PortalContractReportFinancialEditUrl { get; set; }

        public string PortalContractReportPaymentViewUrl { get; set; }

        public string PortalContractReportPaymentEditUrl { get; set; }

        public string PortalContractReportMicroType1ViewUrl { get; set; }

        public string PortalContractReportMicroType2ViewUrl { get; set; }

        public string PortalContractReportMicroType3ViewUrl { get; set; }

        public string PortalContractReportMicroType4ViewUrl { get; set; }

        public string PortalProgrammeCheckListViewUrl { get; set; }

        public string PortalProgrammeCheckListEditUrl { get; set; }

        public string PortalCheckSheetViewUrl { get; set; }

        public string PortalCheckSheetEditUrl { get; set; }

        public string PasswordRegex { get; set; }

        public string PasswordInvalidFormatMessage { get; set; }
    }
}