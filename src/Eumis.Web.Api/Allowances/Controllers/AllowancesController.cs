using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Allowances.Repositories;
using Eumis.Data.Allowances.ViewObjects;
using Eumis.Domain.Allowances;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Allowances.DataObjects;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Allowances.Controllers
{
    [RoutePrefix("api/allowances")]
    public class AllowancesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAllowancesRepository allowancesRepository;
        private IAuthorizer authorizer;

        public AllowancesController(
            IUnitOfWork unitOfWork,
            IAllowancesRepository allowancesRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.allowancesRepository = allowancesRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<AllowanceVO> GetAllowances()
        {
            this.authorizer.AssertCanDo(AllowanceListActions.Search);

            return this.allowancesRepository.GetAllowances();
        }

        [Route("{allowanceId:int}")]
        public AllowanceDO GetAllowance(int allowanceId)
        {
            this.authorizer.AssertCanDo(AllowanceActions.View, allowanceId);

            var allowance = this.allowancesRepository.Find(allowanceId);

            return new AllowanceDO(allowance);
        }

        [HttpPut]
        [Route("{allowanceId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Allowances.Edit.BasicData), IdParam = "allowanceId")]
        public void UpdateAllowance(int allowanceId, AllowanceDataDO allowanceData)
        {
            this.authorizer.AssertCanDo(AllowanceActions.Edit, allowanceId);

            Allowance oldAllowance = this.allowancesRepository.FindForUpdate(allowanceId, allowanceData.Version);
            oldAllowance.UpdateAllowance(allowanceData.Name);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("new")]
        public AllowanceDataDO NewAllowance()
        {
            this.authorizer.AssertCanDo(AllowanceListActions.Create);

            return new AllowanceDataDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Allowances.Create))]
        public AllowanceDO CreateAllowance(AllowanceDataDO allowanceData)
        {
            this.authorizer.AssertCanDo(AllowanceListActions.Create);

            Allowance newAllowance = new Allowance(allowanceData.Name);

            this.allowancesRepository.Add(newAllowance);

            this.unitOfWork.Save();

            return new AllowanceDO(newAllowance);
        }

        [HttpDelete]
        [Route("{allowanceId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Allowances.Delete), IdParam = "allowanceId")]
        public void DeleteAllowance(int allowanceId, string version)
        {
            this.authorizer.AssertCanDo(AllowanceActions.Delete, allowanceId);

            byte[] vers = System.Convert.FromBase64String(version);
            Allowance oldAllowance = this.allowancesRepository.FindForUpdate(allowanceId, vers);

            this.allowancesRepository.Remove(oldAllowance);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{allowanceId:int}/canDelete")]
        public ErrorsDO CanDeleteAllowance(int allowanceId)
        {
            this.authorizer.AssertCanDo(AllowanceActions.View, allowanceId);

            var errorList = this.allowancesRepository.CanDeleteAllowance(allowanceId);

            return new ErrorsDO(errorList);
        }
    }
}
