using System;

namespace Eumis.Data.Guidances.ViewObjects
{
    public class GuidanceVO
    {
        public int GuidanceId { get; set; }

        public string Description { get; set; }

        public Guid FileKey { get; set; }

        public string FileName { get; set; }
    }
}
