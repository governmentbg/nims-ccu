using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public class ProcedureType
    {
        public ProcedureType()
        {
        }

        public int ProcedureTypeId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }
    }

    public class ProcedureTypeMap : EntityTypeConfiguration<ProcedureType>
    {
        public ProcedureTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureTypeId);

            // Properties
            this.Property(t => t.ProcedureTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("ProcedureTypes");
            this.Property(t => t.ProcedureTypeId).HasColumnName("ProcedureTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
        }
    }
}
