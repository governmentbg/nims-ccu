using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.Components.Communicators
{
    public interface IMessageCommunicator
    {
        #region Private

        ContractMessageXml GetProjectMessage(Guid gid, string accessToken, string type);

        ContractMessageXml GetProjectMessageAnswer(Guid communicationGid, Guid answerGid, string accessToken);

        ContractMessageXml PutProjectMessage(Guid gid, string token, string xml, byte[] version, DateTime? questionEndingDate);

        void SubmitProjectMessage(Guid gid, string token, string xml, byte[] version);

        byte[] GetMessageProjectFilesZip(string messageGid, string answerGid, string messageType, string accessToken);

        byte[] GetQuestionFilesZip(string messageGid, string accessToken);

        byte[] GetAnswerFilesZip(string communicationGid, string answerGid, string accessToken);

        #endregion

        #region Public

        ContractMessage GetMessages(string accessToken);

        ContractMessageXml GetMessage(Guid gid, string accessToken);

        ContractMessageXml UpdateAnswer(Guid communicationGid, Guid answerGid, string xml, byte[] version, string accessToken);

        void FinalizeAnswer(Guid communicationGid, Guid answerGid, byte[] version, string accessToken);

        void DefinalizeAnswer(Guid communicationGid, Guid answerGid, byte[] version, string accessToken);

        ContractMessageXml SubmitAnswer(Guid communicationGid, Guid answerGid, byte[] version, string accessToken);

        ContractSendResult SendAnswer(byte[] isun, List<byte[]> signatures, byte[] version, string accessToken);

        ContractMessageXml GetNewAnswer(Guid communicationGid, byte[] version, string accessToken);

        ContractMessageXml GetAnswer(Guid communicationGid, Guid answerGid, string accessToken);

        void DeleteAnswer(Guid communicationGid, Guid answerGid, byte[] version, string accessToken);

        RegMessageCountPVO GetCounts(string accessToken);

        bool HasNewMessage(string accessToken);

        ContractGetMessagesCount GetMessagesCount(string accessToken);

        #endregion
    }
}
