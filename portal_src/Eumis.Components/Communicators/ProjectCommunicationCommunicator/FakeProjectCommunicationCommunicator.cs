using Eumis.Documents.Contracts;
using System;

namespace Eumis.Components.Communicators
{
    public class FakeProjectCommunicationCommunicator : IProjectCommunicationCommunicator
    {
        #region Public

        public ContractProjectCommunications GetCommunications(Guid registeredGid, string accessToken, int limit, int offset)
        {
            throw new NotImplementedException();
        }

        public ContractMessageXml GetNewProjectCommunication(Guid registeredGid, string accessToken)
        {
            throw new NotImplementedException();
        }

        public ContractMessageXml GetProjectCommunication(Guid communicationGid, string accessToken)
        {
            throw new NotImplementedException();
        }

        public ContractMessageXml PutProjectCommunication(Guid communicationGid, string xml, byte[] version, string subject, string accessToken)
        {
            throw new NotImplementedException();
        }

        public void SubmitProjectCommunication(Guid communicationGid, string token, byte[] version)
        {
            throw new NotImplementedException();
        }

        public void DeleteProjectCommunication(Guid communicationGid, string accessToken)
        {
            throw new NotImplementedException();
        }

        public void CancelProjectCommunication(Guid communicationGid, byte[] version, string accessToken)
        {
            throw new NotImplementedException();
        }

        public ContractProjectCommunicationSentPVO GetSentProjectCommunicationInfo(Guid communicationGid, string accessToken)
        {
            throw new NotImplementedException();
        }

        public ContractMessageXml GetProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string accessToken)
        {
            throw new NotImplementedException();
        }

        public ContractMessageXml GetNewProjectCommunicationAnswer(Guid communicationGid, string accessToken)
        {
            throw new NotImplementedException();
        }

        public void DeleteProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, byte[] version, string accessToken)
        {
            throw new NotImplementedException();
        }

        public ContractMessageXml PutProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string xml, byte[] version, string accessToken)
        {
            throw new NotImplementedException();
        }

        public void SubmitProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string token, byte[] version)
        {
            throw new NotImplementedException();
        }

        public ContractProjectCommunicationSentPVO GetSentProjectCommunicationAnswerInfo(Guid answerGid, string accessToken)
        {
            throw new NotImplementedException();
        }

        public bool AssertProjectHasCommunications(Guid registeredGid, string accessToken)
        {
            throw new NotImplementedException();
        }

        public bool UserHasNewCommunnications(string accessToken)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private

        public ContractMessageXml PrivateGetProjectCommunication(Guid gid, string accessToken)
        {
            throw new NotImplementedException();
        }

        public ContractMessageXml PrivatePutProjectCommunication(Guid gid, string token, string xml, byte[] version, string subject)
        {
            throw new NotImplementedException();
        }

        public void PrivateSubmitProjectCommunication(Guid gid, string token, string xml, byte[] version)
        {
            throw new NotImplementedException();
        }

        public ContractMessageXml PrivateGetProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string access_token)
        {
            throw new NotImplementedException();
        }

        public ContractMessageXml PrivatePutProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string token, string xml, byte[] version)
        {
            throw new NotImplementedException();
        }

        public void PrivateSubmitProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string token, string xml, byte[] version)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
