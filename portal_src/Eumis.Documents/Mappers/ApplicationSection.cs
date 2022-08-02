using Eumis.Common.Extensions;
using Eumis.Documents.Contracts;
using Eumis.Documents.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Mappers
{
    [Serializable]
    public class ApplicationSection
    {
        public ApplicationSection()
        {
        }

        public ApplicationSection(ContractProcedureApplicationSection item)
            : this()
        {
            this.IsSelected = item.isSelected;
            this.Name = item.section?.description;
            this.NameAlt = item.section?.descriptionAlt;

            this.OrderNum = item.orderNum;
            this.Section = ApplicationSectionType.GetItem(item.section);
        }
        public ApplicationSectionType Section { get; set; }

        public bool IsSelected { get; set; }

        public int OrderNum { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }
    }
}
