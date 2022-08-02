using System.Xml.Serialization;

namespace Eumis.Public.Domain.Entities.Umis.SapInterfaces.SapDocuments
{
    [XmlRootAttribute("Contract")]
    public class Contract
    {
        [XmlElement]
        public string ContractSapNum { get; set; }

        [XmlElement]
        public EuFund EuFund { get; set; }

        [XmlElement(Type = typeof(ReqPayment), ElementName = "ReqPayment")]
        public ReqPayment[] ReqPaymentCollection;
    }
}
