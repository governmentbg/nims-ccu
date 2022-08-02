using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.OperationalMap.MapNodes
{
    public abstract partial class MapNodeWithDirections : MapNode
    {
        protected MapNodeWithDirections()
            : base()
        {
            this.MapNodeDirections = new List<MapNodeDirection>();
        }

        protected MapNodeWithDirections(string code, string shortName, string name, string nameAlt)
            : base(code, shortName, name, nameAlt)
        {
            this.MapNodeDirections = new List<MapNodeDirection>();
        }

        public virtual ICollection<MapNodeDirection> MapNodeDirections { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class MapNodeWithDirectionsMap : EntityTypeConfiguration<MapNodeWithDirections>
    {
        public MapNodeWithDirectionsMap()
        {
        }
    }
}
