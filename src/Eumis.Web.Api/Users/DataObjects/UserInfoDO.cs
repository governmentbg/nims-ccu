using Eumis.Domain.Users;

namespace Eumis.Web.Api.Users.DataObjects
{
    public class UserInfoDO
    {
        public UserInfoDO(User user)
        {
            this.Username = user.Username;
            this.Fullname = user.Fullname;
            this.IsActive = user.IsActive;
            this.IsDeleted = user.IsDeleted;
            this.IsLocked = user.IsLocked;
        }

        public string Username { get; set; }

        public string Fullname { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsLocked { get; set; }
    }
}
