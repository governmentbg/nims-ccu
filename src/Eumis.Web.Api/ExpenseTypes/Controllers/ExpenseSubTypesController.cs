using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.ExpenseTypes.Repositories;
using Eumis.Domain.ExpenseTypes;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.ExpenseTypes.DataObjects;
using System.Web.Http;

namespace Eumis.Web.Api.ExpenseTypes.Controllers
{
    [RoutePrefix("api/expenseTypes/{expenseTypeId:int}/expenseSubTypes")]
    public class ExpenseSubTypesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IExpenseTypesRepository expenseTypesRepository;
        private IAuthorizer authorizer;

        public ExpenseSubTypesController(
            IUnitOfWork unitOfWork,
            IExpenseTypesRepository expenseTypesRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.expenseTypesRepository = expenseTypesRepository;
            this.authorizer = authorizer;
        }

        [Route("{expenseSubTypeId:int}")]
        public ExpenseSubTypeDO GetExpenseSubType(int expenseTypeId, int expenseSubTypeId)
        {
            this.authorizer.AssertCanDo(ExpenseTypeActions.View, expenseTypeId);

            var expenseType = this.expenseTypesRepository.Find(expenseTypeId);
            var expenseSubType = expenseType.FindExpenseSubType(expenseSubTypeId);

            return new ExpenseSubTypeDO(expenseSubType, expenseType.Version);
        }

        [HttpPut]
        [Route("{expenseSubTypeId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ExpenseTypes.Edit.ExpenseSubTypes.Edit), IdParam = "expenseTypeId", ChildIdParam = "expenseSubTypeId")]
        public void UpdateExpenseSubType(int expenseTypeId, int expenseSubTypeId, ExpenseSubTypeDO expenseSubType)
        {
            this.authorizer.AssertCanDo(ExpenseTypeActions.Edit, expenseTypeId);

            ExpenseType expenseType = this.expenseTypesRepository.FindForUpdate(expenseTypeId, expenseSubType.Version);
            expenseType.UpdateExpenseSubType(expenseSubTypeId, expenseSubType.Name, expenseSubType.NameAlt);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("new")]
        public ExpenseSubTypeDO NewExpenseSubType(int expenseTypeId)
        {
            this.authorizer.AssertCanDo(ExpenseTypeListActions.Create);

            ExpenseType expenseType = this.expenseTypesRepository.Find(expenseTypeId);

            return new ExpenseSubTypeDO(expenseTypeId, expenseType.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ExpenseTypes.Edit.ExpenseSubTypes.Create), IdParam = "expenseTypeId")]
        public void CreateExpenseSubType(int expenseTypeId, ExpenseSubTypeDO expenseSubType)
        {
            this.authorizer.AssertCanDo(ExpenseTypeListActions.Create);

            ExpenseType expenseType = this.expenseTypesRepository.FindForUpdate(expenseTypeId, expenseSubType.Version);

            expenseType.AddExpenseSubType(expenseSubType.Name, expenseSubType.NameAlt);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{expenseSubTypeId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ExpenseTypes.Edit.ExpenseSubTypes.Delete), IdParam = "expenseTypeId", ChildIdParam = "expenseSubTypeId")]
        public void DeleteTExpenseSubType(int expenseTypeId, int expenseSubTypeId, string version)
        {
            this.authorizer.AssertCanDo(ExpenseTypeActions.Edit, expenseTypeId);

            byte[] vers = System.Convert.FromBase64String(version);
            ExpenseType expenseType = this.expenseTypesRepository.FindForUpdate(expenseTypeId, vers);

            expenseType.RemoveExpenseSubType(expenseSubTypeId);

            this.unitOfWork.Save();
        }
    }
}
