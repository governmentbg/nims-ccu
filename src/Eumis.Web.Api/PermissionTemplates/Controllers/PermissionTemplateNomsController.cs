using Eumis.Data;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Measures.Repositories;
using Eumis.Data.Measures.ViewObjects;
using Eumis.Domain.Measures;
using Eumis.Domain.PermissionTemplates;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Measures.DataObjects;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace Eumis.Web.Api.PermissionTemplates.Controllers
{
    [RoutePrefix("api/nomenclatures/permissionTemplates")]
    public class PermissionTemplateNomsController : EntityNomsController<PermissionTemplate, EntityNomVO>
    {
        public PermissionTemplateNomsController(IEntityNomsRepository<PermissionTemplate, EntityNomVO> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
