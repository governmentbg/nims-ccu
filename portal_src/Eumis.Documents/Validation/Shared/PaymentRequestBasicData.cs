using System;
using System.Collections.Generic;
using System.Linq;

using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;

namespace Eumis.Documents.Validation.Shared
{
    public class PaymentRequestBasicData : CSValidatorBase<R_10049.PaymentRequestBasicData>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10049.PaymentRequestBasicData complexType, string modelPath, IList<ValidationOption> errors)
        {
            complexType.IsStartDateValid = true;
            complexType.IsEndDateValid = true;
            complexType.IsFinanceReportAmountValid = true;
            complexType.IsAdditionalIncomeValid = true;
            complexType.IsTotalAmountValid = true;
            complexType.IsFinanceReportAmountWithoutIncomeValid = true;
            //complexType.IsOtherRegistrationValid = true;
            complexType.IsBeneficiaryRegistrationVATValid = true;
            complexType.IsBankAccountValid = true;

            if (!complexType.StartDate.HasValue)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".StartDate",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.PaymentStartDate, Global.SectionGeneralInformation), true, true));

                complexType.IsStartDateValid = false;
            }

            if (!complexType.EndDate.HasValue)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".EndDate",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.PaymentEndDate, Global.SectionGeneralInformation), true, true));

                complexType.IsEndDateValid = false;
            }

            if (complexType.StartDate.HasValue && complexType.EndDate.HasValue && complexType.EndDate.Value < complexType.StartDate.Value)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".EndDate",
                                    string.Empty,
                                    string.Format(Global.ViewTemplateStartEndDates, Global.PaymentStartDate, Global.PaymentEndDate, Global.SectionGeneralInformation), true, true));

                complexType.IsStartDateValid = false;
                complexType.IsEndDateValid = false;
            }

            if (complexType.FinanceReportAmount < 0)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".FinanceReportAmount",
                                    Global.ShortTemplateNonNegativeNumber,
                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.FinanceReportAmount, Global.SectionGeneralInformation), true, true));

                complexType.IsFinanceReportAmountValid = false;
            }

            if (complexType.AdditionalIncome < 0)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".AdditionalIncome",
                                    Global.ShortTemplateNonNegativeNumber,
                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.AdditionalIncome, Global.SectionGeneralInformation), true, true));

                complexType.IsAdditionalIncomeValid = false;
            }

            if (complexType.TotalAmount < 0)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".TotalAmount",
                                    Global.ShortTemplateNonNegativeNumber,
                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.PaymentTotalAmount, Global.SectionGeneralInformation), true, true));

                complexType.IsTotalAmountValid = false;
            }

            if (complexType.FinanceReportAmountWithoutIncome < 0)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".FinanceReportAmountWithoutIncome",
                                    Global.ShortTemplateNonNegativeNumber,
                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.FinanceReportAmountWithoutIncome, Global.SectionGeneralInformation), true, true));

                complexType.IsFinanceReportAmountWithoutIncomeValid = false;
            }

            /*if (complexType.FinanceReportAmountWithoutIncome != complexType.TotalAmount - complexType.AdditionalIncome)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".FinanceReportAmountWithoutIncome",
                                    string.Format(Global.ShortTemplateDifference, Global.PaymentTotalAmount, Global.AdditionalIncome),
                                    string.Format(Global.ViewTemplateDifference, Global.FinanceReportAmountWithoutIncome, Global.SectionGeneralInformation, Global.PaymentTotalAmount, Global.AdditionalIncome), true, true));

                complexType.IsFinanceReportAmountWithoutIncomeValid = false;
            }*/

            //if (string.IsNullOrWhiteSpace(complexType.OtherRegistration))
            //{
            //    errors.Add(ValidationOption.Create(
            //                        modelPath + ".OtherRegistration",
            //                        Global.ShortTemplateRequired,
            //                        string.Format(Global.ViewTemplateRequired, Global.OtherRegistration, Global.SectionGeneralInformation), true, true));

            //    complexType.IsOtherRegistrationValid = false;
            //}

            if (string.IsNullOrWhiteSpace(complexType.BankAccount))
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".BankAccount",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.BankAccount, Global.SectionGeneralInformation), true, true));

                complexType.IsBankAccountValid = false;
            }
        }
    }
}
