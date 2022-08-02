using Eumis.Domain.Contracts;
using Eumis.Domain.NonAggregates;
using System;

public class ContractServiceDO
{
    public ContractServiceDO()
    {
        this.ContractType = ContractType.ServiceAgreement;
    }

    public int? ProgrammeId { get; set; }

    public int ProcedureId { get; set; }

    public ContractType ContractType { get; set; }

    public ContractRegistrationType RegistrationType { get; set; }

    public int CompanyId { get; set; }

    public string CompanyName { get; set; }

    public string CompanyUin { get; set; }

    public UinType CompanyUinType { get; set; }

    public string Name { get; set; }

    public string NameAlt { get; set; }

    public string Notes { get; set; }
}
