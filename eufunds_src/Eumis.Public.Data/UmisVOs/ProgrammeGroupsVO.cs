using Eumis.Public.Domain.Entities.Umis.OperationalMap.ProgrammeGroups;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Public.Data.UmisVOs
{
    public class ProgrammeGroupsVO
    {
        public ProgrammeGroupsVO(IList<ProgrammeBudgetDetailedVO> programmeBudgets, IList<ProgrammeGroup> programmeGroups)
        {
            this.ProgrammeGroups = new List<OperationalProgramGroupVO>();

            foreach (var programmeGroup in programmeGroups)
            {
                var programmes = programmeBudgets.Where(b => b.ProgrammeGroupId == programmeGroup.ProgrammeGroupId).ToList();

                var group = new OperationalProgramGroupVO(programmeGroup.TransShortName, programmes);

                this.ProgrammeGroups.Add(group);
            }

            this.OtherProgrammes = programmeBudgets.Where(b => b.ProgrammeGroupId == null).ToList();

            this.GrandTotals = new OperationalProgramTotalsVO(programmeBudgets);
        }

        public IList<OperationalProgramGroupVO> ProgrammeGroups { get; set; }

        public IList<ProgrammeBudgetDetailedVO> OtherProgrammes { get; set; }

        public OperationalProgramTotalsVO GrandTotals { get; set; }
    }
}
