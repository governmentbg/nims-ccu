using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;
using Eumis.Documents.Enums;
using Eumis.Documents.Mappers;

namespace Eumis.Documents.Validation.Eumis
{
    public class Procurements : CSValidatorBase<R_10041.Procurements>
    {
        private Regex _latinRegex = new Regex(@"^[^А-Яа-я]*$");

        protected override void Validate(ICSValidationEngine csValidationEngine, R_10041.Procurements complexType, string modelPath, IList<ValidationOption> errors)
        {
            #region ListOfContractors

            if (complexType.Contractors != null && complexType.Contractors.ContractorCollection != null && complexType.Contractors.ContractorCollection.Count > 0 && !complexType.Contractors.isLocked)
            {
                if (complexType.Contractors.ContractorCollection.Count <= Constants.ContractorsMaxCount)
                {
                    var duplicatedUins = new List<string>();
                    for (int i = 0; i < complexType.Contractors.ContractorCollection.Count; i++)
                    {
                        var current = complexType.Contractors.ContractorCollection[i];

                        bool isUinTypeEmpty = current.UinType == null || string.IsNullOrWhiteSpace(current.UinType.Id) || string.IsNullOrWhiteSpace(current.UinType.Name);

                        current.IsUinTypeValid = true;
                        current.IsUinValid = true;
                        current.IsNameValid = true;
                        current.IsNameEnValid = true;
                        current.IsAddressValid = true;
                        current.IsRepresentativeNamesValid = true;
                        current.IsRepresentativeIDNumberValid = true;

                        if (!current.isActive && complexType.ContractContractors.ContractContractorCollection.Any(x =>
                            x.Contractor != null && !string.IsNullOrWhiteSpace(x.Contractor.Id) && !string.IsNullOrWhiteSpace(x.Contractor.Name) && x.Contractor.Id == current.gid && x.isActive))
                        {
                            errors.Add(ValidationOption.Create(
                                            modelPath + ".Contractors.ContractorCollection[" + i + "]",
                                            string.Empty,
                                           string.Format(Global.ContractContractorDependancy, current.Uin, Global.SectionAgreementsWithContractors), true, true));
                        }

                        if (!current.isActive)
                        {
                            continue;
                        }

                        if (isUinTypeEmpty)
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Contractors.ContractorCollection[" + i + "].UinType.id",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.Bulstat, Global.SectionContractors), true, true));

                            current.IsUinTypeValid = false;
                        }

                        if (string.IsNullOrWhiteSpace(current.Uin))
                        {
                           errors.Add(ValidationOption.Create(
                                                modelPath + ".Contractors.ContractorCollection[" + i + "].Uin",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.Bulstat, Global.SectionContractors), true, true));

                            current.IsUinValid = false;
                        }
                        else if (!isUinTypeEmpty)
                        {
                            string uinTypeValue = current.UinType.Id;

                            if ((uinTypeValue == UinTypeNomenclature.Bulstat.Code && !BulstatValidator.ValidateBulstat(current.Uin)) ||
                                (uinTypeValue == UinTypeNomenclature.UIC.Code && !BulstatValidator.ValidateBulstat(current.Uin)) ||
                                 uinTypeValue == UinTypeNomenclature.Freelancers.Code && !BulstatValidator.IsValidEGN(current.Uin))
                            {
                                errors.Add(ValidationOption.Create(
                                        modelPath + ".Contractors.ContractorCollection[" + i + "].Uin",
                                        Global.ShortTemplateInvalid,
                                        string.Format(Global.ViewTemplateInvalid, Global.Bulstat, Global.SectionContractors), true, true));

                                current.IsUinValid = false;
                            }
                            else if (current.Uin.Length > Constants.BulstatFieldMaxLength)
                            {
                                errors.Add(ValidationOption.Create(
                                                modelPath + ".Contractors.ContractorCollection[" + i + "].Uin",
                                                string.Format(Global.ShortTemplateSymbolsMax, Constants.BulstatFieldMaxLength),
                                                string.Format(Global.ViewTemplateSymbolsMax, Global.Bulstat, Global.SectionContractors, Constants.BulstatFieldMaxLength), true, true));

                                current.IsUinValid = false;
                            }
                            
                            int countUins = complexType.Contractors.ContractorCollection.Where(x => x.Uin == current.Uin && x.UinType.Id != UinTypeNomenclature.Foreign.Code && x.isActive).Count();
                            if (countUins > 1)
                            {
                                if (!duplicatedUins.Contains(current.Uin))
                                {
                                    errors.Add(ValidationOption.Create(
                                                    modelPath + ".Contractors.ContractorCollection[" + i + "].Uin",
                                                    Global.ShortTemplateDuplicatedValue,
                                                    string.Format(Global.ViewTemplateDuplicatedValue, Global.Bulstat, current.Uin, countUins, Global.SectionContractors), true, true));
                                    duplicatedUins.Add(current.Uin);
                                }
                                current.IsUinValid = false;
                            }
                        }

                        if (string.IsNullOrWhiteSpace(current.Name))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Contractors.ContractorCollection[" + i + "].Name",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.ContractorName, Global.SectionContractors), true, true));

                            current.IsNameValid = false;
                        }
                        else if (current.Name.Length > Constants.ContractorNameLength)
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Contractors.ContractorCollection[" + i + "].Name",
                                                    string.Format(Global.ShortTemplateSymbolsMax, Constants.ContractorNameLength),
                                                    string.Format(Global.ViewTemplateSymbolsMax, Global.ContractorName, Global.SectionContractors, Constants.CandidateNameLength), true, true));

                            current.IsNameValid = false;
                        }

                        if (string.IsNullOrWhiteSpace(current.NameEN))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Contractors.ContractorCollection[" + i + "].NameEN",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.ContractorNameEn, Global.SectionContractors), true, true));

                            current.IsNameEnValid = false;
                        }
                        else if (current.NameEN.Length > Constants.ContractorNameEnLength)
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".Contractors.ContractorCollection[" + i + "].NameEN",
                                                    string.Format(Global.ShortTemplateSymbolsMax, Constants.ContractorNameEnLength),
                                                    string.Format(Global.ViewTemplateSymbolsMax, Global.ContractorNameEn, Global.SectionContractors, Constants.ContractorNameEnLength), true, true));

                            current.IsNameEnValid = false;
                        }
                        else if (!_latinRegex.IsMatch(current.NameEN))
                        {
                            errors.Add(ValidationOption.Create(
                                modelPath + ".Contractors.ContractorCollection[" + i + "].NameEN",
                                Global.ShortTemplateSymbolsNames2,
                                string.Format(Global.ViewTemplateSymbolsNames2, Global.ContractorNameEn, Global.SectionContractors), true, true));

                            current.IsNameEnValid = false;
                        }

                        if (current.Seat != null)
                        {
                            EngineValidate(csValidationEngine, current.Seat, modelPath + ".Contractors.ContractorCollection[" + i + "].Seat", errors);
                        }

                        

                        // if (string.IsNullOrWhiteSpace(current.RepresentativeNames))
                        // {
                        //     errors.Add(ValidationOption.Create(
                        //                         modelPath + ".Contractors.ContractorCollection[" + i + "].RepresentativeNames",
                        //                         Global.ShortTemplateRequired,
                        //                         string.Format(Global.ViewTemplateRequired, Global.RepresentativeNames, Global.SectionContractors), true, true));
                        // 
                        //     current.IsRepresentativeNamesValid = false;
                        // }

                        // if (string.IsNullOrWhiteSpace(current.RepresentativeIDNumber))
                        // {
                        //     errors.Add(ValidationOption.Create(
                        //                         modelPath + ".Contractors.ContractorCollection[" + i + "].RepresentativeIDNumber",
                        //                         Global.ShortTemplateRequired,
                        //                         string.Format(Global.ViewTemplateRequired, Global.RepresentativeIDNumber, Global.SectionContractors), true, true));
                        // 
                        //     current.IsRepresentativeIDNumberValid = false;
                        // }
                        // else if (!BulstatValidator.IsValidEGN(current.RepresentativeIDNumber))
                        // {
                        //     errors.Add(ValidationOption.Create(
                        //                         modelPath + "..Contractors.ContractorCollection[" + i + "].RepresentativeIDNumber",
                        //                         Global.ShortTemplateInvalid,
                        //                         string.Format(Global.ViewTemplateInvalid, Global.RepresentativeIDNumber, Global.SectionContractors), true, true));
                        // 
                        //     current.IsRepresentativeIDNumberValid = false;
                        // }
                    }

                    complexType.Contractors.IsValid = true;
                }
                else
                {
                    errors.Add(ValidationOption.Create(
                        modelPath + ".Contractors.ContractorCollection",
                        string.Format(Global.ShortTemplateMaxContractors, Constants.ContractorsMaxCount),
                        string.Format(Global.ViewTemplateMaxContractors, Constants.ContractorsMaxCount,
                            Global.SectionContractors),
                        true, true));

                    complexType.Contractors.IsValid = false;
                }
            }

            #endregion

            #region AgreementsWithContractors

            if (complexType.ContractContractors != null && complexType.ContractContractors.ContractContractorCollection != null && complexType.ContractContractors.ContractContractorCollection.Count > 0 && !complexType.ContractContractors.isLocked)
            {
                if (complexType.ContractContractors.ContractContractorCollection.Count <= Constants.ContractContractorsMaxCount)
                {
                    for (int i = 0; i < complexType.ContractContractors.ContractContractorCollection.Count; i++)
                    {
                        var current = complexType.ContractContractors.ContractContractorCollection[i];

                        current.IsSignDateValid = true;
                        current.IsNumberValid = true;
                        current.IsTotalAmountExcludingVATValid = true;
                        current.IsContractAmountWithoutVATValid = true;
                        current.IsVATAmountIfEligibleValid = true;
                        current.IsTotalFundedValueValid = true;
                        current.IsBudgetDifferenceValueValid = true;
                        current.IsStartDateValid = true;
                        current.IsEndDateValid = true;
                        current.IsContractorValid = true;
                        current.IsUniquePairValid = true;
                        current.IsNumberAnnexesValid = true;
                        current.IsCommentValid = true;

                        if (!current.isActive)
                        {
                            continue;
                        }

                        if (!current.SignDate.HasValue)
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].SignDate",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.SignDate, Global.SectionAgreementsWithContractors),
                                                true, true));

                            current.IsSignDateValid = false;
                        }

                        if (string.IsNullOrWhiteSpace(current.Number))
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].Number",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.Number, Global.SectionAgreementsWithContractors),
                                                true, true));

                            current.IsNumberValid = false;
                        }

                        if (current.TotalAmountExcludingVAT < 0)
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].TotalAmountExcludingVAT",
                                                Global.ShortTemplateNonNegativeNumber,
                                                string.Format(Global.ViewTemplateNonNegativeNumber, Global.TotalAmountExcludingVAT, Global.SectionAgreementsWithContractors),
                                                true, true));

                            current.IsTotalAmountExcludingVATValid = false;
                        }

                        if (current.VATAmountIfEligible < 0)
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].VATAmountIfEligible",
                                                Global.ShortTemplateNonNegativeNumber,
                                                string.Format(Global.ViewTemplateNonNegativeNumber, Global.VATAmountIfEligible, Global.SectionAgreementsWithContractors),
                                                true, true));

                            current.IsVATAmountIfEligibleValid = false;
                        }

                        if (current.TotalFundedValue < 0)
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].TotalFundedValue",
                                                Global.ShortTemplateNonNegativeNumber,
                                                string.Format(Global.ViewTemplateNonNegativeNumber, Global.TotalFundedValue, Global.SectionAgreementsWithContractors),
                                                true, true));

                            current.IsTotalFundedValueValid = false;
                        }

                        if (!string.IsNullOrWhiteSpace(current.NumberAnnexes))
                        {
                            int t;

                            if (!int.TryParse(current.NumberAnnexes, out t))
                            {
                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].NumberAnnexes",
                                                    Global.ShortTemplateInteger,
                                                    string.Format(Global.ViewTemplateInteger, Global.NumberAnnexes, Global.SectionAgreementsWithContractors), true, true));

                                current.IsNumberAnnexesValid = false;
                            }
                        }
                        else
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].NumberAnnexes",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.NumberAnnexes, Global.SectionAgreementsWithContractors), true, true));

                            current.IsNumberAnnexesValid = false;
                        }

                        if (current.CurrentAnnexTotalAmount < 0)
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].CurrentAnnexTotalAmount",
                                                Global.ShortTemplateNonNegativeNumber,
                                                string.Format(Global.ViewTemplateNonNegativeNumber, Global.CurrentAnnexTotalAmount, Global.SectionAgreementsWithContractors),
                                                true, true));

                            current.IsCurrentAnnexTotalAmountValid = false;
                        }

                        if (!current.StartDate.HasValue)
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].StartDate",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.StartDate, Global.SectionAgreementsWithContractors),
                                                true, true));

                            current.IsStartDateValid = false;
                        }

                        if (!current.EndDate.HasValue)
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].EndDate",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.EndDate, Global.SectionAgreementsWithContractors),
                                                true, true));

                            current.IsEndDateValid = false;
                        }

                        if (current.StartDate.HasValue && current.EndDate.HasValue && current.EndDate.Value < current.StartDate.Value)
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].EndDate",
                                                string.Empty,
                                                string.Format(Global.ViewTemplateStartEndDates, Global.StartDate, Global.EndDate, Global.SectionAgreementsWithContractors),
                                                true, true));

                            current.IsStartDateValid = false;
                            current.IsEndDateValid = false;
                        }

                        if (current.Contractor == null || string.IsNullOrWhiteSpace(current.Contractor.Id) || string.IsNullOrWhiteSpace(current.Contractor.Name))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].Contractor.Id",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.Contractor, Global.SectionAgreementsWithContractors),
                                                    true, true));

                            current.IsContractorValid = false;
                        }

                        if (current.HasSubcontractorMember && current.SubcontractorMemberCollection != null && current.SubcontractorMemberCollection.Count > 0)
                        {
                            for (int j = 0; j < current.SubcontractorMemberCollection.Count; j++)
                            {
                                var member = current.SubcontractorMemberCollection[j];

                                member.IsContractorValid = true;
                                member.IsContractDateValid = true;
                                member.IsContractNumberValid = true;
                                member.IsContractAmountValid = true;

                                if (member.Contractor == null || string.IsNullOrWhiteSpace(member.Contractor.Id) || string.IsNullOrWhiteSpace(member.Contractor.Name))
                                {
                                    errors.Add(ValidationOption.Create(
                                                            modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].SubcontractorMemberCollection[" + j + "].Contractor.Id",
                                                            Global.ShortTemplateRequired,
                                                            string.Format(Global.ViewTemplateRequiredRowTableSection, Global.SubcontractorMemberName, (i + 1), Global.SubcontractorMembersTable, Global.SectionAgreementsWithContractors),
                                                            true, true));

                                    member.IsContractorValid = false;
                                }

                                if (!member.ContractDate.HasValue)
                                {
                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].SubcontractorMemberCollection[" + j + "].ContractDate",
                                                        Global.ShortTemplateRequired,
                                                        string.Format(Global.ViewTemplateRequiredRowTableSection, Global.SubcontractorMemberContractDate, (i + 1), Global.SubcontractorMembersTable, Global.SectionAgreementsWithContractors),
                                                        true, true));

                                    member.IsContractDateValid = false;
                                }

                                if (string.IsNullOrWhiteSpace(member.ContractNumber))
                                {
                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].SubcontractorMemberCollection[" + j + "].ContractNumber",
                                                        Global.ShortTemplateRequired,
                                                        string.Format(Global.ViewTemplateRequiredRowTableSection, Global.SubcontractorMemberContractNumber, (i + 1), Global.SubcontractorMembersTable, Global.SectionAgreementsWithContractors),
                                                        true, true));

                                    member.IsContractNumberValid = false;
                                }

                                if (member.ContractAmount < 0)
                                {
                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].SubcontractorMemberCollection[" + j + "].ContractAmount",
                                                        Global.ShortTemplateRequired,
                                                        string.Format(Global.ViewTemplateRequiredRowTableSection, Global.SubcontractorMemberContractAmount, (i + 1), Global.SubcontractorMembersTable, Global.SectionAgreementsWithContractors),
                                                        true, true));

                                    member.IsContractAmountValid = false;
                                }

                                if (member.Contractor != null && complexType?.Contractors?.ContractorCollection != null && member.IsContractorValid &&
                                    !complexType.Contractors.ContractorCollection.Where(x => x.gid == member.Contractor.Id).Any())
                                {
                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].SubcontractorMemberCollection[" + j + "].Contractor.Id",
                                                        string.Empty,
                                                        string.Format(Global.ViewTemplateRequiredSubcontractorInContractors, Global.Subcontractor, (i + 1), Global.SubcontractorMembersTable, Global.SectionContractors),
                                                        true, true));
                                    member.IsContractorValid = false;
                                }
                            }
                        }

                        #region List of activities on budget lines

                        if (current.isActive)
                        {
                            for (int j = 0; j < current.ActivitiesBudgetDetailsRefCollection.Count; j++)
                            {
                                var innerCurrent = current.ActivitiesBudgetDetailsRefCollection[j];

                                innerCurrent.IsContractActivityValid = true;
                                innerCurrent.IsBudgetDetailValid = true;

                                var isContractActivityEmpty = innerCurrent.ContractActivity == null || string.IsNullOrWhiteSpace(innerCurrent.ContractActivity.Id) || string.IsNullOrWhiteSpace(innerCurrent.ContractActivity.Name);
                                var isBudgetDetailEmpty = innerCurrent.BudgetDetail == null || string.IsNullOrWhiteSpace(innerCurrent.BudgetDetail.Id) || string.IsNullOrWhiteSpace(innerCurrent.BudgetDetail.Name);

                                if (isContractActivityEmpty)
                                {
                                    // errors.Add(ValidationOption.Create(
                                    //                 modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].ActivitiesBudgetDetailsRefCollection[" + j + "].ContractActivity.Id",
                                    //                 Global.ShortTemplateRequired,
                                    //                 string.Format(Global.ViewTemplateRequired, Global.ContractActivity, Global.SectionAgreementsWithContractors),
                                    //                 true, true));
                                    // 
                                    // innerCurrent.IsContractActivityValid = false;
                                }

                                if (isBudgetDetailEmpty)
                                {
                                    errors.Add(ValidationOption.Create(
                                                    modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].ActivitiesBudgetDetailsRefCollection[" + j + "].BudgetDetail.Id",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.BudgetDetail, Global.SectionAgreementsWithContractors),
                                                    true, true));

                                    innerCurrent.IsBudgetDetailValid = false;
                                }

                                if (!isContractActivityEmpty && !isBudgetDetailEmpty)
                                {
                                    var hasUniquePairs = true;

                                    for (int k = 0; k < current.ActivitiesBudgetDetailsRefCollection.Count; k++)
                                    {
                                        if (current.ActivitiesBudgetDetailsRefCollection[k].ContractActivity != null && current.ActivitiesBudgetDetailsRefCollection[k].BudgetDetail != null &&
                                            current.ActivitiesBudgetDetailsRefCollection[j].ContractActivity.Id == current.ActivitiesBudgetDetailsRefCollection[k].ContractActivity.Id &&
                                            current.ActivitiesBudgetDetailsRefCollection[j].BudgetDetail.Id == current.ActivitiesBudgetDetailsRefCollection[k].BudgetDetail.Id && k != j)
                                        {
                                            hasUniquePairs = false;
                                        }
                                    }

                                    if (!hasUniquePairs)
                                    {
                                        errors.Add(ValidationOption.Create(
                                                    modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].ActivitiesBudgetDetailsRefCollection",
                                                    Global.ShortTemplateUniquePairsRequired,
                                                    string.Format(Global.ViewTemplateUniquePairsRequired, Global.SectionAgreementsWithContractors),
                                                    true, true));

                                        current.IsUniquePairValid = false;
                                    }
                                }

                                var isContractActivityActive = false;
                                if (complexType.ContractActivityCollection != null && !isContractActivityEmpty)
                                {
                                    isContractActivityActive = complexType.ContractActivityCollection.Any(x => x.gid == innerCurrent.ContractActivity.Id && x.isActive);

                                    if (!isContractActivityActive)
                                    {
                                        errors.Add(ValidationOption.Create(
                                                    modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].ActivitiesBudgetDetailsRefCollection[" + j + "].ContractActivity.Id",
                                                    Global.ShortTemplateContractActivityCanceled,
                                                    string.Format(Global.ViewTemplateContractActivityCanceled, innerCurrent.ContractActivity.Name, Global.SectionAgreementsWithContractors),
                                                    true, true));

                                        innerCurrent.IsContractActivityValid = false;
                                    }
                                }

                                var isBudgetDetailActive = false;
                                if (complexType.BudgetLevel3Collection != null && !isBudgetDetailEmpty)
                                {
                                    isBudgetDetailActive = complexType.BudgetLevel3Collection.Any(x => x.gid == innerCurrent.BudgetDetail.Id && x.isActive);

                                    if (!isBudgetDetailActive)
                                    {
                                        errors.Add(ValidationOption.Create(
                                                    modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].ActivitiesBudgetDetailsRefCollection[" + j + "].BudgetDetail.Id",
                                                    Global.ShortTemplateBudgetDetailCanceled,
                                                    string.Format(Global.ViewTemplateBudgetDetailCanceled, innerCurrent.BudgetDetail.Name, Global.SectionAgreementsWithContractors),
                                                    true, true));

                                        innerCurrent.IsBudgetDetailValid = false;
                                    }
                                }
                            }
                        }

                        #region AttachedDocumentDataCollection

                        if (current.AttachedDocumentCollection != null && current.AttachedDocumentCollection.Count > 0)
                        {
                            for (int j = 0; j < current.AttachedDocumentCollection.Count; j++)
                            {
                                var document = current.AttachedDocumentCollection[j];

                                if (document.AttachedDocumentContent == null)
                                    document.AttachedDocumentContent = new R_09992.AttachedDocumentContent();
                                document.AttachedDocumentContent.IsDocumentValid = true;

                                if (document.AttachedDocumentContent == null ||
                                    string.IsNullOrWhiteSpace(document.AttachedDocumentContent.BlobContentId) ||
                                    string.IsNullOrWhiteSpace(document.AttachedDocumentContent.FileName) ||
                                    string.IsNullOrWhiteSpace(document.AttachedDocumentContent.Size))
                                {
                                    errors.Add(ValidationOption.Create(
                                        modelPath + ".ContractContractors.ContractContractorCollection[" + i + "].AttachedDocumentCollection[" + j +
                                        "].AttachedDocumentContent.BlobContentId",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.SystemName,
                                            Global.SectionAgreementsWithContractors), true, true));

                                    if (document.AttachedDocumentContent != null)
                                    {
                                        document.AttachedDocumentContent.IsDocumentValid = false;
                                    }
                                }
                            }
                        }

                        #endregion

                        #endregion

                        if (!current.WithoutProcurementPlan)
                        {
                            var contractIsUsedInDifferentiatedPositions = complexType.ProcurementPlans.ProcurementPlanCollection.Where(
                                                                            x => x.DifferentiatedPositionCollection.Where(z => z.ContractContractor?.Id == current.gid).Any()).Any();
                            if (!contractIsUsedInDifferentiatedPositions)
                            {
                                errors.Add(ValidationOption.Create(
                                                        modelPath + ".ContractContractors.ContractContractorCollection[" + i + "]",
                                                        string.Empty,
                                                        string.Format(Global.ViewTemplateNotUsedContract, current.NomenclatureName),
                                                        false, true));
                                complexType.ContractContractors.IsValid = false;
                            }
                        }
                    }
                    complexType.ContractContractors.IsValid = true;
                }
                else
                {
                    errors.Add(ValidationOption.Create(
                        modelPath + ".ContractContractors.ContractContractorCollection",
                        string.Format(Global.ShortTemplateMaxContractContractors, Constants.ContractContractorsMaxCount),
                        string.Format(Global.ViewTemplateMaxContractContractors, Constants.ContractContractorsMaxCount,
                            Global.SectionAgreementsWithContractors),
                        true, true));

                    complexType.ContractContractors.IsValid = false;
                }
            }

            #endregion

            #region ProcurementPlans

            if (complexType.ProcurementPlans != null && complexType.ProcurementPlans.ProcurementPlanCollection != null && complexType.ProcurementPlans.ProcurementPlanCollection.Count > 0 && !complexType.ProcurementPlans.isLocked)
            {
                if (complexType.ProcurementPlans.ProcurementPlanCollection.Count <= Constants.ProcurementPlansMaxCount)
                {
                    for (int i = 0; i < complexType.ProcurementPlans.ProcurementPlanCollection.Count; i++)
                    {
                        var current = complexType.ProcurementPlans.ProcurementPlanCollection[i];

                        #region BFPContractPlan

                        var currentBFP = complexType.ProcurementPlans.ProcurementPlanCollection[i].BFPContractPlan;

                        currentBFP.IsNameValid = true;
                        currentBFP.IsErrandAreaValid = true;
                        currentBFP.IsErrandLegalActValid = true;
                        currentBFP.IsErrandTypeValid = true;
                        currentBFP.IsAmountValid = true;
                        currentBFP.IsPlanDateValid = true;
                        currentBFP.IsDescriptionValid = true;

                        if (string.IsNullOrWhiteSpace(currentBFP.Name))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".ProcurementPlans.ProcurementPlanCollection[" + i + "].BFPContractPlan.Name",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.ProcedureSubject, Global.SectionProcurementPlans), true, true));

                            currentBFP.IsNameValid = false;
                        }
                        else if (currentBFP.Name.Length > Constants.ProjectErrandNameLength)
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".ProcurementPlans.ProcurementPlanCollection[" + i + "].BFPContractPlan.Name",
                                                    string.Format(Global.ShortTemplateSymbolsMax, Constants.ProjectErrandNameLength),
                                                    string.Format(Global.ViewTemplateSymbolsMax, Global.ProcedureSubject, Global.SectionProcurementPlans, Constants.ProjectErrandNameLength), true, true));

                            currentBFP.IsNameValid = false;
                        }

                        if (currentBFP.ErrandArea == null || string.IsNullOrWhiteSpace(currentBFP.ErrandArea.Code) || string.IsNullOrWhiteSpace(currentBFP.ErrandArea.Name))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".ProcurementPlans.ProcurementPlanCollection[" + i + "].BFPContractPlan.ErrandArea.Code",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.ProcedureObject, Global.SectionProcurementPlans), true, true));

                            currentBFP.IsErrandAreaValid = false;
                        }

                        if (currentBFP.ErrandLegalAct == null || string.IsNullOrWhiteSpace(currentBFP.ErrandLegalAct.Id) || string.IsNullOrWhiteSpace(currentBFP.ErrandLegalAct.Name))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".ProcurementPlans.ProcurementPlanCollection[" + i + "].BFPContractPlan.ErrandLegalAct.id",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.ApplicableLegalAct, Global.SectionProcurementPlans), true, true));

                            currentBFP.IsErrandLegalActValid = false;
                        }

                        if (currentBFP.ErrandType == null || string.IsNullOrWhiteSpace(currentBFP.ErrandType.Code) || string.IsNullOrWhiteSpace(currentBFP.ErrandType.Name))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".ProcurementPlans.ProcurementPlanCollection[" + i + "].BFPContractPlan.ErrandType.Code",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.ProcedureType, Global.SectionProcurementPlans), true, true));

                            currentBFP.IsErrandTypeValid = false;
                        }

                        if (string.IsNullOrWhiteSpace(currentBFP.Description))
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".ProcurementPlans.ProcurementPlanCollection[" + i + "].BFPContractPlan.Description",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.ProjectErrandDescription, Global.SectionProcurementPlans), true, true));

                            currentBFP.IsDescriptionValid = false;
                        }
                        else if (currentBFP.Description.Length > Constants.ProjectErrandDescriptionLength)
                        {
                            errors.Add(ValidationOption.Create(
                                                    modelPath + ".ProcurementPlans.ProcurementPlanCollection[" + i + "].BFPContractPlan.Description",
                                                    string.Format(Global.ShortTemplateSymbolsMax, Constants.ProjectErrandDescriptionLength),
                                                    string.Format(Global.ViewTemplateSymbolsMax, Global.ProjectErrandDescription, Global.SectionProcurementPlans, Constants.ProjectErrandDescriptionLength), true, true));

                            currentBFP.IsDescriptionValid = false;
                        }

                        #endregion

                        current.IsNameValid = true;
                        current.IsAreaValid = true;
                        current.IsLegalActValid = true;
                        current.IsProcedureTypeValid = true;
                        current.IsMAPreliminaryControlValid = true;
                        current.IsPPAPreliminaryControlValid = true;
                        current.IsInternetAddressValid = true;
                        current.IsExpectedAmountValid = true;
                        current.IsNoticeDateValid = true;
                        current.IsOffersDeadlineDateValid = true;
                        current.IsDifferentiatedPositionCountValid = true;
                        current.IsPublicAttachedDocumentCountValid = true;

                        if (current.ExpectedAmount < 0)
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".ProcurementPlans.ProcurementPlanCollection[" + i + "].ExpectedAmount",
                                                Global.ShortTemplateNonNegativeNumber,
                                                string.Format(Global.ViewTemplateNonNegativeNumber, Global.ExpectedAmount, Global.SectionProcurementPlans),
                                                true, true));

                            current.IsExpectedAmountValid = false;
                        }

                        if (!current.OffersDeadlineDate.HasValue)
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".ProcurementPlans.ProcurementPlanCollection[" + i + "].OffersDeadlineDate",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.OffersDeadlineDate, Global.SectionProcurementPlans),
                                                true, true));

                            current.IsOffersDeadlineDateValid = false;
                        }

                        if (current.DifferentiatedPositionCollection != null && current.DifferentiatedPositionCollection.Count > 0)
                        {
                            for (int j = 0; j < current.DifferentiatedPositionCollection.Count; j++)
                            {
                                var position = current.DifferentiatedPositionCollection[j];

                                position.IsNameValid = true;
                                position.IsSubmittedOffersCountValid = true;
                                position.IsRankedOffersCountValid = true;
                                position.IsCommentValid = true;

                                if (string.IsNullOrWhiteSpace(position.Name))
                                {
                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".ProcurementPlans.ProcurementPlanCollection[" + i + "].DifferentiatedPositionCollection[" + j + "].Name",
                                                        Global.ShortTemplateRequired,
                                                        string.Format(Global.ViewTemplateRequired, Global.DifferentiatedPositionName, Global.SectionProcurementPlans),
                                                        true, true));

                                    position.IsNameValid = false;
                                }

                                if (!string.IsNullOrWhiteSpace(position.SubmittedOffersCount))
                                {
                                    int t;

                                    if (!int.TryParse(position.SubmittedOffersCount, out t))
                                    {
                                        errors.Add(ValidationOption.Create(
                                                            modelPath + ".ProcurementPlans.ProcurementPlanCollection[" + i + "].DifferentiatedPositionCollection[" + j + "].SubmittedOffersCount",
                                                            Global.ShortTemplateInteger,
                                                            string.Format(Global.ViewTemplateInteger, Global.SubmittedOffersCount, Global.SectionProcurementPlans), true, true));

                                        position.IsSubmittedOffersCountValid = false;
                                    }
                                }
                                // else
                                // {
                                //     errors.Add(ValidationOption.Create(
                                //                             modelPath + ".ProcurementPlans.ProcurementPlanCollection[" + i + "].DifferentiatedPositionCollection[" + j + "].SubmittedOffersCount",
                                //                             Global.ShortTemplateRequired,
                                //                             string.Format(Global.ViewTemplateRequired, Global.SubmittedOffersCount, Global.SectionProcurementPlans), true, true));
                                // 
                                //     position.IsSubmittedOffersCountValid = false;
                                // }

                                if (!string.IsNullOrWhiteSpace(position.RankedOffersCount))
                                {
                                    int t;

                                    if (!int.TryParse(position.RankedOffersCount, out t))
                                    {
                                        errors.Add(ValidationOption.Create(
                                                            modelPath + ".ProcurementPlans.ProcurementPlanCollection[" + i + "].DifferentiatedPositionCollection[" + j + "].RankedOffersCount",
                                                            Global.ShortTemplateInteger,
                                                            string.Format(Global.ViewTemplateInteger, Global.RankedOffersCount, Global.SectionProcurementPlans), true, true));

                                        position.IsRankedOffersCountValid = false;
                                    }
                                }
                                // else
                                // {
                                //     errors.Add(ValidationOption.Create(
                                //                             modelPath + ".ProcurementPlans.ProcurementPlanCollection[" + i + "].DifferentiatedPositionCollection[" + j + "].RankedOffersCount",
                                //                             Global.ShortTemplateRequired,
                                //                             string.Format(Global.ViewTemplateRequired, Global.RankedOffersCount, Global.SectionProcurementPlans), true, true));
                                // 
                                //     position.IsRankedOffersCountValid = false;
                                // }

                                // if (string.IsNullOrWhiteSpace(position.Comment))
                                // {
                                //     errors.Add(ValidationOption.Create(
                                //                         modelPath + ".ProcurementPlans.ProcurementPlanCollection[" + i + "].DifferentiatedPositionCollection[" + j + "].Comment",
                                //                         Global.ShortTemplateRequired,
                                //                         string.Format(Global.ViewTemplateRequired, Global.DifferentiatedPositionComment, Global.SectionProcurementPlans),
                                //                         true, true));
                                // 
                                //     position.IsCommentValid = false;
                                // }
                            }
                        }

                        #region AttachedDocumentDataCollection

                        if (current.AttachedDocumentCollection != null && current.AttachedDocumentCollection.Count > 0)
                        {
                            var hasProcurementDocumentTypes = complexType.Nomenclatures[NomenclatureType.ContractReportDocumentType].Any();

                            for (int j = 0; j < current.AttachedDocumentCollection.Count; j++)
                            {
                                var document = current.AttachedDocumentCollection[j];

                                if (hasProcurementDocumentTypes && document.VersionNum == complexType.orderNum &&
                                    (document.Type == null || string.IsNullOrWhiteSpace(document.Type.Id) || string.IsNullOrWhiteSpace(document.Type.Name)))
                                {
                                    errors.Add(ValidationOption.Create(
                                        modelPath + ".ProcurementPlans.ProcurementPlanCollection[" + i + "].AttachedDocumentCollection[" + j +
                                        "].Type.id",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequiredWithSubsection, Global.DocumentType,
                                            Global.SectionProcurementPlans, Global.AttachedDocuments), true, true));

                                    document.IsTypeValid = false;
                                }
                                else
                                {
                                    document.IsTypeValid = true;
                                }

                                string[] extensions = null;

                                if (document.Type != null && !string.IsNullOrWhiteSpace(document.Type.Id) &&
                                    complexType.DocumentsExtensions != null &&
                                    complexType.DocumentsExtensions.ContainsKey(document.Type.Id))
                                {
                                    extensions =
                                        complexType.DocumentsExtensions[document.Type.Id].Split(
                                            new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

                                    extensions = extensions.Select(x => x.ToLower()).ToArray();
                                }


                                if (document.AttachedDocumentContent == null)
                                    document.AttachedDocumentContent = new R_09992.AttachedDocumentContent();
                                document.AttachedDocumentContent.IsDocumentValid = true;

                                if (document.AttachedDocumentContent == null ||
                                    string.IsNullOrWhiteSpace(document.AttachedDocumentContent.BlobContentId) ||
                                    string.IsNullOrWhiteSpace(document.AttachedDocumentContent.FileName))
                                {
                                    errors.Add(ValidationOption.Create(
                                        modelPath + ".ProcurementPlans.ProcurementPlanCollection[" + i + "].AttachedDocumentCollection[" + j +
                                        "].AttachedDocumentContent.BlobContentId",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.SystemName,
                                            Global.SectionProcurementPlans), true, true));

                                    if (document.AttachedDocumentContent != null)
                                    {
                                        document.AttachedDocumentContent.IsDocumentValid = false;
                                    }
                                }
                                else if (extensions != null && extensions.Length > 0)
                                {
                                    var hasValidExtension =
                                        extensions.Any(ext => document.AttachedDocumentContent.FileName.EndsWith(ext));

                                    if (!hasValidExtension)
                                    {
                                        string validExtensions = string.Join(", ", extensions);
                                        string viewErr;

                                        if (document.IsTypeValid)
                                        {
                                            viewErr = string.Format(Global.ViewTemplateNamedDocumentExtensionWithSubsection,
                                                document.Type.Name, Global.SectionProcurementPlans, Global.AttachedDocuments, validExtensions);
                                        }
                                        else
                                        {
                                            viewErr = string.Format(Global.ViewTemplateDocumentExtensionWithSubsection,
                                                Global.SectionProcurementPlans, Global.AttachedDocuments, validExtensions);
                                        }

                                        errors.Add(ValidationOption.Create(
                                            modelPath + ".AttachedDocuments.AttachedDocumentCollection[" + i +
                                            "].AttachedDocumentContent.BlobContentId",
                                            string.Format(Global.ShortTemplateDocumentExtension, validExtensions),
                                            viewErr, true, true));

                                        document.AttachedDocumentContent.IsDocumentValid = false;
                                    }
                                }
                            }
                        }

                        if (current.AttachedDocumentCollection != null && complexType.RequiredDocumentsCodesNames != null && complexType.RequiredDocumentsCodesNames.Count > 0)
                        {
                            foreach (var docCodeName in complexType.RequiredDocumentsCodesNames)
                            {
                                bool contains = current.AttachedDocumentCollection.Any(d => d != null && d.Type != null && d.Type.Id == docCodeName.Item1);

                                if (!contains)
                                {
                                    errors.Add(ValidationOption.Create(
                                                        docCodeName.Item1,
                                                        string.Empty,
                                                        string.Format(Global.ViewTemplateRequiredDocumentsWithSubsection, docCodeName.Item2, Global.SectionProcurementPlans, Global.AttachedDocuments), true, true));
                                }
                            }
                        }

                        #endregion

                        if (current.IsAnnounced && !current.AnnouncedDate.HasValue && current.BFPContractPlan.ErrandLegalAct.Id == Constants.ProcurementPlansErrandLegalActPmsGid)
                        {
                            if (current.OffersDeadlineDate <= DateTime.Now.Date)
                            {
                                errors.Add(ValidationOption.Create(
                                                modelPath + ".ProcurementPlans.ProcurementPlanCollection[" + i + "].OffersDeadlineDate",
                                                string.Empty,
                                                string.Format(Global.ViewTemplateDateLaterThanCurrent, Global.OffersDeadlineDate, Global.SectionProcurementPlans),
                                                true, true));
                                current.IsOffersDeadlineDateValid = false;
                            }

                            if (current.DifferentiatedPositionCollection == null || current.DifferentiatedPositionCollection.Count == 0)
                            {
                                errors.Add(ValidationOption.Create(
                                                modelPath + ".ProcurementPlans.ProcurementPlanCollection[" + i + "].DifferentiatedPositionCollection",
                                                Global.ShortTemplateAtLeastOneRow,
                                                string.Format(Global.ViewTemplateAtLeastOneRow, Global.SectionDifferentiatedPositions),
                                                true, true));
                                current.IsDifferentiatedPositionCountValid = false;
                            }
                        }
                    }
                    complexType.ProcurementPlans.IsValid = true;
                }
                else
                {
                    errors.Add(ValidationOption.Create(
                        modelPath + ".ProcurementPlans.ProcurementPlanCollection",
                        string.Format(Global.ShortTemplateMaxProcurementPlans, Constants.ProcurementPlansMaxCount),
                        string.Format(Global.ViewTemplateMaxProcurementPlans, Constants.ProcurementPlansMaxCount,
                            Global.SectionProcurementPlans),
                        true, true));

                    complexType.ProcurementPlans.IsValid = false;
                }
            }

            #endregion
        }
    }
}
