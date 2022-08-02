using Eumis.Documents.Enums;
using System;
using System.Linq;
using System.Xml.Serialization;

namespace R_10066
{
    public partial class CostSupportingDocument
    {
        [XmlIgnore]
        public decimal TotalAmount
        {
            get { return this.FinanceReportBudgetItemDataCollection.Sum(i => i.TotalAmount); }
        }

        public void Load()
        {
            if (this.AttachedDocumentCollection == null || this.AttachedDocumentCollection.Count == 0)
                this.AttachedDocumentCollection = new R_10066.AttachedDocumentCollection()
                {
                    new R_10018.AttachedDocument()
                };
            for (int i = 0; i < this.AttachedDocumentCollection.Count; i++)
            {
                if (this.AttachedDocumentCollection[i].AttachedDocumentContent == null)
                    this.AttachedDocumentCollection[i].AttachedDocumentContent = new R_09992.AttachedDocumentContent();
            }

            if (this.FinanceReportBudgetItemDataCollection == null || this.FinanceReportBudgetItemDataCollection.Count == 0)
                this.FinanceReportBudgetItemDataCollection = new R_10066.FinanceReportBudgetItemDataCollection()
                {
                    new R_10065.FinanceReportBudgetItemData()
                };

            for (int i = 0; i < this.FinanceReportBudgetItemDataCollection.Count; i++)
            {
                var financeReportBudgetItem = this.FinanceReportBudgetItemDataCollection[i];

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
}
