using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using Eumis.Common.Config;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures.ProcedureContractReportDocuments;

namespace Eumis.Domain.Procedures
{
    public partial class Procedure : IAggregateRoot, IEventEmitter, INotificationEventEmitter
    {
        public static readonly ProcedureStatus[] EvalSessionOrProjectCreationStatuses = new ProcedureStatus[]
        {
            ProcedureStatus.Active,
            ProcedureStatus.Ended,
        };

        public static readonly bool HideIndicators = "true".Equals(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Api:HideIndicators"), StringComparison.InvariantCultureIgnoreCase);

        private Procedure()
        {
            this.ProcedureShares = new List<ProcedureShare>();
            this.ProcedureSpecFields = new List<ProcedureSpecField>();
            this.ProcedureIndicators = new List<ProcedureIndicator>();
            this.ProcedureTimeLimits = new List<ProcedureTimeLimit>();
            this.ProcedureNumbers = new List<ProcedureNumber>();
            this.ProcedureApplicationGuidelines = new List<ProcedureApplicationGuideline>();
            this.ProcedureDocuments = new List<ProcedureDocument>();
            this.ProcedureApplicationDocs = new List<ProcedureApplicationDoc>();
            this.ProcedureProgrammes = new List<ProcedureProgramme>();
            this.ProcedureEvalTables = new List<ProcedureEvalTable>();
            this.ProcedureQuestions = new List<ProcedureQuestion>();
            this.ProcedureContractReportDocuments = new List<ProcedureContractReportDocument>();
            this.ProcedureLocations = new List<ProcedureLocation>();
            this.ProcedureApplicationSections = new List<ProcedureApplicationSection>();
            this.ProcedureDirections = new List<ProcedureDirection>();

            ((IEventEmitter)this).Events = new List<IDomainEvent>();
            ((INotificationEventEmitter)this).NotificationEvents = new List<INotificationEvent>();
        }

        public Procedure(
            int number,
            ProcedureStatus procedureStatus,
            ApplicationFormType applicationFormType,
            ProcedureKind procedureKind,
            int year,
            string code,
            string name,
            string nameAlt,
            string description,
            string descriptionAlt,
            AllowedRegistrationType? allowedRegistrationType,
            decimal? projectMinAmount,
            string projectMinAmountInfo,
            string projectMinAmountInfoAlt,
            decimal? projectMaxAmount,
            string projectMaxAmountInfo,
            string projectMaxAmountInfoAlt,
            int? projectDuration,
            int shareProgrammeId,
            int shareProgrammePriorityId,
            decimal shareBAmount,
            bool shareIsPrimary,
            DateTime timeLimitEndDate,
            string timeLimitNotes)
            : this()
        {
            this.Gid = Guid.NewGuid();
            this.ProcedureStatus = procedureStatus;
            this.Year = year;
            this.ApplicationFormType = applicationFormType;
            this.ProcedureContractReportDocumentsSectionStatus = ProcedureContractReportDocumentsSectionStatus.Draft;

            this.SetAttributes(
                procedureKind,
                code,
                name,
                nameAlt,
                description,
                descriptionAlt,
                allowedRegistrationType,
                projectMinAmount,
                projectMinAmountInfo,
                projectMinAmountInfoAlt,
                projectMaxAmount,
                projectMaxAmountInfo,
                projectMaxAmountInfoAlt,
                projectDuration);

            this.AddProcedureShare(
                shareProgrammeId,
                shareProgrammePriorityId,
                shareBAmount,
                shareIsPrimary);

            this.AddProcedureTimeLimit(
                timeLimitEndDate,
                timeLimitNotes);

            this.ProcedureNumbers.Add(new ProcedureNumber()
            {
                ProgrammePriorityId = shareProgrammePriorityId,
                Year = year,
                Number = number,
            });

            this.CreateDate = DateTime.Now;
        }

        public Procedure(
            int number,
            ProcedureKind procedureKind,
            ProcedureStatus procedureStatus,
            ApplicationFormType applicationFormType,
            int year,
            string code,
            string name,
            string nameAlt,
            string description,
            string descriptionAlt,
            AllowedRegistrationType? allowedRegistrationType,
            decimal? projectMinAmount,
            string projectMinAmountInfo,
            string projectMinAmountInfoAlt,
            decimal? projectMaxAmount,
            string projectMaxAmountInfo,
            string projectMaxAmountInfoAlt,
            int? projectDuration,
            int shareProgrammeId,
            int shareProgrammePriorityId,
            decimal shareBAmount,
            bool shareIsPrimary)
            : this()
        {
            this.Gid = Guid.NewGuid();
            this.ProcedureStatus = procedureStatus;
            this.Year = year;
            this.ApplicationFormType = applicationFormType;
            this.ProcedureContractReportDocumentsSectionStatus = ProcedureContractReportDocumentsSectionStatus.Draft;

            bool isCopy = true;

            this.SetAttributes(
                procedureKind,
                code,
                name,
                nameAlt,
                description,
                descriptionAlt,
                allowedRegistrationType,
                projectMinAmount,
                projectMinAmountInfo,
                projectMinAmountInfoAlt,
                projectMaxAmount,
                projectMaxAmountInfo,
                projectMaxAmountInfoAlt,
                projectDuration,
                isCopy);

            this.AddProcedureShare(
                shareProgrammeId,
                shareProgrammePriorityId,
                shareBAmount,
                shareIsPrimary);

            this.ProcedureNumbers.Add(new ProcedureNumber()
            {
                ProgrammePriorityId = shareProgrammePriorityId,
                Year = year,
                Number = number,
            });

            this.CreateDate = DateTime.Now;
        }

        public int ProcedureId { get; set; }

        public Guid Gid { get; set; }

        public ProcedureStatus ProcedureStatus { get; set; }

        public ProcedureKind ProcedureKind { get; set; }

        public ProcedureContractReportDocumentsSectionStatus ProcedureContractReportDocumentsSectionStatus { get; set; }

        public ApplicationFormType ApplicationFormType { get; set; }

        public int Year { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string Description { get; set; }

        public string DescriptionAlt { get; set; }

        public AllowedRegistrationType? AllowedRegistrationType { get; set; }

        public decimal? ProjectMinAmount { get; set; }

        public string ProjectMinAmountInfo { get; set; }

        public string ProjectMinAmountInfoAlt { get; set; }

        public decimal? ProjectMaxAmount { get; set; }

        public string ProjectMaxAmountInfo { get; set; }

        public string ProjectMaxAmountInfoAlt { get; set; }

        public int? ProjectDuration { get; set; }

        public DateTime? ActivationDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public virtual ProcedureApplicationSectionAdditionalSetting ProcedureApplicationSectionAdditionalSetting { get; set; }

        public virtual ICollection<ProcedureShare> ProcedureShares { get; set; }

        public virtual ICollection<ProcedureSpecField> ProcedureSpecFields { get; set; }

        public virtual ICollection<ProcedureIndicator> ProcedureIndicators { get; set; }

        public virtual ICollection<ProcedureTimeLimit> ProcedureTimeLimits { get; set; }

        public virtual ICollection<ProcedureNumber> ProcedureNumbers { get; set; }

        public virtual ICollection<ProcedureApplicationGuideline> ProcedureApplicationGuidelines { get; set; }

        public virtual ICollection<ProcedureDocument> ProcedureDocuments { get; set; }

        public virtual ICollection<ProcedureApplicationDoc> ProcedureApplicationDocs { get; set; }

        public virtual ICollection<ProcedureProgramme> ProcedureProgrammes { get; set; }

        public virtual ICollection<ProcedureEvalTable> ProcedureEvalTables { get; set; }

        public virtual ICollection<ProcedureQuestion> ProcedureQuestions { get; set; }

        public virtual ICollection<ProcedureContractReportDocument> ProcedureContractReportDocuments { get; set; }

        public virtual ICollection<ProcedureLocation> ProcedureLocations { get; set; }

        public virtual ICollection<ProcedureApplicationSection> ProcedureApplicationSections { get; set; }

        public virtual ICollection<ProcedureDirection> ProcedureDirections { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }

        ICollection<INotificationEvent> INotificationEventEmitter.NotificationEvents { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureMap : EntityTypeConfiguration<Procedure>
    {
        public ProcedureMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureId);

            // Properties
            this.Property(t => t.ProcedureId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.Code)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.ProjectMinAmountInfo)
                .HasMaxLength(500)
                .IsOptional();

            this.Property(t => t.ProjectMinAmountInfoAlt)
                .HasMaxLength(500)
                .IsOptional();

            this.Property(t => t.ProjectMaxAmountInfo)
                .HasMaxLength(500)
                .IsOptional();

            this.Property(t => t.ProjectMaxAmountInfoAlt)
                .HasMaxLength(500)
                .IsOptional();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            this.Property(t => t.ProcedureKind)
                .IsRequired();

            this.Property(t => t.Year)
               .IsRequired();

            // Table & Column Mappings
            this.ToTable("Procedures");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.ProcedureStatus).HasColumnName("ProcedureStatus");
            this.Property(t => t.ProcedureKind).HasColumnName("ProcedureKind");
            this.Property(t => t.ProcedureContractReportDocumentsSectionStatus).HasColumnName("ProcedureContractReportDocumentsSectionStatus");
            this.Property(t => t.ApplicationFormType).HasColumnName("ApplicationFormType");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.DescriptionAlt).HasColumnName("DescriptionAlt");
            this.Property(t => t.AllowedRegistrationType).HasColumnName("AllowedRegistrationType");
            this.Property(t => t.ProjectMinAmount).HasColumnName("ProjectMinAmount");
            this.Property(t => t.ProjectMinAmountInfo).HasColumnName("ProjectMinAmountInfo");
            this.Property(t => t.ProjectMinAmountInfoAlt).HasColumnName("ProjectMinAmountInfoAlt");
            this.Property(t => t.ProjectMaxAmount).HasColumnName("ProjectMaxAmount");
            this.Property(t => t.ProjectMaxAmountInfo).HasColumnName("ProjectMaxAmountInfo");
            this.Property(t => t.ProjectMaxAmountInfoAlt).HasColumnName("ProjectMaxAmountInfoAlt");
            this.Property(t => t.ProjectDuration).HasColumnName("ProjectDuration");
            this.Property(t => t.ActivationDate).HasColumnName("ActivationDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
