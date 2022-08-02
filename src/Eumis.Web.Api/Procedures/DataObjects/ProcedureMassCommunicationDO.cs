using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.Procedures.DataObjects
{
    public class ProcedureMassCommunicationDO
    {
        public ProcedureMassCommunicationDO()
        {
        }

        public ProcedureMassCommunicationDO(ProcedureMassCommunication communication)
        {
            this.ProcedureMassCommunicationId = communication.ProcedureMassCommunicationId;
            this.ProgrammeId = communication.ProgrammeId;
            this.ProcedureId = communication.ProcedureId;
            this.ModifyDate = communication.ModifyDate;
            this.Status = communication.Status;
            this.Subject = communication.Subject;
            this.Body = communication.Body;

            this.Version = communication.Version;
        }

        public int ProcedureMassCommunicationId { get; set; }

        public int? ProgrammeId { get; set; }

        public int? ProcedureId { get; set; }

        public ProcedureMassCommunicationStatus Status { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
