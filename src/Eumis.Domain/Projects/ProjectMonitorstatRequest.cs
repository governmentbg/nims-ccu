using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Projects
{
    public partial class ProjectMonitorstatRequest
    {
        public ProjectMonitorstatRequest()
        {
            this.MonitorstatResponses = new List<ProjectMonitorstatResponse>();
        }

        public ProjectMonitorstatRequest(
            int procedureMonitorstatRequestId,
            int projectVersionXmlId,
            int? projectVersionXmlFileId,
            Guid? declarationBlobKey,
            int? programmeDeclarationId,
            string uin,
            UinType uinType)
            : this()
        {
            if (projectVersionXmlFileId == null && declarationBlobKey == null)
            {
                throw new DomainValidationException($"One of the {nameof(projectVersionXmlFileId)} or {nameof(declarationBlobKey)} must not be null");
            }

            this.Status = ProjectMonitorstatRequestStatus.Draft;
            this.ProcedureMonitorstatRequestId = procedureMonitorstatRequestId;
            this.ProjectVersionXmlId = projectVersionXmlId;
            this.ProjectVersionXmlFileId = projectVersionXmlFileId;
            this.DeclarationBlobKey = declarationBlobKey;
            this.ProgrammeDeclarationId = programmeDeclarationId;
            this.CompanyUin = uin;
            this.CompanyUinType = uinType;
            this.ModifyDate = DateTime.Now;
        }

        public int ProjectMonitorstatRequestId { get; set; }

        public int ProjectId { get; set; }

        public int ProcedureMonitorstatRequestId { get; set; }

        public int ProjectVersionXmlId { get; set; }

        public string CompanyUin { get; set; }

        public UinType? CompanyUinType { get; set; }

        public ProjectMonitorstatRequestStatus Status { get; set; }

        public int? ProjectVersionXmlFileId { get; set; }

        public Guid? DeclarationBlobKey { get; set; }

        public int? ProgrammeDeclarationId { get; set; }

        public DateTime ModifyDate { get; set; }

        public int? UserId { get; set; }

        public Guid? ForeignGid { get; set; }

        public virtual Project Project { get; set; }

        public virtual Blob DeclarationFile { get; set; }

        public ICollection<ProjectMonitorstatResponse> MonitorstatResponses { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProjectMonitorstatRequestMap : EntityTypeConfiguration<ProjectMonitorstatRequest>
    {
        public ProjectMonitorstatRequestMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectMonitorstatRequestId);

            // Properties
            this.Property(t => t.ProjectMonitorstatRequestId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProjectId)
                .IsRequired();

            this.Property(t => t.ProcedureMonitorstatRequestId)
                .IsRequired();

            this.Property(t => t.ProjectVersionXmlId)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            this.Property(t => t.ProjectVersionXmlFileId)
                .IsOptional();

            this.Property(t => t.DeclarationBlobKey)
                .IsOptional();

            this.Property(t => t.ProgrammeDeclarationId)
                .IsOptional();

            this.Property(t => t.UserId)
               .IsOptional();

            this.Property(t => t.ForeignGid)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("ProjectMonitorstatRequests");
            this.Property(t => t.ProjectMonitorstatRequestId).HasColumnName("ProjectMonitorstatRequestId");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.ProcedureMonitorstatRequestId).HasColumnName("ProcedureMonitorstatRequestId");
            this.Property(t => t.ProjectVersionXmlId).HasColumnName("ProjectVersionXmlId");
            this.Property(t => t.CompanyUin).HasColumnName("CompanyUin");
            this.Property(t => t.CompanyUinType).HasColumnName("CompanyUinType");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ForeignGid).HasColumnName("ForeignGid");
            this.Property(t => t.ProjectVersionXmlFileId).HasColumnName("ProjectVersionXmlFileId");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.DeclarationBlobKey).HasColumnName("DeclarationBlobKey");
            this.Property(t => t.ProgrammeDeclarationId).HasColumnName("ProgrammeDeclarationId");

            this.HasRequired(t => t.Project)
                .WithMany(t => t.MonitorstatRequests)
                .HasForeignKey(t => t.ProjectId)
                .WillCascadeOnDelete();

            this.HasOptional(t => t.DeclarationFile)
                .WithMany()
                .HasForeignKey(d => d.DeclarationBlobKey);
        }
    }
}
