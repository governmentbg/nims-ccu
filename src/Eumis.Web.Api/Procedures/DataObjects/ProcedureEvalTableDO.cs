using System;
using Eumis.Domain.Procedures;

namespace Eumis.Web.Api.Procedures.DataObjects
{
    public class ProcedureEvalTableDO
    {
        public ProcedureEvalTableDO()
        {
        }

        public ProcedureEvalTableDO(int procedureId, byte[] version)
        {
            this.ProcedureId = procedureId;
            this.EvalTableStatus = ProcedureEvalTableStatus.Draft;
            this.Status = ActiveStatus.NotActivated;
            this.Version = version;
        }

        public ProcedureEvalTableDO(ProcedureEvalTable procedureEvalTable, Guid xmlGid, byte[] version)
        {
            this.ProcedureEvalTableId = procedureEvalTable.ProcedureEvalTableId;
            this.ProcedureId = procedureEvalTable.ProcedureId;
            this.Name = procedureEvalTable.Name;
            this.Type = procedureEvalTable.Type;
            this.EvalType = procedureEvalTable.EvalType;
            this.EvalTableStatus = procedureEvalTable.Status;
            this.XmlGid = xmlGid;
            this.IsActivated = procedureEvalTable.IsActivated;
            this.IsActive = procedureEvalTable.IsActive;
            this.Status = !procedureEvalTable.IsActivated ?
                ActiveStatus.NotActivated :
                procedureEvalTable.IsActive ? ActiveStatus.Active : ActiveStatus.Inactive;
            this.Version = version;
        }

        public int ProcedureEvalTableId { get; set; }

        public int ProcedureId { get; set; }

        public string Name { get; set; }

        public ProcedureEvalTableType? Type { get; set; }

        public ProcedureEvalType? EvalType { get; set; }

        public ProcedureEvalTableStatus? EvalTableStatus { get; set; }

        public Guid? XmlGid { get; set; }

        public bool IsActivated { get; set; }

        public bool IsActive { get; set; }

        public ActiveStatus Status { get; set; }

        public byte[] Version { get; set; }
    }
}
