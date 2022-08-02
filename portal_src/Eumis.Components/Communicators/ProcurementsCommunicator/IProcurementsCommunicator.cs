using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.Components.Communicators
{
    public interface IProcurementsCommunicator
    {
        ContractProcurements GetProcurements(Guid gid, string token);

        ContractProcurements PutProcurements(Guid gid, string token, string xml, byte[] version);

        ContractProcurements SubmitProcurements(Guid gid, string token, byte[] version);

        IList<ContractCentralProcurement> GetCentralProcurement(string token);

        //[RoutePrefix("api/contractreg/contracts/{contractGid:guid}/procurements")]
        ContractProcurementPagePVO GetContractProcurements(string accessToken, Guid contractGid, int offset = 0, int? limit = null);

        //[Route("{procurementGid:guid}")]
        ContractDocumentXml GetContractProcurement(string accessToken, Guid contractGid, Guid procurementGid);

        //[Route("{procurementGid:guid/edit}")]
        ContractProcurementDocument GetContractProcurementForEdit(string accessToken, Guid contractGid, Guid gid);

        //[HttpPost]
        //[Route("")]
        ContractDocumentXml CreateContractProcurement(string accessToken, Guid contractGid);

        //[HttpPut]
        //[Route("{procurementGid:guid}")]
        ContractDocumentXml UpdateContractProcurement(string accessToken, Guid contractGid, Guid procurementGid, string xml, byte[] version);

        //[HttpPost]
        //[Route("{procurementGid:guid}/submit")]
        void SubmitContractProcurement(string accessToken, Guid contractGid, Guid procurementGid);

        //[HttpDelete]
        //[Route("{procurementGid:guid}")]
        void DeleteContractProcurement(string accessToken, Guid contractGid, Guid procurementGid);
    }
}