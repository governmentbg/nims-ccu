using System;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class ContractsReportContractAmountsGroupingItem : IEquatable<ContractsReportContractAmountsGroupingItem>
    {
        public int ContractId { get; set; }

        public string ContractBudgetLevel3AmountNutsFullPath { get; set; }

        public string ContractBudgetLevel3AmountNutsFullPathName { get; set; }

        public bool Equals(ContractsReportContractAmountsGroupingItem other)
        {
            if (other == null)
            {
                return false;
            }

            return this.ContractId == other.ContractId
                && this.ContractBudgetLevel3AmountNutsFullPath == other.ContractBudgetLevel3AmountNutsFullPath
                && this.ContractBudgetLevel3AmountNutsFullPathName == other.ContractBudgetLevel3AmountNutsFullPathName;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ContractsReportContractAmountsGroupingItem);
        }

        public override int GetHashCode()
        {
            var hash = this.ContractId.GetHashCode();

            if (this.ContractBudgetLevel3AmountNutsFullPath != null)
            {
                hash ^= this.ContractBudgetLevel3AmountNutsFullPath.GetHashCode();
            }

            if (this.ContractBudgetLevel3AmountNutsFullPathName != null)
            {
                hash ^= this.ContractBudgetLevel3AmountNutsFullPathName.GetHashCode();
            }

            return hash;
        }
    }
}
