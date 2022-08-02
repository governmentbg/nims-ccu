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

namespace R_10010
{
    public partial class Budget : BaseApiSync
    {
        public void Load(List<ContractBudgetExpenseType> expenseTypes)
        {
            if (expenseTypes != null && expenseTypes.Count > 0)
            {
                #region Ordering

                expenseTypes = expenseTypes.OrderBy(e => e.orderNum).ToList();
                int expenseIndex = 1;
                for (int i = 0; i < expenseTypes.Count; i++)
                {
                    expenseTypes[i].orderNum = i + 1;
                    expenseTypes[i].expenses = expenseTypes[i].expenses.OrderBy(e => e.orderNum).ToList();

                    for (int j = 0; j < expenseTypes[i].expenses.Count; j++)
                    {
                        expenseTypes[i].expenses[j].orderNum = expenseIndex;
                        expenseIndex++;
                        expenseTypes[i].expenses[j].details = expenseTypes[i].expenses[j].details.OrderBy(e => e.orderNum).ToList();

                        for (int k = 0; k < expenseTypes[i].expenses[j].details.Count; k++)
                        {
                            expenseTypes[i].expenses[j].details[k].orderNum = k + 1;
                        }
                    }
                }

                #endregion

                foreach (var type in expenseTypes)
                {
                    R_10009.ProgrammeBudget programmeBudget = R_10009.ProgrammeBudget.Load(type);

                    var foundProgrammeBudget = this.ProgrammeBudgetCollection.FirstOrDefault(e => e.gid == programmeBudget.gid);

                    if (foundProgrammeBudget == null && !programmeBudget.IsDeactivated)
                    {
                        var current = new R_10009.ProgrammeBudget();
                        current.Load(programmeBudget);

                        this.ProgrammeBudgetCollection.Add(current);
                    }

                    if (foundProgrammeBudget != null)
                    {
                        foundProgrammeBudget.Load(programmeBudget);
                    }
                }
            }
        }

        public void Load(Budget budget)
        {
            if (budget == null || budget.ProgrammeBudgetCollection == null || this.ProgrammeBudgetCollection == null)
                throw new Exception("Missing elements in budget.");

            if (this.ProgrammeBudgetCollection.Count != budget.ProgrammeBudgetCollection.Count)
                throw new Exception("Different ProgrammeBudgetCollection.");

            for (int i = 0; i < this.ProgrammeBudgetCollection.Count; i++)
            {
                if (this.ProgrammeBudgetCollection[i].ProgrammeExpenseBudgetCollection.Count != budget.ProgrammeBudgetCollection[i].ProgrammeExpenseBudgetCollection.Count)
                    throw new Exception("Different ProgrammeExpenseBudgetCollection.");

                for (int j = 0; j < this.ProgrammeBudgetCollection[i].ProgrammeExpenseBudgetCollection.Count; j++)
                {
                    for (int k = 0; k < budget.ProgrammeBudgetCollection[i].ProgrammeExpenseBudgetCollection[j].ProgrammeDetailsExpenseBudgetCollection.Count; k++)
                    {
                        if (String.IsNullOrWhiteSpace(budget.ProgrammeBudgetCollection[i].ProgrammeExpenseBudgetCollection[j].ProgrammeDetailsExpenseBudgetCollection[k].gid))
                            budget.ProgrammeBudgetCollection[i].ProgrammeExpenseBudgetCollection[j].ProgrammeDetailsExpenseBudgetCollection[k].gid = Guid.NewGuid().ToString();
                    }

                    this.ProgrammeBudgetCollection[i].ProgrammeExpenseBudgetCollection[j].ProgrammeDetailsExpenseBudgetCollection =
                        budget.ProgrammeBudgetCollection[i].ProgrammeExpenseBudgetCollection[j].ProgrammeDetailsExpenseBudgetCollection;
                }
            }
        }

        [XmlIgnore]
        public bool HasMoreThanOneInterventionField { get; set; }
        [XmlIgnore]
        public bool HasMoreThanOneFormOfFinance { get; set; }
        [XmlIgnore]
        public bool HasMoreThanOneTerritorialDimension { get; set; }
        [XmlIgnore]
        public bool HasMoreThanOneTerritorialDeliveryMechanism { get; set; }
        [XmlIgnore]
        public bool HasMoreThanOneThematicObjective { get; set; }
        [XmlIgnore]
        public bool HasMoreThanOneESFSecondaryTheme { get; set; }
        [XmlIgnore]
        public bool HasMoreThanOneEconomicDimension { get; set; }
        [XmlIgnore]
        public bool HasMoreThanOneNutsAddress { get; set; }

        #region Totals

        [XmlIgnore]
        public string GrandDisplay
        {
            get
            {
                double result = 0.00;

                if (this.ProgrammeBudgetCollection != null)
                {
                    double current;
                    foreach (var expense in this.ProgrammeBudgetCollection)
                    {
                        if (Double.TryParse(expense.GrandDisplay, NumberStyles.Any, CultureInfo.InvariantCulture, out current))
                            result += current;
                    }
                }

                return DataUtils.DoubleToStringDecimalPoint(result);
            }
        }

        [XmlIgnore]
        public string SelfDisplay
        {
            get
            {
                double result = 0.00;

                if (this.ProgrammeBudgetCollection != null)
                {
                    double current;
                    foreach (var expense in this.ProgrammeBudgetCollection)
                    {
                        if (Double.TryParse(expense.SelfDisplay, NumberStyles.Any, CultureInfo.InvariantCulture, out current))
                            result += current;
                    }
                }

                return DataUtils.DoubleToStringDecimalPoint(result);
            }
        }

        [XmlIgnore]
        public string TotalDisplay
        {
            get
            {
                double result = 0.00;

                if (this.ProgrammeBudgetCollection != null)
                {
                    double current;
                    foreach (var expense in this.ProgrammeBudgetCollection)
                    {
                        if (Double.TryParse(expense.TotalDisplay, NumberStyles.Any, CultureInfo.InvariantCulture, out current))
                            result += current;
                    }
                }

                return DataUtils.DoubleToStringDecimalPoint(result);
            }
        }

        [XmlIgnore]
        public string GrandPercentageDisplay
        {
            get
            {
                if (this.ProgrammeBudgetCollection != null)
                {
                    double grandTotal = Double.Parse(this.GrandDisplay, CultureInfo.InvariantCulture);
                    double selfTotal = Double.Parse(this.SelfDisplay, CultureInfo.InvariantCulture);

                    if (grandTotal > 0 || selfTotal > 0)
                    {
                        double result = (grandTotal * 100) / (grandTotal + selfTotal);
                        return DataUtils.DoubleToStringDecimalPoint(result);
                    }
                }

                return String.Empty;
            }
        }

        [XmlIgnore]
        public string SelfPercentageDisplay
        {
            get
            {
                double perc;
                if (Double.TryParse(this.GrandPercentageDisplay, NumberStyles.Any, CultureInfo.InvariantCulture, out perc))
                {
                    double result = 100 - perc;
                    return DataUtils.DoubleToStringDecimalPoint(result);
                }
                else
                    return String.Empty;
            }
        }

        #endregion

        [XmlIgnore]
        public bool IsFinalRecipients { get; set; }

        [XmlIgnore]
        public bool IsFinancialIntermediaries { get; set; }
    }
}
