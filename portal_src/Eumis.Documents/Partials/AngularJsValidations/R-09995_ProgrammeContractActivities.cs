using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_09995
{
    public partial class ProgrammeContractActivities
    {
        private bool _isValid = true;
        private bool _isPeriodValid = true;

        [XmlIgnore]
        public bool IsValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }

        [XmlIgnore]
        public bool IsPeriodValid
        {
            get { return _isPeriodValid; }
            set { _isPeriodValid = value; }
        }
    }
}
