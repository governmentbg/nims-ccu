using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class FakeSpendingPlanCommunicator : ISpendingPlanCommunicator
    {
        public ContractSpendingPlan GetSpendingPlan(Guid gid, string token)
        {
            return new ContractSpendingPlan()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractSpendingPlan PutSpendingPlan(Guid gid, string token, string xml, byte[] version)
        {
            return new ContractSpendingPlan()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractSpendingPlan SubmitSpendingPlan(Guid gid, string token, byte[] version)
        {
            return new ContractSpendingPlan()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractSpendingPlanPagePVO GetContractSpendingPlans(string accessToken, Guid contractGid, int offset = 0, int? limit = null)
        {
            return new ContractSpendingPlanPagePVO();
        }

        public ContractDocumentXml GetContractSpendingPlan(string accessToken, Guid contractGid, Guid spendingPlanGid)
        {
            return new ContractDocumentXml();
        }

        public ContractDocumentXml CreateContractSpendingPlan(string accessToken, Guid contractGid)
        {
            return new ContractDocumentXml();
        }

        public ContractDocumentXml UpdateContractSpendingPlan(string accessToken, Guid contractGid, Guid spendingPlanGid, string xml, byte[] version)
        {
            return new ContractDocumentXml();
        }

        public void SubmitContractSpendingPlan(string accessToken, Guid contractGid, Guid spendingPlanGid)
        {
            
        }

        public void DeleteContractSpendingPlan(string accessToken, Guid contractGid, Guid spendingPlanGid)
        {
            
        }
    }
}