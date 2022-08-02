using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.Core;
using Eumis.Domain.Events;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.NotificationEvents;
using Eumis.Rio;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportFinancial
    {
        public override void SetXml(string xml)
        {
            base.SetXml(xml);

            var financialDoc = this.GetDocument();
            this.StartDate = financialDoc.BasicData.StartDate;
            this.EndDate = financialDoc.BasicData.EndDate;

            this.PaymentsFinalRecipientsAmount = 0;
            this.CommitmentsGuaranteeAmount = 0;
            this.ExpenseManagementAmount = 0;
            this.ManagementFeesAmount = 0;
            this.CorrespondingPublicSpendingAmount = 0;

            this.ModifyDate = DateTime.Now;
        }

        public void ChangeCSDPortalAccessibility(
            ContractReportFinancialCSDBudgetItemStatus status,
            Guid budgetDetailGid,
            Guid? contractActivityGid,
            decimal totalAmount,
            DateTime date,
            string number,
            Guid companyGid)
        {
            bool isLocked = false;

            switch (status)
            {
                case ContractReportFinancialCSDBudgetItemStatus.Draft:
                    isLocked = false;
                    break;
                case ContractReportFinancialCSDBudgetItemStatus.Ended:
                    isLocked = true;
                    break;
                default:
                    break;
            }

            this.SetDocumentAttributes(this.GetDocument(), isLocked, budgetDetailGid, contractActivityGid, totalAmount, date, number, companyGid);
        }

        private void SetDocumentAttributes(
            FinanceReport financeReport,
            bool isLocked,
            Guid budgetDetailGid,
            Guid? contractActivityGid,
            decimal totalAmount,
            DateTime date,
            string number,
            Guid companyGid)
        {
            financeReport.CostSupportingDocuments.CostSupportingDocumentCollection.Single(csd =>
                csd.Number == number &&
                csd.Date == date &&
                this.GetCompanyId(csd) == companyGid.ToString().ToLower() &&
                    csd.FinanceReportBudgetItemDataCollection.Any(bi =>
                        bi.BudgetDetail.Id.ToLower() == budgetDetailGid.ToString().ToLower() &&
                        bi.TotalAmount == totalAmount &&
                        (bi.ContractActivity != null ? (string.IsNullOrEmpty(bi.ContractActivity.Id) ? null : bi.ContractActivity.Id.ToLower()) : null) == contractActivityGid?.ToString().ToLower()))
                        .IsLocked = isLocked;

            financeReport.modificationDate = DateTime.Now;

            this.SetXml(financeReport);
        }

        public bool IsFinancialCSDToDelete(DateTime date, string number, Guid companyGid, List<Tuple<string, string, decimal>> budgetItemsData)
        {
            var financeReport = this.GetDocument();

            return financeReport.CostSupportingDocuments
                .CostSupportingDocumentCollection
                .Any(csd =>
                csd.Date == date &&
                csd.Number == number &&
                this.GetCompanyId(csd) == companyGid.ToString().ToLower() &&
                csd.FinanceReportBudgetItemDataCollection.All(bi => budgetItemsData.Any(i =>
                    i.Item1.ToLower() == bi.BudgetDetail.Id.ToLower() &&
                    i.Item2?.ToLower() == (bi.ContractActivity != null ? (string.IsNullOrEmpty(bi.ContractActivity.Id) ? null : bi.ContractActivity.Id.ToLower()) : null) &&
                    i.Item3 == bi.TotalAmount)) &&
                csd.IsLocked == false);
        }

        private string GetCompanyId(CostSupportingDocument csd)
        {
            string companyId = null;

            switch (csd.CompanyType)
            {
                case CompanyTypeNomenclature.Beneficiary:
                    companyId = csd.Beneficiary.Id;
                    break;
                case CompanyTypeNomenclature.Partner:
                    companyId = csd.Partner.Id;
                    break;
                case CompanyTypeNomenclature.Contractor:
                    companyId = csd.Contractor.Id;
                    break;
            }

            return companyId.ToLower();
        }

        public void SetAttachedDocumentsActivationDate()
        {
            var financeReport = this.GetDocument();

            var activationDate = DateTime.Now;

            foreach (var csd in financeReport.CostSupportingDocuments.CostSupportingDocumentCollection.Where(csd => !csd.IsLocked))
            {
                foreach (var document in csd.AttachedDocumentCollection.Where(d => d.VersionNum == financeReport.docNumber && d.VersionSubNum == financeReport.docSubNumber))
                {
                    document.ActivationDate = activationDate;
                }
            }

            this.SetXml(financeReport);
        }

        public void ChangeStatus(ContractReportFinancialStatus status, string note = null)
        {
            this.Status = status;
            this.StatusNote = note;
            this.ModifyDate = DateTime.Now;

            var eventType = this.GetNotificationEventType();
            if (eventType.HasValue)
            {
                ((INotificationEventEmitter)this).NotificationEvents.Add(new ContractNotificationEvent(
                    eventType.Value,
                    this.ContractReportFinancialId,
                    this.ContractReportId,
                    this.ContractId));
            }

            if (this.Status == ContractReportFinancialStatus.Returned)
            {
                ((IEventEmitter)this).Events.Add(new ContractReportReturnedDocumentEvent()
                {
                    ContractReportDocumentType = ContractReportDocumentType.ContractReportFinancial,
                    ContractReportId = this.ContractReportId,
                    VersionNum = this.VersionNum,
                    VersionSubNum = this.VersionSubNum,
                });
            }
        }

        private NotificationEventType? GetNotificationEventType()
        {
            switch (this.Status)
            {
                case ContractReportFinancialStatus.Returned:
                    return NotificationEventType.ContractReportFinancialToReturned;
                case ContractReportFinancialStatus.Actual:
                    if (this.VersionSubNum > 1)
                    {
                        return NotificationEventType.ContractReportFinancialToResent;
                    }

                    break;
                default:
                    break;
            }

            return null;
        }
    }
}
