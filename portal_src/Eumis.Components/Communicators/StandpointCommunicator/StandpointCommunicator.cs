using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class StandpointCommunicator : IStandpointCommunicator
    {
        #region Private

        public ContractDocumentXml GetEvalSessionStandpointXml(string accessToken, Guid standpointGid)
        {
            return StandpointApi.GetEvalSessionStandpointXml(accessToken, standpointGid).ToObject<ContractDocumentXml>(); ;
        }

        public ContractDocumentXml UpdateEvalSessionStandpointXml(string accessToken, Guid standpointGid, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return StandpointApi.UpdateEvalSessionStandpointXml(accessToken, standpointGid, body).ToObject<ContractDocumentXml>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public void SubmitEvalSessionStandpointXml(string accessToken, Guid standpointGid, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            StandpointApi.SubmitEvalSessionStandpointXml(accessToken, standpointGid, body);
        }

        #endregion
    }
}