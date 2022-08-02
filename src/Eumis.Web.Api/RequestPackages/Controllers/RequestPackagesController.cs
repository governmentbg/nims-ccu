using System;
using System.Collections.Generic;
using System.Web.Http;
using Eumis.ApplicationServices.Services.RequestPackage;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Counters;
using Eumis.Data.RequestPackages.ViewObjects;
using Eumis.Data.Users.Repositories;
using Eumis.Data.UserTypes.Repositories;
using Eumis.Domain.RequestPackages;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.RequestPackages.DataObjects;

namespace Eumis.Web.Api.RequestPackages.Controllers
{
    [RoutePrefix("api/requestPackages")]
    public class RequestPackagesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IRequestPackagesRepository requestPackagesRepository;
        private IUsersRepository usersRepository;
        private ICountersRepository countersRepository;
        private IUserClaimsContext currentUserClaimsContext;
        private IUserTypesRepository userTypesRepository;
        private IAuthorizer authorizer;
        private ICacheManager cacheManager;
        private IAccessContext accessContext;
        private IProgrammeCacheManager programmeCacheManager;
        private IRequestPackageService requestPackageService;

        public RequestPackagesController(
            IUnitOfWork unitOfWork,
            IRequestPackagesRepository requestPackagesRepository,
            IUsersRepository usersRepository,
            ICountersRepository countersRepository,
            UserClaimsContextFactory userClaimsContextFactory,
            IUserTypesRepository userTypesRepository,
            IAuthorizer authorizer,
            ICacheManager cacheManager,
            IAccessContext accessContext,
            IProgrammeCacheManager programmeCacheManager,
            IRequestPackageService requestPackageService)
        {
            this.unitOfWork = unitOfWork;
            this.requestPackagesRepository = requestPackagesRepository;
            this.usersRepository = usersRepository;
            this.userTypesRepository = userTypesRepository;
            this.countersRepository = countersRepository;

            if (accessContext.IsUser)
            {
                this.currentUserClaimsContext = userClaimsContextFactory(accessContext.UserId);
            }

            this.authorizer = authorizer;
            this.cacheManager = cacheManager;
            this.accessContext = accessContext;
            this.programmeCacheManager = programmeCacheManager;
            this.requestPackageService = requestPackageService;
        }

        [Route("")]
        public IList<RequestPackageVO> GetRequestPackages(
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            RequestPackageType? typeId = null,
            int? userOrganizationId = null,
            RequestPackageStatus? statusId = null)
        {
            this.authorizer.AssertCanDo(RequestPackageListActions.Search);

            var organizationId = this.currentUserClaimsContext.IsSuperUser || this.authorizer.CanDo(RequestPackageListActions.CanControl) ? userOrganizationId : this.currentUserClaimsContext.UserOrganizationId;

            return this.requestPackagesRepository.GetRequestPackages(
                dateFrom: dateFrom,
                dateTo: dateTo,
                typeId: typeId,
                //// only super users can view/search request packages of other UserOrganizations
                userOrganizationId: organizationId,
                statusId: statusId);
        }

        [Route("{requestPackageId:int}")]
        public RequestPackageDO GetRequestPackage(int requestPackageId)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.View, requestPackageId);

            var requestPackage = this.requestPackagesRepository.Find(requestPackageId);

            return new RequestPackageDO(requestPackage);
        }

        [Route("new")]
        public RequestPackageDO GetNewRequestPackage(bool? isDirect = false)
        {
            if (isDirect.Value)
            {
                this.authorizer.AssertCanDo(RequestPackageListActions.CreateDirect);
            }
            else
            {
                this.authorizer.AssertCanDo(RequestPackageListActions.Create);
            }

            return new RequestPackageDO
            {
                Status = RequestPackageStatus.Draft,
                Type = isDirect.Value ? RequestPackageType.DirectChange : RequestPackageType.Request,
            };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.RequestPackages.Create))]
        public RequestPackageDO CreateRequestPackage(RequestPackageDO requestPackage)
        {
            if (requestPackage.Type == RequestPackageType.DirectChange)
            {
                this.authorizer.AssertCanDo(RequestPackageListActions.CreateDirect);
            }
            else
            {
                this.authorizer.AssertCanDo(UserOrganizationActions.CreateRequestPackage, requestPackage.UserOrganizationId.Value);
            }

            this.countersRepository.CreateRequestPackageCounter();

            var code = this.countersRepository.GetNextRequestPackageNumber();

            RequestPackage newRequestPackage = new RequestPackage(
                requestPackage.Type.Value,
                requestPackage.UserOrganizationId,
                requestPackage.PackageDescription,
                code,
                requestPackage.File1 != null ? requestPackage.File1.Key : (Guid?)null,
                requestPackage.Description1,
                requestPackage.File2 != null ? requestPackage.File2.Key : (Guid?)null,
                requestPackage.Description2,
                requestPackage.File3 != null ? requestPackage.File3.Key : (Guid?)null,
                requestPackage.Description3,
                requestPackage.File4 != null ? requestPackage.File4.Key : (Guid?)null,
                requestPackage.Description4,
                requestPackage.File5 != null ? requestPackage.File5.Key : (Guid?)null,
                requestPackage.Description5);

            this.requestPackagesRepository.Add(newRequestPackage);

            this.unitOfWork.Save();

            return new RequestPackageDO(newRequestPackage);
        }

        [HttpPut]
        [Route("{requestPackageId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.RequestPackages.Edit.BasicData), IdParam = "requestPackageId")]
        public void UpdateRequestPackage(int requestPackageId, RequestPackageDO requestPackage)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.Edit, requestPackageId);

            RequestPackage oldRequestPackage = this.requestPackagesRepository.FindForUpdate(requestPackageId, requestPackage.Version);

            oldRequestPackage.UpdateRequestPackage(
                requestPackage.PackageDescription,
                requestPackage.File1 != null ? requestPackage.File1.Key : (Guid?)null,
                requestPackage.Description1,
                requestPackage.File2 != null ? requestPackage.File2.Key : (Guid?)null,
                requestPackage.Description2,
                requestPackage.File3 != null ? requestPackage.File3.Key : (Guid?)null,
                requestPackage.Description3,
                requestPackage.File4 != null ? requestPackage.File4.Key : (Guid?)null,
                requestPackage.Description4,
                requestPackage.File5 != null ? requestPackage.File5.Key : (Guid?)null,
                requestPackage.Description5);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Transaction]
        [Route("{requestPackageId:int}/changeStatusToDraft")]
        [ActionLog(Action = typeof(ActionLogGroups.RequestPackages.ChangeStatusToDraft), IdParam = "requestPackageId")]
        public void ChangeStatusToDraft(int requestPackageId, string version)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.SetDraft, requestPackageId);

            this.ChangeStatus(requestPackageId, version, RequestPackageStatus.Draft);
        }

        [HttpPut]
        [Transaction]
        [Route("{requestPackageId:int}/changeStatusToEntered")]
        [ActionLog(Action = typeof(ActionLogGroups.RequestPackages.ChangeStatusToEntered), IdParam = "requestPackageId")]
        public void ChangeStatusToEntered(int requestPackageId, string version)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.SetEntered, requestPackageId);

            this.ChangeStatus(requestPackageId, version, RequestPackageStatus.Entered);
        }

        [HttpPut]
        [Transaction]
        [Route("{requestPackageId:int}/changeStatusToChecked")]
        [ActionLog(Action = typeof(ActionLogGroups.RequestPackages.ChangeStatusToChecked), IdParam = "requestPackageId")]
        public void ChangeStatusToChecked(int requestPackageId, string version)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.SetChecked, requestPackageId);

            this.ChangeStatus(requestPackageId, version, RequestPackageStatus.Checked);
        }

        [HttpPut]
        [Route("{requestPackageId:int}/changeStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.RequestPackages.ChangeStatusToEnded), IdParam = "requestPackageId")]
        public void ChangeStatusToEnded(int requestPackageId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.SetEnded, requestPackageId);

            byte[] vers = System.Convert.FromBase64String(version);
            this.requestPackageService.EndRequestPackage(requestPackageId, vers, confirm.Note);
        }

        [HttpPut]
        [Transaction]
        [Route("{requestPackageId:int}/changeStatusToCanceled")]
        [ActionLog(Action = typeof(ActionLogGroups.RequestPackages.ChangeStatusToCanceled), IdParam = "requestPackageId")]
        public void ChangeStatusToCanceled(int requestPackageId, string version)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.SetCanceled, requestPackageId);

            this.ChangeStatus(requestPackageId, version, RequestPackageStatus.Canceled);
        }

        private void ChangeStatus(int requestPackageId, string version, RequestPackageStatus requestPackageStatus)
        {
            byte[] vers = System.Convert.FromBase64String(version);
            RequestPackage oldRequestPackage = this.requestPackagesRepository.FindForUpdate(requestPackageId, vers);

            oldRequestPackage.ChangeStatus(requestPackageStatus, this.accessContext.UserId);
            this.unitOfWork.Save();

            // clear the cache for the updated request package
            this.cacheManager.ClearCache(ClaimsCaches.RequestPackage, requestPackageId);
        }

        [HttpPost]
        [Route("canChangeStatusToEntered")]
        [Route("{requestPackageId:int}/canChangeStatusToEntered")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public ErrorsDO CanChangeStatusToEntered(int requestPackageId, string version)
        {
            RequestPackage oldRequestPackage = this.requestPackagesRepository.Find(requestPackageId);

            var programmeIds = this.programmeCacheManager.ProgrammeIds;
            var usersInfo = this.usersRepository.GetAllUserInfo(requestPackageId);
            var userTypesInfo = this.userTypesRepository.GetUserTypePermissions();

            var errors = oldRequestPackage.CanEnter(programmeIds, usersInfo, userTypesInfo);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{requestPackageId:int}/canChangeStatusToEnded")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public ErrorsDO CanChangeStatusToEnded(int requestPackageId, string version)
        {
            var errors = this.requestPackageService.CanEndRequestPackage(requestPackageId);

            return new ErrorsDO(errors);
        }

        [Route("{requestPackageId:int}/info")]
        public RequestPackageInfoVO GetRequestPackageInfo(int requestPackageId)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.View, requestPackageId);

            return this.requestPackagesRepository.GetRequestPackageInfo(requestPackageId);
        }
    }
}
