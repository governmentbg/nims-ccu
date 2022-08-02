using System.Collections.Generic;
using System.Linq;
using Eumis.Public.Resources;

namespace Eumis.Public.Data.UmisVOs
{
    public class OperationalProgramsVO
    {
        private readonly string[] cfErdfEsfGroupCodes =
            new string[]
            {
                "2014BG05SFOP001",
                "2014BG16M1OP001",
                "2014BG16RFOP001",
                "2014BG05M9OP001",
                "2014BG16RFOP002",
                "2014BG16M1OP002",
                "2014BG05M2OP001",
                "2015BG16RFSM001",
            };

        private readonly string[] esifGroupCodes =
            new string[]
            {
                "2014BG14MFOP001",
                "2014BG06RDNP001",
            };

        private readonly string[] moiGroupCodes =
            new string[]
            {
                "2014BG65AMNP001",
                "2014BG65ISNP001",
            };

        public OperationalProgramsVO(IList<ProgrammeBudgetDetailedVO> operationalPrograms)
        {
            var cfErdfEsfPrograms = operationalPrograms.Where(op => this.cfErdfEsfGroupCodes.Contains(op.Code)).ToList();
            var cfErdfEsfGroup = new OperationalProgramGroupVO(Texts.Global_CF_ERDF_ESF, cfErdfEsfPrograms);

            var esifPrograms = operationalPrograms.Where(op => this.esifGroupCodes.Contains(op.Code)).ToList();
            var esifGroup = new OperationalProgramGroupVO(Texts.Global_ESIF);
            esifGroup.OperationalPrograms = esifPrograms;

            // The ESIF group contains CF, ERD and ESF
            esifGroup.Totals = new OperationalProgramTotalsVO(cfErdfEsfPrograms.Union(esifPrograms).ToList());

            var moiPrograms = operationalPrograms.Where(op => this.moiGroupCodes.Contains(op.Code)).ToList();
            var moiGroup = new OperationalProgramGroupVO(Texts.Global_MoI_Funds, moiPrograms);

            var allGroupCodes = this.cfErdfEsfGroupCodes.Union(this.esifGroupCodes).Union(this.moiGroupCodes).ToList();
            var otherOperationalPrograms =
                operationalPrograms.Where(op => !allGroupCodes.Contains(op.Code)).ToList();

            this.OperationalProgramGroups = new List<OperationalProgramGroupVO>()
                { cfErdfEsfGroup, esifGroup, moiGroup };
            this.OtherOperationalPrograms = otherOperationalPrograms;
            this.GrandTotals = new OperationalProgramTotalsVO(operationalPrograms);
        }

        public List<OperationalProgramGroupVO> OperationalProgramGroups { get; set; }

        public List<ProgrammeBudgetDetailedVO> OtherOperationalPrograms { get; set; }

        public OperationalProgramTotalsVO GrandTotals { get; set; }
    }
}