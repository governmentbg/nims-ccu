using Eumis.Domain;

namespace Eumis.Data.News.PortalViewObjects
{
    public class NewsFilePVO
    {
        public NewsFilePVO(NewsFile file)
        {
            this.NewsFileId = file.NewsFileId;
            this.Description = file.Description;

            this.File = new FilePVO
            {
                Key = file.BlobKey,
                Name = file.Name,
            };
        }

        public int? NewsFileId { get; set; }

        public string Description { get; set; }

        public FilePVO File { get; set; }
    }
}
