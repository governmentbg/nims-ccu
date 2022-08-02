using System;
using System.Linq;
using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.Irregularities
{
    public partial class Irregularity
    {
        #region Irregularity

        public void UpdateBasicData(
            string regNumber)
        {
            this.AssertIsNotRemoved();

            this.RegNumber = regNumber;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToRemoved(string deleteComment)
        {
            this.AssertIsEntered();

            this.Status = IrregularityStatus.Removed;
            this.DeleteNote = deleteComment;
            this.ModifyDate = DateTime.Now;
        }

        public void SetRegNumberPattern(string regNumberPattern)
        {
            this.AssertIsNew();

            this.RegNumberPattern = regNumberPattern;
            this.Status = IrregularityStatus.Entered;
        }

        public void SetFirstReportData(Year firstReportYear, Quarter firstReportQuarter)
        {
            this.AssertIsNotRemoved();

            this.FirstReportYear = firstReportYear;
            this.FirstReportQuarter = firstReportQuarter;
        }

        public void SetData(
            string regNumber,
            Year lastReportYear,
            Quarter lastReportQuarter,
            IrregularityCaseState? caseState,
            DateTime? irregularityEndDate)
        {
            this.AssertIsNotRemoved();

            this.RegNumber = regNumber;
            this.LastReportYear = lastReportYear;
            this.LastReportQuarter = lastReportQuarter;
            this.CaseState = caseState;
            this.IrregularityEndDate = irregularityEndDate;
        }

        private void AssertIsNew()
        {
            if (this.Status != IrregularityStatus.New)
            {
                throw new DomainValidationException("Irregularity status must be entered!");
            }
        }

        private void AssertIsEntered()
        {
            if (this.Status != IrregularityStatus.Entered)
            {
                throw new DomainValidationException("Irregularity status must be entered!");
            }
        }

        private void AssertIsNotRemoved()
        {
            if (this.Status == IrregularityStatus.Removed)
            {
                throw new DomainValidationException("Irregularity status must not be removed!");
            }
        }

        #endregion //Irregularity

        #region IrregularityFinancialCorrection
        public void AddFinancialCorrection(int financialCorrectionId)
        {
            this.AssertIsNotRemoved();

            this.FinancialCorrections.Add(new IrregularityFinancialCorrection
                {
                    IrregularityId = this.IrregularityId,
                    FinancialCorrectionId = financialCorrectionId,
                });

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveFinancialCorrection(int itemId)
        {
            this.AssertIsNotRemoved();

            var item = this.FinancialCorrections.Single(li => li.IrregularityFinancialCorrectionId == itemId);
            this.FinancialCorrections.Remove(item);

            this.ModifyDate = DateTime.Now;
        }
        #endregion //AuditLevelItem

        #region IrregularityDoc

        public void AddDocument(
            string description,
            string fileName,
            Guid fileKey)
        {
            this.AssertIsNotRemoved();

            this.Documents.Add(new IrregularityDoc()
            {
                IrregularityId = this.IrregularityId,
                FileName = fileName,
                Description = description,
                FileKey = fileKey,
            });

            this.ModifyDate = DateTime.Now;
        }

        public IrregularityDoc GetDocument(int documentId)
        {
            var document = this.Documents.Single(d => d.IrregularityDocId == documentId);

            if (document == null)
            {
                throw new DomainObjectNotFoundException("Cannot find IrregularityDoc with id " + documentId);
            }

            return document;
        }

        public void UpdateDocument(
            int documentId,
            string description,
            string fileName,
            Guid fileKey)
        {
            this.AssertIsNotRemoved();

            var document = this.GetDocument(documentId);
            document.SetAttributes(description, fileName, fileKey);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveDocument(int documentId)
        {
            this.AssertIsNotRemoved();

            var document = this.GetDocument(documentId);
            this.Documents.Remove(document);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //IrregularityDoc
    }
}
