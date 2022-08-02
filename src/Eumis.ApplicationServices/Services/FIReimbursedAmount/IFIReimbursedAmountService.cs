namespace Eumis.ApplicationServices.Services.FIReimbursedAmount
{
    public interface IFIReimbursedAmountService
    {
        bool CanCreateFIReimbursedAmount(int userId, Domain.Contracts.Contract contract);
    }
}
