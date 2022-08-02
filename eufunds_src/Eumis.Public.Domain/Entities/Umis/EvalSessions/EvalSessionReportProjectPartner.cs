using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public class EvalSessionReportProjectPartner
    {
        public int EvalSessionReportProjectPartnerId { get; set; }

        public int EvalSessionId { get; set; }

        public int EvalSessionReportId { get; set; }

        public int ProjectId { get; set; }

        public string PartnerUin { get; set; }

        public string PartnerName { get; set; }

        public int PartnerLegalTypeId { get; set; }

        public string PartnerAddress { get; set; }

        public string PartnerRepresentative { get; set; }

        public decimal? PartnerFinancialContribution { get; set; }

        public virtual EvalSessionReportProject Project { get; set; }

        public virtual CompanyLegalType PartnerLegalType { get; set; }
    }

    public class EvalSessionReportProjectPartnerMap : EntityTypeConfiguration<EvalSessionReportProjectPartner>
    {
        public EvalSessionReportProjectPartnerMap()
        {
            // Primary Key
            this.HasKey(t => t.EvalSessionReportProjectPartnerId);

            // Properties
            this.Property(t => t.EvalSessionReportProjectPartnerId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.EvalSessionId)
                .IsRequired();
            this.Property(t => t.EvalSessionReportId)
                .IsRequired();
            this.Property(t => t.ProjectId)
                .IsRequired();
            this.Property(t => t.PartnerUin)
                .HasMaxLength(200)
                .IsRequired();
            this.Property(t => t.PartnerName)
                .HasMaxLength(200)
                .IsRequired();
            this.Property(t => t.PartnerLegalTypeId)
                .IsRequired();
            this.Property(t => t.PartnerAddress)
                .IsRequired();
            this.Property(t => t.PartnerRepresentative)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("EvalSessionReportProjectPartners");
            this.Property(t => t.EvalSessionReportProjectPartnerId).HasColumnName("EvalSessionReportProjectPartnerId");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.EvalSessionReportId).HasColumnName("EvalSessionReportId");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");

            this.Property(t => t.PartnerUin).HasColumnName("PartnerUin");
            this.Property(t => t.PartnerName).HasColumnName("PartnerName");
            this.Property(t => t.PartnerLegalTypeId).HasColumnName("PartnerLegalTypeId");
            this.Property(t => t.PartnerAddress).HasColumnName("PartnerAddress");
            this.Property(t => t.PartnerRepresentative).HasColumnName("PartnerRepresentative");
            this.Property(t => t.PartnerFinancialContribution).HasColumnName("PartnerFinancialContribution");

            //Relationships
            this.HasRequired(t => t.Project)
                .WithMany(t => t.Partners)
                .HasForeignKey(t => new { t.EvalSessionId, t.EvalSessionReportId, t.ProjectId });
            this.HasRequired(t => t.PartnerLegalType)
                .WithMany()
                .HasForeignKey(t => t.PartnerLegalTypeId);
        }
    }
}
