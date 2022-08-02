using Eumis.Domain.ExpenseTypes;

namespace Eumis.Web.Api.ExpenseTypes.DataObjects
{
    public class ExpenseTypeDataDO
    {
        public ExpenseTypeDataDO()
        {
            this.IsActive = true;
        }

        public ExpenseTypeDataDO(ExpenseType expenseType)
        {
            this.ExpenseTypeId = expenseType.ExpenseTypeId;
            this.Name = expenseType.Name;
            this.NameAlt = expenseType.NameAlt;
            this.IsActive = expenseType.IsActive;
            this.Version = expenseType.Version;
        }

        public int? ExpenseTypeId { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }
    }
}
