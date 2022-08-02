using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractProgramme
    {
        public string programmeCode { get; set; }
        public string programmeName { get; set; }
        public string programmeNameAlt { get; set; }
        public List<ContractProgrammePriority> programmePriorities { get; set; }
        public List<ContractBudgetExpenseType> budgetExpenseTypes { get; set; }
        public List<ContractIndicator> indicators { get; set; }

        public void MapEuPercents(List<ContractEuPercentInfo> infos)
        {
            if(infos != null)
            {
                if(this.budgetExpenseTypes != null)
                {
                    for (int i = 0; i < this.budgetExpenseTypes.Count;i++ )
                    {
                        if (this.budgetExpenseTypes[i].expenses != null)
                        {
                            for (int j = 0; j < this.budgetExpenseTypes[i].expenses.Count; j++)
                            {
                                var info = infos.Single(e => e.gid.Equals(this.budgetExpenseTypes[i].expenses[j].gid));

                                this.budgetExpenseTypes[i].expenses[j].euPercent = info.euPercent;
                            }
                        }
                    }
                }
            }
        }
    }
}
