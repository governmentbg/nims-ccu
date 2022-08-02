using System.Collections.Generic;

namespace Eumis.Domain.Procedures
{
    public static class ProcedureApplicationSectionAdditionalSettingType
    {
        private static readonly Dictionary<(ApplicationSectionType applicationSectionType, string additionalSettingName), (string description, string info)> Map = new Dictionary<(ApplicationSectionType applicationSectionType, string additionalSettingName), (string description, string info)>
        {
            { (ApplicationSectionType.BasicData, "FillMainData"), ("Попълни от процедурата", "Полета Наименование на проекта, Наименование на проекта на английски език, Кратко описание на проекта, Кратко описание на проекта на английски език, Цел/и на проекта и Срок на изпълнение, месеци да се попълват от въведените в Процедурата и да са недостъпни за редакция от потребителя") },
        };

        public static (string description, string info)? GetType(ApplicationSectionType applicationSectionType, string additionalSettingName)
        {
            (string description, string info) value;

            if (Map.TryGetValue((applicationSectionType, additionalSettingName), out value))
            {
                return (value.description, value.info);
            }
            else
            {
                return null;
            }
        }
    }
}
