using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10024
{
    public partial class EvalSheetCriteria
    {
        private bool _isCriteriaValid = true;

        [XmlIgnore]
        public bool IsCriteriaValid
        {
            get { return _isCriteriaValid; }
            set { _isCriteriaValid = value; }
        }
    }
}
