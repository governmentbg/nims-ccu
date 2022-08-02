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

namespace R_10068
{
    public partial class FinanceReportBudgetRowAmounts
    {
        public static FinanceReportBudgetRowAmounts Sum(List<FinanceReportBudgetRowAmounts> collection)
        {
            FinanceReportBudgetRowAmounts result = new FinanceReportBudgetRowAmounts()
                {
                    BFPContractAmounts = new R_10058.FinanceReportBudgetAmounts(),
                    CurrentReportAmounts = new R_10058.FinanceReportBudgetAmounts(),
                    CumulativeAmounts = new R_10058.FinanceReportBudgetAmounts()
                };

            if (collection != null)
            {
                foreach(var amount in collection)
                {
                    if (amount.BFPContractAmounts != null)
                    {
                        result.BFPContractAmounts.EUAmount += amount.BFPContractAmounts.EUAmount;
                        result.BFPContractAmounts.NationalAmount += amount.BFPContractAmounts.NationalAmount;

                        result.BFPContractAmounts.GrandAmount += amount.BFPContractAmounts.GrandAmount;
                        result.BFPContractAmounts.SelfAmount += amount.BFPContractAmounts.SelfAmount;
                        result.BFPContractAmounts.TotalAmount += amount.BFPContractAmounts.TotalAmount;
                    }

                    if (amount.CurrentReportAmounts != null)
                    {
                        result.CurrentReportAmounts.EUAmount += amount.CurrentReportAmounts.EUAmount;
                        result.CurrentReportAmounts.NationalAmount += amount.CurrentReportAmounts.NationalAmount;

                        result.CurrentReportAmounts.GrandAmount += amount.CurrentReportAmounts.GrandAmount;
                        result.CurrentReportAmounts.SelfAmount += amount.CurrentReportAmounts.SelfAmount;
                        result.CurrentReportAmounts.TotalAmount += amount.CurrentReportAmounts.TotalAmount;
                    }

                    if (amount.CumulativeAmounts != null)
                    {
                        result.CumulativeAmounts.EUAmount += amount.CumulativeAmounts.EUAmount;
                        result.CumulativeAmounts.NationalAmount += amount.CumulativeAmounts.NationalAmount;

                        result.CumulativeAmounts.GrandAmount += amount.CumulativeAmounts.GrandAmount;
                        result.CumulativeAmounts.SelfAmount += amount.CumulativeAmounts.SelfAmount;
                        result.CumulativeAmounts.TotalAmount += amount.CumulativeAmounts.TotalAmount;
                    }


                    result.DifferenceGrand += amount.DifferenceGrand;
                    result.DifferenceTotal += amount.DifferenceTotal;
                }
            }

            if (result.DifferenceGrand > 0 && result.BFPContractAmounts != null && result.BFPContractAmounts.GrandAmount > 0)
            {
                result.DifferenceGrandPercentage = (result.DifferenceGrand * 100) / result.BFPContractAmounts.GrandAmount;
                result.DifferenceGrandPercentage = Math.Round(result.DifferenceGrandPercentage, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                result.DifferenceGrandPercentage = 0;
            }

            if (result.DifferenceTotal > 0 && result.BFPContractAmounts != null && result.BFPContractAmounts.TotalAmount > 0)
            {
                result.DifferenceTotalPercentage = (result.DifferenceTotal * 100) / result.BFPContractAmounts.TotalAmount;
                result.DifferenceTotalPercentage = Math.Round(result.DifferenceTotalPercentage, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                result.DifferenceTotalPercentage = 0;
            }

            return result;
        }
    }
}
