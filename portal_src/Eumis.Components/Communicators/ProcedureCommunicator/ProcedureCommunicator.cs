using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;
using Newtonsoft.Json.Linq;
using Eumis.Components.Caches;

namespace Eumis.Components.Communicators
{
    public class ProcedureCommunicator : IProcedureCommunicator
    {
        private IEumisCache eumisCache;

        public ProcedureCommunicator(IEumisCache eumisCache)
        {
            this.eumisCache = eumisCache;
        }

        IList<ContractProcedureProgrammesTree> IProcedureCommunicator.GetActiveProcedureProgrammesTree()
        {
            return ProcedureApi.GetActiveProcedureProgrammesTree().ToObject<List<ContractProcedureProgrammesTree>>();
        }

        IList<ContractProcedureProgrammesTree> IProcedureCommunicator.GetEndedProcedureProgrammesTree()
        {
            return ProcedureApi.GetEndedPProcedureProgrammesTree().ToObject<List<ContractProcedureProgrammesTree>>();
        }

        IList<ContractProcedureProgrammesTree> IProcedureCommunicator.GetPublicDiscussionProcedureProgrammesTree()
        {
            return ProcedureApi.GetPublicDiscussionProcedureProgrammesTree().ToObject<List<ContractProcedureProgrammesTree>>();
        }

        IList<ContractProcedureProgrammesTree> IProcedureCommunicator.GetArchivedPublicDiscussionProcedureProgrammesTree()
        {
            return ProcedureApi.GetArchivedPublicDiscussionProcedureProgrammesTree().ToObject<List<ContractProcedureProgrammesTree>>();
        }

        ContractProcedure IProcedureCommunicator.GetProcedureAppData(Guid procedureGid)
        {
            return this.eumisCache.GetKey(
               procedureGid.ToString(),
               new CacheKey(nameof(IProcedureCommunicator.GetProcedureAppData)),
               () => ProcedureApi.GetProcedureAppData(procedureGid).ToObject<ContractProcedure>());
        }

        ContractProcedureInfo IProcedureCommunicator.GetProcedureInfo(Guid procedureGid)
        {
            return ProcedureApi.GetProcedureInfo(procedureGid).ToObject<ContractProcedureInfo>();
        }

        ContractProcedureInfo IProcedureCommunicator.GetProcedurePublicDiscussionInfo(Guid procedureGid)
        {
            return ProcedureApi.GetProcedurePublicDiscussionInfo(procedureGid).ToObject<ContractProcedureInfo>();
        }

        ContractProcedure IProcedureCommunicator.GetProcedureActualAppData(Guid procedureGid)
        {
            return this.eumisCache.GetKey(
               procedureGid.ToString(),
               new CacheKey(nameof(IProcedureCommunicator.GetProcedureActualAppData)),
               () => ProcedureApi.GetProcedureActualAppData(procedureGid).ToObject<ContractProcedure>());
        }

        public List<ContractProcedureDiscussionsInfo> GetProcedureDiscussionsInfo(Guid id, string searchTerm, int limit, int offset)
        {
            return ProcedureApi.GetProcedureDiscussionsInfo(id, searchTerm, limit, offset).ToObject<List<ContractProcedureDiscussionsInfo>>();
        }

        public void SubmitProcedurePublicDiscussionComment(Guid procedureGid, string accessToken, string senderEmail, string senderName, string commentMessage)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    senderEmail = senderEmail,
                    senderName = senderName,
                    commentMessage = commentMessage
                });

            ProcedureApi.SubmitProcedurePublicDiscussionComment(procedureGid, accessToken, body);
        }

        public void SubmitProcedureDiscussionQuestion(Guid procedureGid, string accessToken, string questionContent)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    questionContent = questionContent
                });

            ProcedureApi.SubmitProcedureDiscussionQuestion(procedureGid, accessToken, body);
        }
    }
}
