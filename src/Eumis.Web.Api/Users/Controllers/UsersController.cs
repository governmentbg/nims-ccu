using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Eumis.ApplicationServices.Services.RequestPackage;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.PermissionTemplates.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Data.Users.ViewObjects;
using Eumis.Data.UserTypes.Repositories;
using Eumis.Domain.Users;
using Eumis.Domain.Users.CommonPermissions;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Log.Owin;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Users.DataObjects;

namespace Eumis.Web.Api.Users.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IUsersRepository usersRepository;
        private IProgrammesRepository programmesRepository;
        private IPermissionTemplatesRepository permissionTemplatesRepository;
        private IUserTypesRepository userTypesRepository;
        private IPermissionsRepository permissionsRepository;
        private IContractsRepository contractsRepository;

        private IProgrammeCacheManager programmeCacheManager;
        private IUserClaimsContext currentUserClaimsContext;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IActionLogger actionLogger;
        private IRequestPackageService requestPackageService;

        public UsersController(
            IUnitOfWork unitOfWork,
            IUsersRepository usersRepository,
            IProgrammesRepository programmesRepository,
            IPermissionTemplatesRepository permissionTemplatesRepository,
            IUserTypesRepository userTypesRepository,
            IPermissionsRepository permissionsRepository,
            IContractsRepository contractsRepository,
            IProgrammeCacheManager programmeCacheManager,
            UserClaimsContextFactory userClaimsContextFactory,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IActionLogger actionLogger,
            IRequestPackageService requestPackageService)
        {
            this.unitOfWork = unitOfWork;
            this.usersRepository = usersRepository;
            this.programmesRepository = programmesRepository;
            this.permissionTemplatesRepository = permissionTemplatesRepository;
            this.userTypesRepository = userTypesRepository;
            this.permissionsRepository = permissionsRepository;
            this.contractsRepository = contractsRepository;

            if (accessContext.IsUser)
            {
                this.currentUserClaimsContext = userClaimsContextFactory(accessContext.UserId);
            }

            this.programmeCacheManager = programmeCacheManager;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.actionLogger = actionLogger;
            this.requestPackageService = requestPackageService;
        }

        [Route("")]
        public IList<UserVO> GetUsers(
            string username = null,
            string fullname = null,
            int? userOrganizationId = null,
            bool? active = null,
            bool? deleted = null,
            bool? locked = null,
            bool? hasAcceptedGDPRDeclaration = null,
            bool exact = false)
        {
            this.authorizer.AssertCanDo(UserListActions.Search);

            if (this.currentUserClaimsContext.IsSuperUser)
            {
                return this.usersRepository.GetUsers(username, fullname, userOrganizationId, active, deleted, locked, hasAcceptedGDPRDeclaration, exact);
            }
            else
            {
                // only super users can view/search users of other UserTypes
                return this.usersRepository.GetUsers(username, fullname, this.currentUserClaimsContext.UserOrganizationId, active, deleted, locked, hasAcceptedGDPRDeclaration, exact);
            }
        }

        [Route("{userId:int}")]
        public UserDO GetUser(int userId)
        {
            this.authorizer.AssertCanDo(UserActions.View, userId);

            var user = this.usersRepository.Find(userId);

            return new UserDO(user);
        }

        [Route("userInfo")]
        public UserInfoDO GetUserInfo(int userId)
        {
            this.authorizer.AssertCanDo(UserActions.View, userId);

            var user = this.usersRepository.Find(userId);

            return new UserInfoDO(user);
        }

        [HttpGet]
        [Route("isSuperUser")]
        public object IsSuperUser()
        {
            return this.currentUserClaimsContext.IsSuperUser;
        }

        [Route("new")]
        public UserDO GetNewUser(int? userOrganizationId = null)
        {
            this.authorizer.AssertCanDo(UserListActions.ViewCreate);

            return new UserDO()
            {
                UserOrganizationId = userOrganizationId,
            };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Users.Create))]
        public UserDO CreateUser(UserDO user)
        {
            this.authorizer.AssertCanDo(UserOrganizationActions.CreateUser, user.UserOrganizationId.Value);

            int userTypeUserOrganizationId = this.userTypesRepository.GetUserOrganizationId(user.UserTypeId.Value);

            // make sure we are not being fooled to use a userType from a different organization
            if (user.UserOrganizationId != userTypeUserOrganizationId)
            {
                throw new Exception("UserOrganizationId mismatch");
            }

            var programmeIds = this.programmeCacheManager.ProgrammeIds;
            var permissionTemplate = this.permissionTemplatesRepository.FindByUserType(user.UserTypeId.Value);

            User newUser = new User(
                user.Username,
                user.Uin,
                user.UserTypeId.Value,
                user.UserOrganizationId.Value,
                permissionTemplate.GetPermissions(programmeIds),
                user.Fullname,
                user.Email,
                user.Phone,
                user.Address,
                user.Position,
                false,
                false,
                false);

            this.usersRepository.Add(newUser);

            this.unitOfWork.Save();

            return new UserDO(newUser);
        }

        [HttpGet]
        [Route("isUniqueUin")]
        public object IsUniqueUin(string uin, int? userId = null)
        {
            this.authorizer.AssertCanDo(UserListActions.Search);

            bool isUnique;
            if (userId.HasValue)
            {
                isUnique = !this.usersRepository.OtherUserExists(userId.Value, uin);
            }
            else
            {
                isUnique = !this.usersRepository.UserExists(uin);
            }

            return new { isUnique };
        }

        [HttpGet]
        [Route("isUniqueUsername")]
        public object IsUniqueUsername(string username, int? userId = null)
        {
            this.authorizer.AssertCanDo(UserListActions.Search);

            var isUnique = this.usersRepository.IsUniqueUsername(username, userId);

            return new { isUnique = isUnique };
        }

        [HttpPut]
        [Route("{userId:int}/deleteUser")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Users.Delete), IdParam = "userId")]
        public void DeleteUser(int userId, string version)
        {
            this.authorizer.AssertCanDo(UserActions.SetIsDeleted, userId);

            byte[] vers = System.Convert.FromBase64String(version);
            User oldUser = this.usersRepository.FindForUpdate(userId, vers);

            oldUser.SetIsDeleted();

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{userId:int}/recover")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Users.Recover), IdParam = "userId")]
        public void RecoverUser(int userId, string version)
        {
            this.authorizer.AssertCanDo(UserActions.SetIsDeleted, userId);

            byte[] vers = System.Convert.FromBase64String(version);
            this.requestPackageService.RecoverUser(userId, vers);
        }

        [HttpPost]
        [Route("{userId:int}/canRecover")]
        public ErrorsDO CanRecoverUser(int userId)
        {
            var errors = this.requestPackageService.CanRecoverUser(userId);
            return new ErrorsDO(errors);
        }

        [HttpPut]
        [Route("{userId:int}/lock")]
        [ActionLog(Action = typeof(ActionLogGroups.Users.Lock), IdParam = "userId")]
        public void LockUser(int userId, string version)
        {
            this.authorizer.AssertCanDo(UserActions.SetIsLocked, userId);

            byte[] vers = System.Convert.FromBase64String(version);
            User oldUser = this.usersRepository.FindForUpdate(userId, vers);

            oldUser.SetIsLocked(true);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{userId:int}/unlock")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Users.Unlock), IdParam = "userId")]
        public void UnlockUser(int userId, string version)
        {
            this.authorizer.AssertCanDo(UserActions.SetIsLocked, userId);

            byte[] vers = System.Convert.FromBase64String(version);
            User oldUser = this.usersRepository.FindForUpdate(userId, vers);

            oldUser.SetIsLocked(false);

            this.unitOfWork.Save();
        }

        #region Current user methods

        [Route("current")]
        public UserDO GetUserData()
        {
            var user = this.usersRepository.Find(this.accessContext.UserId);
            var programmeIds = this.programmeCacheManager.ProgrammeIds;

            bool isOperationalMapVisible = false;
            bool areIndicatorsVisible = false;
            bool areProceduresVisible = false;
            bool areProjectsVisible = false;
            bool areProjectCommunicationsVisible = false;
            bool areEvalSessionsVisible = false;
            bool areContractsVisible = false;
            bool areContractCommunicationsVisible = false;
            bool areContractReportsVisible = false;
            bool isMonitoringFinancialControlModuleVisible = false;
            bool areMonitoringFinancialControlItemsVisible = false;
            bool areEuReimbursedAmountsVisible = false;
            bool isProjectDossierVisible = false;

            Func<int, Enum, bool> hasProgrammePermission =
                (pId, p) =>
                    user.UserPermissions.OfType<ProgrammePermission>()
                        .Where(up =>
                            up.ProgrammeId == pId &&
                            up.PermissionString == p.ToString() &&
                            up.GetType() == UserPermission.GetPermissionEntityType(p.GetType()))
                        .Any();

            Func<Enum, bool> hasPermission =
                (p) =>
                    user.UserPermissions.OfType<UserPermission>()
                        .Where(up =>
                            up.PermissionString == p.ToString() &&
                            up.GetType() == UserPermission.GetPermissionEntityType(p.GetType()))
                        .Any();

            var areNewsVisible = hasPermission(NewsPermissions.CanPublish);
            var areGuidancesVisible = hasPermission(GuidancePermissions.CanCreate);
            var isMonitoringVisible = hasPermission(MonitoringPermissions.CanRead);
            var areSapInterfacesVisible = hasPermission(SapInterfacePermissions.CanImport);
            var areInterfacesVisible = hasPermission(InterfacesPermissions.CanExport);
            var areRegistrationsVisible = hasPermission(RegistrationPermissions.CanRead);
            var areContractRegistrationsVisible = hasPermission(ContractRegistrationPermissions.CanRead);
            var areContractAccessCodesVisible = hasPermission(ContractRegistrationPermissions.CanRead);

            foreach (var programmeId in programmeIds)
            {
                if (!isOperationalMapVisible && hasProgrammePermission(programmeId, OperationalMapPermissions.CanRead))
                {
                    isOperationalMapVisible = true;
                }

                if (!areIndicatorsVisible && hasProgrammePermission(programmeId, IndicatorPermissions.CanRead) && !Eumis.Domain.Procedures.Procedure.HideIndicators)
                {
                    areIndicatorsVisible = true;
                }

                if (!areProceduresVisible && hasProgrammePermission(programmeId, ProcedurePermissions.CanRead))
                {
                    areProceduresVisible = true;
                }

                if (!areProjectsVisible && hasProgrammePermission(programmeId, ProjectPermissions.CanRead))
                {
                    areProjectsVisible = true;
                }

                if (!areProjectCommunicationsVisible && hasProgrammePermission(programmeId, ContractPermissions.CanRead))
                {
                    areProjectCommunicationsVisible = true;
                }

                if (!areEvalSessionsVisible &&
                    (hasProgrammePermission(programmeId, EvalSessionPermissions.CanEvaluate) ||
                    hasProgrammePermission(programmeId, EvalSessionPermissions.CanAdministrate) ||
                    hasProgrammePermission(programmeId, EvalSessionPermissions.CanRead)))
                {
                    areEvalSessionsVisible = true;
                }

                if (!areContractsVisible && (hasProgrammePermission(programmeId, ContractPermissions.CanRead)
                    || this.contractsRepository.IsUserAssociatedWithAnyContract(this.accessContext.UserId)))
                {
                    areContractsVisible = true;
                }

                if (!areContractCommunicationsVisible && hasProgrammePermission(programmeId, ContractCommunicationPermissions.CanRead))
                {
                    areContractCommunicationsVisible = true;
                }

                if (!areContractReportsVisible && hasProgrammePermission(programmeId, ContractReportPermissions.CanRead))
                {
                    areContractReportsVisible = true;
                }

                if (!isMonitoringFinancialControlModuleVisible && (hasProgrammePermission(programmeId, MonitoringFinancialControlPermissions.CanRead)
                    || this.contractsRepository.IsUserAssociatedWithAnyContract(this.accessContext.UserId)))
                {
                    isMonitoringFinancialControlModuleVisible = true;
                    areMonitoringFinancialControlItemsVisible = hasProgrammePermission(programmeId, MonitoringFinancialControlPermissions.CanRead);
                }

                if (!isProjectDossierVisible && hasProgrammePermission(programmeId, ProjectDossierPermissions.CanRead))
                {
                    isProjectDossierVisible = true;
                }
            }

            var visibility = new UserVisibilityDO
            {
                IsOperationalMapVisible = isOperationalMapVisible,
                AreIndicatorsVisible = areIndicatorsVisible,
                AreProceduresVisible = areProceduresVisible,
                IsProjectsModuleVisible = areProjectsVisible || areProjectCommunicationsVisible,
                AreProjectsVisible = areProjectsVisible,
                AreProjectCommunicationsVisible = areProjectCommunicationsVisible,
                AreCompaniesVisible = hasPermission(CompanyPermissions.CanRead),
                AreNewsVisible = areNewsVisible,
                AreGuidancesVisible = areGuidancesVisible,
                AreRegistratiosVisible = areRegistrationsVisible,
                AreContractRegistratiosVisible = areContractRegistrationsVisible, // TODO: da se napravqt novi permissions
                AreContractAccessCodesVisible = areContractAccessCodesVisible,
                IsProfilesModuleVisible = areRegistrationsVisible || areContractRegistrationsVisible || areContractAccessCodesVisible,
                AreUsersVisible = hasPermission(UserAdminPermissions.CanAdministrate) ||
                                    hasPermission(UserAdminPermissions.CanControl),
                AreUserProfilesVisible = hasPermission(UserAdminPermissions.CanAdministrate) ||
                                            hasPermission(UserAdminPermissions.CanControl),
                ArePTemplatesVisible = this.currentUserClaimsContext.IsSuperUser,
                AreUserTypesVisible = this.currentUserClaimsContext.IsSuperUser,
                AreRequestPackagesVisible = hasPermission(UserAdminPermissions.CanAdministrate) ||
                                                hasPermission(UserAdminPermissions.CanControl),
                AreUserOrganizationsVisible = this.currentUserClaimsContext.IsSuperUser,
                IsChangePasswordVisible = true,
                AreMessagesVisible = true,
                AreEvalSessionsVisible = areEvalSessionsVisible,
                IsContractModuleVisible = areContractsVisible || areContractCommunicationsVisible || areContractReportsVisible,
                AreContractsVisible = areContractsVisible,
                AreContractCommunicationsVisible = areContractCommunicationsVisible,
                AreContractReportsVisible = areContractReportsVisible,
                IsMonitoringFinancialControlModuleVisible = isMonitoringFinancialControlModuleVisible,
                AreEuReimbursedAmountsVisible = areEuReimbursedAmountsVisible,
                IsMonitoringVisible = isMonitoringVisible,
                IsRegixInterfaceVisible = this.currentUserClaimsContext.IsSuperUser,
                AreAllInterfacesVisible = areInterfacesVisible || areSapInterfacesVisible,
                AreInterfacesVisible = areInterfacesVisible,
                AreSapInterfacesVisible = areSapInterfacesVisible,
                IsProjectDossierVisible = isProjectDossierVisible,
                IsHelpSupportVisible = true,
                IsActionLogVisible = hasPermission(ActionLogPermissions.CanRead),
                AreDeclarationsVisible = hasPermission(NewsPermissions.CanPublish),
                AreMonitoringFinancialControlItemsVisible = areMonitoringFinancialControlItemsVisible,
                AreComunicationsVisible = areNewsVisible || areGuidancesVisible,
            };

            return new UserDO(user, visibility);
        }

        [HttpPut]
        [Route("changePassword")]
        public void ChangeCurrentUserPassword(PasswordsDO passwords)
        {
            this.Request.GetOwinContext().SetContainsSensitiveData();

            this.actionLogger.LogAction(typeof(ActionLogGroups.Users.ChangePassword), null, null, null, null);

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var user = this.usersRepository.Find(this.accessContext.UserId);

                user.ChangePassword(passwords.OldPassword, passwords.NewPassword);

                this.unitOfWork.Save();

                transaction.Commit();
            }
        }

        [HttpPost]
        [Route("isCorrectPassword")]
        public bool IsCorrectPassword([FromBody] string password)
        {
            this.Request.GetOwinContext().SetContainsSensitiveData();

            bool isCorrect;
            if (password == null)
            {
                isCorrect = false;
            }
            else
            {
                isCorrect = this.usersRepository.Find(this.accessContext.UserId).VerifyPassword(password);
            }

            return isCorrect;
        }

        #endregion // Current user methods
    }
}
