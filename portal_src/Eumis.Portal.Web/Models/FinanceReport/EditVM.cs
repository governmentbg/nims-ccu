using System.Reflection;
using Eumis.Common.Validation;
using Eumis.Components.Web;
using Eumis.Documents.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eumis.Documents.Contracts;
using Eumis.Documents.Validation;
using Eumis.Portal.Web.Helpers;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Models.FinanceReport
{
    public class EditVM : BaseVM, IEditVM<R_10043.FinanceReport>, IEngineValidatable, IRemoteValidatable
    {
        public R_10078.FinanceReportBasicData BasicData { get; set; }

        public R_10062.FinanceBudget FinanceBudget { get; set; }

        public R_10043.CostSupportingDocuments CostSupportingDocuments { get; set; }

        public R_10000.PrivateNomenclature Beneficiary { get; set; }

        public List<R_10000.PrivateNomenclature> PartnerItemCollection { get; set; }

        public List<R_10000.PrivateNomenclature> ContractorItemCollection { get; set; }

        public Dictionary<string, List<R_10000.PrivateNomenclature>> ContractContractorItems { get; set; }

        public Dictionary<string, List<R_10043.ActivityBudgetDetailItem>> ActivityBudgetDetailItems { get; set; }

        public string contractGid { get; set; }
        public string packageGid { get; set; }

        public string contractNumber { get; set; }
        public string docNumber { get; set; }
        public string docSubNumber { get; set; }
        public DateTime activationDate { get; set; }

        public List<string> RemoteValidationErrors { get; set; }
        public List<string> RemoteValidationWarnings { get; set; }

        #region Get Set

        public EditVM() { }

        public EditVM(R_10043.FinanceReport financeReport)
        {
            this.BasicData = financeReport.BasicData;

            this.FinanceBudget = financeReport.FinanceBudget;
            this.CostSupportingDocuments = financeReport.CostSupportingDocuments;

            if(this.CostSupportingDocuments != null && this.CostSupportingDocuments.CostSupportingDocumentCollection != null)
            {
                for (int i = 0; i < this.CostSupportingDocuments.CostSupportingDocumentCollection.Count; i++)
                {
                    this.CostSupportingDocuments.CostSupportingDocumentCollection[i].Beneficiary = financeReport.Beneficiary;
                }
            }

            this.Beneficiary = financeReport.Beneficiary;
            this.PartnerItemCollection = financeReport.PartnerItemCollection;
            this.ContractorItemCollection = financeReport.ContractorItemCollection;
            this.ContractContractorItems = financeReport.ContractContractorItems;
            this.ActivityBudgetDetailItems = financeReport.ActivityBudgetDetailItems;

            this.contractGid = financeReport.contractGid;
            this.packageGid = financeReport.packageGid;

            this.contractNumber = financeReport.contractNumber;
            this.docNumber = financeReport.docNumber;
            this.docSubNumber = financeReport.docSubNumber;
            this.activationDate = financeReport.modificationDate;
        }

        public R_10043.FinanceReport Set(R_10043.FinanceReport financeReport)
        {
            return financeReport;
        }

        public R_10043.FinanceReport SetAsync()
        {
            var report = (R_10043.FinanceReport)AppContext.Current.Document;

            #region BasicData

            if (!(report.BasicData != null && report.BasicData.isLocked))
            {
                if (this.BasicData != null && report.BasicData != null)
                {
                    this.BasicData.id = report.BasicData.id;
                    this.BasicData.isLocked = report.BasicData.isLocked;
                }

                report.BasicData = this.BasicData;
            }

            #endregion

            #region CostSupportingDocumentCollection

            if (!(report.CostSupportingDocuments != null && report.CostSupportingDocuments.isLocked))
            {
                if (this.CostSupportingDocuments == null)
                    report.CostSupportingDocuments.CostSupportingDocumentCollection = new R_10043.CostSupportingDocumentCollection();
                else
                {
                    for (int i = 0; i < this.CostSupportingDocuments.CostSupportingDocumentCollection.Count; i++)
                    {
                        if (String.IsNullOrWhiteSpace(this.CostSupportingDocuments.CostSupportingDocumentCollection[i].gid))
                        {
                            this.CostSupportingDocuments.CostSupportingDocumentCollection[i].gid = Guid.NewGuid().ToString();
                        }

                        if(this.CostSupportingDocuments.CostSupportingDocumentCollection[i].CompanyType == R_09986.CompanyTypeNomenclature.Beneficiary)
                        {
                            this.CostSupportingDocuments.CostSupportingDocumentCollection[i].Beneficiary = report.Beneficiary;
                            this.CostSupportingDocuments.CostSupportingDocumentCollection[i].Partner = null;
                            this.CostSupportingDocuments.CostSupportingDocumentCollection[i].Contractor = null;
                            this.CostSupportingDocuments.CostSupportingDocumentCollection[i].ContractContractor = null;
                        }

                        if (this.CostSupportingDocuments.CostSupportingDocumentCollection[i].CompanyType == R_09986.CompanyTypeNomenclature.Partner)
                        {
                            this.CostSupportingDocuments.CostSupportingDocumentCollection[i].Beneficiary = null;
                            this.CostSupportingDocuments.CostSupportingDocumentCollection[i].Contractor = null;
                            this.CostSupportingDocuments.CostSupportingDocumentCollection[i].ContractContractor = null;
                        }

                        if (this.CostSupportingDocuments.CostSupportingDocumentCollection[i].CompanyType == R_09986.CompanyTypeNomenclature.Contractor)
                        {
                            this.CostSupportingDocuments.CostSupportingDocumentCollection[i].Beneficiary = null;
                            this.CostSupportingDocuments.CostSupportingDocumentCollection[i].Partner = null;
                        }
                    }

                    report.CostSupportingDocuments.CostSupportingDocumentCollection = this.CostSupportingDocuments.CostSupportingDocumentCollection;
                }
            }

            #endregion

            report.SetAllSections();

            return report;
        }

        #endregion
    }
}