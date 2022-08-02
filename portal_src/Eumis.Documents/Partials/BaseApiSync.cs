using System;
using System.Xml.Serialization;

namespace Eumis.Documents.Partials
{
    [Serializable]
    public partial class BaseApiSync
    {
        [XmlIgnore]
        public bool IsActive { get; set; }
    }
}