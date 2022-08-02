using Eumis.Data.Core.Nomenclatures;

namespace Eumis.Data.Procurements.PortalViewObjects
{
    public class CentralProcurementPlan
    {
        public string Name { get; set; }

        public EntityCodeNomVO ErrandArea { get; set; }

        public EntityGidNomVO ErrandLegalAct { get; set; }

        public EntityCodeNomVO ErrandType { get; set; }

        public string Description { get; set; }
    }
}
