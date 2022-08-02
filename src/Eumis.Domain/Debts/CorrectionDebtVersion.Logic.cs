using System;
using System.Collections.Generic;

namespace Eumis.Domain.Debts
{
    public partial class CorrectionDebtVersion
    {
        public void UpdateAttributes(
            decimal? debtEuAmount,
            decimal? debtBgAmount,
            decimal? certEuAmount,
            decimal? certBgAmount,
            decimal? reimbursedEuAmount,
            decimal? reimbursedBgAmount,
            string createNotes)
        {
            this.DebtEuAmount = debtEuAmount;
            this.DebtBgAmount = debtBgAmount;
            this.DebtBfpAmount = debtEuAmount.HasValue || debtBgAmount.HasValue ?
                (debtEuAmount ?? 0) + (debtBgAmount ?? 0) : (decimal?)null;

            this.CertEuAmount = certEuAmount;
            this.CertBgAmount = certBgAmount;
            this.CertBfpAmount = certEuAmount.HasValue || certBgAmount.HasValue ?
                (certEuAmount ?? 0) + (certBgAmount ?? 0) : (decimal?)null;

            this.ReimbursedEuAmount = reimbursedEuAmount;
            this.ReimbursedBgAmount = reimbursedBgAmount;
            this.ReimbursedBfpAmount = reimbursedEuAmount.HasValue || reimbursedBgAmount.HasValue ?
                (reimbursedEuAmount ?? 0) + (reimbursedBgAmount ?? 0) : (decimal?)null;

            this.CreateNotes = createNotes;

            this.ModifyDate = DateTime.Now;
        }

        public IList<string> CanChangeStatusToActual()
        {
            var errors = new List<string>();

            if (!this.DebtEuAmount.HasValue)
            {
                errors.Add("Полето 'Дължима сума - ЕС' трябва да е попълнено");
            }

            if (!this.DebtBgAmount.HasValue)
            {
                errors.Add("Полето 'Дължима сума - НФ' трябва да е попълнено");
            }

            if (!this.CertEuAmount.HasValue)
            {
                errors.Add("Полето 'Сертифицирана сума - ЕС' трябва да е попълнено");
            }

            if (!this.CertBgAmount.HasValue)
            {
                errors.Add("Полето 'Сертифицирана сума - НФ' трябва да е попълнено");
            }

            if (!this.ReimbursedEuAmount.HasValue)
            {
                errors.Add("Полето 'Възстановена сума - ЕС' трябва да е попълнено");
            }

            if (!this.ReimbursedBgAmount.HasValue)
            {
                errors.Add("Полето 'Възстановена сума - НФ' трябва да е попълнено");
            }

            return errors;
        }
    }
}
