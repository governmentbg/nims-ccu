using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Eumis.Public.Common.Export
{
    [SuppressMessage("", "SA1649:FileNameMustMatchTypeName", Justification = "Common file name is used for all classes")]
    [Serializable]
    [XmlType(TypeName = "Container")]
    public class OpenDataXmlContainer
    {
        public List<OpenDataXmlProject> Projects { get; set; }

        public List<OpenDataXmlContract> Contracts { get; set; }

        public List<OpenDataXmlEntity> Entities { get; set; }
    }

    [SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "XML classes should be in the same file for simplicity")]
    [Serializable]
    [XmlType(TypeName = "Project")]
    public class OpenDataXmlProject
    {
        public string Id { get; set; }

        public string SourceofFunding { get; set; }

        public DateTime InitialDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime DateofDecisionforFunding { get; set; }

        public OpenDataXmlProjectBeneficiary ProjectBeneficiary { get; set; }

        public List<OpenDataXmlPlaceOfExecution> PlaceOfExecution { get; set; }

        public string Name { get; set; }

        public decimal? TotalValue { get; set; }

        public decimal? BeneficiaryFunding { get; set; }

        public List<OpenDataXmlActuallyPaidAmount> ActuallyPaidAmounts { get; set; }

        public int DurationInMonths { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public List<OpenDataXmlPartner> Partners { get; set; }
    }

    [SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "XML classes should be in the same file for simplicity")]
    [Serializable]
    [XmlType(TypeName = "ProjectBeneficiary")]
    public class OpenDataXmlProjectBeneficiary
    {
        public string EntityId { get; set; }
    }

    [SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "XML classes should be in the same file for simplicity")]
    [Serializable]
    [XmlType(TypeName = "PlaceOfExecution")]
    public class OpenDataXmlPlaceOfExecution
    {
        public string Municipality { get; set; }
    }

    [SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "XML classes should be in the same file for simplicity")]
    [Serializable]
    [XmlType(TypeName = "Partner")]
    public class OpenDataXmlPartner
    {
        public string EntityId { get; set; }
    }

    [SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "XML classes should be in the same file for simplicity")]
    [Serializable]
    [XmlType(TypeName = "Contract")]
    public class OpenDataXmlContract
    {
        public string EntityType { get; set; }

        public string EntityId { get; set; }

        public string ContractId { get; set; }

        public DateTime SignatureDate { get; set; }

        public DateTime InitialDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public List<OpenDataXmlSubcontractEntity> SubcontractEntities { get; set; }
    }

    [SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "XML classes should be in the same file for simplicity")]
    [Serializable]
    [XmlType(TypeName = "Entity")]
    public class OpenDataXmlEntity
    {
        public string EntityId { get; set; }

        public string EntityUin { get; set; }

        public string EntityName { get; set; }

        public string EntityAddress { get; set; }

        public string EntityZipCode { get; set; }

        public string EntityCity { get; set; }

        public string EntityMunicipality { get; set; }

        public string EntityDistrict { get; set; }
    }

    [SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "XML classes should be in the same file for simplicity")]
    [Serializable]
    [XmlType(TypeName = "SubcontractEntity")]
    public class OpenDataXmlSubcontractEntity
    {
        public string EntityId { get; set; }
    }

    [SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "XML classes should be in the same file for simplicity")]
    [Serializable]
    [XmlType(TypeName = "ActuallyPaidAmount")]
    public class OpenDataXmlActuallyPaidAmount
    {
        public decimal? Value { get; set; }
    }
}
