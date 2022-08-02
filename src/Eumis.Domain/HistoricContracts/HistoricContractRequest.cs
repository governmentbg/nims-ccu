using Eumis.Common.Db;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.HistoricContracts
{
    public class HistoricContractRequest : IAggregateRoot
    {
        private static Sequence historicContractRequestSequence = new Sequence("HistoricContractRequestSequence", "DbContext");

        private HistoricContractRequest()
        {
        }

        public HistoricContractRequest(
            DateTime startDate,
            DateTime endDate,
            string statusCode,
            string errorMessage,
            int countContracts,
            string json)
        {
            this.HistoricContractRequestId = historicContractRequestSequence.NextIntValue();
            this.EndDate = endDate;
            this.StatusCode = statusCode;
            this.ErrorMessage = errorMessage;
            this.CountContracts = countContracts;
            this.Json = json;
            this.CreateDate = startDate;
            this.ModifyDate = startDate;
        }

        public int HistoricContractRequestId { get; set; }

        public DateTime EndDate { get; set; }

        public string StatusCode { get; set; }

        public string ErrorMessage { get; set; }

        public int CountContracts { get; set; }

        public string Json { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class HistoricContractRequestMap : EntityTypeConfiguration<HistoricContractRequest>
    {
        public HistoricContractRequestMap()
        {
            // Primary Key
            this.HasKey(t => t.HistoricContractRequestId);

            // Properties
            this.Property(t => t.HistoricContractRequestId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EndDate)
                .IsRequired();

            this.Property(t => t.StatusCode)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.ErrorMessage)
                .IsOptional();

            this.Property(t => t.CountContracts)
                .IsOptional();

            this.Property(t => t.Json)
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
            this.ToTable("HistoricContractRequests");
            this.Property(t => t.HistoricContractRequestId).HasColumnName("HistoricContractRequestId");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.StatusCode).HasColumnName("StatusCode");
            this.Property(t => t.ErrorMessage).HasColumnName("ErrorMessage");
            this.Property(t => t.CountContracts).HasColumnName("CountContracts");
            this.Property(t => t.Json).HasColumnName("Json");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
