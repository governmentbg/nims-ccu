using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class FakeBFPContractCommunicator : IBFPContractCommunicator
    {
        ContractBFPContractData IBFPContractCommunicator.GetContractData(Guid procedureGid, string programmeCode)
        {
            return new ContractBFPContractData() {level2EuPercent = new List<ContractEuPercentInfo>() };
        }

        public ContractBFPContract GetBFPContract(Guid gid, string token)
        {
            return new ContractBFPContract()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }
        public List<ContractProcedureApplicationSection> GetProcedureApplciationSections(Guid gid, string token)
        {
            return new List<ContractProcedureApplicationSection>();
        }

        public ContractBFPContract PutBFPContract(Guid gid, string token, string xml, byte[] version)
        {
            return new ContractBFPContract()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractBFPContract SubmitBFPContract(Guid gid, string token, byte[] version)
        {
            return new ContractBFPContract()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractsPVO GetContracts(string accessToken, int offset = 0, int? limit = null)
        {
            return new ContractsPVO();
        }

        public ContractBFPContractMetadata GetContractMetadata(string accessToken, Guid gid)
        {
            return new ContractBFPContractMetadata();
        }

        public ContractPagePVO<ContractVersionPVO> GetContractVersions(string accessToken, Guid contractGid, int offset = 0, int? limit = null)
        {
            return new ContractPagePVO<ContractVersionPVO>();
        }

        public ContractDocumentXml GetContractVersion(string accessToken, Guid contractGid, Guid versionGid)
        {
            return new ContractDocumentXml();
        }

        public ActualContractDO GetActualContractVersion(string accessToken, Guid contractGid)
        {
            return new ActualContractDO();
        }
    }
}