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

namespace R_10067
{
    public partial class FinanceSourceReportAmounts
    {
        public void Sum(R_10058.FinanceReportBudgetAmounts budgetAmounts)
        {
            if (budgetAmounts != null)
            {
                this.GrandAmount += budgetAmounts.GrandAmount;
                this.SelfAmount += budgetAmounts.SelfAmount;
                this.TotalAmount += budgetAmounts.TotalAmount;
            }
        }
    }
}
