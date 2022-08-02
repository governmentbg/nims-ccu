using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;
using Eumis.Documents.Enums;

namespace Eumis.Documents.Validation.Shared
{
    public class BFPContractBasicData : CSValidatorBase<R_10031.BFPContractBasicData>
    {
        private Regex _latinRegex = new Regex(@"^[^А-Яа-я]*$");

        protected override void Validate(ICSValidationEngine csValidationEngine, R_10031.BFPContractBasicData complexType, string modelPath, IList<ValidationOption> errors)
        {
            complexType.IsNameValid = true;
            complexType.IsDurationValid = true;
            complexType.IsNameEnValid = true;
            complexType.IsDescriptionValid = true;
            complexType.IsDescriptionEnValid = true;
            complexType.IsPurposeValid = true;
            complexType.IsCompletionStatusValid = true;
            complexType.IsStartDateValid = true;
            complexType.IsCompletionDateValid = true;
            complexType.IsContractDateValid = true;

            complexType.IsBankAccountValid = true;

            if (string.IsNullOrWhiteSpace(complexType.Name))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Name",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.BFPContractBasicDataName, Global.SectionBasicData), true, true));

                complexType.IsNameValid = false;
            }
            else if (complexType.Name.Length > Constants.ProjectBasicDataNameLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Name",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.ProjectBasicDataNameLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.BFPContractBasicDataName, Global.SectionBasicData, Constants.ProjectBasicDataNameLength), true, true));

                complexType.IsNameValid = false;
            }

            if (!string.IsNullOrWhiteSpace(complexType.Duration))
            {
                int t;
                int minMonths = 1;

                if (!int.TryParse(complexType.Duration, out t))
                {
                    string durationLabel = string.Format("{0} (1-{1})", Global.Duration, complexType.MaxDuration);

                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Duration",
                                        Global.ShortTemplateInteger,
                                        string.Format(Global.ViewTemplateInteger, durationLabel, Global.SectionBasicData), true, true));

                    complexType.IsDurationValid = false;
                }
                else if (t < minMonths || t > complexType.MaxDuration)
                {
                    string durationLabel = string.Format("{0} (1-{1})", Global.Duration, complexType.MaxDuration);

                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Duration",
                                        string.Format(Global.ShortTemplateNumberRange, minMonths, complexType.MaxDuration),
                                        string.Format(Global.ViewTemplateNumberRange, durationLabel, Global.SectionBasicData, minMonths, complexType.MaxDuration), true, true));

                    complexType.IsDurationValid = false;
                }
            }
            else
            {
                string durationLabel = string.Format("{0} (1-{1})", Global.Duration, complexType.MaxDuration);

                errors.Add(ValidationOption.Create(
                                        modelPath + ".Duration",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, durationLabel, Global.SectionBasicData), true, true));

                complexType.IsDurationValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.NameEN))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".NameEN",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.BFPContractBasicDataNameEn, Global.SectionBasicData), true, true));

                complexType.IsNameEnValid = false;
            }
            else if (complexType.NameEN.Length > Constants.ProjectBasicDataNameEnLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".NameEN",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.ProjectBasicDataNameEnLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.BFPContractBasicDataNameEn, Global.SectionBasicData, Constants.ProjectBasicDataNameEnLength), true, true));

                complexType.IsNameEnValid = false;
            }
            else if (!_latinRegex.IsMatch(complexType.NameEN))
            {
                errors.Add(ValidationOption.Create(
                    modelPath + ".NameEN",
                    Global.ShortTemplateSymbolsNames2,
                    string.Format(Global.ViewTemplateSymbolsNames2, Global.BFPContractBasicDataNameEn, Global.SectionBasicData), true, true));

                complexType.IsNameEnValid = false;
            }

            if (complexType.CompletionStatus == null || string.IsNullOrWhiteSpace(complexType.CompletionStatus.Id) || string.IsNullOrWhiteSpace(complexType.CompletionStatus.Name))
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".CompletionStatus.id",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.CompletionStatus, Global.SectionBasicData),
                                    true, true));

                complexType.IsCompletionStatusValid = false;
            }

            if (!complexType.StartDate.HasValue)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".StartDate",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.BFPStartDate, Global.SectionBasicData), true, true));

                complexType.IsStartDateValid = false;
            }

            if (!complexType.CompletionDate.HasValue)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".CompletionDate",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.BFPCompletionDate, Global.SectionBasicData), true, true));

                complexType.IsCompletionDateValid = false;
            }

            if (complexType.StartDate.HasValue && complexType.CompletionDate.HasValue && complexType.StartDate.Value >= complexType.CompletionDate.Value)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".CompletionDate",
                                    string.Empty,
                                    string.Format(Global.ViewTemplateStartEndDates, Global.BFPStartDate, Global.BFPCompletionDate, Global.SectionBasicData), true, true));

                complexType.IsStartDateValid = false;
                complexType.IsCompletionDateValid = false;
            }

            if (!complexType.ContractDate.HasValue)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".ContractDate",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.ContractDate, Global.SectionBasicData), true, true));

                complexType.IsContractDateValid = false;
            }

            if (complexType.NutsAddress != null && complexType.NutsAddress.NutsAddressContentCollection != null && complexType.NutsAddress.NutsAddressContentCollection.Count > 0)
            {
                string nutsValue = complexType.NutsAddress.NutsLevel.Id;
                List<R_09989.Location> values;
                string labelName, resource;

                if (complexType.Locations == null || complexType.Locations.Count == 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".LocationFullPath",
                                        string.Empty,
                                        Global.ViewTemplateMissingLocation, true, true));
                }

                if (nutsValue == NutsLevelNomenclature.CountryEU.Id)
                {
                    values = complexType.NutsAddress.NutsAddressContentCollection.Select(item => item.Country).ToList();
                    labelName = "Country";
                    resource = Global.Country;
                }
                else if (nutsValue == NutsLevelNomenclature.ProtectedZone.Id)
                {
                    values = complexType.NutsAddress.NutsAddressContentCollection.Select(item => item.ProtectedZone).ToList();
                    labelName = "ProtectedZone";
                    resource = Global.ProtectedZone;
                }
                else if (nutsValue == NutsLevelNomenclature.Nuts1.Id)
                {
                    values = complexType.NutsAddress.NutsAddressContentCollection.Select(item => item.Nuts1).ToList();
                    labelName = "Nuts1";
                    resource = Global.Nuts1;
                }
                else if (nutsValue == NutsLevelNomenclature.Nuts2.Id)
                {
                    values = complexType.NutsAddress.NutsAddressContentCollection.Select(item => item.Nuts2).ToList();
                    labelName = "Nuts2";
                    resource = Global.Nuts2;
                }
                else if (nutsValue == NutsLevelNomenclature.District.Id)
                {
                    values = complexType.NutsAddress.NutsAddressContentCollection.Select(item => item.District).ToList();
                    labelName = "District";
                    resource = Global.District;
                }
                else if (nutsValue == NutsLevelNomenclature.Municipality.Id)
                {
                    values = complexType.NutsAddress.NutsAddressContentCollection.Select(item => item.Municipality).ToList();
                    labelName = "Municipality";
                    resource = Global.Municipality;
                }
                else
                {
                    values = complexType.NutsAddress.NutsAddressContentCollection.Select(item => item.Settlement).ToList();
                    labelName = "Settlement";
                    resource = Global.Settlement;
                }

                List<string> fullPaths = values.Select(v => v != null ? v.FullPath : String.Empty).ToList();

                var filled = values.Where(loc => loc != null && !string.IsNullOrWhiteSpace(loc.Code)).ToList();
                int distinctCount = filled.Select(loc => loc.Code).Distinct().Count();

                if (distinctCount < filled.Count)
                {
                    string path = string.Format("{0}.NutsAddress.NutsAddressContentCollection[0].{1}.Code", modelPath, labelName);

                    errors.Add(ValidationOption.Create(
                                                 path,
                                                 Global.ShortTemplateLocationUnique,
                                                 string.Format(Global.ViewTemplateLocationUnique, Global.SectionBasicData), true, true));

                    SetNutsAddressValidationFlag(complexType, nutsValue, 0);
                }

                for (int i = 0; i < values.Count; i++)
                {
                    if (values[i] == null ||
                        string.IsNullOrWhiteSpace(values[i].Code) ||
                        string.IsNullOrWhiteSpace(values[i].Name))
                    {
                        string path = string.Format("{0}.NutsAddress.NutsAddressContentCollection[{1}].{2}.Code", modelPath, i, labelName);

                        errors.Add(ValidationOption.Create(
                                        path,
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, resource, Global.SectionBasicData), true, true));

                        SetNutsAddressValidationFlag(complexType, nutsValue, i);
                    }
                    else
                    {
                        string path = string.Format("{0}.NutsAddress.NutsAddressContentCollection[{1}].{2}.Code", modelPath, i, labelName);

                        if (string.IsNullOrWhiteSpace(fullPaths[i]))
                        {
                            errors.Add(ValidationOption.Create(
                                        path,
                                        Global.ShortTemplateMissingLocation2,
                                        string.Format(Global.ViewTemplateMissingLocation2, Global.SectionBasicData), true, true));

                            SetNutsAddressValidationFlag(complexType, nutsValue, i);
                        }
                        else if (complexType.Locations != null && !complexType.Locations.Where(x => x.Item2 == Constants.EUCode).Any() && !complexType.Locations.Where(x => fullPaths[i].Contains(x.Item2)).Any())
                        {
                            errors.Add(ValidationOption.Create(
                                        path,
                                        Global.ShortTemplateLocation,
                                        string.Format(Global.ViewTemplateLocation, Global.SectionBasicData), true, true));

                            SetNutsAddressValidationFlag(complexType, nutsValue, i);
                        }
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(complexType.Description))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Description",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.BFPContractBasicDataDescription, Global.SectionBasicData), true, true));

                complexType.IsDescriptionValid = false;
            }
            else if (complexType.Description.Length > Constants.ProjectBasicDataDescriptionLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Description",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.ProjectBasicDataDescriptionLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.BFPContractBasicDataDescription, Global.SectionBasicData, Constants.ProjectBasicDataDescriptionLength), true, true));

                complexType.IsDescriptionValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.DescriptionEN))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".DescriptionEN",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.BFPContractBasicDataDescriptionEn, Global.SectionBasicData), true, true));

                complexType.IsDescriptionEnValid = false;
            }
            else if (complexType.DescriptionEN.Length > Constants.ProjectBasicDataDescriptionEnLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".DescriptionEN",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.ProjectBasicDataDescriptionEnLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.BFPContractBasicDataDescriptionEn, Global.SectionBasicData, Constants.ProjectBasicDataDescriptionEnLength), true, true));

                complexType.IsDescriptionEnValid = false;
            }
            else if (!_latinRegex.IsMatch(complexType.DescriptionEN))
            {
                errors.Add(ValidationOption.Create(
                    modelPath + ".DescriptionEN",
                    Global.ShortTemplateSymbolsNames2,
                    string.Format(Global.ViewTemplateSymbolsNames2, Global.BFPContractBasicDataDescriptionEn, Global.SectionBasicData), true, true));

                complexType.IsDescriptionEnValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.Purpose))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Purpose",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.BFPContractBasicDataPurpose, Global.SectionBasicData), true, true));

                complexType.IsPurposeValid = false;
            }
            else if (complexType.Purpose.Length > Constants.ProjectBasicDataPurposeLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Purpose",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.ProjectBasicDataPurposeLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.BFPContractBasicDataPurpose, Global.SectionBasicData, Constants.ProjectBasicDataPurposeLength), true, true));

                complexType.IsPurposeValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.BankAccount))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".BankAccount",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.BFPContractBasicDataBankAccount, Global.SectionBeneficiary),
                                        true, true));

                complexType.IsBankAccountValid = false;
            }
        }

        private static void SetNutsAddressValidationFlag(R_10031.BFPContractBasicData complexType, string nutsValue, int index)
        {
            complexType.NutsAddress.NutsAddressContentCollection[index].IsCountryValid = true;
            complexType.NutsAddress.NutsAddressContentCollection[index].IsProtectedZoneValid = true;
            complexType.NutsAddress.NutsAddressContentCollection[index].IsNuts1Valid = true;
            complexType.NutsAddress.NutsAddressContentCollection[index].IsNuts2Valid = true;
            complexType.NutsAddress.NutsAddressContentCollection[index].IsDistrictValid = true;
            complexType.NutsAddress.NutsAddressContentCollection[index].IsMunicipalityValid = true;
            complexType.NutsAddress.NutsAddressContentCollection[index].IsSettlementValid = true;

            if (nutsValue == NutsLevelNomenclature.CountryEU.Id)
                complexType.NutsAddress.NutsAddressContentCollection[index].IsCountryValid = false;
            else if (nutsValue == NutsLevelNomenclature.ProtectedZone.Id)
                complexType.NutsAddress.NutsAddressContentCollection[index].IsProtectedZoneValid = false;
            else if (nutsValue == NutsLevelNomenclature.Nuts1.Id)
                complexType.NutsAddress.NutsAddressContentCollection[index].IsNuts1Valid = false;
            else if (nutsValue == NutsLevelNomenclature.Nuts2.Id)
                complexType.NutsAddress.NutsAddressContentCollection[index].IsNuts2Valid = false;
            else if (nutsValue == NutsLevelNomenclature.District.Id)
                complexType.NutsAddress.NutsAddressContentCollection[index].IsDistrictValid = false;
            else if (nutsValue == NutsLevelNomenclature.Municipality.Id)
                complexType.NutsAddress.NutsAddressContentCollection[index].IsMunicipalityValid = false;
            else
                complexType.NutsAddress.NutsAddressContentCollection[index].IsSettlementValid = false;
        }
    }
}
