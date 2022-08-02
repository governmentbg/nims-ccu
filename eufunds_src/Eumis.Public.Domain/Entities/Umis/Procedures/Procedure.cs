using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.Core;
using Eumis.Public.Common.Localization;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public partial class Procedure : IAggregateRoot, IEventEmitter
    {
        public static ProcedureStatus[] EvalSessionOrProjectCreationStatuses = new ProcedureStatus[]
        {
            ProcedureStatus.Active,
            ProcedureStatus.Ended,
        };
        
        public int ProcedureId { get; set; }

        public Guid Gid { get; set; }

        public int ProcedureTypeId { get; set; }

        public ProcedureStatus ProcedureStatus { get; set; }

        public ApplicationFormType ApplicationFormType { get; set; }

        public DateTime? ListingDate { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string Description { get; set; }

        public string DescriptionAlt { get; set; }

        public int? AttachedProcedureId { get; set; }

        public AllowedRegistrationType AllowedRegistrationType { get; set; }

        public decimal ProjectMinAmount { get; set; }

        public decimal ProjectMaxAmount { get; set; }

        public int ProjectDuration { get; set; }

        public string InternetAddress { get; set; }

        public bool IsIntegrated { get; set; }

        public DateTime? ActivationDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<ProcedureShare> ProcedureShares { get; set; }

        public virtual ICollection<ProcedureSpecificTarget> ProcedureSpecificTargets { get; set; }

        public virtual ICollection<ProcedureSpecField> ProcedureSpecFields { get; set; }

        public virtual ICollection<ProcedureIndicator> ProcedureIndicators { get; set; }

        public virtual ICollection<ProcedureInvestmentPriority> ProcedureInvestmentPriorities { get; set; }

        public virtual ICollection<ProcedureInterventionCategory> ProcedureInterventionCategories { get; set; }

        public virtual ICollection<ProcedureTimeLimit> ProcedureTimeLimits { get; set; }

        public virtual ICollection<ProcedureNumber> ProcedureNumbers { get; set; }

        public virtual ICollection<ProcedureApplicationGuideline> ProcedureApplicationGuidelines { get; set; }

        public virtual ICollection<ProcedureDocument> ProcedureDocuments { get; set; }

        public virtual ICollection<ProcedureApplicationDoc> ProcedureApplicationDocs { get; set; }

        public virtual ICollection<ProcedureProgramme> ProcedureProgrammes { get; set; }

        public virtual ICollection<ProcedureEvalTable> ProcedureEvalTables { get; set; }

        public virtual ICollection<ProcedureQuestion> ProcedureQuestions { get; set; }

        public virtual ICollection<ProcedureIndicativeAnnualWorkingProgramme> ProcedureIndicativeAnnualWorkingProgrammes { get; set; }

        public virtual ICollection<ProcedureLocation> ProcedureLocations { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }

        public string TransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.NameAlt ?? this.Name;
                }
                else
                {
                    return this.Name;
                }
            }
        }
    }

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

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Procedures");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.ProcedureTypeId).HasColumnName("ProcedureTypeId");
            this.Property(t => t.ProcedureStatus).HasColumnName("ProcedureStatus");
            this.Property(t => t.ApplicationFormType).HasColumnName("ApplicationFormType");
            this.Property(t => t.ListingDate).HasColumnName("ListingDate");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.DescriptionAlt).HasColumnName("DescriptionAlt");
            this.Property(t => t.AttachedProcedureId).HasColumnName("AttachedProcedureId");
            this.Property(t => t.AllowedRegistrationType).HasColumnName("AllowedRegistrationType");
            this.Property(t => t.ProjectMinAmount).HasColumnName("ProjectMinAmount");
            this.Property(t => t.ProjectMaxAmount).HasColumnName("ProjectMaxAmount");
            this.Property(t => t.ProjectDuration).HasColumnName("ProjectDuration");
            this.Property(t => t.InternetAddress).HasColumnName("InternetAddress");
            this.Property(t => t.IsIntegrated).HasColumnName("IsIntegrated");
            this.Property(t => t.ActivationDate).HasColumnName("ActivationDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
