using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Data.UmisVOs
{
    public class RegionVO
    {
        public int Id { get; set; }

        public NutsLevel NutsLevel { get; set; }

        public string FullPath { get; set; }
    }
}
