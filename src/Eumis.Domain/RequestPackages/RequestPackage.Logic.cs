using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.NotificationEvents;
using Eumis.Domain.Users.PermissionAggregations;

namespace Eumis.Domain.RequestPackages
{
    public partial class RequestPackage : IAggregateRoot
    {
        #region RequestPackage

        #region Private Methods

        private void SetAttributes(
            string packageDescription,
            Guid? blobKey1,
            string description1,
            Guid? blobKey2,
            string description2,
            Guid? blobKey3,
            string description3,
            Guid? blobKey4,
            string description4,
            Guid? blobKey5,
            string description5)
        {
            this.PackageDescription = packageDescription;

            this.BlobKey1 = blobKey1;
            this.Description1 = description1;
            this.BlobKey2 = blobKey2;
            this.Description2 = description2;
            this.BlobKey3 = blobKey3;
            this.Description3 = description3;
            this.BlobKey4 = blobKey4;
            this.Description4 = description4;
            this.BlobKey5 = blobKey5;
            this.Description5 = description5;
        }

        private void AssertIsDraftRequestPackage()
        {
            if (this.Status != RequestPackageStatus.Draft)
            {
                throw new DomainException("Cannot change RequestPackage which is not in 'Draft' status");
            }
        }

        #endregion //Private Methods

        public void UpdateRequestPackage(
            string packageDescription,
            Guid? blobKey1,
            string description1,
            Guid? blobKey2,
            string description2,
            Guid? blobKey3,
            string description3,
            Guid? blobKey4,
            string description4,
            Guid? blobKey5,
            string description5)
        {
            this.AssertIsDraftRequestPackage();

            this.SetAttributes(
                packageDescription,
                blobKey1,
                description1,
                blobKey2,
                description2,
                blobKey3,
                description3,
                blobKey4,
                description4,
                blobKey5,
                description5);
        }

        public void ChangeStatus(RequestPackageStatus requestPackageStatus, int currentUserId)
        {
            if (this.Status == requestPackageStatus)
            {
                throw new DomainValidationException("Cannot make a transition to the same RequestPackage status");
            }

            if (this.Status == RequestPackageStatus.Ended)
            {
                throw new DomainValidationException("RequestPackage status transition not allowed");
            }

            Action<RequestPackageStatus> validateStatus = (allowedStatus) =>
            {
                if (requestPackageStatus != allowedStatus)
                {
                    throw new DomainValidationException("RequestPackage status transition not allowed");
                }
            };

            Action<RequestStatus> changeRequestPackageUsersStatuses = (status) =>
            {
                foreach (var rpu in this.RequestPackageUsers)
                {
                    rpu.Status = status;
                }
            };

            if (requestPackageStatus != RequestPackageStatus.Draft && requestPackageStatus != RequestPackageStatus.Canceled)
            {
                switch (this.Status)
                {
                    case RequestPackageStatus.Draft:
                        validateStatus(RequestPackageStatus.Entered);
                        changeRequestPackageUsersStatuses(RequestStatus.Entered);
                        ((INotificationEventEmitter)this).NotificationEvents.Add(new ProgrammeIndependentEvent(NotificationEventType.RequestPackageStatusToEntered, this.RequestPackageId));
                        this.EnteredByUserId = currentUserId;
                        break;
                    case RequestPackageStatus.Entered:
                        validateStatus(RequestPackageStatus.Checked);
                        changeRequestPackageUsersStatuses(RequestStatus.Checked);
                        ((INotificationEventEmitter)this).NotificationEvents.Add(new ProgrammeIndependentEvent(NotificationEventType.RequestPackageStatusToChecked, this.RequestPackageId));
                        this.CheckedByUserId = currentUserId;
                        break;
                    default:
                        throw new DomainValidationException("RequestPackage status transition not allowed");
                }
            }
            else
            {
                if (requestPackageStatus == RequestPackageStatus.Draft)
                {
                    if (this.Status == RequestPackageStatus.Checked)
                    {
                        ((INotificationEventEmitter)this).NotificationEvents.Add(new ProgrammeIndependentEvent(NotificationEventType.RequestPackageStatusToDraft, this.RequestPackageId));
                    }

                    changeRequestPackageUsersStatuses(RequestStatus.Draft);
                    this.EnteredByUserId = null;
                    this.CheckedByUserId = null;
                }
                else if (requestPackageStatus == RequestPackageStatus.Canceled)
                {
                    changeRequestPackageUsersStatuses(RequestStatus.Canceled);
                }
                else
                {
                    throw new DomainValidationException("RequestPackage status transition not allowed");
                }
            }

            this.Status = requestPackageStatus;
            this.ModifyDate = DateTime.Now;
        }

        public IList<string> CanEnter(int[] porgrammeIds, IDictionary<int, (string fullname, int userTypeId)> usersInfo, IDictionary<int, (string userTypeName, string permissionTemplateString)> userTypesInfo)
        {
            IList<string> errors = new List<string>();
            IList<string> usersWithoutPermissionRequest = new List<string>();
            IList<string> usersWithWrongPermissionRequest = new List<string>();

            if (this.RequestPackageUsers.Count == 0)
            {
                errors.Add("Трябва да има поне един потребител, свързан с пакета");
            }

            if (this.RequestPackageUsers != null && this.RequestPackageUsers.Any(t => t.RegDataRequest == null && t.PermissionRequest == null))
            {
                errors.Add("Всички потребители към пакета трябва да имат заявка");
            }

            foreach (var user in this.RequestPackageUsers)
            {
                (string fullname, int userTypeId) = usersInfo[user.UserId];

                bool userTypeIsChanged = user.RegDataRequest != null && user.RegDataRequest.UserTypeId != userTypeId;

                (string userTypeName, string permissionTemplateString) = userTypesInfo[userTypeIsChanged ? user.RegDataRequest.UserTypeId : userTypeId];

                if (userTypeIsChanged && user.PermissionRequest == null)
                {
                    usersWithoutPermissionRequest.Add(fullname);
                }

                if (user.PermissionRequest != null)
                {
                    var userTypeIdPA = new PermissionAggregation(porgrammeIds, permissionTemplateString);
                    var permissionRequestPA = new PermissionAggregation(porgrammeIds, user.PermissionRequest.PermissionTemplateString);

                    if (!userTypeIdPA.Equals(permissionRequestPA))
                    {
                        usersWithWrongPermissionRequest.Add(fullname);
                    }
                }
            }

            if (usersWithoutPermissionRequest.Count > 0)
            {
                if (usersWithoutPermissionRequest.Count == 1)
                {
                    errors.Add($"Потребителят {usersWithoutPermissionRequest.First()} е със сменена група, но няма въведена заявка за права");
                }
                else
                {
                    errors.Add($"Потребителите {string.Join(", ", usersWithoutPermissionRequest.ToArray())} са със сменена група, но нямат въведена заявка за права");
                }
            }

            if (usersWithWrongPermissionRequest.Count > 0)
            {
                if (usersWithWrongPermissionRequest.Count == 1)
                {
                    errors.Add($"Заявката за права на потребителят {usersWithWrongPermissionRequest.First()} е с шаблон различнен от указаната група в заявката за регистрационни данни");
                }
                else
                {
                    errors.Add($"Заявката за права на потребителите {string.Join(", ", usersWithWrongPermissionRequest.ToArray())} е с шаблон различнен от указаната група в заявката за регистрационни данни");
                }
            }

            return errors;
        }

        public IList<string> CanEnd()
        {
            IList<string> errors = new List<string>();

            if (this.RequestPackageUsers != null)
            {
                if (this.Type == RequestPackageType.Request &&
                    this.RequestPackageUsers.Where(t => t.Status != RequestStatus.Active && t.Status != RequestStatus.Rejected).Any())
                {
                    errors.Add("Не можете да приключите пакета, защото има заявка/и към него със статус различен от \'Активна\' и \'Отхвърлена\'");
                }
            }

            return errors;
        }

        #endregion //RequestPackage

        #region RequestPackageUsers

        public RequestPackageUser FindRequestPackageUser(int userId)
        {
            var requestPackageUser = this.RequestPackageUsers.Where(e => e.UserId == userId).SingleOrDefault();

            if (requestPackageUser == null)
            {
                throw new DomainObjectNotFoundException("Cannot find RequestPackageUser with id " + userId);
            }

            return requestPackageUser;
        }

        public void AddRequestPackageUser(int userId, int userOrganizationId)
        {
            this.AssertIsDraftRequestPackage();

            if (this.Type == RequestPackageType.Request && this.UserOrganizationId != userOrganizationId)
            {
                throw new DomainObjectNotFoundException("Cannot add a User to a RequestPackage with userOrganization different from the package's userOrganization");
            }

            RequestPackageUser requestPackageUser = new RequestPackageUser()
            {
                UserId = userId,
                Status = RequestStatus.Draft,
            };

            this.RequestPackageUsers.Add(requestPackageUser);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveRequestPackageUser(int userId)
        {
            this.AssertIsDraftRequestPackage();

            var requestPackageUser = this.FindRequestPackageUser(userId);

            if (requestPackageUser.RegDataRequest != null || requestPackageUser.PermissionRequest != null)
            {
                throw new DomainObjectNotFoundException("Cannot remove a RequestPackageUser with requests from a RequestPackage");
            }

            this.RequestPackageUsers.Remove(requestPackageUser);

            this.ModifyDate = DateTime.Now;
        }

        public void ChangeRequestPackageUserStatus(int userId, RequestStatus requestStatus, string rejectionMessage)
        {
            var requestPackageUser = this.FindRequestPackageUser(userId);

            if (requestPackageUser.Status == requestStatus)
            {
                throw new DomainValidationException("Cannot make a transition to the same RequestPackageUser status");
            }

            if (this.Status != RequestPackageStatus.Checked)
            {
                throw new DomainValidationException("RequestPackage must be in 'Checked' status to make a RequestPackageUser status transition");
            }

            if (requestStatus == RequestStatus.Rejected)
            {
                requestPackageUser.RejectionMessage = rejectionMessage;
            }
            else if (requestStatus == RequestStatus.Checked)
            {
                requestPackageUser.RejectionMessage = null;
            }

            requestPackageUser.Status = requestStatus;

            this.ModifyDate = DateTime.Now;
        }

        #endregion //RequestPackageUsers

        #region RequestPackageUserRegDataRequest

        public RegDataRequest FindRequestPackageUserRegDataRequest(int userId)
        {
            var regDataRequest = this.FindRequestPackageUser(userId).RegDataRequest;

            if (regDataRequest == null)
            {
                throw new DomainObjectNotFoundException("Cannot find RegDataRequest from RequestPackageUser with requestPackageId = " + this.RequestPackageId + " and userId = " + userId);
            }

            return regDataRequest;
        }

        public void AddRequestPackageUserRegDataRequest(
            int userId,
            string uin,
            string fullname,
            string email,
            string phone,
            string address,
            string position,
            int userOrganizationId,
            int userTypeId)
        {
            this.AssertIsDraftRequestPackage();

            RegDataRequest regDataRequest = new RegDataRequest(
                this.RequestPackageId,
                userId,
                uin,
                fullname,
                email,
                phone,
                address,
                position,
                userOrganizationId,
                userTypeId);

            this.FindRequestPackageUser(userId).RegDataRequest = regDataRequest;

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateRequestPackageUserRegDataRequest(
            int userId,
            string uin,
            string fullname,
            string email,
            string phone,
            string address,
            string position,
            int userOrganizationId,
            int userTypeId)
        {
            this.AssertIsDraftRequestPackage();

            var regDataRequest = this.FindRequestPackageUserRegDataRequest(userId);

            regDataRequest.SetAttributes(
                uin,
                fullname,
                email,
                phone,
                address,
                position,
                userOrganizationId,
                userTypeId);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveRequestPackageUserRegDataRequest(int userId)
        {
            this.AssertIsDraftRequestPackage();

            var regDataRequest = this.FindRequestPackageUserRegDataRequest(userId);

            this.FindRequestPackageUser(userId).RegDataRequest = null;

            this.ModifyDate = DateTime.Now;
        }

        #endregion //RequestPackageUserRegDataRequest

        #region RequestPackageUserPermissionRequest

        public PermissionRequest FindRequestPackageUserPermissionRequest(int userId)
        {
            var permissionRequest = this.FindRequestPackageUser(userId).PermissionRequest;

            if (permissionRequest == null)
            {
                throw new DomainObjectNotFoundException("Cannot find PermissionRequest from RequestPackageUser with requestPackageId = " + this.RequestPackageId + " and userId = " + userId);
            }

            return permissionRequest;
        }

        public void AddRequestPackageUserPermissionRequest(
            int userId,
            PermissionAggregation permissions,
            PermissionAggregation permissionTemplate)
        {
            this.AssertIsDraftRequestPackage();

            PermissionRequest permissionRequest = new PermissionRequest()
            {
                RequestPackageId = this.RequestPackageId,
                UserId = userId,
            };

            permissionRequest.SetPermissions(permissions);
            permissionRequest.SetPermissionTemplate(permissionTemplate);

            this.FindRequestPackageUser(userId).PermissionRequest = permissionRequest;

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateRequestPackageUserPermissionRequest(
            int userId,
            PermissionAggregation permissions)
        {
            this.AssertIsDraftRequestPackage();

            var permissionRequest = this.FindRequestPackageUserPermissionRequest(userId);

            permissionRequest.SetPermissions(permissions);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveRequestPackageUserPermissionRequest(int userId)
        {
            this.AssertIsDraftRequestPackage();

            var permissionRequest = this.FindRequestPackageUserPermissionRequest(userId);

            this.FindRequestPackageUser(userId).PermissionRequest = null;

            this.ModifyDate = DateTime.Now;
        }

        #endregion //RequestPackageUserPermissionRequest
    }
}
