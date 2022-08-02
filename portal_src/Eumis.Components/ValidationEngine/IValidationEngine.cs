using Eumis.Common.Validation;
using Eumis.Documents.Interfaces;
using System.Collections.Generic;

namespace Eumis.Components.ValidationEngine
{
    /// <summary>
    /// Интерфейс на компонента извършващ javascript валидацията
    /// </summary>
    public interface IValidationEngine
    {
        /// <summary>
        /// Валидира обект представляващ заявление
        /// </summary>
        /// <param name="appRioCode">код на заявлението</param>
        /// <param name="complexType">обект</param>
        /// <param name="complexTypePath">expression път до обекта</param>
        /// <param name="fullValidation">валидно при прикачен XML документ</param>
        /// <returns>списък от грешки</returns>
        IDictionary<string, IEnumerable<ValidationOption>> Validate(string code, object document, object complexType, string complexTypePath);
    }
}
