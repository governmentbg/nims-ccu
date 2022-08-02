using Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.SpecificTargets
{
    public partial class SpecificTarget : MapNode
    {
        public SpecificTarget()
        {
        }

        public override MapNodeType Type
        {
            get
            {
                return MapNodeType.SpecificTarget;
            }
        }
    }

    public class SpecificTargetMap : EntityTypeConfiguration<SpecificTarget>
    {
        public SpecificTargetMap()
        {
        }
    }
}
