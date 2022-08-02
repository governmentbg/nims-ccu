using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.Components.Communicators
{
    public interface ICommunicationCommunicator
    {
        #region Report

        ContractCommunicationInfos GetCommunications(Guid contractGid, string type, string token, int limit, int offset);

        ContractCommunication GetCommunication(Guid contractGid, Guid communicationGid, string token);

        void DeleteCommunication(Guid contractGid, Guid communicationGid, string token, string xml, byte[] version);

        ContractCommunication CreateCommunication(Guid contractGid, string type, string token, string xml, byte[] version);

        ContractDocumentXml PutCommunication(Guid contractGid, Guid communicationGid, string token, string xml, byte[] version);

        ContractDocumentXml SubmitCommunication(Guid contractGid, Guid communicationGid, string token, byte[] version);

        #endregion

        #region Private

        ContractCommunication PrivateGetCommunication(Guid communicationGid, string token);

        ContractDocumentXml PrivatePutCommunication(Guid communicationGid, string token, string xml, byte[] version);

        ContractDocumentXml PrivateSubmitCommunication(Guid communicationGid, string token, byte[] version);

        #endregion
    }
}