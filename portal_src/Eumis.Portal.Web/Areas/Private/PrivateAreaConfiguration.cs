namespace Eumis.Portal.Web.Areas.Private
{
    public class PrivateAreaConfiguration
    {
        public static EumisUser CreatePrivateUser(string accessToken)
        {
            return new EumisUser()
            {
                AccessToken = accessToken,
                IsPrivate = true,

                FirstName = string.Empty,
                LastName = string.Empty,
                Phone = string.Empty,
                Email = string.Empty,
            };
        }
    }
}