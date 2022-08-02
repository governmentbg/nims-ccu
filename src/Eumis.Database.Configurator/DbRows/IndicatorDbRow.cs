using Eumis.Database.Configurator.Helpers;

namespace Eumis.Database.Configurator.DbRows
{
    internal class IndicatorDbRow : IDbRow
    {
        public const string DbTableName = "Indicators";
        public const bool UseIdentityInsert = true;
        public static readonly DeterministicGuidGenerator GidGenerator = new DeterministicGuidGenerator();

        public int IndicatorId { get; set; }

        public int ProgrammeId { get; set; }

        public int MeasureId { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public bool HasGenderDivision { get; set; }

        public bool HasQualitativeTarget { get; set; }

        public int ReportingType { get; set; }

        public string CreateRowInsert()
        {
            return string.Format(
                "INSERT INTO [{0}] ([IndicatorId], [ProgrammeId], [MeasureId], [Gid], [Name], [NameAlt], [HasGenderDivision], [CreateDate], [ModifyDate]) VALUES ({1}, {2}, {3}, '{4}', {5}, {6}, {7}, GETDATE(), GETDATE())",
                DbTableName,
                SqlScriptHelper.ToString(this.IndicatorId),
                SqlScriptHelper.ToString(this.ProgrammeId),
                SqlScriptHelper.ToString(this.MeasureId),
                GidGenerator.GetNextDeterministicGuid().ToString(),
                SqlScriptHelper.ToString(this.Name),
                SqlScriptHelper.ToString(this.NameAlt),
                SqlScriptHelper.ToString(this.HasGenderDivision));
        }
    }
}
