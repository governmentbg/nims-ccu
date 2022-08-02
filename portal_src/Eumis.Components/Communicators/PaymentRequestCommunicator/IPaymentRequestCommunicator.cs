using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.Components.Communicators
{
    public interface IPaymentRequestCommunicator
    {
        #region Report

        ContractPaymentRequest GetPaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token);

        ContractPaymentRequest CreatePaymentRequest(Guid contractGid, Guid packageGid, string type, string token);

        ContractPaymentRequest PutPaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token, string xml, byte[] version);

        void DeletePaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token, string version);

        ContractErrors CanCreatePaymentRequest(Guid contractGid, Guid packageGid, string type, string token);

        ContractPaymentRequest SubmitPaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token, byte[] version);

        ContractPaymentRequest MakeDraftPaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token, byte[] version);

        ContractPaymentRequest MakeActualPaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token, byte[] version);

        #endregion

        #region Private

        ContractPaymentRequest PrivateGetPaymentRequest(Guid gid, string token);

        ContractPaymentRequest PrivatePutPaymentRequest(Guid gid, string token, string xml, byte[] version);

        ContractPaymentRequest PrivateSubmitPaymentRequest(Guid gid, string token, byte[] version);

        #endregion
    }
}