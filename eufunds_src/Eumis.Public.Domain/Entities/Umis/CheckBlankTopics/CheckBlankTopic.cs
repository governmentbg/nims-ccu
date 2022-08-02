using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.CheckBlankTopics
{
    public partial class CheckBlankTopic : IAggregateRoot
    {
        public CheckBlankTopic()
        {
        }

        public CheckBlankTopic(string name)
        {
            var currentDate = DateTime.Now;

            this.Name = name;
            this.Gid = Guid.NewGuid();

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int CheckBlankTopicId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    public class CheckBlankTopicMap : EntityTypeConfiguration<CheckBlankTopic>
    {
        public CheckBlankTopicMap()
        {
            // Primary Key
            this.HasKey(t => t.CheckBlankTopicId);

            // Properties
            this.Property(t => t.CheckBlankTopicId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

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
            this.ToTable("CheckBlankTopics");
            this.Property(t => t.CheckBlankTopicId).HasColumnName("CheckBlankTopicId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
