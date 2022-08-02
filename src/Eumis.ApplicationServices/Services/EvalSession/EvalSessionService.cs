using Eumis.ApplicationServices.Communicators;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Data.Projects.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Eumis.ApplicationServices.Services.EvalSession.Parsers
{
    internal class EvalSessionService : IEvalSessionService
    {
        private IEvalSessionsRepository evalSessionsRepository;
        private IProjectsRepository projectsRepository;
        private IBlobServerCommunicator blobServerCommunicator;
        private IEvalSessionProjectParser evalSessionProjectParser;

        public EvalSessionService(
            IEvalSessionsRepository evalSessionsRepository,
            IProjectsRepository projectsRepository,
            IBlobServerCommunicator blobServerCommunicator,
            IEvalSessionProjectParser evalSessionProjectParser)
        {
            this.evalSessionsRepository = evalSessionsRepository;
            this.projectsRepository = projectsRepository;
            this.blobServerCommunicator = blobServerCommunicator;
            this.evalSessionProjectParser = evalSessionProjectParser;
        }

        public EvalSessionLoadedProjectsFromFileVO ParseProjectsExcelFile(int evalSessionId, Guid blobKey)
        {
            var projectIds = new List<int>();
            var errors = new List<string>();
            IList<string> projectRegNumbers = new List<string>();

            try
            {
                using (var excelStream = this.blobServerCommunicator.GetBlobStream(blobKey, true))
                {
                    projectRegNumbers = this.evalSessionProjectParser.ParseExcel(excelStream);
                }
            }
            catch (FileFormatException)
            {
                errors.Add(ApplicationServicesTexts.Common_InvalidFileFormat);
            }

            if (errors.Any() || projectRegNumbers.Count == 0)
            {
                return new EvalSessionLoadedProjectsFromFileVO(projectIds, errors);
            }

            var evalSessionProjectIds = this.evalSessionsRepository.GetEvalSessionProjectIds(evalSessionId);

            foreach (var projectRegNumber in projectRegNumbers)
            {
                var projectId = this.projectsRepository.GetProjectId(projectRegNumber);

                if (!projectId.HasValue)
                {
                    errors.Add(string.Format(ApplicationServicesTexts.EvalSessionService_ParseProjectsExcelFile_ProjectNotFound, projectRegNumber));
                    continue;
                }
                else if (!evalSessionProjectIds.Contains(projectId.Value))
                {
                    errors.Add(string.Format(ApplicationServicesTexts.EvalSessionService_ParseProjectsExcelFile_NotIncluded, projectRegNumber));
                    continue;
                }

                projectIds.Add(projectId.Value);
            }

            return new EvalSessionLoadedProjectsFromFileVO(projectIds, errors);
        }
    }
}
