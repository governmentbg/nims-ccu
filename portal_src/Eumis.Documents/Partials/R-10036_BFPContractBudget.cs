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

namespace R_10036
{
    public partial class BFPContractBudget : BaseApiSync
    {
        public static BFPContractBudget Load(R_10010.Budget initial)
        {
            BFPContractBudget result = new BFPContractBudget() { BFPContractProgrammeBudgetCollection = new BFPContractProgrammeBudgetCollection() };

            if (initial != null && initial.ProgrammeBudgetCollection != null && initial.ProgrammeBudgetCollection.Count > 0)
            {
                foreach (var type in initial.ProgrammeBudgetCollection)
                {
                    result.BFPContractProgrammeBudgetCollection.Add(R_10035.BFPContractProgrammeBudget.Load(type));
                }
            }

            return result;
        }

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
                    R_10035.BFPContractProgrammeBudget programmeBudget = R_10035.BFPContractProgrammeBudget.Load(type);

                    var foundProgrammeBudget = this.BFPContractProgrammeBudgetCollection.FirstOrDefault(e => e.gid == programmeBudget.gid);

                    if (foundProgrammeBudget == null && !programmeBudget.IsDeactivated)
                    {
                        var current = new R_10035.BFPContractProgrammeBudget();
                        current.Load(programmeBudget);

                        this.BFPContractProgrammeBudgetCollection.Add(current);
                    }

                    if (foundProgrammeBudget != null)
                    {
                        foundProgrammeBudget.Load(programmeBudget);
                    }
                }
            }
        }

        public void Load(BFPContractBudget budget, bool isInitial)
        {
            if (budget == null || budget.BFPContractProgrammeBudgetCollection == null || this.BFPContractProgrammeBudgetCollection == null)
                throw new Exception("Missing elements in budget.");

            if (this.BFPContractProgrammeBudgetCollection.Count != budget.BFPContractProgrammeBudgetCollection.Count)
                throw new Exception("Different ProgrammeBudgetCollection.");

            for (int i = 0; i < this.BFPContractProgrammeBudgetCollection.Count; i++)
            {
                if (this.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection.Count != budget.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection.Count)
                    throw new Exception("Different ProgrammeExpenseBudgetCollection.");

                for (int j = 0; j < this.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection.Count; j++)
                {
                    // Check if all activated elements are still existing
                    foreach (var activatedDetail in this.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].BFPContractProgrammeDetailsExpenseBudgetCollection.Where(e => e.isActivated))
                    {
                        var foundActivatedDetail = budget.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].BFPContractProgrammeDetailsExpenseBudgetCollection.FirstOrDefault(e => e.gid == activatedDetail.gid && e.isActivated);
                        if (foundActivatedDetail == null)
                            throw new Exception("Activated detail removed");
                    }

                    for (int k = 0; k < budget.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].BFPContractProgrammeDetailsExpenseBudgetCollection.Count; k++)
                    {
                        if (String.IsNullOrWhiteSpace(budget.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].BFPContractProgrammeDetailsExpenseBudgetCollection[k].gid))
                        {
                            throw new Exception("level3 does not have gid");
                        }

                        if (this.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].BFPContractProgrammeDetailsExpenseBudgetCollection != null)
                        {
                            if (!isInitial)
                            {
                                var foundLevel3 = this.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].BFPContractProgrammeDetailsExpenseBudgetCollection
                                    .FirstOrDefault(e => e.gid.Equals(budget.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].BFPContractProgrammeDetailsExpenseBudgetCollection[k].gid));
                                if (foundLevel3 != null)
                                {
                                    // Get contract amount from previous version, compare with current if it is necessary

                                    /*budget.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].BFPContractProgrammeDetailsExpenseBudgetCollection[k].GrandAmount =
                                        foundLevel3.GrandAmount;

                                    budget.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].BFPContractProgrammeDetailsExpenseBudgetCollection[k].SelfAmount = 
                                        foundLevel3.SelfAmount;

                                    budget.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].BFPContractProgrammeDetailsExpenseBudgetCollection[k].TotalAmount =
                                        foundLevel3.TotalAmount;*/

                                }
                                else
                                {
                                    // Set info for added elements

                                    // Set contract value to zero, if previous version of amounts are not found
                                    //budget.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].BFPContractProgrammeDetailsExpenseBudgetCollection[k].GrandAmount = 0.0m;
                                    //budget.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].BFPContractProgrammeDetailsExpenseBudgetCollection[k].SelfAmount = 0.0m;
                                }
                            }
                        }
                    }

                    var activatedGids = this.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].BFPContractProgrammeDetailsExpenseBudgetCollection.Where(e => e.isActivated).Select(e => e.gid);
                    if (budget.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].BFPContractProgrammeDetailsExpenseBudgetCollection.Any(e => !activatedGids.Contains(e.gid)
                        && (e.isActivated || !e.isActive)))
                        throw new Exception("Budget detail set to Activated or not active irregularly");

                    this.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].BFPContractProgrammeDetailsExpenseBudgetCollection =
                        budget.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].BFPContractProgrammeDetailsExpenseBudgetCollection;

                    this.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].EUAmount
                        = budget.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].EUAmount;

                    this.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].NationalAmount
                        = budget.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].NationalAmount;

                    this.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].GrandAmount
                        = budget.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].GrandAmount;

                    this.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].SelfAmount
                        = budget.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].SelfAmount;

                    this.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].TotalAmount
                        = budget.BFPContractProgrammeBudgetCollection[i].BFPContractProgrammeExpenseBudgetCollection[j].TotalAmount;
                }

                this.BFPContractProgrammeBudgetCollection[i].EUAmount
                    = budget.BFPContractProgrammeBudgetCollection[i].EUAmount;

                this.BFPContractProgrammeBudgetCollection[i].NationalAmount
                    = budget.BFPContractProgrammeBudgetCollection[i].NationalAmount;

                this.BFPContractProgrammeBudgetCollection[i].GrandAmount
                    = budget.BFPContractProgrammeBudgetCollection[i].GrandAmount;

                this.BFPContractProgrammeBudgetCollection[i].SelfAmount
                    = budget.BFPContractProgrammeBudgetCollection[i].SelfAmount;

                this.BFPContractProgrammeBudgetCollection[i].TotalAmount
                    = budget.BFPContractProgrammeBudgetCollection[i].TotalAmount;
            }

            this.EUAmount
                = budget.EUAmount;

            this.NationalAmount
                = budget.NationalAmount;

            this.GrandAmount
                = budget.GrandAmount;

            this.SelfAmount
                = budget.SelfAmount;

            this.TotalAmount
                = budget.TotalAmount;
        }

        public bool HasMoreThanOneNutsAddress { get; set; }

        [XmlIgnore]
        public bool IsInitial { get; set; }

        [XmlIgnore]
        public bool HasDirections { get; set; }
    }
}
