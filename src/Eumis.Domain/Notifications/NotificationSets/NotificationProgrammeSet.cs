using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Notifications.NotificationSets
{
    public class NotificationProgrammeSet : NotificationSet
    {
        public override NotificationScope Scope
        {
            get
            {
                return NotificationScope.Programme;
            }
        }

        public int ProgrammeId { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class NotificationProgrammeSetMap : EntityTypeConfiguration<NotificationProgrammeSet>
    {
        public NotificationProgrammeSetMap()
        {
            // Properties
            this.Property(t => t.ProgrammeId)
                .IsRequired();

            // Table & Column Mappings
            this.Property(t => t.ProgrammeId).HasColumnName("Identifier");
        }
    }
}
