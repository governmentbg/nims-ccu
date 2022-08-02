using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class FakeProcurementsCommunicator : IProcurementsCommunicator
    {
        public ContractProcurements GetProcurements(Guid gid, string token)
        {
            return new ContractProcurements()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public IList<ContractCentralProcurement> GetCentralProcurement(string token)
        {
            return new List<ContractCentralProcurement>();
        }

        public ContractProcurements PutProcurements(Guid gid, string token, string xml, byte[] version)
        {
            return new ContractProcurements()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractProcurements SubmitProcurements(Guid gid, string token, byte[] version)
        {
            return new ContractProcurements()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractProcurementPagePVO GetContractProcurements(string accessToken, Guid contractGid, int offset = 0, int? limit = null)
        {
            return new ContractProcurementPagePVO();
        }

        public ContractDocumentXml GetContractProcurement(string accessToken, Guid contractGid, Guid procurementGid)
        {
            return new ContractDocumentXml();
        }

        public ContractDocumentXml CreateContractProcurement(string accessToken, Guid contractGid)
        {
            return new ContractDocumentXml();
        }

        public ContractDocumentXml UpdateContractProcurement(string accessToken, Guid contractGid, Guid procurementGid, string xml, byte[] version)
        {
            return new ContractDocumentXml();
        }

        public void SubmitContractProcurement(string accessToken, Guid contractGid, Guid procurementGid)
        {
            
        }

        public void DeleteContractProcurement(string accessToken, Guid contractGid, Guid procurementGid)
        {
            
        }

        public ContractProcurementDocument GetContractProcurementForEdit(string accessToken, Guid contractGid, Guid gid)
        {
            return new ContractProcurementDocument();
        }
    }
}