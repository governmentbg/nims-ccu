using System;
using System.Xml.Serialization;

namespace Eumis.Public.Domain.Entities.Umis.SapInterfaces.SapDocuments
{
    [Serializable]
    public enum Currency
    {
        [XmlEnum(Name = "BGN")]
        BGN = 1
    }
}
