using System.Collections.Generic;
using System.Linq;
using Autofac.Features.AttributeFilters;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Users;

namespace Eumis.Authentication.Authorization.ClaimsContexts.User
{
    internal class UserClaimsContext : ClaimsContext, IUserClaimsContextInternal, IUserClaimsContext
    {
        private int userId;

        private IClaimsCache claimsCache;
        private IPermissionsRepository permissionsRepository;
        private IUsersRepository usersRepository;
        private IRequestPackagesRepository requestPackagesRepository;
        private IEvalSessionsRepository evalSessionsRepository;
        private IProgrammeCacheManager programmeCacheManager;
        private IContractsRepository contractsRepository;

        public UserClaimsContext(
            int userId,
            [KeyFilter(ClaimsCaches.User)]IClaimsCache claimsCache,
            IPermissionsRepository permissionsRepository,
            IUsersRepository usersRepository,
            IRequestPackagesRepository requestPackagesRepository,
            IEvalSessionsRepository evalSessionsRepository,
            IContractsRepository contractsRepository,
            IProgrammeCacheManager programmeCacheManager)
            : base(claimsCache)
        {
            this.userId = userId;
            this.claimsCache = claimsCache;
            this.permissionsRepository = permissionsRepository;
            this.usersRepository = usersRepository;
            this.requestPackagesRepository = requestPackagesRepository;
            this.evalSessionsRepository = evalSessionsRepository;
            this.contractsRepository = contractsRepository;
            this.programmeCacheManager = programmeCacheManager;
        }

        public bool IsSuperUser
        {
            get
            {
                bool isSuperUser = this.GetClaim(
                    this.userId,
                    new ClaimKey("IsSuperUser"),
                    () => this.permissionsRepository.UserIsSuperUser(this.userId));

                return isSuperUser;
            }
        }

        public int UserOrganizationId
        {
            get
            {
                return this.GetClaim(
                    this.userId,
                    new ClaimKey("UserOrganizationId"),
                    () => this.usersRepository.GetUserOrganizationId(this.userId));
            }
        }

        public int UserId
        {
            get
            {
                return this.userId;
            }
        }

        public string Fullname
        {
            get
            {
                return this.GetClaim(
                    this.userId,
                    new ClaimKey("Fullname"),
                    () => this.usersRepository.GetUserFullname(this.userId));
            }
        }

        public string Username
        {
            get
            {
                return this.GetClaim(
                    this.userId,
                    new ClaimKey("Username"),
                    () => this.usersRepository.GetUserUsername(this.userId));
            }
        }

        public bool IsMonitoringUser
        {
            get
            {
                bool isMonitoringUser = this.GetClaim(
                    this.userId,
                    new ClaimKey("IsMonitoringUser"),
                    () => this.permissionsRepository.UserIsMonitoringUser(this.userId));

                return isMonitoringUser;
            }
        }

        public bool HasProgrammePermission<TEnum>(int programmeId, TEnum permission)
        {
            var userPermissions = this.GetPermissions();

            return this.HasProgrammePermission(userPermissions, programmeId, permission);
        }

        public bool HasAllProgrammePermissions<TEnum>(int programmeId, params TEnum[] permissions)
        {
            var userPermissions = this.GetPermissions();

            var result = true;
            foreach (var permission in permissions)
            {
                result = result && this.HasProgrammePermission(userPermissions, programmeId, permission);
            }

            return result;
        }

        public bool HasProgrammePermissionForAnyProgramme<TEnum>(TEnum permission)
        {
            var userPermissions = this.GetPermissions();

            int[] programmeIds = this.programmeCacheManager.ProgrammeIds;
            foreach (var programmeId in programmeIds)
            {
                if (this.HasProgrammePermission(userPermissions, programmeId, permission))
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasCommonPermission<TEnum>(TEnum permission)
        {
            var userPermissions = this.GetPermissions();

            return this.HasCommonPermission(userPermissions, permission);
        }

        public bool HasAnyCommonPermission<TEnum>(params TEnum[] permissions)
        {
            var userPermissions = this.GetPermissions();

            foreach (var permission in permissions)
            {
                if (this.HasCommonPermission(userPermissions, permission))
                {
                    return true;
                }
            }

            return false;
        }

        private IList<UserPermission> GetPermissions()
        {
            return this.GetClaim(
                this.userId,
                new ClaimKey("Permissions"),
                () => this.permissionsRepository.GetAllUserPermissions(this.userId));
        }

        private bool HasProgrammePermission<TEnum>(IList<UserPermission> userPermissions, int programmeId, TEnum permission)
        {
            return userPermissions.OfType<ProgrammePermission>()
                .Where(up =>
                    up.ProgrammeId == programmeId &&
                    up.PermissionString == permission.ToString() &&
                    up.GetType() == UserPermission.GetPermissionEntityType(typeof(TEnum)))
                .Any();
        }

        private bool HasCommonPermission<TEnum>(IList<UserPermission> userPermissions, TEnum permission)
        {
            return userPermissions.OfType<UserPermission>()
                .Where(up =>
                    up.PermissionString == permission.ToString() &&
                    up.GetType() == UserPermission.GetPermissionEntityType(typeof(TEnum)))
                .Any();
        }

        public bool IsEvalSessionAdmin(int evalSessionId)
        {
            return this.GetClaim(
                this.userId,
                new ClaimKey("IsEvalSessionAdmin", evalSessionId.ToString()),
                () => this.IsEvalSessionAdminImpl(evalSessionId));
        }

        private bool IsEvalSessionAdminImpl(int evalSessionId)
        {
            return this.evalSessionsRepository.IsEvalSessionUser(this.userId, evalSessionId, EvalSessionUserType.Administrator);
        }

        public bool IsEvalSessionAssessor(int evalSessionId)
        {
            return this.GetClaim(
                this.userId,
                new ClaimKey("IsEvalSessionAssessor", evalSessionId.ToString()),
                () => this.IsEvalSessionAssessorImpl(evalSessionId));
        }

        private bool IsEvalSessionAssessorImpl(int evalSessionId)
        {
            return this.evalSessionsRepository.IsEvalSessionUser(this.userId, evalSessionId, EvalSessionUserType.Assessor);
        }

        public bool IsEvalSessionAssistantAssessor(int evalSessionId)
        {
            return this.GetClaim(
                this.userId,
                new ClaimKey("IsEvalSessionAssistantAssessor", evalSessionId.ToString()),
                () => this.IsEvalSessionAssistantAssessorImpl(evalSessionId));
        }

        private bool IsEvalSessionAssistantAssessorImpl(int evalSessionId)
        {
            return this.evalSessionsRepository.IsEvalSessionUser(this.userId, evalSessionId, EvalSessionUserType.AssistantAssessor);
        }

        public bool IsEvalSessionObserver(int evalSessionId)
        {
            return this.GetClaim(
                this.userId,
                new ClaimKey("IsEvalSessionObserver", evalSessionId.ToString()),
                () => this.IsEvalSessionObserverImpl(evalSessionId));
        }

        private bool IsEvalSessionObserverImpl(int evalSessionId)
        {
            return this.evalSessionsRepository.IsEvalSessionUser(this.userId, evalSessionId, EvalSessionUserType.Observer);
        }

        public bool IsEvalSessionProjectAdmin(int projectId)
        {
            return this.GetClaim(
                this.userId,
                new ClaimKey("IsEvalSessionProjectAdmin", projectId.ToString()),
                () => this.IsEvalSessionProjectAdminImpl(projectId));
        }

        private bool IsEvalSessionProjectAdminImpl(int projectId)
        {
            return this.evalSessionsRepository.IsEvalSessionProjectUserAdmin(this.userId, projectId);
        }

        public bool IsEvalSessionProjectAssessor(int projectId)
        {
            return this.GetClaim(
                this.userId,
                new ClaimKey("IsEvalSessionProjectAssessor", projectId.ToString()),
                () => this.IsEvalSessionProjectAssessorImpl(projectId));
        }

        private bool IsEvalSessionProjectAssessorImpl(int projectId)
        {
            return this.evalSessionsRepository.IsEvalSessionProjectAssessor(this.userId, projectId);
        }

        public bool IsEvalSessionProjectAssistantAssessor(int projectId)
        {
            return this.GetClaim(
                this.userId,
                new ClaimKey("IsEvalSessionProjectAssistantAssessor", projectId.ToString()),
                () => this.IsEvalSessionProjectAssistantAssessorImpl(projectId));
        }

        private bool IsEvalSessionProjectAssistantAssessorImpl(int projectId)
        {
            return this.evalSessionsRepository.IsEvalSessionProjectAssistantAssessor(this.userId, projectId);
        }

        public bool IsEvalSessionProjectObserver(int projectId)
        {
            return this.GetClaim(
                this.userId,
                new ClaimKey("IsEvalSessionProjectObserver", projectId.ToString()),
                () => this.IsEvalSessionProjectObserverImpl(projectId));
        }

        private bool IsEvalSessionProjectObserverImpl(int projectId)
        {
            return this.evalSessionsRepository.IsEvalSessionProjectObserver(this.userId, projectId);
        }

        public bool IsAssessorAssociatedWithEvalSessionSheet(int evalSessionSheetId)
        {
            return this.GetClaim(
                this.userId,
                new ClaimKey("IsAssessorAssociatedWithEvalSessionSheet", evalSessionSheetId.ToString()),
                () => this.IsAssessorAssociatedWithEvalSessionSheetImpl(evalSessionSheetId));
        }

        private bool IsAssessorAssociatedWithEvalSessionSheetImpl(int evalSessionSheetId)
        {
            return this.evalSessionsRepository.IsAssessorAssociatedWithEvalSessionSheet(evalSessionSheetId, this.userId);
        }

        public bool IsUserAssociatedWithEvalSessionStandpoint(int evalSessionStandpointId)
        {
            return this.GetClaim(
                this.userId,
                new ClaimKey("IsUserAssociatedWithEvalSessionStandpoint", evalSessionStandpointId.ToString()),
                () => this.IsUserAssociatedWithEvalSessionStandpointImpl(evalSessionStandpointId));
        }

        private bool IsUserAssociatedWithEvalSessionStandpointImpl(int evalSessionStandpointId)
        {
            return this.evalSessionsRepository.IsAssessorAssociatedWithEvalSessionStandpoint(evalSessionStandpointId, this.userId);
        }

        public bool IsContractExternalUser(int? contractId)
        {
            return this.GetClaim(
                this.userId,
                new ClaimKey("IsContractExternalUser", contractId.ToString()),
                () => this.IsContractExternalUserImpl(contractId));
        }

        private bool IsContractExternalUserImpl(int? contractId)
        {
            if (!contractId.HasValue)
            {
                return false;
            }

            return this.contractsRepository.IsUserAssociatedWithContract(contractId.Value, this.userId);
        }

        public bool HasAnyContractExternalUserPermission()
        {
            return this.GetClaim(
                this.userId,
                new ClaimKey("HasAnyContractExternalUserPermission"),
                () => this.contractsRepository.IsUserAssociatedWithAnyContract(this.userId));
        }
    }
}
