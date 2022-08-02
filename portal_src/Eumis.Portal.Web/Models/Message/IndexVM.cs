using Eumis.Documents.Contracts;

namespace Eumis.Portal.Web.Models.Message
{
    public class IndexVM
    {
        public bool? HasFinishedAnswer { get; set; }

        public ContractMessage Messages { get; set; }
    }
}
