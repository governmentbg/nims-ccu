using System;

namespace Eumis.Portal.Model.Entities
{
    public partial class BlobContent
    {
        public System.Guid Key { get; set; }
        public string Hash { get; set; }
        public Nullable<int> Size { get; set; }
        public byte[] Content { get; set; }
        public bool IsDeleted { get; set; }
    }
}
