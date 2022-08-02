using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class FakeStandpointCommunicator : IStandpointCommunicator
    {
        #region Private

        public ContractDocumentXml GetEvalSessionStandpointXml(string accessToken, Guid standpointGid)
        {
            return new ContractDocumentXml();
        }

        public ContractDocumentXml UpdateEvalSessionStandpointXml(string accessToken, Guid standpointGid, string xml, byte[] version)
        {
            return new ContractDocumentXml();
        }

        public void SubmitEvalSessionStandpointXml(string accessToken, Guid standpointGid, byte[] version)
        {
            
        }
        
        #endregion
    }
}