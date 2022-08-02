using System.Web.Mvc;
using Eumis.Public.Web.Models.Charts;

namespace Eumis.Public.Web.InfrastructureClasses.Charts
{
    public class JsonChartResult : JsonResultCameCase
    {
        public JsonChartResult()
        {
            this.JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet;
        }

        public JsonChartResult(ChartModel chart)
            : this()
        {
            this.Chart = chart;
        }

        public ChartModel Chart { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            this.Data = this.Chart;
            base.ExecuteResult(context);
        }
    }
}