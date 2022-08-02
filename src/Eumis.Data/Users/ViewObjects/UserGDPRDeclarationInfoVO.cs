namespace Eumis.Data.Users.ViewObjects
{
    public class UserGDPRDeclarationInfoVO
    {
        public string Username { get; set; }

        public string Fullname { get; set; }

        public string Email { get; set; }

        public bool HasAcceptedGDPRDeclaration { get; set; }
    }
}
