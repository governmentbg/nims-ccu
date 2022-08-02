using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.ExpenseTypes
{
    public class ExpenseSubType
    {
        public ExpenseSubType()
        {
        }

        public int ExpenseSubTypeId { get; set; }

        public int ExpenseTypeId { get; set; }

        public string Name { get; set; }

        public virtual ExpenseType ExpenseType { get; set; }

        internal void SetAttributes(string name)
        {
            this.Name = name;
        }
    }

    public class ExpenseSubTypeMap : EntityTypeConfiguration<ExpenseSubType>
    {
        public ExpenseSubTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ExpenseSubTypeId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ExpenseSubTypes");
            this.Property(t => t.ExpenseSubTypeId).HasColumnName("ExpenseSubTypeId");
            this.Property(t => t.ExpenseTypeId).HasColumnName("ExpenseTypeId");
            this.Property(t => t.Name).HasColumnName("Name");

            // Relationships
            this.HasRequired(t => t.ExpenseType)
                .WithMany(t => t.ExpenseSubTypes)
                .HasForeignKey(d => d.ExpenseTypeId)
                .WillCascadeOnDelete();
        }
    }
}
