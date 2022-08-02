//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Eumis.Common.Validation;
using Eumis.Documents;
using Eumis.Documents.Contracts;
using Eumis.Documents.Enums;
using Eumis.Documents.Interfaces;
using Eumis.Documents.Validation;
using R_10018;

namespace R_10043
{
    public partial class FinanceReport : IDocumentNomenclatures, IEumisDocument, IEumisDocumentWithFiles, ILocalValidatable
    {
        string IEumisDocument.Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        DateTime IEumisDocument.CreateDate
        {
            get
            {
                return this.createDate;
            }
            set
            {
                this.createDate = value;
            }
        }

        DateTime IEumisDocument.ModificationDate
        {
            get
            {
                return this.modificationDate;
            }
            set
            {
                this.modificationDate = value;
            }
        }

        public IEnumerable<AttachedDocument> Files
        {
            get
            {
                return this.GetFiles(d => d.CostSupportingDocuments.CostSupportingDocumentCollection, csd => csd.AttachedDocumentCollection);
            }
        }

        public static FinanceReport Init(string packageGid, 
            string contractGid, 
            string contractNumber, 
            string docNumber, 
            string docSubNumber,
            R_10040.BFPContract contract, 
            R_10041.Procurements procurements, 
            R_10045.PaymentRequest request,
            FinanceReport lastReport,
            string procedureTypeAlias,
            List<ContractReportFinancialCSDBudgetItemPVO> approvedCumulativeCSDBudgetAmounts)
        {
            FinanceReport report = new FinanceReport();

            report.id = Guid.NewGuid().ToString();
            report.createDate = DateTime.Now;

            report.packageGid = packageGid;
            report.contractGid = contractGid;

            report.contractNumber = contractNumber;
            report.docNumber = docNumber;
            report.docSubNumber = docSubNumber;

            #region BasicData

            report.BasicData = new R_10078.FinanceReportBasicData();
            report.BasicData.id = Guid.NewGuid().ToString();

            report.BasicData.ProcedureTypeAlias = procedureTypeAlias;

            #endregion

            #region Budget

            // Get data from contract
            if (contract != null && contract.BFPContractDirectionsBudgetContract != null)
            {
                report.FinanceBudget = R_10062.FinanceBudget.Load(contract.BFPContractDirectionsBudgetContract.BFPContractBudget);

                if (contract.BFPContractDirectionsBudgetContract.Contract != null)
                    report.FinanceBudget.BFPContractCrossFinancingAmount = contract.BFPContractDirectionsBudgetContract.Contract.RequestedFundingPartOfCrossFinancingAmount;
            }

            #endregion

            #region CostSupportingDocuments

            report.CostSupportingDocuments = new CostSupportingDocuments();
            report.CostSupportingDocuments.CostSupportingDocumentCollection = new CostSupportingDocumentCollection();
            report.CostSupportingDocuments.id = Guid.NewGuid().ToString();

            #endregion

            #region Nomenclatures

            // Load from BFP Contract

            #region Beneficiary

            if (contract != null && contract.Beneficiary != null)
            {
                report.Beneficiary = new R_10000.PrivateNomenclature()
                {
                    Id = contract.Beneficiary.id,
                    Name = contract.Beneficiary.Uin + ' ' + contract.Beneficiary.Name
                };
            }

            #endregion

            #region Partners

            if (contract != null && contract.Partners != null && contract.Partners.PartnerCollection != null)
            {
                report.PartnerItemCollection = new PrivateNomenclatureCollection();
                report.PartnerItemCollection.AddRange(
                    contract.Partners.PartnerCollection.Select(e => new R_10000.PrivateNomenclature
                    {
                        Id = e.gid,
                        Name = e.Uin + ' ' + e.Name
                    })
                );
            }

            #endregion

            #region Add all items with empty contractor id

            report.ActivityBudgetDetailDataCollection = new ActivityBudgetDetailDataCollection();

            var fullItem = new ActivityBudgetDetailData()
            {
                ContractContractorId = "",
                ActivityBudgetDetailPairCollection = new ActivityBudgetDetailPairCollection()
            };

            List<R_10000.PrivateNomenclature> contractActivityItemCollection = new List<R_10000.PrivateNomenclature>();
            List<R_10000.PrivateNomenclature> budgetLevel3ItemCollection = new List<R_10000.PrivateNomenclature>();

            if (contract != null)
            {
                if (contract.BFPContractContractActivities != null && contract.BFPContractContractActivities.BFPContractContractActivityCollection != null)
                {
                    foreach (var activity in contract.BFPContractContractActivities.BFPContractContractActivityCollection)
                    {
                        if (activity.isActive)
                            contractActivityItemCollection.Add(new R_10000.PrivateNomenclature() { Id = activity.gid, Name = activity.Code });
                    }
                }

                if (contract.BFPContractDirectionsBudgetContract != null
                    && contract.BFPContractDirectionsBudgetContract.BFPContractBudget != null
                    && contract.BFPContractDirectionsBudgetContract.BFPContractBudget.BFPContractProgrammeBudgetCollection != null)
                {
                    for (int i = 0; i < contract.BFPContractDirectionsBudgetContract.BFPContractBudget.BFPContractProgrammeBudgetCollection.Count; i++)
                    {
                        var level1 = contract.BFPContractDirectionsBudgetContract.BFPContractBudget.BFPContractProgrammeBudgetCollection[i];
                        if (level1.BFPContractProgrammeExpenseBudgetCollection != null)
                        {
                            for (int j = 0; j < level1.BFPContractProgrammeExpenseBudgetCollection.Count; j++)
                            {
                                var level2 = level1.BFPContractProgrammeExpenseBudgetCollection[j];
                                if (level2.BFPContractProgrammeDetailsExpenseBudgetCollection != null)
                                {
                                    for (int k = 0; k < level2.BFPContractProgrammeDetailsExpenseBudgetCollection.Count; k++)
                                    {
                                        var level3 = level2.BFPContractProgrammeDetailsExpenseBudgetCollection[k];

                                        if (level3.isActive)
                                            budgetLevel3ItemCollection.Add(new R_10000.PrivateNomenclature() { Id = level3.gid, Name = R_10033.BFPContractProgrammeDetailsExpenseBudget.GetEnumText(level1, level2, level3) });
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (contractActivityItemCollection != null && budgetLevel3ItemCollection != null)
            {
                bool hasActivites = contractActivityItemCollection.Count > 0;

                foreach (var level3 in budgetLevel3ItemCollection)
                {
                    if (hasActivites)
                    {
                        foreach (var activity in contractActivityItemCollection)
                        {
                            fullItem.ActivityBudgetDetailPairCollection.Add(
                                new ActivityBudgetDetailPair()
                                {
                                    BudgetDetail = level3,
                                    ContractActivity = activity
                                });
                        }
                    }
                    else
                    {
                        fullItem.ActivityBudgetDetailPairCollection.Add(
                            new ActivityBudgetDetailPair()
                            {
                                BudgetDetail = level3,
                                ContractActivity = new R_10000.PrivateNomenclature()
                            });
                    }
                }
            }

            report.ActivityBudgetDetailDataCollection.Add(fullItem);

            #endregion

            #endregion

            report.Load(procurements, request, lastReport, approvedCumulativeCSDBudgetAmounts);

            return report;
        }

        public void Load( 
            R_10041.Procurements procurements,
            R_10045.PaymentRequest request,
            FinanceReport lastReport,
            List<ContractReportFinancialCSDBudgetItemPVO> approvedCumulativeCSDBudgetAmounts)
        {
            // Get data from last finance report
            if (lastReport != null && this.FinanceBudget != null)
                this.FinanceBudget.LoadApprovedAmounts(approvedCumulativeCSDBudgetAmounts);


            // Load from procurements
            if (procurements != null && procurements.ContractContractors != null && procurements.ContractContractors.ContractContractorCollection != null)
            {
                #region Contractors

                this.ContractorItemCollection = new PrivateNomenclatureCollection();
                this.ContractorItemCollection.AddRange(
                    procurements.Contractors.ContractorCollection.Select(e => new R_10000.PrivateNomenclature
                    {
                        Id = e.gid,
                        Name = e.Uin + ' ' + e.Name
                    })
                );

                #endregion

                #region ContactContractors

                this.ContractContractorDataCollection = new ContractContractorDataCollection();

                foreach (var contractor in this.ContractorItemCollection)
                {
                    var contracts = procurements.ContractContractors.ContractContractorCollection.Where(e => e.Contractor.Id.Equals(contractor.Id));

                    var newItem = new ContractContractorData();
                    newItem.ContractorId = contractor.Id;
                    newItem.ContractContractorItemCollection = new PrivateNomenclatureCollection();

                    if (contracts != null)
                    {
                        newItem.ContractContractorItemCollection.AddRange(contracts.Select(e => new R_10000.PrivateNomenclature
                        {
                            Id = e.gid,
                            Name = e.NomenclatureName
                        }));
                    }

                    this.ContractContractorDataCollection.Add(newItem);
                }

                #endregion

                #region Activity Budget

                if (this.ActivityBudgetDetailDataCollection == null)
                    this.ActivityBudgetDetailDataCollection = new ActivityBudgetDetailDataCollection();
                else
                {
                    this.ActivityBudgetDetailDataCollection.RemoveAll(e => !String.IsNullOrWhiteSpace(e.ContractContractorId));
                }

                foreach (var contractContractor in procurements.ContractContractors.ContractContractorCollection)
                {
                    var newItem = new ActivityBudgetDetailData()
                    {
                        ContractContractorId = contractContractor.gid,
                        ActivityBudgetDetailPairCollection = new ActivityBudgetDetailPairCollection()
                    };

                    if (contractContractor.ActivitiesBudgetDetailsRefCollection != null)
                    {
                        var references = contractContractor.ActivitiesBudgetDetailsRefCollection
                            .OrderBy(e =>
                                procurements.BudgetLevel3ItemCollection.IndexOf
                                    (procurements.BudgetLevel3ItemCollection.FirstOrDefault(inner => inner.Id.Equals(e.BudgetDetail.Id)))
                                );

                        foreach (var reference in references)
                        {
                            newItem.ActivityBudgetDetailPairCollection.Add(new ActivityBudgetDetailPair()
                            {
                                BudgetDetail = reference.BudgetDetail,
                                ContractActivity = reference.ContractActivity
                            });
                        }
                    }

                    this.ActivityBudgetDetailDataCollection.Add(newItem);
                }

                #endregion
            }

            this.SetAllSections();

            this.Load();
        }

        private void Load()
        {
            #region CostSupportingDocuments

            // Init
            if (this.CostSupportingDocuments == null)
                this.CostSupportingDocuments = new CostSupportingDocuments();
            if (this.CostSupportingDocuments.CostSupportingDocumentCollection == null)
                this.CostSupportingDocuments.CostSupportingDocumentCollection = new CostSupportingDocumentCollection();
            if (String.IsNullOrWhiteSpace(this.CostSupportingDocuments.id))
                this.CostSupportingDocuments.id = Guid.NewGuid().ToString();

            for (int i = 0; i < this.CostSupportingDocuments.CostSupportingDocumentCollection.Count; i++)
            {
                if (this.CostSupportingDocuments.CostSupportingDocumentCollection[i].FinanceReportBudgetItemDataCollection == null
                    || this.CostSupportingDocuments.CostSupportingDocumentCollection[i].FinanceReportBudgetItemDataCollection.Count == 0)
                {
                    this.CostSupportingDocuments.CostSupportingDocumentCollection[i].FinanceReportBudgetItemDataCollection = new R_10066.FinanceReportBudgetItemDataCollection()
                    {
                        new R_10065.FinanceReportBudgetItemData()
                    };

                    for (int j = 0; j < this.CostSupportingDocuments.CostSupportingDocumentCollection[i].FinanceReportBudgetItemDataCollection.Count; j++)
                    {
                        var financeReportBudgetItem = this.CostSupportingDocuments.CostSupportingDocumentCollection[i].FinanceReportBudgetItemDataCollection[j];

                        if (financeReportBudgetItem.CrossFinancing == null || String.IsNullOrWhiteSpace(financeReportBudgetItem.CrossFinancing.Value))
                            financeReportBudgetItem.CrossFinancing = new R_09991.EnumNomenclature
                            {
                                Value = YesNoNotApplicableNomenclature.No.Id,
                                Description = YesNoNotApplicableNomenclature.No.Name
                            };

                        if (financeReportBudgetItem.InsideEU == null || String.IsNullOrWhiteSpace(financeReportBudgetItem.InsideEU.Value))
                            financeReportBudgetItem.InsideEU = new R_09991.EnumNomenclature
                            {
                                Value = YesNoNotApplicableNomenclature.No.Id,
                                Description = YesNoNotApplicableNomenclature.No.Name
                            };

                        if (financeReportBudgetItem.OutsideEU == null || String.IsNullOrWhiteSpace(financeReportBudgetItem.OutsideEU.Value))
                            financeReportBudgetItem.OutsideEU = new R_09991.EnumNomenclature
                            {
                                Value = YesNoNotApplicableNomenclature.No.Id,
                                Description = YesNoNotApplicableNomenclature.No.Name
                            };

                        if (financeReportBudgetItem.OutsideEUInProgrammingAreaEFRR == null || String.IsNullOrWhiteSpace(financeReportBudgetItem.OutsideEUInProgrammingAreaEFRR.Value))
                            financeReportBudgetItem.OutsideEUInProgrammingAreaEFRR = new R_09991.EnumNomenclature
                            {
                                Value = YesNoNotApplicableNomenclature.No.Id,
                                Description = YesNoNotApplicableNomenclature.No.Name
                            };

                        if (financeReportBudgetItem.ThirdCountriesEFRR == null || String.IsNullOrWhiteSpace(financeReportBudgetItem.ThirdCountriesEFRR.Value))
                            financeReportBudgetItem.ThirdCountriesEFRR = new R_09991.EnumNomenclature
                            {
                                Value = YesNoNotApplicableNomenclature.No.Id,
                                Description = YesNoNotApplicableNomenclature.No.Name
                            };

                        if (financeReportBudgetItem.AdvancePayment == null || String.IsNullOrWhiteSpace(financeReportBudgetItem.AdvancePayment.Value))
                            financeReportBudgetItem.AdvancePayment = new R_09991.EnumNomenclature
                            {
                                Value = YesNoNotApplicableNomenclature.No.Id,
                                Description = YesNoNotApplicableNomenclature.No.Name
                            };

                        if (financeReportBudgetItem.ContributionNature == null || String.IsNullOrWhiteSpace(financeReportBudgetItem.ContributionNature.Value))
                            financeReportBudgetItem.ContributionNature = new R_09991.EnumNomenclature
                            {
                                Value = YesNoNotApplicableNomenclature.No.Id,
                                Description = YesNoNotApplicableNomenclature.No.Name
                            };
                    }
                }
            }

            #endregion
        }

        public static void LoadNomenclatures(ref R_10043.FinanceReport financeReport, IList<ContractReportDocument> procedureContractReportFinancialDocuments)
        {
            #region Nomenclatures

            financeReport.Nomenclatures = new Dictionary<Eumis.Documents.Mappers.NomenclatureType, List<Eumis.Documents.Mappers.Nomenclature>>();

            financeReport.Nomenclatures.Add(Eumis.Documents.Mappers.NomenclatureType.ContractReportDocumentType, procedureContractReportFinancialDocuments.Where(e => e.isActive).Select(e => new Eumis.Documents.Mappers.Nomenclature(e)).ToList());

            if (financeReport.Nomenclatures[Eumis.Documents.Mappers.NomenclatureType.ContractReportDocumentType] != null)
            {
                financeReport.RequiredDocumentsCodesNames = financeReport
                    .Nomenclatures[Eumis.Documents.Mappers.NomenclatureType.ContractReportDocumentType]
                    .Where(e => e.IsRequired.HasValue && e.IsRequired.Value)
                    .Select(e => new Tuple<string, string>(e.Value, e.Name))
                    .ToList();
            }

            financeReport.DocumentsExtensions = new Dictionary<string, string>();

            foreach (var document in procedureContractReportFinancialDocuments.Where(reportDocument => reportDocument.isActive))
            {
                if (!string.IsNullOrWhiteSpace(document.extension) && !financeReport.DocumentsExtensions.ContainsKey(document.gid))
                {
                    financeReport.DocumentsExtensions[document.gid] = document.extension;
                }
            }

            #endregion
        }

        public void Copy(FinanceReport originFinancialReport)
        {
            #region BasicData

            BasicData.StartDate = originFinancialReport.BasicData.StartDate;
            BasicData.EndDate = originFinancialReport.BasicData.EndDate;

            #endregion

            #region CostSupportingDocument

            CostSupportingDocuments = originFinancialReport.CostSupportingDocuments;
            CostSupportingDocuments.id = Guid.NewGuid().ToString();

            if (CostSupportingDocuments?.CostSupportingDocumentCollection == null)
            {
                return;
            }

            foreach (var csd in CostSupportingDocuments.CostSupportingDocumentCollection)
            {
                string contractContractorId = "";
                
                if (csd.IsLocked)
                {
                    csd.IsLocked = false;
                }

                if (csd.IsLocked)
                {
                    csd.IsLocked = false;
                }

                switch (csd.CompanyType)
                {
                    case R_09986.CompanyTypeNomenclature.Beneficiary:
                        break;
                    case R_09986.CompanyTypeNomenclature.Partner:
                        if (!PartnerItemCollection.Where(x => x.Id == csd.Partner.Id).Any())
                        {
                            csd.Partner = null;
                        }
                        break;
                    case R_09986.CompanyTypeNomenclature.Contractor:
                        var contractorId = csd.Contractor?.Id;
                        contractContractorId = csd.ContractContractor?.Id;

                        var contractContractors = ContractContractorDataCollection
                            .Where(x => x.ContractorId == contractorId)
                            .Select(x => x.ContractContractorItemCollection)
                            .FirstOrDefault();

                        if (contractContractors == null)
                        {
                            csd.Contractor = null;
                            csd.ContractContractor = null;
                        }
                        else if (!contractContractors.Where(x => x.Id == contractContractorId).Any())
                        {
                            csd.ContractContractor = null;
                        }
                        break;
                    default:
                        break;
                }

                var budgetActivities = ActivityBudgetDetailDataCollection
                    .Where(x => x.ContractContractorId == contractContractorId)
                    .Select(x => x.ActivityBudgetDetailPairCollection)
                    .FirstOrDefault();

                if (csd.FinanceReportBudgetItemDataCollection != null)
                {
                    foreach (var budgetItem in csd.FinanceReportBudgetItemDataCollection)
                    {
                        if (budgetActivities == null)
                        {
                            budgetItem.BudgetDetail = null;
                            budgetItem.ContractActivity = null;
                        }
                        else if ((!budgetActivities.Where(x => x.BudgetDetail.Id == budgetItem.BudgetDetail?.Id && x.ContractActivity.Id == budgetItem.ContractActivity?.Id).Any()))
                        {
                            if (!budgetActivities.Where(x => x.BudgetDetail.Id == budgetItem.BudgetDetail?.Id).Any())
                            {
                                budgetItem.BudgetDetail = null;
                            }

                            budgetItem.ContractActivity = null;
                        }
                    }
                }

                foreach (var attachedDocument in csd.AttachedDocumentCollection)
                {
                    attachedDocument.VersionNum = this.docNumber;
                    attachedDocument.VersionSubNum = this.docSubNumber;
                    attachedDocument.ActivationDate = null;
                }
            }

            #endregion

        }

        public void SetAllSections()
        {
            this.CalculateCurrentDatas();
            this.SumBudgetLevels();
            this.SetFinanceSources();
        }

        // sets data from cost financial documents to budget
        private void CalculateCurrentDatas()
        {
            if (this.FinanceBudget != null && this.FinanceBudget.FinanceBudgetLevel1Collection != null)
            {
                for (int i = 0; i < this.FinanceBudget.FinanceBudgetLevel1Collection.Count; i++)
                {
                    var level1 = this.FinanceBudget.FinanceBudgetLevel1Collection[i];

                    if (level1.FinanceBudgetLevel2Collection != null)
                    {
                        for (int j = 0; j < level1.FinanceBudgetLevel2Collection.Count; j++)
                        {
                            var level2 = level1.FinanceBudgetLevel2Collection[j];

                            if (level2.FinanceBudgetLevel3Collection != null)
                            {
                                for (int k = 0; k < level2.FinanceBudgetLevel3Collection.Count; k++)
                                {
                                    var level3 = level2.FinanceBudgetLevel3Collection[k];

                                    var documentAmounts = this.GetAmountsForLevel3(level3.gid);

                                    level3.GrandAmount = documentAmounts.GrandAmount;
                                    level3.SelfAmount = documentAmounts.SelfAmount;
                                    level3.TotalAmount = documentAmounts.TotalAmount;
                                }
                            }
                        }
                    }
                }
            }
        }

        // sums level2, level1, totals amounts
        private void SumBudgetLevels()
        {
            if (this.FinanceBudget != null)
            {
                this.FinanceBudget.Amounts = new R_10068.FinanceReportBudgetRowAmounts();
                if (this.FinanceBudget.FinanceBudgetLevel1Collection != null)
                {
                    for (int i = 0; i < this.FinanceBudget.FinanceBudgetLevel1Collection.Count; i++)
                    {
                        var level1 = this.FinanceBudget.FinanceBudgetLevel1Collection[i];
                        level1.Amounts = new R_10068.FinanceReportBudgetRowAmounts();

                        if (level1.FinanceBudgetLevel2Collection != null)
                        {
                            for (int j = 0; j < level1.FinanceBudgetLevel2Collection.Count; j++)
                            {
                                var level2 = level1.FinanceBudgetLevel2Collection[j];
                                level2.Amounts = new R_10068.FinanceReportBudgetRowAmounts();
                            }

                            level1.Amounts = R_10068.FinanceReportBudgetRowAmounts.Sum(level1.FinanceBudgetLevel2Collection.Select(e => e.Amounts).ToList());
                        }
                    }
                    this.FinanceBudget.Amounts = R_10068.FinanceReportBudgetRowAmounts.Sum(this.FinanceBudget.FinanceBudgetLevel1Collection.Select(e => e.Amounts).ToList());
                }
            }
        }

        // sets FinancesSources from budget
        private void SetFinanceSources()
        {
            var bfpContractTotalAmount = this.FinanceBudget.Amounts.BFPContractAmounts.TotalAmount;
            var currentReportTotalAmount = this.FinanceBudget.Amounts.CurrentReportAmounts.TotalAmount;
            var cumulativeTotalAmount = this.FinanceBudget.Amounts.CumulativeAmounts.TotalAmount;

            #region Get from level2-s and cross financing

            List<string> crossFinancingGids = new List<string>();
            foreach (var item in this.GetBudgetItemDatas())
            {
                if (IsTrue(item.CrossFinancing) && item.BudgetDetail != null)
                {
                    crossFinancingGids.Add(item.BudgetDetail.Id);
                }
            }

            if (this.FinanceBudget.FinanceBudgetLevel1Collection != null)
            {
                for (int i = 0; i < this.FinanceBudget.FinanceBudgetLevel1Collection.Count; i++)
                {
                    var level1 = this.FinanceBudget.FinanceBudgetLevel1Collection[i];

                    if (level1.FinanceBudgetLevel2Collection != null)
                    {
                        for (int j = 0; j < level1.FinanceBudgetLevel2Collection.Count; j++)
                        {
                            var level2 = level1.FinanceBudgetLevel2Collection[j];

                            R_10063.FinanceSourceAmounts selectedByFinanceSource = null;
                            R_10063.FinanceSourceAmounts selectedByAidMode = null;

                            if (level2.FinanceBudgetLevel3Collection != null)
                            {
                                if (selectedByFinanceSource != null)
                                    selectedByFinanceSource.Sum(level2.Amounts.BFPContractAmounts.EUAmount
                                        , level2.Amounts.CurrentReportAmounts.EUAmount
                                        , level2.Amounts.CumulativeAmounts.EUAmount
                                        , bfpContractTotalAmount
                                        , currentReportTotalAmount
                                        , cumulativeTotalAmount);

                                if (selectedByAidMode != null)
                                    selectedByAidMode.Sum(level2.Amounts.BFPContractAmounts.GrandAmount
                                        , level2.Amounts.CurrentReportAmounts.GrandAmount
                                        , level2.Amounts.CumulativeAmounts.GrandAmount
                                        , bfpContractTotalAmount
                                        , currentReportTotalAmount
                                        , cumulativeTotalAmount);
                            }
                        }
                    }
                }
            }

            #endregion

        }

        [XmlIgnore]
        public IList<string> CanEnterErrors { get; set; }

        [XmlIgnore]
        public List<ModelValidationResultExtended> LocalValidationErrors { get; set; }

        [XmlIgnore]
        public Dictionary<Eumis.Documents.Mappers.NomenclatureType, List<Eumis.Documents.Mappers.Nomenclature>> Nomenclatures { get; set; }

        [XmlIgnore]
        public List<Tuple<string, string>> RequiredDocumentsCodesNames { get; set; }

        [XmlIgnore]
        public Dictionary<string, string> DocumentsExtensions { get; set; }

        [XmlIgnore]
        public List<string> RemoteValidationErrors { get; set; }

        [XmlIgnore]
        public List<string> RemoteValidationWarnings { get; set; }

        [XmlIgnore]
        public Dictionary<string, List<R_10000.PrivateNomenclature>> ContractContractorItems
        {
            get
            {
                Dictionary<string, List<R_10000.PrivateNomenclature>> result = new Dictionary<string, List<R_10000.PrivateNomenclature>>();

                if (this.ContractContractorDataCollection != null && this.ContractContractorDataCollection.Count > 0)
                {
                    foreach (var contractor in this.ContractContractorDataCollection)
                    {
                        var key = contractor.ContractorId;
                        result.Add(key, new List<R_10000.PrivateNomenclature>());

                        if (contractor.ContractContractorItemCollection != null)
                        {
                            result[key].AddRange(contractor.ContractContractorItemCollection);
                        }
                    }
                }

                return result;
            }
        }

        [XmlIgnore]
        public Dictionary<string, List<ActivityBudgetDetailItem>> ActivityBudgetDetailItems
        {
            get
            {
                Dictionary<string, List<ActivityBudgetDetailItem>> result = new Dictionary<string, List<ActivityBudgetDetailItem>>();

                if (this.ActivityBudgetDetailDataCollection != null && this.ActivityBudgetDetailDataCollection.Count > 0)
                {
                    foreach (var contractContractor in this.ActivityBudgetDetailDataCollection)
                    {
                        var key = contractContractor.ContractContractorId;
                        result.Add(key, new List<ActivityBudgetDetailItem>());

                        if (contractContractor.ActivityBudgetDetailPairCollection != null)
                        {
                            foreach (var reference in contractContractor.ActivityBudgetDetailPairCollection)
                            {
                                result[key].Add(new ActivityBudgetDetailItem()
                                {
                                    BudgetDetail = reference.BudgetDetail,
                                    ContractActivity = reference.ContractActivity
                                });
                            }
                        }
                    }
                }

                return result;
            }
        }

        #region GetAmounts

        private List<R_10065.FinanceReportBudgetItemData> GetBudgetItemDatas()
        {
            List<R_10065.FinanceReportBudgetItemData> result = new List<R_10065.FinanceReportBudgetItemData>();

            if (this.CostSupportingDocuments != null && this.CostSupportingDocuments.CostSupportingDocumentCollection != null)
            {
                foreach (var doc in this.CostSupportingDocuments.CostSupportingDocumentCollection)
                {
                    if (doc.FinanceReportBudgetItemDataCollection != null)
                    {
                        foreach (var item in doc.FinanceReportBudgetItemDataCollection)
                        {
                            result.Add(item);
                        }
                    }
                }
            }

            return result;
        }

        private R_10067.FinanceSourceReportAmounts GetAmountsForLevel3(string level3Gid)
        {
            R_10067.FinanceSourceReportAmounts result = new R_10067.FinanceSourceReportAmounts();

            if (!String.IsNullOrWhiteSpace(level3Gid))
            {
                foreach (var item in this.GetBudgetItemDatas())
                {
                    if (item.BudgetDetail != null && level3Gid.Equals(item.BudgetDetail.Id))
                    {
                        result.GrandAmount += item.GrandAmount;
                        result.SelfAmount += item.SelfAmount;
                        result.TotalAmount += item.TotalAmount;
                    }
                }
            }

            return result;
        }

        private R_10067.FinanceSourceReportAmounts GetInsideEUAmounts()
        {
            R_10067.FinanceSourceReportAmounts result = new R_10067.FinanceSourceReportAmounts();

            foreach (var item in this.GetBudgetItemDatas())
            {
                if (IsTrue(item.InsideEU))
                {
                    result.GrandAmount += item.GrandAmount;
                    result.SelfAmount += item.SelfAmount;
                    result.TotalAmount += item.TotalAmount;
                }
            }

            return result;
        }

        private R_10067.FinanceSourceReportAmounts GetOutsideEUAmounts()
        {
            R_10067.FinanceSourceReportAmounts result = new R_10067.FinanceSourceReportAmounts();

            foreach (var item in this.GetBudgetItemDatas())
            {
                if (IsTrue(item.OutsideEU))
                {
                    result.GrandAmount += item.GrandAmount;
                    result.SelfAmount += item.SelfAmount;
                    result.TotalAmount += item.TotalAmount;
                }
            }

            return result;
        }

        private R_10067.FinanceSourceReportAmounts GetOutsideEUInProgrammingAreaEFRRAmounts()
        {
            R_10067.FinanceSourceReportAmounts result = new R_10067.FinanceSourceReportAmounts();

            foreach (var item in this.GetBudgetItemDatas())
            {
                if (IsTrue(item.OutsideEUInProgrammingAreaEFRR))
                {
                    result.GrandAmount += item.GrandAmount;
                    result.SelfAmount += item.SelfAmount;
                    result.TotalAmount += item.TotalAmount;
                }
            }

            return result;
        }

        private R_10067.FinanceSourceReportAmounts GetThirdCountriesEFRRAmounts()
        {
            R_10067.FinanceSourceReportAmounts result = new R_10067.FinanceSourceReportAmounts();

            foreach (var item in this.GetBudgetItemDatas())
            {
                if (IsTrue(item.ThirdCountriesEFRR))
                {
                    result.GrandAmount += item.GrandAmount;
                    result.SelfAmount += item.SelfAmount;
                    result.TotalAmount += item.TotalAmount;
                }
            }

            return result;
        }

        private R_10067.FinanceSourceReportAmounts GetAdvancePaymentAmounts()
        {
            R_10067.FinanceSourceReportAmounts result = new R_10067.FinanceSourceReportAmounts();

            foreach (var item in this.GetBudgetItemDatas())
            {
                if (IsTrue(item.AdvancePayment))
                {
                    result.GrandAmount += item.GrandAmount;
                    result.SelfAmount += item.SelfAmount;
                    result.TotalAmount += item.TotalAmount;
                }
            }

            return result;
        }

        private R_10067.FinanceSourceReportAmounts GetContributionNatureAmounts()
        {
            R_10067.FinanceSourceReportAmounts result = new R_10067.FinanceSourceReportAmounts();

            foreach (var item in this.GetBudgetItemDatas())
            {
                if (IsTrue(item.ContributionNature))
                {
                    result.GrandAmount += item.GrandAmount;
                    result.SelfAmount += item.SelfAmount;
                    result.TotalAmount += item.TotalAmount;
                }
            }

            return result;
        }

        private bool IsTrue(R_09991.EnumNomenclature nom)
        {
            return nom != null && YesNoNotApplicableNomenclature.Yes.Id.Equals(nom.Value);
        }

        #endregion
    }

    [Serializable]
    public class ActivityBudgetDetailItem
    {
        [XmlIgnore]
        public R_10000.PrivateNomenclature ContractActivity { get; set; }

        [XmlIgnore]
        public R_10000.PrivateNomenclature BudgetDetail { get; set; }
    }
}
