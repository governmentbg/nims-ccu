using System;

namespace Eumis.Public.Domain.Entities.Umis.Core
{
    public abstract class RioXmlFile
    {
        public int FileId { get; set; }

        public Guid BlobKey { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
