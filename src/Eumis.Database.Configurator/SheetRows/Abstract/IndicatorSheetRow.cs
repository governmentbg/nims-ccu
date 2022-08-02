namespace Eumis.Database.Configurator.SheetRows.Abstract
{
    internal abstract class IndicatorSheetRow : SheetRow
    {
        internal enum Type
        {
            Table3,
            Table4,
            Table4a,
            Table5,
            Table6,
            Table12,
            Table13,
        }

        public Type SheetType { get; protected set; }

        public int? ProgrammeId { get; protected set; }

        public int? MapNodeId { get; protected set; }

        // Indicator fields
        public string Code { get; protected set; }

        public string Name { get; protected set; }

        public string NameAlt { get; protected set; }

        public string Measure { get; protected set; }

        public string Kind { get; protected set; }

        public string IsAggregated { get; protected set; }

        public string ReportingType { get; protected set; }

        // MapNodeIndicator fields
        public string RegionCategory { get; protected set; }

        public string FinanceSource { get; protected set; }

        public string BaseTotalValueString { get; protected set; }

        public decimal? BaseTotalValue { get; protected set; }

        public decimal? BaseMenValue { get; protected set; }

        public decimal? BaseWomenValue { get; protected set; }

        public int? BaseYear { get; protected set; }

        public string TargetTotalValueString { get; protected set; }

        public decimal? TargetTotalValue { get; protected set; }

        public decimal? TargetMenValue { get; protected set; }

        public decimal? TargetWomenValue { get; protected set; }

        public decimal? MilestoneTargetTotalValue { get; protected set; }

        public decimal? MilestoneTargetMenValue { get; protected set; }

        public decimal? MilestoneTargetWomenValue { get; protected set; }

        public decimal? FinalTargetTotalValue { get; protected set; }

        public decimal? FinalTargetMenValue { get; protected set; }

        public decimal? FinalTargetWomenValue { get; protected set; }

        public string BaseTargetValueMeasure { get; protected set; }

        public string DataSource { get; protected set; }

        public string ReportingFrequancy { get; protected set; }

        public string CommonBaseIndicator { get; protected set; }

        public string Description { get; protected set; }
    }
}
