using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.MapNodes.DataObjects;

namespace Eumis.Domain.OperationalMap.ProgrammePriorities
{
    public partial class ProgrammePriority
    {
        #region ProgrammePriority

        public void UpdateProgrammePriority(
            string name,
            string nameAlt,
            string description,
            string descriptionAlt,
            ProgrammePriorityType programmePriorityType,
            int companyId,
            int? higherOrderCompanyId)
        {
            this.AssertIsNotCanceled();

            this.Name = name;
            this.NameAlt = nameAlt;
            this.Description = description;
            this.DescriptionAlt = descriptionAlt;

            this.SetCompanyData(programmePriorityType, companyId, higherOrderCompanyId);

            this.ModifyDate = DateTime.Now;
        }

        #endregion // ProgrammePriority

        #region MapNodeRelation

        public void AddProgrammeRelation(int programmeId)
        {
            this.AssertIsNotCanceled();

            this.MapNodeRelation = new MapNodeRelation
            {
                MapNodeId = this.MapNodeId,
                ParentMapNodeId = programmeId,
                ProgrammeId = programmeId,
                ProgrammePriorityId = this.MapNodeId,
            };
        }

        #endregion //MapNodeRelation

        #region ProgrammePriorityBudget

        public IList<MapNodeBudget> FindProgrammePriorityBudgetPeriod(int budgetPeriodId)
        {
            var budgets = this.ProgrammePriorityBudgets.Where(ppi => ppi.BudgetPeriodId == budgetPeriodId).ToList();

            if (budgets.Count == 0)
            {
                throw new DomainObjectNotFoundException(string.Format(
                    "Cannot find ProgrammePriorityBudgets with ProgrammePriorityId {0} and BudgetPeriodId {1}",
                    this.MapNodeId,
                    budgetPeriodId));
            }

            return budgets;
        }

        public void AddProgrammePriorityBudgetPeriod(
            int budgetPeriodId,
            ProgrammePriorityBudgetDO budget)
        {
            this.AssertIsNotCanceled();

            this.AddProgrammePriorityBudget(
                budgetPeriodId,
                budget.BgAmount.Value);

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateProgrammePriorityBudgetPeriod(
            int budgetPeriodId,
            decimal? bgAmount,
            bool budgetIsActive)
        {
            this.AssertIsNotCanceled();
            var ppBudget = this.ProgrammePriorityBudgets
                    .Where(ppi => ppi.BudgetPeriodId == budgetPeriodId)
                    .SingleOrDefault();

            if (ppBudget != null)
            {
                if (budgetIsActive)
                {
                    ppBudget.BgAmount = bgAmount ?? 0;
                }
                else
                {
                    this.ProgrammePriorityBudgets.Remove(ppBudget);
                }
            }
            else if (budgetIsActive)
            {
                this.AddProgrammePriorityBudget(
                    budgetPeriodId,
                    bgAmount ?? 0);
            }

            this.ModifyDate = DateTime.Now;
        }

        private void AddProgrammePriorityBudget(
            int budgetPeriodId,
            decimal bgAmount)
        {
            this.ProgrammePriorityBudgets.Add(new MapNodeBudget()
            {
                MapNodeId = this.MapNodeId,
                BudgetPeriodId = budgetPeriodId,
                ProgrammeId = this.MapNodeRelation.ProgrammeId.Value,
                BgAmount = bgAmount,
            });
        }

        public void RemoveProgrammePriorityBudgetPeriod(int budgetPeriodId)
        {
            this.AssertIsNotCanceled();

            var budgets = this.FindProgrammePriorityBudgetPeriod(budgetPeriodId).ToList();
            foreach (var budget in budgets)
            {
                this.ProgrammePriorityBudgets.Remove(budget);
            }

            this.ModifyDate = DateTime.Now;
        }

        private MapNodeBudget FindProgrammePriorityBudget(int budgetPeriodId)
        {
            var budget = this.ProgrammePriorityBudgets.Where(ppi => ppi.BudgetPeriodId == budgetPeriodId).SingleOrDefault();

            if (budget == null)
            {
                throw new DomainObjectNotFoundException(
                    string.Format(
                        "Cannot find ProgrammePriorityBudget with ProgrammePriorityId {0}, BudgetPeriodId {1}",
                        this.MapNodeId,
                        budgetPeriodId));
            }

            return budget;
        }

        #endregion // ProgrammePriorityBudget

        private void SetCompanyData(ProgrammePriorityType programmePriorityType, int companyId, int? higherOrderCompanyId)
        {
            this.CompanyData = new ProgrammePriorityCompany()
            {
                CompanyId = companyId,
                ProgrammePriorityType = programmePriorityType,
                HigherOrderCompanyId = higherOrderCompanyId,
            };
        }
    }
}
