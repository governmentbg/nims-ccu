using System;

namespace Eumis.Data.Contracts.PortalViewObjects
{
    public class ContractAccessCodePVO
    {
        public Guid Gid { get; set; }

        public string Code { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public string Identifier { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public AccessCodePermissionPVO Permissions { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
