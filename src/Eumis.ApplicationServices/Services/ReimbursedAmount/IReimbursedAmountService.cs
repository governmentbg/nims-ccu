namespace Eumis.ApplicationServices.Services.ReimbursedAmount
{
    public interface IReimbursedAmountService
    {
        bool CanCreateDebtReimbursedAmount(int userId, Domain.Debts.ContractDebt contractDebt);

        bool CanCreateContractReimbursedAmount(int userId, Domain.Contracts.Contract contract);
    }
}
