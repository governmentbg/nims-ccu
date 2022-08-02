namespace Eumis.ApplicationServices.Services.EuReimbursedAmount
{
    public interface IEuReimbursedAmountService
    {
        bool CanCreate(int userId, int programmeId);

        void AddCertReports(Domain.EuReimbursedAmounts.EuReimbursedAmount amount, int userId, int[] itemIds);
    }
}
