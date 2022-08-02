using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Notifications.NotificationSets
{
    public class NotificationProgrammePrioritySet : NotificationSet
    {
        public override NotificationScope Scope
        {
            get
            {
                return NotificationScope.Programme;
            }
        }

        public int ProgrammePriorityId { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class NotificationProgrammePrioritySetMap : EntityTypeConfiguration<NotificationProgrammePrioritySet>
    {
        public NotificationProgrammePrioritySetMap()
        {
            // Properties
            this.Property(t => t.ProgrammePriorityId)
                .IsRequired();

            // Table & Column Mappings
            this.Property(t => t.ProgrammePriorityId).HasColumnName("Identifier");
        }
    }
}
