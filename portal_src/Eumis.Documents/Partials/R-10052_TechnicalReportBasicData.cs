using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_10052
{
    public partial class TechnicalReportBasicData
    {
        public void Load(TechnicalReportBasicData basicData)
        {
            this.StartDate = basicData.StartDate;
            this.EndDate = basicData.EndDate;
            this.PreparerName = basicData.PreparerName;
            this.PreparerPhone = basicData.PreparerPhone;
            this.PreparerPosition = basicData.PreparerPosition;
            this.PreparerEmail = basicData.PreparerEmail;
        }
    }
}
