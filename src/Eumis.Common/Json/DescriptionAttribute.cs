using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Common.Json
{
    [AttributeUsage(AttributeTargets.All)]
    public class DescriptionAttribute : Attribute
    {
        // use DisplayAttribute's built in localization support
        private readonly DisplayAttribute attribute = new DisplayAttribute();

        public DescriptionAttribute()
        {
        }

        public DescriptionAttribute(string description)
        {
            this.Description = description;
        }

        public string Description
        {
            get { return this.attribute.Description; }
            set { this.attribute.Description = value; }
        }

        public Type ResourceType
        {
            get { return this.attribute.ResourceType; }
            set { this.attribute.ResourceType = value; }
        }

        public string GetDescription()
        {
            return this.attribute.GetDescription();
        }
    }
}
