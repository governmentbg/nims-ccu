using System;
using System.Xml.Serialization;

namespace R_10093
{
    public partial class Direction
    {
        private bool _isDirectionValid = true;

        [XmlIgnore]
        public bool IsDirectionValid { get { return _isDirectionValid; } set { _isDirectionValid = value; } }
    }
}
