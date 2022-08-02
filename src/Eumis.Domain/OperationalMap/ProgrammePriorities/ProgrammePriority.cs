using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.OperationalMap.MapNodes;

namespace Eumis.Domain.OperationalMap.ProgrammePriorities
{
    public partial class ProgrammePriority : MapNodeWithDirections
    {
        public ProgrammePriority()
        {
        }

        public ProgrammePriority(
            string code,
            string name,
            string nameAlt,
            string description,
            string descriptionAlt,
            ProgrammePriorityType programmePriorityType,
            int companyId,
            int? higherOrderCompanyId)
            : base(code, code, name, nameAlt)
        {
            this.Description = description;
            this.DescriptionAlt = descriptionAlt;

            this.SetCompanyData(programmePriorityType, companyId, higherOrderCompanyId);
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

        public virtual ProgrammePriorityCompany CompanyData { get; set; }

        public virtual ICollection<MapNodeBudget> ProgrammePriorityBudgets { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProgrammePriorityMap : EntityTypeConfiguration<ProgrammePriority>
    {
        public ProgrammePriorityMap()
        {
            // Properties
            this.Property(t => t.Code)
                .IsRequired();

            // Table & Column Mappings
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.DescriptionAlt).HasColumnName("DescriptionAlt");

            // Relationships
            this.HasOptional(t => t.CompanyData)
                .WithRequired(t => t.ProgrammePriority);
        }
    }
}
