using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Indicators.Repositories;
using Eumis.Data.Indicators.ViewObjects;
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
    [RoutePrefix("api/indicators")]
    public class IndicatorsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IIndicatorsRepository indicatorsRepository;
        private IAuthorizer authorizer;
        private IPermissionsRepository permissionsRepository;
        private IAccessContext accessContext;

        public IndicatorsController(
            IUnitOfWork unitOfWork,
            IIndicatorsRepository indicatorsRepository,
            IAuthorizer authorizer,
            IPermissionsRepository permissionsRepository,
            IAccessContext accessContext)
        {
            this.unitOfWork = unitOfWork;
            this.indicatorsRepository = indicatorsRepository;
            this.authorizer = authorizer;
            this.permissionsRepository = permissionsRepository;
            this.accessContext = accessContext;
        }

        [Route("")]
        public IList<IndicatorsVO> GetIndicators()
        {
            this.authorizer.AssertCanDo(IndicatorListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, IndicatorPermissions.CanRead);

            return this.indicatorsRepository.GetIndicators(programmeIds);
        }

        [Route("{indicatorId:int}")]
        public IndicatorDO GetIndicator(int indicatorId)
        {
            this.authorizer.AssertCanDo(IndicatorActions.View, indicatorId);

            var indicator = this.indicatorsRepository.Find(indicatorId);

            return new IndicatorDO(indicator);
        }

        [HttpPut]
        [Route("{indicatorId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Indicators.Edit), IdParam = "indicatorId")]
        public void UpdateIndicator(int indicatorId, IndicatorDO indicator)
        {
            this.authorizer.AssertCanDo(IndicatorActions.Edit, indicatorId);

            Indicator oldIndicator = this.indicatorsRepository.FindForUpdate(indicatorId, indicator.Version);

            oldIndicator.UpdateAttributes(
                indicator.MeasureId.Value,
                indicator.Name,
                indicator.NameAlt,
                indicator.HasGenderDivision.Value);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{indicatorId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Indicators.Delete), IdParam = "indicatorId")]
        public void DeleteIndicator(int indicatorId, string version)
        {
            this.authorizer.AssertCanDo(IndicatorActions.Delete, indicatorId);

            byte[] vers = System.Convert.FromBase64String(version);

            Indicator oldIndicator = this.indicatorsRepository.FindForUpdate(indicatorId, vers);

            this.indicatorsRepository.Remove(oldIndicator);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{indicatorId:int}/canDelete")]
        public ErrorsDO CanDeleteIndicator(int indicatorId)
        {
            this.authorizer.AssertCanDo(IndicatorActions.View, indicatorId);

            var errors = this.indicatorsRepository.CanDeleteIndicator(indicatorId);

            return new ErrorsDO(errors);
        }
    }
}
