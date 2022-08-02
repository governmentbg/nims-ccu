using Eumis.Domain.Core;
using Eumis.Domain.Procurements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.Procurements.DataOjects
{
    public class ProcurementDocumentDO
    {
        public ProcurementDocumentDO()
        {
        }

        public ProcurementDocumentDO(int procurementId, byte[] version)
        {
            this.ProcurementId = procurementId;
            this.Version = version;
        }

        public ProcurementDocumentDO(ProcurementDocument procurementDocument, byte[] version)
        {
            this.ProcurementDocumentId = procurementDocument.ProcurementDocumentId;
            this.ProcurementId = procurementDocument.ProcurementId;
            this.Name = procurementDocument.Name;
            this.Description = procurementDocument.Description;

            if (procurementDocument.File != null)
            {
                this.File = new FileDO
                {
                    Key = procurementDocument.File.Key,
                    Name = procurementDocument.File.FileName,
                };
            }

            this.Version = version;
        }

        public int ProcurementDocumentId { get; set; }

        public int ProcurementId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
