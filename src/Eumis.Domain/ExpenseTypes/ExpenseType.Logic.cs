using System;
using System.Linq;

namespace Eumis.Domain.ExpenseTypes
{
    public partial class ExpenseType
    {
        public void UpdateExpenseType(string name, string nameAlt)
        {
            this.Name = name;
            this.NameAlt = nameAlt;
            this.ModifyDate = DateTime.Now;
        }

        public void DeactivateExpenseType()
        {
            this.IsActive = false;
            this.ModifyDate = DateTime.Now;
        }

        public void ActivateExpenseType()
        {
            this.IsActive = true;
            this.ModifyDate = DateTime.Now;
        }

        #region ExpenseSubType

        public ExpenseSubType FindExpenseSubType(int expenseSubTypeId)
        {
            var expenseSubType = this.ExpenseSubTypes.Where(i => i.ExpenseSubTypeId == expenseSubTypeId).SingleOrDefault();

            if (expenseSubType == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ExpenseSubType with ExpenseSubTypeId " + expenseSubTypeId);
            }

            return expenseSubType;
        }

        public void UpdateExpenseSubType(int expenseSubTypeId, string name, string nameAlt)
        {
            var expenseSubType = this.FindExpenseSubType(expenseSubTypeId);

            expenseSubType.SetAttributes(name, nameAlt);

            this.ModifyDate = DateTime.Now;
        }

        public void AddExpenseSubType(string name, string nameAlt)
        {
            this.ExpenseSubTypes.Add(new ExpenseSubType()
            {
                ExpenseTypeId = this.ExpenseTypeId,
                Name = name,
                NameAlt = nameAlt,
            });

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveExpenseSubType(int expenseSubTypeId)
        {
            var expenseSubType = this.FindExpenseSubType(expenseSubTypeId);

            this.ExpenseSubTypes.Remove(expenseSubType);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //ExpenseSubType
    }
}
