using Eumis.Common.Helpers;
using Eumis.Documents.Contracts;
using Eumis.Documents.Partials;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10062
{
    public partial class FinanceBudget : BaseApiSync
    {
        public static FinanceBudget Load(R_10036.BFPContractBudget initial)
        {
            FinanceBudget result = new FinanceBudget() { FinanceBudgetLevel1Collection = new FinanceBudgetLevel1Collection() };

            if (initial != null && initial.BFPContractProgrammeBudgetCollection != null && initial.BFPContractProgrammeBudgetCollection.Count > 0)
            {
                foreach (var level1 in initial.BFPContractProgrammeBudgetCollection)
                {
                    result.FinanceBudgetLevel1Collection.Add(R_10061.FinanceBudgetLevel1.Load(level1));
                }
            }

            return result;
        }

        public void LoadApprovedAmounts(List<ContractReportFinancialCSDBudgetItemPVO> approvedCumulativeCSDBudgetAmounts)
        {
            if (approvedCumulativeCSDBudgetAmounts != null && approvedCumulativeCSDBudgetAmounts.Count > 0)
            {
                if (this.FinanceBudgetLevel1Collection != null)
                {
                    for (int i = 0; i < this.FinanceBudgetLevel1Collection.Count; i++)
                    {
                        var level1 = this.FinanceBudgetLevel1Collection[i];
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

                                        ContractReportFinancialCSDBudgetItemPVO contractReportFinancialCSDBudgetItemPVO =
                                            approvedCumulativeCSDBudgetAmounts.FirstOrDefault(e => e.ContractBudgetLevel3AmountGid.ToString() == level3.gid);

                                        if (contractReportFinancialCSDBudgetItemPVO != null)
                                        {
                                            level3.GrandAmount = contractReportFinancialCSDBudgetItemPVO.ApprovedCumulativeBfpTotalAmount ?? 0;
                                            level3.SelfAmount = contractReportFinancialCSDBudgetItemPVO.ApprovedCumulativeSelfAmount ?? 0;
                                            level3.TotalAmount = contractReportFinancialCSDBudgetItemPVO.ApprovedCumulativeTotalAmount ?? 0;

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
