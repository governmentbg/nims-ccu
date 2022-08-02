using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public class EvalSessionResult
    {
        private EvalSessionResult()
        {
            this.Projects = new List<EvalSessionResultProject>();
        }

        public int EvalSessionResultId { get; set; }

        public int EvalSessionId { get; set; }

        public EvalSessionResultStatus Status { get; set; }

        public EvalSessionResultType Type { get; set; }

        public string StatusNote { get; set; }

        public int OrderNum { get; set; }

        public DateTime? PublicationDate { get; set; }

        public int? PublicationUserId { get; set; }

        public int ProcedureId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public virtual IList<EvalSessionResultProject> Projects { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class EvalSessionAdminAdmissResultMap : EntityTypeConfiguration<EvalSessionResult>
    {
        public EvalSessionAdminAdmissResultMap()
        {
            // Primary Key
            this.HasKey(t => t.EvalSessionResultId);

            // Properties
            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.OrderNum)
                .IsRequired();

            this.Property(t => t.ProcedureId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("EvalSessionResults");
            this.Property(t => t.EvalSessionResultId).HasColumnName("EvalSessionResultId");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.StatusNote).HasColumnName("StatusNote");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.PublicationDate).HasColumnName("PublicationDate");
            this.Property(t => t.PublicationUserId).HasColumnName("PublicationUserId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
        }
    }
}
