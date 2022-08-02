using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Eumis.Public.Common.Export
{
    [SuppressMessage("", "SA1649:FileNameMustMatchTypeName", Justification = "Common file name is used for all classes")]
    [Serializable]
    [XmlType(TypeName = "Container")]
    public class GenericXmlContainer
    {
        public List<GenericXmlTable> Tables { get; set; }
    }

    [SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "XML classes should be in the same file for simplicity")]
    [Serializable]
    [XmlType(TypeName = "Table")]
    public class GenericXmlTable
    {
        public List<GenericXmlRow> Rows { get; set; }
    }

    [SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "XML classes should be in the same file for simplicity")]
    [Serializable]
    [XmlType(TypeName = "Row")]
    public class GenericXmlRow
    {
        public List<GenericXmlCell> Cells { get; set; }
    }

    [SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "XML classes should be in the same file for simplicity")]
    [Serializable]
    [XmlType(TypeName = "Cell")]
    public class GenericXmlCell
    {
        public string Value { get; set; }
    }
}
