using Eumis.Common.Validation;
using Eumis.Components.Web;
using Eumis.Documents.Mappers;
using Eumis.Documents.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eumis.Portal.Web.Helpers;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Models
{
    public class ValidationVM : BaseVM, IRemoteValidatable
    {
        public override IEnumerable<ValidatableObject> GetValidatableObjects()
        {
            return new List<ValidatableObject>()
            {
                new ValidatableObject
                {
                    Object = AppContext.Current.Document,
                },
            };
        }

        public List<string> RemoteValidationErrors { get; set; }
        public List<string> RemoteValidationWarnings { get; set; }
    }
}