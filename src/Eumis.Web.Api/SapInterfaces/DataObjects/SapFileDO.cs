using Eumis.Domain.Core;
using Eumis.Domain.SapInterfaces;
using System;

namespace Eumis.Web.Api.SapInterfaces.DataObjects
{
    public class SapFileDO
    {
        public SapFileDO()
        {
        }

        public SapFileDO(SapFile sapFile, string createdByUser)
        {
            this.SapFileId = sapFile.SapFileId;
            this.Status = sapFile.Status;
            this.Type = sapFile.Type;
            this.SapKey = sapFile.SapKey;
            this.SapDate = sapFile.SapDate;
            this.SapUser = sapFile.SapUser;
            this.CreatedByUser = createdByUser;

            this.File = new FileDO
            {
                Key = sapFile.FileKey,
                Name = sapFile.FileName,
            };

            this.CreateDate = sapFile.CreateDate;
            this.Version = sapFile.Version;
        }

        public int SapFileId { get; set; }

        public SapFileStatus Status { get; set; }

        public SapFileType Type { get; set; }

        public FileDO File { get; set; }

        public string SapKey { get; set; }

        public DateTime SapDate { get; set; }

        public string SapUser { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime CreateDate { get; set; }

        public byte[] Version { get; set; }
    }
}
