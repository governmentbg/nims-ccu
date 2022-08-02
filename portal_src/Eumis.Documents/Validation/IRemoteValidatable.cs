using Eumis.Documents.Contracts;
using System.Collections.Generic;
namespace Eumis.Documents.Validation
{
    public interface IRemoteValidatable
    {
        List<string> RemoteValidationErrors { get; set; }
        List<string> RemoteValidationWarnings { get; set; } 
    }
}