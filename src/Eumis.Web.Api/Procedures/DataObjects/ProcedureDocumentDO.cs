using Eumis.Domain.Core;
using Eumis.Domain.Procedures;

namespace Eumis.Web.Api.Procedures.DataObjects
{
    public class ProcedureDocumentDO
    {
        public ProcedureDocumentDO()
        {
        }

        public ProcedureDocumentDO(int procedureId, byte[] version)
        {
            this.ProcedureId = procedureId;
            this.Version = version;
        }

        public ProcedureDocumentDO(ProcedureDocument procedureDocument, byte[] version)
        {
            this.ProcedureDocumentId = procedureDocument.ProcedureDocumentId;
            this.ProcedureId = procedureDocument.ProcedureId;
            this.Name = procedureDocument.Name;
            this.Description = procedureDocument.Description;

            if (procedureDocument.File != null)
            {
                this.File = new FileDO
                {
                    Key = procedureDocument.File.Key,
                    Name = procedureDocument.File.FileName,
                };
            }

            this.Version = version;
        }

        public int ProcedureDocumentId { get; set; }

        public int ProcedureId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
