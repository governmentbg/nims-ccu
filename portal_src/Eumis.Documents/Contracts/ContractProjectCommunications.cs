using Eumis.Common.Localization;
using System.Collections.Generic;

namespace Eumis.Documents.Contracts
{
    public class ContractProjectCommunications
    {
        public Communications communications { get; set; }

        public string projectRegNumber { get; set; }
    }

    public class Communications
    {
        public List<MessagePVO> results { get; set; }

        public int count { get; set; }
    }
}
