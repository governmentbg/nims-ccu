using System;

namespace Eumis.Web.Api.Core
{
    public class FilePVO
    {
        public FilePVO()
        {
        }

        public Guid Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
