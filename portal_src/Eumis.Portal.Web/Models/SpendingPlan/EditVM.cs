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

namespace Eumis.Portal.Web.Models.SpendingPlan
{
    public class EditVM : BaseVM, IEditVM<R_10077.SpendingPlan>, IEngineValidatable, IRemoteValidatable
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public R_10076.SpendingBudget SpendingBudget { get; set; }

        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int StartQuarter { get; set; }
        public int EndQuarter { get; set; }

        public List<string> RemoteValidationErrors { get; set; }
        public List<string> RemoteValidationWarnings { get; set; }

        #region Get Set

        public EditVM() { }

        public EditVM(R_10077.SpendingPlan plan)
        {
            this.StartDate = plan.StartDate.Value;
            this.EndDate = plan.EndDate.Value;
            this.SpendingBudget = plan.SpendingBudget;

            this.StartYear = plan.StartYear;
            this.EndYear = plan.EndYear;
            this.StartQuarter = plan.StartQuarter;
            this.EndQuarter = plan.EndQuarter;
        }

        public R_10077.SpendingPlan Set(R_10077.SpendingPlan plan)
        {
            plan.SpendingBudget.TotalCalculatedAmount = this.SpendingBudget.TotalCalculatedAmount;

            for (int i = 0; i < plan.SpendingBudget.QuarterlyDistributionCollection.Count; i++)
            {
                plan.SpendingBudget.QuarterlyDistributionCollection[i].Q1Amount =
                    this.SpendingBudget.QuarterlyDistributionCollection[i].Q1Amount;

                plan.SpendingBudget.QuarterlyDistributionCollection[i].Q2Amount =
                    this.SpendingBudget.QuarterlyDistributionCollection[i].Q2Amount;

                plan.SpendingBudget.QuarterlyDistributionCollection[i].Q3Amount =
                    this.SpendingBudget.QuarterlyDistributionCollection[i].Q3Amount;

                plan.SpendingBudget.QuarterlyDistributionCollection[i].Q4Amount =
                    this.SpendingBudget.QuarterlyDistributionCollection[i].Q4Amount;
            }


            for (int i = 0; i < plan.SpendingBudget.SpendingBudgetLevel1Collection.Count; i++)
            {
                plan.SpendingBudget.SpendingBudgetLevel1Collection[i].TotalCalculatedAmount 
                    = this.SpendingBudget.SpendingBudgetLevel1Collection[i].TotalCalculatedAmount;

                for (int j = 0; j < plan.SpendingBudget.SpendingBudgetLevel1Collection[i].QuarterlyDistributionCollection.Count; j++)
                {
                        plan.SpendingBudget.SpendingBudgetLevel1Collection[i].QuarterlyDistributionCollection[j].Q1Amount =
                            this.SpendingBudget.SpendingBudgetLevel1Collection[i].QuarterlyDistributionCollection[j].Q1Amount;

                        plan.SpendingBudget.SpendingBudgetLevel1Collection[i].QuarterlyDistributionCollection[j].Q2Amount =
                            this.SpendingBudget.SpendingBudgetLevel1Collection[i].QuarterlyDistributionCollection[j].Q2Amount;

                        plan.SpendingBudget.SpendingBudgetLevel1Collection[i].QuarterlyDistributionCollection[j].Q3Amount =
                            this.SpendingBudget.SpendingBudgetLevel1Collection[i].QuarterlyDistributionCollection[j].Q3Amount;

                        plan.SpendingBudget.SpendingBudgetLevel1Collection[i].QuarterlyDistributionCollection[j].Q4Amount =
                            this.SpendingBudget.SpendingBudgetLevel1Collection[i].QuarterlyDistributionCollection[j].Q4Amount;
                }
            }

            return plan;
        }

        public R_10077.SpendingPlan SetAsync()
        {
            var plan = (R_10077.SpendingPlan)AppContext.Current.Document;

            return plan;
        }

        #endregion
    }
}