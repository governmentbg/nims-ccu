using Eumis.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10034
{
    public partial class BFPContractProgrammeExpenseBudget
    {
        [XmlIgnore]
        public bool IsDeactivated { get; set; }

        [XmlIgnore]
        public string EUPercentDisplay
        {
            get
            {
                return DataUtils.DecimalToStringDecimalPoint(this.EuPercent);
            }
        }

        [XmlIgnore]
        public string NationalPercentDisplay
        {
            get
            {
                decimal result = 100 - this.EuPercent;
                return DataUtils.DecimalToStringDecimalPoint(result);
            }
        }

        [XmlIgnore]
        public string GrandPercentDisplay
        {
            get
            {
                if (this.BFPContractProgrammeDetailsExpenseBudgetCollection != null)
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
