using System;
using Eumis.Domain.EvalSessions;

namespace Eumis.Web.Api.EvalSessions.DataObjects
{
    public class EvalSessionStandpointDO
    {
        public EvalSessionStandpointDO()
        {
        }

        public EvalSessionStandpointDO(int evalSessionId, byte[] version)
        {
            var currentDate = DateTime.Now;

            this.EvalSessionId = evalSessionId;
            this.Status = EvalSessionStandpointStatus.Draft;
            this.StatusDate = currentDate;
            this.CreateDate = currentDate;

            this.Version = version;
        }

        public EvalSessionStandpointDO(
            EvalSessionStandpoint evalSessionStandpoint,
            byte[] version,
            EvalSessionStandpointXml evalSessionStandpointXml)
        {
            this.EvalSessionId = evalSessionStandpoint.EvalSessionId;
            this.EvalSessionStandpointId = evalSessionStandpoint.EvalSessionStandpointId;
            this.EvalSessionUserId = evalSessionStandpoint.EvalSessionUserId;
            this.ProjectId = evalSessionStandpoint.ProjectId;
            this.Note = evalSessionStandpoint.Note;
            this.Status = evalSessionStandpoint.Status;
            this.StatusDate = evalSessionStandpoint.StatusDate;
            this.CreateDate = evalSessionStandpoint.CreateDate;
            this.DeleteNote = evalSessionStandpoint.DeleteNote;
            this.XmlGid = evalSessionStandpointXml.Gid;
            this.Version = version;
        }

        public int? EvalSessionId { get; set; }

        public int? EvalSessionStandpointId { get; set; }

        public int? EvalSessionUserId { get; set; }

        public int? ProjectId { get; set; }

        public string Note { get; set; }

        public EvalSessionStandpointStatus? Status { get; set; }

        public DateTime StatusDate { get; set; }

        public DateTime CreateDate { get; set; }

        public string DeleteNote { get; set; }

        public Guid? XmlGid { get; set; }

        public byte[] Version { get; set; }
    }
}
