using Eumis.Common.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace Eumis.Documents.Contracts
{
    public class ContractDraft
    {
        public List<RegProjectXmlPVO> results { get; set; }
        public int count { get; set; }
    }

    public class RegProjectXmlPVO
    {
        public Guid gid { get; set; }
        public DateTime modifyDate { get; set; }
        public string projectName { get; set; }
        public string projectNameAlt { get; set; }
        public string displayProjectName
        {
            get
            {
                return SystemLocalization.GetDisplayName(projectName, projectNameAlt);
            }
        }
        public string companyName { get; set; }
        public string companyNameAlt { get; set; }
        public string displayCompanyName
        {
            get
            {
                return SystemLocalization.GetDisplayName(companyName, companyNameAlt);
            }
        }
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
        public DateTime procedureEndingDate { get; set; }
        public string programmeName { get; set; }
        public string programmeNameAlt { get; set; }
        public string displayProgrammeName
        {
            get
            {
                return SystemLocalization.GetDisplayName(programmeName, programmeNameAlt);
            }
        }

        public string registrationNumber { get; set; }
        public DateTime? registrationDate { get; set; }
        public RegProjectXmlRegType? registrationType { get; set; }
        public string registrationTypeText { get; set; }
        public string registrationTypeTextAlt { get; set; }
        public string displayRegistrationTypeText
        {
            get
            {
                return SystemLocalization.GetDisplayName(registrationTypeText, registrationTypeTextAlt);
            }
        }
        public RegProjectXmlProjectStatus? projectStatus { get; set; }
        public string projectStatusText { get; set; }
        public string projectStatusTextAlt { get; set; }
        public string displayProjectStatusText
        {
            get
            {
                return SystemLocalization.GetDisplayName(projectStatusText, projectStatusTextAlt);
            }

        }
        public ProjectRegistrationStatus? projectRegStatus { get; set; }
        public string projectRegStatusText { get; set; }
        public List<MessagePVO> messages { get; set; }
        public List<MessagePVO> projectCommunications { get; set; }
        public List<ContractProjectVersionPVO> projectVersions { get; set; }
        public DateTime? submitDate { get; set; }
        public string hash { get; set; }
        public bool hasCommunications { get; set; }
        public bool hasNewQuestions { get; set; }
        public bool hasNewAnswers { get; set; }
    }

    public enum RegProjectXmlRegType
    {
        [Description("Електронно")]
        Digital = 1,

        [Description("На хартия")]
        Paper = 2
    }

    public enum RegProjectXmlProjectStatus
    {
        [Description("Оценяване")]
        Evaluation = 1,

        [Description("Приключила оценка")]
        Evaluated = 2,

        [Description("Договориран")]
        Contracted = 3,

        [Description("Чакащо потвърждение от ОО")]
        PendingApproval = 4,
    }

    public enum RegMessageType
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
    }

    public enum ProjectRegistrationStatus
    {
        [Description("Регистрирано")]
        Registered = 1,

        [Description("Регистрирано извън срок")]
        RegisteredLate = 2,

        [Description("Оттеглено")]
        Withdrawn = 3,

        [Description("Службено регистрирано")]
        RegisteredService = 4,
    }

    public enum ProjectCommunicationStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Изпратено")]
        Sent = 2,

        [Description("Непрочетено")]
        NotRead = 3,

        [Description("Чака отговор")]
        PendingAnswer = 4,

        [Description("Изпратен отговор")]
        Answered = 5,

        [Description("Анулирано")]
        Canceled = 6,
    }

    public class MessagePVO
    {
        public MessagePVO()
        {
        }

        public Guid messageGid { get; set; }

        public RegMessageType status { get; set; }

        public string statusText { get; set; }

        public string statusTextAlt { get; set; }

        public string displayStatusText
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.statusText, this.statusTextAlt);
            }
        }

        public string registrationNumber { get; set; }

        public DateTime? messageDate { get; set; }

        public DateTime? replyDate { get; set; }

        public DateTime? readDate { get; set; }

        public DateTime? questionEndingDate { get; set; }

        public bool? isBeneficiary { get; set; }

        public byte[] version { get; set; }

        public List<ContractMessageAnswerPVO> answers { get; set; }

        public ProjectCommunicationStatus communicationStatus { get; set; }

        public string communicationStatusText { get; set; }

        public string communicationStatusTextAlt { get; set; }

        public string displayCommunicationStatusText
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.communicationStatusText, this.communicationStatusTextAlt);
            }
        }
    }
}
