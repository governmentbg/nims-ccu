using System.Collections.Generic;
namespace Eumis.Documents.Contracts
{
    public class ContractErrors
    {
        public List<string> errors { get; set; }

        public List<string> warnings { get; set; }
    }
}