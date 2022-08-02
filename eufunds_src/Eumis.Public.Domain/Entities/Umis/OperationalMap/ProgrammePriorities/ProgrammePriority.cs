using Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.ProgrammePriorities
{
    public partial class ProgrammePriority : MapNodeWithCategories
    {
        public ProgrammePriority()
        {
        }

        public override MapNodeType Type
        {
            get
            {
                return MapNodeType.ProgrammePriority;
            }
        }

        public string Description { get; set; }

        public string DescriptionAlt { get; set; }

        public virtual ICollection<MapNodeBudget> ProgrammePriorityBudgets { get; set; }
    }

    public class ProgrammePriorityMap : EntityTypeConfiguration<ProgrammePriority>
    {
        public ProgrammePriorityMap()
        {
            // Properties
            this.Property(t => t.Code)
                .IsRequired();
            this.Property(t => t.ShortName)
                .IsRequired();

            // Table & Column Mappings
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.DescriptionAlt).HasColumnName("DescriptionAlt");
        }
    }
}
