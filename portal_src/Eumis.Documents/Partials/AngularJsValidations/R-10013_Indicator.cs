using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10013
{
    public partial class Indicator
    {
        private bool _isNameValid = true;
        private bool _isBaseMenValid = true;
        private bool _isBaseWomenValid = true;
        private bool _isBaseValid = true;
        private bool _isTargetMenValid = true;
        private bool _isTargetWomenValid = true;
        private bool _isTargetValid = true;
        private bool _isDescriptionValid = true;

        [XmlIgnore]
        public bool IsNameValid
        {
            get { return _isNameValid; }
            set { _isNameValid = value; }
        }

        [XmlIgnore]
        public bool IsBaseMenValid
        {
            get { return _isBaseMenValid; }
            set { _isBaseMenValid = value; }
        }

        [XmlIgnore]
        public bool IsBaseWomenValid
        {
            get { return _isBaseWomenValid; }
            set { _isBaseWomenValid = value; }
        }

        [XmlIgnore]
        public bool IsBaseValid
        {
            get { return _isBaseValid; }
            set { _isBaseValid = value; }
        }

        [XmlIgnore]
        public bool IsTargetMenValid
        {
            get { return _isTargetMenValid; }
            set { _isTargetMenValid = value; }
        }

        [XmlIgnore]
        public bool IsTargetWomenValid
        {
            get { return _isTargetWomenValid; }
            set { _isTargetWomenValid = value; }
        }

        [XmlIgnore]
        public bool IsTargetValid
        {
            get { return _isTargetValid; }
            set { _isTargetValid = value; }
        }

        [XmlIgnore]
        public bool IsDescriptionValid
        {
            get { return _isDescriptionValid; }
            set { _isDescriptionValid = value; }
        }
    }
}
