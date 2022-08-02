using Eumis.Common.Validation;
using System;
using System.Collections.Generic;

namespace Eumis.Portal.Web.Models.ProjectCommunication
{
    public class EditVM : BaseVM, IEditVM<R_10020.Message>, IEngineValidatable
    {
        public bool IsBeneficiary { get; set; }

        public Guid RegisteredGid { get; set; }

        public string ProjectRegNumber { get; set; }

        public string CompanyName { get; set; }

        public string Content { get; set; }

        public string Reply { get; set; }

        public R_09991.EnumNomenclature Subject { get; set; }

        public bool IsSubjectValid { get; set; }

        public List<R_10018.AttachedDocument> ContentAttachedDocumentCollection { get; set; }

        public List<R_10018.AttachedDocument> ReplyAttachedDocumentCollection { get; set; }


        #region Get Set

        public EditVM() { }

        public EditVM(R_10020.Message message)
        {
            if (message != null)
            {
                this.Subject = message.Subject;
                this.IsSubjectValid = message.IsSubjectValid;

                this.RegisteredGid = message.RegisteredGid;
                this.ProjectRegNumber = message.ProjectRegNumber;

                this.Content = message.Content;
                this.ContentAttachedDocumentCollection = message.ContentAttachedDocumentCollection;
            }
        }

        public R_10020.Message Set(R_10020.Message message)
        {
            message.modificationDate = DateTime.Now;

            message.Subject = this.Subject;
            message.Content = this.Content;

            message.ContentAttachedDocumentCollection = new R_10020.AttachedDocumentCollection();

            if (this.ContentAttachedDocumentCollection != null)
            {
                message.ContentAttachedDocumentCollection.AddRange(this.ContentAttachedDocumentCollection);
            }

            return message;
        }

        #endregion
    }
}
