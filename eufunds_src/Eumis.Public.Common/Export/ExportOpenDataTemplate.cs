using System.Collections.Generic;
using System.Linq;

namespace Eumis.Public.Common.Export
{
    public class ExportOpenDataTemplate
    {
        public ExportOpenDataTemplate(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public List<ExportProject> Projects { get; set; }

        public List<ExportContract> Contracts { get; set; }

        public List<ExportEntity> Entities { get; set; }

        public OpenDataXmlContainer GenerateOpenDataXmlContainer()
        {
            var xmlProjects = new List<OpenDataXmlProject>();
            var xmlContracts = new List<OpenDataXmlContract>();
            var xmlEntities = new List<OpenDataXmlEntity>();

            if (this.Projects != null && this.Projects.Count > 0)
            {
                foreach (var project in this.Projects)
                {
                    xmlProjects.Add(
                        new OpenDataXmlProject
                        {
                            Id = project.Id,
                            SourceofFunding = project.SourceofFunding,
                            InitialDate = project.InitialDate,
                            EndDate = project.EndDate,
                            DateofDecisionforFunding = project.DateofDecisionforFunding,
                            ProjectBeneficiary = new OpenDataXmlProjectBeneficiary { EntityId = project.ProjectBeneficiaryEntityId },
                            PlaceOfExecution = project.PlaceOfExecution?.Select(p => new OpenDataXmlPlaceOfExecution { Municipality = p }).ToList(),
                            Name = project.Name,
                            TotalValue = project.TotalValue,
                            BeneficiaryFunding = project.BeneficiaryFunding,
                            ActuallyPaidAmounts = project.ActuallyPaid?.Select(p => new OpenDataXmlActuallyPaidAmount { Value = p }).ToList(),
                            DurationInMonths = project.DurationInMonths,
                            Description = project.Description,
                            Status = project.Status,
                            Partners = project.PartnerEntityIds?.Select(p => new OpenDataXmlPartner { EntityId = p }).ToList(),
                        });
                }
            }

            if (this.Contracts != null && this.Contracts.Count > 0)
            {
                foreach (var contract in this.Contracts)
                {
                    xmlContracts.Add(
                        new OpenDataXmlContract
                        {
                            EntityType = contract.EntityType,
                            EntityId = contract.EntityId,
                            ContractId = contract.ContractId,
                            SignatureDate = contract.SignatureDate,
                            InitialDate = contract.InitialDate,
                            EndDate = contract.EndDate,
                            Description = contract.Description,
                            Amount = contract.Amount,
                            SubcontractEntities = contract.SubcontractEntityIds?.Select(e => new OpenDataXmlSubcontractEntity { EntityId = e }).ToList(),
                        });
                }
            }

            if (this.Entities != null && this.Entities.Count > 0)
            {
                foreach (var entity in this.Entities)
                {
                    xmlEntities.Add(
                        new OpenDataXmlEntity
                        {
                            EntityId = entity.Id,
                            EntityUin = entity.EntityUin,
                            EntityName = entity.EntityName,
                            EntityAddress = entity.EntityAddress,
                            EntityZipCode = entity.EntityZipCode,
                            EntityCity = entity.EntityCity,
                            EntityMunicipality = entity.EntityMunicipality,
                            EntityDistrict = entity.EntityDistrict,
                        });
                }
            }

            return new OpenDataXmlContainer()
            {
                Projects = xmlProjects,
                Contracts = xmlContracts,
                Entities = xmlEntities,
            };
        }
    }
}
