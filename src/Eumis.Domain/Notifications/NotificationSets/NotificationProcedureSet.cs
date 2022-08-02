using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Notifications.NotificationSets
{
    public class NotificationProcedureSet : NotificationSet
    {
        public override NotificationScope Scope
        {
            get
            {
                return NotificationScope.Procedure;
            }
        }

        public int ProcedureId { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class NotificationProcedureSetMap : EntityTypeConfiguration<NotificationProcedureSet>
    {
        public NotificationProcedureSetMap()
        {
            // Properties
            this.Property(t => t.ProcedureId)
                .IsRequired();

            // Table & Column Mappings
            this.Property(t => t.ProcedureId).HasColumnName("Identifier");
        }
    }
}
