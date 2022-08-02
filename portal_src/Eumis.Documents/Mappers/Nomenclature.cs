using Eumis.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eumis.Documents.Mappers
{
    [Serializable]
    public class Nomenclature
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public string NameAlt { get; set; }
        public string ParentId { get; set; }
        public bool? IsRequired { get; set; }
        public bool? IsSignatureRequired { get; set; }

        public Nomenclature() { }

        public Nomenclature(string value, string name, string parentId = null)
        {
            this.Value = value;
            this.Name = name;
            this.ParentId = parentId;
        }

        public Nomenclature(Eumis.Documents.Contracts.ContractPrivateNomenclature nomenclature)
        {
            this.Value = nomenclature.gid;
            this.Name = nomenclature.name;
            this.NameAlt = nomenclature.nameAlt;
        }

        public Nomenclature(Eumis.Documents.Contracts.ContractPublicNomenclature nomenclature)
        {
            this.Value = nomenclature.code;
            this.Name = nomenclature.name;
            this.NameAlt = nomenclature.nameAlt;
        }

        public Nomenclature(Eumis.Documents.Contracts.ContractApplicationDoc nomenclature)
        {
            this.Value = nomenclature.gid;
            this.Name = nomenclature.name;
            this.IsRequired = nomenclature.isRequired;
            this.IsSignatureRequired = nomenclature.isSignatureRequired;
        }

        public Nomenclature(Eumis.Documents.Contracts.ContractReportDocument nomenclature)
        {
            this.Value = nomenclature.gid;
            this.Name = nomenclature.name;
            this.IsRequired = nomenclature.isRequired;
        }

        public Nomenclature(SerializableSelectListItem item)
        {
            this.Value = item.Value;
            this.Name = item.Text;
        }
    }
}
