using Eumis.Documents.Enums;
using Eumis.Documents.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eumis.Portal.Web.Helplers.Extensions
{
    public static class ApplicationSectionExtension
    {
        public static bool IsSectionSelected(this List<ApplicationSection> applicationSections, ApplicationSectionType section)
        {
            var currentSection = applicationSections.FirstOrDefault(x => x.Section.Name.Equals(section.Name));
            if (currentSection != null)
            {
                return currentSection.IsSelected;
            }

            return false;
        }
    }
}
