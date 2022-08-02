using Eumis.Documents.Contracts;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Enums
{
    [Serializable]
    public class ApplicationSectionType
    {
        public ApplicationSectionType()
        {

        }

        public string Name { get; set; }

        public int Value { get; set; }

        public static readonly ApplicationSectionType BasicData = new ApplicationSectionType { Value = 1, Name = "basicData" };
        public static readonly ApplicationSectionType Beneficary = new ApplicationSectionType { Value = 2, Name = "beneficary" };
        public static readonly ApplicationSectionType Partners = new ApplicationSectionType { Value = 3, Name = "partners" };
        public static readonly ApplicationSectionType Directions = new ApplicationSectionType { Value = 4, Name = "directions" };
        public static readonly ApplicationSectionType Budget = new ApplicationSectionType { Value = 5, Name = "budget" };
        public static readonly ApplicationSectionType SummaryData = new ApplicationSectionType { Value = 6, Name = "summaryData" };
        public static readonly ApplicationSectionType Activities = new ApplicationSectionType { Value = 7, Name = "activities" };
        public static readonly ApplicationSectionType Indicators = new ApplicationSectionType { Value = 8, Name = "indicators" };
        public static readonly ApplicationSectionType Team = new ApplicationSectionType { Value = 9, Name = "team" };
        public static readonly ApplicationSectionType ProcurementPlan = new ApplicationSectionType { Value = 10, Name = "procurementPlan" };
        public static readonly ApplicationSectionType AdditionalInformation = new ApplicationSectionType { Value = 11, Name = "additionalInformation" };
        public static readonly ApplicationSectionType AttachedDocuments = new ApplicationSectionType { Value = 12, Name = "attachedDocuments" };
        public static readonly ApplicationSectionType Programme = new ApplicationSectionType { Value = 13, Name = "programme" };
        public static readonly ApplicationSectionType ProgrammePriority = new ApplicationSectionType { Value = 14, Name = "programmePriority" };
        public static readonly ApplicationSectionType ElectronicDeclarations = new ApplicationSectionType { Value = 15, Name = "electronicDeclarations" };

        private static IList<ApplicationSectionType> GetItems()
        {
            return new List<ApplicationSectionType>()
            {
                BasicData,
                Beneficary,
                Partners,
                Directions,
                Budget,
                SummaryData,
                Activities,
                Indicators,
                Team,
                ProcurementPlan,
                AdditionalInformation,
                AttachedDocuments,
                Programme,
                ProgrammePriority,
                ElectronicDeclarations,
            };
        }

        public static ApplicationSectionType GetItem(ContractEnumNomenclature section)
        {
            if (section == null)
            {
                throw new NullReferenceException($"Parameter {nameof(section)} must contains value");
            }
            
            var item = ApplicationSectionType.GetItems().FirstOrDefault(t => t.Name.ToLower().Equals(section.value.ToLower()));

            if (item == null)
            {
                throw new ArgumentOutOfRangeException(nameof(section.value), $"Application section type with name '{section.value}' not found");
            }

            return item;
        }
    }
}
