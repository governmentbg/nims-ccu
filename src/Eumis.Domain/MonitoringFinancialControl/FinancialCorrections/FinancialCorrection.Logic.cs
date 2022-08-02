using System;

namespace Eumis.Domain.MonitoringFinancialControl.FinancialCorrections
{
    public partial class FinancialCorrection
    {
        public void UpdateAttributes(DateTime impositionDate)
        {
            this.AssertIsNotRemoved();

            this.ImpositionDate = impositionDate;

            this.ModifyDate = DateTime.Now;
        }

        public void MakeEntered()
        {
            this.AssertIsNew();
            this.Status = FinancialCorrectionStatus.Entered;
        }

        public void CancelFinancialCorrection(string deleteNote)
        {
            this.AssertIsEntered();

            this.Status = FinancialCorrectionStatus.Removed;
            this.DeleteNote = deleteNote;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToEntered()
        {
            this.AssertIsRemoved();

            this.Status = FinancialCorrectionStatus.Entered;
            this.DeleteNote = null;
            this.ModifyDate = DateTime.Now;
        }

        private void AssertIsNew()
        {
            if (this.Status != FinancialCorrectionStatus.New)
            {
                throw new DomainValidationException("FinancialCorrection must be in 'New' status.");
            }
        }

        private void AssertIsEntered()
        {
            if (this.Status != FinancialCorrectionStatus.Entered)
            {
                throw new DomainValidationException("FinancialCorrection must be in 'Entered' status.");
            }
        }

        private void AssertIsNotRemoved()
        {
            if (this.Status == FinancialCorrectionStatus.Removed)
            {
                throw new DomainValidationException("FinancialCorrection must not be in 'Removed' status.");
            }
        }

        private void AssertIsRemoved()
        {
            if (this.Status != FinancialCorrectionStatus.Removed)
            {
                throw new DomainValidationException("FinancialCorrection must be in 'Removed' status.");
            }
        }
    }
}
