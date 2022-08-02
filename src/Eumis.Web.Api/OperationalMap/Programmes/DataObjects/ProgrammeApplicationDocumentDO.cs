using Eumis.Domain.OperationalMap.Programmes;
using System.Collections.Generic;

namespace Eumis.Web.Api.OperationalMap.Programmes.DataObjects
{
    public class ProgrammeApplicationDocumentDO
    {
        public ProgrammeApplicationDocumentDO()
        {
        }

        public ProgrammeApplicationDocumentDO(int programmeId, byte[] version)
        {
            this.ProgrammeId = programmeId;
            this.Version = version;
            this.IsActive = true;
        }

        public ProgrammeApplicationDocumentDO(ProgrammeApplicationDocument programmeApplicationDocument, bool isDocumentAttached, byte[] version)
        {
            this.ProgrammeApllicationDocumentId = programmeApplicationDocument.ProgrammeApplicationDocumentId;
            this.ProgrammeId = programmeApplicationDocument.ProgrammeId;
            this.Name = programmeApplicationDocument.Name;
            this.Extension = programmeApplicationDocument.Extension;
            this.IsSignatureRequired = programmeApplicationDocument.IsSignatureRequired;
            this.IsActive = programmeApplicationDocument.IsActive;
            this.IsAttached = isDocumentAttached;
            this.Version = version;
        }

        public int ProgrammeApllicationDocumentId { get; set; }

        public int ProgrammeId { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public bool IsSignatureRequired { get; set; }

        public bool IsActive { get; set; }

        public bool IsAttached { get; set; }

        public byte[] Version { get; set; }
    }
}
