using Eumis.Domain.Core;
using Eumis.Domain.Messages;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Messages.DataObjects
{
    public class MessageFileDO
    {
        public MessageFileDO()
        {
            this.Status = FileStatus.Added;
        }

        public MessageFileDO(MessageFile file)
        {
            this.MessageFileId = file.MessageFileId;
            this.Description = file.Description;
            this.Status = FileStatus.Updated;

            this.File = new FileDO
            {
                Key = file.BlobKey,
                Name = file.Name,
            };
        }

        public int? MessageFileId { get; set; }

        public string Description { get; set; }

        public FileStatus Status { get; set; }

        public FileDO File { get; set; }
    }
}
