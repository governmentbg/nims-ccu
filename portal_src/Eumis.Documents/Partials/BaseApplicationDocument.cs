using Eumis.Documents.Contracts;
using Eumis.Documents.Enums;
using Eumis.Documents.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Eumis.Documents.Partials
{
    [Serializable]
    public abstract class BaseApplicationDocument
    {
        [XmlIgnore]
        public List<ApplicationSection> ApplicationSections { get; set; }


        public void LoadNomenclature(IList<ContractProcedureApplicationSection> applicationSections)
        {
            this.ApplicationSections = applicationSections.Select(x => new ApplicationSection(x)).ToList();
        }

        public bool IsApplicationSectionSelected(ApplicationSectionType section)
        {
            var currentSection = this.ApplicationSections.FirstOrDefault(x => x.Section.Name.Equals(section.Name));
            if (currentSection != null)
            {
                return currentSection.IsSelected;
            }

            return false;
        }
    }
}
