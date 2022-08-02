using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.Components.Communicators
{
    public interface IProcedureCommunicator
    {
        IList<ContractProcedureProgrammesTree> GetActiveProcedureProgrammesTree();
        IList<ContractProcedureProgrammesTree> GetEndedProcedureProgrammesTree();
        IList<ContractProcedureProgrammesTree> GetPublicDiscussionProcedureProgrammesTree();
        IList<ContractProcedureProgrammesTree> GetArchivedPublicDiscussionProcedureProgrammesTree();
        ContractProcedure GetProcedureAppData(Guid procedureGid);
        ContractProcedureInfo GetProcedureInfo(Guid procedureGid);
        ContractProcedureInfo GetProcedurePublicDiscussionInfo(Guid procedureGid);

        ContractProcedure GetProcedureActualAppData(Guid procedureGid);

        List<ContractProcedureDiscussionsInfo> GetProcedureDiscussionsInfo(Guid id, string searchTerm, int limit, int offset);

        void SubmitProcedurePublicDiscussionComment(Guid procedureGid, string accessToken, string senderEmail, string senderName, string commentMessage);

        void SubmitProcedureDiscussionQuestion(Guid procedureGid, string accessToken, string questionMessage);
    }
}
