namespace Eumis.PortalIntegration.Api.Portal.ContractRegistrations.DataObjects
{
    public class AccessCodePermissionDO
    {
        public bool CanReadContracts { get; set; }

        public bool CanReadProcurements { get; set; }

        public bool CanWriteProcurements { get; set; }

        public bool CanReadTechnicalPlan { get; set; }

        public bool CanWriteTechnicalPlan { get; set; }

        public bool CanReadFinancialPlan { get; set; }

        public bool CanWriteFinancialPlan { get; set; }

        public bool CanReadMicrodata { get; set; }

        public bool CanWriteMicrodata { get; set; }

        public bool CanReadPaymentRequest { get; set; }

        public bool CanWritePaymentRequest { get; set; }

        public bool CanReadCommunication { get; set; }

        public bool CanReadSpendingPlan { get; set; }

        public bool CanWriteSpendingPlan { get; set; }
    }
}
