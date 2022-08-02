using System.Collections.Generic;

using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;

namespace Eumis.Documents.Validation.Shared
{
    public class TechnicalReportActivity :  CSValidatorBase<R_10053.TechnicalReportActivity>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10053.TechnicalReportActivity complexType, string modelPath, IList<ValidationOption> errors)
        {
            //complexType.IsExecutionDescriptionValid = true;
            complexType.IsStatusValid = true;
            //complexType.IsMonthsDurationValid = true;
            complexType.IsActualStartDateValid = true;
            complexType.IsActualEndDateValid = true;
            //complexType.IsDelayReasonValid = true;
            //complexType.IsPeriodResultValid = true;
            //complexType.IsCumulativeResultValid = true;

            //if (string.IsNullOrWhiteSpace(complexType.ExecutionDescription))
            //{
            //    errors.Add(ValidationOption.Create(
            //                        modelPath + ".ExecutionDescription",
            //                        Global.ShortTemplateRequired,
            //                        string.Format(Global.ViewTemplateRequired, Global.ActivityExecutionDescription, Global.SectionActivities), true, true));

            //    complexType.IsExecutionDescriptionValid = false;
            //}

            if (complexType.Status == null || string.IsNullOrWhiteSpace(complexType.Status.Value) || string.IsNullOrWhiteSpace(complexType.Status.Description))
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".Status.Value",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.ActivityStatus, Global.SectionActivities), true, true));

                complexType.IsStatusValid = false;
            }

            //if (string.IsNullOrWhiteSpace(complexType.MonthsDuration))
            //{
            //    errors.Add(ValidationOption.Create(
            //                        modelPath + ".MonthsDuration",
            //                        Global.ShortTemplateRequired,
            //                        string.Format(Global.ViewTemplateRequired, Global.ActivityMonthsDuration, Global.SectionActivities), true, true));

            //    complexType.IsMonthsDurationValid = false;
            //}

            //if (!complexType.ActualStartDate.HasValue)
            //{
            //    errors.Add(ValidationOption.Create(
            //                        modelPath + ".ActualStartDate",
            //                        Global.ShortTemplateRequired,
            //                        string.Format(Global.ViewTemplateRequired, Global.ActivityActualStartDate, Global.SectionActivities), true, true));

            //    complexType.IsActualStartDateValid = false;
            //}

            //if (!complexType.ActualEndDate.HasValue)
            //{
            //    errors.Add(ValidationOption.Create(
            //                        modelPath + ".ActualEndDate",
            //                        Global.ShortTemplateRequired,
            //                        string.Format(Global.ViewTemplateRequired, Global.ActivityActualEndDate, Global.SectionActivities), true, true));

            //    complexType.IsActualEndDateValid = false;
            //}

            if (complexType.ActualStartDate.HasValue && complexType.ActualEndDate.HasValue && complexType.ActualEndDate.Value < complexType.ActualStartDate.Value)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".ActualEndDate",
                                    string.Empty,
                                    string.Format(Global.ViewTemplateStartEndDates, Global.ActivityActualStartDate, Global.ActivityActualEndDate, Global.SectionActivities), true, true));

                complexType.IsActualStartDateValid = false;
                complexType.IsActualEndDateValid = false;
            }

            //if (string.IsNullOrWhiteSpace(complexType.DelayReason))
            //{
            //    errors.Add(ValidationOption.Create(
            //                        modelPath + ".DelayReason",
            //                        Global.ShortTemplateRequired,
            //                        string.Format(Global.ViewTemplateRequired, Global.ActivityDelayReason, Global.SectionActivities), true, true));

            //    complexType.IsDelayReasonValid = false;
            //}

            //if (string.IsNullOrWhiteSpace(complexType.PeriodResult))
            //{
            //    errors.Add(ValidationOption.Create(
            //                        modelPath + ".PeriodResult",
            //                        Global.ShortTemplateRequired,
            //                        string.Format(Global.ViewTemplateRequired, Global.ActivityPeriodResult, Global.SectionActivities), true, true));

            //    complexType.IsPeriodResultValid = false;
            //}

            //if (string.IsNullOrWhiteSpace(complexType.CumulativeResult))
            //{
            //    errors.Add(ValidationOption.Create(
            //                        modelPath + ".CumulativeResult",
            //                        Global.ShortTemplateRequired,
            //                        string.Format(Global.ViewTemplateRequired, Global.ActivityCumulativeResult, Global.SectionActivities), true, true));

            //    complexType.IsCumulativeResultValid = false;
            //}
        }
    }
}
