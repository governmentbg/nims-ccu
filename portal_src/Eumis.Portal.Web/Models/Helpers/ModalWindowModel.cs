using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eumis.Portal.Web.Models.Helpers
{
    public class ModalWindowModel
    {
        public ModalWindowModel(string title, string message)
        {
            this.Title = title;
            this.Message = message;
        }

        public ModalWindowModel(string message)
            : this(Views.Shared.App_LocalResources.Draft.Warning, message)
        {
        }

        public string Title { get; set; }

        public string Message { get; set; }
    }
}