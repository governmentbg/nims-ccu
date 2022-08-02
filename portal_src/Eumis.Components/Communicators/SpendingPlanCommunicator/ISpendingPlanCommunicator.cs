using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.Components.Communicators
{
    public interface ISpendingPlanCommunicator
    {
        ContractSpendingPlan GetSpendingPlan(Guid gid, string token);

        ContractSpendingPlan PutSpendingPlan(Guid gid, string token, string xml, byte[] version);

        ContractSpendingPlan SubmitSpendingPlan(Guid gid, string token, byte[] version);

        //[RoutePrefix("api/contractreg/contracts/{contractGid:guid}/spendingPlan")]
        ContractSpendingPlanPagePVO GetContractSpendingPlans(string accessToken, Guid contractGid, int offset = 0, int? limit = null);

        //[Route("{spendingPlanGid:guid}")]
        ContractDocumentXml GetContractSpendingPlan(string accessToken, Guid contractGid, Guid spendingPlanGid);

        //[HttpPost]
        //[Route("")]
        ContractDocumentXml CreateContractSpendingPlan(string accessToken, Guid contractGid);

        //[HttpPut]
        //[Route("{spendingPlanGid:guid}")]
        ContractDocumentXml UpdateContractSpendingPlan(string accessToken, Guid contractGid, Guid spendingPlanGid, string xml, byte[] version);

        //[HttpPost]
        //[Route("{spendingPlanGid:guid}/submit")]
        void SubmitContractSpendingPlan(string accessToken, Guid contractGid, Guid spendingPlanGid);

        //[HttpDelete]
        //[Route("{spendingPlanGid:guid}")]
        void DeleteContractSpendingPlan(string accessToken, Guid contractGid, Guid spendingPlanGid);
    }
}