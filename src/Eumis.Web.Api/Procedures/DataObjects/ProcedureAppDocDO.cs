using Eumis.Domain.Procedures;

namespace Eumis.Web.Api.Procedures.DataObjects
{
    public class ProcedureAppDocDO
    {
        public ProcedureAppDocDO()
        {
        }

        public ProcedureAppDocDO(int procedureId, byte[] version)
        {
            this.ProcedureId = procedureId;
            this.Status = ActiveStatus.NotActivated;
            this.Version = version;
        }

        public ProcedureAppDocDO(ProcedureApplicationDoc procedureAppDoc, byte[] version, string documentExtension = null)
        {
            this.ProcedureApplicationDocId = procedureAppDoc.ProcedureApplicationDocId;
            this.ProcedureId = procedureAppDoc.ProcedureId;
            this.ProgrammeApplicationDocumentId = procedureAppDoc.ProgrammeApplicationDocumentId;
            this.Name = procedureAppDoc.Name;
            this.Extension = documentExtension ?? procedureAppDoc.Extension;
            this.IsRequired = procedureAppDoc.IsRequired;
            this.IsSignatureRequired = procedureAppDoc.IsSignatureRequired;
            this.IsActivated = procedureAppDoc.IsActivated;
            this.IsActive = procedureAppDoc.IsActive;
            this.Status = !procedureAppDoc.IsActivated ?
                ActiveStatus.NotActivated :
                procedureAppDoc.IsActive ? ActiveStatus.Active : ActiveStatus.Inactive;
            this.Version = version;
        }

        public int ProcedureApplicationDocId { get; set; }

        public int ProcedureId { get; set; }

        public int? ProgrammeApplicationDocumentId { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public bool? IsRequired { get; set; }

        public bool? IsSignatureRequired { get; set; }

        public bool IsActivated { get; set; }

        public bool IsActive { get; set; }

        public ActiveStatus Status { get; set; }

        public byte[] Version { get; set; }
    }
}
