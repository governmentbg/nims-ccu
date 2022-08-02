using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class FakePaymentRequestCommunicator : IPaymentRequestCommunicator
    {
        #region Report

        public ContractPaymentRequest GetPaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token) { throw new NotImplementedException(); }

        public ContractPaymentRequest CreatePaymentRequest(Guid contractGid, Guid packageGid, string type, string token) { throw new NotImplementedException(); }

        public ContractPaymentRequest PutPaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token, string xml, byte[] version) { throw new NotImplementedException(); }

        public void DeletePaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token, string version) { throw new NotImplementedException(); }

        public ContractErrors CanCreatePaymentRequest(Guid contractGid, Guid packageGid, string type, string token) { throw new NotImplementedException(); }

        public ContractPaymentRequest SubmitPaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token, byte[] version) { throw new NotImplementedException(); }

        public ContractPaymentRequest MakeDraftPaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token, byte[] version) { throw new NotImplementedException(); }

        public ContractPaymentRequest MakeActualPaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token, byte[] version) { throw new NotImplementedException(); }

        #endregion

        #region Private

        public ContractPaymentRequest PrivateGetPaymentRequest(Guid gid, string token)
        {
            return new ContractPaymentRequest()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractPaymentRequest PrivatePutPaymentRequest(Guid gid, string token, string xml, byte[] version)
        {
            return new ContractPaymentRequest()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractPaymentRequest PrivateSubmitPaymentRequest(Guid gid, string token, byte[] version)
        {
            return new ContractPaymentRequest()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        #endregion
    }
}