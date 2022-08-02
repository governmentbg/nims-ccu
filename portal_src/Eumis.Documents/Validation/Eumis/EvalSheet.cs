using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;
using Eumis.Documents.Enums;

namespace Eumis.Documents.Validation.Eumis
{
    public class EvalSheet : CSValidatorBase<R_10026.EvalSheet>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10026.EvalSheet complexType, string modelPath, IList<ValidationOption> errors)
        {
            var type = complexType.type;
            decimal totalCalc = 0.00m;
            List<bool> groupResults = new List<bool>();

            if (complexType.EvalSheetGroupCollection != null && complexType.EvalSheetGroupCollection.Count > 0)
            {
                for (int i = 0; i < complexType.EvalSheetGroupCollection.Count; i++)
                {
                    var currentGroup = complexType.EvalSheetGroupCollection[i];

                    if (type == R_09993.EvalTypeNomenclature.Weight)
                    {
                        bool hasGroupTotal = false;
                        decimal groupTotal = currentGroup.Total;
                        decimal groupTotalCalc = 0.00m;

                        if (complexType.EvalSheetGroupCollection[i].Total < 0)
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".EvalSheetGroupCollection[" + i + "].Total",
                                                Global.ShortTemplateNonNegativeNumber,
                                                string.Format(Global.ViewTemplateNonNegativeNumber, Global.Total, Global.SectionCriterias),
                                                true, true));

                            complexType.EvalSheetGroupCollection[i].IsTotalValid = false;
                        }
                        else
                        {
                            complexType.EvalSheetGroupCollection[i].IsTotalValid = true;
                            hasGroupTotal = true;
                        }

                        if (complexType.EvalSheetGroupCollection[i].EvalSheetCriteriaCollection != null && complexType.EvalSheetGroupCollection[i].EvalSheetCriteriaCollection.Count > 0)
                        {
                            for (int j = 0; j < complexType.EvalSheetGroupCollection[i].EvalSheetCriteriaCollection.Count; j++)
                            {
                                var currentCriteria = complexType.EvalSheetGroupCollection[i].EvalSheetCriteriaCollection[j];

                                if (currentCriteria.Evaluation < 0)
                                {
                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".EvalSheetGroupsWrapper.EvalSheetGroupCollection[" + i + "].EvalSheetCriteriaCollection[" + j + "].Evaluation",
                                                        Global.ShortTemplateNonNegativeNumber,
                                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Evaluation, Global.SectionCriterias),
                                                        true, true));

                                    complexType.EvalSheetGroupCollection[i].EvalSheetCriteriaCollection[j].IsCriteriaValid = false;
                                }
                                else if (hasGroupTotal)
                                {
                                    decimal currentWeight = 0.00m;

                                    if (currentCriteria.EvalTableCriteria != null)
                                    {
                                        currentWeight = currentCriteria.EvalTableCriteria.Weight;
                                    }

                                    decimal evaluationRounded = Math.Round(currentCriteria.Evaluation, 2);
                                    decimal currentWeightRounded = Math.Round(currentWeight, 2);

                                    if (currentWeightRounded < evaluationRounded)
                                    {
                                        errors.Add(ValidationOption.Create(
                                                        modelPath + ".EvalSheetGroupsWrapper.EvalSheetGroupCollection[" + i + "].EvalSheetCriteriaCollection[" + j + "].Evaluation",
                                                        Global.ShortTemplateWeightEvaluation,
                                                        string.Format(Global.ViewTemplateWeightEvaluation, Global.SectionCriterias),
                                                        true, true));

                                        complexType.EvalSheetGroupCollection[i].EvalSheetCriteriaCollection[j].IsCriteriaValid = false;
                                    }
                                    else
                                        complexType.EvalSheetGroupCollection[i].EvalSheetCriteriaCollection[j].IsCriteriaValid = true;

                                    groupTotalCalc += Math.Round(evaluationRounded, 2);
                                }
                            }
                        }

                        if (hasGroupTotal)
                        {
                            if (groupTotal != groupTotalCalc)
                            {
                                errors.Add(ValidationOption.Create(
                                                              modelPath + ".EvalSheetGroupsWrapper.EvalSheetGroupCollection[" + i + "].Total",
                                                              Global.ShortTemplateMatchGroupTotalOnly,
                                                              string.Format(Global.ViewTemplateMatchGroupTotalOnly, Global.Total, Global.SectionCriterias), true, true));

                                complexType.EvalSheetGroupCollection[i].IsTotalValid = false;
                            }
                            else
                            { 
                                complexType.EvalSheetGroupCollection[i].IsTotalValid = true;

                                groupResults.Add(complexType.EvalSheetGroupCollection[i].Total >= complexType.EvalSheetGroupCollection[i].Limit);
                            }

                            totalCalc += groupTotalCalc;
                        }
                    }
                    else if (type == R_09993.EvalTypeNomenclature.Rejection)
                    {
                        List<bool> criteriasResults = new List<bool>();

                        if (complexType.EvalSheetGroupCollection[i].EvalSheetCriteriaCollection != null && complexType.EvalSheetGroupCollection[i].EvalSheetCriteriaCollection.Count > 0)
                        {
                            bool groupPasses = false;

                            for (int j = 0; j < complexType.EvalSheetGroupCollection[i].EvalSheetCriteriaCollection.Count; j++)
                            {
                                bool isAccepted = false;
                                var currentCriteria = complexType.EvalSheetGroupCollection[i].EvalSheetCriteriaCollection[j];

                                if (currentCriteria.Accept == null || string.IsNullOrWhiteSpace(currentCriteria.Accept.Id) || string.IsNullOrWhiteSpace(currentCriteria.Accept.Name))
                                {
                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".EvalSheetGroupsWrapper.EvalSheetGroupCollection[" + i + "].EvalSheetCriteriaCollection[" + j + "].Accept.Id",
                                                        Global.ShortTemplateRequired,
                                                        string.Format(Global.ViewTemplateRequired, Global.Evaluation, Global.SectionCriterias),
                                                        true, true));

                                    complexType.EvalSheetGroupCollection[i].EvalSheetCriteriaCollection[j].IsCriteriaValid = false;
                                }
                                else
                                {
                                    complexType.EvalSheetGroupCollection[i].EvalSheetCriteriaCollection[j].IsCriteriaValid = true;
                                    var acceptType = currentCriteria.Accept.Id;
                                    isAccepted = acceptType != AcceptanceTypeNomenclature.No.Id;
                                }

                                bool leadsToRejection = currentCriteria.EvalTableCriteria.IsRejection;
                                bool criteriaPasses = !leadsToRejection || isAccepted;
                                criteriasResults.Add(criteriaPasses);
                            }

                            groupPasses = criteriasResults.All(item => item);
                            groupResults.Add(groupPasses);
                            criteriasResults.Clear();
                        }
                    }
                }
            }

            if (type == R_09993.EvalTypeNomenclature.Rejection)
            {
                bool computedResult = groupResults.All(item => item);
                bool actualResult = complexType.IsSuccess;

                if (!complexType.IsManual && computedResult != actualResult)
                {
                    errors.Add(ValidationOption.Create(
                                         modelPath + ".IsSuccess",
                                         Global.ShortTemplateEvaluation,
                                         string.Format(Global.ViewTemplateEvaluation, Global.SectionCriterias),
                                         true, true));
                }
            }
            else if (type == R_09993.EvalTypeNomenclature.Weight)
            {
                if (complexType.Total < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".EvalSheetGroupsWrapper.Total",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Total, Global.SectionCriterias),
                                        true, true));

                    complexType.IsTotalValid = false;
                }
                else
                {
                    decimal totalRounded = Math.Round(complexType.Total, 2);

                    if (totalRounded != totalCalc)
                    {
                        errors.Add(ValidationOption.Create(
                                                           modelPath + ".EvalSheetGroupsWrapper.Total",
                                                           string.Format(Global.ShortTemplateMatchTotalGroups, Global.Evaluation),
                                                           string.Format(Global.ViewTemplateMatchTotalGroups, Global.Total, Global.SectionCriterias), true, true));

                        complexType.IsTotalValid = false;
                    }
                    else
                    {
                        complexType.IsTotalValid = true;

                        bool computedResult = groupResults.All(item => item) && complexType.Total >= complexType.Limit;
                        bool actualResult = complexType.IsSuccess;

                        if (!complexType.IsManual && computedResult != actualResult)
                        {
                            errors.Add(ValidationOption.Create(
                                                 modelPath + ".IsSuccess",
                                                 Global.ShortTemplateEvaluation,
                                                 string.Format(Global.ViewTemplateEvaluation, Global.SectionCriterias),
                                                 true, true));
                        }
                    }
                }
            }

            if (complexType.AttachedDocumentCollection != null && complexType.AttachedDocumentCollection.Count > 0)
            {
                for (int i = 0; i < complexType.AttachedDocumentCollection.Count; i++)
                {
                    var current = complexType.AttachedDocumentCollection[i];

                    if (string.IsNullOrWhiteSpace(current.Description))
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".AttachedDocumentCollection[" + i + "].Description",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.Description, Global.SectionAttachedDocuments), true, true));

                        complexType.AttachedDocumentCollection[i].IsDescriptionValid = false;
                    }
                    else
                        complexType.AttachedDocumentCollection[i].IsDescriptionValid = true;

                    if (current.AttachedDocumentContent == null)
                        current.AttachedDocumentContent = new R_09992.AttachedDocumentContent();

                    if (current.AttachedDocumentContent == null || string.IsNullOrWhiteSpace(current.AttachedDocumentContent.BlobContentId) || string.IsNullOrWhiteSpace(current.AttachedDocumentContent.FileName))
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".AttachedDocumentCollection[" + i + "].AttachedDocumentContent.BlobContentId",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.SystemName, Global.SectionInstructions), true, true));

                        complexType.AttachedDocumentCollection[i].AttachedDocumentContent.IsDocumentValid = false;
                    }
                    else
                        complexType.AttachedDocumentCollection[i].AttachedDocumentContent.IsDocumentValid = true;
                }
            }

        }
    }
}
