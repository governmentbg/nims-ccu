using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class CommunicationCommunicator : ICommunicationCommunicator
    {
        #region Report

        public ContractCommunicationInfos GetCommunications(Guid contractGid, string type, string token, int limit, int offset)
        {
            return CommunicationApi.GetCommunications(contractGid, type, token, limit, offset).ToObject<ContractCommunicationInfos>();
        }

        public ContractCommunication GetCommunication(Guid contractGid, Guid communicationGid, string token)
        {
            return CommunicationApi.GetCommunication(contractGid, communicationGid, token).ToObject<ContractCommunication>();
        }

        public void DeleteCommunication(Guid contractGid, Guid communicationGid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            CommunicationApi.DeleteCommunication(contractGid, communicationGid, body, token);
        }

        public ContractCommunication CreateCommunication(Guid contractGid, string type, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            return CommunicationApi.CreateCommunication(contractGid, type, token, body).ToObject<ContractCommunication>();
        }

        public ContractDocumentXml PutCommunication(Guid contractGid, Guid communicationGid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return CommunicationApi.PutCommunication(contractGid, communicationGid, token, body).ToObject<ContractDocumentXml>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public ContractDocumentXml SubmitCommunication(Guid contractGid, Guid communicationGid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return CommunicationApi.SubmitCommunication(contractGid, communicationGid, token, body).ToObject<ContractDocumentXml>();
        }

        #endregion

        #region Private

        public ContractCommunication PrivateGetCommunication(Guid communicationGid, string token)
        {
            return CommunicationApi.PrivateGetCommunication(communicationGid, token).ToObject<ContractCommunication>();
        }

        public ContractDocumentXml PrivatePutCommunication(Guid communicationGid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return CommunicationApi.PrivatePutCommunication(communicationGid, token, body).ToObject<ContractDocumentXml>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public ContractDocumentXml PrivateSubmitCommunication(Guid communicationGid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return CommunicationApi.PrivateSubmitCommunication(communicationGid, token, body).ToObject<ContractDocumentXml>();
        }

        #endregion
    }
}