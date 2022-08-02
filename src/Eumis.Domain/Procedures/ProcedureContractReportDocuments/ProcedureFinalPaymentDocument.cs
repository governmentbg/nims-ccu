using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures.ProcedureContractReportDocuments
{
    public class ProcedureFinalPaymentDocument : ProcedureContractReportDocument
    {
        public ProcedureFinalPaymentDocument()
        {
        }

        public ProcedureFinalPaymentDocument(string name, string extension, bool isRequired)
            : base(name, extension, isRequired)
        {
        }

        public override ProcedureContractReportDocumentType Type
        {
            get
            {
                return ProcedureContractReportDocumentType.FinalPaymentDocument;
            }
        }
    }
}
