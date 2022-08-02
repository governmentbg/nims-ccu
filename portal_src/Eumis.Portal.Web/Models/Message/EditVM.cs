using Eumis.Common.Validation;
using Eumis.Components.Web;
using Eumis.Documents.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eumis.Portal.Web.Models.Message
{
    public class EditVM : BaseVM, IEditVM<R_10020.Message>, IEngineValidatable, IRemoteValidatable
    {
        public string Content { get; set; }
        public string Reply { get; set; }
        public Models.Project.EditVM Project { get; set; }

        public List<R_10018.AttachedDocument> ContentAttachedDocumentCollection { get; set; }
        public List<R_10018.AttachedDocument> ReplyAttachedDocumentCollection { get; set; }

        public List<string> RemoteValidationErrors { get; set; }
        public List<string> RemoteValidationWarnings { get; set; }

        public DateTime? EndingDate { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime? MessageDate { get; set; }

        #region Get Set

        public EditVM() { }

        public EditVM(R_10020.Message message)
        {
            this.Content = message.Content;
            this.ContentAttachedDocumentCollection = message.ContentAttachedDocumentCollection;

            this.Project = new Project.EditVM(message.Project);

            // lock documents added in previous project version 
            if (this.Project.AttachedDocuments != null &&
                this.Project.AttachedDocuments.AttachedDocumentCollection != null &&
                this.Project.AttachedDocuments.AttachedDocumentCollection.Count > 0)
            {
                foreach (var document in this.Project.AttachedDocuments.AttachedDocumentCollection)
                {
                    if ((!document.ActivationDate.HasValue || (document.ActivationDate.HasValue && document.ActivationDate <= message.LastSendingDate))
                        && !string.IsNullOrWhiteSpace(document.AttachedDocumentContent?.BlobContentId))
                    {
                        document.IsLocked = true;
                    }
                }
            }

            this.Reply = message.Reply;
            this.ReplyAttachedDocumentCollection = message.ReplyAttachedDocumentCollection;

            this.EndingDate = message.EndingDate;
            this.RegistrationNumber = message.RegistrationNumber;
            this.MessageDate = message.MessageDate;
        }

        public R_10020.Message Set(R_10020.Message message)
        {
            message.modificationDate = DateTime.Now;

            message.Project = this.Project.Set(message.Project);
            message.Reply = this.Reply;

            message.type = R_09990.MessageTypeNomenclature.Answer;

            message.ReplyAttachedDocumentCollection = new R_10020.AttachedDocumentCollection();
            if (this.ReplyAttachedDocumentCollection != null)
                message.ReplyAttachedDocumentCollection.AddRange(this.ReplyAttachedDocumentCollection);

            return message;
        }

        #endregion
    }
}