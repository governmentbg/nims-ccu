using System.Collections.Generic;

namespace Eumis.Portal.Model.Entities
{
    public partial class User
    {
        public User()
        {
            this.Roles = new List<Role>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Fullname { get; set; }
        public string Notes { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
