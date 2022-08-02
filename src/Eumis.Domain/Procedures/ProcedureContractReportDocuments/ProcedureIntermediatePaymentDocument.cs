using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures.ProcedureContractReportDocuments
{
    public class ProcedureIntermediatePaymentDocument : ProcedureContractReportDocument
    {
        public ProcedureIntermediatePaymentDocument()
        {
        }

        public ProcedureIntermediatePaymentDocument(string name, string extension, bool isRequired)
            : base(name, extension, isRequired)
        {
        }

        public override ProcedureContractReportDocumentType Type
        {
            get
            {
                return ProcedureContractReportDocumentType.IntermediatePaymentDocument;
            }
        }
    }
}
