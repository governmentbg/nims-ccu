using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.Components.Communicators
{
    public interface IBFPContractCommunicator
    {
        ContractBFPContractData GetContractData(Guid procedureGid, string programmeCode);

        ContractBFPContract GetBFPContract(Guid gid, string token);

        ContractBFPContract PutBFPContract(Guid gid, string token, string xml, byte[] version);

        ContractBFPContract SubmitBFPContract(Guid gid, string token, byte[] version);

        ContractsPVO GetContracts(string accessToken, int offset = 0, int? limit = null);

        ContractBFPContractMetadata GetContractMetadata(string accessToken, Guid gid);

        ContractPagePVO<ContractVersionPVO> GetContractVersions(string accessToken, Guid contractGid, int offset = 0, int? limit = null);
        ContractDocumentXml GetContractVersion(string accessToken, Guid contractGid, Guid versionGid);
        ActualContractDO GetActualContractVersion(string accessToken, Guid contractGid);

        List<ContractProcedureApplicationSection> GetProcedureApplciationSections(Guid gid, string token);
    }
}
