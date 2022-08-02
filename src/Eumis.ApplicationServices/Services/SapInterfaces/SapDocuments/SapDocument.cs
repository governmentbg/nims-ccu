using System;
using System.Xml.Serialization;

namespace Eumis.ApplicationServices.SapInterfaces.SapDocuments
{
    [XmlRootAttribute("SAPImport")]
    public class SapDocument
    {
        [XmlElement]
        public string SapKey { get; set; }

        [XmlElement(DataType = "date")]
        public DateTime Date { get; set; }

        [XmlElement(DataType = "time")]
        public DateTime Time { get; set; }

        [XmlElement]
        public string SapUser { get; set; }

        [XmlElement(Type = typeof(Contract), ElementName = "Contract")]
        public Contract[] ContractCollection { get; set; }
    }
}
