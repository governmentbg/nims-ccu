using Eumis.Common.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace Eumis.Documents.Contracts
{
    public class ContractMessage
    {
        public List<RegMessagePVO> results { get; set; }
        public int count { get; set; }
    }

    public class RegMessagePVO
    {
        public Guid gid { get; set; }
        public DateTime sentDate { get; set; }
        public DateTime? messageEndingDate { get; set; }
        public DateTime? messageReadDate { get; set; }
        public RegMessageStatusPVO status { get; set; }
        public string statusText { get; set; }
        public string statusTextAlt { get; set; }
        public string displayStatusText
        {
            get
            {
                return SystemLocalization.GetDisplayName(statusText, statusTextAlt);
            }
        }
        public string registrationNumber { get; set; }
        public string programmeName { get; set; }
        public string procedureName { get; set; }
        public string procedureNameAlt { get; set; }
        public string displayProcedureName
        {
            get
            {
                return SystemLocalization.GetDisplayName(procedureName, procedureNameAlt);
            }
        }
        public string procedureCode { get; set; }
        public string projectName { get; set; }
        public string projectNameAlt { get; set; }
        public string displayProjectName
        {
            get
            {
                return SystemLocalization.GetDisplayName(projectName, projectNameAlt);
            }
        }
        public ContractEnumNomenclature projectRegistrationStatus { get; set; }
        public string companyName { get; set; }
        public string companyNameAlt { get; set; }
        public string displayCompanyName
        {
            get
            {
                return SystemLocalization.GetDisplayName(companyName, companyNameAlt);
            }
        }
        public string message { get; set; }

        public DateTime? submitDate { get; set; }

        public byte[] version { get; set; }

        public List<ContractMessageAnswerPVO> answers { get; set; }

        public bool isProjectWithdrawn
        {
            get
            {
                return this.projectRegistrationStatus != null && this.projectRegistrationStatus.value.ToLower() == Contracts.ProjectRegistrationStatus.Withdrawn.ToString().ToLower();
            }
        }
    }

    public enum RegMessageStatusPVO
    {
        [Description("За отговор")]
        New = 1,

        [Description("Чернова")]
        Draft = 2,

        [Description("За изпращане")]
        Finalized = 3,

        [Description("На хартия")]
        PaperSubmitted = 4,

        [Description("Изпратено")]
        Submitted = 5,

        [Description("Анулирано")]
        Cancelled = 6,

        [Description("Изтекло")]
        Expired = 7,

        [Description("Изпратено")]
        Processed = 8,
    }

    public class RegMessageCountPVO
    {
        public int newCount { get; set; }

        public int draftCount { get; set; }

        public int finalizedCount { get; set; }

        public int paperSubmittedCount { get; set; }

        public int submittedCount { get; set; }

        public int appliedCount { get; set; }

        public int rejectedCount { get; set; }

        public int cancelledCount { get; set; }

        public int expiredCount { get; set; }
    }

    public class ContractSendResult
    {
        public string registrationNumber { get; set; }
        public DateTime replyDate { get; set; }
    }

    public class ContractGetMessagesCount
    {
        public int NewMessages { get; set; }

        public int AllMessages { get; set; }
    }
}