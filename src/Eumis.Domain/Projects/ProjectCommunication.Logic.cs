using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.Core;
using Eumis.Domain.Events;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.NotificationEvents;

namespace Eumis.Domain.Projects
{
    public partial class ProjectCommunication
    {
        #region ProjectCommunication

        public void UpdateAttributes(DateTime? questionEndingDate)
        {
            if (this.Answers.Any(a => a.Answer.MessageDate != null))
            {
                throw new InvalidOperationException("Cannot edit ProjectCommunication when there is an recieved answer.");
            }

            if (
                this.Status != ProjectCommunicationStatus.Question &&
                this.Status != ProjectCommunicationStatus.DraftAnswer &&
                this.Status != ProjectCommunicationStatus.AnswerFinalized &&
                this.Status != ProjectCommunicationStatus.PaperAnswer)
            {
                throw new InvalidOperationException("Cannot edit ProjectCommunication when the status is different from 'Question', 'DraftAnswer', 'AnswerFinalized' or 'PaperAnswer'.");
            }

            if (questionEndingDate.HasValue)
            {
                if (questionEndingDate.Value.Date < this.Question.MessageDate.Value.Date)
                {
                    throw new InvalidOperationException("QuestionEndingDate must be greater or equal to questionMessageDate.");
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

        public void SetEndingDate(DateTime? questionEndingDate)
        {
            if (this.Status != ProjectCommunicationStatus.DraftQuestion)
            {
                throw new InvalidOperationException("Cannot set QuestionEndingDate when the status is not 'DraftQuestion'.");
            }

            if (questionEndingDate.HasValue)
            {
                var currentDate = DateTime.Now;

                if (questionEndingDate.Value.Date > currentDate.Date)
                {
                    // ends at 23:59:59 at the given day
                    this.QuestionEndingDate = questionEndingDate.Value.AddDays(1).Date.AddMilliseconds(-1);
                    this.ModifyDate = currentDate;
                }
            }
        }

        public void SetQuestionXml(string xml)
        {
            if (this.Status != ProjectCommunicationStatus.DraftQuestion)
            {
                throw new InvalidOperationException("Question can be edited only if it is in status DraftQuestion.");
            }

            this.Question.SetXml(xml);
            this.UpdateFiles(ProjectCommunicationMessageType.Question);

            this.ModifyDate = DateTime.Now;
        }

        public void MakeCancelled(string cancelNote)
        {
            if (!ProjectCommunication.CancellableStatuses.Contains(this.Status))
            {
                throw new DomainValidationException("Communication can be canceled only if it is in not DraftQuestion or in final status.");
            }

            this.StatusNote = cancelNote;
            this.Status = ProjectCommunicationStatus.Canceled;
            this.ModifyDate = DateTime.Now;
        }

        public void MakeQuestion(string regNumber)
        {
            if (this.Status != ProjectCommunicationStatus.DraftQuestion)
            {
                throw new DomainValidationException("Question can be activated only if it is in status DraftQuestion.");
            }

            this.RegNumber = regNumber;
            this.Status = ProjectCommunicationStatus.Question;

            var currentDate = DateTime.Now;
            this.Question.MessageDate = currentDate;

            this.ModifyDate = currentDate;

            ((IEventEmitter)this).Events.Add(new QuestionSentEvent()
            {
                ProjectCommunicationId = this.ProjectCommunicationId,
            });
        }

        public void MakeExpired()
        {
            var targetStatus = ProjectCommunicationStatus.Expired;

            if (this.Status == targetStatus)
            {
                throw new DomainValidationException("Cannot transition to the same status");
            }

            if (!ProjectCommunication.ExpirableStatuses.Contains(this.Status))
            {
                throw new DomainValidationException("ProjectCommunication is not in expirable status");
            }

            if (ProjectCommunication.FinalStatuses.Contains(this.Status))
            {
                throw new DomainValidationException("ProjectCommunication is in final status");
            }

            if (this.Answers.Any(a => a.Answer.MessageDate != null))
            {
                throw new DomainValidationException("It has already recieved an answer");
            }

            if (!this.QuestionEndingDate.HasValue)
            {
                throw new DomainValidationException("ProjectCommunication does not have QuestionEndingDate");
            }

            var currentTime = DateTime.Now;

            if (currentTime < this.QuestionEndingDate.Value)
            {
                throw new DomainValidationException("QuestionEndingDate has not passed");
            }

            this.Status = targetStatus;
            this.ModifyDate = currentTime;
        }

        #endregion

        #region  ProjectCommunicationAnswer

        public void CanCreateAnswer()
        {
            bool hasActiveAnswers = this.Answers.Any(a => a.Status != ProjectCommunicationAnswerStatus.Answer && a.Status != ProjectCommunicationAnswerStatus.Canceled);

            if ((!ProjectCommunication.AnswerAllowedStatuses.Contains(this.Status)) ||
                (this.QuestionEndingDate.HasValue && this.QuestionEndingDate.Value < DateTime.Now) ||
                hasActiveAnswers)
            {
                throw new InvalidOperationException("Cannot create ProjectCommunicationAnswer");
            }
        }

        public void MakeAnswerFinalized(Guid answerGid)
        {
            if (this.IsInitialAnswer())
            {
                if (this.Status != ProjectCommunicationStatus.DraftAnswer)
                {
                    throw new DomainValidationException("Status transition allowed only from status DraftAnswer.");
                }

                this.Status = ProjectCommunicationStatus.AnswerFinalized;

                var currentDate = DateTime.Now;
                this.ModifyDate = currentDate;
            }

            var answer = this.FindAnswer(answerGid);

            answer.MakeFinalized();
        }

        public void DefinalizeAnswer(Guid answerGid)
        {
            if (this.IsInitialAnswer())
            {
                if (this.Status != ProjectCommunicationStatus.AnswerFinalized)
                {
                    throw new DomainValidationException("Status transition allowed only from status AnswerFinalized.");
                }

                this.Status = ProjectCommunicationStatus.DraftAnswer;

                var currentDate = DateTime.Now;
                this.ModifyDate = currentDate;
            }

            var answer = this.FindAnswer(answerGid);
            answer.MakeDraft();
        }

        public void MakeDraftAnswer()
        {
            if (this.IsInitialAnswer())
            {
                if (this.Status != ProjectCommunicationStatus.Question && this.Status != ProjectCommunicationStatus.DraftAnswer)
                {
                    throw new DomainValidationException("Status transition allowed only from status Question or DraftAnswer.");
                }

                this.Status = ProjectCommunicationStatus.DraftAnswer;

                var currentDate = DateTime.Now;
                this.ModifyDate = currentDate;
            }
        }

        public void MakePaperAnswer(Guid answerGid)
        {
            if (this.IsInitialAnswer())
            {
                if (this.Status != ProjectCommunicationStatus.AnswerFinalized)
                {
                    throw new DomainValidationException("PaperAnswer can be sent only if it is in status AnswerFinalized.");
                }

                this.Status = ProjectCommunicationStatus.PaperAnswer;
            }

            var currentDate = DateTime.Now;

            if (currentDate > this.QuestionEndingDate)
            {
                throw new DomainValidationException("Answer time exceeded.");
            }

            var answer = this.FindAnswer(answerGid);
            answer.MakePaper();

            this.ModifyDate = currentDate;
        }

        public void MakeAnswer(int answerId)
        {
            if (this.IsInitialAnswer())
            {
                if (this.Status != ProjectCommunicationStatus.AnswerFinalized)
                {
                    throw new DomainValidationException("Answer can be sent only if it is in status 'AnswerFinalized'.");
                }

                this.Status = ProjectCommunicationStatus.Answer;
            }
            else
            {
                var oldAnswer = this.Answers
                    .Where(a => a.Status == ProjectCommunicationAnswerStatus.Answer)
                    .Single();

                oldAnswer.Status = ProjectCommunicationAnswerStatus.Canceled;
            }

            var currentDate = DateTime.Now;

            if (currentDate > this.QuestionEndingDate)
            {
                throw new DomainValidationException("Answer time exceeded.");
            }

            var answer = this.FindAnswer(answerId);
            answer.MakeAnswer(currentDate, answer.Answer.Hash);

            this.ModifyDate = currentDate;

            ((IEventEmitter)this).Events.Add(new AnswerReceivedEvent()
            {
                ProjectCommunicationId = this.ProjectCommunicationId,
            });

            ((INotificationEventEmitter)this).NotificationEvents.Add(new EvalSessionNotificationEvent(this.EvalSessionId, this.ProjectId, this.ProjectCommunicationId));
        }

        public void RegisterAnswer(int answerId, string answerHash, DateTime regDate)
        {
            if (this.IsInitialAnswer())
            {
                if (this.Status != ProjectCommunicationStatus.PaperAnswer)
                {
                    throw new DomainValidationException("Communication status must be PaperAnswer.");
                }

                this.Status = ProjectCommunicationStatus.Answer;
            }
            else
            {
                var oldAnswer = this.Answers
                    .Where(a => a.Status == ProjectCommunicationAnswerStatus.Answer)
                    .Single();

                oldAnswer.Status = ProjectCommunicationAnswerStatus.Canceled;
            }

            if (regDate > this.QuestionEndingDate)
            {
                throw new DomainValidationException("Answer time exceeded.");
            }

            var answer = this.FindAnswer(answerId);
            var currentDate = DateTime.Now;

            answer.MakeAnswer(currentDate, answerHash);

            this.ModifyDate = currentDate;

            ((IEventEmitter)this).Events.Add(new AnswerReceivedEvent()
            {
                ProjectCommunicationId = this.ProjectCommunicationId,
            });
        }

        public void RemoveAnswer(int answerId)
        {
            var currentDate = DateTime.Now;

            if (this.IsInitialAnswer())
            {
                if (this.Status != ProjectCommunicationStatus.Question && this.Status != ProjectCommunicationStatus.DraftAnswer)
                {
                    throw new DomainValidationException("Answer can be deleted only if the Communication is in Question or DraftAnswer status.");
                }

                this.Status = ProjectCommunicationStatus.Question;
            }

            if (this.QuestionEndingDate.HasValue && this.QuestionEndingDate <= currentDate)
            {
                throw new DomainValidationException("Answer can be deleted only if the Communication response period has not expired.");
            }

            var answer = this.FindAnswer(answerId);

            answer.AssertIsDraft();

            this.Answers.Remove(answer);
            this.ModifyDate = currentDate;
        }

        public DateTime GetActiveAnswerDate()
        {
            return this.Answers
                .Where(a => a.Status == ProjectCommunicationAnswerStatus.Answer)
                .Select(a => a.Answer.MessageDate.Value)
                .Single();
        }

        private bool IsInitialAnswer()
        {
            return this.Answers.Count == 1;
        }

        #endregion
    }
}
