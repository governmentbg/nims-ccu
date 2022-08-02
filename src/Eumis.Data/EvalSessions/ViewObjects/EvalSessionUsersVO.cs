using Eumis.Common.Json;
using Eumis.Domain.EvalSessions;
using Newtonsoft.Json;

namespace Eumis.Data.EvalSessions.ViewObjects
{
    public class EvalSessionUsersVO
    {
        public int EvalSessionUserId { get; set; }

        public int EvalSessionId { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public string Fullname { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionUserType? Type { get; set; }

        public EvalSessionUserStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionUserStatus? StatusName { get; set; }

        public string Position { get; set; }
    }
}
