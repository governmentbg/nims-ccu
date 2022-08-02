using Eumis.Domain;
using System;

namespace Eumis.Web.Api.Declarations.DataObjects
{
    public class DeclarationPublishDO
    {
        public DeclarationPublishDO()
        {
        }

        public DeclarationPublishDO(Declaration declaration)
        {
            this.ActivationDate = declaration.ActivationDate;
            this.Version = declaration.Version;
        }

        public DateTime? ActivationDate { get; set; }

        public byte[] Version { get; set; }
    }
}
