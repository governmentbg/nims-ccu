using Eumis.Common.Helpers;
using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10059
{
    public partial class FinanceBudgetLevel3
    {
        [XmlIgnore]
        public string Level1OrderNum { get; set; }
        [XmlIgnore]
        public string Level2OrderNum { get; set; }

    }
}
