using Eumis.Documents.Contracts;
using System.Collections.Generic;
using System;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class MessageCommunicator : IMessageCommunicator
    {
        #region Private

        public ContractMessageXml GetProjectMessage(Guid gid, string accessToken, string type)
        {
            return MessageApi.GetProjectMessage(gid, accessToken, type).ToObject<ContractMessageXml>();
        }

        public ContractMessageXml GetProjectMessageAnswer(Guid communicationGid, Guid answerGid, string accessToken)
        {
            return MessageApi.GetProjectMessageAnswer(communicationGid, answerGid, accessToken).ToObject<ContractMessageXml>();
        }

        public ContractMessageXml PutProjectMessage(Guid gid, string token, string xml, byte[] version, DateTime? questionEndingDate)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version,
                    messageEndingDate = questionEndingDate?.ToISO8601Format()
                });

            try
            {
                return MessageApi.PutProjectMessage(gid, token, body).ToObject<ContractMessageXml>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public void SubmitProjectMessage(Guid gid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            MessageApi.SubmitProjectMessage(gid, token, body);
        }

        public byte[] GetMessageProjectFilesZip(string messageGid, string answerGid, string messageType, string accessToken)
        {
            var zipFile = MessageApi.GetMessageProjectFilesZip(messageGid, answerGid, messageType, accessToken).Value<string>("zipFile");

            return System.Convert.FromBase64String(zipFile);
        }

        public byte[] GetQuestionFilesZip(string messageGid, string accessToken)
        {
            var zipFile = MessageApi.GetQuestionFilesZip(messageGid, accessToken).Value<string>("zipFile");

            return System.Convert.FromBase64String(zipFile);
        }

        public byte[] GetAnswerFilesZip(string communicationGid, string answerGid, string accessToken)
        {
            var zipFile = MessageApi.GetAnswerFilesZip(communicationGid, answerGid, accessToken).Value<string>("zipFile");

            return System.Convert.FromBase64String(zipFile);
        }

        #endregion

        #region Public

        public ContractMessage GetMessages(string accessToken)
        {
            return MessageApi.GetMessages(accessToken).ToObject<ContractMessage>();
        }

        public ContractMessageXml GetMessage(System.Guid gid, string accessToken)
        {
            return MessageApi.GetMessage(gid, accessToken).ToObject<ContractMessageXml>();
        }

        public ContractMessageXml UpdateAnswer(Guid communicationGid, Guid answerGid, string xml, byte[] version, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return MessageApi.UpdateAnswer(communicationGid, answerGid, body, accessToken).ToObject<ContractMessageXml>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public RegMessageCountPVO GetCounts(string accessToken)
        {
            return MessageApi.GetCounts(accessToken).ToObject<RegMessageCountPVO>();
        }

        public void FinalizeAnswer(Guid communicationGid, Guid answerGid, byte[] version, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            try
            {
                MessageApi.FinalizeAnswer(communicationGid, answerGid, body, accessToken);
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
            }
        }

        public void DefinalizeAnswer(Guid communicationGid, Guid answerGid, byte[] version, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            try
            {
                MessageApi.DefinalizeAnswer(communicationGid, answerGid, body, accessToken);
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
            }
        }

        public ContractMessageXml SubmitAnswer(Guid communicationGid, Guid answerGid, byte[] version, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            try
            {
                return MessageApi.SubmitAnswer(communicationGid, answerGid, body, accessToken).ToObject<ContractMessageXml>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public ContractSendResult SendAnswer(byte[] isun, List<byte[]> signatures, byte[] version, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                isun = isun,
                signatures = signatures,
                version = version,
            });

            return MessageApi.SendAnswer(body, accessToken).ToObject<ContractSendResult>();
        }

        public ContractMessageXml GetAnswer(Guid communicationGid, Guid answerGid, string accessToken)
        {
            return MessageApi.GetAnswer(communicationGid, answerGid, accessToken).ToObject<ContractMessageXml>();
        }

        public bool HasNewMessage(string accessToken)
        {
            var messagesStatistics = this.GetCounts(accessToken);
            return messagesStatistics.newCount > 0
                || messagesStatistics.draftCount > 0
                || messagesStatistics.finalizedCount > 0
                || messagesStatistics.paperSubmittedCount > 0;
        }

        public ContractGetMessagesCount GetMessagesCount(string accessToken)
        {
            var messagesStatistics = this.GetCounts(accessToken);

            var newMessages = messagesStatistics.newCount
                + messagesStatistics.draftCount
                + messagesStatistics.finalizedCount
                + messagesStatistics.paperSubmittedCount;

            var allMessages = newMessages
                + messagesStatistics.submittedCount
                + messagesStatistics.appliedCount
                + messagesStatistics.rejectedCount
                + messagesStatistics.cancelledCount
                + messagesStatistics.expiredCount;

            return new ContractGetMessagesCount
            {
                NewMessages = newMessages,
                AllMessages = allMessages,
            };
        }

        public ContractMessageXml GetNewAnswer(Guid gid, byte[] version, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            try
            {
                return MessageApi.GetNewAnswer(gid, body, accessToken).ToObject<ContractMessageXml>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public void DeleteAnswer(Guid communicationGid, Guid answerGid, byte[] version, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                version = version,
            });

            try
            {
                MessageApi.DeleteAnswer(communicationGid, answerGid, body, accessToken);
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
            }
        }

        #endregion
    }
}
