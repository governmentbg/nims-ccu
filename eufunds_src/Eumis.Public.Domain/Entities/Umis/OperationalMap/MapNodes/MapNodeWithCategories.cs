using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes
{
    public abstract partial class MapNodeWithCategories : MapNode
    {
        protected MapNodeWithCategories()
            :base()
        {
            this.MapNodeInterventionCategories = new List<MapNodeInterventionCategory>();
        }

        public virtual ICollection<MapNodeInterventionCategory> MapNodeInterventionCategories { get; set; }
    }

    public class MapNodeWithCategoriesMap : EntityTypeConfiguration<MapNodeWithCategories>
    {
        public MapNodeWithCategoriesMap()
        {
        }
    }
}
