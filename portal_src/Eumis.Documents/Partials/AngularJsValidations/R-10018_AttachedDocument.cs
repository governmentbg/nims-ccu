using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_10018
{
	public partial class AttachedDocument
	{
        private bool _isTypeValid = true;
        private bool _isDescriptionValid = true;
        private bool _isDocumentValid = true;
        private bool _isSignatureValid = true;

        [XmlIgnore]
        public bool IsTypeValid
        {
            get { return _isTypeValid; }
            set { _isTypeValid = value; }
        }

        [XmlIgnore]
        public bool IsDescriptionValid
        {
            get { return _isDescriptionValid; }
            set { _isDescriptionValid = value; }
        }

        [XmlIgnore]
        public bool IsDocumentValid
        {
            get { return _isDocumentValid; }
            set { _isDocumentValid = value; }
        }

        [XmlIgnore]
        public bool IsSignatureValid
        {
            get { return _isSignatureValid; }
            set { _isSignatureValid = value; }
        }
	}
}
