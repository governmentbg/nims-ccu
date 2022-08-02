using Eumis.Documents.Contracts;
using System;

namespace Eumis.Components.Communicators
{
    public interface IProjectCommunicationCommunicator
    {
        #region Portal

        ContractProjectCommunications GetCommunications(Guid registeredGid, string accessToken, int limit, int offset);

        ContractMessageXml GetNewProjectCommunication(Guid registeredGid, string accessToken);

        void DeleteProjectCommunication(Guid gid, string accessToken);

        void CancelProjectCommunication(Guid communicationGid, byte[] version, string accessToken);

        ContractMessageXml GetProjectCommunication(Guid communicationGid, string accessToken);

        ContractMessageXml PutProjectCommunication(Guid communicationGid, string xml, byte[] version, string subject, string accessToken);

        void SubmitProjectCommunication(Guid communicationGid, string accessToken, byte[] version);

        ContractProjectCommunicationSentPVO GetSentProjectCommunicationInfo(Guid communicationGid, string accessToken);

        ContractMessageXml GetProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string accessToken);

        ContractMessageXml GetNewProjectCommunicationAnswer(Guid communicationGid, string accessToken);

        void DeleteProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, byte[] version, string accessToken);

        ContractMessageXml PutProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string xml, byte[] version, string accessToken);

        void SubmitProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string token, byte[] version);

        ContractProjectCommunicationSentPVO GetSentProjectCommunicationAnswerInfo(Guid answerGid, string accessToken);

        bool AssertProjectHasCommunications(Guid registeredGid, string accessToken);

        bool UserHasNewCommunnications(string accessToken);

        #endregion

        #region Private

        ContractMessageXml PrivateGetProjectCommunication(Guid gid, string accessToken);

        ContractMessageXml PrivatePutProjectCommunication(Guid gid, string token, string xml, byte[] version, string subject);

        void PrivateSubmitProjectCommunication(Guid gid, string token, string xml, byte[] version);

        ContractMessageXml PrivateGetProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string access_token);

        ContractMessageXml PrivatePutProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string token, string xml, byte[] version);

        void PrivateSubmitProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string token, string xml, byte[] version);

        #endregion
    }
}
