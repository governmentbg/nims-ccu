using System.Collections.Generic;

namespace Eumis.Common.Validation
{
    /// <summary>
    /// ValidatorProvider-а филтрира всички грешки, които не отговарят на поне един FilterRegex от някой ErrorFilter
    /// За тези който отговарят на някой regex, ModelPath-а се трансформира чрез string.Format(PathFormat, regex groups values)
    /// от пътвия отговарящ ErrorFilter
    /// </summary>
    public class ErrorFilter
    {
        public ErrorFilter(string filterRegex)
        {
            FilterRegex = filterRegex;
            PathFormat = null;
            SplitEnd = false;
        }

        public ErrorFilter(string filterRegex, string pathFormat)
        {
            FilterRegex = filterRegex;
            PathFormat = pathFormat;
            SplitEnd = false;
        }

        public ErrorFilter(string filterRegex, string pathFormat, bool splitEnd = false)
        {
            FilterRegex = filterRegex;
            PathFormat = pathFormat;
            SplitEnd = splitEnd;
        }

        public string FilterRegex { get; private set; }
        public string PathFormat { get; private set; }
        public bool SplitEnd { get; private set; }
    }

    /// <summary>
    /// Клас съдържащ обект, който да бъде валидиран,
    /// както и данни, които да бъдат валидирани
    /// </summary>
    public class ValidatableObject
    {
        /// <summary>
        /// Обект, който да бъде валидиран
        /// </summary>
        public object Object { get; set; }

        /// <summary>
        /// Списък с филтри за грешките, виж <see cref="ErrorFilter"/>
        /// Ако стойността е null грешките не се филтрират
        /// </summary>
        public IEnumerable<ErrorFilter> ErrorFilters { get; set; }
    }

    /// <summary>
    /// Интерфейс на валидатора
    /// </summary>
    public interface IEngineValidatable
    {
        /// <summary>
        /// Извлича обектите който ще се валидират
        /// </summary>
        /// <returns>масив от обектите който ще се валидират</returns>
        IEnumerable<ValidatableObject> GetValidatableObjects();
    }
}