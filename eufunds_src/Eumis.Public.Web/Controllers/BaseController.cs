using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Eumis.Public.Common;
using Eumis.Public.Common.Localization;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Domain.Entities;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.Programmes;
using Eumis.Public.Resources;

namespace Eumis.Public.Web.Controllers
{
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.")]
    public abstract partial class BaseController : Controller
    {
        private IMapsRepository mapsRepository;
        private IInfrastructureRepository infrastructureRepository;

        private int? opId;
        private int? prId;

        private Programme programme;
        private MapRegion mapRegion;

        public BaseController(
            IMapsRepository mapsRepository,
            IInfrastructureRepository infrastructureRepository)
        {
            this.InfrastructureRepository = infrastructureRepository;
            this.MapsRepository = mapsRepository;
        }

        public Programme GetOP
        {
            get
            {
                if (this.programme == null)
                {
                    var routeOp = this.RouteData.Values[RouteConfig.OPABB] != null ? this.RouteData.Values[RouteConfig.OPABB].ToString() : Common.Configuration.OP_DEFAULT_ID.ToString();

                    int opId = Common.Configuration.OP_DEFAULT_ID;
                    if (int.TryParse(routeOp, out opId) && !routeOp.Equals(Common.Configuration.OP_DEFAULT_ID.ToString()))
                    {
                        this.programme = this.InfrastructureRepository.GetAllOps().Single(e => e.MapNodeId == opId);
                    }
                    else
                    {
                        this.programme = new Programme()
                        {
                            MapNodeId = opId,
                            PortalName = Texts.Global_NoFilter,
                            PortalNameAlt = Texts.Global_NoFilter,
                        };
                    }
                }

                return this.programme;
            }
        }

        public MapRegion GetPR
        {
            get
            {
                if (this.mapRegion == null)
                {
                    var routePr = this.RouteData.Values[RouteConfig.PRABB] != null ? this.RouteData.Values[RouteConfig.PRABB].ToString() : Common.Configuration.PR_DEFAULT_ID.ToString();

                    int prId = Common.Configuration.PR_DEFAULT_ID;
                    if (int.TryParse(routePr, out prId) && routePr.Equals(Common.Configuration.PR_DEFAULT_ID.ToString()))
                    {
                        this.mapRegion = new MapRegion()
                        {
                            Id = prId,
                            Name = Texts.Global_NoFilter,
                            NameAlt = Texts.Global_NoFilter,
                        };
                    }
                    else if (int.TryParse(routePr, out prId) && routePr.Equals(Common.Configuration.PR_INTERNATIONAL_ID.ToString()))
                    {
                        this.mapRegion = new MapRegion()
                        {
                            Id = prId,
                            Name = Texts.Global_International,
                            NameAlt = Texts.Global_International,
                            RegionId = prId,
                        };
                    }
                    else if (int.TryParse(routePr, out prId) && routePr.Equals(Common.Configuration.PR_BULGARIA_ID.ToString()))
                    {
                        this.mapRegion = new MapRegion()
                        {
                            Id = prId,
                            Name = Texts.Global_Bulgaria,
                            NameAlt = Texts.Global_Bulgaria,
                            NutsLevel = Domain.Entities.Umis.NonAggregates.NutsLevel.Country,
                            RegionId = 23,
                        };
                    }
                    else
                    {
                        this.mapRegion = this.MapsRepository.GetAllMapRegionsByType(MapTypeEnum.BgNuts).Single(e => e.Id == prId);
                    }
                }

                return this.mapRegion;
            }
        }

        /// <summary>
        /// Gets operational programme id.
        /// </summary>
        protected int OpId
        {
            get
            {
                if (this.opId.HasValue)
                {
                    return this.opId.Value;
                }
                else
                {
                    this.opId = this.GetOP.MapNodeId;
                    return this.opId.Value;
                }
            }
        }

        /// <summary>
        /// Gets programme region id.
        /// </summary>
        protected int PrId
        {
            get
            {
                if (this.prId.HasValue)
                {
                    return this.prId.Value;
                }
                else
                {
                    this.prId = this.GetPR.Id;
                    return this.prId.Value;
                }
            }
        }

        protected IMapsRepository MapsRepository { get => this.mapsRepository; set => this.mapsRepository = value; }

        protected IInfrastructureRepository InfrastructureRepository { get => this.infrastructureRepository; set => this.infrastructureRepository = value; }

        protected override void ExecuteCore()
        {
            this.SetLocalizationCulture();

            base.ExecuteCore();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            this.SetLocalizationCulture();

            // prevent browser caching
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnActionExecuting(filterContext);
        }

        protected void SetLocalizationCulture()
        {
            var bgLangHeader = "bg";

            if (SystemLocalization.IsLangaugePage(this.RouteData))
            {
                if (this.RouteData.Values["lang"] != null && !string.IsNullOrWhiteSpace(this.RouteData.Values["lang"].ToString()))
                {
                    var lang = this.RouteData.Values["lang"].ToString();
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(lang);
                }
                else
                {
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(bgLangHeader);
                    this.RouteData.Values["lang"] = bgLangHeader;
                }
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(bgLangHeader);
            }

            SystemLocalization.Culture = Thread.CurrentThread.CurrentUICulture;
        }
    }
}