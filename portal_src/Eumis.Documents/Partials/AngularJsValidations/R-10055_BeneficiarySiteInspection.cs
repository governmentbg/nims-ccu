using System;
using System.Xml.Serialization;

namespace R_10055
{
    public partial class BeneficiarySiteInspection
    {
        private bool _isNumberValid = true;
        private bool _isCheckSubjectValid = true;
        private bool _isRangeValid = true;
        private bool _isStartDateValid = true;
        private bool _isEndDateValid = true;
        private bool _isKeyRecommendationsValid = true;
        private bool _isRecommendationsFulfilledValid = true;
        private bool _isCommentValid = true;

        [XmlIgnore]
        public bool IsNumberValid { get { return _isNumberValid; } set { _isNumberValid = value; } }
        [XmlIgnore]
        public bool IsCheckSubjectValid { get { return _isCheckSubjectValid; } set { _isCheckSubjectValid = value; } }
        [XmlIgnore]
        public bool IsRangeValid { get { return _isRangeValid; } set { _isRangeValid = value; } }
        [XmlIgnore]
        public bool IsStartDateValid { get { return _isStartDateValid; } set { _isStartDateValid = value; } }
        [XmlIgnore]
        public bool IsEndDateValid { get { return _isEndDateValid; } set { _isEndDateValid = value; } }
        [XmlIgnore]
        public bool IsKeyRecommendationsValid { get { return _isKeyRecommendationsValid; } set { _isKeyRecommendationsValid = value; } }
        [XmlIgnore]
        public bool IsRecommendationsFulfilledValid { get { return _isRecommendationsFulfilledValid; } set { _isRecommendationsFulfilledValid = value; } }
        [XmlIgnore]
        public bool IsCommentValid { get { return _isCommentValid; } set { _isCommentValid = value; } }
    }
}
