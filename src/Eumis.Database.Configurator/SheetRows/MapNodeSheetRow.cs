using Eumis.Database.Configurator.SheetRows.Abstract;
using System.Collections.Generic;

namespace Eumis.Database.Configurator.SheetRows
{
    internal class MapNodeSheetRow : SheetRow
    {
        public MapNodeSheetRow(Dictionary<int, string> row)
        {
            this.ProgrammeId = this.GetIntCellValue(0, row);
            this.ProgrammeCode = this.GetStringCellValue(1, row);
            this.ProgrammeName = this.GetStringCellValue(2, row);
            this.ProgrammeShortName = this.GetStringCellValue(3, row);

            this.ProgrammePriorityId = this.GetIntCellValue(4, row);
            this.ProgrammePriorityCode = this.GetStringCellValue(5, row);
            this.ProgrammePriorityName = this.GetStringCellValue(6, row);
            this.ProgrammePriorityShortName = this.GetStringCellValue(7, row);

            this.ProgrammePriorityInvestmentPriorityId = this.GetIntCellValue(8, row);
            this.InvestmentPriorityId = this.GetIntCellValue(9, row);
            this.InvestmentPriorityCode = this.GetStringCellValue(10, row);
            this.InvestmentPriorityName = this.GetStringCellValue(11, row);
            this.InvestmentPriorityShortName = this.GetStringCellValue(12, row);

            this.SpecificTargetId = this.GetIntCellValue(13, row);
            this.SpecificTargetCode = this.GetStringCellValue(14, row);
            this.SpecificTargetName = this.GetStringCellValue(15, row);
            this.SpecificTargetShortName = this.GetStringCellValue(16, row);

            this.ProgrammeNameAlt = this.GetStringCellValue(17, row);
            this.ProgrammePriorityNameAlt = this.GetStringCellValue(18, row);
            this.InvestmentPriorityNameAlt = this.GetStringCellValue(19, row);
            this.SpecificTargetNameAlt = this.GetStringCellValue(20, row);

            this.PortalOrderNum = this.GetIntCellValue(21, row);
            this.PortalName = this.GetStringCellValue(22, row);
            this.PortalNameAlt = this.GetStringCellValue(23, row);
            this.PortalShortNameAlt = this.GetStringCellValue(24, row);

            this.IrregularityCode = this.GetStringCellValue(25, row);
        }

        public int? ProgrammeId { get; private set; }

        public string ProgrammeCode { get; private set; }

        public string ProgrammeName { get; private set; }

        public string ProgrammeShortName { get; private set; }

        public int? ProgrammePriorityId { get; private set; }

        public string ProgrammePriorityCode { get; private set; }

        public string ProgrammePriorityName { get; private set; }

        public string ProgrammePriorityShortName { get; private set; }

        public int? ProgrammePriorityInvestmentPriorityId { get; private set; }

        public int? InvestmentPriorityId { get; private set; }

        public string InvestmentPriorityCode { get; private set; }

        public string InvestmentPriorityName { get; private set; }

        public string InvestmentPriorityShortName { get; private set; }

        public int? SpecificTargetId { get; private set; }

        public string SpecificTargetCode { get; private set; }

        public string SpecificTargetName { get; private set; }

        public string SpecificTargetShortName { get; private set; }

        public string ProgrammeNameAlt { get; private set; }

        public string ProgrammePriorityNameAlt { get; private set; }

        public string InvestmentPriorityNameAlt { get; private set; }

        public string SpecificTargetNameAlt { get; private set; }

        public int? PortalOrderNum { get; private set; }

        public string PortalName { get; private set; }

        public string PortalNameAlt { get; private set; }

        public string PortalShortNameAlt { get; private set; }

        public string IrregularityCode { get; private set; }
    }
}
