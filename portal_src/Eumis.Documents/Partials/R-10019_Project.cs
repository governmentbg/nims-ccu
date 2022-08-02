//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Eumis.Common.Linq;
using Eumis.Common.Validation;
using Eumis.Documents;
using Eumis.Documents.Contracts;
using Eumis.Documents.Enums;
using Eumis.Documents.Interfaces;
using Eumis.Documents.Mappers;
using Eumis.Documents.Partials;
using Eumis.Documents.Validation;
using R_10018;
using R_10093;

namespace R_10019
{
    public partial class Project : BaseApplicationDocument, IDocumentNomenclatures, IEumisDocument, IEumisDocumentWithFiles, IRemoteValidatable, ILocalValidatable
    {
        string IEumisDocument.Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        DateTime IEumisDocument.CreateDate
        {
            get
            {
                return this.createDate;
            }
            set
            {
                this.createDate = value;
            }
        }

        DateTime IEumisDocument.ModificationDate
        {
            get
            {
                return this.modificationDate;
            }
            set
            {
                this.modificationDate = value;
            }
        }

        public IEnumerable<AttachedDocument> Files
        {
            get
            {
                return EnumerableExtensions.Concat(
                    this.GetFiles(d => d.AttachedDocuments.AttachedDocumentCollection),
                    this.GetFiles(
                        d => d.AttachedDocuments.AttachedDocumentCollection,
                        d => d.SignatureContentCollection.Select(adc => new AttachedDocument() { AttachedDocumentContent = adc })));
            }
        }

        [XmlIgnore]
        public bool IsPreliminary
        {
            get
            {
                return this.ProjectBasicData != null
                    && this.ProjectBasicData.ApplicationFormType != null
                    && ApplicationFormTypeNomenclature.PreliminarySelection.Code.Equals(this.ProjectBasicData.ApplicationFormType.Value);
            }
        }

        [XmlIgnore]
        public bool IsStandardForBudgetLine
        {
            get
            {
                return this.ProjectBasicData != null
                    && this.ProjectBasicData.ApplicationFormType != null
                    && ApplicationFormTypeNomenclature.StandardForBudgetLine.Code.Equals(this.ProjectBasicData.ApplicationFormType.Value);
            }
        }

        [XmlIgnore]
        public bool IsStandardSimplified
        {
            get
            {
                return this.ProjectBasicData != null
                    && this.ProjectBasicData.ApplicationFormType != null
                    && ApplicationFormTypeNomenclature.StandardSimplified.Code.Equals(this.ProjectBasicData.ApplicationFormType.Value);
            }
        }

        [XmlIgnore]
        public bool IsFinalRecipients
        {
            get
            {
                return this.ProjectBasicData != null
                    && this.ProjectBasicData.ApplicationFormType != null
                    && ApplicationFormTypeNomenclature.EndUsersInfo.Code.Equals(this.ProjectBasicData.ApplicationFormType.Value);
            }
        }

        [XmlIgnore]
        public bool IsFinancialIntermediaries
        {
            get
            {
                return this.ProjectBasicData != null
                    && this.ProjectBasicData.ApplicationFormType != null
                    && ApplicationFormTypeNomenclature.FOFFinancialAgentsInfo.Code.Equals(this.ProjectBasicData.ApplicationFormType.Value);
            }
        }

        [XmlIgnore]
        public RegistrationTypeNomenclature RegistrationType
        {
            get
            {
                if (this != null
                    && this.ProjectBasicData != null
                    && this.ProjectBasicData.AllowedRegistrationType != null
                    && !String.IsNullOrWhiteSpace(this.ProjectBasicData.AllowedRegistrationType.Value))
                {
                    string allowedRegType = this.ProjectBasicData.AllowedRegistrationType.Value;

                    if (allowedRegType.Equals(RegistrationTypeNomenclature.Digital.Code))
                    {
                        return RegistrationTypeNomenclature.Digital;
                    }

                    if (allowedRegType.Equals(RegistrationTypeNomenclature.Paper.Code))
                    {
                        return RegistrationTypeNomenclature.Paper;
                    }
                }

                return RegistrationTypeNomenclature.DigitalOrPaper;
            }
        }

        public static R_10019.Project Load(ContractProcedure procedure, Project project, Guid? procedureGid = null)
        {
            bool hasProgrammes = procedure.programmes != null && procedure.programmes.Count > 0;
            bool hasOnlyOneProgramme = procedure.programmes != null && procedure.programmes.Count == 1;

            bool hasDifferentProgrammes = !hasProgrammes || project == null || project.ProjectBasicData == null
                || project.ProjectBasicData.ProgrammeBasicDataCollection == null
                || project.ProjectBasicData.ProgrammeBasicDataCollection.Count != procedure.programmes.Count;

            if (project == null)
            {
                project = new Project();
                project.createDate = DateTime.Now;
            }

            if (String.IsNullOrWhiteSpace(project.id))
                project.id = Guid.NewGuid().ToString();

            project.EndingDate = procedure.endingDate;

            #region Nomenclatures

            LoadNomenclatures(ref project, procedure);

            #endregion

            #region ApplicationSection

            project.ApplicationSections = procedure.applicationSections.Select(x => new ApplicationSection(x)).ToList();

            #endregion

            #region ProjectBasicData

            if (project.ProjectBasicData == null)
                project.ProjectBasicData = new R_10002.ProjectBasicData();

            project.ProjectBasicData.ApplicationFormType = new R_09991.EnumNomenclature(procedure.applicationFormType);
            project.ProjectBasicData.AllowedRegistrationType = new R_09991.EnumNomenclature(procedure.allowedRegistrationType);

            if (procedure.applicationSections.SelectMany(x => x.additionalSettings).Any())
            {
                project.ProjectBasicData.FillMainData = procedure
                    .applicationSections
                    .Where(x => x.section.value == "basicData")
                    .SelectMany(x => x.additionalSettings)
                    .Where(x => x.name == "FillMainData")
                    .Select(x => x.isSelected)
                    .First();
            }

            if (project.ProjectBasicData.FillMainData)
            {
                project.ProjectBasicData.Name = procedure.procedureName;
                project.ProjectBasicData.NameEN = procedure.procedureNameAlt;
                project.ProjectBasicData.Description = procedure.procedureName;
                project.ProjectBasicData.DescriptionEN = procedure.procedureNameAlt;
                project.ProjectBasicData.Purpose = procedure.procedureDescription;
                project.ProjectBasicData.Duration = procedure.projectDuration.ToString();
            }

            project.ProjectBasicData.Locations = procedure.locations.Select(x => new Tuple<R_09991.EnumNomenclature, string>(new R_09991.EnumNomenclature(x.nutsLevel), x.locationFullPath)).ToList();
            project.ProjectBasicData.MaxDuration = procedure.projectDuration.HasValue ? procedure.projectDuration.Value : 0;

            if (String.IsNullOrWhiteSpace(project.ProjectBasicData.id))
                project.ProjectBasicData.id = Guid.NewGuid().ToString();

            if (project.ProjectBasicData.NutsAddress == null)
                project.ProjectBasicData.NutsAddress = new R_09999.NutsAddress();

            if (project.ProjectBasicData.NutsAddress.NutsLevel == null || String.IsNullOrWhiteSpace(project.ProjectBasicData.NutsAddress.NutsLevel.Id))
                project.ProjectBasicData.NutsAddress.NutsLevel = new R_10000.PrivateNomenclature()
                {
                    Id = NutsLevelNomenclature.Settlement.Id,
                    Name = NutsLevelNomenclature.Settlement.Name,
                    NameEN = NutsLevelNomenclature.Settlement.NameEN
                };

            if (project.ProjectBasicData.NutsAddress.NutsAddressContentCollection == null || project.ProjectBasicData.NutsAddress.NutsAddressContentCollection.Count == 0)
                project.ProjectBasicData.NutsAddress.NutsAddressContentCollection = new R_09999.NutsAddressContentCollection()
                {
                    new R_09999.NutsAddressContent()
                };

            project.ProjectBasicData.ProgrammeBasicDataCollection = new R_10002.ProgrammeBasicDataCollection();
            if (hasProgrammes)
            {
                foreach (var programme in procedure.programmes)
                {
                    var basicData = new R_09997.ProgrammeBasicData()
                    {
                        Programme = new R_10001.PublicNomenclature()
                        {
                            Code = programme.programmeCode,
                            Name = programme.programmeName,
                            NameEN = programme.programmeNameAlt
                        },
                        ProgrammePriorityCollection = new R_09997.PublicNomenclatureCollection()
                    };
                    basicData.ProgrammePriorityCollection.AddRange(programme.programmePriorities
                                .Select(e => new R_10001.PublicNomenclature()
                                {
                                    Code = e.programmePriorityCode,
                                    Name = e.programmePriorityName,
                                    NameEN = e.programmePriorityNameAlt
                                }));

                    project.ProjectBasicData.ProgrammeBasicDataCollection.Add
                        (
                            basicData
                        );
                }
            }

            if (procedureGid.HasValue)
            {
                project.ProjectBasicData.ProcedureIdentifier = procedureGid.ToString();
            }

            project.ProjectBasicData.Procedure = new R_10001.PublicNomenclature();
            project.ProjectBasicData.Procedure.Code = procedure.procedureCode;
            project.ProjectBasicData.Procedure.Name = procedure.procedureName;
            project.ProjectBasicData.Procedure.NameEN = procedure.procedureNameAlt;

            #endregion

            #region Candidate

            if (project.Candidate == null)
            {
                project.Candidate = new R_10004.Company()
                {
                    Seat = new R_10003.Address() { Country = new R_10001.PublicNomenclature() { Code = Constants.BulgariaId, Name = Constants.BulgariaName, NameEN = Constants.BulgariaNameEN } },
                    Correspondence = new R_10003.Address() { Country = new R_10001.PublicNomenclature() { Code = Constants.BulgariaId, Name = Constants.BulgariaName, NameEN = Constants.BulgariaNameEN } }
                };
            }

            if (String.IsNullOrWhiteSpace(project.Candidate.id))
                project.Candidate.id = Guid.NewGuid().ToString();

            #endregion

            #region PartnerCollection

            if (project.Partners == null)
                project.Partners = new Partners();

            if (String.IsNullOrWhiteSpace(project.Partners.id))
                project.Partners.id = Guid.NewGuid().ToString();

            #endregion

            #region DimensionsBudgetContractCollection

            if (hasDifferentProgrammes)
            {
                project.DirectionsBudgetContractCollection = new DirectionsBudgetContractCollection();
                if (hasProgrammes)
                {
                    foreach (var programme in procedure.programmes)
                    {
                        var triple = new R_09998.DirectionsBudgetContract()
                        {
                            id = Guid.NewGuid().ToString(),
                            Index = procedure.programmes.IndexOf(programme),
                            programmeCode = programme.programmeCode,
                            programmeName = hasOnlyOneProgramme ? "" : programme.programmeName,
                            Directions = new R_10093.DirectionSection(),
                            Budget = new R_10010.Budget(),
                            Contract = new R_10006.Contract()
                        };
                        triple.Budget.Load(programme.budgetExpenseTypes);

                        triple.Directions.Items = DirectionSection.Load(procedure.directions);

                        if (triple.Directions.DirectionCollection == null)
                        {
                            triple.Directions.DirectionCollection = new DirectionCollection();
                        }

                        project.DirectionsBudgetContractCollection.Add(triple);
                    }
                }
            }
            else
            {
                for (int i = 0; i < procedure.programmes.Count; i++)
                {
                    project.DirectionsBudgetContractCollection[i].Index = i;

                    project.DirectionsBudgetContractCollection[i].programmeCode = procedure.programmes[i].programmeCode;

                    project.DirectionsBudgetContractCollection[i].programmeName = hasOnlyOneProgramme ? "" : procedure.programmes[i].programmeName;
                    project.DirectionsBudgetContractCollection[i].IsDirectionSelected = project.IsApplicationSectionSelected(ApplicationSectionType.Directions);

                    if (project.DirectionsBudgetContractCollection[i].Directions == null)
                        project.DirectionsBudgetContractCollection[i].Directions = new R_10093.DirectionSection();
                        project.DirectionsBudgetContractCollection[i].id = Guid.NewGuid().ToString();

                    if (project.DirectionsBudgetContractCollection[i].Budget == null)
                    {
                        project.DirectionsBudgetContractCollection[i].Budget = new R_10010.Budget();
                    }
                    else
                    {

                    }

                    project.DirectionsBudgetContractCollection[i].Budget.Load(procedure.programmes[i].budgetExpenseTypes);

                    project.DirectionsBudgetContractCollection[i].Directions.Items = DirectionSection.Load(procedure.directions);

                    if (project.DirectionsBudgetContractCollection[i].Directions.DirectionCollection == null)
                    {
                        project.DirectionsBudgetContractCollection[i].Directions.DirectionCollection = new DirectionCollection();
                    }
                }
            }

            #endregion

            #region ProgrammeContractActivitiesCollection

            if (hasDifferentProgrammes)
            {
                project.ProgrammeContractActivitiesCollection = new ProgrammeContractActivitiesCollection();
                if (hasProgrammes)
                {
                    foreach (var programme in procedure.programmes)
                    {
                        project.ProgrammeContractActivitiesCollection.Add
                            (
                                new R_09995.ProgrammeContractActivities()
                                {
                                    Index = procedure.programmes.IndexOf(programme),
                                    id = Guid.NewGuid().ToString(),
                                    programmeCode = programme.programmeCode,
                                    programmeName = hasOnlyOneProgramme ? "" : programme.programmeName,
                                }
                            );
                    }
                }
            }
            else
            {
                for (int i = 0; i < procedure.programmes.Count; i++)
                {
                    project.ProgrammeContractActivitiesCollection[i].Index = i;
                    project.ProgrammeContractActivitiesCollection[i].programmeCode = procedure.programmes[i].programmeCode;
                    project.ProgrammeContractActivitiesCollection[i].programmeName = hasOnlyOneProgramme ? "" : procedure.programmes[i].programmeName;
                }
            }

            #endregion

            #region ProgrammeIndicatorsCollection

            if (hasDifferentProgrammes)
            {
                project.ProgrammeIndicatorsCollection = new ProgrammeIndicatorsCollection() { };
                if (hasProgrammes)
                {
                    foreach (var programme in procedure.programmes)
                    {
                        project.ProgrammeIndicatorsCollection.Add
                            (
                                new R_10014.ProgrammeIndicators()
                                {
                                    id = Guid.NewGuid().ToString(),
                                    programmeCode = programme.programmeCode,
                                    programmeName = hasOnlyOneProgramme ? "" : programme.programmeName,
                                    Items = R_10014.ProgrammeIndicators.Load(programme.indicators)
                                }
                            );
                    }
                }
            }
            else
            {
                for (int i = 0; i < procedure.programmes.Count; i++)
                {
                    project.ProgrammeIndicatorsCollection[i].programmeCode = procedure.programmes[i].programmeCode;

                    project.ProgrammeIndicatorsCollection[i].programmeName = hasOnlyOneProgramme ? "" : procedure.programmes[i].programmeName;

                    project.ProgrammeIndicatorsCollection[i].Items = R_10014.ProgrammeIndicators.Load(procedure.programmes[i].indicators);

                    // set inactive
                    var inactiveIndicatorIds = procedure.programmes[i].indicators.Where(e => !e.isActive).Select(e => e.gid);

                    for (int j = 0; j < project.ProgrammeIndicatorsCollection[i].IndicatorCollection.Count; j++)
                    {
                        if (inactiveIndicatorIds.Contains(project.ProgrammeIndicatorsCollection[i].IndicatorCollection[j].Id))
                            project.ProgrammeIndicatorsCollection[i].IndicatorCollection[j].IsDeactivated = true;
                    }
                }
            }

            #endregion

            #region ContractTeamCollection

            if (project.ContractTeams == null)
                project.ContractTeams = new ContractTeams();

            if (String.IsNullOrWhiteSpace(project.ContractTeams.id))
                project.ContractTeams.id = Guid.NewGuid().ToString();

            #endregion

            #region ProjectErrandCollection

            if (project.ProjectErrands == null)
                project.ProjectErrands = new ProjectErrands();

            if (String.IsNullOrWhiteSpace(project.ProjectErrands.id))
                project.ProjectErrands.id = Guid.NewGuid().ToString();

            #endregion

            #region ElectronicDeclarations

            if (project.ElectronicDeclarations == null)
                project.ElectronicDeclarations = new ElectronicDeclarations();

            if (String.IsNullOrWhiteSpace(project.ElectronicDeclarations.id))
                project.ElectronicDeclarations.id = Guid.NewGuid().ToString();

            if (project.IsApplicationSectionSelected(ApplicationSectionType.ElectronicDeclarations))
            {
                project.ElectronicDeclarations.ElectronicDeclarationCollection = ElectronicDeclarationCollection.Load(procedure.declarations, project.ElectronicDeclarations.ElectronicDeclarationCollection);
            }

            #endregion

            #region ProjectSpecFieldCollection

            if (project.ProjectSpecFields == null)
                project.ProjectSpecFields = new ProjectSpecFields();

            if (String.IsNullOrWhiteSpace(project.ProjectSpecFields.id))
                project.ProjectSpecFields.id = Guid.NewGuid().ToString();

            if (project.ProjectSpecFields.ProjectSpecFieldCollection == null)
                project.ProjectSpecFields.ProjectSpecFieldCollection = new ProjectSpecFieldCollection();
            project.ProjectSpecFields.ProjectSpecFieldCollection = ProjectSpecFieldCollection.Load(procedure.specFields, project.ProjectSpecFields.ProjectSpecFieldCollection);

            #endregion

            #region AttachedDocumentCollection

            if (project.AttachedDocuments == null)
                project.AttachedDocuments = new AttachedDocuments();

            if (String.IsNullOrWhiteSpace(project.AttachedDocuments.id))
                project.AttachedDocuments.id = Guid.NewGuid().ToString();

            if (project.AttachedDocuments.AttachedDocumentCollection == null)
                project.AttachedDocuments.AttachedDocumentCollection = new AttachedDocumentCollection();

            // set inactive
            var inactiveAttachedDocumentIds = procedure.applicationDocs.Where(e => !e.isActive).Select(e => e.gid);
            for (int i = 0; i < project.AttachedDocuments.AttachedDocumentCollection.Count; i++)
            {
                if (project.AttachedDocuments.AttachedDocumentCollection[i].Type != null
                    && inactiveAttachedDocumentIds.Contains(project.AttachedDocuments.AttachedDocumentCollection[i].Type.Id))
                    project.AttachedDocuments.AttachedDocumentCollection[i].IsDeactivated = true;
            }

            #endregion

            project.RequiredDocumentsCodesNames = new List<Tuple<string, string>>();

            if (project.Nomenclatures[Eumis.Documents.Mappers.NomenclatureType.AttachedDocumentType] != null)
                project.RequiredDocumentsCodesNames = project.Nomenclatures[Eumis.Documents.Mappers.NomenclatureType.AttachedDocumentType].Where(e => e.IsRequired.HasValue && e.IsRequired.Value).Select(e => new Tuple<string, string>(e.Value, e.Name)).ToList();

            project.DocumentsExtensions = new Dictionary<string, string>();

            foreach (var document in procedure.applicationDocs.Where(appDoc => appDoc.isActive))
            {
                if (!string.IsNullOrWhiteSpace(document.extension) && !project.DocumentsExtensions.ContainsKey(document.gid))
                {
                    project.DocumentsExtensions[document.gid] = document.extension;
                }
            }

            return project;
        }

        public static void LoadNomenclatures(ref R_10019.Project project, ContractProcedure procedure)
        {
            #region Nomenclatures

            project.Nomenclatures = new Dictionary<Eumis.Documents.Mappers.NomenclatureType, List<Eumis.Documents.Mappers.Nomenclature>>();

            project.Nomenclatures.Add(Eumis.Documents.Mappers.NomenclatureType.AttachedDocumentType, procedure.applicationDocs.Where(e => e.isActive).Select(e => new Eumis.Documents.Mappers.Nomenclature(e)).ToList());

            #endregion
        }

        public void SetDeclarationsAttributes(List<ContractProcedureDeclaration> procedureDeclarations)
        {
            if (this.HasElectronicDeclarations)
            {
                for (int i = 0; i < this.ElectronicDeclarations.ElectronicDeclarationCollection.Count; i++)
                {
                    var procedureDeclaration = procedureDeclarations
                        .Where(d => d.gid.ToString() == this.ElectronicDeclarations.ElectronicDeclarationCollection[i].Gid)
                        .SingleOrDefault();

                    if (procedureDeclaration != null)
                    {
                        this.ElectronicDeclarations.ElectronicDeclarationCollection[i].IsActive = procedureDeclaration.isActive;
                    }
                }
            }
        }

        public static void LockUnlockAllSections(R_10019.Project project, bool isLock)
        {
            if (project.ProjectBasicData != null)
                project.ProjectBasicData.isLocked = isLock;

            if (project.Candidate != null)
                project.Candidate.isLocked = isLock;

            if (project.Partners != null)
                project.Partners.isLocked = isLock;

            if (project.DirectionsBudgetContractCollection != null)
            {
                for (int i = 0; i < project.DirectionsBudgetContractCollection.Count; i++)
                    project.DirectionsBudgetContractCollection[i].isLocked = isLock;
            }

            if (project.ProgrammeContractActivitiesCollection != null)
            {
                for (int i = 0; i < project.ProgrammeContractActivitiesCollection.Count; i++)
                    project.ProgrammeContractActivitiesCollection[i].isLocked = isLock;
            }

            if (project.ProgrammeIndicatorsCollection != null)
            {
                for (int i = 0; i < project.ProgrammeIndicatorsCollection.Count; i++)
                    project.ProgrammeIndicatorsCollection[i].isLocked = isLock;
            }

            if (project.ContractTeams != null)
                project.ContractTeams.isLocked = isLock;

            if (project.ProjectErrands != null)
                project.ProjectErrands.isLocked = isLock;

            if (project.ProjectSpecFields != null)
                project.ProjectSpecFields.isLocked = isLock;

            if (project.ElectronicDeclarations != null)
                project.ElectronicDeclarations.isLocked = isLock;

            if (project.AttachedDocuments != null)
                project.AttachedDocuments.isLocked = isLock;
        }

        public void PassFormTypeInfo()
        {
            if (this.ProjectBasicData != null)
            {
                this.ProjectBasicData.IsFinalRecipients = this.IsFinalRecipients;
                this.ProjectBasicData.IsFinancialIntermediaries = this.IsFinancialIntermediaries;
            }

            if (this.DirectionsBudgetContractCollection != null && this.DirectionsBudgetContractCollection.Count > 0)
            {
                for (int i = 0; i < this.DirectionsBudgetContractCollection.Count; i++)
                {
                    this.DirectionsBudgetContractCollection[i].IsFinalRecipients = this.IsFinalRecipients;
                    this.DirectionsBudgetContractCollection[i].IsFinancialIntermediaries = this.IsFinancialIntermediaries;

                    if (this.DirectionsBudgetContractCollection[i].Budget != null)
                    {
                        this.DirectionsBudgetContractCollection[i].Budget.IsFinalRecipients = this.IsFinalRecipients;
                        this.DirectionsBudgetContractCollection[i].Budget.IsFinancialIntermediaries = this.IsFinancialIntermediaries;
                    }

                    if (this.DirectionsBudgetContractCollection[i].Contract != null)
                    {
                        this.DirectionsBudgetContractCollection[i].Contract.IsFinalRecipients = this.IsFinalRecipients;
                        this.DirectionsBudgetContractCollection[i].Contract.IsFinancialIntermediaries = this.IsFinancialIntermediaries;
                    }
                }
            }

            if (this.ProgrammeContractActivitiesCollection != null && this.ProgrammeContractActivitiesCollection.Count > 0)
            {
                for (int i = 0; i < this.ProgrammeContractActivitiesCollection.Count; i++)
                {
                    this.ProgrammeContractActivitiesCollection[i].IsFinalRecipients = this.IsFinalRecipients;
                    this.ProgrammeContractActivitiesCollection[i].IsFinancialIntermediaries = this.IsFinancialIntermediaries;
                }
            }

            if (this.ContractTeams != null && this.ContractTeams.ContractTeamCollection != null)
            {
                this.ContractTeams.ContractTeamCollection.IsFinalRecipients = this.IsFinalRecipients;
                this.ContractTeams.ContractTeamCollection.IsFinancialIntermediaries = this.IsFinancialIntermediaries;
            }

            if (this.ProjectErrands != null && this.ProjectErrands.ProjectErrandCollection != null)
            {
                this.ProjectErrands.ProjectErrandCollection.IsFinalRecipients = this.IsFinalRecipients;
                this.ProjectErrands.ProjectErrandCollection.IsFinancialIntermediaries = this.IsFinancialIntermediaries;
            }
        }

        [XmlIgnore]
        public bool HasProjectSpecFields
        {
            get
            {
                return this.ProjectSpecFields != null
                    && this.ProjectSpecFields.ProjectSpecFieldCollection != null
                    && this.ProjectSpecFields.ProjectSpecFieldCollection.Count > 0;
            }
        }

        [XmlIgnore]
        public bool HasPaperAttachedDocuments
        {
            get
            {
                return
                    (this.Nomenclatures != null
                    && this.Nomenclatures[Eumis.Documents.Mappers.NomenclatureType.AttachedDocumentType] != null
                    && this.Nomenclatures[Eumis.Documents.Mappers.NomenclatureType.AttachedDocumentType].Count > 0);
            }
        }

        [XmlIgnore]
        public bool HasAttachedDocuments
        {
            get
            {
                return
                    (this.Nomenclatures != null
                    && this.Nomenclatures[Eumis.Documents.Mappers.NomenclatureType.AttachedDocumentType] != null
                    && this.Nomenclatures[Eumis.Documents.Mappers.NomenclatureType.AttachedDocumentType].Count > 0)
                    ||
                    (this.AttachedDocuments != null
                    && this.AttachedDocuments.AttachedDocumentCollection != null
                    && this.AttachedDocuments.AttachedDocumentCollection.Count > 0);
            }
        }

        [XmlIgnore]
        public bool HasElectronicDeclarations
        {
            get
            {
                return this.ElectronicDeclarations != null
                    && this.ElectronicDeclarations.ElectronicDeclarationCollection != null
                    && this.ElectronicDeclarations.ElectronicDeclarationCollection.Count > 0;
            }
        }

        [XmlIgnore]
        public Dictionary<Eumis.Documents.Mappers.NomenclatureType, List<Eumis.Documents.Mappers.Nomenclature>> Nomenclatures { get; set; }

        [XmlIgnore]
        public ContractProjectHeader ProjectHeader { get; set; }

        [XmlIgnore]
        public List<ModelValidationResultExtended> LocalValidationErrors { get; set; }

        [XmlIgnore]
        public List<string> RemoteValidationErrors { get; set; }

        [XmlIgnore]
        public List<string> RemoteValidationWarnings { get; set; }

        [XmlIgnore]
        public DateTime? EndingDate { get; set; }

        [XmlIgnore]
        public DateTime? VersionCreateDate { get; set; }

        [XmlIgnore]
        public Guid ProjectGid { get; set; }

        [XmlIgnore]
        public bool IsSubmitted { get; set; }

        [XmlIgnore]
        public List<Tuple<string, string>> RequiredDocumentsCodesNames { get; set; }

        [XmlIgnore]
        public Dictionary<string, string> DocumentsExtensions { get; set; }

        private static List<ContractBudgetExpenseType> GetFakeContractBudgetExpenseTypes()
        {
            var result = new List<ContractBudgetExpenseType>();

            for (int i = 1; i < 4; i++)
            {
                var expenseType = new ContractBudgetExpenseType()
                {
                    gid = String.Format("gid{0}", i),
                    isActive = true,
                    name = String.Format("level{0}", i),
                    orderNum = i,
                    expenses = new List<ContractExpense>()
                };

                for (int j = 1; j < 4; j++)
                {
                    var expense = new ContractExpense()
                    {
                        gid = String.Format("gid{0}.{1}", i, j),
                        isActive = true,
                        name = String.Format("level{0}.{1}", i, j),
                        orderNum = j,
                        details = new List<ContractExpenseDetails>()
                    };

                    for (int k = 1; k < 4; k++)
                    {
                        var detail = new ContractExpenseDetails()
                        {
                            orderNum = k,
                            gid = String.Format("gid{0}.{1}.{2}", i, j, k),
                            note = String.Format("level{0}.{1}.{2}", i, j, k),
                        };

                        expense.details.Add(detail);
                    }

                    expenseType.expenses.Add(expense);
                }

                result.Add(expenseType);
            }

            #region Set active

            result[0].isActive = false;
            result[1].expenses[0].isActive = false;
            result[2].expenses[0].isActive = false;

            #endregion

            return result;
        }
    }
}
