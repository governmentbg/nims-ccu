using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class PaymentRequestCommunicator : IPaymentRequestCommunicator
    {
        #region Report

        public ContractPaymentRequest GetPaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token)
        {
            return PaymentRequestApi.GetPaymentRequest(contractGid, packageGid, paymentRequestGid, token).ToObject<ContractPaymentRequest>();
        }

        public ContractPaymentRequest CreatePaymentRequest(Guid contractGid, Guid packageGid, string type, string token)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                });

            return PaymentRequestApi.CreatePaymentRequest(contractGid, packageGid, type, token, body).ToObject<ContractPaymentRequest>();
        }

        public ContractPaymentRequest PutPaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return PaymentRequestApi.PutPaymentRequest(contractGid, packageGid, paymentRequestGid, token, body).ToObject<ContractPaymentRequest>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public void DeletePaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token, string version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            PaymentRequestApi.DeletePaymentRequest(contractGid, packageGid, paymentRequestGid, token, body);
        }

        public ContractErrors CanCreatePaymentRequest(Guid contractGid, Guid packageGid, string type, string token)
        {
            return PaymentRequestApi.CanCreatePaymentRequest(contractGid, packageGid, type, token).ToObject<ContractErrors>();
        }

        public ContractPaymentRequest SubmitPaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return PaymentRequestApi.SubmitPaymentRequest(contractGid, packageGid, paymentRequestGid, token, body).ToObject<ContractPaymentRequest>();
        }

        public ContractPaymentRequest MakeDraftPaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return PaymentRequestApi.MakeDraftPaymentRequest(contractGid, packageGid, paymentRequestGid, token, body).ToObject<ContractPaymentRequest>();
        }

        public ContractPaymentRequest MakeActualPaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return PaymentRequestApi.MakeActualPaymentRequest(contractGid, packageGid, paymentRequestGid, token, body).ToObject<ContractPaymentRequest>();
        }

        #endregion

        #region Private

        public ContractPaymentRequest PrivateGetPaymentRequest(Guid gid, string token)
        {
            return PaymentRequestApi.PrivateGetPaymentRequest(gid, token).ToObject<ContractPaymentRequest>();
        }

        public ContractPaymentRequest PrivatePutPaymentRequest(Guid gid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return PaymentRequestApi.PrivatePutPaymentRequest(gid, token, body).ToObject<ContractPaymentRequest>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public ContractPaymentRequest PrivateSubmitPaymentRequest(Guid gid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return PaymentRequestApi.PrivateSubmitPaymentRequest(gid, token, body).ToObject<ContractPaymentRequest>();
        }

        #endregion
    }
}