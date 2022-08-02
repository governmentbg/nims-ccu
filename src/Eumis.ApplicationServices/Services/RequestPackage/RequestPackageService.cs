using Eumis.Authentication.Authorization;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Users.Repositories;
using Eumis.Domain;
using Eumis.Domain.Core;
using Eumis.Domain.Events;
using Eumis.Domain.RequestPackages;
using Eumis.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.ApplicationServices.Services.RequestPackage
{
    public class RequestPackageService : IRequestPackageService
    {
        private IUnitOfWork unitOfWork;
        private IUsersRepository usersRepository;
        private IRequestPackagesRepository requestPackagesRepository;
        private IAccessContext accessContext;
        private ICacheManager cacheManager;
        private IProgrammeCacheManager programmeCacheManager;

        public RequestPackageService(
            IUnitOfWork unitOfWork,
            IUsersRepository usersRepository,
            IRequestPackagesRepository requestPackagesRepository,
            IAccessContext accessContext,
            ICacheManager cacheManager,
            IProgrammeCacheManager programmeCacheManager)
        {
            this.unitOfWork = unitOfWork;
            this.usersRepository = usersRepository;
            this.requestPackagesRepository = requestPackagesRepository;
            this.accessContext = accessContext;
            this.cacheManager = cacheManager;
            this.programmeCacheManager = programmeCacheManager;
        }

        public IList<string> CanEndRequestPackage(int requestPackageId)
        {
            var requestPackage = this.requestPackagesRepository.Find(requestPackageId);
            var errors = requestPackage.CanEnd().ToList();

            foreach (var rpu in requestPackage.RequestPackageUsers)
            {
                if (rpu.RegDataRequest != null && this.usersRepository.OtherUserExists(rpu.UserId, rpu.RegDataRequest.Uin))
                {
                    errors.Add("Съществува друг потребител със същия ЕГН");
                }
            }

            return errors;
        }

        public void EndRequestPackage(int requestPackageId, byte[] version, string endingMessage)
        {
            var requestPackage = this.requestPackagesRepository.FindForUpdate(requestPackageId, version);
            int[] userIds = requestPackage.RequestPackageUsers.Select(t => t.UserId).ToArray();
            var users = this.usersRepository.FindAll(userIds);

            if (requestPackage.Type == RequestPackageType.Request && requestPackage.Status != RequestPackageStatus.Checked)
            {
                throw new DomainValidationException("RequestPackage status transition not allowed");
            }

            if (requestPackage.RequestPackageUsers.Count != users.Count)
            {
                throw new DomainValidationException("RequestPackageUsers or User changed");
            }

            if (requestPackage.Type == RequestPackageType.Request && (!requestPackage.EnteredByUserId.HasValue || !requestPackage.CheckedByUserId.HasValue))
            {
                throw new DomainValidationException("RequestPackage cannot have EnteredByUserId or CheckedByUserId equal to null");
            }

            var programmeIds = this.programmeCacheManager.ProgrammeIds;
            var previousRequestPackageData = this.requestPackagesRepository.GetPreviousRequestPackageData(userIds);

            foreach (var rpu in requestPackage.RequestPackageUsers)
            {
                if (requestPackage.Type == RequestPackageType.Request && rpu.Status != RequestStatus.Active && rpu.Status != RequestStatus.Rejected)
                {
                    throw new DomainValidationException("Cannot end a RequestPackage that has request/s with status different from 'Active' or 'Rejected'");
                }

                if (requestPackage.Type == RequestPackageType.DirectChange && rpu.Status == RequestStatus.Rejected)
                {
                    throw new DomainValidationException("Cannot have a Request that has status 'Rejected' when the RequestPackageType is 'DirectChange'");
                }

                if (rpu.Status == RequestStatus.Rejected)
                {
                    continue;
                }

                var user = users.Where(t => t.UserId == rpu.UserId).Single();
                if (rpu.RegDataRequest != null)
                {
                    if (this.usersRepository.OtherUserExists(rpu.UserId, rpu.RegDataRequest.Uin))
                    {
                        throw new DomainValidationException("User with the same Uin already exists");
                    }

                    user.UpdateUserRegData(
                        rpu.RegDataRequest.Uin,
                        rpu.RegDataRequest.Fullname,
                        rpu.RegDataRequest.Email,
                        rpu.RegDataRequest.Phone,
                        rpu.RegDataRequest.Address,
                        rpu.RegDataRequest.Position,
                        rpu.RegDataRequest.UserOrganizationId,
                        rpu.RegDataRequest.UserTypeId);
                }

                if (rpu.PermissionRequest != null)
                {
                    user.SetUserPermissions(rpu.PermissionRequest.GetPermissions(programmeIds));
                    user.SetUserPermissionTemplate(rpu.PermissionRequest.GetPermissionTemplate(programmeIds));
                }

                bool hasPermissionRequests = false;
                bool hasRegDataRequests = false;
                if (previousRequestPackageData.ContainsKey(rpu.UserId))
                {
                    (hasPermissionRequests, hasRegDataRequests) = previousRequestPackageData[rpu.UserId];
                }

                if ((hasRegDataRequests || rpu.RegDataRequest != null) &&
                    (hasPermissionRequests || rpu.PermissionRequest != null) &&
                    !user.IsActive)
                {
                    user.ActivateUser();
                }
                else if (rpu.RegDataRequest != null || rpu.PermissionRequest != null)
                {
                    ((IEventEmitter)requestPackage).Events.Add(new UserUpdatedEvent() { UserId = user.UserId });
                }

                if (requestPackage.Type == RequestPackageType.DirectChange)
                {
                    rpu.Status = RequestStatus.Active;
                }
            }

            if (requestPackage.Type == RequestPackageType.DirectChange)
            {
                requestPackage.CheckedByUserId = this.accessContext.UserId;
            }

            requestPackage.EndedMessage = endingMessage;
            requestPackage.Status = RequestPackageStatus.Ended;
            requestPackage.EndedByUserId = this.accessContext.UserId;
            requestPackage.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            // clear the cache for the updated request package
            this.cacheManager.ClearCache(ClaimsCaches.RequestPackage, requestPackageId);

            // clear the cache for each user in the package
            // as its permissions may have changed
            foreach (int userId in userIds)
            {
                this.cacheManager.ClearCache(ClaimsCaches.User, userId);
            }
        }

        public IList<string> CanRecoverUser(int userId)
        {
            var errors = new List<string>();
            User user = this.usersRepository.FindWithoutIncludes(userId);

            if (this.usersRepository.UserExists(user.Uin))
            {
                errors.Add("Съществува друг потребител със същия ЕГН");
            }

            return errors;
        }

        public void RecoverUser(int userId, byte[] version)
        {
            User user = this.usersRepository.FindForUpdate(userId, version);

            if (this.usersRepository.UserExists(user.Uin))
            {
                throw new DomainValidationException("User with the same Uin already exists");
            }

            user.IsDeleted = false;
            this.unitOfWork.Save();
        }
    }
}
