using System.Collections.Generic;

namespace Eumis.Common.Validation
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Интерфейсът е поставен в това assembly за да може да се използва
    /// едновременно от Components и RioObjects
    /// </remarks>
    public interface ICSValidator
    {
        string TypeFullName { get; }

        void Validate(
            ICSValidationEngine csValidationEngine,          
            object complexType,
            string modelPath,
            IList<ValidationOption> errors);
    }
}
