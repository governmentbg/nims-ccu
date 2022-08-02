using Eumis.Common.Localization;
using System;
using System.Collections.Generic;

namespace Eumis.Documents.Contracts
{
    public class ApplicationGuideline
    {
        public Guid gid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string filename { get; set; }
        public DateTime statusDate { get; set; }
        public Guid blobKey { get; set; }
    }

    public class ApplicationComment
    {
        public DateTime createDate { get; set; }
        public string senderEmail { get; set; }
        public string comment { get; set; }
        public string standpoint { get; set; }
    }

    public class ContractProcedureInfo
    {
        public Guid gid { get; set; }
        public string code { get; set; }
        public bool isActive { get; set; }
        public string name { get; set; }
        public string nameAlt { get; set; }
        public string displayName { get { return SystemLocalization.GetDisplayName(name, nameAlt); } }
        public string description { get; set; }
        public string descriptionAlt { get; set; }
        public string displayDescription { get { return SystemLocalization.GetDisplayName(description, descriptionAlt); } }
        public string statusText { get; set; }
        public string statusTextAlt { get; set; }
        public string displayStatus { get { return SystemLocalization.GetDisplayName(statusText, statusTextAlt); } }
        public string internetAddress { get; set; }
        public DateTime? listingDate { get; set; }
        public DateTime? publicDiscussionFirstPublicationDate { get; set; }
        public DateTime? endingDate { get; set; }
        public DateTime? publicDiscussionEndDate { get; set; }
        public string endingDateNotes { get; set; }
        public List<ApplicationGuideline> applicationGuidelines { get; set; }
        public List<ApplicationGuideline> publicDiscussionGuidelines { get; set; }
        public List<ApplicationComment> publicDiscussionComments { get; set; }
        public bool HasAnyPublicDiscussionComments { get; set; }

        public Guid? qaBlobKey { get; set; }
        public string qaFileName { get; set; }
        public DateTime? qaModifyDate { get; set; }
    }
}