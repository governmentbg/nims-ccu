using System;
using System.Collections.Generic;
using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;
using System.Text.RegularExpressions;

namespace Eumis.Documents.Validation.Shared
{
    public class Address : CSValidatorBase<R_10003.Address>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10003.Address complexType, string modelPath, IList<ValidationOption> errors)
        {
            string newPath = Regex.Replace(modelPath, @"\[\d+\]", string.Empty);

            complexType.IsCountryValid = true;
            complexType.IsSettlementValid = true;
            complexType.IsPostCodeValid = true;
            complexType.IsStreetValid = true;
            complexType.IsFullAddressValid = true;

            if (complexType.Country == null || string.IsNullOrWhiteSpace(complexType.Country.Code) || string.IsNullOrWhiteSpace(complexType.Country.Name))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Country.Code",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.Country, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                complexType.IsCountryValid = false;
            }
            else
            {
                if (complexType.Country.Code == Constants.BulgariaId)
                {
                    if (complexType.Settlement == null || string.IsNullOrWhiteSpace(complexType.Settlement.Code) || string.IsNullOrWhiteSpace(complexType.Settlement.Name))
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".Settlement.Code",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.Settlement, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                        complexType.IsSettlementValid = false;
                    }

                    if (!string.IsNullOrWhiteSpace(complexType.PostCode))
                    {
                        if (complexType.PostCode.Length != 4)
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".PostCode",
                                                string.Format(Global.ShortTemplateDigitsExact, 4),
                                                string.Format(Global.ViewTemplateDigitsExact, Global.PostCode, GetResourceKey(csValidationEngine.AppRioCode, newPath), 4),
                                                true, true));

                            complexType.IsPostCodeValid = false;
                        }
                        else
                        {
                            if (!_postCodeRegex.IsMatch(complexType.PostCode))
                            {
                                errors.Add(ValidationOption.Create(
                                                modelPath + ".PostCode",
                                                Global.ShortTemplateDigits,
                                                string.Format(Global.ViewTemplateDigits, Global.PostCode, GetResourceKey(csValidationEngine.AppRioCode, newPath)),
                                                true, true));

                                complexType.IsPostCodeValid = false;
                            }
                        }
                    }
                    else
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".PostCode",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.PostCode, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                        complexType.IsPostCodeValid = false;
                    }

                    if (string.IsNullOrWhiteSpace(complexType.Street))
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".Street",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.Street, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                        complexType.IsStreetValid = false;
                    }
                    else if (complexType.Street.Length > Constants.CandidateStreetLength)
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".Street",
                                                string.Format(Global.ShortTemplateSymbolsMax, Constants.CandidateStreetLength),
                                                string.Format(Global.ViewTemplateSymbolsMax, Global.Street, GetResourceKey(csValidationEngine.AppRioCode, newPath), Constants.CandidateStreetLength), true, true));

                        complexType.IsStreetValid = false;
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(complexType.FullAddress))
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".FullAddress",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.Address, GetResourceKey(csValidationEngine.AppRioCode, newPath)), true, true));

                        complexType.IsFullAddressValid = false;
                    }
                }
            }
        }

        private Regex _postCodeRegex = new Regex("^[0-9]+$");
    }
}