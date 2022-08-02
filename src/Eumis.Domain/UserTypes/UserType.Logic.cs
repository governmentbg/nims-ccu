using System;

namespace Eumis.Domain.UserTypes
{
    public partial class UserType : IAggregateRoot
    {
        public void UpdateAttributes(string name, bool isSuperUser)
        {
            this.Name = name;
            this.IsSuperUser = isSuperUser;

            this.ModifyDate = DateTime.Now;
        }
    }
}
