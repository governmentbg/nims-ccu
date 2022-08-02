using System;
using System.Collections.Generic;
using Eumis.Data.RequestPackages.ViewObjects;
using Eumis.Domain.RequestPackages;

namespace Eumis.Data.Users.Repositories
{
    public interface IRequestPackagesRepository : IAggregateRepository<RequestPackage>
    {
        IList<RequestPackageVO> GetRequestPackages(
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            RequestPackageType? typeId = null,
            int? userOrganizationId = null,
            RequestPackageStatus? statusId = null);

        IList<RequestPackageUserVO> GetRequestPackageUsers(int requestPackageId);

        int? GetUserOrganizationId(int requestPackageId);

        RequestPackageStatus GetStatus(int requestPackageId);

        RequestPackageType GetType(int requestPackageId);

        RequestPackageInfoVO GetRequestPackageInfo(int requestPackageId);

        IDictionary<int, (bool hasPermissionRequests, bool hasRegDataRequests)> GetPreviousRequestPackageData(int[] userIds);
    }
}
