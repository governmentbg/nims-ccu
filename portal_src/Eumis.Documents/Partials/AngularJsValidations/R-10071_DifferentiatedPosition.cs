using System.Xml.Serialization;

namespace R_10071
{
    public partial class DifferentiatedPosition
    {
        private bool _isNameValid = true;
        private bool _isSubmittedOffersCountValid = true;
        private bool _isRankedOffersCountValid = true;
        private bool _isCommentValid = true;

        [XmlIgnore]
        public bool IsNameValid
        {
            get { return _isNameValid; }
            set { _isNameValid = value; }
        }

        [XmlIgnore]
        public bool IsSubmittedOffersCountValid
        {
            get { return _isSubmittedOffersCountValid; }
            set { _isSubmittedOffersCountValid = value; }
        }

        [XmlIgnore]
        public bool IsRankedOffersCountValid
        {
            get { return _isRankedOffersCountValid; }
            set { _isRankedOffersCountValid = value; }
        }

        [XmlIgnore]
        public bool IsCommentValid
        {
            get { return _isCommentValid; }
            set { _isCommentValid = value; }
        }
    }
}
