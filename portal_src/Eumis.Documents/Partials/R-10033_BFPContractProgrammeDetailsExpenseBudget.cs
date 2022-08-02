using Eumis.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10033
{
    public partial class BFPContractProgrammeDetailsExpenseBudget
    {
        public static string GetEnumText(R_10035.BFPContractProgrammeBudget level1, R_10034.BFPContractProgrammeExpenseBudget level2, BFPContractProgrammeDetailsExpenseBudget level3)
        {
            var result = string.Format("{0}.{1}.{2} {3}"
                , DataUtils.Romanize(level1.OrderNum), level2.OrderNum, level3.OrderNum, level3.Name);

            if(level2.AidMode != null)
            {
                result += string.Format(" (режим на финансиране: {0})", level2.AidMode.Description);
            }

            return result;
        }
    }
}
