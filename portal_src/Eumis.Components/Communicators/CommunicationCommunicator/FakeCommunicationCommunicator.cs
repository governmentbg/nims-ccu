using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class FakeCommunicationCommunicator : ICommunicationCommunicator
    {
        #region Report

        public ContractCommunicationInfos GetCommunications(Guid contractGid, string type, string token, int limit, int offset)
        {
            return new ContractCommunicationInfos();
        }

        public ContractCommunication GetCommunication(Guid contractGid, Guid communicationGid, string token)
        {
            return new ContractCommunication();
        }

        public void DeleteCommunication(Guid contractGid, Guid communicationGid, string token, string xml, byte[] version)
        {

        }

        public ContractCommunication CreateCommunication(Guid contractGid, string type, string token, string xml, byte[] version)
        {
            return new ContractCommunication();
        }

        public ContractDocumentXml PutCommunication(Guid contractGid, Guid communicationGid, string token, string xml, byte[] version)
        {
            return new ContractDocumentXml();
        }

        public ContractDocumentXml SubmitCommunication(Guid contractGid, Guid communicationGid, string token, byte[] version)
        {
            return new ContractDocumentXml();
        }

        #endregion

        #region Private

        public ContractCommunication PrivateGetCommunication(Guid communicationGid, string token)
        {
            return new ContractCommunication();
        }

        public ContractDocumentXml PrivatePutCommunication(Guid communicationGid, string token, string xml, byte[] version)
        {
            return new ContractDocumentXml();
        }

        public ContractDocumentXml PrivateSubmitCommunication(Guid communicationGid, string token, byte[] version)
        {
            return new ContractDocumentXml();
        }

        #endregion
    }
}