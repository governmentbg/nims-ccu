using Eumis.Common.Validation;
using Eumis.Documents.Contracts;
using System.Collections.Generic;
namespace Eumis.Documents.Validation
{
    public interface ILocalValidatable
    {
        List<ModelValidationResultExtended> LocalValidationErrors { get; set; }
    }
}