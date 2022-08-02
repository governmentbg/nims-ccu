using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Projects
{
    public partial class ProjectVersionXml : IAggregateRoot, IEventEmitter
    {
        public string Xml { get; private set; }

        public string Hash { get; private set; }
        
        public int ProjectVersionXmlId { get; set; }

        public Guid Gid { get; set; }

        public int ProjectId { get; set; }

        public int OrderNum { get; set; }

        public ProjectVersionXmlStatus Status { get; set; }

        public int CreatedByUserId { get; set; }

        public string CreateNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }
    }

    public class ProjectVersionXmlMap : EntityTypeConfiguration<ProjectVersionXml>
    {
        public ProjectVersionXmlMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectVersionXmlId);

            // Properties
            this.Property(t => t.ProjectVersionXmlId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.ProjectId)
                .IsRequired();

            this.Property(t => t.OrderNum)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.CreatedByUserId)
                .IsRequired();

            this.Property(t => t.CreateNote)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProjectVersionXmls");
            this.Property(t => t.ProjectVersionXmlId).HasColumnName("ProjectVersionXmlId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            this.Property(t => t.CreateNote).HasColumnName("CreateNote");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            //RioXmlDocument Mapping
            this.Property(t => t.Xml)
                .IsRequired();

            this.Property(t => t.Hash)
                .IsFixedLength()
                .HasMaxLength(10)
                .IsRequired();

            this.Property(t => t.Xml).HasColumnName("Xml");
            this.Property(t => t.Hash).HasColumnName("Hash");
        }
    }
}
