using System.Collections.Generic;

namespace Eumis.Public.Web.Models.Charts
{
    public class ChartDataModel
    {
        public ChartDataModel()
        {
            this.Data = new List<decimal>();
        }

        public string Name { get; set; }

        public string Stack { get; set; }

        public string Type { get; set; }

        public IEnumerable<decimal> Data { get; set; }
    }
}