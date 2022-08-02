using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10030
{
    public partial class PreliminaryContractActivity
    {
        private bool _isCodeValid = true;
        private bool _isNameValid = true;
        private bool _isResultValid = true;

        [XmlIgnore]
        public bool IsCodeValid
        {
            get { return _isCodeValid; }
            set { _isCodeValid = value; }
        }

        [XmlIgnore]
        public bool IsNameValid
        {
            get { return _isNameValid; }
            set { _isNameValid = value; }
        }

        [XmlIgnore]
        public bool IsResultValid
        {
            get { return _isResultValid; }
            set { _isResultValid = value; }
        }
    }
}
