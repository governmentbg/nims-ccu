using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class EvalCommunicator : IEvalCommunicator
    {
        public ContractEvalDocument GetEvalTable(Guid gid, string token)
        {
            return EvalApi.GetEvalTable(gid, token).ToObject<ContractEvalDocument>();
        }

        public ContractEvalDocument PutEvalTable(Guid gid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return EvalApi.PutEvalTable(gid, token, body).ToObject<ContractEvalDocument>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public ContractEvalDocument SubmitEvalTable(Guid gid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return EvalApi.SubmitEvalTable(gid, token, body).ToObject<ContractEvalDocument>();
        }

        public ContractEvalDocument GetEvalSheet(Guid gid, string token)
        {
            return EvalApi.GetEvalSheet(gid, token).ToObject<ContractEvalDocument>();
        }

        public ContractEvalDocument PutEvalSheet(Guid gid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return EvalApi.PutEvalSheet(gid, token, body).ToObject<ContractEvalDocument>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public ContractEvalDocument SubmitEvalSheet(Guid gid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return EvalApi.SubmitEvalSheet(gid, token, body).ToObject<ContractEvalDocument>();
        }

        public ContractEvalDocument PauseEvalSheet(Guid gid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return EvalApi.PauseEvalSheet(gid, token, body).ToObject<ContractEvalDocument>();
        }
    }
}