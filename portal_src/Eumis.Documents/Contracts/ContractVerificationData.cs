namespace Eumis.Documents.Contracts
{
    public class ContractVerificationData
    {
        public decimal? paymentRequestAmount { get; set; }

        public decimal? reportedEuAmount { get; set; }

        public decimal? reportedBgAmount { get; set; }

        public decimal? reportedSelfAmount { get; set; }

        public decimal? totalUnapprovedEuAmount { get; set; }

        public decimal? totalUnapprovedBgAmount { get; set; }

        public decimal? totalUnapprovedSelfAmount { get; set; }

        public decimal? unapprovedEuAmount { get; set; }

        public decimal? unapprovedBgAmount { get; set; }

        public decimal? unapprovedSelfAmount { get; set; }

        public decimal? unapprovedByCorrectionEuAmount { get; set; }

        public decimal? unapprovedByCorrectionBgAmount { get; set; }

        public decimal? unapprovedByCorrectionSelfAmount { get; set; }

        public decimal? approvedEuAmount { get; set; }

        public decimal? approvedBgAmount { get; set; }

        public decimal? approvedSelfAmount { get; set; }

        public decimal? correctedApprovedEuAmount { get; set; }

        public decimal? correctedApprovedBgAmount { get; set; }

        public decimal? correctedApprovedSelfAmount { get; set; }

        public decimal? approvedPaymentEuAmount { get; set; }

        public decimal? approvedPaymentBgAmount { get; set; }
    }
}
