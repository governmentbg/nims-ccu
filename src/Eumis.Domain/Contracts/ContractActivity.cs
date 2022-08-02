using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.Contracts
{
    public class ContractActivity
    {
        private ContractActivity()
        {
            this.ContractActivityCompanies = new List<ContractActivityCompany>();
        }

        public ContractActivity(
            Guid gid,
            bool isActive,
            string code,
            string name,
            string executionMethod,
            string result,
            int startMonth,
            int duration,
            decimal amount,
            IList<Tuple<string, UinType, string>> companies)
            : this()
        {
            this.Gid = gid;
            this.IsActive = isActive;
            this.Code = code;
            this.Name = name;
            this.ExecutionMethod = executionMethod;
            this.Result = result;
            this.StartMonth = startMonth;
            this.Duration = duration;
            this.Amount = amount;
            foreach (var c in companies)
            {
                this.ContractActivityCompanies.Add(
                    new ContractActivityCompany(
                        c.Item1,
                        c.Item2,
                        c.Item3));
            }
        }

        public int ContractActivityId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public bool IsActive { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string ExecutionMethod { get; set; }

        public string Result { get; set; }

        public int StartMonth { get; set; }

        public int Duration { get; set; }

        public decimal Amount { get; set; }

        public virtual Contract Contract { get; set; }

        public virtual ICollection<ContractActivityCompany> ContractActivityCompanies { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractActivityMap : EntityTypeConfiguration<ContractActivity>
    {
        public ContractActivityMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractActivityId);

            // Properties
            this.Property(t => t.ContractActivityId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();
            this.Property(t => t.IsActive)
                .IsRequired();
            this.Property(t => t.Code)
                .IsRequired();
            this.Property(t => t.Name)
                .IsRequired();
            this.Property(t => t.ExecutionMethod)
                .IsRequired();
            this.Property(t => t.Result)
                .IsRequired();
            this.Property(t => t.StartMonth)
                .IsRequired();
            this.Property(t => t.Duration)
                .IsRequired();
            this.Property(t => t.Amount)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractActivities");
            this.Property(t => t.ContractActivityId).HasColumnName("ContractActivityId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ExecutionMethod).HasColumnName("ExecutionMethod");
            this.Property(t => t.Result).HasColumnName("Result");
            this.Property(t => t.StartMonth).HasColumnName("StartMonth");
            this.Property(t => t.Duration).HasColumnName("Duration");
            this.Property(t => t.Amount).HasColumnName("Amount");

            this.HasRequired(t => t.Contract)
                .WithMany(t => t.ContractActivities)
                .HasForeignKey(t => t.ContractId)
                .WillCascadeOnDelete();
        }
    }
}
