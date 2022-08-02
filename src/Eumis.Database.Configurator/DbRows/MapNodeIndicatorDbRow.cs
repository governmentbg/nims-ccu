using Eumis.Database.Configurator.Helpers;

namespace Eumis.Database.Configurator.DbRows
{
    internal class MapNodeIndicatorDbRow : IDbRow
    {
        public const string DbTableName = "MapNodeIndicators";
        public const bool UseIdentityInsert = false;

        public int MapNodeId { get; set; }

        public int IndicatorId { get; set; }

        public string Type { get; set; }

        public int? RegionCategory { get; set; }

        public int? FinanceSource { get; set; }

        public decimal? BaseTotalValue { get; set; }

        public decimal? BaseMenValue { get; set; }

        public decimal? BaseWomenValue { get; set; }

        public string BaseQualitativeValue { get; set; }

        public int? BaseYear { get; set; }

        public decimal? TargetTotalValue { get; set; }

        public decimal? TargetMenValue { get; set; }

        public decimal? TargetWomenValue { get; set; }

        public string TargetQualitativeValue { get; set; }

        public decimal? MilestoneTargetTotalValue { get; set; }

        public decimal? MilestoneTargetMenValue { get; set; }

        public decimal? MilestoneTargetWomenValue { get; set; }

        public decimal? FinalTargetTotalValue { get; set; }

        public decimal? FinalTargetMenValue { get; set; }

        public decimal? FinalTargetWomenValue { get; set; }

        public int? MeasureId { get; set; }

        public string DataSource { get; set; }

        public string ReportingFrequancy { get; set; }

        public string CommonBaseIndicator { get; set; }

        public string Description { get; set; }

        public string CreateRowInsert()
        {
            return string.Format(
                "INSERT INTO [{0}] ([MapNodeId], [IndicatorId], [Type], [RegionCategory], [FinanceSource], [BaseTotalValue], [BaseMenValue], [BaseWomenValue], [BaseQualitativeValue], [BaseYear], [TargetTotalValue], [TargetMenValue], [TargetWomenValue], [TargetQualitativeValue], [MilestoneTargetTotalValue], [MilestoneTargetMenValue], [MilestoneTargetWomenValue], [FinalTargetTotalValue], [FinalTargetMenValue], [FinalTargetWomenValue], [MeasureId], [DataSource], [ReportingFrequancy], [CommonBaseIndicator], [Description]) VALUES ({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25})",
                DbTableName,
                SqlScriptHelper.ToString(this.MapNodeId),
                SqlScriptHelper.ToString(this.IndicatorId),
                SqlScriptHelper.ToString(this.Type),
                SqlScriptHelper.ToString(this.RegionCategory),
                SqlScriptHelper.ToString(this.FinanceSource),
                SqlScriptHelper.ToString(this.BaseTotalValue),
                SqlScriptHelper.ToString(this.BaseMenValue),
                SqlScriptHelper.ToString(this.BaseWomenValue),
                SqlScriptHelper.ToString(this.BaseQualitativeValue),
                SqlScriptHelper.ToString(this.BaseYear),
                SqlScriptHelper.ToString(this.TargetTotalValue),
                SqlScriptHelper.ToString(this.TargetMenValue),
                SqlScriptHelper.ToString(this.TargetWomenValue),
                SqlScriptHelper.ToString(this.TargetQualitativeValue),
                SqlScriptHelper.ToString(this.MilestoneTargetTotalValue),
                SqlScriptHelper.ToString(this.MilestoneTargetMenValue),
                SqlScriptHelper.ToString(this.MilestoneTargetWomenValue),
                SqlScriptHelper.ToString(this.FinalTargetTotalValue),
                SqlScriptHelper.ToString(this.FinalTargetMenValue),
                SqlScriptHelper.ToString(this.FinalTargetWomenValue),
                SqlScriptHelper.ToString(this.MeasureId),
                SqlScriptHelper.ToString(this.DataSource),
                SqlScriptHelper.ToString(this.ReportingFrequancy),
                SqlScriptHelper.ToString(this.CommonBaseIndicator),
                SqlScriptHelper.ToString(this.Description));
        }
    }
}
