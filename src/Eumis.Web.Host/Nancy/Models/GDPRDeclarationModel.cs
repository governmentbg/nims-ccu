using Eumis.Data.Users.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eumis.Web.Host.Nancy.Models
{
    public class GDPRDeclarationModel : LayoutModel
    {
        public GDPRDeclarationModel(UserGDPRDeclarationInfoVO declarationInfo, bool isViewOnly = false)
        {
            this.Fullname = declarationInfo.Fullname;
            this.Username = declarationInfo.Username;
            this.Email = declarationInfo.Email;
            this.IsViewOnly = isViewOnly;
        }

        public string Fullname { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsViewOnly { get; set; }
    }
}