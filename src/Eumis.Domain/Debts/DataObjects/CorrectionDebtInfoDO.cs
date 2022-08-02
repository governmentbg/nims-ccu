namespace Eumis.Domain.Debts.DataObjects
{
    public class CorrectionDebtInfoDO
    {
        public CorrectionDebtInfoDO()
        {
        }

        public CorrectionDebtInfoDO(CorrectionDebt correctionDebt)
        {
            this.CorrectionDebtId = correctionDebt.CorrectionDebtId;
            this.Status = correctionDebt.Status;
        }

        public int CorrectionDebtId { get; set; }

        public CorrectionDebtStatus Status { get; set; }
    }
}
