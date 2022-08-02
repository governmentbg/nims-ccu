using Eumis.Domain.NonAggregates;

namespace Eumis.Data.OperationalMap.MapNodes.ViewObjects
{
    public class MapNodeInterventionCategoryVO
    {
        public int? InterventionCategoryId { get; set; }

        public decimal? Amount { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public Dimension Dimension { get; set; }

        public byte[] Version { get; set; }
    }
}
