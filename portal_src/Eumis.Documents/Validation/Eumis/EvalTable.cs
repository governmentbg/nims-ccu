using System;
using System.Collections.Generic;
using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;

namespace Eumis.Documents.Validation.Eumis
{
    public class EvalTable : CSValidatorBase<R_10023.EvalTable>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10023.EvalTable complexType, string modelPath, IList<ValidationOption> errors)
        {
            var type = complexType.type;

            if (complexType.EvalTableGroupCollection != null && complexType.EvalTableGroupCollection.Count > 0)
            {
                for (int i = 0; i < complexType.EvalTableGroupCollection.Count; i++)
                {
                    var currentGroup = complexType.EvalTableGroupCollection[i];

                    if (string.IsNullOrWhiteSpace(currentGroup.Name))
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".EvalTableGroupsWrapper.EvalTableGroupCollection[" + i + "].Name",
                                                Global.ShortTemplateRequired,
                                                string.Format(Global.ViewTemplateRequired, Global.Group, Global.SectionCriterias), true, true));

                        currentGroup.IsNameValid = false;
                    }
                    else
                        currentGroup.IsNameValid = true;

                    if (type == R_09993.EvalTypeNomenclature.Weight)
                    {
                        if (currentGroup.Limit < 0)
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".EvalTableGroupsWrapper.EvalTableGroupCollection[" + i + "].Limit",
                                                Global.ShortTemplateNonNegativeNumber,
                                                string.Format(Global.ViewTemplateNonNegativeNumber, Global.Limit, Global.SectionCriterias),
                                                true, true));

                            currentGroup.IsLimitValid = false;
                        }
                        else if (currentGroup.Limit > currentGroup.WeightDecimal)
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".EvalTableGroupsWrapper.EvalTableGroupCollection[" + i + "].Limit",
                                                Global.ShortTemplateInvalid,
                                                string.Format(Global.ViewTemplateSmallerEqualNumber, Global.Limit, Global.Weight),
                                                true, true));

                            currentGroup.IsLimitValid = false;
                        }
                        else if (currentGroup.Limit > complexType.Limit)
                        {
                            errors.Add(ValidationOption.Create(
                                                modelPath + ".EvalTableGroupsWrapper.EvalTableGroupCollection[" + i + "].Limit",
                                                Global.ShortTemplateInvalid,
                                                string.Format(Global.ViewTemplateSmallerEqualNumber, Global.Limit + " " + Global.Group, Global.Limit + " " + Global.Total),
                                                true, true));

                            currentGroup.IsLimitValid = false;
                        }
                        else
                            currentGroup.IsLimitValid = true;
                    }

                    if (currentGroup.EvalTableCriteriaCollection != null && currentGroup.EvalTableCriteriaCollection.Count > 0)
                    {
                        for (int j = 0; j < currentGroup.EvalTableCriteriaCollection.Count; j++)
                        {
                            var currentCriteria = currentGroup.EvalTableCriteriaCollection[j];

                            if (string.IsNullOrWhiteSpace(currentCriteria.Name))
                            {
                                errors.Add(ValidationOption.Create(
                                                        modelPath + ".EvalTableGroupsWrapper.EvalTableGroupCollection[" + i + "].EvalTableCriteriaCollection[" + j + "].Name",
                                                        Global.ShortTemplateRequired,
                                                        string.Format(Global.ViewTemplateRequired, Global.Criteria, Global.SectionCriterias), true, true));

                                currentCriteria.IsNameValid = false;
                            }
                            else
                                currentCriteria.IsNameValid = true;

                            if (type == R_09993.EvalTypeNomenclature.Weight)
                            {
                                if (currentCriteria.Weight < 0)
                                {
                                    errors.Add(ValidationOption.Create(
                                                        modelPath + ".EvalTableGroupsWrapper.EvalTableGroupCollection[" + i + "].EvalTableCriteriaCollection[" + j + "].Weight",
                                                        Global.ShortTemplateNonNegativeNumber,
                                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.Weight, Global.SectionCriterias),
                                                        true, true));

                                    currentCriteria.IsWeightValid = false;
                                }
                                else
                                    currentCriteria.IsWeightValid = true;
                            }
                        }
                    }
                    else
                    {
                        errors.Add(ValidationOption.Create(
                                                modelPath + ".EvalTableGroupsWrapper.EvalTableGroupCollection[" + i + "]",
                                                Global.ShortTemplateAtLeastOneRow,
                                                string.Format(Global.ViewTemplateAtLeastOneCriteria, Global.SectionCriterias),
                                                true, true));

                        currentGroup.HasCriterias = false;
                    }
                }
            }
            else
            {
                errors.Add(ValidationOption.Create(
                       modelPath + ".EvalTableGroupsWrapper.EvalTableGroupCollection",
                       Global.ShortTemplateAtLeastOneRow,
                       string.Format(Global.ViewTemplateAtLeastOneRow, Global.SectionCriterias),
                       true, true));
            }

            if (type == R_09993.EvalTypeNomenclature.Weight)
            {
                if (complexType.Limit < 0)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".EvalTableGroupsWrapper.Limit",
                                        Global.ShortTemplateNonNegativeNumber,
                                        string.Format(Global.ViewTemplateNonNegativeNumber, Global.MinimumLimit, Global.SectionCriterias),
                                        true, true));

                    complexType.IsLimitValid = false;
                }
                else if (complexType.Limit > complexType.WeightDecimal)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".EvalTableGroupsWrapper.Limit",
                                        Global.ShortTemplateInvalid,
                                        string.Format(Global.ViewTemplateSmallerEqualNumber, Global.Limit, Global.Weight),
                                        true, true));

                    complexType.IsLimitValid = false;
                }
                else
                    complexType.IsLimitValid = true;
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
                                                string.Format(Global.ViewTemplateRequired, Global.Description, Global.SectionInstructions), true, true));

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
