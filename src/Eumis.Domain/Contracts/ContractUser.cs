using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Contracts
{
    public class ContractUser
    {
        public ContractUser()
        {
        }

        public ContractUser(int userId)
            : this()
        {
            this.UserId = userId;
        }

        public int ContractUserId { get; set; }

        public int ContractId { get; set; }

        public int UserId { get; set; }

        public virtual Contract Contract { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractUserMap : EntityTypeConfiguration<ContractUser>
    {
        public ContractUserMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ContractUserId });

            // Properties
            this.Property(t => t.ContractUserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.UserId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractUsers");
            this.Property(t => t.ContractUserId).HasColumnName("ContractUserId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.UserId).HasColumnName("UserId");

            // Relationships
            this.HasRequired(t => t.Contract)
                .WithMany(t => t.ContractUsers)
                .HasForeignKey(t => t.ContractId)
                .WillCascadeOnDelete();
        }
    }
}
