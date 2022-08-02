using Eumis.Domain.Users;
using System;

namespace Eumis.Web.Api.Users.DataObjects
{
    public class UserDO
    {
        public UserDO()
        {
        }

        public UserDO(User user)
        {
            this.UserId = user.UserId;
            this.Username = user.Username;
            this.UserTypeId = user.UserTypeId;
            this.UserOrganizationId = user.UserOrganizationId;

            this.Uin = user.Uin;
            this.Fullname = user.Fullname;
            this.Email = user.Email;
            this.Phone = user.Phone;
            this.Address = user.Address;
            this.Position = user.Position;
            this.IsActive = user.IsActive;
            this.IsDeleted = user.IsDeleted;
            this.IsLocked = user.IsLocked;
            this.HasAcceptedGDPRDeclaration = user.GDPRDeclarationAcceptDate.HasValue;
            this.GDPRDeclarationAcceptDate = user.GDPRDeclarationAcceptDate;

            this.Version = user.Version;
        }

        public UserDO(User user, UserVisibilityDO userVisibility)
            : this(user)
        {
            this.UserVisibility = userVisibility;
        }

        public int UserId { get; set; }

        public string Username { get; set; }

        public string Uin { get; set; }

        public int? UserTypeId { get; set; }

        public int? UserOrganizationId { get; set; }

        public string Fullname { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Position { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsLocked { get; set; }

        public bool HasAcceptedGDPRDeclaration { get; set; }

        public DateTime? GDPRDeclarationAcceptDate { get; set; }

        public byte[] Version { get; set; }

        public UserVisibilityDO UserVisibility { get; set; }
    }
}
