using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10002
{
    public partial class ProjectBasicData
    {
        private bool _isNameValid = true;
        private bool _isDurationValid = true;
        private bool _isNameEnValid = true;
        private bool _isDescriptionValid = true;
        private bool _isDescriptionEnValid = true;
        private bool _isPurposeValid = true;
        private bool _isAdditionalDescriptionValid = true;

        [XmlIgnore]
        public bool IsNameValid
        {
            get { return _isNameValid; }
            set { _isNameValid = value; }
        }

        [XmlIgnore]
        public bool IsDurationValid 
        {
            get { return _isDurationValid; }
            set { _isDurationValid = value; }
        }

        [XmlIgnore]
        public bool IsNameEnValid
        {
            get { return _isNameEnValid; }
            set { _isNameEnValid = value; }
        }

        [XmlIgnore]
        public bool IsDescriptionValid
        {
            get { return _isDescriptionValid; }
            set { _isDescriptionValid = value; }
        }

        [XmlIgnore]
        public bool IsDescriptionEnValid
        {
            get { return _isDescriptionEnValid; }
            set { _isDescriptionEnValid = value; }
        }

        [XmlIgnore]
        public bool IsPurposeValid
        {
            get { return _isPurposeValid; }
            set { _isPurposeValid = value; }
        }

        [XmlIgnore]
        public bool IsAdditionalDescriptionValid
        {
            get { return _isAdditionalDescriptionValid; }
            set { _isAdditionalDescriptionValid = value; }
        }
    }
}
