using System.Xml.Serialization;

namespace Eumis.ApplicationServices.SapInterfaces.SapDocuments
{
    [XmlRootAttribute("Contract")]
    public class Contract
    {
        [XmlElement]
        public string ContractSapNum { get; set; }

        [XmlElement]
        public string EuFund { get; set; }

        [XmlElement(Type = typeof(ReqPayment), ElementName = "ReqPayment")]
        public ReqPayment[] ReqPaymentCollection { get; set; }
    }
}
