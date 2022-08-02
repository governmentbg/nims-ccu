using System;

namespace Eumis.Documents.Contracts
{
    public class ContractProject
    {
        public string xml { get; set; }

        //true possible only with user token, should allow edit if true
        public bool isDraft { get; set; }
        public ContractProjectHeader regData { get; set; }
        public DateTime createDate { get; set; }
        public byte[] version { get; set; }
    }
}