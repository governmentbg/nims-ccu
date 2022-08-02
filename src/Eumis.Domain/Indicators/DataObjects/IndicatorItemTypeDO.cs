using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Indicators.DataObjects
{
    public class IndicatorItemTypeDO
    {
        public IndicatorItemTypeDO()
        {
        }

        public IndicatorItemTypeDO(IndicatorItemType indicatorType)
            : this()
        {
            this.IndicatorItemTypeId = indicatorType.IndicatorItemTypeId;
            this.Name = indicatorType.Name;
            this.NameAlt = indicatorType.NameAlt;
            this.Version = indicatorType.Version;
        }

        public int IndicatorItemTypeId { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public byte[] Version { get; set; }
    }
}
