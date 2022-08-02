using System.Collections.Generic;
using System.Linq;
using Eumis.Public.Common.Helpers;

namespace Eumis.Public.Data.UmisVOs
{
    public class OperationalProgramTotalsVO
    {
        public OperationalProgramTotalsVO()
        {
        }

        public OperationalProgramTotalsVO(IEnumerable<ProgrammeBudgetDetailedVO> operationalPrograms)
        {
            this.BudgetTotalSum = operationalPrograms.Sum(m => m.BudgetTotal);
            this.BudgetEUSum = operationalPrograms.Sum(m => m.BudgetEU);
            this.BudgetNationalSum = operationalPrograms.Sum(m => m.BudgetNational);
            this.ProjectsCountSum = operationalPrograms.Sum(m => m.ProjectsCount);
            this.ContractsCountSum = operationalPrograms.Sum(m => m.ContractsCount);
            this.ContractTotalSum = operationalPrograms.Sum(m => m.ContractTotal);
            this.ContractBFPSum = operationalPrograms.Sum(m => m.ContractBFP);
            this.ContractEUSum = operationalPrograms.Sum(m => m.ContractEU);
            this.PaidBFPSum = operationalPrograms.Sum(m => m.PaidBFP);
            this.PaidEUSum = operationalPrograms.Sum(m => m.PaidEU);
            this.PaidECSum = operationalPrograms.Sum(p => p.ReceivedTotal);
        }

        public decimal BudgetTotalSum { get; set; }

        public decimal BudgetEUSum { get; set; }

        public decimal BudgetNationalSum { get; set; }

        public int ProjectsCountSum { get; set; }

        public int ContractsCountSum { get; set; }

        public decimal ContractTotalSum { get; set; }

        public decimal ContractBFPSum { get; set; }

        public decimal ContractSumPercent
        {
            get
            {
                if (this.BudgetTotalSum != 0)
                {
                    return DataUtils.Percent(this.ContractBFPSum / this.BudgetTotalSum);
                }

                return default(decimal);
            }
        }

        public decimal ContractEUSum { get; set; }

        public decimal PaidBFPSum { get; set; }

        public decimal PaidSumPercent
        {
            get
            {
                if (this.BudgetTotalSum != 0)
                {
                    return DataUtils.Percent(this.PaidBFPSum / this.BudgetTotalSum);
                }

                return default(decimal);
            }
        }

        public decimal PaidEUSum { get; set; }

        public decimal PaidECSum { get; set; }

        public decimal PaidECSumPercent
        {
            get
            {
                if (this.BudgetEUSum != 0)
                {
                    return DataUtils.Percent(this.PaidECSum / this.BudgetEUSum);
                }

                return default(decimal);
            }
        }
    }
}