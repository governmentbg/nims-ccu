using System;

namespace Eumis.Domain.UserOrganizations
{
    public partial class UserOrganization : IAggregateRoot
    {
        public void UpdateUserOrganization(
            string name)
        {
            this.Name = name;

            this.ModifyDate = DateTime.Now;
        }
    }
}
