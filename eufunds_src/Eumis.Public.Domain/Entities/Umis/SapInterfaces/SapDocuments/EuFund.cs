using System;
using System.Xml.Serialization;

namespace Eumis.Public.Domain.Entities.Umis.SapInterfaces.SapDocuments
{
    [Serializable]
    public enum EuFund
    {
        [XmlEnum(Name = "ESF")]
        ESF = 1,

        [XmlEnum(Name = "ERDF")]
        ERDF = 2,

        [XmlEnum(Name = "CF")]
        CF = 3
    }
}
