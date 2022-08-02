using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class BFPContractCommunicator : IBFPContractCommunicator
    {
        ContractBFPContractData IBFPContractCommunicator.GetContractData(Guid procedureGid, string programmeCode)
        {
            return BFPContractApi.GetContractData(procedureGid, programmeCode).ToObject<ContractBFPContractData>();
        }

        public ContractBFPContract GetBFPContract(Guid gid, string token)
        {
            return BFPContractApi.GetBFPContract(gid, token).ToObject<ContractBFPContract>();
        }

        public ContractBFPContract PutBFPContract(Guid gid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return BFPContractApi.PutBFPContract(gid, token, body).ToObject<ContractBFPContract>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public ContractBFPContract SubmitBFPContract(Guid gid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return BFPContractApi.SubmitBFPContract(gid, token, body).ToObject<ContractBFPContract>();
        }

        public List<ContractProcedureApplicationSection> GetProcedureApplciationSections(Guid gid, string token)
        {
            return BFPContractApi.GetBFPContractApplicationSections(gid, token).ToObject<List<ContractProcedureApplicationSection>>();
        }

        public ContractsPVO GetContracts(string accessToken, int offset = 0, int? limit = null)
        {
            return BFPContractApi.GetContracts(accessToken, offset, limit).ToObject<ContractsPVO>();
        }

        public ContractBFPContractMetadata GetContractMetadata(string accessToken, Guid gid)
        {
            return BFPContractApi.GetContractMetadata(accessToken, gid).ToObject<ContractBFPContractMetadata>();
        }

        public ContractPagePVO<ContractVersionPVO> GetContractVersions(string accessToken, Guid contractGid, int offset = 0, int? limit = null)
        {
            return BFPContractApi.GetContractVersions(accessToken, contractGid, offset, limit)
                .ToObject<ContractPagePVO<ContractVersionPVO>>();
        }

        public ContractDocumentXml GetContractVersion(string accessToken, Guid contractGid, Guid versionGid)
        {
            return BFPContractApi.GetContractVersion(accessToken, contractGid, versionGid)
                .ToObject<ContractDocumentXml>();
        }

        public ActualContractDO GetActualContractVersion(string accessToken, Guid contractGid)
        {
            return BFPContractApi.GetActualContractVersion(accessToken, contractGid)
                .ToObject<ActualContractDO>();
        }
    }
}