using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.Common.Api;
using Eumis.Data.NonAggregates.Repositories.Repos;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Core;
using Eumis.Domain.Registrations;
using Eumis.Domain.RioExtensions;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Portal.Projects.DataObjects;
using Eumis.Rio;

namespace Eumis.PortalIntegration.Api.Portal.Projects.Controllers
{
    public class ProjectsController : ApiController
    {
        private IProceduresRepository proceduresRepository;
        private IProgrammesRepository programmesRepository;
        private IBlobsRepository blobsRepository;

        public ProjectsController(
            IProceduresRepository proceduresRepository,
            IProgrammesRepository programmesRepository,
            IBlobsRepository blobsRepository)
        {
            this.proceduresRepository = proceduresRepository;
            this.programmesRepository = programmesRepository;
            this.blobsRepository = blobsRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/projects/validate")]
        public List<ProjectValidationErrorDO> ValidateDraft(XmlDO projectXmlDO)
        {
            // create a RioProjectXmlDocument just for the validation
            var projectXmlDoc = new RioXmlDocument<Eumis.Rio.Project>();
            projectXmlDoc.SetXml(projectXmlDO.Xml);

            return this.ValidateRegProjectXml(projectXmlDoc.GetDocument());
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/projects/resurrectFiles")]
        [Transaction]
        public void ResurrectFiles(XmlDO projectXmlDO)
        {
            this.blobsRepository.ResurrectBlobs(RegProjectXml.GetFilesFromXml(projectXmlDO.Xml).Select(f => f.BlobKey));
        }

        private List<ProjectValidationErrorDO> ValidateRegProjectXml(Eumis.Rio.Project projectXml)
        {
            string procedureCode = projectXml.Get(d => d.ProjectBasicData.Procedure.Code);

            var procedure = this.proceduresRepository.FindByCode(procedureCode);
            var programmeAttributesDictionary = this.programmesRepository.GetProgrammesIdCodeAndShortName();

            var errors = projectXml.Validate(procedure, programmeAttributesDictionary);

            return errors.Select(e => new ProjectValidationErrorDO(e.Item1, e.Item2, e.Item3)).ToList();
        }
    }
}
