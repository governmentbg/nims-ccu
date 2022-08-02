using System.Collections.Generic;
using System.Linq;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Localization;

namespace Eumis.Public.Web.Models.PriorityLines
{
    public class PriorityAxisModel
    {
        public int ProgrammePriorityId { get; set; }

        public string ProgrammePriorityName { get; set; }

        public string ProgrammePriorityNameAlt { get; set; }

        public decimal EuAmount { get; set; }

        public decimal BgAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal ContractedTotalSum
        {
            get
            {
                return this.Procedures.Sum(p => p.ContractedTotalAmount);
            }
        }

        public decimal ContractedNationalSum
        {
            get
            {
                return this.Procedures.Sum(p => p.ContractedBgAmount);
            }
        }

        public decimal ContractedSelfSum
        {
            get
            {
                return this.Procedures.Sum(p => p.ContractedSelfAmount);
            }
        }

        public decimal ContractedEuSum
        {
            get
            {
                return this.Procedures.Sum(p => p.ContractedEuAmount);
            }
        }

        public decimal PayedTotalSum
        {
            get
            {
                return this.Procedures.Sum(p => p.PayedTotalAmount);
            }
        }

        public decimal PayedNationalSum
        {
            get
            {
                return this.Procedures.Sum(p => p.PayedBgAmount);
            }
        }

        public decimal PayedEuSum
        {
            get
            {
                return this.Procedures.Sum(p => p.PayedEuAmount);
            }
        }

        public decimal ContractedPercentExec
        {
            get
            {
                if (this.TotalAmount != 0)
                {
                    return DataUtils.Percent((this.ContractedEuSum + this.ContractedNationalSum) / (this.EuAmount + this.BgAmount));
                }

                return default(decimal);
            }
        }

        public decimal PayedPercentExec
        {
            get
            {
                if (this.TotalAmount != 0)
                {
                    return DataUtils.Percent(this.PayedTotalSum / this.TotalAmount);
                }

                return default(decimal);
            }
        }

        public IEnumerable<ProcedureModel> Procedures { get; set; }

        public string TransProgrammePriorityName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.ProgrammePriorityNameAlt ?? this.ProgrammePriorityName;
                }
                else
                {
                    return this.ProgrammePriorityName;
                }
            }
        }
    }
}