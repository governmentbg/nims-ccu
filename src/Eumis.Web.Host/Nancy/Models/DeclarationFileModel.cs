using System;

namespace Eumis.Web.Host.Nancy.Models
{
    public class DeclarationFileModel
    {
        public Guid BlobKey { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
