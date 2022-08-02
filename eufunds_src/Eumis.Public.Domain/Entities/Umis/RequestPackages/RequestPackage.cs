using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.Core;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.RequestPackages
{
    public partial class RequestPackage : IAggregateRoot, IEventEmitter
    {

        public int RequestPackageId { get; set; }

        public RequestPackageType Type { get; set; }

        public string Code { get; set; }

        public RequestPackageStatus Status { get; set; }

        public int? EnteredByUserId { get; set; }

        public int? CheckedByUserId { get; set; }

        public int? EndedByUserId { get; set; }

        public int? UserOrganizationId { get; set; }

        public string PackageDescription { get; set; }

        public Guid? BlobKey1 { get; set; }

        public string Description1 { get; set; }

        public Guid? BlobKey2 { get; set; }

        public string Description2 { get; set; }

        public Guid? BlobKey3 { get; set; }

        public string Description3 { get; set; }

        public Guid? BlobKey4 { get; set; }

        public string Description4 { get; set; }

        public Guid? BlobKey5 { get; set; }

        public string Description5 { get; set; }

        public string EndedMessage { get; set; }

        public byte[] Version { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public virtual ICollection<RequestPackageUser> RequestPackageUsers { get; set; }

        public virtual Blob File1 { get; set; }

        public virtual Blob File2 { get; set; }

        public virtual Blob File3 { get; set; }

        public virtual Blob File4 { get; set; }

        public virtual Blob File5 { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }
    }

    public class RequestPackageMap : EntityTypeConfiguration<RequestPackage>
    {
        public RequestPackageMap()
        {
            // Primary Key
            this.HasKey(t => t.RequestPackageId);

            // Properties
            this.Property(t => t.RequestPackageId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.Type)
                .IsRequired();

            this.Property(t => t.Code)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("RequestPackages");
            this.Property(t => t.RequestPackageId).HasColumnName("RequestPackageId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.EnteredByUserId).HasColumnName("EnteredByUserId");
            this.Property(t => t.CheckedByUserId).HasColumnName("CheckedByUserId");
            this.Property(t => t.EndedByUserId).HasColumnName("EndedByUserId");
            this.Property(t => t.UserOrganizationId).HasColumnName("UserOrganizationId");
            this.Property(t => t.PackageDescription).HasColumnName("PackageDescription");
            this.Property(t => t.BlobKey1).HasColumnName("BlobKey1");
            this.Property(t => t.Description1).HasColumnName("Description1");
            this.Property(t => t.BlobKey2).HasColumnName("BlobKey2");
            this.Property(t => t.Description2).HasColumnName("Description2");
            this.Property(t => t.BlobKey3).HasColumnName("BlobKey3");
            this.Property(t => t.Description3).HasColumnName("Description3");
            this.Property(t => t.BlobKey4).HasColumnName("BlobKey4");
            this.Property(t => t.Description4).HasColumnName("Description4");
            this.Property(t => t.BlobKey5).HasColumnName("BlobKey5");
            this.Property(t => t.Description5).HasColumnName("Description5");
            this.Property(t => t.EndedMessage).HasColumnName("EndedMessage");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            //Relationships
            this.HasOptional(t => t.File1)
                .WithMany()
                .HasForeignKey(d => d.BlobKey1);

            this.HasOptional(t => t.File2)
                .WithMany()
                .HasForeignKey(d => d.BlobKey2);

            this.HasOptional(t => t.File3)
                .WithMany()
                .HasForeignKey(d => d.BlobKey3);

            this.HasOptional(t => t.File4)
                .WithMany()
                .HasForeignKey(d => d.BlobKey4);

            this.HasOptional(t => t.File5)
                .WithMany()
                .HasForeignKey(d => d.BlobKey5);
        }
    }
}
