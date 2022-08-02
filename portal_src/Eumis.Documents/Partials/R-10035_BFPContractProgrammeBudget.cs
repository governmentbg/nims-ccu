using Eumis.Common.Helpers;
using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10035
{
    public partial class BFPContractProgrammeBudget
    {

        public static BFPContractProgrammeBudget Load(R_10009.ProgrammeBudget initial)
        {
            BFPContractProgrammeBudget result = new BFPContractProgrammeBudget() { BFPContractProgrammeExpenseBudgetCollection = new BFPContractProgrammeExpenseBudgetCollection() };

            if (initial != null)
            {
                result.Name = initial.Name;
                result.OrderNum = initial.OrderNum;
                result.gid = initial.gid;

                if (initial.ProgrammeExpenseBudgetCollection != null && initial.ProgrammeExpenseBudgetCollection.Count > 0)
                {
                    foreach (var expense in initial.ProgrammeExpenseBudgetCollection)
                    {
                        R_10034.BFPContractProgrammeExpenseBudget programmeExpenseBudget = new R_10034.BFPContractProgrammeExpenseBudget() { BFPContractProgrammeDetailsExpenseBudgetCollection = new R_10034.BFPContractProgrammeDetailsExpenseBudgetCollection() };
                        programmeExpenseBudget.Name = expense.Name;
                        programmeExpenseBudget.OrderNum = expense.OrderNum;
                        programmeExpenseBudget.gid = expense.gid;
                        programmeExpenseBudget.ProgrammePriorityCode = expense.ProgrammePriorityCode;
                        programmeExpenseBudget.AidMode = expense.AidMode;

                        if (expense.ProgrammeDetailsExpenseBudgetCollection != null && expense.ProgrammeDetailsExpenseBudgetCollection.Count > 0)
                        {
                            foreach (var detail in expense.ProgrammeDetailsExpenseBudgetCollection)
                            {
                                R_10033.BFPContractProgrammeDetailsExpenseBudget programmeDetailsExpenseBudget = new R_10033.BFPContractProgrammeDetailsExpenseBudget();
                                programmeDetailsExpenseBudget.Name = detail.Name;
                                programmeDetailsExpenseBudget.OrderNum = detail.OrderNum;
                                programmeDetailsExpenseBudget.gid = detail.gid;
                                programmeDetailsExpenseBudget.Direction = detail.Direction;

                                programmeDetailsExpenseBudget.GrandAmount = detail.GrandAmount;
                                programmeDetailsExpenseBudget.SelfAmount = detail.SelfAmount;
                                programmeDetailsExpenseBudget.TotalAmount = detail.TotalAmount;

                                programmeDetailsExpenseBudget.isActive = true;
                                programmeDetailsExpenseBudget.isActivated = false;
                                if (detail.Nuts != null)
                                {
                                    programmeDetailsExpenseBudget.Nuts = detail.Nuts;
                                }

                                programmeExpenseBudget.BFPContractProgrammeDetailsExpenseBudgetCollection.Add(programmeDetailsExpenseBudget);
                            }
                        }

                        result.BFPContractProgrammeExpenseBudgetCollection.Add(programmeExpenseBudget);
                    }
                }
            }

            return result;
        }

        public static BFPContractProgrammeBudget Load(ContractBudgetExpenseType type)
        {
            BFPContractProgrammeBudget programmeBudget = new BFPContractProgrammeBudget() { BFPContractProgrammeExpenseBudgetCollection = new BFPContractProgrammeExpenseBudgetCollection() };

            if (type != null)
            {
                programmeBudget.Name = type.name;
                programmeBudget.OrderNum = type.orderNum.ToString();
                programmeBudget.gid = type.gid;
                programmeBudget.IsDeactivated = !type.isActive;

                if (type.expenses != null && type.expenses.Count > 0)
                {
                    foreach (var expense in type.expenses)
                    {
                        R_10034.BFPContractProgrammeExpenseBudget programmeExpenseBudget = new R_10034.BFPContractProgrammeExpenseBudget() { BFPContractProgrammeDetailsExpenseBudgetCollection = new R_10034.BFPContractProgrammeDetailsExpenseBudgetCollection() };
                        programmeExpenseBudget.Name = expense.name;
                        programmeExpenseBudget.OrderNum = expense.orderNum.ToString();
                        programmeExpenseBudget.gid = expense.gid;
                        programmeExpenseBudget.IsDeactivated = !expense.isActive;
                        programmeExpenseBudget.EuPercent = expense.euPercent;
                        programmeExpenseBudget.ProgrammePriorityCode = expense.programmePriorityCode;
                        programmeExpenseBudget.AidMode = new R_09991.EnumNomenclature(expense.aidMode);

                        if (expense.details != null && expense.details.Count > 0)
                        {
                            foreach (var detail in expense.details)
                            {
                                R_10033.BFPContractProgrammeDetailsExpenseBudget programmeDetailsExpenseBudget = new R_10033.BFPContractProgrammeDetailsExpenseBudget();
                                programmeDetailsExpenseBudget.Name = detail.note;
                                programmeDetailsExpenseBudget.OrderNum = detail.orderNum.ToString();
                                programmeDetailsExpenseBudget.gid = detail.gid;

                                programmeDetailsExpenseBudget.isActive = true;
                                programmeDetailsExpenseBudget.isActivated = false;

                                programmeExpenseBudget.BFPContractProgrammeDetailsExpenseBudgetCollection.Add(programmeDetailsExpenseBudget);
                            }
                        }

                        programmeBudget.BFPContractProgrammeExpenseBudgetCollection.Add(programmeExpenseBudget);
                    }
                }
            }

            return programmeBudget;
        }

        public void Load(BFPContractProgrammeBudget programmeBudget)
        {
            if (programmeBudget != null)
            {
                this.IsDeactivated = programmeBudget.IsDeactivated;
                this.Name = programmeBudget.Name;
                this.OrderNum = programmeBudget.OrderNum;
                this.gid = programmeBudget.gid;

                if (programmeBudget.BFPContractProgrammeExpenseBudgetCollection != null && programmeBudget.BFPContractProgrammeExpenseBudgetCollection.Count > 0)
                {
                    foreach (var expense in programmeBudget.BFPContractProgrammeExpenseBudgetCollection)
                    {
                        var foundExpenseBudget = this.BFPContractProgrammeExpenseBudgetCollection.FirstOrDefault(e => e.gid == expense.gid);

                        if (foundExpenseBudget == null && !expense.IsDeactivated)
                            this.BFPContractProgrammeExpenseBudgetCollection.Add(expense);

                        if (foundExpenseBudget != null)
                        {
                            foundExpenseBudget.IsDeactivated = expense.IsDeactivated;
                            foundExpenseBudget.AidMode = expense.AidMode;
                            foundExpenseBudget.EuPercent = expense.EuPercent;
                            foundExpenseBudget.Name = expense.Name;
                            foundExpenseBudget.ProgrammePriorityCode = expense.ProgrammePriorityCode;
                        }
                    }
                }
            }
        }

        [XmlIgnore]
        public bool IsDeactivated { get; set; }

        [XmlIgnore]
        public string EUPercentDisplay
        {
            get
            {
                if (this.BFPContractProgrammeExpenseBudgetCollection != null)
                {
                    double EUTotal = Convert.ToDouble(this.EUAmount);
                    double NationalTotal = Convert.ToDouble(this.NationalAmount);

                    if (EUTotal > 0 || NationalTotal > 0)
                    {
                        double result = (EUTotal * 100) / (EUTotal + NationalTotal);
                        return DataUtils.DoubleToStringDecimalPoint(result);
                    }
                }

                return String.Empty;
            }
        }

        [XmlIgnore]
        public string NationalPercentDisplay
        {
            get
            {
                double perc;
                if (Double.TryParse(this.EUPercentDisplay, NumberStyles.Any, CultureInfo.InvariantCulture, out perc))
                {
                    double result = 100 - perc;
                    return DataUtils.DoubleToStringDecimalPoint(result);
                }
                else
                    return String.Empty;
            }
        }

        [XmlIgnore]
        public string GrandPercentDisplay
        {
            get
            {
                if (this.BFPContractProgrammeExpenseBudgetCollection != null)
                {
                    double grandTotal = Convert.ToDouble(this.GrandAmount);
                    double selfTotal = Convert.ToDouble(this.SelfAmount);

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
        public string SelfPercentDisplay
        {
            get
            {
                double perc;
                if (Double.TryParse(this.GrandPercentDisplay, NumberStyles.Any, CultureInfo.InvariantCulture, out perc))
                {
                    double result = 100 - perc;
                    return DataUtils.DoubleToStringDecimalPoint(result);
                }
                else
                    return String.Empty;
            }
        }
    }
}
