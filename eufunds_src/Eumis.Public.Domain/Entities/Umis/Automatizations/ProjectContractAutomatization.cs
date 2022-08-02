using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Automatizations
{
    public partial class ProjectContractAutomatization : IAggregateRoot
    {
        private ProjectContractAutomatization()
        {
        }

        public ProjectContractAutomatization(int projectId, int procedureId)
        {
            this.ProjectId = projectId;
            this.ProcedureId = procedureId;
            this.Status = ProjectContractAutomatizationStatus.ProjectRegistered;
            this.HasError = false;

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ProjectId { get; set; }

        public int ProcedureId { get; set; }

        public ProjectContractAutomatizationStatus Status { get; set; }

        public bool HasError { get; set; }

        public string ErrorText { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    public class ProjectContractAutomatizationMap : EntityTypeConfiguration<ProjectContractAutomatization>
    {
        public ProjectContractAutomatizationMap()
            : base()
        {
            // Primary Key
            this.HasKey(t => t.ProjectId);

            // Properties
            this.Property(t => t.ProjectId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(t => t.ProjectId)
                .IsRequired();
            this.Property(t => t.ProcedureId)
                .IsRequired();
            this.Property(t => t.Status)
                .IsRequired();
            this.Property(t => t.HasError)
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
            this.ToTable("ProjectContractAutomatizations");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.HasError).HasColumnName("HasError");
            this.Property(t => t.ErrorText).HasColumnName("ErrorText");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
