using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_09992
{
    public partial class AttachedDocumentContent
	{
        private bool _isDocumentValid = true;

        [XmlIgnore]
        public bool IsDocumentValid
        {
            get { return _isDocumentValid; }
            set { _isDocumentValid = value; }
        }
	}
}
