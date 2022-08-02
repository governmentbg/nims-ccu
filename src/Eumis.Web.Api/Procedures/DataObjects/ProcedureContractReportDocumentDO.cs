using Eumis.Domain.Procedures;
using Eumis.Domain.Procedures.ProcedureContractReportDocuments;

namespace Eumis.Web.Api.Procedures.DataObjects
{
    public class ProcedureContractReportDocumentDO
    {
        public ProcedureContractReportDocumentDO()
        {
        }

        public ProcedureContractReportDocumentDO(int procedureId, byte[] version)
        {
            this.ProcedureId = procedureId;
            this.Status = ActiveStatus.NotActivated;
            this.Version = version;
        }

        public ProcedureContractReportDocumentDO(ProcedureContractReportDocument procedureContractReportDocument, byte[] version)
        {
            this.ProcedureContractReportDocumentId = procedureContractReportDocument.ProcedureContractReportDocumentId;
            this.ProcedureId = procedureContractReportDocument.ProcedureId;
            this.Name = procedureContractReportDocument.Name;
            this.Extension = procedureContractReportDocument.Extension;
            this.IsRequired = procedureContractReportDocument.IsRequired;
            this.IsActivated = procedureContractReportDocument.IsActivated;
            this.IsActive = procedureContractReportDocument.IsActive;
            this.Status = !procedureContractReportDocument.IsActivated ?
                ActiveStatus.NotActivated :
                procedureContractReportDocument.IsActive ? ActiveStatus.Active : ActiveStatus.Inactive;
            this.Version = version;
        }

        public int ProcedureContractReportDocumentId { get; set; }

        public int ProcedureId { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public bool? IsRequired { get; set; }

        public bool IsActivated { get; set; }

        public bool IsActive { get; set; }

        public ActiveStatus Status { get; set; }

        public byte[] Version { get; set; }
    }
}
