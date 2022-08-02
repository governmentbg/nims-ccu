using System.Collections.Generic;

namespace Eumis.Public.Web.Models.Charts
{
    public class ChartModel
    {
        public ChartModel()
        {
            this.Categories = new List<string>();
            this.Data = new List<ChartDataModel>();
        }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<ChartDataModel> Data { get; set; }
    }
}