using System;

namespace Eumis.Domain.Projects
{
    public partial class ProjectCommunicationAnswer
    {
        public void AssertIsDraft()
        {
            if (this.Status != ProjectCommunicationAnswerStatus.Draft)
            {
                throw new InvalidOperationException("ProjectCommunicationAnswer must be in status 'Draft'.");
            }
        }

        public void SetAnswerXml(string xml)
        {
            this.AssertIsDraft();

            this.Answer.SetAnswerXml(xml);
        }

        public void SetReadDate()
        {
            this.ReadDate = DateTime.Now;
        }

        public void MakeDraft()
        {
            if (this.Status != ProjectCommunicationAnswerStatus.AnswerFinalized)
            {
                throw new DomainValidationException("Status transition allowed only from status AnswerFinalized.");
            }

            this.Status = ProjectCommunicationAnswerStatus.Draft;
        }

        public void MakePaper()
        {
            if (this.Status != ProjectCommunicationAnswerStatus.AnswerFinalized)
            {
                throw new DomainValidationException("Status transition allowed only from status AnswerFinalized.");
            }

            this.Status = ProjectCommunicationAnswerStatus.PaperAnswer;
        }

        public void MakeFinalized()
        {
            this.AssertIsDraft();

            this.Status = ProjectCommunicationAnswerStatus.AnswerFinalized;
        }

        public void MakeAnswer()
        {
            this.AssertIsDraft();

            this.Status = ProjectCommunicationAnswerStatus.Answer;
            this.Answer.MessageDate = DateTime.Now;
        }

        public void MakeAnswer(DateTime currentDate, string answerHash)
        {
            if (this.Status != ProjectCommunicationAnswerStatus.PaperAnswer && this.Status != ProjectCommunicationAnswerStatus.AnswerFinalized)
            {
                throw new DomainValidationException("Answer can be registered only if it is in status PaperAnswer or AnswerFinalized.");
            }

            if (this.Answer.Hash != answerHash)
            {
                throw new DomainValidationException("Incorrect answer hash.");
            }

            this.Answer.MessageDate = currentDate;
            this.Status = ProjectCommunicationAnswerStatus.Answer;
        }
    }
}
