using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using Eumis.Domain.Procedures.Json;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class ProcedureAppDataPVO
    {
        public ProcedureAppDataPVO(ProcedureVersionJson procVersion, bool isActive, DateTime currentEndDate)
        {
            this.ProcedureName = procVersion.Name;
            this.ProcedureNameAlt = procVersion.NameAlt;
            this.ProcedureCode = procVersion.Code;
            this.ProcedureDescription = procVersion.Description;
            this.EndingDate = currentEndDate;
            this.AllowedRegistrationType = new EnumPVO<AllowedRegistrationType>
            {
                Description = procVersion.AllowedRegistrationType,
                Value = procVersion.AllowedRegistrationType,
            };
            this.ProcedureKind = new EnumPVO<ProcedureKind>
            {
                Description = procVersion.ProcedureKind,
                Value = procVersion.ProcedureKind,
            };
            this.ApplicationFormType = new EnumPVO<ApplicationFormType>
            {
                Description = procVersion.ApplicationFormType,
                Value = procVersion.ApplicationFormType,
            };
            this.Year = procVersion.Year;

            this.Programmes = new List<ProcedureProgrammePVO>();

            this.ProjectDuration = procVersion.ProjectDuration;
            this.IsActive = isActive;
            this.SpecFields = procVersion.SpecFields.Select(sf => new SpecFieldPVO(sf)).ToList();
            this.ApplicationDocs = procVersion.AppDocs.Select(p => new ApplicationDocPVO(p)).ToList();
            this.Programmes = procVersion.Programmes.Select(p => new ProcedureProgrammePVO(p)).ToList();
            this.Locations = procVersion.Locations.Select(p => new LocationPVO(p.NutsLevel, p.LocationFullPath)).ToList();
            this.ApplicationSections = procVersion.ApplicationSections.Select(p => new ProcedureApplicationSectionPVO(p)).ToList();
            this.Directions = procVersion.Directions.Select(d => new DirectionPairPVO(d)).ToList();
            this.Declarations = procVersion.Declarations.Select(p => new ProcedureDeclarationPVO(p)).ToList();

            if (procVersion.Locations.Count() == 0 && (int)procVersion.NutsLevel != 0)
            {
                // deprecated json versions without location lists
                this.Locations.Add(new LocationPVO(procVersion.NutsLevel, procVersion.LocationFullPath));
            }
        }

        public string ProcedureName { get; set; }

        public string ProcedureNameAlt { get; set; }

        public string ProcedureCode { get; set; }

        public DateTime EndingDate { get; set; }

        public EnumPVO<AllowedRegistrationType> AllowedRegistrationType { get; set; }

        public EnumPVO<ApplicationFormType> ApplicationFormType { get; set; }

        public EnumPVO<ProcedureKind> ProcedureKind { get; set; }

        public string InternetAddress { get; set; }

        public int? ProjectDuration { get; set; }

        public bool IsActive { get; set; }

        public int? Year { get; set; }

        public string ProcedureDescription { get; set; }

        public IList<ProcedureProgrammePVO> Programmes { get; set; }

        public IList<ApplicationDocPVO> ApplicationDocs { get; set; }

        public IList<SpecFieldPVO> SpecFields { get; set; }

        public IList<LocationPVO> Locations { get; set; }

        public IList<ProcedureApplicationSectionPVO> ApplicationSections { get; set; }

        public IList<DirectionPairPVO> Directions { get; set; }

        public IList<ProcedureDeclarationPVO> Declarations { get; set; }
    }
}
