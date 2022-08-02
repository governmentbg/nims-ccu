using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Registrations
{
    public partial class RegProjectXml : IAggregateRoot, IEventEmitter
    {
        public string Xml { get; private set; }

        public string Hash { get; private set; }

        public int RegProjectXmlId { get; set; }

        public Guid Gid { get; set; }

        public int RegistrationId { get; set; }

        public int ProcedureId { get; set; }

        public RegProjectXmlStatus Status { get; set; }

        public string ProjectName { get; set; }

        public string CompanyName { get; set; }

        public RegProjectXmlRegType? RegistrationType { get; set; }

        public int? ProjectId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }
    }

    public class RegProjectXmlMap : EntityTypeConfiguration<RegProjectXml>
    {
        public RegProjectXmlMap()
        {
            // Primary Key
            this.HasKey(t => t.RegProjectXmlId);

            // Properties
            this.Property(t => t.RegProjectXmlId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.RegistrationId)
                .IsRequired();

            this.Property(t => t.ProcedureId)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.ProjectName)
                .HasMaxLength(200)
                .IsOptional();

            this.Property(t => t.CompanyName)
                .HasMaxLength(200)
                .IsOptional();

            this.Property(t => t.RegistrationType)
                .IsOptional();

            this.Property(t => t.ProjectId)
                .IsOptional();

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
            this.ToTable("RegProjectXmls");
            this.Property(t => t.RegProjectXmlId).HasColumnName("RegProjectXmlId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.RegistrationId).HasColumnName("RegistrationId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ProjectName).HasColumnName("ProjectName");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.RegistrationType).HasColumnName("RegistrationType");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
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
