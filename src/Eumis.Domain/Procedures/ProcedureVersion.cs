using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.Directions;
using Eumis.Domain.Procedures.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.Procedures
{
    public partial class ProcedureVersion : IAggregateRoot
    {
        private ProcedureVersionJson procedureVersionJson;

        public ProcedureVersion()
        {
        }

        public ProcedureVersion(
            int procedureVersionId,
            int procedureId,
            Guid procedureGid,
            string name,
            string nameAlt,
            string code,
            string description,
            string descriptionAlt,
            ApplicationFormType applicationFormType,
            ProcedureKind procedureKind,
            int? year,
            int? projectDuration,
            int? questionId,
            Guid? qaBlobKey,
            string qaFileName,
            DateTime? qaModifyDate,
            IList<ProcedureAppGuidlineJson> appGuidelines,
            IList<ProcedureAppDocJson> appDocs,
            IList<ProcedureSpecFieldJson> specFields,
            IList<ProcedureProgrammeJson> programmes,
            IList<ProcedureLocationJson> locations,
            IList<ProcedureApplicationSectionJson> sections,
            IList<DirectionPairJson> directions,
            IList<ProcedureDeclarationJson> declarations,
            bool? isActive = null)
        {
            this.ProcedureId = procedureId;
            this.ProcedureVersionId = procedureVersionId;
            this.ProcedureGid = procedureGid;

            this.ProcedureVersionJson = new ProcedureVersionJson(
                name,
                nameAlt,
                code,
                description,
                descriptionAlt,
                applicationFormType,
                procedureKind,
                year,
                projectDuration,
                questionId,
                qaBlobKey,
                qaFileName,
                qaModifyDate,
                appGuidelines,
                appDocs,
                specFields,
                programmes,
                locations,
                sections,
                directions,
                declarations);

            // the isActive param is passed when a new
            // ProcedureVersion creation is forced
            if (isActive.HasValue)
            {
                this.IsActive = isActive.Value;
            }
            else
            {
                this.IsActive = true;
            }

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ProcedureId { get; private set; }

        public int ProcedureVersionId { get; private set; }

        public Guid ProcedureGid { get; private set; }

        public string ProcedureText { get; private set; }

        public ProcedureVersionJson ProcedureVersionJson
        {
            get
            {
                if (this.procedureVersionJson == null)
                {
                    this.procedureVersionJson = JsonConvert.DeserializeObject<ProcedureVersionJson>(this.ProcedureText);
                }

                return this.procedureVersionJson;
            }

            private set
            {
                this.ProcedureText = JsonConvert.SerializeObject(value, Formatting.None);
                this.procedureVersionJson = JsonConvert.DeserializeObject<ProcedureVersionJson>(this.ProcedureText);
            }
        }

        public bool IsActive { get; private set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureVersionMap : EntityTypeConfiguration<ProcedureVersion>
    {
        public ProcedureVersionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProcedureId, t.ProcedureVersionId });

            // Properties
            this.Property(t => t.ProcedureGid)
                .IsRequired();

            this.Property(t => t.ProcedureText)
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
            this.ToTable("ProcedureVersions");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ProcedureVersionId).HasColumnName("ProcedureVersionId");
            this.Property(t => t.ProcedureGid).HasColumnName("ProcedureGid");
            this.Property(t => t.ProcedureText).HasColumnName("ProcedureText");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            // Local properties
            this.Ignore(t => t.ProcedureVersionJson);
        }
    }
}
