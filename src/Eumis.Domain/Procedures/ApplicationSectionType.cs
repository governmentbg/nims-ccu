using Eumis.Common.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Procedures
{
    public enum ApplicationSectionType
    {
        [Description(Description = nameof(DomainEnumTexts.ApplicableSection_BasicData), ResourceType = typeof(DomainEnumTexts))]
        BasicData = 1,

        [Description(Description = nameof(DomainEnumTexts.ApplicableSection_Beneficary), ResourceType = typeof(DomainEnumTexts))]
        Beneficary = 2,

        [Description(Description = nameof(DomainEnumTexts.ApplicableSection_Partners), ResourceType = typeof(DomainEnumTexts))]
        Partners = 3,

        [Description(Description = nameof(DomainEnumTexts.ApplicableSection_Directions), ResourceType = typeof(DomainEnumTexts))]
        Directions = 4,

        [Description(Description = nameof(DomainEnumTexts.ApplicableSection_Budget), ResourceType = typeof(DomainEnumTexts))]
        Budget = 5,

        [Description(Description = nameof(DomainEnumTexts.ApplicableSection_SummaryData), ResourceType = typeof(DomainEnumTexts))]
        SummaryData = 6,

        [Description(Description = nameof(DomainEnumTexts.ApplicableSection_Activities), ResourceType = typeof(DomainEnumTexts))]
        Activities = 7,

        [Description(Description = nameof(DomainEnumTexts.ApplicableSection_Indicators), ResourceType = typeof(DomainEnumTexts))]
        Indicators = 8,

        [Description(Description = nameof(DomainEnumTexts.ApplicableSection_Team), ResourceType = typeof(DomainEnumTexts))]
        Team = 9,

        [Description(Description = nameof(DomainEnumTexts.ApplicableSection_ProcurementPlan), ResourceType = typeof(DomainEnumTexts))]
        ProcurementPlan = 10,

        [Description(Description = nameof(DomainEnumTexts.ApplicableSection_AdditionalInformation), ResourceType = typeof(DomainEnumTexts))]
        AdditionalInformation = 11,

        [Description(Description = nameof(DomainEnumTexts.ApplicableSection_AttachedDocuments), ResourceType = typeof(DomainEnumTexts))]
        AttachedDocuments = 12,

        [Description(Description = nameof(DomainEnumTexts.ApplicableSection_Programme), ResourceType = typeof(DomainEnumTexts))]
        Programme = 13,

        [Description(Description = nameof(DomainEnumTexts.ApplicableSection_ProgrammePriority), ResourceType = typeof(DomainEnumTexts))]
        ProgrammePriority = 14,

        [Description(Description = nameof(DomainEnumTexts.ApplicableSection_ElectronicDeclarations), ResourceType = typeof(DomainEnumTexts))]
        ElectronicDeclarations = 15,
    }
}
