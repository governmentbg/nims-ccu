using Eumis.Domain.Companies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.OperationalMap.ProgrammePriorities
{
    public class ProgrammePriorityCompany
    {
        [ForeignKey("ProgrammePriority")]
        public int ProgrammePriorityId { get; set; }

        public ProgrammePriorityType ProgrammePriorityType { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        public int? HigherOrderCompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ProgrammePriority ProgrammePriority { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProgrammePriorityCompanyMap : EntityTypeConfiguration<ProgrammePriorityCompany>
    {
        public ProgrammePriorityCompanyMap()
        {
            // Primary Key
            this.HasKey(t => t.ProgrammePriorityId);

            // Properties
            this.Property(t => t.ProgrammePriorityId)
                .IsRequired();

            this.Property(t => t.CompanyId)
                .IsRequired();

            this.Property(t => t.ProgrammePriorityType)
                .IsRequired();

            this.Property(t => t.HigherOrderCompanyId)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("ProgrammePriorityCompanies");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.CompanyId).HasColumnName("CompanyId");
            this.Property(t => t.HigherOrderCompanyId).HasColumnName("HigherOrderCompanyId");
        }
    }
}
