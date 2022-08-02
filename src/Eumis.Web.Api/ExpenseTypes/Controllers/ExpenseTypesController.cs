using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.ExpenseTypes.Repositories;
using Eumis.Data.ExpenseTypes.ViewObjects;
using Eumis.Domain.ExpenseTypes;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.ExpenseTypes.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ExpenseTypes.Controllers
{
    [RoutePrefix("api/expenseTypes")]
    public class ExpenseTypesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IExpenseTypesRepository expenseTypesRepository;
        private IAuthorizer authorizer;

        public ExpenseTypesController(
            IUnitOfWork unitOfWork,
            IExpenseTypesRepository expenseTypesRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.expenseTypesRepository = expenseTypesRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ExpenseTypeVO> GetExpenseTypes()
        {
            this.authorizer.AssertCanDo(ExpenseTypeListActions.Search);

            return this.expenseTypesRepository.GetExpenseTypes();
        }

        [Route("{expenseTypeId:int}")]
        public ExpenseTypeDO GetExpenseType(int expenseTypeId)
        {
            this.authorizer.AssertCanDo(ExpenseTypeActions.View, expenseTypeId);

            var expenseType = this.expenseTypesRepository.Find(expenseTypeId);

            return new ExpenseTypeDO(expenseType);
        }

        [HttpPut]
        [Route("{expenseTypeId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ExpenseTypes.Edit.BasicData), IdParam = "expenseTypeId")]
        public void UpdateExpenseType(int expenseTypeId, ExpenseTypeDataDO expenseTypeData)
        {
            this.authorizer.AssertCanDo(ExpenseTypeActions.Edit, expenseTypeId);

            ExpenseType oldExpenseType = this.expenseTypesRepository.FindForUpdate(expenseTypeId, expenseTypeData.Version);
            oldExpenseType.UpdateExpenseType(expenseTypeData.Name, expenseTypeData.NameAlt);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("new")]
        public ExpenseTypeDataDO NewExpenseType()
        {
            this.authorizer.AssertCanDo(ExpenseTypeListActions.Create);

            return new ExpenseTypeDataDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ExpenseTypes.Create))]
        public ExpenseTypeDO CreateExpenseType(ExpenseTypeDataDO expenseTypeData)
        {
            this.authorizer.AssertCanDo(ExpenseTypeListActions.Create);

            ExpenseType newExpenseType = new ExpenseType(expenseTypeData.Name, expenseTypeData.NameAlt, expenseTypeData.IsActive);

            this.expenseTypesRepository.Add(newExpenseType);

            this.unitOfWork.Save();

            return new ExpenseTypeDO(newExpenseType);
        }

        [HttpPut]
        [Route("{expenseTypeId:int}/deactivate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ExpenseTypes.Deactivate), IdParam = "expenseTypeId")]
        public void DeactivateExpenseType(int expenseTypeId, string version)
        {
            this.authorizer.AssertCanDo(ExpenseTypeActions.Edit, expenseTypeId);

            byte[] vers = System.Convert.FromBase64String(version);
            ExpenseType expenseType = this.expenseTypesRepository.FindForUpdate(expenseTypeId, vers);

            expenseType.DeactivateExpenseType();

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{expenseTypeId:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ExpenseTypes.Activate), IdParam = "expenseTypeId")]
        public void ActivateExpenseType(int expenseTypeId, string version)
        {
            this.authorizer.AssertCanDo(ExpenseTypeActions.Edit, expenseTypeId);

            byte[] vers = System.Convert.FromBase64String(version);
            ExpenseType expenseType = this.expenseTypesRepository.FindForUpdate(expenseTypeId, vers);

            expenseType.ActivateExpenseType();

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{expenseTypeId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ExpenseTypes.Delete), IdParam = "expenseTypeId")]
        public void DeleteExpenseType(int expenseTypeId, string version)
        {
            this.authorizer.AssertCanDo(ExpenseTypeActions.Delete, expenseTypeId);

            byte[] vers = System.Convert.FromBase64String(version);
            ExpenseType oldExpenseType = this.expenseTypesRepository.FindForUpdate(expenseTypeId, vers);

            this.expenseTypesRepository.Remove(oldExpenseType);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{expenseTypeId:int}/canDelete")]
        public ErrorsDO CanDeleteExpenseType(int expenseTypeId)
        {
            this.authorizer.AssertCanDo(ExpenseTypeActions.View, expenseTypeId);

            var errorList = this.expenseTypesRepository.CanDeleteExpenseType(expenseTypeId);

            return new ErrorsDO(errorList);
        }
    }
}
