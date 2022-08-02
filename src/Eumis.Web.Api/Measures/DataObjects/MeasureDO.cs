using Eumis.Domain.Measures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.Measures.DataObjects
{
    public class MeasureDO
    {
        public MeasureDO()
        {
        }

        public MeasureDO(Measure measure)
        {
            this.MeasureId = measure.MeasureId;
            this.ShortName = measure.ShortName;
            this.Name = measure.Name;
            this.NameAlt = measure.NameAlt;

            this.Version = measure.Version;
        }

        public int? MeasureId { get; set; }

        public string ShortName { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public byte[] Version { get; set; }
    }
}
