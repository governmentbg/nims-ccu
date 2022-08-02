using System;
using System.Collections.Generic;
using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;

namespace Eumis.Documents.Validation.Shared
{
    public class PaperAttachedDocument : CSValidatorBase<R_09994.PaperAttachedDocument>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_09994.PaperAttachedDocument complexType, string modelPath, IList<ValidationOption> errors)
        {
            if (complexType.Type == null || string.IsNullOrWhiteSpace(complexType.Type.Id) || string.IsNullOrWhiteSpace(complexType.Type.Name))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Type.id",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.Type, Global.SectionPaperAttachedDocuments), true, true));

                complexType.IsTypeValid = false;
            }
            else
                complexType.IsTypeValid = true;

            if (string.IsNullOrWhiteSpace(complexType.Description))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Description",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.Description, Global.SectionPaperAttachedDocuments), true, true));

                complexType.IsDescriptionValid = false;
            }
            else
                complexType.IsDescriptionValid = true;
        }
    }
}