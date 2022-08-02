using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.Projects
{
    public partial class ProjectMassManagingAuthorityCommunication
    {
        #region ProjectMassManagingAuthorityCommunication

        public void UpdateAttributes(
            int programmeId,
            int procedureId,
            ProjectManagingAuthorityCommunicationSubject? subject,
            string message,
            DateTime? endingDate,
            int? orderNum)
        {
            this.Message = message;
            this.Subject = subject;
            this.ProcedureId = procedureId;

            if (this.ProgrammeId != programmeId)
            {
                this.OrderNum = orderNum.Value;
            }

            this.ProgrammeId = programmeId;

            if (endingDate.HasValue)
            {
                if (endingDate.Value.Date < DateTime.Now.Date)
                {
                    throw new InvalidOperationException("QuestionEndingDate must be greater or equal to current date.");
                }

                // ends at 23:59:59 at the given day
                this.EndingDate = endingDate.Value.AddDays(1).Date.AddMilliseconds(-1);
            }
            else
            {
                this.EndingDate = null;
            }

            this.ModifyDate = DateTime.Now;
        }

        public IList<string> CanDelete()
        {
            var errorList = new List<string>();

            if (this.Status != ProjectMassManagingAuthorityCommunicationStatus.Draft)
            {
                errorList.Add("Не може да изтриете комуникация, чийто статус е различен от 'Чернова'");
            }

            return errorList;
        }

        public IList<string> CanSend()
        {
            var errorList = new List<string>();
            if (!this.Subject.HasValue)
            {
                errorList.Add("Не е попълнена тема на съобщението");
            }

            if (!this.EndingDate.HasValue)
            {
                errorList.Add("Не е попълнен краен срок за отговор");
            }
            else if (this.EndingDate.Value.Date < DateTime.Now.Date)
            {
                errorList.Add("Датата на краен срок за отговор трябва да е по-голяма или равна на текущата дата");
            }

            if (string.IsNullOrEmpty(this.Message))
            {
                errorList.Add("Не е попълненo съдържание на съобщението");
            }

            if (this.Recipients.Count == 0)
            {
                errorList.Add("Не са указани получатели за комуникацията");
            }

            return errorList;
        }

        public void AssertIsDraft()
        {
            if (this.Status != ProjectMassManagingAuthorityCommunicationStatus.Draft)
            {
                throw new DomainException("Project mass communication status is different from 'Draft'");
            }
        }

        #endregion ProjectMassManagingAuthorityCommunication

        #region ProjectMassManagingAuthorityCommunicationDocument

        public ProjectMassManagingAuthorityCommunicationDocument FindDocument(int documentId)
        {
            return this.Documents.Single(x => x.ProjectMassManagingAuthorityCommunicationDocumentId == documentId);
        }

        public void AddDocument(string name, string description, Guid? fileKey, string fileName)
        {
            this.Documents.Add(new ProjectMassManagingAuthorityCommunicationDocument
            {
                Name = name,
                Description = description,
                FileKey = fileKey,
                FileName = fileName,
            });

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateDocument(int documentId, string name, string description, string fileName, Guid? fileKey)
        {
            var document = this.FindDocument(documentId);
            document.Name = name;
            document.Description = description;
            document.FileName = fileName;
            document.FileKey = fileKey;

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveDocument(int documentId)
        {
            this.Documents.Remove(this.FindDocument(documentId));

            this.ModifyDate = DateTime.Now;
        }

        #endregion ProjectMassManagingAuthorityCommunicationDocument

        #region ProjectMassManagingAuthorityCommunicationRecipient

        public void AddRecipients(int[] projectIds)
        {
            projectIds
                .ToList()
                .ForEach(projectId => this.Recipients.Add(new ProjectMassManagingAuthorityCommunicationRecipient(projectId)));

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveRecipient(int projectId)
        {
            var recipient = this.Recipients.Single(x => x.ProjectId == projectId);
            this.Recipients.Remove(recipient);

            this.ModifyDate = DateTime.Now;
        }

        #endregion ProjectMassManagingAuthorityCommunicationRecipient
    }
}
