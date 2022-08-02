using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Procedures
{
    public partial class ProcedureMassCommunication
    {
        public void UpdateAttributes(int programmeId, int procedureId, string subject, string body)
        {
            this.Body = body;
            this.Subject = subject;
            this.ProcedureId = procedureId;
            this.ProgrammeId = programmeId;

            this.ModifyDate = DateTime.Now;
        }

        public IList<string> CanSend()
        {
            var errorList = new List<string>();
            if (string.IsNullOrEmpty(this.Subject))
            {
                errorList.Add("Не е попълнена тема на съобщението");
            }

            if (string.IsNullOrEmpty(this.Body))
            {
                errorList.Add("Не е попълненo съдържание на съобщението");
            }

            if (this.Recipients.Count == 0)
            {
                errorList.Add("Не са указани получатели за кореспонденцията");
            }

            return errorList;
        }

        public IList<string> CanDelete()
        {
            var errorList = new List<string>();

            if (this.Status != ProcedureMassCommunicationStatus.Draft)
            {
                errorList.Add("Не може да изтриете елемент, чийто статус е различен от 'Чернова'");
            }

            return errorList;
        }

        public void AssertIsDraft()
        {
            if (this.Status != ProcedureMassCommunicationStatus.Draft)
            {
                throw new DomainException("Procedure mass communication status is different than 'Draft'");
            }
        }

        public ProcedureMassCommunicationDocument FindDocument(int documentId)
        {
            return this.Documents.Single(x => x.ProcedureMassCommunicationDocumentId == documentId);
        }

        public void AddDocument(string name, string description, Guid? fileKey, string fileName)
        {
            this.Documents.Add(new ProcedureMassCommunicationDocument
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

        public void AddRecipients(int[] contractIds)
        {
            contractIds
                .ToList()
                .ForEach(x => this.Recipients.Add(new ProcedureMassCommunicationRecipient(x)));

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveRecipient(int contractId)
        {
            var recipient = this.Recipients.Single(x => x.ContractId == contractId);
            this.Recipients.Remove(recipient);

            this.ModifyDate = DateTime.Now;
        }
    }
}
