using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.ExpenseTypes.Repositories;
using Eumis.Domain.ExpenseTypes;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ExpenseTypes.Controllers
{
    [RoutePrefix("api/nomenclatures/expenseTypes")]
    public class ExpenseTypeNomsController : EntityNomsController<ExpenseType, EntityNomVO>
    {
        private IExpenseTypeNomsRepository expenseTypeNomRepository;

        public ExpenseTypeNomsController(IExpenseTypeNomsRepository expenseTypeNomRepository)
            : base(expenseTypeNomRepository)
        {
            this.expenseTypeNomRepository = expenseTypeNomRepository;
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetUnusedExpenseTypesByProcedureAndProgramme(int procedureId, int programmeId, string term = null, int offset = 0, int? limit = null)
        {
            return this.expenseTypeNomRepository.GetExpenseTypeNoms(procedureId, programmeId, term, offset, limit);
        }
    }
}
