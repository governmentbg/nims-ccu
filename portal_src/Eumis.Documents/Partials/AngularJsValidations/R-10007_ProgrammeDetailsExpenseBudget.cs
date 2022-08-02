using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10007
{
    public partial class ProgrammeDetailsExpenseBudget
    {
        private bool _isNameValid = true;
        private bool _isGrandAmountValid = true;
        private bool _isSelfAmountValid = true;
        private bool _isTotalAmountValid = true;
        private bool _isDirectionValid = true;
        private bool _isNutsValid = true;

        [XmlIgnore]
        public bool IsNameValid
        {
            get { return _isNameValid; }
            set { _isNameValid = value; }
        }

        [XmlIgnore]
        public bool IsGrandAmountValid
        {
            get { return _isGrandAmountValid; }
            set { _isGrandAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsSelfAmountValid
        {
            get { return _isSelfAmountValid; }
            set { _isSelfAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsTotalAmountValid
        {
            get { return _isTotalAmountValid; }
            set { _isTotalAmountValid = value; }
        }

        [XmlIgnore]
        public bool IsNutsValid
        {
            get { return _isNutsValid; }
            set { _isNutsValid = value; }
        }

        [XmlIgnore]
        public bool IsDirectionValid
        {
            get { return _isDirectionValid; }
            set { _isDirectionValid = value; }
        }
    }
}
