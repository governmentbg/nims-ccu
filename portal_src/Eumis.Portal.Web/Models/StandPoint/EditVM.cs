using Eumis.Common.Validation;
using Eumis.Components.Web;
using Eumis.Documents.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eumis.Portal.Web.Models.Standpoint
{
    public class EditVM : BaseVM, IEditVM<R_10027.Standpoint>, IEngineValidatable
    {
        public string Subject { get; set; }

        public string Content { get; set; }

        public bool IsSubjectValid { get; set; }

        public bool IsContentValid { get; set; }

        public List<R_10018.AttachedDocument> AttachedDocumentCollection { get; set; }

        #region Get Set

        public EditVM() { }

        public EditVM(R_10027.Standpoint standpoint)
        {
            this.Subject = standpoint.Subject;
            this.Content = standpoint.Content;
            this.IsSubjectValid = standpoint.IsSubjectValid;
            this.IsContentValid = standpoint.IsContentValid;
            this.AttachedDocumentCollection = standpoint.AttachedDocumentCollection;
        }

        public R_10027.Standpoint Set(R_10027.Standpoint standpoint)
        {
            standpoint.Subject = this.Subject;
            standpoint.Content = this.Content;
            standpoint.IsSubjectValid = this.IsSubjectValid;
            standpoint.IsContentValid = this.IsContentValid;

            standpoint.AttachedDocumentCollection = new R_10027.AttachedDocumentCollection();
            if (this.AttachedDocumentCollection != null)
                standpoint.AttachedDocumentCollection.AddRange(this.AttachedDocumentCollection);

            return standpoint;
        }

        #endregion
    }
}