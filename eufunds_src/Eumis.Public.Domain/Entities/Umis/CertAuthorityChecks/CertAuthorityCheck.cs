using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.CertAuthorityChecks
{
    public partial class CertAuthorityCheck : IAggregateRoot
    {
        public CertAuthorityCheck()
        {
            this.Ascertainments = new List<CertAuthorityCheckAscertainment>();
            this.LevelItems = new List<CertAuthorityCheckLevelItem>();
            this.CertAuthorityCheckDocuments = new List<CertAuthorityCheckDocument>();
        }

        public CertAuthorityCheck(
            CertAuthorityCheckLevel level,
            CertAuthorityCheckKind kind,
            CertAuthorityCheckType type)
            : this()
        {
            this.Status = CertAuthorityCheckStatus.Draft;
            this.Level = level;
            this.Kind = kind;
            this.Type = type;
            this.IsActivated = false;

            this.CreateDate = this.ModifyDate = DateTime.Now;
        }

        public int CertAuthorityCheckId { get; set; }

        public CertAuthorityCheckStatus Status { get; set; }

        public CertAuthorityCheckLevel Level { get; set; }

        public CertAuthorityCheckKind Kind { get; set; }

        public CertAuthorityCheckType Type { get; set; }

        public int? CheckNum { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public CertAuthorityCheckSubjectType? SubjectType { get; set; }

        public string SubjectName { get; set; }

        public string Team { get; set; }

        public bool IsActivated { get; set; }

        public string DeleteNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ICollection<CertAuthorityCheckAscertainment> Ascertainments { get; set; }

        public ICollection<CertAuthorityCheckLevelItem> LevelItems { get; set; }

        public ICollection<CertAuthorityCheckDocument> CertAuthorityCheckDocuments { get; set; }
    }

    public class CertAuthorityCheckMap : EntityTypeConfiguration<CertAuthorityCheck>
    {
        public CertAuthorityCheckMap()
        {
            // Primary Key
            this.HasKey(t => t.CertAuthorityCheckId);

            // Properties
            this.Property(t => t.CertAuthorityCheckId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Status)
                .IsRequired();
            this.Property(t => t.Level)
                .IsRequired();
            this.Property(t => t.Kind)
                .IsRequired();
            this.Property(t => t.Type)
                .IsRequired();
            this.Property(t => t.SubjectName)
                .HasMaxLength(500)
                .IsOptional();
            this.Property(t => t.Team)
                .HasMaxLength(500)
                .IsOptional();
            this.Property(t => t.IsActivated)
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
            this.ToTable("CertAuthorityChecks");
            this.Property(t => t.CertAuthorityCheckId).HasColumnName("CertAuthorityCheckId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Level).HasColumnName("Level");
            this.Property(t => t.Kind).HasColumnName("CheckKind");
            this.Property(t => t.Type).HasColumnName("CheckType");
            this.Property(t => t.CheckNum).HasColumnName("CheckNum");

            this.Property(t => t.DateFrom).HasColumnName("DateFrom");
            this.Property(t => t.DateTo).HasColumnName("DateTo");
            this.Property(t => t.SubjectType).HasColumnName("SubjectType");
            this.Property(t => t.SubjectName).HasColumnName("SubjectName");
            this.Property(t => t.Team).HasColumnName("Team");

            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.DeleteNote).HasColumnName("DeleteNote");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
