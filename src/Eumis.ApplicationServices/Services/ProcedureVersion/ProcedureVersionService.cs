using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.Directions;
using Eumis.Domain.Procedures;
using Eumis.Domain.Procedures.Json;

namespace Eumis.ApplicationServices.Services.ProcedureVersion
{
    public class ProcedureVersionService : IProcedureVersionService
    {
        private IProceduresRepository proceduresRepository;
        private IProcedureVersionsRepository procedureVersionsRepository;
        private IEntityCodeNomsRepository<Country, EntityCodeNomVO> countryNomsRepository;
        private INuts1NomsRepository nuts1NomsRepository;
        private INuts2NomsRepository nuts2NomsRepository;
        private IDistrictNomsRepository districtNomsRepository;
        private IMunicipalityNomsRepository municipalityNomsRepository;
        private ISettlementNomsRepository settlementNomsRepository;
        private IProtectedZoneNomsRepository protectedZoneNomsRepository;
        private IProcedureAppFormDeclarationsRepository procedureAppFormDeclarationsRepository;

        public ProcedureVersionService(
            IProceduresRepository proceduresRepository,
            IProcedureVersionsRepository procedureVersionsRepository,
            IEntityCodeNomsRepository<Country, EntityCodeNomVO> countryNomsRepository,
            INuts1NomsRepository nuts1NomsRepository,
            INuts2NomsRepository nuts2NomsRepository,
            IDistrictNomsRepository districtNomsRepository,
            IMunicipalityNomsRepository municipalityNomsRepository,
            ISettlementNomsRepository settlementNomsRepository,
            IProtectedZoneNomsRepository protectedZoneNomsRepository,
            IProcedureAppFormDeclarationsRepository procedureAppFormDeclarationsRepository)
        {
            this.proceduresRepository = proceduresRepository;
            this.procedureVersionsRepository = procedureVersionsRepository;
            this.countryNomsRepository = countryNomsRepository;
            this.nuts1NomsRepository = nuts1NomsRepository;
            this.nuts2NomsRepository = nuts2NomsRepository;
            this.districtNomsRepository = districtNomsRepository;
            this.municipalityNomsRepository = municipalityNomsRepository;
            this.settlementNomsRepository = settlementNomsRepository;
            this.protectedZoneNomsRepository = protectedZoneNomsRepository;
            this.procedureAppFormDeclarationsRepository = procedureAppFormDeclarationsRepository;
        }

        public Eumis.Domain.Procedures.ProcedureVersion CreateProcedureVersion(int procedureId, bool? isActive = null)
        {
            var procedure = this.proceduresRepository.Find(procedureId);

            var prevVersion = this.procedureVersionsRepository.GetLastVersion(procedureId);
            if (prevVersion != null)
            {
                prevVersion.DeactivateVersion();
            }

            var lastQuestion = procedure.ProcedureQuestions
                .OrderByDescending(pq => pq.CreateDate)
                .FirstOrDefault();

            if (lastQuestion != null && lastQuestion.File == null)
            {
                // workaround for newly created ProcedureVersions
                // they have not been retrieved with a Find method and the File property is null
                this.proceduresRepository.LoadReference(lastQuestion, q => q.File);
            }

            Eumis.Domain.Procedures.ProcedureVersion version = new Eumis.Domain.Procedures.ProcedureVersion(
                prevVersion == null ? 1 : prevVersion.ProcedureVersionId + 1,
                procedure.ProcedureId,
                procedure.Gid,
                procedure.Name,
                procedure.NameAlt,
                procedure.Code,
                procedure.Description,
                procedure.DescriptionAlt,
                procedure.ApplicationFormType,
                procedure.ProcedureKind,
                procedure.Year,
                procedure.ProjectDuration,
                lastQuestion == null ? (int?)null : lastQuestion.ProcedureQuestionId,
                lastQuestion == null ? (Guid?)null : lastQuestion.BlobKey,
                lastQuestion == null ? null : lastQuestion.File.FileName,
                lastQuestion == null ? (DateTime?)null : lastQuestion.CreateDate,
                procedure.ProcedureApplicationGuidelines.Select(ag => new ProcedureAppGuidlineJson(ag)).ToList(),
                procedure.ProcedureApplicationDocs.Select(ad => new ProcedureAppDocJson(ad)).ToList(),
                procedure.ProcedureSpecFields.Select(sp => new ProcedureSpecFieldJson(sp)).ToList(),
                this.GetProgrammes(procedure.ProcedureId),
                procedure.ProcedureLocations.Select(pl => new ProcedureLocationJson(pl.NutsLevel, this.GetProcLocationFullPath(pl))).ToList(),
                this.GetProcedureApplicationSections(procedure.ProcedureId, procedure.ProcedureApplicationSections, procedure.ProcedureApplicationSectionAdditionalSetting),
                procedure.ProcedureDirections.Select(d => new DirectionPairJson(d)).ToList(),
                this.procedureAppFormDeclarationsRepository.GetDeclarationsForProcedureVersion(procedure.ProcedureId),
                isActive);

            this.procedureVersionsRepository.Add(version);

            return version;
        }

        private IList<ProcedureProgrammeJson> GetProgrammes(int procedureId)
        {
            var procedureTree = this.proceduresRepository.GetExpenseBudgetTree(procedureId);
            var indicators = this.proceduresRepository.GetProcedureIndicators(procedureId)
                .GroupBy(pi => pi.ProgrammeId)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(gi => new IndicatorJson
                    {
                        IndicatorId = gi.IndicatorId,
                        Gid = gi.Gid,
                        Name = gi.Name,
                        NameAlt = gi.NameAlt,
                        MeasureName = gi.MeasureName,
                        MeasureNameAlt = gi.MeasureNameAlt,
                        HasGenderDivision = gi.HasGenderDivision,
                        IsActive = gi.IsActive,
                    }).ToList());

            var shares = this.proceduresRepository.GetProcedureShares(procedureId);

            int primaryProgrammeId = shares.Single(s => s.IsPrimary).ProgrammeId;

            var programmePriorities = shares
                .GroupBy(s => s.ProgrammeId)
                .Select(g => new
                {
                    ProgrammeId = g.Key,
                    ProgrammePriorities = g.Select(gi =>
                        new
                        {
                            gi.ProgrammePriorityId,
                            gi.ProgrammePriorityGid,
                            gi.ProgrammePriorityCode,
                            gi.ProgrammePriorityName,
                            gi.ProgrammePriorityNameAlt,
                        })
                    .Distinct(),
                })
                .ToDictionary(
                    i => i.ProgrammeId,
                    i => i.ProgrammePriorities.Select(pp => new ProgrammePriorityJson
                    {
                        ProgrammePriorityId = pp.ProgrammePriorityId,
                        Gid = pp.ProgrammePriorityGid,
                        Code = pp.ProgrammePriorityCode,
                        Name = pp.ProgrammePriorityName,
                        NameAlt = pp.ProgrammePriorityNameAlt,
                    }).ToList());

            return procedureTree.Programmes
                    .OrderByDescending(p => p.ProgrammeId == primaryProgrammeId) // make the primary programme first
                    .Select(p => new ProcedureProgrammeJson
                    {
                        ProgrammeId = p.ProgrammeId,
                        ProgrammeName = p.DisplayName,
                        ProgrammeNameAlt = p.NameAlt,
                        ProgrammeCode = p.Code,
                        ProgrammePriorities = programmePriorities[p.ProgrammeId],
                        Indicators = indicators.ContainsKey(p.ProgrammeId) ? indicators[p.ProgrammeId] : new List<IndicatorJson>(),
                        BudgetExpenseTypes = p.Level1Items.Select(l1 => new BudgetExpenseTypeJson
                        {
                            BudgetLevel1Id = l1.ProcedureBudgetLevel1Id,
                            Gid = l1.Gid,
                            Name = l1.DisplayName,
                            NameAlt = l1.NameAlt,
                            IsActive = l1.IsActive,
                            Expenses = l1.Level2Items.Select(l2 => new BudgetExpenseJson
                            {
                                BudgetLevel2Id = l2.ProcedureBudgetLevel2Id,
                                Gid = l2.Gid,
                                Name = l2.DisplayName,
                                NameAlt = l2.NameAlt,
                                IsActive = l1.IsActive && l2.IsActive,
                                ProgrammePriorityCode = l2.ProgrammePriorityCode,
                                AidMode = l2.AidMode,
                                IsEligibleCost = l2.IsEligibleCost,
                                IsStandardTablesExpense = l2.IsStandardTablesExpense,
                                IsOneTimeExpense = l2.IsOneTimeExpense,
                                IsFlatRateExpense = l2.IsFlatRateExpense,
                                IsLandExpense = l2.IsLandExpense,
                                IsEuApprovedStandardTablesExpense = l2.IsEuApprovedStandardTablesExpense,
                                IsEuApprovedOneTimeExpense = l2.IsEuApprovedOneTimeExpense,
                                Details = l2.Level3Items.Select(l3 => new BudgetExpenseDetailJson
                                {
                                    BudgetLevel3Id = l3.ProcedureBudgetLevel3Id,
                                    Gid = l3.Gid,
                                    Note = l3.DisplayName,
                                }).ToList(),
                            }).ToList(),
                        }).ToList(),
                    })
                    .ToList();
        }

        private string GetProcLocationFullPath(ProcedureLocation location)
        {
            switch (location.NutsLevel)
            {
                case NutsLevel.Country:
                    return location.CountryId.HasValue ?
                        this.countryNomsRepository.GetNom(location.CountryId.Value).Code :
                        null;
                case NutsLevel.RegionNUTS1:
                    return location.Nuts1Id.HasValue ?
                        this.nuts1NomsRepository.GetNom(location.Nuts1Id.Value).FullPath :
                        null;
                case NutsLevel.RegionNUTS2:
                    return location.Nuts2Id.HasValue ?
                        this.nuts2NomsRepository.GetNom(location.Nuts2Id.Value).FullPath :
                        null;
                case NutsLevel.District:
                    return location.DistrictId.HasValue ?
                        this.districtNomsRepository.GetNom(location.DistrictId.Value).FullPath :
                        null;
                case NutsLevel.Municipality:
                    return location.MunicipalityId.HasValue ?
                        this.municipalityNomsRepository.GetNom(location.MunicipalityId.Value).FullPath :
                        null;
                case NutsLevel.Settlement:
                    return location.SettlementId.HasValue ?
                        this.settlementNomsRepository.GetNom(location.SettlementId.Value).FullPath :
                        null;
                case NutsLevel.ProtectedZone:
                    return location.ProtectedZoneId.HasValue ?
                        this.protectedZoneNomsRepository.GetNom(location.ProtectedZoneId.Value).FullPath :
                        null;
                default:
                    throw new Exception("Uknown nuts level.");
            }
        }

        private IList<ProcedureApplicationSectionJson> GetProcedureApplicationSections(int procedureId, ICollection<ProcedureApplicationSection> sections, ProcedureApplicationSectionAdditionalSetting sectionAdditionalSetting)
        {
            sectionAdditionalSetting = sectionAdditionalSetting ?? new ProcedureApplicationSectionAdditionalSetting(procedureId);

            var result = new List<ProcedureApplicationSectionJson>();

            foreach (var section in sections)
            {
                var newSection = new ProcedureApplicationSectionJson(section);

                foreach (var property in sectionAdditionalSetting.GetType().GetProperties().Where(prop => prop.PropertyType == typeof(bool)))
                {
                    var propertyValue = (bool)property.GetValue(sectionAdditionalSetting);
                    var foundAdditionalSetting = ProcedureApplicationSectionAdditionalSettingType.GetType(section.Section, property.Name);

                    if (foundAdditionalSetting.HasValue)
                    {
                        newSection.AdditionalSettings.Add(new ProcedureApplicationSectionAdditionalSettingJson(property.Name, propertyValue));
                    }
                }

                result.Add(newSection);
            }

            return result;
        }
    }
}
