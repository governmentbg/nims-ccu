using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Db;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Procedures.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/procedures/{procedureId}/applicationSections")]
    public class ProcedureApplicationSectionsController : ApiController
    {
        private IProceduresRepository proceduresRepository;
        private IAuthorizer authorizer;
        private IUnitOfWork unitOfWork;

        public ProcedureApplicationSectionsController(
            IAuthorizer authorizer,
            IProceduresRepository proceduresRepository,
            IUnitOfWork unitOfWork)
        {
            this.proceduresRepository = proceduresRepository;
            this.authorizer = authorizer;
            this.unitOfWork = unitOfWork;
        }

        [Route("")]
        public ApplicationSectionDO GetProcedureApplicationSections(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);
            var procedure = this.proceduresRepository.FindWithoutIncludes(procedureId);
            var procedureApplicationSections = this.proceduresRepository.GetApplicationSections(procedureId);

            return new ApplicationSectionDO(procedureApplicationSections, procedure.Version);
        }

        [HttpPost]
        [Route("")]
        public void AddProcedureApplicationSections(int procedureId, ApplicationSectionDO data)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            var procedure = this.proceduresRepository.FindForUpdate(procedureId, data.Version);
            var sections = data.Sections
                .Select(x => (x.ApplicationSection, x.IsSelected.Value, x.OrderNum))
                .ToList();

            procedure.InsertOrUpdateApplicationSections(sections);

            var additionalSettings = data.Sections
                .SelectMany(x => x.AdditionalSettings)
                .Select(x => (x.Type, x.IsSelected))
                .ToList();

            procedure.InsertOrUpdateApplicationSectionAdditionalSettings(additionalSettings);

            this.unitOfWork.Save();
        }
    }
}
