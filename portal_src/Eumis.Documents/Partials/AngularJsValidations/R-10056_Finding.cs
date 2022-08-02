using System;
using System.Xml.Serialization;

namespace R_10056
{
    public partial class Finding
    {
        private bool _isKeyFindingsValid = true;
        private bool _isRecommendationsValid = true;
        private bool _isRecommendationsFulfilledValid = true;
        private bool _isCommentValid = true;

        [XmlIgnore]
        public bool IsKeyFindingsValid { get { return _isKeyFindingsValid; } set { _isKeyFindingsValid = value; } }
        [XmlIgnore]
        public bool IsRecommendationsValid { get { return _isRecommendationsValid; } set { _isRecommendationsValid = value; } }
        [XmlIgnore]
        public bool IsRecommendationsFulfilledValid { get { return _isRecommendationsFulfilledValid; } set { _isRecommendationsFulfilledValid = value; } }
        [XmlIgnore]
        public bool IsCommentValid { get { return _isCommentValid; } set { _isCommentValid = value; } }
    }
}
