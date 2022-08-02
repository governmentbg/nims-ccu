namespace Eumis.Authentication.Authorization.ClaimsContexts.User
{
    public delegate IUserClaimsContext UserClaimsContextFactory(int userId);

    public interface IUserClaimsContext
    {
        bool IsSuperUser { get; }

        bool IsMonitoringUser { get; }

        int UserOrganizationId { get; }

        int UserId { get; }

        string Fullname { get; }

        string Username { get; }
    }
}
