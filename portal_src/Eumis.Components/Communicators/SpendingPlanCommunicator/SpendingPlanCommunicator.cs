using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class SpendingPlanCommunicator : ISpendingPlanCommunicator
    {
        public ContractSpendingPlan GetSpendingPlan(Guid gid, string token)
        {
            return SpendingPlanApi.GetSpendingPlan(gid, token).ToObject<ContractSpendingPlan>();
        }

        public ContractSpendingPlan PutSpendingPlan(Guid gid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return SpendingPlanApi.PutSpendingPlan(gid, token, body).ToObject<ContractSpendingPlan>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public ContractSpendingPlan SubmitSpendingPlan(Guid gid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return SpendingPlanApi.SubmitSpendingPlan(gid, token, body).ToObject<ContractSpendingPlan>();
        }

        public ContractSpendingPlanPagePVO GetContractSpendingPlans(string accessToken, Guid contractGid, int offset = 0, int? limit = null)
        {
            return SpendingPlanApi.GetContractSpendingPlans(accessToken, contractGid, offset, limit).ToObject<ContractSpendingPlanPagePVO>();
        }

        public ContractDocumentXml GetContractSpendingPlan(string accessToken, Guid contractGid, Guid spendingPlanGid)
        {
            return SpendingPlanApi.GetContractSpendingPlan(accessToken, contractGid, spendingPlanGid).ToObject<ContractDocumentXml>();
        }

        public ContractDocumentXml CreateContractSpendingPlan(string accessToken, Guid contractGid)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                });

            try
            {
                return SpendingPlanApi.CreateContractSpendingPlan(accessToken, contractGid, body).ToObject<ContractDocumentXml>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public ContractDocumentXml UpdateContractSpendingPlan(string accessToken, Guid contractGid, Guid spendingPlanGid, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return SpendingPlanApi.UpdateContractSpendingPlan(accessToken, contractGid, spendingPlanGid, body).ToObject<ContractDocumentXml>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public void SubmitContractSpendingPlan(string accessToken, Guid contractGid, Guid spendingPlanGid)
        {
            SpendingPlanApi.SubmitContractSpendingPlan(accessToken, contractGid, spendingPlanGid);
        }

        public void DeleteContractSpendingPlan(string accessToken, Guid contractGid, Guid spendingPlanGid)
        {
            SpendingPlanApi.DeleteContractSpendingPlan(accessToken, contractGid, spendingPlanGid);
        }
    }
}