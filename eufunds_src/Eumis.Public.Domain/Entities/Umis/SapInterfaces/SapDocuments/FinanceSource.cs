using System;
using System.Xml.Serialization;

namespace Eumis.Public.Domain.Entities.Umis.SapInterfaces.SapDocuments
{
    [Serializable]
    public enum FinanceSource
    {
        [XmlEnum(Name = "EU")]
        EU = 1,

        [XmlEnum(Name = "BG")]
        BG = 2
    }
}
