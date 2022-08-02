using Eumis.Public.Domain.Entities.Umis.OperationalMap.Programmes;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes
{
    public class MapNodeInstitution
    {
        public MapNodeInstitution()
        {
        }

        public int MapNodeId { get; set; }
        public int InstitutionId { get; set; }
        public int InstitutionTypeId { get; set; }
        public string ContactName { get; set; }
        public string ContactPosition { get; set; }
        public string ContactPhone { get; set; }
        public string ContactFax { get; set; }
        public string ContactEmail { get; set; }

        public virtual Programme Programme { get; set; }

        internal void SetAttributes(
            int institutionTypeId,
            string name,
            string position,
            string phone,
            string fax,
            string email)
        {
            this.InstitutionTypeId = institutionTypeId;
            this.ContactName = name;
            this.ContactPosition = position;
            this.ContactPhone = phone;
            this.ContactFax = fax;
            this.ContactEmail = email;
        }
    }

    public class MapNodeInstitutionMap : EntityTypeConfiguration<MapNodeInstitution>
    {
        public MapNodeInstitutionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.MapNodeId, t.InstitutionId });

            // Properties
            this.Property(t => t.MapNodeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.InstitutionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ContactName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.ContactPosition)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ContactPhone)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ContactFax)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ContactEmail)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MapNodeInstitutions");
            this.Property(t => t.MapNodeId).HasColumnName("MapNodeId");
            this.Property(t => t.InstitutionId).HasColumnName("InstitutionId");
            this.Property(t => t.InstitutionTypeId).HasColumnName("InstitutionTypeId");
            this.Property(t => t.ContactName).HasColumnName("ContactName");
            this.Property(t => t.ContactPosition).HasColumnName("ContactPosition");
            this.Property(t => t.ContactPhone).HasColumnName("ContactPhone");
            this.Property(t => t.ContactFax).HasColumnName("ContactFax");
            this.Property(t => t.ContactEmail).HasColumnName("ContactEmail");

            // Relationships
            this.HasRequired(t => t.Programme)
                .WithMany(t => t.ProgrammeInstitutions)
                .HasForeignKey(d => d.MapNodeId);

        }
    }
}
