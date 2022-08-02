using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;
using Eumis.Documents.Enums;
using Eumis.Documents.Mappers;

namespace Eumis.Documents.Validation.Eumis
{
    public class FinanceReport : CSValidatorBase<R_10043.FinanceReport>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10043.FinanceReport complexType, string modelPath, IList<ValidationOption> errors)
        {
            #region FinanceReportBasicData

            if (complexType.BasicData != null)
            {
                EngineValidate(csValidationEngine, complexType.BasicData, modelPath + ".BasicData", errors);
            }
            else
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".BasicData",
                                        string.Empty,
                                        string.Format(Global.ViewTemplateMissingSection, Global.SectionGeneralInformation), true, true));
            }

            #endregion

            #region CostSupportingDocuments

            if (complexType.CostSupportingDocuments == null || complexType.CostSupportingDocuments.CostSupportingDocumentCollection == null
                || complexType.CostSupportingDocuments.CostSupportingDocumentCollection.Count == 0)
            {
                errors.Add(ValidationOption.Create(
                            modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection",
                            Global.ShortTemplateAtLeastOneRow,
                            string.Format(Global.ViewTemplateAtLeastOneRow, Global.SectionCostSupportingDocuments),
                            true, true));

                complexType.CostSupportingDocuments.IsValid = false;
            }
            else
            {
                if (complexType.CostSupportingDocuments != null && complexType.CostSupportingDocuments.CostSupportingDocumentCollection != null && complexType.CostSupportingDocuments.CostSupportingDocumentCollection.Count > 0)
                {
                    for (int i = 0; i < complexType.CostSupportingDocuments.CostSupportingDocumentCollection.Count; i++)
                    {
                        var current = complexType.CostSupportingDocuments.CostSupportingDocumentCollection[i];

                        string publisherCSD = null;

                        if (current.CompanyType == R_09986.CompanyTypeNomenclature.Beneficiary)
                        {
                            publisherCSD = current.Beneficiary?.displayName;
                        }
                        else if (current.CompanyType == R_09986.CompanyTypeNomenclature.Partner)
                        {
                            publisherCSD = current.Partner?.displayName;
                        }
                        else if (current.CompanyType == R_09986.CompanyTypeNomenclature.Contractor)
                        {
                            publisherCSD = current.Contractor?.displayName;
                        }

                        string errorRowIdentifier = string.Format(Global.CostSupportingDocumentCommonError, current.Number, current.Date?.ToString("dd.MM.yyyy"), publisherCSD);

                        current.IsTypeValid = true;
                        current.IsDescriptionValid = true;
                        current.IsNumberValid = true;
                        current.IsDateValid = true;
                        current.IsPaymentDateValid = true;
                        current.IsPartnerValid = true;
                        current.IsContractorValid = true;
                        current.IsContractContractorValid = true;
                        // current.AttachedDocument.IsDescriptionValid = true;

                        if (current.CostSupportingDocumentType == null || string.IsNullOrWhiteSpace(current.CostSupportingDocumentType.Value) || string.IsNullOrWhiteSpace(current.CostSupportingDocumentType.Description))
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].CostSupportingDocumentType.Value",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.CostSupportType, Global.SectionCostSupportingDocuments), 
                                                true, 
                                                true,
                                                errorRowIdentifier));

                            current.IsTypeValid = false;
                        }

                        //if (string.IsNullOrWhiteSpace(current.CostSupportingDocumentDescription))
                        //{
                        //    errors.Add(ValidationOption.Create(
                        //                        modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].CostSupportingDocumentDescription",
                        //                        Global.ShortTemplateRequired,
                        //                        string.Format(Global.ViewTemplateRequired, Global.CostSupportDescription, Global.SectionCostSupportingDocuments), true, true));

                        //    current.IsDescriptionValid = false;
                        //}

                        if (string.IsNullOrWhiteSpace(current.Number))
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].Number",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.CostSupportNumber, Global.SectionCostSupportingDocuments),
                                                true,
                                                true,
                                                errorRowIdentifier));

                            current.IsNumberValid = false;
                        }

                        if (!current.Date.HasValue)
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].Date",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.CostSupportDate, Global.SectionCostSupportingDocuments),
                                                true,
                                                true,
                                                errorRowIdentifier));

                            current.IsDateValid = false;
                        }

                        if (!current.PaymentDate.HasValue)
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].PaymentDate",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.CostSupportPaymentDate, Global.SectionCostSupportingDocuments),
                                                true,
                                                true,
                                                errorRowIdentifier));

                            current.IsPaymentDateValid = false;
                        }

                        if (current.CompanyType == R_09986.CompanyTypeNomenclature.Beneficiary)
                        {

                        }
                        else if (current.CompanyType == R_09986.CompanyTypeNomenclature.Partner)
                        {
                            if (current.Partner == null || string.IsNullOrWhiteSpace(current.Partner.Id) || string.IsNullOrWhiteSpace(current.Partner.Name))
                            {
                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].Partner.Id",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.Partner, Global.SectionCostSupportingDocuments),
                                                    true,
                                                    true,
                                                    errorRowIdentifier));

                                current.IsPartnerValid = false;
                            }
                            else
                            {
                                var isPartnerValid = complexType.PartnerItemCollection.Any(x => x.Id == current.Partner.Id);

                                if (!isPartnerValid)
                                {
                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].Partner.Id",
                                                        Global.ShortTemplateInvalid,
                                                        string.Format(Global.ViewTemplateInvalid, Global.Partner, Global.SectionCostSupportingDocuments),
                                                        true,
                                                        true,
                                                        errorRowIdentifier));

                                    current.IsPartnerValid = false;
                                }
                            }
                        }
                        else if (current.CompanyType == R_09986.CompanyTypeNomenclature.Contractor)
                        {
                            if (current.Contractor == null || string.IsNullOrWhiteSpace(current.Contractor.Id) || string.IsNullOrWhiteSpace(current.Contractor.Name))
                            {
                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].Contractor.Id",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.Contractor, Global.SectionCostSupportingDocuments),
                                                    true,
                                                    true,
                                                    errorRowIdentifier));

                                current.IsContractorValid = false;
                            }
                            else
                            {
                                var isContractorValid = complexType.ContractorItemCollection.Any(x => x.Id == current.Contractor.Id);

                                if (!isContractorValid)
                                {
                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].Contractor.Id",
                                                        Global.ShortTemplateInvalid,
                                                        string.Format(Global.ViewTemplateInvalid, Global.Contractor, Global.SectionCostSupportingDocuments),
                                                        true,
                                                        true,
                                                        errorRowIdentifier));

                                    current.IsContractorValid = false;
                                }
                            }

                            if (current.ContractContractor != null && !string.IsNullOrWhiteSpace(current.ContractContractor.Id) && !string.IsNullOrWhiteSpace(current.ContractContractor.Name))
                            {
                                var isContractContractorValid = complexType.ContractContractorItems.Any(x => x.Key == current.Contractor.Id && x.Value.Any(y => y.Id == current.ContractContractor.Id));

                                if (!isContractContractorValid)
                                {
                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].ContractContractor.Id",
                                                        Global.ShortTemplateInvalid,
                                                        string.Format(Global.ViewTemplateInvalid, Global.CostSupportContractContractor, Global.SectionCostSupportingDocuments),
                                                        true,
                                                        true,
                                                        errorRowIdentifier));

                                    current.IsContractContractorValid = false;
                                }
                            }
                        }

                        if (this.IsCostSupportingDocumentValid(current))
                        {
                            var currentCSDNumber = current.Number;
                            var currentCSDDate = current.Date;
                            var currentCSDCompanyId = this.GetCSDCompanyData(current).Item1;

                            List<Tuple<string, string, decimal>> currentCSDBudgetItems = current
                                .FinanceReportBudgetItemDataCollection.Select(bi => new Tuple<string, string, decimal>(
                                    item1: bi.BudgetDetail.Id,
                                    item2: this.GetContractActivity(bi.ContractActivity),
                                    item3: bi.TotalAmount
                                ))
                                .ToList();

                            for (int j = i + 1; j < complexType.CostSupportingDocuments.CostSupportingDocumentCollection.Count; j++)
                            {
                                var csd = complexType.CostSupportingDocuments.CostSupportingDocumentCollection[j];

                                if (this.IsCostSupportingDocumentValid(csd) && (current.IsLocked == false || csd.IsLocked == false))
                                {
                                    if (csd.Number == currentCSDNumber && csd.Date == currentCSDDate && this.GetCSDCompanyData(csd).Item1 == currentCSDCompanyId)
                                    {
                                        var csdBudgetItems = complexType.CostSupportingDocuments.CostSupportingDocumentCollection[j].FinanceReportBudgetItemDataCollection;

                                        foreach (var budgetItem in csdBudgetItems)
                                        {
                                            if (currentCSDBudgetItems.Any(bi =>
                                                bi.Item1 == budgetItem.BudgetDetail.Id &&
                                                bi.Item2 == GetContractActivity(budgetItem.ContractActivity) &&
                                                bi.Item3 == budgetItem.TotalAmount))
                                            {
                                                errors.Add(ValidationOption.Create(
                                                    modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "]",
                                                    Global.ShortBudgetItemsDuplicated,
                                                    string.Format(Global.BudgetItemsDuplicated, currentCSDNumber, currentCSDDate?.ToString("dd.MM.yyyy"), this.GetCSDCompanyData(current).Item2, budgetItem.BudgetDetail.Name, budgetItem.ContractActivity?.Name, budgetItem.TotalAmount), true, true));
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        #region FinanceReportBudgetItemDataCollection

                        if (current.FinanceReportBudgetItemDataCollection != null && current.FinanceReportBudgetItemDataCollection.Count > 0)
                        {
                            for (int j = 0; j < current.FinanceReportBudgetItemDataCollection.Count; j++)
                            {
                                var innerCurrent = current.FinanceReportBudgetItemDataCollection[j];

                                innerCurrent.IsBudgetDetailValid = true;
                                innerCurrent.IsContractActivityValid = true;
                                innerCurrent.IsGrandAmountValid = true;
                                innerCurrent.IsSelfAmountValid = true;
                                innerCurrent.IsCrossFinancingValid = true;
                                innerCurrent.IsTotalAmountValid = true;
                                // innerCurrent.IsUnitDefinitionValid = true;
                                innerCurrent.IsProducedUnitsCountValid = true;
                                innerCurrent.IsUnitCostValid = true;

                                if (innerCurrent.SelfAmount > 0 && innerCurrent.AdvancePayment.id == YesNoNotApplicableNomenclature.Yes.Id)
                                {
                                    errors.Add(ValidationOption.Create(
                                                    modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].FinanceReportBudgetItemDataCollection[" + j + "].SelfAmount",
                                                    String.Format(Global.ShortTemplateIntegerMax, 0),
                                                    String.Format(Global.AdvancePaymentWithSelfAmountNotAllowed, Global.SectionCostSupportingDocuments),
                                                    true,
                                                    true,
                                                    errorRowIdentifier));
                                    innerCurrent.IsSelfAmountValid = false;
                                }

                                var isBudgetDetailEmpty = innerCurrent.BudgetDetail == null || string.IsNullOrWhiteSpace(innerCurrent.BudgetDetail.Id) || string.IsNullOrWhiteSpace(innerCurrent.BudgetDetail.Name);

                                if (isBudgetDetailEmpty)
                                {
                                    errors.Add(ValidationOption.Create(
                                                    modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].FinanceReportBudgetItemDataCollection[" + j + "].BudgetDetail.Id",
                                                    Global.ShortTemplateRequired,
                                                    string.Format(Global.ViewTemplateRequired, Global.BudgetDetail, Global.SectionCostSupportingDocuments),
                                                    true,
                                                    true,
                                                    errorRowIdentifier));

                                    innerCurrent.IsBudgetDetailValid = false;
                                }
                                else
                                {
                                    var budgetDetailValid = current.ContractContractor == null || string.IsNullOrWhiteSpace(current.ContractContractor.Id) || complexType.ActivityBudgetDetailItems.Any(x => x.Key == current.ContractContractor.Id && x.Value.Any(y => y.BudgetDetail.Id == innerCurrent.BudgetDetail.Id));

                                    if (!budgetDetailValid)
                                    {
                                        errors.Add(ValidationOption.Create(
                                                                modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].FinanceReportBudgetItemDataCollection[" + j + "].BudgetDetail.Id",
                                                                Global.ShortTemplateInvalid,
                                                                string.Format(Global.ShortTemplateInvalid, Global.BudgetDetail, Global.SectionCostSupportingDocuments),
                                                                true, true));

                                        innerCurrent.IsBudgetDetailValid = false;
                                    }
                                }

                                var isContractActivityEmpty = innerCurrent.ContractActivity == null || string.IsNullOrWhiteSpace(innerCurrent.ContractActivity.Id) || string.IsNullOrWhiteSpace(innerCurrent.ContractActivity.Name);

                                if (isContractActivityEmpty)
                                {
                                    // errors.Add(ValidationOption.Create(
                                    //                 modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].FinanceReportBudgetItemDataCollection[" + j + "].ContractActivity.Id",
                                    //                 Global.ShortTemplateRequired,
                                    //                 string.Format(Global.ViewTemplateRequired, Global.ContractActivity, Global.SectionCostSupportingDocuments),
                                    //                 true, true));
                                    //
                                    // innerCurrent.IsContractActivityValid = false;
                                }
                                else
                                {
                                    var contractActivityValid = current.ContractContractor == null || string.IsNullOrWhiteSpace(current.ContractContractor.Id) || complexType.ActivityBudgetDetailItems.Any(x => x.Key == current.ContractContractor.Id && x.Value.Any(y => y.ContractActivity == null || y.ContractActivity.Id == innerCurrent.ContractActivity.Id));

                                    if (!contractActivityValid)
                                    {
                                        errors.Add(ValidationOption.Create(
                                                                modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].FinanceReportBudgetItemDataCollection[" + j + "].ContractActivity.Id",
                                                                Global.ShortTemplateInvalid,
                                                                string.Format(Global.ShortTemplateInvalid, Global.ContractActivity, Global.SectionCostSupportingDocuments),
                                                                true, true));

                                        innerCurrent.IsContractActivityValid = false;
                                    }
                                }

                                if (innerCurrent.GrandAmount < 0)
                                {
                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].FinanceReportBudgetItemDataCollection[" + j + "].GrandAmount",
                                                        Global.ShortTemplateNonNegativeNumber,
                                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.CostSupportGrandAmount, Global.SectionCostSupportingDocuments),
                                                        true,
                                                        true,
                                                        errorRowIdentifier));

                                    innerCurrent.IsGrandAmountValid = false;
                                }

                                if (innerCurrent.SelfAmount < 0)
                                {
                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].FinanceReportBudgetItemDataCollection[" + j + "].SelfAmount",
                                                        Global.ShortTemplateNonNegativeNumber,
                                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.CostSupportSelfAmount, Global.SectionCostSupportingDocuments),
                                                        true,
                                                        true,
                                                        errorRowIdentifier));

                                    innerCurrent.IsSelfAmountValid = false;
                                }

                                if (innerCurrent.CrossFinancing == null || string.IsNullOrWhiteSpace(innerCurrent.CrossFinancing.Value) && string.IsNullOrWhiteSpace(innerCurrent.CrossFinancing.Description))
                                {
                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].FinanceReportBudgetItemDataCollection[" + j + "].CrossFinancing",
                                                        Global.ShortTemplateNonNegativeNumber,
                                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.CostSupportCrossFinancing, Global.SectionCostSupportingDocuments),
                                                        true,
                                                        true,
                                                        errorRowIdentifier));

                                    innerCurrent.IsCrossFinancingValid = false;
                                }

                                if (innerCurrent.TotalAmount < 0)
                                {
                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].FinanceReportBudgetItemDataCollection[" + j + "].TotalAmount",
                                                        Global.ShortTemplateNonNegativeNumber,
                                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.CostSupportTotalAmount, Global.SectionCostSupportingDocuments),
                                                        true,
                                                        true,
                                                        errorRowIdentifier));

                                    innerCurrent.IsTotalAmountValid = false;
                                }

                                //if (string.IsNullOrWhiteSpace(innerCurrent.UnitDefinition))
                                //{
                                //    errors.Add(ValidationOption.Create(
                                //                        modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].FinanceReportBudgetItemDataCollection[" + j + "].UnitDefinition",
                                //                        Global.ShortTemplateRequired,
                                //                        string.Format(Global.ViewTemplateRequired, Global.CostSupportUnitDefinition, Global.SectionCostSupportingDocuments),
                                //                        true, true));

                                //    innerCurrent.IsUnitDefinitionValid = false;
                                //}

                                if (!string.IsNullOrWhiteSpace(innerCurrent.ProducedUnitsCount))
                                {
                                    int t;

                                    if (!int.TryParse(innerCurrent.ProducedUnitsCount, out t))
                                    {
                                        errors.Add(ValidationOption.Create(
                                                            modelPath + ".ProducedUnitsCount",
                                                            Global.ShortTemplateInteger,
                                                            string.Format(Global.ViewTemplateInteger, Global.CostSupportProducedUnitsCount, Global.SectionCostSupportingDocuments),
                                                            true,
                                                            true,
                                                            errorRowIdentifier));

                                        innerCurrent.IsProducedUnitsCountValid = false;
                                    }
                                }
                                //else
                                //{
                                //    errors.Add(ValidationOption.Create(
                                //                        modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].FinanceReportBudgetItemDataCollection[" + j + "].ProducedUnitsCount",
                                //                        Global.ShortTemplateRequired,
                                //                        string.Format(Global.ViewTemplateRequired, Global.CostSupportProducedUnitsCount, Global.SectionCostSupportingDocuments),
                                //                        true, true));

                                //    innerCurrent.IsProducedUnitsCountValid = false;
                                //}

                                if (innerCurrent.UnitCost < 0)
                                {
                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].FinanceReportBudgetItemDataCollection[" + j + "].UnitCost",
                                                        Global.ShortTemplateNonNegativeNumber,
                                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.CostSupportUnitCost, Global.SectionCostSupportingDocuments),
                                                        true,
                                                        true,
                                                        errorRowIdentifier));

                                    innerCurrent.IsUnitCostValid = false;
                                }
                            }
                        }

                        #endregion

                        #region AttachedDocumentDataCollection

                        if (current.AttachedDocumentCollection != null && current.AttachedDocumentCollection.Count > 0)
                        {
                            var hasFinancialReportDocumentTypes = complexType.Nomenclatures[NomenclatureType.ContractReportDocumentType].Any();

                            for (int j = 0; j < current.AttachedDocumentCollection.Count; j++)
                            {
                                var document = current.AttachedDocumentCollection[j];

                                var isCurrentVersion = document.VersionNum == complexType.docNumber && document.VersionSubNum == complexType.docSubNumber;

                                if (hasFinancialReportDocumentTypes && isCurrentVersion &&
                                    (document.Type == null || string.IsNullOrWhiteSpace(document.Type.Id) || string.IsNullOrWhiteSpace(document.Type.Name)))
                                {
                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].AttachedDocumentCollection[" + j +
                                        "].Type.id",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.DocumentType, Global.SectionCostSupportingDocuments),
                                        true,
                                        true,
                                        errorRowIdentifier));

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
                                    string.IsNullOrWhiteSpace(document.AttachedDocumentContent.FileName) ||
                                    string.IsNullOrWhiteSpace(document.AttachedDocumentContent.Size))
                                {
                                    errors.Add(ValidationOption.Create(
                                        modelPath + ".CostSupportingDocuments.CostSupportingDocumentCollection[" + i + "].AttachedDocumentCollection[" + j +
                                        "].AttachedDocumentContent.BlobContentId",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.SystemName, Global.SectionCostSupportingDocuments),
                                        true,
                                        true,
                                        errorRowIdentifier));

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
                                            viewErr = string.Format(Global.ViewTemplateNamedDocumentExtension,
                                                document.Type.Name, Global.SectionCostSupportingDocuments, validExtensions);
                                        }
                                        else
                                        {
                                            viewErr = string.Format(Global.ViewTemplateDocumentExtension,
                                                Global.SectionCostSupportingDocuments, validExtensions);
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

                        if (!current.IsLocked && current.AttachedDocumentCollection != null && complexType.RequiredDocumentsCodesNames != null && complexType.RequiredDocumentsCodesNames.Count > 0)
                        {
                            foreach (var docCodeName in complexType.RequiredDocumentsCodesNames)
                            {
                                bool contains = current.AttachedDocumentCollection.Any(d => d != null && d.Type != null && d.Type.Id == docCodeName.Item1);


                                if (!contains)
                                {
                                    errors.Add(ValidationOption.Create(
                                                        docCodeName.Item1,
                                                        string.Empty,
                                                        string.Format(Global.ViewTemplateRequiredDocument, docCodeName.Item2, Global.SectionCostSupportingDocuments), true, true));
                                }

                            }
                        }

                        #endregion
                    }

                    complexType.CostSupportingDocuments.IsValid = true;
                }
            }

            #endregion

        }

        private Tuple<string, string> GetCSDCompanyData(R_10066.CostSupportingDocument costSupportingDocument)
        {
            Tuple<string, string> companyData = null;

            switch (costSupportingDocument.CompanyType)
            {
                case R_09986.CompanyTypeNomenclature.Beneficiary:
                    companyData =  new Tuple<string, string>(costSupportingDocument.Beneficiary?.Id, costSupportingDocument.Beneficiary?.Name);
                    break;
                case R_09986.CompanyTypeNomenclature.Partner:
                    companyData = new Tuple<string, string>(costSupportingDocument.Partner?.Id, costSupportingDocument.Partner?.Name);
                    break;
                case R_09986.CompanyTypeNomenclature.Contractor:
                    companyData = new Tuple<string, string>(costSupportingDocument.Contractor?.Id, costSupportingDocument.Contractor?.Name);
                    break;
            }

            return companyData;
        }

        private string GetContractActivity(R_10000.PrivateNomenclature contractActivity)
        {
            return contractActivity != null ? (string.IsNullOrEmpty(contractActivity.Id) ? null : contractActivity.Id) : null;
        }

        private bool IsCostSupportingDocumentValid(R_10066.CostSupportingDocument costSupportingDocument)
        {
            return costSupportingDocument.Date.HasValue &&
               !string.IsNullOrWhiteSpace(costSupportingDocument.Number) &&
               costSupportingDocument.FinanceReportBudgetItemDataCollection != null &&
               costSupportingDocument.FinanceReportBudgetItemDataCollection.Count > 0 &&
               costSupportingDocument.FinanceReportBudgetItemDataCollection.All(bi => bi.BudgetDetail != null && bi.BudgetDetail.Id != null);
        }
    }
}
