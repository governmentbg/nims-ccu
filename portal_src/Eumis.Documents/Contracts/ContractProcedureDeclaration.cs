using Eumis.Common.Localization;
using R_10098;
using System;
using System.Collections.Generic;

namespace Eumis.Documents.Contracts
{
    public class ContractProcedureDeclaration
    {
        public Guid gid { get; set; }

        public int orderNum { get; set; }

        public string name { get; set; }

        public string nameAlt { get; set; }

        public string content { get; set; }

        public string contentAlt { get; set; }

        public FieldType fieldType { get; set; }

        public bool isRequired { get; set; }

        public bool isActive { get; set; }

        public string displayName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.nameAlt))
                {
                    return this.name;
                }

                return SystemLocalization.GetDisplayName(this.name, this.nameAlt);
            }
        }

        public string displayContent
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.contentAlt))
                {
                    return this.content;
                }

                return SystemLocalization.GetDisplayName(this.content, this.contentAlt);
            }
        }

        public IList<ContractProcedureDeclarationItem> items { get; set; }
    }
}
