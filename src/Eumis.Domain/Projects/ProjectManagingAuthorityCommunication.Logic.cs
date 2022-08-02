using Eumis.Domain.Core;
using Eumis.Domain.Events;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.NotificationEvents;
using System;
using System.Linq;

namespace Eumis.Domain.Projects
{
    public partial class ProjectManagingAuthorityCommunication
    {
        #region ProjectManagingAuthorityCommunication

        public void UpdateAttributes(DateTime? questionEndingDate)
        {
            if (this.ManagingAuthorityCommunicationStatus != ProjectManagingAuthorityCommunicationStatus.DraftQuestion &&
                this.ManagingAuthorityCommunicationStatus != ProjectManagingAuthorityCommunicationStatus.Question)
            {
                throw new InvalidOperationException("Cannot edit ProjectCommunication when the status is different from 'DraftQuestion' or 'Question'.");
            }

            if (questionEndingDate.HasValue)
            {
                if (questionEndingDate.Value.Date < DateTime.Now)
                {
                    throw new InvalidOperationException("QuestionEndingDate must be greater or equal to current date.");
                }

                // ends at 23:59:59 at the given day
                this.QuestionEndingDate = questionEndingDate.Value.AddDays(1).Date.AddMilliseconds(-1);
            }
            else
            {
                this.QuestionEndingDate = null;
            }

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateAttributes(ProjectManagingAuthorityCommunicationSubject? subject)
        {
            this.AssertIsDraft();

            if (subject.HasValue)
            {
                if (this.Source == ProjectManagingAuthorityCommunicationSource.Beneficiary && !BeneficiarySubjects.Contains(subject.Value))
                {
                    throw new DomainValidationException("ProjectManagingAuthorityCommunication subject must be'ProjectProposalWithdrawal' or 'Complaint' or 'ChangesAndCircumstances'.");
                }
                else if (this.Source == ProjectManagingAuthorityCommunicationSource.ManagingAuthority && !ManagingAuthoritySubjects.Contains(subject.Value))
                {
                    throw new DomainValidationException("ProjectManagingAuthorityCommunication subject must be 'ContractConclusionDocuments' or 'ChangesAndCircumstances' or 'Message'.");
                }

                this.Subject = subject.Value;
            }
        }

        public void MakeQuestion(string regNumber)
        {
            if (this.ManagingAuthorityCommunicationStatus != ProjectManagingAuthorityCommunicationStatus.DraftQuestion)
            {
                throw new DomainValidationException("Question can be activated only if it is in status DraftQuestion.");
            }

            this.RegNumber = regNumber;
            this.ManagingAuthorityCommunicationStatus = ProjectManagingAuthorityCommunicationStatus.Question;

            var currentDate = DateTime.Now;
            this.Question.MessageDate = currentDate;

            this.ModifyDate = currentDate;

            if (this.Source == ProjectManagingAuthorityCommunicationSource.ManagingAuthority)
            {
                ((IEventEmitter)this).Events.Add(new ProjectMACommunicationQuestionSentEvent()
                {
                    ProjectCommunicationId = this.ProjectCommunicationId,
                });
            }
            else if (this.Source == ProjectManagingAuthorityCommunicationSource.Beneficiary)
            {
                ((INotificationEventEmitter)this).NotificationEvents.Add(new ProjectNotificationEvent(
                    NotificationEventType.ProjectManagingAuthorityCommunicationRecieved,
                    this.ProjectId,
                    this.ProjectCommunicationId));
            }
        }

        public void SetQuestionXml(string xml)
        {
            if (this.ManagingAuthorityCommunicationStatus != ProjectManagingAuthorityCommunicationStatus.DraftQuestion)
            {
                throw new InvalidOperationException("Question can be edited only if it is in status DraftQuestion.");
            }

            this.Question.SetXml(xml);
            this.UpdateFiles(ProjectCommunicationMessageType.Question);

            this.ModifyDate = DateTime.Now;
        }

        public void MakeCancelled(string cancelNote = null)
        {
            if (this.Answers.Count > 0 && this.Answers.Any(a => a.Status != ProjectCommunicationAnswerStatus.Draft))
            {
                throw new InvalidOperationException("Communication cant be canceled with sent answer.");
            }

            if (this.ManagingAuthorityCommunicationStatus != ProjectManagingAuthorityCommunicationStatus.Question)
            {
                throw new DomainValidationException("Communication can be canceled only if it is in status Question.");
            }

            this.StatusNote = cancelNote;
            this.ManagingAuthorityCommunicationStatus = ProjectManagingAuthorityCommunicationStatus.Canceled;
            this.ModifyDate = DateTime.Now;
        }

        public void AssertIsDraft()
        {
            if (this.ManagingAuthorityCommunicationStatus != ProjectManagingAuthorityCommunicationStatus.DraftQuestion)
            {
                throw new InvalidOperationException("Cannot edit/delete ProjectManagingAuyhorityCommunication when not in 'DraftQuestion' status");
            }
        }

        public void SetSubject(ProjectManagingAuthorityCommunicationSubject subject)
        {
            this.Subject = subject;
            this.ModifyDate = DateTime.Now;
        }

        #endregion

        #region ProjectCommunicationAnswer

        public bool CanCreateAnswer()
        {
            return this.Answers.All(a => a.Status == ProjectCommunicationAnswerStatus.Answer || a.Status == ProjectCommunicationAnswerStatus.Canceled);
        }

        public void DeleteAnswer(int answerId)
        {
            var answer = this.FindAnswer(answerId);
            answer.AssertIsDraft();

            this.RemoveAnswerFiles(answer.ProjectCommunicationAnswerId);

            this.Answers.Remove(answer);
            this.ModifyDate = DateTime.Now;
        }

        private void RemoveAnswerFiles(int projectCommunicationAnswerId)
        {
            var answerFiles = this.Files
                .Where(f => f.ProjectCommunicationAnswerId == projectCommunicationAnswerId)
                .ToList();

            foreach (var file in answerFiles)
            {
                this.Files.Remove(file);
            }
        }

        #endregion
    }
}
