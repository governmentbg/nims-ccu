using System.Collections.Generic;
using System.Web.Mvc;
using Eumis.Public.Web.Models.Maps;

namespace Eumis.Public.Web.InfrastructureClasses.Maps
{
    public class JsonMapsResult : JsonResultCameCase
    {
        public JsonMapsResult()
        {
            this.JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet;
        }

        public JsonMapsResult(IMarkupMapModel map)
            : this()
        {
            this.Maps = new List<IMarkupMapModel>() { map };
        }

        public JsonMapsResult(IEnumerable<IMarkupMapModel> maps)
            : this()
        {
            this.Maps = new List<IMarkupMapModel>();
            this.Maps.AddRange(maps);
        }

        public List<IMarkupMapModel> Maps { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            this.Data = this.Maps;
            base.ExecuteResult(context);
        }
    }
}