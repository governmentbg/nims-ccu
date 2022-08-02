using Eumis.Common.Config;
using Eumis.Common.Email;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Job.Host.Jobs.Email
{
    public class TemplateConfig
    {
        private static readonly IList<TemplateConfig> AllTemplates = new List<TemplateConfig>()
        {
            new TemplateConfig(
                EmailTemplate.ActivationMessage,
                "СУНИ: Активиране на заявка за създаване на нов профил"),

            new TemplateConfig(
                EmailTemplate.RecoverPasswordMessage,
                "СУНИ: Забравена парола, заявка за смяна"),

            new TemplateConfig(
                EmailTemplate.ContractRegistrationActivationMessage,
                "СУНИ: Активиране на заявка за създаване на нов профил за отчитане"),

            new TemplateConfig(
                EmailTemplate.ContractRegistrationRecoverPasswordMessage,
                "СУНИ: Забравена парола, заявка за смяна"),

            new TemplateConfig(
                EmailTemplate.SystemRecoverPasswordMessage,
                "СУНИ: Забравена парола, заявка за смяна"),

            new TemplateConfig(
                EmailTemplate.FeedbackMessage,
                "СУНИ (обратна връзка)"),

            new TemplateConfig(
                EmailTemplate.UserActivatedMessage,
                "Съобщение за активация на потребител в СУНИ"),

            new TemplateConfig(
                EmailTemplate.UserUpdatedMessage,
                "Съобщение за актуализация на потребител в СУНИ"),

            new TemplateConfig(
                EmailTemplate.ProjectRegisteredMessage,
                "СУНИ: Получено проектно предложение"),

            new TemplateConfig(
                EmailTemplate.ProjectEvalStatusChangedMessage,
                "СУНИ: Промяна на статус на проектно предложение"),

            new TemplateConfig(
                EmailTemplate.QuestionSentMessage,
                "СУНИ: Изпратен въпрос от оценителна комисия"),

            new TemplateConfig(
                EmailTemplate.AnswerReceivedMessage,
                "СУНИ: Получен отговор на въпрос от оценителна комисия"),

            new TemplateConfig(
                EmailTemplate.NewsPublishedMessage,
                "СУНИ: Нова публикация в секция новини"),

            new TemplateConfig(
                EmailTemplate.NewMsgMessage,
                "СУНИ: Ново съобщение"),

            new TemplateConfig(
                EmailTemplate.ContractCommunicationSentMessage,
                "СУНИ: Изпратено съобщение към договор"),

            new TemplateConfig(
                EmailTemplate.ContractVersionActivatedMessage,
                "СУНИ: Нова промяна/изменение към договор"),

            new TemplateConfig(
                EmailTemplate.ContractContracRegistrationActivatedMessage,
                "СУНИ: Присъединяване на профил към управлението на договор"),

            new TemplateConfig(
                EmailTemplate.ContractContracRegistrationDeactivatedMessage,
                "СУНИ: Премахване на профил от управлението на договор"),

            new TemplateConfig(
                EmailTemplate.ContractReportReturnedDocumentMessage,
                "СУНИ: Искане за коригиране на данни"),

            new TemplateConfig(
                EmailTemplate.EvalSessionPublishedMessage,
                "СУНИ: Резултати от извършена проверка за административно съответствие и допустимост"),

            new TemplateConfig(
                EmailTemplate.ProjectQuestionExpireMessage,
                "СУНИ: Краен срок за отговор на изпратен въпрос"),

            new TemplateConfig(
                EmailTemplate.ProjectMACommunicationQuestionSentMessage,
                "Получено съобщение по схема за кандидатстване в СУНИ"),
        };

        private TemplateConfig(string templateName, string templateFileName, string sender, string mailSubject, bool isBodyHtml)
        {
            this.TemplateName = templateName;
            this.TemplateFileName = templateFileName;
            this.Sender = sender;
            this.MailSubject = mailSubject;
            this.IsBodyHtml = isBodyHtml;
        }

        private TemplateConfig(string templateName, string mailSubject)
            : this(templateName, $"{templateName}.cshtml", System.Configuration.ConfigurationManager.AppSettings.GetWithEnv("Eumis.Job.Host:ActivationSender"), mailSubject, true)
        {
        }

        public string TemplateName { get; private set; }

        public string TemplateFileName { get; private set; }

        public string Sender { get; private set; }

        public string MailSubject { get; private set; }

        public bool IsBodyHtml { get; private set; }

        public static TemplateConfig Get(string name)
        {
            return AllTemplates.Single(e => e.TemplateName == name);
        }
    }
}