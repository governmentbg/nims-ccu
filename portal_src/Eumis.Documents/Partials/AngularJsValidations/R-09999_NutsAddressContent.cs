using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_09999
{
    public partial class NutsAddressContent
    {
        private bool _isCountryValid = true;
        private bool _isProtectedZoneValid = true;
        private bool _isNuts1Valid = true;
        private bool _isNuts2Valid = true;
        private bool _isDistrictValid = true;
        private bool _isMunicipalityValid = true;
        private bool _isSettlementValid = true;

        [XmlIgnore]
        public bool IsCountryValid
        {
            get { return _isCountryValid; }
            set { _isCountryValid = value; }
        }

        [XmlIgnore]
        public bool IsProtectedZoneValid
        {
            get { return _isProtectedZoneValid; }
            set { _isProtectedZoneValid = value; }
        }

        [XmlIgnore]
        public bool IsNuts1Valid
        {
            get { return _isNuts1Valid; }
            set { _isNuts1Valid = value; }
        }

        [XmlIgnore]
        public bool IsNuts2Valid
        {
            get { return _isNuts2Valid; }
            set { _isNuts2Valid = value; }
        }

        [XmlIgnore]
        public bool IsDistrictValid
        {
            get { return _isDistrictValid; }
            set { _isDistrictValid = value; }
        }

        [XmlIgnore]
        public bool IsMunicipalityValid
        {
            get { return _isMunicipalityValid; }
            set { _isMunicipalityValid = value; }
        }

        [XmlIgnore]
        public bool IsSettlementValid
        {
            get { return _isSettlementValid; }
            set { _isSettlementValid = value; }
        }
    }
}
