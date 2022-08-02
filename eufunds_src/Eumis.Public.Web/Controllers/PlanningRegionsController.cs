using System.Linq;
using System.Web.Mvc;
using Eumis.Public.Common;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Web.InfrastructureClasses;
using Eumis.Public.Web.InfrastructureClasses.Maps;

namespace Eumis.Public.Web.Controllers
{
    public partial class PlanningRegionsController : BaseController
    {
        public PlanningRegionsController(IMapsRepository mapsRepository, IInfrastructureRepository infrastructureRepository)
            : base(mapsRepository, infrastructureRepository)
        {
        }

        public virtual ActionResult Index()
        {
            int showId;
            if (MapRegionHelper.GetAll().Any(e => e.Id == this.PrId))
            {
                showId = MapRegionHelper.GetAll().Single(e => e.Id == this.PrId).ParentId;
            }
            else if (this.PrId == Common.Configuration.PR_BULGARIA_ID || this.PrId == Common.Configuration.PR_INTERNATIONAL_ID)
            {
                showId = MapRegionHelper.Bulgaria.ParentId;
            }
            else
            {
                showId = this.MapsRepository.GetAllMapRegions().Single(e => e.Id == this.PrId).MapId;
            }

            BgMapRendererModel mapModel = new BgMapRendererModel(
                RendererType.All,
                BgMapDataType.NameOnly,
                Common.Configuration.PR_BULGARIA_ID,
                new UrlDef(MVC.PlanningRegions.Name, MVC.PlanningRegions.ActionNames.Show));
            mapModel.ShowId = showId;
            return this.View(mapModel);
        }

        public virtual ActionResult Show(int id, int? op)
        {
            return this.RedirectToAction(MVC.Project.ActionNames.Search, MVC.Project.Name, new { pr = id, showRes = true });
        }
    }
}