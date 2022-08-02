using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.InvestmentPriorities
{
    public partial class InvestmentPriority : MapNode
    {
        public InvestmentPriority()
        {
        }

        public override MapNodeType Type
        {
            get
            {
                return MapNodeType.InvestmentPriority;
            }
        }
        public int RegulationInvestmentPriorityId { get; set; }

        public bool IsHidden { get; set; }

        public virtual RegulationInvestmentPriority RegulationInvestmentPriority { get; set; }
    }

    public class InvestmentPriorityMap : EntityTypeConfiguration<InvestmentPriority>
    {
        public InvestmentPriorityMap()
        {
            // Properties
            this.Property(t => t.Code)
                .IsRequired();
            this.Property(t => t.ShortName)
                .IsRequired();
            this.Property(t => t.RegulationInvestmentPriorityId)
                .IsRequired();
            this.Property(t => t.IsHidden)
                .IsRequired();

            // Table & Column Mappings
            this.Property(t => t.RegulationInvestmentPriorityId).HasColumnName("InvestmentPriorityId");
            this.Property(t => t.IsHidden).HasColumnName("IsHidden");

            this.HasRequired(t => t.RegulationInvestmentPriority)
                .WithMany()
                .HasForeignKey(t => t.RegulationInvestmentPriorityId);
        }
    }
}
