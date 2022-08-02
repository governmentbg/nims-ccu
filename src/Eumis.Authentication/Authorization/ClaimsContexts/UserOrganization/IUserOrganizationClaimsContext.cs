namespace Eumis.Authentication.Authorization.ClaimsContexts.UserOrganization
{
    internal delegate IUserOrganizationClaimsContext UserOrganizationClaimsContextFactory(int userOrganizationId);

    internal interface IUserOrganizationClaimsContext
    {
        int UserOrganizationId { get; }
    }
}
