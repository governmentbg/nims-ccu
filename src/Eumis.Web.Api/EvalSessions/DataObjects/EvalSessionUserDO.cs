using Eumis.Domain.EvalSessions;

namespace Eumis.Web.Api.EvalSessions.DataObjects
{
    public class EvalSessionUserDO
    {
        public EvalSessionUserDO()
        {
        }

        public EvalSessionUserDO(int evalSessionId, byte[] version)
        {
            this.EvalSessionId = evalSessionId;
            this.Version = version;
        }

        public EvalSessionUserDO(EvalSessionUser evalSessionUser, byte[] version)
        {
            this.EvalSessionUserId = evalSessionUser.EvalSessionUserId;
            this.EvalSessionId = evalSessionUser.EvalSessionId;
            this.UserId = evalSessionUser.UserId;
            this.Type = evalSessionUser.Type;
            this.Status = evalSessionUser.Status;
            this.Position = evalSessionUser.Position;

            this.Version = version;
        }

        public int? EvalSessionUserId { get; set; }

        public int? EvalSessionId { get; set; }

        public int? UserId { get; set; }

        public EvalSessionUserType? Type { get; set; }

        public EvalSessionUserStatus Status { get; set; }

        public string Position { get; set; }

        public byte[] Version { get; set; }
    }
}
