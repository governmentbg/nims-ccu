using System.Web.Mvc;
using Eumis.Public.Web.Models.Pies;

namespace Eumis.Public.Web.InfrastructureClasses.Pies
{
    public class JsonPieResult : JsonResultCameCase
    {
        public JsonPieResult()
        {
            this.JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet;
        }

        public JsonPieResult(PieModel pie)
            : this()
        {
            this.Pie = pie;
        }

        public PieModel Pie { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            this.Data = this.Pie;
            base.ExecuteResult(context);
        }
    }
}