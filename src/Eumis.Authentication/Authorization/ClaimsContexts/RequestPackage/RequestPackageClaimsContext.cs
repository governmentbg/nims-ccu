using System;
using Autofac.Features.AttributeFilters;
using Eumis.Authentication.AccessContexts;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.RequestPackages;

namespace Eumis.Authentication.Authorization.ClaimsContexts.RequestPackage
{
    internal class RequestPackageClaimsContext : ClaimsContext, IRequestPackageClaimsContext
    {
        private int requestPackageId;

        private IClaimsCache claimsCache;
        private IRequestPackagesRepository requestPackagesRepository;

        public RequestPackageClaimsContext(
            int requestPackageId,
            [KeyFilter(ClaimsCaches.RequestPackage)]IClaimsCache claimsCache,
            IRequestPackagesRepository requestPackagesRepository)
            : base(claimsCache)
        {
            this.requestPackageId = requestPackageId;
            this.claimsCache = claimsCache;
            this.requestPackagesRepository = requestPackagesRepository;
        }

        public int? UserOrganizationId
        {
            get
            {
                return this.GetClaim(
                    this.requestPackageId,
                    new ClaimKey("UserOrganizationId"),
                    () => this.requestPackagesRepository.GetUserOrganizationId(this.requestPackageId));
            }
        }

        public RequestPackageStatus Status
        {
            get
            {
                return this.GetClaim(
                    this.requestPackageId,
                    new ClaimKey("Status"),
                    () => this.requestPackagesRepository.GetStatus(this.requestPackageId));
            }
        }

        public RequestPackageType Type
        {
            get
            {
                return this.GetClaim(
                    this.requestPackageId,
                    new ClaimKey("Type"),
                    () => this.requestPackagesRepository.GetType(this.requestPackageId));
            }
        }
    }
}
