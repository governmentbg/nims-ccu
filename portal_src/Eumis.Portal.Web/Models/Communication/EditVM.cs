using System.Reflection;
using Eumis.Common.Validation;
using Eumis.Components.Web;
using Eumis.Documents.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eumis.Documents.Contracts;
using Eumis.Documents.Validation;
using Eumis.Portal.Web.Helpers;

namespace Eumis.Portal.Web.Models.Communication
{
    public class EditVM : BaseVM, IEditVM<R_10042.Communication>, IEngineValidatable, IRemoteValidatable
    {
        public string Subject { get; set; }
        public string Content { get; set; }

        public List<R_10018.AttachedDocument> AttachedDocumentCollection { get; set; }

        public R_09987.CommunicationTypeNomenclature type { get; set; }

        public List<string> RemoteValidationErrors { get; set; }
        public List<string> RemoteValidationWarnings { get; set; }

        #region Get Set

        public EditVM() { }

        public EditVM(R_10042.Communication communication)
        {
            this.Subject = communication.Subject;
            this.Content = communication.Content;
            this.AttachedDocumentCollection = communication.AttachedDocumentCollection;
            this.type = communication.type;
        }

        public R_10042.Communication Set(R_10042.Communication communication)
        {
            communication.Subject = this.Subject;
            communication.Content = this.Content;

            communication.AttachedDocumentCollection = new R_10042.AttachedDocumentCollection();
            if (this.AttachedDocumentCollection != null)
                communication.AttachedDocumentCollection.AddRange(this.AttachedDocumentCollection);

            return communication;
        }

        #endregion

    }
}