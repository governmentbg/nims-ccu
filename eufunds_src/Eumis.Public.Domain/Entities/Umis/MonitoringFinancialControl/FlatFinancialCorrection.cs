using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl
{
    public partial class FlatFinancialCorrection : IAggregateRoot
    {
        private FlatFinancialCorrection()
        {
            this.FlatFinancialCorrectionLevelItems = new List<FlatFinancialCorrectionLevelItem>();
        }

        public FlatFinancialCorrection(
            int programmeId,
            string name,
            int orderNum,
            FlatFinancialCorrectionLevel level,
            FlatFinancialCorrectionType type,
            DateTime? impositionDate,
            string impositionNumber,
            string description,
            Guid? blobKey,
            int? contractId) : this()
        {
            var currentDate = DateTime.Now;

            this.ProgrammeId = programmeId;
            this.Name = name;
            this.OrderNum = orderNum;
            this.Level = level;
            this.Type = type;
            this.Status = FlatFinancialCorrectionStatus.Draft;
            this.ImpositionDate = impositionDate;
            this.ImpositionNumber = impositionNumber;
            this.Description = description;
            this.BlobKey = blobKey;
            this.ContractId = contractId;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int FlatFinancialCorrectionId { get; set; }
        public int ProgrammeId { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }
        public FlatFinancialCorrectionLevel Level { get; set; }
        public FlatFinancialCorrectionType Type { get; set; }
        public FlatFinancialCorrectionStatus Status { get; set; }
        public DateTime? ImpositionDate { get; set; }
        public string ImpositionNumber { get; set; }
        public string Description { get; set; }
        public string Base { get; set; }
        public Guid? BlobKey { get; set; }

        public int? ContractId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public byte[] Version { get; set; }

        public virtual Blob File { get; set; }

        public virtual ICollection<FlatFinancialCorrectionLevelItem> FlatFinancialCorrectionLevelItems { get; set; }
    }

    public class FlatFinancialCorrectionMap : EntityTypeConfiguration<FlatFinancialCorrection>
    {
        public FlatFinancialCorrectionMap()
        {
            // Primary Key
            this.HasKey(t => t.FlatFinancialCorrectionId);

            // Properties
            this.Property(t => t.FlatFinancialCorrectionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.ProgrammeId)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.OrderNum)
                .IsRequired();

            this.Property(t => t.Level)
                .IsRequired();

            this.Property(t => t.Type)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("FlatFinancialCorrections");
            this.Property(t => t.FlatFinancialCorrectionId).HasColumnName("FlatFinancialCorrectionId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.Level).HasColumnName("Level");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ImpositionDate).HasColumnName("ImpositionDate");
            this.Property(t => t.ImpositionNumber).HasColumnName("ImpositionNumber");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Base).HasColumnName("Base");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            //Relationships
            this.HasOptional(t => t.File)
                .WithMany()
                .HasForeignKey(d => d.BlobKey);
        }
    }
}
