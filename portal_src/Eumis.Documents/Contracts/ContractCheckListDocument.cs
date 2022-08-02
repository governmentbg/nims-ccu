using Eumis.Common.Localization;

namespace Eumis.Documents.Contracts
{
    public class ContractCheckListDocument : ContractDocumentXml
    {
        public string programmeName { get; set; }

        public string checkListName { get; set; }

        public string description { get; set; }

        public string versionNum { get; set; }

        public ProgrammeCheckListType type { get; set; }

        public string checkListTypeText { get; set; }
    }

    public enum ProgrammeCheckListType
    {
        Procedure = 1,
        Contract = 2,
        ContractReport = 3,
        Procurements = 4,
        CertReport = 5,
        SpotCheck = 6,
        Programme = 7,
        IrregularitySignal = 8,
        ContractReportFinancialCorrection = 9,
        ContractReportFinancialRevalidation = 10,
        ContractReportCorrection = 11,
        ContractReportRevalidation = 12,
    }
}
