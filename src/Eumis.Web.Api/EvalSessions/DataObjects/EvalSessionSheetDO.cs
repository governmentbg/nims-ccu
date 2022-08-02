using System;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;

namespace Eumis.Web.Api.EvalSessions.DataObjects
{
    public class EvalSessionSheetDO
    {
        public EvalSessionSheetDO()
        {
        }

        public EvalSessionSheetDO(int evalSessionId, byte[] version)
        {
            var currentDate = DateTime.Now;

            this.EvalSessionId = evalSessionId;
            this.Status = EvalSessionSheetStatus.Draft;
            this.StatusDate = currentDate;
            this.CreateDate = currentDate;

            this.Version = version;
        }

        public EvalSessionSheetDO(EvalSessionSheet evalSessionSheet, byte[] version, EvalSessionSheetXml evalSessionSheetXml)
        {
            this.EvalSessionId = evalSessionSheet.EvalSessionId;
            this.EvalSessionSheetId = evalSessionSheet.EvalSessionSheetId;
            this.EvalSessionUserId = evalSessionSheet.EvalSessionUserId;
            this.ProjectId = evalSessionSheet.ProjectId;
            this.EvalTableType = evalSessionSheet.EvalTableType;
            this.Status = evalSessionSheet.Status;
            this.StatusNote = evalSessionSheet.StatusNote;
            this.StatusDate = evalSessionSheet.StatusDate;
            this.CreateDate = evalSessionSheet.CreateDate;
            this.Notes = evalSessionSheet.Notes;
            this.EvalSessionDistributionId = evalSessionSheet.EvalSessionDistributionId;
            this.DistributionType = evalSessionSheet.DistributionType;
            this.ContinuedEvalSessionSheetId = evalSessionSheet.ContinuedEvalSessionSheetId;
            this.XmlGid = evalSessionSheetXml.Gid;

            this.EvalType = evalSessionSheetXml.EvalType;
            this.EvalIsPassed = evalSessionSheetXml.EvalIsPassed;
            this.EvalPoints = evalSessionSheetXml.EvalPoints;
            this.EvalNote = evalSessionSheetXml.EvalNote;

            this.Version = version;
        }

        public int? EvalSessionId { get; set; }

        public int? EvalSessionSheetId { get; set; }

        public int? EvalSessionUserId { get; set; }

        public int? ProjectId { get; set; }

        public ProcedureEvalTableType? EvalTableType { get; set; }

        public EvalSessionSheetStatus? Status { get; set; }

        public string StatusNote { get; set; }

        public DateTime StatusDate { get; set; }

        public DateTime CreateDate { get; set; }

        public string Notes { get; set; }

        public int? EvalSessionDistributionId { get; set; }

        public EvalSessionDistributionType? DistributionType { get; set; }

        public int? ContinuedEvalSessionSheetId { get; set; }

        public Guid? XmlGid { get; set; }

        public ProcedureEvalType? EvalType { get; set; }

        public bool? EvalIsPassed { get; set; }

        public decimal? EvalPoints { get; set; }

        public string EvalNote { get; set; }

        public byte[] Version { get; set; }
    }
}
