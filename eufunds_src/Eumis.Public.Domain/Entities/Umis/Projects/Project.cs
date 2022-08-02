using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.Projects
{
    public partial class Project : IAggregateRoot
    {
        public int ProjectId { get; set; }

        public Guid Gid { get; set; }

        public int ProcedureId { get; set; }

        public int ProjectTypeId { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyUin { get; set; }

        public UinType CompanyUinType { get; set; }

        public int? CompanyKidCodeId { get; set; }

        public int CompanySizeTypeId { get; set; }

        public int CompanyLegalTypeId { get; set; }

        public string CompanyEmail { get; set; }

        public int? CompanySeatCountryId { get; set; }

        public int? CompanySeatSettlementId { get; set; }

        public string CompanySeatPostCode { get; set; }

        public string CompanySeatStreet { get; set; }

        public string CompanySeatAddress { get; set; }

        public string Name { get; set; }

        public int? KidCodeId { get; set; }

        public ProjectRegistrationStatus RegistrationStatus { get; set; }

        public string RegNumber { get; set; }

        public DateTime RegDate { get; set; }

        public ProjectRecieveType RecieveType { get; set; }

        public DateTime RecieveDate { get; set; }

        public DateTime SubmitDate { get; set; }

        public string StoragePlace { get; set; }

        public int? Originals { get; set; }

        public int? Copies { get; set; }

        public string Notes { get; set; }

        public ProjectEvalStatus EvalStatus { get; set; }

        public int? Duration { get; set; }

        public decimal? TotalBfpAmount { get; set; }

        public decimal? CoFinancingAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    public class ProjectMap : EntityTypeConfiguration<Project>
    {
        public ProjectMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectId);

            // Properties
            this.Property(t => t.ProjectId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.ProcedureId)
                .IsRequired();

            this.Property(t => t.ProjectTypeId)
                .IsRequired();

            this.Property(t => t.CompanyId)
                .IsRequired();

            this.Property(t => t.CompanyName)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.CompanyUin)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.CompanyUinType)
                .IsRequired();

            this.Property(t => t.CompanySizeTypeId)
                .IsRequired();

            this.Property(t => t.CompanyLegalTypeId)
                .IsRequired();

            this.Property(t => t.CompanyEmail)
                .HasMaxLength(200)
                .IsOptional();

            this.Property(t => t.CompanySeatPostCode)
                .HasMaxLength(50)
                .IsOptional();

            this.Property(t => t.CompanySeatStreet)
                .HasMaxLength(200)
                .IsOptional();

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.RegistrationStatus)
                .IsRequired();

            this.Property(t => t.RegNumber)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.RegDate)
                .IsRequired();

            this.Property(t => t.RecieveType)
                .IsRequired();

            this.Property(t => t.RecieveDate)
                .IsRequired();

            this.Property(t => t.SubmitDate)
                .IsRequired();

            this.Property(t => t.StoragePlace)
                .IsOptional();

            this.Property(t => t.Originals)
                .IsOptional();

            this.Property(t => t.Copies)
                .IsOptional();

            this.Property(t => t.Notes)
                .IsOptional();

            this.Property(t => t.EvalStatus)
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
            this.ToTable("Projects");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ProjectTypeId).HasColumnName("ProjectTypeId");
            this.Property(t => t.CompanyId).HasColumnName("CompanyId");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.CompanyUin).HasColumnName("CompanyUin");
            this.Property(t => t.CompanyUinType).HasColumnName("CompanyUinType");
            this.Property(t => t.CompanyKidCodeId).HasColumnName("CompanyKidCodeId");
            this.Property(t => t.CompanySizeTypeId).HasColumnName("CompanySizeTypeId");
            this.Property(t => t.CompanyLegalTypeId).HasColumnName("CompanyLegalTypeId");
            this.Property(t => t.CompanyEmail).HasColumnName("CompanyEmail");
            this.Property(t => t.CompanySeatCountryId).HasColumnName("CompanySeatCountryId");
            this.Property(t => t.CompanySeatSettlementId).HasColumnName("CompanySeatSettlementId");
            this.Property(t => t.CompanySeatPostCode).HasColumnName("CompanySeatPostCode");
            this.Property(t => t.CompanySeatStreet).HasColumnName("CompanySeatStreet");
            this.Property(t => t.CompanySeatAddress).HasColumnName("CompanySeatAddress");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.KidCodeId).HasColumnName("KidCodeId");
            this.Property(t => t.RegistrationStatus).HasColumnName("RegistrationStatus");
            this.Property(t => t.RegNumber).HasColumnName("RegNumber");
            this.Property(t => t.RegDate).HasColumnName("RegDate");
            this.Property(t => t.RecieveType).HasColumnName("RecieveType");
            this.Property(t => t.RecieveDate).HasColumnName("RecieveDate");
            this.Property(t => t.SubmitDate).HasColumnName("SubmitDate");
            this.Property(t => t.StoragePlace).HasColumnName("StoragePlace");
            this.Property(t => t.Originals).HasColumnName("Originals");
            this.Property(t => t.Copies).HasColumnName("Copies");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.EvalStatus).HasColumnName("EvalStatus");
            this.Property(t => t.Duration).HasColumnName("Duration");
            this.Property(t => t.TotalBfpAmount).HasColumnName("TotalBfpAmount");
            this.Property(t => t.CoFinancingAmount).HasColumnName("CoFinancingAmount");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
