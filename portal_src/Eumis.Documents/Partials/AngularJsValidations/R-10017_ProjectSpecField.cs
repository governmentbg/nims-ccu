using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10017
{
    public partial class ProjectSpecField
    {
        private bool _isValueValid = true;

        [XmlIgnore]
        public bool IsValueValid
        {
            get { return _isValueValid; }
            set { _isValueValid = value; }
        }
    }
}
