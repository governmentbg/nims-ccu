using Eumis.Domain.NonAggregates;
using System.Collections.Generic;

namespace Eumis.Domain.Core
{
    public class DispatchResolver
    {
        private static readonly IDictionary<NotificationEventType, string> EventToPath = new Dictionary<NotificationEventType, string>()
        {
            { NotificationEventType.IndicatorChanged, "root.map.indicators.edit" },
            { NotificationEventType.ProgrammeDataChanged, "root.map.programmes.view" },
            { NotificationEventType.ProcedureStatusChangedToDraft, "root.procedures.view" },
            { NotificationEventType.ProjectSubmitted, "root.projects.edit" },
            { NotificationEventType.ContractProcurementActivated, "root.contracts.view.amendments.procurements.edit" },
            { NotificationEventType.ContractSpendingPlanActivated, "root.contracts.view.amendments.spendingPlans.edit" },
            { NotificationEventType.ContractCommunicationReceived, "root.contracts.view.communications.edit" },
            { NotificationEventType.ContractReportSentUnchecked, "root.contractReportChecks.view" },
            { NotificationEventType.ContractReportPaymentToResent, "root.contractReportChecks.view.documents.editPayment" },
            { NotificationEventType.ContractReportPaymentToReturned, "root.contractReportChecks.view.documents.editPayment" },
            { NotificationEventType.ContractReportFinancialToReturned, "root.contractReportChecks.view.documents.editFinancial" },
            { NotificationEventType.ContractReportFinancialToResent, "root.contractReportChecks.view.documents.editFinancial" },
            { NotificationEventType.ContractReportTechnicalToReturned, "root.contractReportChecks.view.documents.editTechnical" },
            { NotificationEventType.ContractReportTechnicalToResent, "root.contractReportChecks.view.documents.editTechnical" },
            { NotificationEventType.ContractReportMicroDataToReturned, "root.contractReportChecks.view.documents" },
            { NotificationEventType.ContractReportMicroDataToResent, "root.contractReportChecks.view.documents" },
            { NotificationEventType.CertReportStatusToEnded, "root.certReportChecks.view.edit" },
            { NotificationEventType.CertReportStatusToReturned, "root.certReports.view.edit" },
            { NotificationEventType.CertAuthorityCommunicationReceived, "root.certAuthorityCommunications.view.communication.edit" },
            { NotificationEventType.RequestPackageStatusToEntered, "root.requestPackages.view.edit" },
            { NotificationEventType.RequestPackageStatusToChecked, "root.requestPackages.view.edit" },
            { NotificationEventType.RequestPackageStatusToDraft, "root.requestPackages.view.edit" },
            { NotificationEventType.ProjectCandidateAnswerRegistered, "root.evalSessions.view.projects.view" },
            { NotificationEventType.EvalSheetDistributionTypeToContinued, "root.evalSessions.view.sheets.edit" },
            { NotificationEventType.AuditAuthorityCommunicationReceived, "root.auditAuthorityCommunications.view.communication.edit" },
            { NotificationEventType.CheckSheetCompletion, "root.{0}.edit" },
            { NotificationEventType.ProcedureStatusChangedToActivated, "root.procedures.view" },
            { NotificationEventType.CheckSheetReturn, "root.{0}.edit" },
        };

        private DispatchParameterIdentifier firstParameterName;
        private DispatchParameterIdentifier secondParameterName;

        public DispatchResolver(NotificationEventType eventType, int firstParameterValue)
        {
            this.EventType = eventType;
            this.FirstParameterValue = firstParameterValue;
            this.firstParameterName = DispatchParameterIdentifier.Id;
        }

        public DispatchResolver(NotificationEventType eventType, int firstParameterValue, int secondParameterValue)
        {
            this.EventType = eventType;
            this.FirstParameterValue = firstParameterValue;
            this.firstParameterName = DispatchParameterIdentifier.Id;
            this.SecondParameterValue = secondParameterValue;
            this.secondParameterName = DispatchParameterIdentifier.Ind;
        }

        public DispatchResolver(NotificationEventType eventType, int firstParameterValue, int secondParameterValue, DispatchParameterIdentifier secondParameterName)
        {
            this.EventType = eventType;
            this.FirstParameterValue = firstParameterValue;
            this.SecondParameterValue = secondParameterValue;
            this.firstParameterName = DispatchParameterIdentifier.Id;
            this.secondParameterName = secondParameterName;
        }

        public int FirstParameterValue { get; set; }

        public int? SecondParameterValue { get; set; }

        public NotificationEventType EventType { get; set; }

        public string GetDispatcherPath()
        {
            string path = EventToPath[this.EventType];

            if (string.IsNullOrEmpty(path) || this.FirstParameterValue == 0)
            {
                return string.Empty;
            }

            if (this.SecondParameterValue == null || this.SecondParameterValue == 0)
            {
                return $"{path}({{{this.firstParameterName.Value}:{this.FirstParameterValue}}})";
            }
            else
            {
                return $"{path}({{{this.firstParameterName.Value}:{this.FirstParameterValue}, {this.secondParameterName.Value}:{this.SecondParameterValue}}})";
            }
        }

        public int GetDispatcherId()
        {
            if (this.SecondParameterValue != null && this.SecondParameterValue != 0)
            {
                return (int)this.SecondParameterValue;
            }

            return this.FirstParameterValue;
        }

        public string GetCheckSheetDispatcherPath(string specificPath)
        {
            string path = EventToPath[this.EventType];
            var fullPath = string.Format(path, specificPath);

            return $"{fullPath}({{{this.firstParameterName.Value}:{this.FirstParameterValue}, {this.secondParameterName.Value}:{this.SecondParameterValue}}})";
        }
    }
}
