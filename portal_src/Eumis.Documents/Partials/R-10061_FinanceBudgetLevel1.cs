using Eumis.Common.Helpers;
using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10061
{
    public partial class FinanceBudgetLevel1
    {
        public static FinanceBudgetLevel1 Load(R_10035.BFPContractProgrammeBudget initial)
        {
            FinanceBudgetLevel1 result = new FinanceBudgetLevel1() { FinanceBudgetLevel2Collection = new FinanceBudgetLevel2Collection() };

            if (initial != null)
            {
                result.Name = initial.Name;
                result.OrderNum = initial.OrderNum;
                result.gid = initial.gid;

                if (initial.BFPContractProgrammeExpenseBudgetCollection != null && initial.BFPContractProgrammeExpenseBudgetCollection.Count > 0)
                {
                    foreach (var contractLevel2 in initial.BFPContractProgrammeExpenseBudgetCollection)
                    {
                        R_10060.FinanceBudgetLevel2 financeLevel2 = new R_10060.FinanceBudgetLevel2() { FinanceBudgetLevel3Collection = new R_10060.FinanceBudgetLevel3Collection() };
                        financeLevel2.Name = contractLevel2.Name;
                        financeLevel2.OrderNum = contractLevel2.OrderNum;
                        financeLevel2.gid = contractLevel2.gid;
                        financeLevel2.ProgrammePriorityCode = contractLevel2.ProgrammePriorityCode;
                        financeLevel2.AidMode = contractLevel2.AidMode;

                        financeLevel2.EuPercent = contractLevel2.EuPercent;

                        if (contractLevel2.BFPContractProgrammeDetailsExpenseBudgetCollection != null && contractLevel2.BFPContractProgrammeDetailsExpenseBudgetCollection.Count > 0)
                        {
                            foreach (var contractLevel3 in contractLevel2.BFPContractProgrammeDetailsExpenseBudgetCollection)
                            {
                                R_10059.FinanceBudgetLevel3 financeLevel3 = new R_10059.FinanceBudgetLevel3();

                                financeLevel3.Name = contractLevel3.Name;
                                financeLevel3.OrderNum = contractLevel3.OrderNum;
                                financeLevel3.gid = contractLevel3.gid;
                                financeLevel3.SelfAmount = contractLevel3.SelfAmount;
                                financeLevel3.GrandAmount = contractLevel3.GrandAmount;
                                financeLevel3.TotalAmount = contractLevel3.TotalAmount;
                               
                                financeLevel2.FinanceBudgetLevel3Collection.Add(financeLevel3);
                            }
                        }

                        result.FinanceBudgetLevel2Collection.Add(financeLevel2);
                    }
                }
            }

            return result;
        }

    }
}
