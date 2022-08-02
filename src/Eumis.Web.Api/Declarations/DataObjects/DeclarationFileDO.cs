using Eumis.Domain;
using Eumis.Domain.Core;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Declarations.DataObjects
{
    public class DeclarationFileDO
    {
        public DeclarationFileDO()
        {
            this.Status = FileStatus.Added;
        }

        public DeclarationFileDO(DeclarationFile file)
        {
            this.DeclarationFileId = file.DeclarationFileId;
            this.Description = file.Description;
            this.Status = FileStatus.Updated;

            this.File = new FileDO
            {
                Key = file.BlobKey,
                Name = file.Name,
            };
        }

        public int? DeclarationFileId { get; set; }

        public string Description { get; set; }

        public FileStatus Status { get; set; }

        public FileDO File { get; set; }
    }
}
