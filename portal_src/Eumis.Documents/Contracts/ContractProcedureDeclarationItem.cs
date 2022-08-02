using System;

namespace Eumis.Documents.Contracts
{
    [Serializable]
    public class ContractProcedureDeclarationItem
    {
        public Guid? gid { get; set; }

        public int orderNum { get; set; }

        public string content { get; set; }

        public bool isActive { get; set; }

        public string id
        {
            get { return this.gid.ToString(); }
        }

        public string text
        {
            get { return this.content; }
        }
    }
}
