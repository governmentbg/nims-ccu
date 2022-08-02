using Eumis.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Procurements.ViewObjects
{
    public class ProcurementDocumentVO
    {
        public int ProcurementDocumentId { get; set; }

        public int ProcurementId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileVO File { get; set; }
    }
}
