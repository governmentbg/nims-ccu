namespace Eumis.Documents.News
{
    public class NewsFilePVO
    {
        public int? NewsFileId { get; set; }

        public string Description { get; set; }

        public FilePVO File { get; set; }
    }
}
