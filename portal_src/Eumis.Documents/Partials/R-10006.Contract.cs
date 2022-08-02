using Eumis.Documents.Partials;
using System.Xml.Serialization;

namespace R_10006
{
    public partial class Contract : BaseApiSync
    {
        [XmlIgnore]
        public bool IsFinalRecipients { get; set; }

        [XmlIgnore]
        public bool IsFinancialIntermediaries { get; set; }
    }
}
