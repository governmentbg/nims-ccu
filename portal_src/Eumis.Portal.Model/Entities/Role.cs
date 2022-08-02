using System.Collections.Generic;

namespace Eumis.Portal.Model.Entities
{
    public partial class Role
    {
        public Role()
        {
            this.Users = new List<User>();
        }

        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Permissions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
