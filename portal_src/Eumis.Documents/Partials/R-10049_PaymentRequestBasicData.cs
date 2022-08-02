using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10049
{
    public partial class PaymentRequestBasicData
    {
        [XmlIgnore]
        public bool? ContractReportHasAdvanceVerificationPayment { get; set; }

        [XmlIgnore]
        public bool IsAdvanceType
        {
            get
            {
                return this.Type != null && !String.IsNullOrWhiteSpace(this.Type.Value)
                    && (Eumis.Documents.Enums.PaymentRequestTypeNomenclature.Advance.Id.Equals(this.Type.Value)
                    || Eumis.Documents.Enums.PaymentRequestTypeNomenclature.AdvanceVerification.Id.Equals(this.Type.Value));
            }
        }
    }
}
