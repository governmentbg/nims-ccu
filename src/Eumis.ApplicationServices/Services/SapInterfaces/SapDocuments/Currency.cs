using System;
using System.Xml.Serialization;

namespace Eumis.ApplicationServices.SapInterfaces.SapDocuments
{
    [Serializable]
    public enum Currency
    {
        [XmlEnum(Name = "BGN")]
        BGN = 1,

        [XmlEnum(Name = "EUR")]
        EUR = 2,
    }
}
