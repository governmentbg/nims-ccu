using Eumis.Common.Localization;
using System;

namespace Eumis.Documents.Contracts
{
    public class ProcedureDiscussion
    {
        public DateTime createDate { get; set; }

        public string senderEmail { get; set; }

        public string question { get; set; }

        public string answer { get; set; }
    }

    public class ContractProcedureDiscussionsInfo
    {
        public Guid Gid { get; set; }

        public string name { get; set; }

        public string nameAlt { get; set; }

        public string displayName { get { return SystemLocalization.GetDisplayName(name, nameAlt); } }

        public DateTime? procedureDiscussionDeadline { get; set; }

        public Guid? qaBlobKey { get; set; }

        public string qaFileName { get; set; }

        public DateTime? qaModifyDate { get; set; }

        public ContractPagePVO<ProcedureDiscussion> procedureDiscussions { get; set; }
    }
}
