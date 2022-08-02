using System;
using System.Collections.Generic;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.Directions;

namespace Eumis.Domain.Procedures.Json
{
    public class ProcedureVersionJson
    {
        public static readonly int CURRENT_JSON_VERSION = 1;

        public ProcedureVersionJson()
        {
            this.AppGuidelines = new List<ProcedureAppGuidlineJson>();
            this.AppDocs = new List<ProcedureAppDocJson>();
            this.SpecFields = new List<ProcedureSpecFieldJson>();
            this.Programmes = new List<ProcedureProgrammeJson>();
            this.Locations = new List<ProcedureLocationJson>();
            this.ApplicationSections = new List<ProcedureApplicationSectionJson>();
            this.Directions = new List<DirectionPairJson>();
            this.Declarations = new List<ProcedureDeclarationJson>();
        }

        public ProcedureVersionJson(
            string name,
            string nameAlt,
            string code,
            string description,
            string descriptionAlt,
            ApplicationFormType applicationFormType,
            ProcedureKind procedureKind,
            int? year,
            int? projectDuration,
            int? questionId,
            Guid? qaBlobKey,
            string qaFileName,
            DateTime? qaModifyDate,
            IList<ProcedureAppGuidlineJson> appGuidelines,
            IList<ProcedureAppDocJson> appDocs,
            IList<ProcedureSpecFieldJson> specFields,
            IList<ProcedureProgrammeJson> programmes,
            IList<ProcedureLocationJson> locations,
            IList<ProcedureApplicationSectionJson> applicationSections,
            IList<DirectionPairJson> directions,
            IList<ProcedureDeclarationJson> declarations)
        {
            this.Name = name;
            this.NameAlt = nameAlt;
            this.Code = code;
            this.Description = description;
            this.DescriptionAlt = descriptionAlt;
            this.ApplicationFormType = applicationFormType;
            this.ProcedureKind = procedureKind;
            this.Year = year;
            this.ProjectDuration = projectDuration;

            this.QuestionId = questionId;
            this.QaBlobKey = qaBlobKey;
            this.QaFileName = qaFileName;
            this.QaModifyDate = qaModifyDate;

            this.AppGuidelines = appGuidelines;
            this.AppDocs = appDocs;
            this.SpecFields = specFields;
            this.Programmes = programmes;
            this.Locations = locations;
            this.ApplicationSections = applicationSections;
            this.Directions = directions;
            this.Declarations = declarations;

            this.Version = ProcedureVersionJson.CURRENT_JSON_VERSION;
        }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string DescriptionAlt { get; set; }

        public ApplicationFormType ApplicationFormType { get; set; }

        public ProcedureKind ProcedureKind { get; set; }

        public int? Year { get; set; }

        public AllowedRegistrationType AllowedRegistrationType => AllowedRegistrationType.Digital;

        public int? ProjectDuration { get; set; }

        public int? QuestionId { get; set; }

        public Guid? QaBlobKey { get; set; }

        public string QaFileName { get; set; }

        public DateTime? QaModifyDate { get; set; }

        // Deprecated property for obsolete json version
        public NutsLevel NutsLevel { get; set; }

        // Deprecated property for obsolete json version
        public string LocationFullPath { get; set; }

        public ProcedureStatus Status { get; set; }

        public IList<ProcedureAppGuidlineJson> AppGuidelines { get; set; }

        public IList<ProcedureAppDocJson> AppDocs { get; set; }

        public IList<ProcedureSpecFieldJson> SpecFields { get; set; }

        public IList<ProcedureProgrammeJson> Programmes { get; set; }

        public IList<ProcedureLocationJson> Locations { get; set; }

        public IList<ProcedureApplicationSectionJson> ApplicationSections { get; set; }

        public IList<DirectionPairJson> Directions { get; set; }

        public IList<ProcedureDeclarationJson> Declarations { get; set; }

        public int? Version { get; set; }
    }
}
