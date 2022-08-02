using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures.ProcedureContractReportDocuments
{
    public class ProcedureFinancialReportDocument : ProcedureContractReportDocument
    {
        public ProcedureFinancialReportDocument()
        {
        }

        public ProcedureFinancialReportDocument(string name, string extension, bool isRequired)
            : base(name, extension, isRequired)
        {
        }

        public override ProcedureContractReportDocumentType Type
        {
            get
            {
                return ProcedureContractReportDocumentType.FinancialReportDocument;
            }
        }
    }
}
