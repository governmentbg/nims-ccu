using System;
using System.Xml.Serialization;

namespace R_10040
{
    public partial class BFPContractContractTeams
    {
        private bool _isValid = true;

        [XmlIgnore]
        public bool IsValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }
    }
}
