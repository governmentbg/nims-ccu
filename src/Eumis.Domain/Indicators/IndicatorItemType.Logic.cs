using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Indicators
{
    public partial class IndicatorItemType
    {
        public void UpdateAttributes(string name, string nameAlt)
        {
            this.Name = name;
            this.NameAlt = nameAlt;

            this.ModifyDate = DateTime.Now;
        }
    }
}
