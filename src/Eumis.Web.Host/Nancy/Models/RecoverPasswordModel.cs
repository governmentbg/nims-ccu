﻿namespace Eumis.Web.Host.Nancy.Models
{
    public class RecoverPasswordModel : LayoutModel
    {
        public string Username { get; set; }

        public string Password1 { get; set; }

        public string Password2 { get; set; }

        public string Code { get; set; }
    }
}