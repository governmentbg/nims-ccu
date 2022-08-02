using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.ExpenseTypes
{
    public partial class ExpenseType : IAggregateRoot
    {
        private ExpenseType()
        {
            this.ExpenseSubTypes = new List<ExpenseSubType>();
        }

        public ExpenseType(string name, string nameAlt, bool isActive)
            : this()
        {
            var currentDate = DateTime.Now;

            this.Name = name;
            this.NameAlt = nameAlt;
            this.IsActive = isActive;
            this.Gid = Guid.NewGuid();

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ExpenseTypeId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<ExpenseSubType> ExpenseSubTypes { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ExpenseTypeMap : EntityTypeConfiguration<ExpenseType>
    {
        public ExpenseTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ExpenseTypeId);

            // Properties
            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.IsActive)
                .IsRequired();

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ExpenseTypes");
            this.Property(t => t.ExpenseTypeId).HasColumnName("ExpenseTypeId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
