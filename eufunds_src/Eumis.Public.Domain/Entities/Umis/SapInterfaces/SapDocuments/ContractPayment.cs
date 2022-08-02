using System;
using System.Xml.Serialization;

namespace Eumis.Public.Domain.Entities.Umis.SapInterfaces.SapDocuments
{
    [XmlRootAttribute("ContractPayment")]
    public class ContractPayment
    {
        [XmlElement]
        public FinanceSource FinanceSource { get; set; }

        [XmlElement]
        public double PayedAmount { get; set; }

        [XmlElement]
        public Currency Currency { get; set; }

        [XmlElement]
        public PaymentType? PaymentType { get; set; }

        [XmlElement]
        public DateTime AccDate { get; set; }

        [XmlElement]
        public DateTime BankDate { get; set; }

        [XmlElement]
        public DateTime SAPDate { get; set; }

        [XmlElement]
        public string Comment { get; set; }

        [XmlElement]
        public string StornoCode { get; set; }

        [XmlElement]
        public string StornoDescr { get; set; }

        [XmlElement]
        public string Field3 { get; set; }

        [XmlElement]
        public string Field4 { get; set; }

        [XmlElement]
        public string Field5 { get; set; }
    }
}
