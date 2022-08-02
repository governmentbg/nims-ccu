using System;
using System.Collections.Generic;
namespace Eumis.Documents.Contracts
{
    public class GuidanceVO
    {
        public int GuidanceId { get; set; }

        public string Description { get; set; }

        public Guid FileKey { get; set; }

        public string FileName { get; set; }
    }
}