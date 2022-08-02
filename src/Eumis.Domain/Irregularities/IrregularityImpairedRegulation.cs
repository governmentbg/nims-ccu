using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Irregularities
{
    public class IrregularityImpairedRegulation
    {
        public IrregularityImpairedRegulationAct? ImpairedRegulationAct { get; set; }

        public string ImpairedRegulationNum { get; set; }

        public int? ImpairedRegulationYear { get; set; }

        public string ImpairedRegulation { get; set; }

        public string ImpairedNationalRegulation { get; set; }

        public void SetAttributes(
            IrregularityImpairedRegulationAct? impairedRegulationAct,
            string impairedRegulationNum,
            int? impairedRegulationYear,
            string impairedRegulation,
            string impairedNationalRegulation)
        {
            this.ImpairedRegulationAct = impairedRegulationAct;
            this.ImpairedRegulationNum = impairedRegulationNum;
            this.ImpairedRegulationYear = impairedRegulationYear;
            this.ImpairedRegulation = impairedRegulation;
            this.ImpairedNationalRegulation = impairedNationalRegulation;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class IrregularityImpairedRegulationMap : ComplexTypeConfiguration<IrregularityImpairedRegulation>
    {
        public IrregularityImpairedRegulationMap()
        {
            // Properties
            this.Property(t => t.ImpairedRegulationNum)
                .HasMaxLength(50)
                .IsOptional();
            this.Property(t => t.ImpairedRegulation)
                .HasMaxLength(100)
                .IsOptional();
        }
    }
}
