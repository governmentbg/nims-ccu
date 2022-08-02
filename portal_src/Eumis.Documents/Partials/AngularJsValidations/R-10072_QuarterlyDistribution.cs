using System.Xml.Serialization;

namespace R_10072
{
    public partial class QuarterlyDistribution
    {
        private bool _isQ1AmountValid = true;
        private bool _isQ2AmountValid = true;
        private bool _isQ3AmountValid = true;
        private bool _isQ4AmountValid = true;

        [XmlIgnore]
        public bool IsQ1AmountValid
        {
            get { return _isQ1AmountValid; }
            set { _isQ1AmountValid = value; }
        }

        [XmlIgnore]
        public bool IsQ2AmountValid
        {
            get { return _isQ2AmountValid; }
            set { _isQ2AmountValid = value; }
        }

        [XmlIgnore]
        public bool IsQ3AmountValid
        {
            get { return _isQ3AmountValid; }
            set { _isQ3AmountValid = value; }
        }

        [XmlIgnore]
        public bool IsQ4AmountValid
        {
            get { return _isQ4AmountValid; }
            set { _isQ4AmountValid = value; }
        }
    }
}
