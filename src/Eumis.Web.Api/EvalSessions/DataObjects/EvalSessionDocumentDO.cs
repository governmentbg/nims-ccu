using Eumis.Domain.Core;
using Eumis.Domain.EvalSessions;

namespace Eumis.Web.Api.EvalSessions.DataObjects
{
    public class EvalSessionDocumentDO
    {
        public EvalSessionDocumentDO()
        {
        }

        public EvalSessionDocumentDO(int evalSessionId, byte[] version)
        {
            this.EvalSessionId = evalSessionId;
            this.Version = version;
        }

        public EvalSessionDocumentDO(EvalSessionDocument evalSessionDocument, byte[] version)
        {
            this.EvalSessionId = evalSessionDocument.EvalSessionId;
            this.EvalSessionDocumentId = evalSessionDocument.EvalSessionDocumentId;
            this.Name = evalSessionDocument.Name;
            this.Description = evalSessionDocument.Description;

            if (evalSessionDocument.File != null)
            {
                this.File = new FileDO
                {
                    Key = evalSessionDocument.File.Key,
                    Name = evalSessionDocument.File.FileName,
                };
            }

            this.IsDeleted = evalSessionDocument.IsDeleted;
            this.IsDeletedNote = evalSessionDocument.IsDeletedNote;

            this.Version = version;
        }

        public int? EvalSessionId { get; set; }

        public int? EvalSessionDocumentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public bool? IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }

        public byte[] Version { get; set; }
    }
}
