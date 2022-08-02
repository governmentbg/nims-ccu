using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class ProcurementsCommunicator : IProcurementsCommunicator
    {
        public ContractProcurements GetProcurements(Guid gid, string token)
        {
            return ProcurementsApi.GetProcurements(gid, token).ToObject<ContractProcurements>();
        }

        public IList<ContractCentralProcurement> GetCentralProcurement(string token)
        { 
            return ProcurementsApi.GetCentralProcurements(token).ToObject<IList<ContractCentralProcurement>>();
        }

        public ContractProcurements PutProcurements(Guid gid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return ProcurementsApi.PutProcurements(gid, token, body).ToObject<ContractProcurements>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public ContractProcurements SubmitProcurements(Guid gid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return ProcurementsApi.SubmitProcurements(gid, token, body).ToObject<ContractProcurements>();
        }

        public ContractProcurementPagePVO GetContractProcurements(string accessToken, Guid contractGid, int offset = 0, int? limit = null)
        {
            return ProcurementsApi.GetContractProcurements(accessToken, contractGid, offset, limit).ToObject<ContractProcurementPagePVO>();
        }

        public ContractDocumentXml GetContractProcurement(string accessToken, Guid contractGid, Guid procurementGid)
        {
            return ProcurementsApi.GetContractProcurement(accessToken, contractGid, procurementGid).ToObject<ContractDocumentXml>();
        }

        public ContractProcurementDocument GetContractProcurementForEdit(string accessToken, Guid contractGid, Guid procurementGid)
        {
            return ProcurementsApi.GetContractProcurementForEdit(accessToken, contractGid, procurementGid).ToObject<ContractProcurementDocument>();
        }

        public ContractDocumentXml CreateContractProcurement(string accessToken, Guid contractGid)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                });

            try
            {
                return ProcurementsApi.CreateContractProcurement(accessToken, contractGid, body).ToObject<ContractDocumentXml>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public ContractDocumentXml UpdateContractProcurement(string accessToken, Guid contractGid, Guid procurementGid, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return ProcurementsApi.UpdateContractProcurement(accessToken, contractGid, procurementGid, body).ToObject<ContractDocumentXml>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public void SubmitContractProcurement(string accessToken, Guid contractGid, Guid procurementGid)
        {
            ProcurementsApi.SubmitContractProcurement(accessToken, contractGid, procurementGid);
        }

        public void DeleteContractProcurement(string accessToken, Guid contractGid, Guid procurementGid)
        {
            ProcurementsApi.DeleteContractProcurement(accessToken, contractGid, procurementGid);
        }
    }
}