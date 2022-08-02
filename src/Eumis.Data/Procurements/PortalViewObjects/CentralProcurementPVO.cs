using Eumis.Data.Core.Nomenclatures;
using System;
using System.Collections.Generic;

namespace Eumis.Data.Procurements.PortalViewObjects
{
    public class CentralProcurementPVO
    {
        public CentralProcurementPVO()
        {
            this.DifferentiatedPositions = new List<CentralDifferentiatedPositionPVO>();
            this.ProcurementDocuments = new List<CentralProcurementDocumentPVO>();
            this.ProcurementPlan = new CentralProcurementPlan();
        }

        public EntityGidNomVO CentralProcurement { get; set; }

        public string PPANumber { get; set; }

        public decimal? ExpectedAmount { get; set; }

        public DateTime? OffersDeadlineDate { get; set; }

        public DateTime? AnnouncedDate { get; set; }

        public CentralProcurementPlan ProcurementPlan { get; set; }

        public IList<CentralDifferentiatedPositionPVO> DifferentiatedPositions { get; set; }

        public IList<CentralProcurementDocumentPVO> ProcurementDocuments { get; set; }
    }
}
