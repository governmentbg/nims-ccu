using Eumis.Common.Helpers;
using Eumis.Documents.Contracts;
using Eumis.Documents.Partials;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10019
{
    public partial class ProjectErrandCollection
    {
        [XmlIgnore]
        public bool IsFinalRecipients { get; set; }

        [XmlIgnore]
        public bool IsFinancialIntermediaries { get; set; }
    }
}
