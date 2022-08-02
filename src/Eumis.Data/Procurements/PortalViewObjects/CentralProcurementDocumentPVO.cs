using Eumis.Data.Core.Nomenclatures;
using System;

namespace Eumis.Data.Procurements.PortalViewObjects
{
    public class CentralProcurementDocumentPVO
    {
        public EntityGidNomVO ProcurementDocument { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? BlobKey { get; set; }
    }
}
