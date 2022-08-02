using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures.ProcedureContractReportDocuments
{
    public class ProcedureProcurementDocument : ProcedureContractReportDocument
    {
        public ProcedureProcurementDocument()
        {
        }

        public ProcedureProcurementDocument(string name, string extension, bool isRequired)
            : base(name, extension, isRequired)
        {
        }

        public override ProcedureContractReportDocumentType Type
        {
            get
            {
                return ProcedureContractReportDocumentType.ProcurementDocument;
            }
        }
    }
}
