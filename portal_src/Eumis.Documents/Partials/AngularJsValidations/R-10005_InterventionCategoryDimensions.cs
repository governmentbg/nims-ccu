using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10005
{
    public partial class InterventionCategoryDimensions
    {
        private bool _isInterventionFieldValid = true;
        private bool _isFormOfFinanceValid = true;
        private bool _isTerritorialDimensionValid = true;
        private bool _isTerritorialDeliveryMechanismValid = true;
        private bool _isThematicObjectiveValid = true;
        private bool _isESFSecondaryThemeValid = true;
        private bool _isEconomicDimensionValid = true;

        [XmlIgnore]
        public bool IsInterventionFieldValid
        {
            get { return _isInterventionFieldValid; }
            set { _isInterventionFieldValid = value; }
        }

        [XmlIgnore]
        public bool IsFormOfFinanceValid
        {
            get { return _isFormOfFinanceValid; }
            set { _isFormOfFinanceValid = value; }
        }

        [XmlIgnore]
        public bool IsTerritorialDimensionValid
        {
            get { return _isTerritorialDimensionValid; }
            set { _isTerritorialDimensionValid = value; }
        }

        [XmlIgnore]
        public bool IsTerritorialDeliveryMechanismValid
        {
            get { return _isTerritorialDeliveryMechanismValid; }
            set { _isTerritorialDeliveryMechanismValid = value; }
        }

        [XmlIgnore]
        public bool IsThematicObjectiveValid
        {
            get { return _isThematicObjectiveValid; }
            set { _isThematicObjectiveValid = value; }
        }

        [XmlIgnore]
        public bool IsESFSecondaryThemeValid
        {
            get { return _isESFSecondaryThemeValid; }
            set { _isESFSecondaryThemeValid = value; }
        }

        [XmlIgnore]
        public bool IsEconomicDimensionValid
        {
            get { return _isEconomicDimensionValid; }
            set { _isEconomicDimensionValid = value; }
        }
    }
}
