using System;

namespace Eumis.Documents.Contracts
{
    public class ContractReportDocument
    {
        public string gid { get; set; }
        public string name { get; set; }
        public bool isRequired { get; set; }
        public bool isActive { get; set; }
        public string extension { get; set; }
    }
}
