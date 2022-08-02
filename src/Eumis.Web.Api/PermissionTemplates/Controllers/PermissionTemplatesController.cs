using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.PermissionTemplates.Repositories;
using Eumis.Data.PermissionTemplates.ViewObjects;
using Eumis.Data.Users.Repositories;
using Eumis.Data.UserTypes.Repositories;
using Eumis.Domain.PermissionTemplates;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.PermissionTemplates.DataObjects;

namespace Eumis.Web.Api.Users.Controllers
{
    [RoutePrefix("api/permissionTemplates")]
    public class PermissionTemplatesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IPermissionTemplatesRepository permissionTemplatesRepository;
        private IProgrammesRepository programmesRepository;
        private IUserTypesRepository userTypesRepository;
        private IUsersRepository usersRepository;
        private IAuthorizer authorizer;
        private IProgrammeCacheManager programmeCacheManager;

        public PermissionTemplatesController(
            IUnitOfWork unitOfWork,
            IPermissionTemplatesRepository permissionTemplatesRepository,
            IProgrammesRepository programmesRepository,
            IUserTypesRepository userTypesRepository,
            IUsersRepository usersRepository,
            IAuthorizer authorizer,
            IProgrammeCacheManager programmeCacheManager)
        {
            this.unitOfWork = unitOfWork;
            this.permissionTemplatesRepository = permissionTemplatesRepository;
            this.programmesRepository = programmesRepository;
            this.userTypesRepository = userTypesRepository;
            this.usersRepository = usersRepository;
            this.authorizer = authorizer;
            this.programmeCacheManager = programmeCacheManager;
        }

        [Route("")]
        public IList<PermissionTemplateVO> GetPermissionTemplates()
        {
            this.authorizer.AssertCanDo(PermissionTemplateListActions.Search);

            return this.permissionTemplatesRepository.GetPermissionTemplates();
        }

        [Route("{permissionTemplateId:int}")]
        public PermissionTemplateDO GetPermissionTemplate(int permissionTemplateId)
        {
            this.authorizer.AssertCanDo(PermissionTemplateActions.View, permissionTemplateId);

            var permissionTemplate = this.permissionTemplatesRepository.Find(permissionTemplateId);

            var programmes = this.programmesRepository.GetProgrammesIdAndShortName();

            return new PermissionTemplateDO(permissionTemplate, programmes, permissionTemplate.Version);
        }

        [Route("new")]
        public PermissionTemplateDO GetNewPermissionTemplate()
        {
            this.authorizer.AssertCanDo(PermissionTemplateListActions.Create);

            var programmes = this.programmesRepository.GetProgrammesIdAndShortName();

            return new PermissionTemplateDO(programmes);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.PermissionTemplates.Create))]
        public void CreatePermissionTemplate(PermissionTemplateDO permissionTemplate)
        {
            this.authorizer.AssertCanDo(PermissionTemplateListActions.Create);

            var programmeIds = this.programmeCacheManager.ProgrammeIds;
            PermissionTemplate newPermissionTemplate =
                new PermissionTemplate(
                    permissionTemplate.Name,
                    permissionTemplate.UserPermissions.GetPermissions(programmeIds));

            this.permissionTemplatesRepository.Add(newPermissionTemplate);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{permissionTemplateid:int}/canUpdate")]
        public ErrorsDO CanUpdate(int permissionTemplateId, PermissionTemplateDO permissionTemplate)
        {
            var programmeIds = this.programmeCacheManager.ProgrammeIds;
            var newPermissionTemplate = permissionTemplate.UserPermissions.GetPermissions(programmeIds);
            var users = this.usersRepository.FindAllByPermissionTemplate(permissionTemplateId);
            var programmes = this.programmesRepository.GetProgrammesIdAndShortName();

            IList<string> errors = new List<string>();

            foreach (var user in users)
            {
                var permissionWarnings = user.WouldChangePermissions(programmeIds, newPermissionTemplate, programmes);

                if (permissionWarnings.Any())
                {
                    errors.Add($"Потребител {user.Fullname.ToUpper()} e обвързан с отбелязаните за премахване права:\r\n{string.Join(";\r\n", permissionWarnings.ToArray())}");
                }
            }

            return new ErrorsDO(errors);
        }

        [HttpPut]
        [Route("{permissionTemplateid:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.PermissionTemplates.Edit), IdParam = "permissionTemplateId")]
        public void UpdatePermissionTemplate(int permissionTemplateId, PermissionTemplateDO permissionTemplate)
        {
            this.authorizer.AssertCanDo(PermissionTemplateActions.Edit, permissionTemplateId);

            PermissionTemplate oldPermissionTemplate = this.permissionTemplatesRepository.FindForUpdate(permissionTemplateId, permissionTemplate.Version);

            var programmeIds = this.programmeCacheManager.ProgrammeIds;
            oldPermissionTemplate.Update(
                permissionTemplate.Name,
                permissionTemplate.UserPermissions.GetPermissions(programmeIds));

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("isNameExist")]
        public bool IsNameExist(string name)
        {
            var isNameExist = this.permissionTemplatesRepository.IsNameExist(name);

            return isNameExist;
        }
    }
}
