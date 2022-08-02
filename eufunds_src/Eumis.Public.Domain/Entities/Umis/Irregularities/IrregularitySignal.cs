using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Irregularities
{
    public partial class IrregularitySignal : IAggregateRoot
    {
        public IrregularitySignal()
        {
            this.Documents = new List<IrregularitySignalDoc>();
            this.InvolvedPersons = new List<IrregularitySignalInvolvedPerson>();
        }

        public IrregularitySignal(
            int programmeId,
            int procedureId,
            int projectId,
            int contractId)
        {
            this.ProgrammeId = programmeId;
            this.ProcedureId = procedureId;
            this.ProjectId = projectId;
            this.ContractId = contractId;

            this.Status = IrregularitySignalStatus.Draft;
            this.IsActivated = false;

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int IrregularitySignalId { get; set; }

        public int ProgrammeId { get; set; }

        public int ProcedureId { get; set; }

        public int ProjectId { get; set; }

        public int ContractId { get; set; }

        public IrregularitySignalStatus Status { get; set; }

        public int? Number { get; set; }

        public string RegNumber { get; set; }

        public DateTime? RegDate { get; set; }

        public string SignalSource { get; set; }

        public DateTime? MASystemRegDate { get; set; }

        public string SignalKind { get; set; }

        public string ViolationDesrc { get; set; }

        public string Actions { get; set; }

        public string ActRegNum { get; set; }

        public DateTime? ActRegDate { get; set; }

        public string Comment { get; set; }

        public bool IsActivated { get; set; }

        public string DeleteNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ICollection<IrregularitySignalDoc> Documents { get; set; }

        public ICollection<IrregularitySignalInvolvedPerson> InvolvedPersons { get; set; }
    }

    public class IrregularitySignalMap : EntityTypeConfiguration<IrregularitySignal>
    {
        public IrregularitySignalMap()
        {
            // Primary Key
            this.HasKey(t => t.IrregularitySignalId);

            // Properties
            this.Property(t => t.IrregularitySignalId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProgrammeId)
                .IsRequired();
            this.Property(t => t.ProcedureId)
                .IsRequired();
            this.Property(t => t.ProjectId)
                .IsRequired();
            this.Property(t => t.ContractId)
                .IsRequired();
            this.Property(t => t.Status)
                .IsRequired();
            this.Property(t => t.RegNumber)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.ActRegNum)
                .HasMaxLength(50)
                .IsOptional();
            this.Property(t => t.SignalSource)
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
            this.ToTable("IrregularitySignals");
            this.Property(t => t.IrregularitySignalId).HasColumnName("IrregularitySignalId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Status).HasColumnName("Status");

            this.Property(t => t.Number).HasColumnName("Number");
            this.Property(t => t.RegNumber).HasColumnName("RegNumber");
            this.Property(t => t.RegDate).HasColumnName("RegDate");
            this.Property(t => t.SignalSource).HasColumnName("SignalSource");
            this.Property(t => t.MASystemRegDate).HasColumnName("MASystemRegDate");
            this.Property(t => t.SignalKind).HasColumnName("SignalKind");
            this.Property(t => t.ViolationDesrc).HasColumnName("ViolationDesrc");
            this.Property(t => t.Actions).HasColumnName("Actions");
            this.Property(t => t.ActRegNum).HasColumnName("ActRegNum");
            this.Property(t => t.ActRegDate).HasColumnName("ActRegDate");
            this.Property(t => t.Comment).HasColumnName("Comment");

            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.DeleteNote).HasColumnName("DeleteNote");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
