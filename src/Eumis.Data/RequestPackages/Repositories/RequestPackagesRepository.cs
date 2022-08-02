using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Data.RequestPackages.ViewObjects;
using Eumis.Domain.RequestPackages;
using Eumis.Domain.UserOrganizations;
using Eumis.Domain.Users;
using Eumis.Domain.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Users.Repositories
{
    internal class RequestPackagesRepository : AggregateRepository<RequestPackage>, IRequestPackagesRepository
    {
        public RequestPackagesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<RequestPackage, object>>[] Includes
        {
            get
            {
                return new Expression<Func<RequestPackage, object>>[]
                {
                    rp => rp.RequestPackageUsers.Select(t => t.RegDataRequest),
                    rp => rp.RequestPackageUsers.Select(t => t.PermissionRequest),
                    rp => rp.File1,
                    rp => rp.File2,
                    rp => rp.File3,
                    rp => rp.File4,
                    rp => rp.File5,
                };
            }
        }

        public IList<RequestPackageVO> GetRequestPackages(
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            RequestPackageType? typeId = null,
            int? userOrganizationId = null,
            RequestPackageStatus? statusId = null)
        {
            var predicate = PredicateBuilder.True<RequestPackage>()
                .AndEquals(rp => rp.Type, typeId)
                .AndEquals(rp => rp.UserOrganizationId, userOrganizationId)
                .AndEquals(rp => rp.Status, statusId);

            if (dateFrom.HasValue)
            {
                predicate = predicate.And(rp => rp.CreateDate >= dateFrom.Value);
            }

            if (dateTo.HasValue)
            {
                var endDate = dateTo.Value.AddDays(1);
                predicate = predicate.And(rp => rp.CreateDate < endDate);
            }

            return (from rp in this.unitOfWork.DbContext.Set<RequestPackage>().Where(predicate)

                    join uo in this.unitOfWork.DbContext.Set<UserOrganization>() on rp.UserOrganizationId equals uo.UserOrganizationId into g1
                    from uo in g1.DefaultIfEmpty()

                    orderby rp.CreateDate descending

                    select new RequestPackageVO
                    {
                        RequestPackageId = rp.RequestPackageId,
                        Type = rp.Type,
                        Code = rp.Code,
                        CreateDate = rp.CreateDate,
                        Organization = uo != null ? uo.Name : null,
                        Status = rp.Status,
                        RequestPackageUsersCount = rp.RequestPackageUsers.Count,
                    })
                .ToList();
        }

        public IList<RequestPackageUserVO> GetRequestPackageUsers(int requestPackageId)
        {
            var results =
                   (from rp in this.unitOfWork.DbContext.Set<RequestPackage>()
                    join rpu in this.unitOfWork.DbContext.Set<RequestPackageUser>() on rp.RequestPackageId equals rpu.RequestPackageId
                    join u in this.unitOfWork.DbContext.Set<User>() on rpu.UserId equals u.UserId
                    join ut in this.unitOfWork.DbContext.Set<UserType>() on u.UserTypeId equals ut.UserTypeId
                    join uo in this.unitOfWork.DbContext.Set<UserOrganization>() on ut.UserOrganizationId equals uo.UserOrganizationId

                    join pr in this.unitOfWork.DbContext.Set<PermissionRequest>() on new { rpu.RequestPackageId, rpu.UserId } equals new { pr.RequestPackageId, pr.UserId } into g1
                    from pr in g1.DefaultIfEmpty()

                    join rdr in this.unitOfWork.DbContext.Set<RegDataRequest>() on new { rpu.RequestPackageId, rpu.UserId } equals new { rdr.RequestPackageId, rdr.UserId } into g2
                    from rdr in g2.DefaultIfEmpty()
                    where rp.RequestPackageId == requestPackageId
                    select new { rp, rpu, u, ut, uo, pr, rdr })
            .ToList();

            return results.Select(t => new RequestPackageUserVO
            {
                RequestPackageId = t.rp.RequestPackageId,
                UserId = t.rpu.UserId,
                Status = t.rpu.Status,
                StatusName = t.rpu.Status,
                Username = t.u.Username,
                Fullname = t.u.Fullname,
                UserType = t.ut.Name,
                UserOrganization = t.uo.Name,
                IsActive = t.u.IsActive,
                IsDeleted = t.u.IsDeleted,
                IsLocked = t.u.IsLocked,
                HasPermissionRequest = t.pr != null,
                HasRegDataRequest = t.rdr != null,
                RejectionMessage = t.rpu.RejectionMessage,
            })
            .ToList();
        }

        public int? GetUserOrganizationId(int requestPackageId)
        {
            return this.unitOfWork.DbContext.Set<RequestPackage>()
                .Where(r => r.RequestPackageId == requestPackageId)
                .Select(r => r.UserOrganizationId)
                .Single();
        }

        public RequestPackageStatus GetStatus(int requestPackageId)
        {
            return this.unitOfWork.DbContext.Set<RequestPackage>()
                .Where(r => r.RequestPackageId == requestPackageId)
                .Select(r => r.Status)
                .Single();
        }

        public RequestPackageType GetType(int requestPackageId)
        {
            return this.unitOfWork.DbContext.Set<RequestPackage>()
                .Where(r => r.RequestPackageId == requestPackageId)
                .Select(r => r.Type)
                .Single();
        }

        public RequestPackageInfoVO GetRequestPackageInfo(int requestPackageId)
        {
            return (from rp in this.unitOfWork.DbContext.Set<RequestPackage>()
                    where rp.RequestPackageId == requestPackageId
                    select new RequestPackageInfoVO()
                    {
                        Code = rp.Code,
                        Type = rp.Type,
                        Status = rp.Status,
                    })
                    .SingleOrDefault();
        }

        public IDictionary<int, (bool hasPermissionRequests, bool hasRegDataRequests)> GetPreviousRequestPackageData(int[] userIds)
        {
            var userPermissionRequests =
                from r in this.unitOfWork.DbContext.Set<RequestPackageUser>()

                join rp in this.unitOfWork.DbContext.Set<RequestPackage>() on r.RequestPackageId equals rp.RequestPackageId

                join pr in this.unitOfWork.DbContext.Set<PermissionRequest>() on new { r.RequestPackageId, r.UserId } equals new { pr.RequestPackageId, pr.UserId } into j1
                from pr in j1.DefaultIfEmpty()

                join rdr in this.unitOfWork.DbContext.Set<RegDataRequest>() on new { r.RequestPackageId, r.UserId } equals new { rdr.RequestPackageId, rdr.UserId } into j2
                from rdr in j2.DefaultIfEmpty()

                where
                    userIds.Contains(r.UserId) &&
                    r.Status == RequestStatus.Active &&
                    rp.Status == RequestPackageStatus.Ended
                select new
                {
                    UserID = r.UserId,
                    PermissionRequests = pr != null ? true : false,
                    RegDataRequests = rdr != null ? true : false,
                };

            var data = (from upr in userPermissionRequests
                        group upr by upr.UserID into g
                        select new
                        {
                            ID = g.Key,
                            PermissionRequests = g.Any(p => p.PermissionRequests == true),
                            RegDataRequests = g.Any(p => p.RegDataRequests == true),
                        })
                        .ToDictionary(
                            row => row.ID,
                            row => (hasPermissionRequests: row.PermissionRequests, hasRegDataRequests: row.RegDataRequests));

            return data;
        }
    }
}
