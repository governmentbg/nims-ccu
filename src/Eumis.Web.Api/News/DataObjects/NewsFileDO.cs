using Eumis.Domain;
using Eumis.Domain.Core;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.News.DataObjects
{
    public class NewsFileDO
    {
        public NewsFileDO()
        {
            this.Status = FileStatus.Added;
        }

        public NewsFileDO(NewsFile file)
        {
            this.NewsFileId = file.NewsFileId;
            this.Description = file.Description;
            this.Status = FileStatus.Updated;

            this.File = new FileDO
            {
                Key = file.BlobKey,
                Name = file.Name,
            };
        }

        public int? NewsFileId { get; set; }

        public string Description { get; set; }

        public FileStatus Status { get; set; }

        public FileDO File { get; set; }
    }
}
