namespace Eumis.Domain.Indicators.DataObjects
{
    public class IndicatorDO
    {
        public IndicatorDO()
        {
        }

        public IndicatorDO(Indicator indicator, int? sourceMapNodeId = null)
        {
            this.IndicatorId = indicator.IndicatorId;
            this.IndicatorItemTypeId = indicator.IndicatorItemTypeId;
            this.ProgrammeId = indicator.ProgrammeId;
            this.MeasureId = indicator.MeasureId;
            this.Name = indicator.Name;
            this.NameAlt = indicator.NameAlt;
            this.HasGenderDivision = indicator.HasGenderDivision;
            this.SourceMapNodeId = sourceMapNodeId;
            this.Version = indicator.Version;
        }

        public int? IndicatorId { get; set; }

        public int? ProgrammeId { get; set; }

        public int? MeasureId { get; set; }

        public int? IndicatorItemTypeId { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public bool? HasGenderDivision { get; set; }

        public int? SourceMapNodeId { get; set; }

        public byte[] Version { get; set; }
    }
}
