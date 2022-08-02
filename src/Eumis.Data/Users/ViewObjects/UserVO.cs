namespace Eumis.Data.Users.ViewObjects
{
    public class UserVO
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Fullname { get; set; }

        public string UserType { get; set; }

        public string UserOrganization { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsLocked { get; set; }

        public string Email { get; set; }

        public bool HasAcceptedGDPRDeclaration { get; set; }
    }
}
