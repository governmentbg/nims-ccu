using Eumis.Public.Common.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Public.Data.UmisVOs
{
    public class OpenDataVO
    {
        public List<ExportProject> Projects { get; set; }

        public List<ExportContract> Contracts { get; set; }

        public List<ExportEntity> Entities { get; set; }
    }
}
