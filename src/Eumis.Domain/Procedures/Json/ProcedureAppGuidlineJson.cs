using System;

namespace Eumis.Domain.Procedures.Json
{
    public class ProcedureAppGuidlineJson
    {
        public ProcedureAppGuidlineJson()
        {
        }

        public ProcedureAppGuidlineJson(ProcedureApplicationGuideline appGuideline)
        {
            this.AppGuidelineId = appGuideline.ProcedureApplicationGuidelineId;
            this.Gid = appGuideline.Gid;
            this.Name = appGuideline.Name;
            this.Description = appGuideline.Decription;
            this.BlobKey = appGuideline.BlobKey;
            this.Filename = appGuideline.File.FileName;
        }

        public int AppGuidelineId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid BlobKey { get; set; }

        public string Filename { get; set; }
    }
}
