using System;
using System.Collections.Generic;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.Procedures.Json
{
    public class ProcedureVersionJson
    {

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public ApplicationFormType ApplicationFormType { get; set; }

        public AllowedRegistrationType AllowedRegistrationType { get; set; }

        public int ProjectDuration { get; set; }

        public NutsLevel NutsLevel { get; set; }

        public string InternetAddress { get; set; }

        public string LocationFullPath { get; set; }

        public int? QuestionId { get; set; }

        public Guid? QaBlobKey { get; set; }

        public string QaFileName { get; set; }

        public DateTime? QaModifyDate { get; set; }

        public int TimeLimitId { get; set; }

        public DateTime TimeLimitsEndingDate { get; set; }

        public string TimeLimitsNotes { get; set; }

        public ProcedureCategoriesJson Categories { get; set; }

        public IList<ProcedureAppGuidlineJson> AppGuidelines { get; set; }

        public IList<ProcedureAppDocJson> AppDocs { get; set; }

        public IList<ProcedureSpecFieldJson> SpecFields { get; set; }

        public IList<ProcedureProgrammeJson> Programmes { get; set; }

        public int? Version { get; set; }
    }
}
