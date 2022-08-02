using System;
using Eumis.Rio;

namespace Eumis.Domain.Core
{
    public abstract class RioXmlFile
    {
        public RioXmlFile()
        {
        }

        public RioXmlFile(AttachedDocument attachedDocument)
        {
            this.BlobKey = new Guid(attachedDocument.AttachedDocumentContent.BlobContentId);
            this.Name = attachedDocument.AttachedDocumentContent.FileName;
            this.Description = attachedDocument.Description;
        }

        public int FileId { get; private set; }

        public Guid BlobKey { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }
    }
}
