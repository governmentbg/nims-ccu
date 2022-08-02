using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10016
{
    public partial class ProjectErrand
    {
        private bool _isNameValid = true;
        private bool _isErrandAreaValid = true;
        private bool _isErrandLegalActValid = true;
        private bool _isErrandTypeValid = true;
        private bool _isAmountValid = true;
        private bool _isPlanDateValid = true;
        private bool _isDescriptionValid = true;

        [XmlIgnore]
        public bool IsNameValid
        {
            get { return _isNameValid; }
            set { _isNameValid = value; }
        }

        [XmlIgnore]
        public bool IsErrandAreaValid
        {
            get { return _isErrandAreaValid; }
            set { _isErrandAreaValid = value; }
        }

        [XmlIgnore]
        public bool IsErrandLegalActValid
        {
            get { return _isErrandLegalActValid; }
            set { _isErrandLegalActValid = value; }
        }

        [XmlIgnore]
        public bool IsErrandTypeValid
        {
            get { return _isErrandTypeValid; }
            set { _isErrandTypeValid = value; }
        }

        [XmlIgnore]
        public bool IsAmountValid
        {
            get { return _isAmountValid; }
            set { _isAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsPlanDateValid
        {
            get { return _isPlanDateValid; }
            set { _isPlanDateValid = value; }
        }

        [XmlIgnore]
        public bool IsDescriptionValid
        {
            get { return _isDescriptionValid; }
            set { _isDescriptionValid = value; }
        }
    }
}
