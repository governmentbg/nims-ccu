using System;
using Eumis.Domain.Procedures.Json;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class AppGuidelinePVO
    {
        public AppGuidelinePVO()
        {
        }

        public AppGuidelinePVO(ProcedureAppGuidlineJson appGuideline)
        {
            this.Gid = appGuideline.Gid;
            this.Name = appGuideline.Name;
            this.Description = appGuideline.Description;
            this.Filename = appGuideline.Filename;
            this.BlobKey = appGuideline.BlobKey;
        }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Filename { get; set; }

        public DateTime StatusDate { get; set; }

        public Guid BlobKey { get; set; }
    }
}
