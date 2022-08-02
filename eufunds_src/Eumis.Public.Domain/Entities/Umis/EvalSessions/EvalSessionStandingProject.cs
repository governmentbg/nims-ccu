using Eumis.Public.Domain.Entities.Umis.Projects;
using Newtonsoft.Json;
using System;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Common.Json;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public class EvalSessionStandingProjectDO
    {
        public EvalSessionStandingProjectDO()
        {
        }

        public int EvalSessionId { get; set; }

        public int ProjectId { get; set; }

        public string ProcedureName { get; set; }

        public string ProjectRegNumber { get; set; }

        public string ProjectName { get; set; }

        public string CompanyName { get; set; }

        public DateTime? ProjectRegDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectRegistrationStatus? ProjectRegistrationStatus { get; set; }

        public bool? IsPassedASD { get; set; }

        public decimal? PointsASD { get; set; }

        public bool? IsPassedTFO { get; set; }

        public decimal? PointsTFO { get; set; }

        public bool? IsPassedComplex { get; set; }

        public decimal? PointsComplex { get; set; }

        public bool IsProjectDeleted { get; set; }

        public int? OrderNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionProjectStandingStatus? Status { get; set; }

        public EvalSessionProjectStandingStatus? StatusName { get; set; }

        public decimal? GrandAmount { get; set; }

        public bool IsStandingDeleted { get; set; }

    }

    public class EvalSessionStandingProject
    {
        public EvalSessionStandingProject()
        {
        }

        public int EvalSessionId { get; set; }

        public int EvalSessionStandingId { get; set; }

        public int ProjectId { get; set; }

        public virtual EvalSessionStanding EvalSessionStanding { get; set; }
    }

    public class EvalSessionStandingProjectMap : EntityTypeConfiguration<EvalSessionStandingProject>
    {
        public EvalSessionStandingProjectMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EvalSessionId, t.EvalSessionStandingId, t.ProjectId });

            // Table & Column Mappings
            this.ToTable("EvalSessionStandingProjects");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.EvalSessionStandingId).HasColumnName("EvalSessionStandingId");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");

            //Relationships
            this.HasRequired(t => t.EvalSessionStanding)
                .WithMany(t => t.EvalSessionStandingProjects)
                .HasForeignKey(t => new { t.EvalSessionId, t.EvalSessionStandingId });
        }
    }
}
