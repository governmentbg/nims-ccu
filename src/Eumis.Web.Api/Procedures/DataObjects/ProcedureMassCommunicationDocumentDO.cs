using Eumis.Domain.Core;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.Procedures.DataObjects
{
    public class ProcedureMassCommunicationDocumentDO
    {
        public ProcedureMassCommunicationDocumentDO()
        {
        }

        public ProcedureMassCommunicationDocumentDO(int communicationId, byte[] version)
            : this()
        {
            this.CommunicationId = communicationId;
            this.Version = version;
        }

        public ProcedureMassCommunicationDocumentDO(ProcedureMassCommunicationDocument document, byte[] version)
            : this(document.ProcedureMassCommunicationId, version)
        {
            this.ProcedureMassCommunicationDocumentId = document.ProcedureMassCommunicationDocumentId;
            this.Name = document.Name;
            this.Description = document.Description;
            this.File = document.FileKey.HasValue ? new FileDO
            {
                Key = document.FileKey.Value,
                Name = document.FileName,
            }
            : null;
        }

        public int ProcedureMassCommunicationDocumentId { get; set; }

        public int CommunicationId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
