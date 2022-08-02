using System;
using System.Xml.Serialization;

namespace Eumis.ApplicationServices.SapInterfaces.SapDocuments
{
    [XmlRootAttribute("ReqPayment")]
    public class ReqPayment
    {
        [XmlElement]
        public string ReqPaymentNum { get; set; }

        [XmlElement]
        public DateTime ReqPaymentDate { get; set; }

        [XmlElement(Type = typeof(ContractPayment), ElementName = "ContractPayment")]
        public ContractPayment[] ContractPaymentCollection { get; set; }
    }
}
