using System.Data.Entity.ModelConfiguration;

namespace Eumis.Portal.Model.Entities.Mapping
{
    public class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            // Primary Key
            this.HasKey(t => t.LogId);

            // Properties
            this.Property(t => t.Level)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IP)
                .HasMaxLength(50);

            this.Property(t => t.RawUrl)
                .HasMaxLength(500);

            this.Property(t => t.Form)
                .HasMaxLength(500);

            this.Property(t => t.UserAgent)
                .HasMaxLength(200);

            this.Property(t => t.SessionId)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Logs");
            this.Property(t => t.LogId).HasColumnName("LogId");
            this.Property(t => t.Level).HasColumnName("Level");
            this.Property(t => t.LogDate).HasColumnName("LogDate");
            this.Property(t => t.IP).HasColumnName("IP");
            this.Property(t => t.RawUrl).HasColumnName("RawUrl");
            this.Property(t => t.Form).HasColumnName("Form");
            this.Property(t => t.UserAgent).HasColumnName("UserAgent");
            this.Property(t => t.SessionId).HasColumnName("SessionId");
            this.Property(t => t.RequestId).HasColumnName("RequestId");
            this.Property(t => t.Message).HasColumnName("Message");
        }
    }
}
