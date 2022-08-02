using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Indicators.Repositories;
using Eumis.Data.Indicators.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.Indicators;
using Eumis.Domain.Indicators.DataObjects;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Indicators.Controllers
{
    [RoutePrefix("api/indicatorItemTypes")]
    public class IndicatorItemTypesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IIndicatorItemTypesRepository indicatorItemTypesRepository;
        private IAuthorizer authorizer;
        private IPermissionsRepository permissionsRepository;
        private IAccessContext accessContext;

        public IndicatorItemTypesController(
            IUnitOfWork unitOfWork,
            IIndicatorItemTypesRepository indicatorsItemTypeRepository,
            IAuthorizer authorizer,
            IPermissionsRepository permissionsRepository,
            IAccessContext accessContext)
        {
            this.unitOfWork = unitOfWork;
            this.indicatorItemTypesRepository = indicatorsItemTypeRepository;
            this.authorizer = authorizer;
            this.permissionsRepository = permissionsRepository;
            this.accessContext = accessContext;
        }

        [Route("")]
        public IList<IndicatorItemTypesVO> GetIndicators()
        {
            this.authorizer.AssertCanDo(IndicatorListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, IndicatorPermissions.CanRead);

            return this.indicatorItemTypesRepository.GetIndicatorTypes();
        }

        [Route("{indicatorItemTypeId:int}")]
        public IndicatorItemTypeDO GetIndicator(int indicatorItemTypeId)
        {
            this.authorizer.AssertCanDo(IndicatorListActions.Search);

            var indicatorType = this.indicatorItemTypesRepository.Find(indicatorItemTypeId);

            return new IndicatorItemTypeDO(indicatorType);
        }

        [HttpGet]
        [Route("new")]
        public IndicatorItemTypeDO CreateIndicator()
        {
            this.authorizer.AssertCanDo(IndicatorListActions.Search);

            return new IndicatorItemTypeDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.IndicatorItemTypes.Create))]
        public void CreateIndicator(IndicatorItemTypeDO indicatorType)
        {
            this.authorizer.AssertCanDo(IndicatorListActions.Create);

            var newIndicatorType = new IndicatorItemType(indicatorType.Name, indicatorType.NameAlt);
            this.indicatorItemTypesRepository.Add(newIndicatorType);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{indicatorItemTypeId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.IndicatorItemTypes.Edit), IdParam = "indicatorItemTypeId")]
        public void UpdateIndicator(int indicatorItemTypeId, IndicatorItemTypeDO indicatorType)
        {
            this.authorizer.AssertCanDo(IndicatorListActions.Create);

            IndicatorItemType oldIndicatorType = this.indicatorItemTypesRepository.FindForUpdate(indicatorItemTypeId, indicatorType.Version);

            oldIndicatorType.UpdateAttributes(
                indicatorType.Name,
                indicatorType.NameAlt);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{indicatorItemTypeId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.IndicatorItemTypes.Delete), IdParam = "indicatorItemTypeId")]
        public void DeleteIndicator(int indicatorItemTypeId, string version)
        {
            this.authorizer.AssertCanDo(IndicatorListActions.Create);

            var errors = this.indicatorItemTypesRepository.CanDeleteIndicatorType(indicatorItemTypeId);
            if (errors.Count > 0)
            {
                throw new DomainValidationException("Indicator item type is still referred in indicator");
            }

            byte[] vers = System.Convert.FromBase64String(version);

            IndicatorItemType oldIndicatorType = this.indicatorItemTypesRepository.FindForUpdate(indicatorItemTypeId, vers);

            this.indicatorItemTypesRepository.Remove(oldIndicatorType);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{indicatorItemTypeId:int}/canDelete")]
        public ErrorsDO CanDeleteIndicator(int indicatorItemTypeId)
        {
            this.authorizer.AssertCanDo(IndicatorListActions.Create);

            var errors = this.indicatorItemTypesRepository.CanDeleteIndicatorType(indicatorItemTypeId);

            return new ErrorsDO(errors);
        }
    }
}
