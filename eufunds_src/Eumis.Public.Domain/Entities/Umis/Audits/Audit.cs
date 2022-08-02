using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Audits
{
    public partial class Audit : IAggregateRoot
    {
        public Audit()
        {
            this.Documents = new List<AuditDoc>();
            this.Ascertainments = new List<AuditAscertainment>();
            this.LevelItems = new List<AuditLevelItem>();
        }

        public Audit(
            int programmeId,
            int? contractId,
            AuditLevel level,
            AuditInstitution auditInstitution,
            AuditType auditType,
            AuditKind auditKind) : this()
        {
            this.ProgrammeId = programmeId;
            this.Level = level;
            this.AuditInstitution = auditInstitution;
            this.AuditType = auditType;
            this.AuditKind = auditKind;

            if (level == AuditLevel.ContractContract)
            {
                this.ContractId = contractId.Value;
            }

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int AuditId { get; set; }

        public int ProgrammeId { get; set; }

        public AuditInstitution AuditInstitution { get; set; }

        public AuditType AuditType { get; set; }

        public AuditKind AuditKind { get; set; }

        public AuditLevel Level { get; set; }

        public int? ContractId { get; set; }

        public DateTime? FinalReportDate { get; set; }

        public string FinalReportNum { get; set; }

        public DateTime? CheckStartDate { get; set; }

        public DateTime? CheckEndDate { get; set; }

        public AuditSubjectType? AuditSubjectType { get; set; }

        public string AuditSubjectName { get; set; }

        public string Comment { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ICollection<AuditDoc> Documents { get; set; }

        public ICollection<AuditAscertainment> Ascertainments { get; set; }

        public ICollection<AuditLevelItem> LevelItems { get; set; }
    }

    public class AuditMap : EntityTypeConfiguration<Audit>
    {
        public AuditMap()
        {
            // Primary Key
            this.HasKey(t => t.AuditId);

            // Properties
            this.Property(t => t.AuditId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProgrammeId)
                .IsRequired();
            this.Property(t => t.AuditInstitution)
                .IsRequired();
            this.Property(t => t.AuditType)
                .IsRequired();
            this.Property(t => t.AuditKind)
                .IsRequired();
            this.Property(t => t.Level)
                .IsRequired();
            this.Property(t => t.FinalReportNum)
                .HasMaxLength(100)
                .IsOptional();
            this.Property(t => t.AuditSubjectName)
                .HasMaxLength(500)
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
            this.ToTable("Audits");
            this.Property(t => t.AuditId).HasColumnName("AuditId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.AuditInstitution).HasColumnName("AuditInstitution");
            this.Property(t => t.AuditType).HasColumnName("AuditType");
            this.Property(t => t.AuditKind).HasColumnName("AuditKind");
            this.Property(t => t.Level).HasColumnName("Level");
            this.Property(t => t.ContractId).HasColumnName("ContractId");

            this.Property(t => t.FinalReportDate).HasColumnName("FinalReportDate");
            this.Property(t => t.FinalReportNum).HasColumnName("FinalReportNum");

            this.Property(t => t.CheckStartDate).HasColumnName("CheckStartDate");
            this.Property(t => t.CheckEndDate).HasColumnName("CheckEndDate");
            this.Property(t => t.AuditSubjectType).HasColumnName("AuditSubjectType");
            this.Property(t => t.AuditSubjectName).HasColumnName("AuditSubjectName");
            this.Property(t => t.Comment).HasColumnName("Comment");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
