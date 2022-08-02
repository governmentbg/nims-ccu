using System.Linq;
using System.Web.Mvc;
using Eumis.Public.Common;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Domain.Entities;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.Programmes;
using Eumis.Public.Resources;

namespace Eumis.Public.Web.Views
{
    public abstract class EumisPublicBaseView<TModel> : WebViewPage<TModel>
    {
        private int? opId;
        private int? prId;

        private MapRegion pr;
        private Programme op;

        public string Language
        {
            get
            {
                var lang = this.ViewContext.RequestContext.RouteData.Values["lang"].ToString();

                return string.IsNullOrWhiteSpace(lang) ? "bg" : lang;
            }
        }

        public string ActionName
        {
            get
            {
                return this.ViewContext.RouteData.Values["action"]?.ToString();
            }
        }

        public Programme GetOP
        {
            get
            {
                if (this.op == null)
                {
                    var routeOp = this.ViewContext.RequestContext.RouteData.Values[RouteConfig.OPABB] != null ? this.ViewContext.RequestContext.RouteData.Values[RouteConfig.OPABB].ToString() : Common.Configuration.OP_DEFAULT_ID.ToString();

                    int opId = Common.Configuration.OP_DEFAULT_ID;
                    if (int.TryParse(routeOp, out opId) && !routeOp.Equals(Common.Configuration.OP_DEFAULT_ID.ToString()))
                    {
                        this.op = ((IInfrastructureRepository)DependencyResolver.Current
                                    .GetService(typeof(IInfrastructureRepository)))
                                    .GetAllOps().Single(e => e.MapNodeId == opId);
                    }
                    else
                    {
                        this.op = new Programme()
                        {
                            MapNodeId = opId,
                            PortalName = Texts.Global_NoFilter,
                            PortalNameAlt = Texts.Global_NoFilter,
                        };
                    }
                }

                return this.op;
            }
        }

        public MapRegion GetPR
        {
            get
            {
                if (this.pr == null)
                {
                    var routePr = this.ViewContext.RequestContext.RouteData.Values[RouteConfig.PRABB] != null ? this.ViewContext.RequestContext.RouteData.Values[RouteConfig.PRABB].ToString() : Common.Configuration.PR_DEFAULT_ID.ToString();

                    int prId = Common.Configuration.PR_DEFAULT_ID;
                    if (int.TryParse(routePr, out prId) && routePr.Equals(Common.Configuration.PR_DEFAULT_ID.ToString()))
                    {
                        this.pr = new MapRegion()
                        {
                            Id = prId,
                            Name = Texts.Global_NoFilter,
                            NameAlt = Texts.Global_NoFilter,
                        };
                    }
                    else if (int.TryParse(routePr, out prId) && routePr.Equals(Common.Configuration.PR_INTERNATIONAL_ID.ToString()))
                    {
                        this.pr = new MapRegion()
                        {
                            Id = prId,
                            Name = Texts.Global_International,
                            NameAlt = Texts.Global_International,
                            RegionId = prId,
                        };
                    }
                    else if (int.TryParse(routePr, out prId) && routePr.Equals(Common.Configuration.PR_BULGARIA_ID.ToString()))
                    {
                        this.pr = new MapRegion()
                        {
                            Id = prId,
                            Name = Texts.Global_Bulgaria,
                            NameAlt = Texts.Global_Bulgaria,
                            NutsLevel = Domain.Entities.Umis.NonAggregates.NutsLevel.Country,
                            RegionId = Common.Configuration.BULGARIA_COUNTRY_ID,
                        };
                    }
                    else
                    {
                        this.pr = ((IMapsRepository)DependencyResolver.Current
                                    .GetService(typeof(IMapsRepository)))
                                    .GetAllMapRegionsByType(MapTypeEnum.BgNuts).Single(e => e.Id == prId);
                    }
                }

                return this.pr;
            }
        }

        public bool HasTabs
        {
            get
            {
                return !(this.OpId == Common.Configuration.OP_DEFAULT_ID);
            }
        }

        public string ScriptSectionName => ViewSectionNames.Scripts;

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
    }
}