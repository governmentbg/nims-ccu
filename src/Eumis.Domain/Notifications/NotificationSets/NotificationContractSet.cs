using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Notifications.NotificationSets
{
    public class NotificationContractSet : NotificationSet
    {
        public override NotificationScope Scope
        {
            get
            {
                return NotificationScope.Contract;
            }
        }

        public int ContractId { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class NotificationContractSetMap : EntityTypeConfiguration<NotificationContractSet>
    {
        public NotificationContractSetMap()
        {
            // Properties
            this.Property(t => t.ContractId)
                .IsRequired();

            // Table & Column Mappings
            this.Property(t => t.ContractId).HasColumnName("Identifier");
        }
    }
}
