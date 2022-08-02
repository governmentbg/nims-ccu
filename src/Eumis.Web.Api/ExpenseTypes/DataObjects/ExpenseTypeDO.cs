using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.ExpenseTypes;

namespace Eumis.Web.Api.ExpenseTypes.DataObjects
{
    public class ExpenseTypeDO
    {
        public ExpenseTypeDO()
        {
            this.ExpenseSubTypes = new List<ExpenseSubTypeDO>();
        }

        public ExpenseTypeDO(ExpenseType expenseType)
        {
            this.ExpenseTypeData = new ExpenseTypeDataDO(expenseType);
            this.ExpenseSubTypes = expenseType.ExpenseSubTypes
                .Select(est => new ExpenseSubTypeDO(est, expenseType.Version));
        }

        public ExpenseTypeDataDO ExpenseTypeData { get; set; }

        public IEnumerable<ExpenseSubTypeDO> ExpenseSubTypes { get; set; }
    }
}
