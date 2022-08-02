using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.Components.Communicators
{
    public interface IStandpointCommunicator
    {
        #region Private

        //[Route("{xmlGid:guid}")]
        ContractDocumentXml GetEvalSessionStandpointXml(string accessToken, Guid standpointGid);

        //[HttpPut]
        //[Route("{xmlGid:guid}")]
        ContractDocumentXml UpdateEvalSessionStandpointXml(string accessToken, Guid standpointGid, string xml, byte[] version);

        //[HttpPost]
        //[Route("{xmlGid:guid}/submit")]
        void SubmitEvalSessionStandpointXml(string accessToken, Guid standpointGid, byte[] version);

        #endregion
    }
}