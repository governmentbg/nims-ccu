using Eumis.Common.Json;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public enum ProjectsReportItemEvalResult
    {
        [Description(Description = nameof(DataEnumTexts.ProjectsReportItemEvalResult_Passed), ResourceType = typeof(DataEnumTexts))]
        Passed = 1,

        [Description(Description = nameof(DataEnumTexts.ProjectsReportItemEvalResult_NotPassed), ResourceType = typeof(DataEnumTexts))]
        NotPassed = 2,
    }
}
