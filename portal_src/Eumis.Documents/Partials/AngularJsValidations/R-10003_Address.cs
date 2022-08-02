using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10003
{
    public partial class Address
    {
        private bool _isCountryValid = true;
        private bool _isSettlementValid = true;
        private bool _isPostCodeValid = true;
        private bool _isStreetValid = true;
        private bool _isFullAddressValid = true;

        [XmlIgnore]
        public bool IsCountryValid
        {
            get { return _isCountryValid; }
            set { _isCountryValid = value; }
        }

        [XmlIgnore]
        public bool IsSettlementValid
        {
            get { return _isSettlementValid; }
            set { _isSettlementValid = value; }
        }

        [XmlIgnore]
        public bool IsPostCodeValid
        {
            get { return _isPostCodeValid; }
            set { _isPostCodeValid = value; }
        }

        [XmlIgnore]
        public bool IsStreetValid
        {
            get { return _isStreetValid; }
            set { _isStreetValid = value; }
        }

        [XmlIgnore]
        public bool IsFullAddressValid
        {
            get { return _isFullAddressValid; }
            set { _isFullAddressValid = value; }
        }
    }
}
