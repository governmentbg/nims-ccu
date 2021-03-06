//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;
using Eumis.Common.Resources;
using Eumis.Documents.Contracts;
using Eumis.Documents.Enums;
using System.Collections.Generic;
using Eumis.Documents.Interfaces;
using System.Linq;
using Eumis.Documents;
using Eumis.Documents.Validation;
using Eumis.Common.Validation;
using Eumis.Common.Helpers;

namespace R_10022
{
    public partial class EvalTableGroup
    {
        [XmlIgnore]
        public decimal WeightDecimal
        {
            get
            {
                decimal result = 0.00m;

                if (this.EvalTableCriteriaCollection != null)
                {
                    foreach (var criteria in this.EvalTableCriteriaCollection)
                    {
                        result += criteria.Weight;
                    }
                }

                return result;
            }
        }

        [XmlIgnore]
        public string WeightTotal
        {
            get
            {
                return DataUtils.DecimalToStringDecimalPointSpace(this.WeightDecimal);
            }
        }
    }
}
