using System;

namespace Eumis.Portal.Model.Entities
{
    public partial class Blob
    {
        public System.Guid Key { get; set; }
        public string Hash { get; set; }
        public Nullable<int> Size { get; set; }
    }
}
