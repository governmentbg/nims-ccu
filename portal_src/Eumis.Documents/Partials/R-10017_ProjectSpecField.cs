using Eumis.Common.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10017
{
    public partial class ProjectSpecField
    {
        [XmlIgnore]
        public bool? IsRequired { get; set; }

        [XmlIgnore]
        public bool IsDeactivated { get; set; }

        [XmlIgnore]
        public int MaxLength { get; set; }

        [XmlIgnore]
        public string DisplayTitle
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.Title, this.TitleEN);
            }
        }

        [XmlIgnore]
        public string DisplayDescription
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.Description, this.DescriptionEN);
            }
        }

        [XmlIgnore]
        public bool IsIBANField
        {
            get
            {
                return this.MaxLength == 0;
            }
        }
    }
}
