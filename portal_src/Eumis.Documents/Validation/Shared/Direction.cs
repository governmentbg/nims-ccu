using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Validation.Shared
{
    public class Direction : CSValidatorBase<R_10093.Direction>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10093.Direction complexType, string modelPath, IList<ValidationOption> errors)
        {
            complexType.IsDirectionValid = true;
            if (complexType.DirectionItem == null || string.IsNullOrWhiteSpace(complexType.DirectionItem.Id))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Id",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.DirectionName, Global.SectionDirection), true, true));
                complexType.IsDirectionValid = false;
            }
        }
    }
}
