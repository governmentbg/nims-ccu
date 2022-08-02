using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.InterestSchemes.Repositories;
using Eumis.Data.InterestSchemes.ViewObjects;
using Eumis.Domain.InterestSchemes;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.InterestSchemes.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.InterestSchemes.Controllers
{
    [RoutePrefix("api/interestSchemes")]
    public class InterestSchemesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IInterestSchemesRepository interestSchemesRepository;
        private IAuthorizer authorizer;

        public InterestSchemesController(
            IUnitOfWork unitOfWork,
            IInterestSchemesRepository interestSchemesRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.interestSchemesRepository = interestSchemesRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<InterestSchemeVO> GetInterestSchemes()
        {
            this.authorizer.AssertCanDo(InterestSchemeListActions.Search);

            return this.interestSchemesRepository.GetInterestSchemes();
        }

        [Route("{interestSchemeId:int}")]
        public InterestSchemeDO GetInterestScheme(int interestSchemeId)
        {
            this.authorizer.AssertCanDo(InterestSchemeActions.View, interestSchemeId);

            var interestScheme = this.interestSchemesRepository.Find(interestSchemeId);

            return new InterestSchemeDO(interestScheme);
        }

        [HttpPut]
        [Route("{interestSchemeId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.InterestSchemes.Edit), IdParam = "interestSchemeId")]
        public void UpdateInterestScheme(int interestSchemeId, InterestSchemeDO interestScheme)
        {
            this.authorizer.AssertCanDo(InterestSchemeActions.Edit, interestSchemeId);

            InterestScheme oldInterestScheme = this.interestSchemesRepository.FindForUpdate(interestSchemeId, interestScheme.Version);
            oldInterestScheme.UpdateInterestScheme(
                interestScheme.Name,
                interestScheme.BasicInterestRateId.Value,
                interestScheme.AllowanceId.Value,
                interestScheme.AnnualBasis.Value,
                interestScheme.IsActive.Value);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("new")]
        public InterestSchemeDO NewInterestScheme()
        {
            this.authorizer.AssertCanDo(InterestSchemeListActions.Create);

            return new InterestSchemeDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.InterestSchemes.Create))]
        public InterestSchemeDO CreateInterestScheme(InterestSchemeDO interestScheme)
        {
            this.authorizer.AssertCanDo(InterestSchemeListActions.Create);

            InterestScheme newInterestScheme = new InterestScheme(
                interestScheme.Name,
                interestScheme.BasicInterestRateId.Value,
                interestScheme.AllowanceId.Value,
                interestScheme.AnnualBasis.Value,
                interestScheme.IsActive.Value);

            this.interestSchemesRepository.Add(newInterestScheme);

            this.unitOfWork.Save();

            return new InterestSchemeDO(newInterestScheme);
        }

        [HttpDelete]
        [Route("{interestSchemeId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.InterestSchemes.Delete), IdParam = "interestSchemeId")]
        public void DeleteInterestScheme(int interestSchemeId, string version)
        {
            this.authorizer.AssertCanDo(InterestSchemeActions.Delete, interestSchemeId);

            byte[] vers = System.Convert.FromBase64String(version);
            InterestScheme oldInterestScheme = this.interestSchemesRepository.FindForUpdate(interestSchemeId, vers);

            if (this.interestSchemesRepository.CanDelete(interestSchemeId).Any())
            {
                throw new Exception("Cannot delete InterestScheme");
            }

            this.interestSchemesRepository.Remove(oldInterestScheme);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{interestSchemeId:int}/canDelete")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.InterestSchemes.Create))]
        public ErrorsDO CanDeleteInterestScheme(int interestSchemeId)
        {
            this.authorizer.AssertCanDo(InterestSchemeActions.Delete, interestSchemeId);

            var errors = this.interestSchemesRepository.CanDelete(interestSchemeId);

            return new ErrorsDO(errors);
        }
    }
}
