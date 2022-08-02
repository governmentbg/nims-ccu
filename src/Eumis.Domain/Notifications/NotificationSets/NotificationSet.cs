using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Notifications.NotificationSets
{
    public abstract class NotificationSet
    {
        public int NotificationSettingSetId { get; set; }

        public abstract NotificationScope Scope { get; }

        public int NotificationSettingId { get; set; }

        public DateTime ModifyDate { get; set; }

        public virtual NotificationSetting NotificationSetting { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class NotificationSetMap : EntityTypeConfiguration<NotificationSet>
    {
        public NotificationSetMap()
        {
            // Primary Key
            this.HasKey(t => t.NotificationSettingSetId);

            // Properties
            this.Property(t => t.NotificationSettingSetId)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Ignore(t => t.Scope);

            // Table & Column Mappings
            this.ToTable("NotificationSettingSets");
            this.Property(t => t.NotificationSettingSetId).HasColumnName("NotificationSettingSetId");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");

            this.HasRequired(t => t.NotificationSetting)
                .WithMany(t => t.Set)
                .HasForeignKey(t => t.NotificationSettingId)
                .WillCascadeOnDelete();

            this.Map<NotificationProgrammeSet>(t => t.Requires("Scope").HasValue<int>((int)NotificationScope.Programme));
            this.Map<NotificationProgrammePrioritySet>(t => t.Requires("Scope").HasValue<int>((int)NotificationScope.ProgrammePriority));
            this.Map<NotificationProcedureSet>(t => t.Requires("Scope").HasValue<int>((int)NotificationScope.Procedure));
            this.Map<NotificationContractSet>(t => t.Requires("Scope").HasValue<int>((int)NotificationScope.Contract));
        }
    }
}
