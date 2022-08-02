using System.Collections.Generic;
using System.Text.RegularExpressions;

using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;

namespace Eumis.Documents.Validation.Shared
{
    public class FinanceReportBasicData : CSValidatorBase<R_10078.FinanceReportBasicData>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10078.FinanceReportBasicData complexType, string modelPath, IList<ValidationOption> errors)
        {
            complexType.IsStartDateValid = true;
            complexType.IsEndDateValid = true;

            if (!complexType.StartDate.HasValue)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".StartDate",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.TechReportStartDate, Global.SectionGeneralInformation), true, true));

                complexType.IsStartDateValid = false;
            }

            if (!complexType.EndDate.HasValue)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".EndDate",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.TechReportEndDate, Global.SectionGeneralInformation), true, true));

                complexType.IsEndDateValid = false;
            }

            if (complexType.StartDate.HasValue && complexType.EndDate.HasValue && complexType.EndDate.Value < complexType.StartDate.Value)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".EndDate",
                                    string.Empty,
                                    string.Format(Global.ViewTemplateStartEndDates, Global.TechReportStartDate, Global.TechReportEndDate, Global.SectionGeneralInformation), true, true));

                complexType.IsStartDateValid = false;
                complexType.IsEndDateValid = false;
            }
        }
    }
}
