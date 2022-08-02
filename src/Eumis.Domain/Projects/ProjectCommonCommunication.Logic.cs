using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.Projects
{
    public abstract partial class ProjectCommonCommunication
    {
        #region ProjectCommonCommunication

        public void SetReadDate()
        {
            var currentDate = DateTime.Now;
            this.QuestionReadDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public void UpdateFiles(ProjectCommunicationMessageType type, ProjectCommunicationAnswer answer = null)
        {
            IList<ProjectCommunicationMessageFile> oldFiles = new List<ProjectCommunicationMessageFile>();

            if (type == ProjectCommunicationMessageType.Answer)
            {
                oldFiles = this.Files.Where(f => f.MessageType == type && f.ProjectCommunicationAnswerId == answer.ProjectCommunicationAnswerId).ToList();
            }
            else
            {
                oldFiles = this.Files.Where(f => f.MessageType == type).ToList();
            }

            foreach (var oldFile in oldFiles)
            {
                this.Files.Remove(oldFile);
            }

            IEnumerable<ProjectCommunicationMessageFile> newFiles = new List<ProjectCommunicationMessageFile>();
            switch (type)
            {
                case ProjectCommunicationMessageType.Question:
                    newFiles = this.Question.Files;
                    break;
                case ProjectCommunicationMessageType.Answer:
                    if (answer != null && answer.Answer != null)
                    {
                        newFiles = answer.Answer.Files;
                    }

                    break;
            }

            foreach (var newFile in newFiles)
            {
                newFile.MessageType = type;

                if (type == ProjectCommunicationMessageType.Answer && answer != null)
                {
                    newFile.ProjectCommunicationAnswerId = answer.ProjectCommunicationAnswerId;
                }

                this.Files.Add(newFile);
            }
        }

        #endregion

        #region ProjectCommunicationAnswer

        public ProjectCommunicationAnswer FindAnswer(int answerId)
        {
            var answer = this.Answers.Where(a => a.ProjectCommunicationAnswerId == answerId).SingleOrDefault();

            if (answer == null)
            {
                throw new DomainObjectNotFoundException($"Cannot find ProjectCommunicaionAnswer with Id {answerId} attached to ProjectCommunication with id {this.ProjectCommunicationId}");
            }

            return answer;
        }

        public ProjectCommunicationAnswer FindAnswer(Guid answerGid)
        {
            var answer = this.Answers.Where(a => a.Gid == answerGid).SingleOrDefault();

            if (answer == null)
            {
                throw new DomainObjectNotFoundException($"Cannot find ProjectCommunicaionAnswer with Gid {answerGid} attached to ProjectCommunication with id {this.ProjectCommunicationId}");
            }

            return answer;
        }

        public int GetNextAnswerOrderNum()
        {
            var lastAnswerOrderNum = this.Answers.Max(p => (int?)p.OrderNum);

            return lastAnswerOrderNum.HasValue ? lastAnswerOrderNum.Value + 1 : 1;
        }

        public string GetLastAnswerXml()
        {
            return this.Answers
                .Where(a => a.Status == ProjectCommunicationAnswerStatus.Answer)
                .OrderByDescending(a => a.OrderNum)
                .First()
                .Answer
                .Xml;
        }

        public bool HasActiveAnswer()
        {
            return this.Answers.Any(a => a.Status == ProjectCommunicationAnswerStatus.Answer);
        }

        #endregion
    }
}
