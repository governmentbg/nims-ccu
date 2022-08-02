using Eumis.Common.Db;
using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportFinancialCSD : IAggregateRoot
    {
        private static Sequence contractReportFinancialCSDSequence = new Sequence("ContractReportFinancialCSDSequence", "DbContext");

        private ContractReportFinancialCSD()
        {
            this.Files = new List<ContractReportFinancialCSDFile>();
        }

        public ContractReportFinancialCSD(
            int contractReportFinancialId,
            int contractReportId,
            int contractId,
            CostSupportingDocumentType type,
            string description,
            string number,
            DateTime date,
            DateTime paymentDate,
            CostSupportingDocumentCompanyType companyType,
            Guid companyGid,
            string companyUin,
            UinType companyUinType,
            string companyName,
            Guid? contractContractorGid,
            string contractContractorName)
        {
            this.ContractReportFinancialCSDId = contractReportFinancialCSDSequence.NextIntValue();

            var currentDate = DateTime.Now;

            this.Gid = Guid.NewGuid();
            this.ContractReportFinancialId = contractReportFinancialId;
            this.ContractReportId = contractReportId;
            this.ContractId = contractId;

            this.Type = type;
            this.Description = description;
            this.Number = number;
            this.Date = date;
            this.PaymentDate = paymentDate;

            this.CompanyType = companyType;
            this.CompanyGid = companyGid;
            this.CompanyUin = companyUin;
            this.CompanyUinType = companyUinType;
            this.CompanyName = companyName;
            this.ContractContractorGid = contractContractorGid;
            this.ContractContractorName = contractContractorName;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ContractReportFinancialCSDId { get; set; }

        public int ContractReportFinancialId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public CostSupportingDocumentType Type { get; set; }

        public string Description { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public DateTime PaymentDate { get; set; }

        public CostSupportingDocumentCompanyType CompanyType { get; set; }

        public Guid CompanyGid { get; set; }

        public string CompanyUin { get; set; }

        public UinType CompanyUinType { get; set; }

        public string CompanyName { get; set; }

        // this is the ContractContract Gid and Name when the CompanyType is Contractor: TODO - rename
        public Guid? ContractContractorGid { get; set; }

        public string ContractContractorName { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<ContractReportFinancialCSDFile> Files { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportFinancialCSDMap : EntityTypeConfiguration<ContractReportFinancialCSD>
    {
        public ContractReportFinancialCSDMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportFinancialCSDId);

            // Properties
            this.Property(t => t.ContractReportFinancialId)
                .IsRequired();

            this.Property(t => t.ContractReportId)
                .IsRequired();

            this.Property(t => t.ContractId)
                .IsRequired();

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.Type)
                .IsRequired();

            this.Property(t => t.Number)
                .IsRequired();

            this.Property(t => t.Date)
                .IsRequired();

            this.Property(t => t.PaymentDate)
                .IsRequired();

            this.Property(t => t.CompanyType)
                .IsRequired();

            this.Property(t => t.CompanyGid)
                .IsRequired();

            this.Property(t => t.CompanyName)
                .IsRequired();

            this.Property(t => t.CompanyUin)
                .IsRequired();

            this.Property(t => t.CompanyUinType)
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
            this.ToTable("ContractReportFinancialCSDs");
            this.Property(t => t.ContractReportFinancialCSDId).HasColumnName("ContractReportFinancialCSDId");
            this.Property(t => t.ContractReportFinancialId).HasColumnName("ContractReportFinancialId");
            this.Property(t => t.ContractReportId).HasColumnName("ContractReportId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Gid).HasColumnName("Gid");

            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Number).HasColumnName("Number");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.PaymentDate).HasColumnName("PaymentDate");

            this.Property(t => t.CompanyType).HasColumnName("CompanyType");
            this.Property(t => t.CompanyGid).HasColumnName("CompanyGid");
            this.Property(t => t.CompanyUin).HasColumnName("CompanyUin");
            this.Property(t => t.CompanyUinType).HasColumnName("CompanyUinType");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.ContractContractorGid).HasColumnName("ContractContractorGid");
            this.Property(t => t.ContractContractorName).HasColumnName("ContractContractorName");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
