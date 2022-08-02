using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10026
{
    public partial class EvalSheet
    {
        private bool _isTotalValid = true;

        [XmlIgnore]
        public bool IsTotalValid
        {
            get { return _isTotalValid; }
            set { _isTotalValid = value; }
        }
    }
}
