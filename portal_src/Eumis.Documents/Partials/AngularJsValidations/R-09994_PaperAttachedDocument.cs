using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_09994
{
	public partial class PaperAttachedDocument
	{
        private bool _isTypeValid = true;
        private bool _isDescriptionValid = true;

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
	}
}
