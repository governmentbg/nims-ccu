using System;
using System.Collections.Generic;

namespace Eumis.Domain.Debts
{
    public partial class ContractDebtVersion
    {
        public void UpdateAttributes(
            decimal? euAmount,
            decimal? bgAmount,
            decimal? totalAmount,
            ContractDebtCertStatus? certStatus,
            decimal? certEuAmount,
            decimal? certBgAmount,
            decimal? certTotalAmount,
            string createNotes,
            ContractDebtExecutionStatus? executionStatus)
        {
            this.AssertIsDraft();

            this.EuAmount = euAmount;
            this.BgAmount = bgAmount;
            this.TotalAmount = totalAmount;
            this.CertStatus = certStatus;
            if (certStatus == ContractDebtCertStatus.Partial)
            {
                this.CertEuAmount = certEuAmount;
                this.CertBgAmount = certBgAmount;
                this.CertTotalAmount = certTotalAmount;
            }
            else if (certStatus == ContractDebtCertStatus.Yes)
            {
                this.CertEuAmount = euAmount;
                this.CertBgAmount = bgAmount;
                this.CertTotalAmount = totalAmount;
            }
            else if (certStatus == ContractDebtCertStatus.No)
            {
                this.CertEuAmount = 0m;
                this.CertBgAmount = 0m;
                this.CertTotalAmount = 0m;
            }

            this.CreateNotes = createNotes;
            this.ExecutionStatus = executionStatus;

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateAttributesPartial(
            decimal? euAmount,
            decimal? bgAmount,
            decimal? totalAmount,
            ContractDebtCertStatus? certStatus,
            decimal? certEuAmount,
            decimal? certBgAmount,
            decimal? certTotalAmount,
            string createNotes,
            DateTime? modifyDate)
        {
            this.AssertIsNotDraft();

            this.EuAmount = euAmount;
            this.BgAmount = bgAmount;
            this.TotalAmount = totalAmount;
            this.CertStatus = certStatus;
            if (certStatus == ContractDebtCertStatus.Partial)
            {
                this.CertEuAmount = certEuAmount;
                this.CertBgAmount = certBgAmount;
                this.CertTotalAmount = certTotalAmount;
            }
            else if (certStatus == ContractDebtCertStatus.Yes)
            {
                this.CertEuAmount = euAmount;
                this.CertBgAmount = bgAmount;
                this.CertTotalAmount = totalAmount;
            }
            else if (certStatus == ContractDebtCertStatus.No)
            {
                this.CertEuAmount = 0m;
                this.CertBgAmount = 0m;
                this.CertTotalAmount = 0m;
            }

            this.CreateNotes = createNotes;
            this.ModifyDate = modifyDate ?? DateTime.Now;
        }

        public IList<string> CanChangeStatusToActual()
        {
            var errors = new List<string>();

            if (this.EuAmount.HasValue && this.BgAmount.HasValue && this.TotalAmount.HasValue)
            {
                if (this.BgAmount.Value + this.EuAmount.Value != this.TotalAmount.Value)
                {
                    errors.Add("Сумата на главницата по ЕС + НФ трябва да е равна на общото.");
                }
            }
            else
            {
                if (!this.EuAmount.HasValue)
                {
                    errors.Add("Полето 'БФП - ЕС' трябва да е попълнено");
                }

                if (!this.BgAmount.HasValue)
                {
                    errors.Add("Полето 'БФП - НФ' трябва да е попълнено");
                }

                if (!this.TotalAmount.HasValue)
                {
                    errors.Add("Полето 'Обща сума' трябва да е попълнено");
                }
            }

            if (this.CertBgAmount.HasValue && this.CertEuAmount.HasValue && this.CertTotalAmount.HasValue)
            {
                if (this.CertBgAmount.Value + this.CertEuAmount.Value != this.CertTotalAmount.Value)
                {
                    errors.Add("Сумата на сертифицираната част по ЕС + НФ трябва да е равна на общото.");
                }
            }
            else
            {
                // when CertStatus is Yes/No the values are filled automatically by the domain
                errors.Add("Когато дългът е частично сертифициран полетата за сертифицираната част трябва да са попълнени.");
            }

            if (!this.CertStatus.HasValue)
            {
                errors.Add("Полето 'Сертифициран' трябва да е попълнено");
            }

            if (!this.ExecutionStatus.HasValue)
            {
                errors.Add("Полето 'Статус на изпълнение' трябва да е попълнено");
            }

            return errors;
        }

        private void AssertIsDraft()
        {
            if (this.Status != ContractDebtVersionStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractDebtVersion when not in 'Draft' status");
            }
        }

        private void AssertIsNotDraft()
        {
            if (this.Status == ContractDebtVersionStatus.Draft)
            {
                throw new DomainException("ContractDebtVersion is in 'Draft' status");
            }
        }
    }
}
