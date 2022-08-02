using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public partial class ContractLocation
    {
        private ContractLocation()
        {
        }

        public ContractLocation(
            string nutsCode,
            string name,
            string fullPath,
            string fullPathName,
            string nameAlt,
            string fullPathNameAlt)
        {
            this.NutsCode = nutsCode;
            this.Name = name;
            this.FullPath = fullPath;
            this.FullPathName = fullPathName;
            this.NameAlt = nameAlt;
            this.FullPathNameAlt = fullPathNameAlt;
        }

        public int ContractLocationId { get; set; }

        public int ContractId { get; set; }

        public string NutsCode { get; set; }

        public string Name { get; set; }

        public string FullPath { get; set; }

        public string FullPathName { get; set; }

        public string NameAlt { get; set; }

        public string FullPathNameAlt { get; set; }

        public virtual Contract Contract { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractLocationMap : EntityTypeConfiguration<ContractLocation>
    {
        public ContractLocationMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractLocationId);

            // Properties
            this.Property(t => t.ContractLocationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.NutsCode)
                .IsRequired();
            this.Property(t => t.Name)
                .IsRequired();
            this.Property(t => t.FullPath)
                .IsRequired();
            this.Property(t => t.FullPathName)
                .IsRequired();
            this.Property(t => t.NameAlt)
                .HasMaxLength(200);
            this.Property(t => t.FullPathNameAlt)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("ContractLocations");
            this.Property(t => t.ContractLocationId).HasColumnName("ContractLocationId");
            this.Property(t => t.NutsCode).HasColumnName("NutsCode");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.FullPath).HasColumnName("FullPath");
            this.Property(t => t.FullPathName).HasColumnName("FullPathName");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.FullPathNameAlt).HasColumnName("FullPathNameAlt");

            this.HasRequired(t => t.Contract)
                .WithMany(t => t.ContractLocations)
                .HasForeignKey(t => t.ContractId)
                .WillCascadeOnDelete();
        }
    }
}
