using Eumis.Domain.RequestPackages;

namespace Eumis.Authentication.Authorization.ClaimsContexts.RequestPackage
{
    internal delegate IRequestPackageClaimsContext RequestPackageClaimsContextFactory(int requestPackageId);

    internal interface IRequestPackageClaimsContext
    {
        int? UserOrganizationId { get; }

        RequestPackageStatus Status { get; }

        RequestPackageType Type { get; }
    }
}
