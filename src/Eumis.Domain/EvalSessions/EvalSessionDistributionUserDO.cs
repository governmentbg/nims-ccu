namespace Eumis.Domain.EvalSessions
{
    public class EvalSessionDistributionUserDO
    {
        public EvalSessionDistributionUserDO()
        {
            this.IsDeleted = false;
        }

        public EvalSessionDistributionUserDO(EvalSessionDistributionUser evalSessionDistributionUser, string username, string fullname)
        {
            this.EvalSessionId = evalSessionDistributionUser.EvalSessionId;
            this.EvalSessionDistributionId = evalSessionDistributionUser.EvalSessionDistributionId;
            this.EvalSessionUserId = evalSessionDistributionUser.EvalSessionUserId;
            this.Username = username;
            this.Fullname = fullname;
            this.IsDeleted = evalSessionDistributionUser.IsDeleted;
            this.IsDeletedNote = evalSessionDistributionUser.IsDeletedNote;
        }

        public int EvalSessionId { get; set; }

        public int? EvalSessionDistributionId { get; set; }

        public int EvalSessionUserId { get; set; }

        public string Username { get; set; }

        public string Fullname { get; set; }

        public bool IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }
    }
}
