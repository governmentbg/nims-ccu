using System;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.Procedures.Json;
using Newtonsoft.Json;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public partial class ProcedureVersion : IAggregateRoot
    {
        private ProcedureVersionJson procedureVersionJson;

        public int ProcedureId { get; private set; }

        public int ProcedureVersionId { get; private set; }

        public Guid ProcedureGid { get; private set; }

        public string ProcedureText { get; private set; }

        public ProcedureVersionJson ProcedureVersionJson
        {
            get
            {
                if (procedureVersionJson == null)
                {
                    procedureVersionJson = JsonConvert.DeserializeObject<ProcedureVersionJson>(this.ProcedureText);
                }

                return procedureVersionJson;
            }
            private set
            {
                this.ProcedureText = JsonConvert.SerializeObject(value, Formatting.None);
                procedureVersionJson = JsonConvert.DeserializeObject<ProcedureVersionJson>(this.ProcedureText);
            }
        }

        public bool IsActive { get; private set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

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
