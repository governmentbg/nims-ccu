using Eumis.Domain.Core;
using Eumis.Domain.Procedures;
using System;

namespace Eumis.Web.Api.Procedures.DataObjects
{
    public class ProcedureQuestionDO
    {
        public ProcedureQuestionDO()
        {
        }

        public ProcedureQuestionDO(int procedureId, string username, byte[] version)
        {
            this.ProcedureId = procedureId;
            this.CreatedByUser = username;
            this.Status = ActiveStatus.NotActivated;
            this.Version = version;
        }

        public ProcedureQuestionDO(ProcedureQuestion procedureQuestion, string username, byte[] version)
        {
            this.ProcedureQuestionId = procedureQuestion.ProcedureQuestionId;
            this.ProcedureId = procedureQuestion.ProcedureId;
            this.CreatedByUser = username;
            this.CreateDate = procedureQuestion.CreateDate;
            this.IsActivated = procedureQuestion.IsActivated;
            this.Status = procedureQuestion.IsActivated ? ActiveStatus.Active : ActiveStatus.NotActivated;

            if (procedureQuestion.File != null)
            {
                this.File = new FileDO
                {
                    Key = procedureQuestion.File.Key,
                    Name = procedureQuestion.File.FileName,
                };
            }

            this.Version = version;
        }

        public int ProcedureQuestionId { get; set; }

        public int ProcedureId { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsActivated { get; set; }

        public ActiveStatus Status { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
