using Eumis.Documents.Contracts;
using System;

namespace Eumis.Components.Communicators
{
    public class ProjectCommunicationCommunicator : IProjectCommunicationCommunicator
    {
        #region Public

        public ContractProjectCommunications GetCommunications(Guid registeredGid, string accessToken, int limit, int offset)
        {
            return ProjectCommunicationApi.GetCommunications(registeredGid, accessToken, limit, offset).ToObject<ContractProjectCommunications>();
        }

        public ContractMessageXml GetNewProjectCommunication(Guid registeredGid, string accessToken)
        {
            return ProjectCommunicationApi.GetNewProjectCommunication(registeredGid, accessToken).ToObject<ContractMessageXml>();
        }

        public void DeleteProjectCommunication(Guid communicationGid, string accessToken)
        {
            try
            {
                ProjectCommunicationApi.DeleteProjectCommunication(communicationGid, accessToken);
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
            }
        }

        public void CancelProjectCommunication(Guid communicationGid, byte[] version, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version,
                });

            ProjectCommunicationApi.CancelProjectCommunication(communicationGid, accessToken, body);
        }

        public ContractMessageXml GetProjectCommunication(Guid communicationGid, string accessToken)
        {
            return ProjectCommunicationApi.GetProjectCommunication(communicationGid, accessToken).ToObject<ContractMessageXml>();
        }

        public ContractMessageXml PutProjectCommunication(Guid communicationGid, string xml, byte[] version, string subject, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version,
                    subject = subject
                });

            try
            {
                return ProjectCommunicationApi.PutProjectCommunication(communicationGid, accessToken, body).ToObject<ContractMessageXml>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public void SubmitProjectCommunication(Guid communicationGid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            try
            {
                ProjectCommunicationApi.SubmitProjectCommunication(communicationGid, token, body);
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
            }
        }

        public ContractProjectCommunicationSentPVO GetSentProjectCommunicationInfo(Guid communicationGid, string accessToken)
        {
            return ProjectCommunicationApi.GetSentProjectCommunicationInfo(communicationGid, accessToken).ToObject<ContractProjectCommunicationSentPVO>();
        }

        public ContractMessageXml GetProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string accessToken)
        {
            return ProjectCommunicationApi.GetProjectCommunicationAnswer(communicationGid, answerGid, accessToken).ToObject<ContractMessageXml>();
        }

        public ContractMessageXml GetNewProjectCommunicationAnswer(Guid gid, string accessToken)
        {
            try
            {
                return ProjectCommunicationApi.GetNewProjectCommunicationAnswer(gid, accessToken).ToObject<ContractMessageXml>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public void DeleteProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, byte[] version, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            try
            {
                ProjectCommunicationApi.DeleteProjectCommunicationAnswer(communicationGid, answerGid, accessToken, body);
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
            }
        }

        public ContractMessageXml PutProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string xml, byte[] version, string token)
        {

            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return ProjectCommunicationApi.PutProjectCommunicationAnswer(communicationGid, answerGid, token, body).ToObject<ContractMessageXml>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public void SubmitProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            try
            {
                ProjectCommunicationApi.SubmitProjectCommunicationAnswer(communicationGid, answerGid, token, body);
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
            }
        }

        public ContractProjectCommunicationSentPVO GetSentProjectCommunicationAnswerInfo(Guid answerGid, string accessToken)
        {
            return ProjectCommunicationApi.GetSentProjectCommunicationAnswerInfo(answerGid, accessToken).ToObject<ContractProjectCommunicationSentPVO>();
        }

        public bool AssertProjectHasCommunications(Guid registeredGid, string accessToken)
        {
            return ProjectCommunicationApi.AssertProjectHasCommunications(registeredGid, accessToken);
        }

        public bool UserHasNewCommunnications(string accessToken)
        {
            return ProjectCommunicationApi.UserHasNewCommunnications(accessToken);
        }

        #endregion

        #region Private

        public ContractMessageXml PrivateGetProjectCommunication(Guid gid, string accessToken)
        {
            return ProjectCommunicationApi.PrivateGetProjectCommunication(gid, accessToken).ToObject<ContractMessageXml>();
        }

        public ContractMessageXml PrivatePutProjectCommunication(Guid gid, string token, string xml, byte[] version, string subject)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version,
                    subject = subject
                });

            try
            {
                return ProjectCommunicationApi.PrivatePutProjectCommunication(gid, token, body).ToObject<ContractMessageXml>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public void PrivateSubmitProjectCommunication(Guid gid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });


            try
            {
                ProjectCommunicationApi.PrivateSubmitProjectCommunication(gid, token, body);
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
            }
        }

        public ContractMessageXml PrivateGetProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string accessToken)
        {
            return ProjectCommunicationApi.PrivateGetProjectCommunicationAnswer(communicationGid, answerGid, accessToken).ToObject<ContractMessageXml>();
        }

        public ContractMessageXml PrivatePutProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return ProjectCommunicationApi.PrivatePutProjectCommunicationAnswer(communicationGid, answerGid, token, body).ToObject<ContractMessageXml>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public void PrivateSubmitProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                ProjectCommunicationApi.PrivateSubmitProjectCommunicationAnswer(communicationGid, answerGid, token, body);
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
            }
        }

        #endregion
    }
}
