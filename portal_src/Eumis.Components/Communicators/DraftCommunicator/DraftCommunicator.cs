using System.Linq;
using Eumis.Documents.Contracts;
using System.Collections.Generic;
using System;
using System.Net;
namespace Eumis.Components.Communicators
{
    public class DraftCommunicator : IDraftCommunicator
    {
        public ContractDraft GetDrafts(string accessToken, int limit, int offset)
        {
            var draft = DraftApi.GetDrafts(accessToken, limit, offset).ToObject<ContractDraft>();

            if (draft != null && draft.results != null && draft.results.Count > 0)
            {
                // Order by: 
                // 1. ending date after current date
                // 2. closest ending date first
                // 3. closest modify date first
                var now = DateTime.Now;

                draft.results = draft.results
                    .OrderBy(e => e.procedureEndingDate < now)
                    .ThenBy(e => (now - e.procedureEndingDate).Duration())
                    .ThenBy(e => (now - e.modifyDate).Duration())
                    .ToList();
            }

            return draft;
        }

        public ContractFinalized GetFinalizedProjects(string accessToken, int limit, int offset)
        {
            var finalized = DraftApi.GetFinalizedProjects(accessToken, limit, offset).ToObject<ContractFinalized>();

            if (finalized != null && finalized.results != null && finalized.results.Count > 0)
            {
                // Order by: 
                // 1. ending date after current date
                // 2. closest ending date first
                // 3. closest modify date first
                var now = DateTime.Now;

                finalized.results = finalized.results
                    .OrderBy(e => e.procedureEndingDate < now)
                    .ThenBy(e => (now - e.procedureEndingDate).Duration())
                    .ThenBy(e => (now - e.modifyDate).Duration())
                    .ToList();
            }

            return finalized;
        }

        public ContractRegistered GetRegisteredProjects(string accessToken, int limit, int offset)
        {
            var registered = DraftApi.GetRegisteredProjects(accessToken, limit, offset).ToObject<ContractRegistered>();

            if (registered != null && registered.results != null && registered.results.Count > 0)
            {
                registered.results = registered.results
                    .OrderByDescending(e => e.modifyDate)
                    .ToList();

                foreach (var result in registered.results)
                {
                    if (result != null && result.messages != null && result.messages.Count > 0)
                    {
                        result.messages = result.messages
                            .OrderByDescending(e => e.replyDate)
                            .ToList();
                    }
                }
            }

            return registered;
        }

        public ContractRegistered GetRegisteredProjectsCommunications(string accessToken, int limit, int offset)
        {
            return DraftApi.GetRegisteredProjectsCommunications(accessToken, limit, offset).ToObject<ContractRegistered>();
        }

        public ContractSubmitted GetSubmittedProjects(string accessToken, int limit, int offset)
        {
            var submitted = DraftApi.GetSubmittedProjects(accessToken, limit, offset).ToObject<ContractSubmitted>();

            if (submitted != null && submitted.results != null && submitted.results.Count > 0)
            {
                // Order by: 
                // 1. ending date after current date
                // 2. closest ending date first
                // 3. closest modify date first
                var now = DateTime.Now;

                submitted.results = submitted.results
                    .OrderBy(e => e.procedureEndingDate < now)
                    .ThenBy(e => (now - e.procedureEndingDate).Duration())
                    .ThenBy(e => (now - e.modifyDate).Duration())
                    .ToList();
            }

            return submitted;
        }

        public ContractDocumentXml GetDraft(System.Guid gid, string accessToken)
        {
            return DraftApi.GetDraft(gid, accessToken).ToObject<ContractDocumentXml>();
        }

        public ContractDocumentXml GetProjectVersion(System.Guid gid, string accessToken)
        {
            return DraftApi.GetProjectVersion(gid, accessToken).ToObject<ContractDocumentXml>();
        }

        public ContractDocumentXml CreateDraft(string xml, byte[] version, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });
            
            try
            {
                return DraftApi.CreateDraft(body, accessToken).ToObject<ContractDocumentXml>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public ContractDocumentXml UpdateDraft(Guid gid, string xml, byte[] version, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return DraftApi.UpdateDraft(gid, body, accessToken).ToObject<ContractDocumentXml>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public void DeleteDraft(Guid gid, string xml, byte[] version, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            DraftApi.DeleteDraft(gid, body, accessToken);
        }

        public void FinalizeDraft(Guid gid, byte[] version, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            DraftApi.FinalizeDraft(gid, body, accessToken);
        }

        public void DefinalizeDraft(Guid gid, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new { });

            DraftApi.DefinalizeDraft(gid, body, accessToken);
        }

        public string Submit(Guid gid, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new { });

            return DraftApi.Submit(gid, body, accessToken).Value<string>("hash");
        }

        public string Register(byte[] isun, List<byte[]> signatures, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                isun = isun,
                signatures = signatures
            });

            return DraftApi.Register(body, accessToken).Value<string>("regNumber");
        }
    }
}