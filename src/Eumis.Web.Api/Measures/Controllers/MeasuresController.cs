using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Measures.Repositories;
using Eumis.Data.Measures.ViewObjects;
using Eumis.Domain.Measures;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Measures.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Measures.Controllers
{
    [RoutePrefix("api/measures")]
    public class MeasuresController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IMeasuresRepository measuresRepository;
        private IAuthorizer authorizer;

        public MeasuresController(IUnitOfWork unitOfWork, IMeasuresRepository measuresRepository, IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.measuresRepository = measuresRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<MeasuresVO> GetMeasures()
        {
            this.authorizer.AssertCanDo(MeasureListActions.Search);

            return this.measuresRepository.GetMeasures();
        }

        [Route("{measureId:int}")]
        public MeasureDO GetMeasure(int measureId)
        {
            this.authorizer.AssertCanDo(MeasureActions.View, measureId);

            var measure = this.measuresRepository.Find(measureId);

            return new MeasureDO(measure);
        }

        [HttpGet]
        [Route("new")]
        public MeasureDO NewMeasure()
        {
            this.authorizer.AssertCanDo(MeasureListActions.Create);

            return new MeasureDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Measures.Create))]
        public MeasureDO CreateMeasure(MeasureDO measure)
        {
            this.authorizer.AssertCanDo(MeasureListActions.Create);

            Measure newMeasure = new Measure(
                measure.ShortName,
                measure.Name,
                measure.NameAlt);

            this.measuresRepository.Add(newMeasure);

            this.unitOfWork.Save();

            return new MeasureDO(newMeasure);
        }

        [HttpPut]
        [Route("{measureId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Measures.Edit), IdParam = "measureId")]
        public void UpdateMeasure(int measureId, MeasureDO measure)
        {
            this.authorizer.AssertCanDo(MeasureActions.Edit, measureId);

            Measure oldMeasure = this.measuresRepository.FindForUpdate(measureId, measure.Version);

            oldMeasure.UpdateMeasure(
                measure.ShortName,
                measure.Name,
                measure.NameAlt);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{measureId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Measures.Delete), IdParam = "measureId")]
        public void DeleteMeasure(int measureId, string version)
        {
            this.authorizer.AssertCanDo(MeasureActions.Delete, measureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Measure oldMeasure = this.measuresRepository.FindForUpdate(measureId, vers);

            this.measuresRepository.Remove(oldMeasure);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{measureId:int}/canDelete")]
        public ErrorsDO CanDeleteMeasure(int measureId)
        {
            this.authorizer.AssertCanDo(MeasureActions.View, measureId);

            var errorList = this.measuresRepository.CanDeleteMeasure(measureId);

            return new ErrorsDO(errorList);
        }
    }
}
