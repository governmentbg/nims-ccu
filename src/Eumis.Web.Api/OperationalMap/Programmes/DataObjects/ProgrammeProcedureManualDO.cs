using Eumis.Domain.Core;
using Eumis.Domain.OperationalMap.ProcedureManuals;
using System;

namespace Eumis.Web.Api.OperationalMap.Programmes.DataObjects
{
    public class ProgrammeProcedureManualDO
    {
        public ProgrammeProcedureManualDO()
        {
        }

        public ProgrammeProcedureManualDO(int programmeId, int orderNum, byte[] version)
        {
            this.ProgrammeId = programmeId;
            this.OrderNum = orderNum;
            this.Status = ProgrammeProcedureManualStatus.Draft;

            this.Version = version;
        }

        public ProgrammeProcedureManualDO(ProgrammeProcedureManual programmeProcedureManual, string username, byte[] version)
        {
            this.ProgrammeProcedureManualId = programmeProcedureManual.ProgrammeProcedureManualId;
            this.ProgrammeId = programmeProcedureManual.MapNodeId;
            this.Name = programmeProcedureManual.Name;
            this.Description = programmeProcedureManual.Description;
            this.OrderNum = programmeProcedureManual.OrderNum;
            this.Status = programmeProcedureManual.Status;
            this.ActivationDate = programmeProcedureManual.ActivationDate;
            this.Username = username;

            this.Version = version;

            if (programmeProcedureManual.File != null)
            {
                this.File = new FileDO
                {
                    Key = programmeProcedureManual.File.Key,
                    Name = programmeProcedureManual.File.FileName,
                };
            }
        }

        public int ProgrammeProcedureManualId { get; set; }

        public int ProgrammeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int OrderNum { get; set; }

        public ProgrammeProcedureManualStatus Status { get; set; }

        public DateTime? ActivationDate { get; set; }

        public string Username { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
