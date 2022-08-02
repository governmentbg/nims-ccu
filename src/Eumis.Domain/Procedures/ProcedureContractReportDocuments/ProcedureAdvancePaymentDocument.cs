using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures.ProcedureContractReportDocuments
{
    public class ProcedureAdvancePaymentDocument : ProcedureContractReportDocument
    {
        public ProcedureAdvancePaymentDocument()
        {
        }

        public ProcedureAdvancePaymentDocument(string name, string extension, bool isRequired)
            : base(name, extension, isRequired)
        {
        }

        public override ProcedureContractReportDocumentType Type
        {
            get
            {
                return ProcedureContractReportDocumentType.AdvancePaymentDocument;
            }
        }
    }
}
