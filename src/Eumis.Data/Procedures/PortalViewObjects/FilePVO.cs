using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class FilePVO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public Guid? FileKey { get; set; }
    }
}
