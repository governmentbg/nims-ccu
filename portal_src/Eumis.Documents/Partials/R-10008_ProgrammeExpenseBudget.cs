using Eumis.Common.Helpers;
using Eumis.Common.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10008
{
    public partial class ProgrammeExpenseBudget
    {
        [XmlIgnore]
        public bool IsDeactivated { get; set; }

        [XmlIgnore]
        public string GrandDisplay
        {
            get
            {
                decimal result = 0.00m;

                if (this.ProgrammeDetailsExpenseBudgetCollection != null)
                {
                    foreach (var detail in this.ProgrammeDetailsExpenseBudgetCollection)
                    {
                        result += detail.GrandAmount;
                    }
                }

                return DataUtils.DecimalToStringDecimalPoint(result);
            }
        }

        [XmlIgnore]
        public string SelfDisplay
        {
            get
            {
                decimal result = 0.00m;

                if (this.ProgrammeDetailsExpenseBudgetCollection != null)
                {
                    foreach (var detail in this.ProgrammeDetailsExpenseBudgetCollection)
                    {
                        result += detail.SelfAmount;
                    }
                }

                return DataUtils.DecimalToStringDecimalPoint(result);
            }
        }

        [XmlIgnore]
        public string TotalDisplay
        {
            get
            {
                decimal result = 0.00m;

                if (this.ProgrammeDetailsExpenseBudgetCollection != null)
                {
                    foreach (var detail in this.ProgrammeDetailsExpenseBudgetCollection)
                    {
                        result += detail.TotalAmount;
                    }
                }

                return DataUtils.DecimalToStringDecimalPoint(result);
            }
        }

        [XmlIgnore]
        public string GrandPercentageDisplay
        {
            get
            {
                if (this.ProgrammeDetailsExpenseBudgetCollection != null)
                {
                    double grandTotal = Double.Parse(this.GrandDisplay, CultureInfo.InvariantCulture);
                    double selfTotal = Double.Parse(this.SelfDisplay, CultureInfo.InvariantCulture);

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
        public string SelfPercentageDisplay
        {
            get
            {
                double perc;
                if (Double.TryParse(this.GrandPercentageDisplay, NumberStyles.Any, CultureInfo.InvariantCulture, out perc))
                {
                    double result = 100 - perc;
                    return DataUtils.DoubleToStringDecimalPoint(result);
                }
                else
                    return String.Empty;
            }
        }

        [XmlIgnore]
        public string DisplayName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.Name, this.NameEN);
            }
        }
    }
}
