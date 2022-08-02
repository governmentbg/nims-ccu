using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eumis.Common.Email
{
    public static class EmailTemplate
    {
        public static string ActivationMessage => "ActivationMessage";

        public static string RecoverPasswordMessage => "RecoverPasswordMessage";

        public static string ContractRegistrationActivationMessage => "ContractRegistrationActivationMessage";

        public static string ContractRegistrationRecoverPasswordMessage => "ContractRegistrationRecoverPasswordMessage";

        public static string SystemRecoverPasswordMessage => "SystemRecoverPasswordMessage";

        public static string FeedbackMessage => "FeedbackMessage";

        public static string UserActivatedMessage => "UserActivatedMessage";

        public static string UserUpdatedMessage => "UserUpdatedMessage";

        public static string ProjectRegisteredMessage => "ProjectRegisteredMessage";

        public static string ProjectEvalStatusChangedMessage => "ProjectEvalStatusChangedMessage";

        public static string QuestionSentMessage => "QuestionSentMessage";

        public static string AnswerReceivedMessage => "AnswerReceivedMessage";

        public static string NewsPublishedMessage => "NewsPublishedMessage";

        public static string NewMsgMessage => "NewMsgMessage";

        public static string ContractCommunicationSentMessage => "ContractCommunicationSentMessage";

        public static string ContractVersionActivatedMessage => "ContractVersionActivatedMessage";

        public static string ContractContracRegistrationActivatedMessage => "ContractContracRegistrationActivatedMessage";

        public static string ContractContracRegistrationDeactivatedMessage => "ContractContracRegistrationDeactivatedMessage";

        public static string ContractReportReturnedDocumentMessage => "ContractReportReturnedDocumentMessage";

        public static string EvalSessionPublishedMessage => "EvalSessionPublishedMessage";

        public static string ProjectQuestionExpireMessage => "ProjectQuestionExpireMessage";

        public static string ProjectMACommunicationQuestionSentMessage => "ProjectMACommunicationQuestionSentMessage";
    }
}
