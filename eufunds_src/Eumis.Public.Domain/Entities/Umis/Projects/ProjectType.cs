using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Projects
{
    public class ProjectType
    {
        public ProjectType()
        {
        }

        public int ProjectTypeId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }
    }

    public class ProjectTypeMap : EntityTypeConfiguration<ProjectType>
    {
        public ProjectTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectTypeId);

            // Properties
            this.Property(t => t.ProjectTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Alias)
                .HasMaxLength(50)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("ProjectTypes");
            this.Property(t => t.ProjectTypeId).HasColumnName("ProjectTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
        }
    }
}
