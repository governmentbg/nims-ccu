using Eumis.Domain.Core;
using Eumis.Domain.Guidances;
using System;

namespace Eumis.Web.Api.Guidances.DataObjects
{
    public class GuidanceDO
    {
        public GuidanceDO()
        {
        }

        public GuidanceDO(string createdByUser)
        {
            var currentDate = DateTime.Now;

            this.CreatedByUser = createdByUser;
            this.CreateDate = currentDate;
        }

        public GuidanceDO(Guidance guidance, string createdByUser)
        {
            this.GuidanceId = guidance.GuidanceId;
            this.Description = guidance.Description;
            this.Module = guidance.Module;
            this.CreatedByUser = createdByUser;
            this.CreateDate = guidance.CreateDate;
            this.Version = guidance.Version;
            this.File = new FileDO
            {
                Key = guidance.BlobKey,
                Name = guidance.FileName,
            };
        }

        public int? GuidanceId { get; set; }

        public string Description { get; set; }

        public GuidanceModule? Module { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime? CreateDate { get; set; }

        public byte[] Version { get; set; }

        public FileDO File { get; set; }
    }
}
