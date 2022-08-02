namespace Eumis.Authentication.Authorization.ClaimsContexts.UserOrganization
{
    internal class UserOrganizationClaimsContext : IUserOrganizationClaimsContext
    {
        private int userOrganizationId;

        public UserOrganizationClaimsContext(int userOrganizationId)
        {
            this.userOrganizationId = userOrganizationId;
        }

        public int UserOrganizationId
        {
            get
            {
                return this.userOrganizationId;
            }
        }
    }
}
