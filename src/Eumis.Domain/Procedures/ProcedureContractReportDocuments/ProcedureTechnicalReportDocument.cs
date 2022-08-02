using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures.ProcedureContractReportDocuments
{
    public class ProcedureTechnicalReportDocument : ProcedureContractReportDocument
    {
        public ProcedureTechnicalReportDocument()
        {
        }

        public ProcedureTechnicalReportDocument(string name, string extension, bool isRequired)
            : base(name, extension, isRequired)
        {
        }

        public override ProcedureContractReportDocumentType Type
        {
            get
            {
                return ProcedureContractReportDocumentType.TechnicalReportDocument;
            }
        }
    }
}
