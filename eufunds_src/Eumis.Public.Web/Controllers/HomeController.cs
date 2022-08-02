using System.Linq;
using System.Web.Mvc;
using Eumis.Public.Common.CacheProvider;
using Eumis.Public.Data.ProgrammeGroups.Repositories;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Web.InfrastructureClasses;
using Eumis.Public.Web.Models.Home;

namespace Eumis.Public.Web.Controllers
{
    public partial class HomeController : BaseController
    {
        private IProgrammeGroupsRepository programmeGroupsRepository;
        private IUmisRepository umisRepository;

        public HomeController(
            IMapsRepository mapsRepository,
            IInfrastructureRepository infrastructureRepository,
            IProgrammeGroupsRepository programmeGroupsRepository,
            IUmisRepository umisRepository)
            : base(mapsRepository, infrastructureRepository)
        {
            this.programmeGroupsRepository = programmeGroupsRepository;
            this.umisRepository = umisRepository;
        }

        [ClearRoute]
        public virtual ActionResult Index()
        {
            IndexModel model = new IndexModel(
                    programmeGroups: this.programmeGroupsRepository.GetAllProgrammeGroups()
                                            .OrderBy(e => e.PortalOrderNum)
                                            .Select(st => new NomenclatureModel(st.ProgrammeGroupId, st.TransName)),
                    planningRegions: MapRegionHelper.GetAll()
                                            .Select(st => new NomenclatureModel(st.Id, st.TransName)));

            return this.View(model);
        }

        public virtual ActionResult Glossary()
        {
            return this.View();
        }

        public virtual ActionResult About()
        {
            return this.View();
        }

        public virtual ActionResult AccessibilityPolicy()
        {
            return this.View();
        }

        public virtual PartialViewResult SavedTrees()
        {
            int savedTreesCount = EumisMemoryCache.GetItem<int>(nameof(EumisMemoryCacheKeys.SavedTrees), () => this.umisRepository.GetSavedTrees());

            GreenCornerModel vm = new GreenCornerModel
            {
                SavedTreesCount = savedTreesCount,
            };

            return this.PartialView(MVC.Shared.Views._GreenCorner, vm);
        }
    }
}