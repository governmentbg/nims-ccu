using Eumis.Common.Helpers;
using Eumis.Common.Localization;
using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10009
{
    public partial class ProgrammeBudget
    {

        public static ProgrammeBudget Load(ContractBudgetExpenseType type)
        {
            R_10009.ProgrammeBudget programmeBudget = new R_10009.ProgrammeBudget() { ProgrammeExpenseBudgetCollection = new R_10009.ProgrammeExpenseBudgetCollection() };

            if (type != null)
            {
                programmeBudget.Name = type.name;
                programmeBudget.NameEN = type.nameAlt;
                programmeBudget.OrderNum = type.orderNum.ToString();
                programmeBudget.gid = type.gid;
                programmeBudget.IsDeactivated = !type.isActive;

                if (type.expenses != null && type.expenses.Count > 0)
                {
                    foreach (var expense in type.expenses)
                    {
                        R_10008.ProgrammeExpenseBudget programmeExpenseBudget = new R_10008.ProgrammeExpenseBudget() { ProgrammeDetailsExpenseBudgetCollection = new R_10008.ProgrammeDetailsExpenseBudgetCollection() };
                        programmeExpenseBudget.Name = expense.name;
                        programmeExpenseBudget.NameEN = expense.nameAlt;
                        programmeExpenseBudget.OrderNum = expense.orderNum.ToString();
                        programmeExpenseBudget.gid = expense.gid;
                        programmeExpenseBudget.IsDeactivated = !expense.isActive;
                        programmeExpenseBudget.ProgrammePriorityCode = expense.programmePriorityCode;
                        programmeExpenseBudget.AidMode = new R_09991.EnumNomenclature(expense.aidMode);

                        if (expense.details != null && expense.details.Count > 0)
                        {
                            foreach (var detail in expense.details)
                            {
                                R_10007.ProgrammeDetailsExpenseBudget programmeDetailsExpenseBudget = new R_10007.ProgrammeDetailsExpenseBudget();
                                programmeDetailsExpenseBudget.Name = detail.note;
                                programmeDetailsExpenseBudget.OrderNum = detail.orderNum.ToString();
                                programmeDetailsExpenseBudget.gid = detail.gid;

                                programmeExpenseBudget.ProgrammeDetailsExpenseBudgetCollection.Add(programmeDetailsExpenseBudget);
                            }
                        }

                        programmeBudget.ProgrammeExpenseBudgetCollection.Add(programmeExpenseBudget);
                    }
                }
            }

            return programmeBudget;
        }

        public void Load(ProgrammeBudget programmeBudget)
        {
            if (programmeBudget != null)
            {
                this.IsDeactivated = programmeBudget.IsDeactivated;
                this.Name = programmeBudget.Name;
                this.NameEN = programmeBudget.NameEN;
                this.OrderNum = programmeBudget.OrderNum;
                this.gid = programmeBudget.gid;

                if (programmeBudget.ProgrammeExpenseBudgetCollection != null && programmeBudget.ProgrammeExpenseBudgetCollection.Count > 0)
                {
                    foreach (var expense in programmeBudget.ProgrammeExpenseBudgetCollection)
                    {
                        var foundExpenseBudget = this.ProgrammeExpenseBudgetCollection.FirstOrDefault(e => e.gid == expense.gid);

                        if (foundExpenseBudget == null && !expense.IsDeactivated)
                            this.ProgrammeExpenseBudgetCollection.Add(expense);

                        if (foundExpenseBudget != null)
                            foundExpenseBudget.IsDeactivated = expense.IsDeactivated;
                    }
                }
            }
        }

        [XmlIgnore]
        public bool IsDeactivated { get; set; }

        [XmlIgnore]
        public string GrandDisplay
        {
            get
            {
                double result = 0.00;

                if (this.ProgrammeExpenseBudgetCollection != null)
                {
                    double current;
                    foreach (var expense in this.ProgrammeExpenseBudgetCollection)
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

                if (this.ProgrammeExpenseBudgetCollection != null)
                {
                    double current;
                    foreach (var expense in this.ProgrammeExpenseBudgetCollection)
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

                if (this.ProgrammeExpenseBudgetCollection != null)
                {
                    double current;
                    foreach (var expense in this.ProgrammeExpenseBudgetCollection)
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
                if (this.ProgrammeExpenseBudgetCollection != null)
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

        [XmlIgnore]
        public string DisplayName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.Name, this.NameEN);
            }
        }
    }
}
