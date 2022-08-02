using Eumis.Domain.ExpenseTypes;

namespace Eumis.Web.Api.ExpenseTypes.DataObjects
{
    public class ExpenseSubTypeDO
    {
        public ExpenseSubTypeDO()
        {
        }

        public ExpenseSubTypeDO(int expenseTypeId, byte[] version)
        {
            this.ExpenseTypeId = expenseTypeId;
            this.Version = version;
        }

        public ExpenseSubTypeDO(ExpenseSubType expenseSubType, byte[] version)
        {
            this.ExpenseTypeId = expenseSubType.ExpenseTypeId;
            this.ExpenseSubTypeId = expenseSubType.ExpenseSubTypeId;
            this.Name = expenseSubType.Name;
            this.NameAlt = expenseSubType.NameAlt;

            this.Version = version;
        }

        public int ExpenseTypeId { get; set; }

        public int? ExpenseSubTypeId { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public byte[] Version { get; set; }
    }
}
