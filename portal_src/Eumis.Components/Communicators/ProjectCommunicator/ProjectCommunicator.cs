using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;

namespace Eumis.Components.Communicators
{
    public class ProjectCommunicator : IProjectCommunicator
    {
        public ContractProject GetProject(Guid gid, string token)
        {
            return ProjectApi.GetProject(gid, token).ToObject<ContractProject>();
        }

        public ContractProject PutProject(Guid gid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return ProjectApi.PutProject(gid, token, body).ToObject<ContractProject>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public void SubmitProject(Guid gid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            ProjectApi.Submit(gid, token, body);
        }

        public List<ContractValidationError> ValidateDraft(string xml, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml
                });

            return ProjectApi.ValidateDraft(body, accessToken).ToObject<List<ContractValidationError>>();
        }

        public void ResurrectFiles(string xml)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml
                });

            ProjectApi.ResurrectFiles(body);
        }

        public byte[] GetProjectFilesZip(string projectGid, string accessToken)
        {
            var zipFile = ProjectApi.GetProjectFilesZip(projectGid, accessToken).Value<string>("zipFile");

            return System.Convert.FromBase64String(zipFile);
        }
    }
}
