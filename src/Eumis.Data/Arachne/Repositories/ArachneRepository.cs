using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Eumis.Common;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Domain.Arachne;
using Eumis.Domain.Companies;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Rio;

namespace Eumis.Data.Arachne.Repositories
{
    internal class ArachneRepository : Repository, IArachneRepository
    {
        private const string ArachneSource = "eumis2020.government.bg";

        private IAccessContext accessContext;

        public ArachneRepository(IUnitOfWork unitOfWork, IAccessContext accessContext)
            : base(unitOfWork)
        {
            this.accessContext = accessContext;
        }

        public ECDataExchangeXmlFormat GetArachneReport(int programmeId)
        {
            var programme = this.GetProgramme(programmeId);

            var contracts = this.GetContracts(programmeId).ToDictionary(cb => cb.ContractId);

            var contractBeneficiariesByContract = this.GetContractBeneficiaries(programmeId).ToDictionary(cb => cb.ContractId);

            var contractPartnersByContract = this.GetContractPartners(programmeId).GroupBy(p => p.ContractId).ToDictionary(g => g.Key, g => g.AsEnumerable());

            var contractContracts = this.GetContractContracts(programmeId).ToDictionary(cc => cc.ContractContractId);

            var subcontracts = this.GetSubcontracts(programmeId);

            var subcontractsByContractContract = subcontracts.GroupBy(sc => sc.ContractContractId).ToDictionary(g => g.Key, g => g.AsEnumerable());

            var contractContractors = this.GetContractContractors(programmeId).ToDictionary(cc => cc.ContractContractorId);

            var costSupportingDocuments = this.GetCostSupportingDocuments(programmeId);

            List<project> arachneProjects = new List<project>();

            Dictionary<string, entity> arachneEntities = new Dictionary<string, entity>();

            Dictionary<string, person> arachneEntityRepresentatives = new Dictionary<string, person>();

            Action<EntityDO, string, string> tryAddEntity = (entity, entityRole, projectId) =>
            {
                var id = entity.GetId();

                if (!arachneEntities.ContainsKey(id))
                {
                    arachneEntities.Add(id, entity.GetArachneEntity());
                }
            };

            List<person> otherRelatedPeople = new List<person>();

            foreach (var c in contracts.Values)
            {
                var beneficiary = contractBeneficiariesByContract[c.ContractId];

                tryAddEntity(beneficiary, "бенефициент", c.GetId());

                var partners = contractPartnersByContract.ContainsKey(c.ContractId) ? contractPartnersByContract[c.ContractId] : Enumerable.Empty<EntityDO>();

                foreach (var p in partners)
                {
                    tryAddEntity(p, "бенефициент", c.GetId());
                }

                var foundEmplFinanceSource = false;

                var dgType = foundEmplFinanceSource ? dglist.DG_EMPL : dglist.DG_REGIO;

                arachneProjects.Add(
                    new project
                    {
                        Id = c.GetId(),
                        Status =
                            c.ExecutionStatus == ContractExecutionStatus.Ended ? (int)ProjectStatus.Closed :
                            c.ExecutionStatus == ContractExecutionStatus.Canceled ? (int)ProjectStatus.Cancelled :
                            (int)ProjectStatus.Open,
                        Name = c.Name.TruncateWithEllipsis(300),
                        Beneficiary = new projectBeneficiary
                        {
                            EntityId = beneficiary.GetId(),
                            ApplicationTurnoverSpecified = false,
                        },
                        StartDate = c.StartDate ?? DateTime.MinValue,
                        StartDateSpecified = c.StartDate.HasValue,
                        EndDate = (c.ExecutionStatus == ContractExecutionStatus.Canceled ? c.TerminationDate : c.CompletionDate) ?? DateTime.MinValue,
                        EndDateSpecified = c.ExecutionStatus == ContractExecutionStatus.Canceled ? c.TerminationDate.HasValue : c.CompletionDate.HasValue,
                        ECFinancialAssistance = decimal.Round(c.TotalEuAmount ?? 0m, 2),
                        ECFinancialAssistanceSpecified = c.TotalEuAmount.HasValue,
                        OtherContributions = decimal.Round(c.TotalBgAmount ?? 0m + c.TotalSelfAmount ?? 0m, 2),
                        OtherContributionsSpecified = c.TotalBgAmount.HasValue || c.TotalSelfAmount.HasValue,
                        IncomeSpecified = false, // TODO Extract income
                        TotalCost = decimal.Round(c.TotalAmount ?? 0m, 2),
                        Partners = partners.Any() ? partners.Select(p => new projectPartner { Id = p.GetId() }).ToArray() : null,
                        Block = new block
                        {
                            Dg = dgType,
                            ProjectType = (int)ArachneProjectType.Other, // TODO extract from ContractVersionXml.BFPContractDimensionBudgetContract.InterventionCategoryDimensions
                        },
                    });

                // extract team members as other related people
                var contractTeamMembers = RioExtensions
                    .Deserialize<BFPContract>(c.Xml)
                    .Get(x => x.BFPContractContractTeams.BFPContractContractTeamCollection) ?? Enumerable.Empty<BFPContractContractTeam>();

                foreach (var tm in contractTeamMembers.Where(tm => !string.IsNullOrEmpty(tm.Name)))
                {
                    otherRelatedPeople.Add(new person
                    {
                        FirstName = string.Empty,
                        LastName = tm.Name,
                        BirthDateSpecified = false,
                        Roles =
                            new personRole[]
                            {
                                new personRole
                                {
                                    ProjectId = c.GetId(),
                                    Function = tm.Position,
                                },
                            },
                    });
                }
            }

            List<contract> arachneContracts = new List<contract>();

            foreach (var cc in contractContracts.Values)
            {
                var contract = contracts[cc.ContractId];

                var contractContractor = contractContractors[cc.ContractContractorId];

                tryAddEntity(contractContractor, "изпълнител", contract.GetId());

                var subcontractsForContract =
                    subcontractsByContractContract.ContainsKey(cc.ContractContractId) ?
                        subcontractsByContractContract[cc.ContractContractId] :
                        Enumerable.Empty<ContractSubcontractDO>();

                var consortiumMembers = subcontractsForContract
                    .Where(sc => sc.Type == ContractSubcontractType.Member)
                    .Select(sc => contractContractors[sc.ContractContractorId]);

                foreach (var cm in consortiumMembers)
                {
                    tryAddEntity(cm, "член на обединение", contract.GetId());
                }

                arachneContracts.Add(new contract
                {
                    Id = cc.GetId(),
                    ProjectId = contract.GetId(),
                    ContractorId = contractContractor.GetId(),
                    ContractType =
                        cc.ContractDifferentiatedPositionErrandAreaCode == "2" ? (int)ArachneContractType.Works :
                        (int)ArachneContractType.Other, // TODO check Supervision type
                    ContractTypeSpecified = true,
                    Modified = cc.NumberAnnexes > 0,
                    ModifiedSpecified = true,
                    SignatureDate = cc.SignDate,
                    SignatureDateSpecified = true,
                    InitialEndDate = cc.EndDate,
                    InitialEndDateSpecified = true,
                    FinalEndDate = cc.EndDate, // TODO extract new field from xml
                    FinalEndDateSpecified = true,
                    Name = cc.ContractDifferentiatedPositionName,
                    Description = cc.ContractProcurementPlanDescription,
                    Amount = decimal.Round(cc.TotalFundedValue, 2),
                    Addenda = cc.NumberAnnexes != 0 ?
                        new contractAddenda
                        {
                            Amount = decimal.Round(cc.CurrentAnnexTotalAmount, 2),
                            AmountSpecified = true,
                            Count = cc.NumberAnnexes.ToString(),
                        }
                        : null,
                    KeyExperts = null,
                    ConsortiumMembers = consortiumMembers.Any() ?
                        consortiumMembers
                        .Select(cm => new contractConsortiumMember
                        {
                            Id = cm.GetId(),
                        }).ToArray()
                        : null,
                    Procurement = new contractProcurement
                    {
                        ProcurementType =
                            cc.ContractDifferentiatedPositionErrandTypeCode == "01" ? (int)ArachneProcurementType.OpenProcedure :
                            cc.ContractDifferentiatedPositionErrandTypeCode == "02" ? (int)ArachneProcurementType.RestrictedProcedure :
                            cc.ContractDifferentiatedPositionErrandTypeCode == "04" || cc.ContractDifferentiatedPositionErrandTypeCode == "05" ? (int)ArachneProcurementType.NegotiatedProcedure :
                            cc.ContractDifferentiatedPositionErrandTypeCode == "06" ? (int)ArachneProcurementType.CompetitiveDialogue :
                            cc.ContractDifferentiatedPositionErrandTypeCode == "26" ? (int)ArachneProcurementType.DirectAward :
                            (int)ArachneProcurementType.Other,
                        ValidTenders = cc.ContractDifferentiatedPositionRankedOffersCount ?? 0,
                        ExcludedTenders = (cc.ContractDifferentiatedPositionSubmittedOffersCount ?? 0) - (cc.ContractDifferentiatedPositionRankedOffersCount ?? 0),
                        TotalTenders = cc.ContractDifferentiatedPositionSubmittedOffersCount ?? 0,
                    },
                });
            }

            List<subcontract> arachneSubcontracts = new List<subcontract>();

            // TODO check if consortium member contracts count as subcontracts
            foreach (var sc in subcontracts.Where(t => t.Type == ContractSubcontractType.Subcontractor))
            {
                var contract = contracts[sc.ContractId];

                var contractContract = contractContracts[sc.ContractContractId];

                var contractContractor = contractContractors[sc.ContractContractorId];

                tryAddEntity(contractContractor, "подизпълнител", contract.GetId());

                arachneSubcontracts.Add(new subcontract
                {
                    Id = sc.GetId(),
                    Amount = decimal.Round(sc.Amount, 2),
                    ContractId = contractContract.GetId(),
                    SubContractorId = contractContractor.GetId(),
                });
            }

            List<expense> arachneExpenses = new List<expense>();

            foreach (var csd in costSupportingDocuments)
            {
                var contract = contracts[csd.ContractId];

                arachneExpenses.Add(new expense
                {
                    Id = csd.GetId(),
                    ProjectId = contract.GetId(),
                    ContractorId = csd.ContractorUin,
                    InvoiceDate = csd.Date,
                    InvoiceDateSpecified = true,
                    PaymentDate = csd.PaymentDate,
                    PaymentDateSpecified = true,
                    Description = csd.Description,
                    Amount = decimal.Round(csd.Amount, 2),
                    Type = (int)ArachneExpenseTypes.Other, // TODO extract
                });
            }

            return new ECDataExchangeXmlFormat
            {
                Source = ArachneSource,
                Date = DateTime.Now,
                Author = "g.stratiev@government.bg", // TODO extract from user
                OperationalProgramId = programme.Code,
                ManagingAuthorityId = programme.ManagingAuthority,
                MemberState = iso31662.BG,
                Currency = currency.BGN,
                Projects = arachneProjects.ToArray(),
                Contracts = arachneContracts.ToArray(),
                SubContracts = arachneSubcontracts.ToArray(),
                Entities = arachneEntities.Values.ToArray(),
                RelatedPeople = arachneEntityRepresentatives.Values.Concat(otherRelatedPeople).ToArray(),
                Expenses = arachneExpenses.ToArray(),
            };
        }

        private ProgrammeDO GetProgramme(int programmeId)
        {
            return (
                from p in this.unitOfWork.DbContext.Set<Programme>()

                join c in this.unitOfWork.DbContext.Set<Eumis.Domain.Companies.Company>() on p.CompanyId equals c.CompanyId into g0
                from c in g0.DefaultIfEmpty()

                where p.MapNodeId == programmeId

                select new ProgrammeDO
                {
                    Code = p.Code,
                    ManagingAuthority = c.NameAlt,
                }).Single();
        }

        private IQueryable<Domain.Contracts.Contract> GetContractsQuery(int programmeId)
        {
            return
                from c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>()
                where c.ProgrammeId == programmeId && c.ContractStatus == ContractStatus.Entered
                select c;
        }

        private List<ContractDO> GetContracts(int programmeId)
        {
            return (
                from c in this.GetContractsQuery(programmeId)

                join cv in this.unitOfWork.DbContext.Set<ContractVersionXml>() on c.ContractId equals cv.ContractId

                where cv.Status == ContractVersionStatus.Active

                select new ContractDO
                {
                    ContractId = c.ContractId,
                    RegNumber = c.RegNumber,
                    ExecutionStatus = c.ExecutionStatus,
                    Name = c.Name,
                    StartDate = c.StartDate,
                    CompletionDate = c.CompletionDate,
                    TerminationDate = c.TerminationDate,
                    TotalEuAmount = c.TotalEuAmount,
                    TotalBgAmount = c.TotalBgAmount,
                    TotalSelfAmount = c.TotalSelfAmount,
                    TotalAmount = c.TotalAmount,
                    Xml = cv.Xml,
                }).ToList();
        }

        private List<EntityDO> GetContractPartners(int programmeId)
        {
            return (
                from c in this.GetContractsQuery(programmeId)
                join cp in this.unitOfWork.DbContext.Set<ContractPartner>() on c.ContractId equals cp.ContractId

                join co in this.unitOfWork.DbContext.Set<Country>() on cp.SeatCountryId equals co.CountryId into g0
                from co in g0.DefaultIfEmpty()

                join s in this.unitOfWork.DbContext.Set<Settlement>() on cp.SeatSettlementId equals s.SettlementId into g1
                from s in g1.DefaultIfEmpty()

                select new EntityDO
                {
                    ContractId = c.ContractId,
                    Uin = cp.Uin,
                    Name = cp.Name,
                    HasVatRegistration = false,
                    SeatCountryCode = co.NutsCode,
                    SeatSettlement = s.Name,
                    SeatPostCode = cp.SeatPostCode,
                    SeatStreet = cp.SeatStreet,
                    SeatAddress = cp.SeatAddress,
                    HasFinancialCorrection = false, // TODO do financial corrections affect partners?
                }).ToList();
        }

        private List<EntityDO> GetContractBeneficiaries(int programmeId)
        {
            var financialCorrectionsByContract =
                from fc in this.unitOfWork.DbContext.Set<FinancialCorrection>()
                group fc.FinancialCorrectionId by fc.ContractId into g
                select new
                {
                    ContractId = g.Key,
                };

            return (
                from c in this.GetContractsQuery(programmeId)
                join comp in this.unitOfWork.DbContext.Set<Domain.Companies.Company>() on c.CompanyId equals comp.CompanyId

                join co in this.unitOfWork.DbContext.Set<Country>() on c.BeneficiarySeatCountryId equals co.CountryId into g0
                from co in g0.DefaultIfEmpty()

                join s in this.unitOfWork.DbContext.Set<Settlement>() on c.BeneficiarySeatSettlementId equals s.SettlementId into g1
                from s in g1.DefaultIfEmpty()

                join fc in financialCorrectionsByContract on c.ContractId equals fc.ContractId into g2
                from fc in g2.DefaultIfEmpty()

                select new EntityDO
                {
                    ContractId = c.ContractId,
                    Uin = c.CompanyUin,
                    Name = c.CompanyName,
                    HasVatRegistration = false,
                    SeatCountryCode = co.NutsCode,
                    SeatSettlement = s.Name,
                    SeatPostCode = c.BeneficiarySeatPostCode,
                    SeatStreet = c.BeneficiarySeatStreet,
                    SeatAddress = c.BeneficiarySeatAddress,
                    HasFinancialCorrection = fc != null,
                }).ToList();
        }

        private List<ContractContractDO> GetContractContracts(int programmeId)
        {
            var firstDifferentiatedPositions =
                from cdp in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>()
                join cpp in this.unitOfWork.DbContext.Set<ContractProcurementPlan>() on cdp.ContractProcurementPlanId equals cpp.ContractProcurementPlanId
                join et in this.unitOfWork.DbContext.Set<ErrandType>() on cpp.ErrandTypeId equals et.ErrandTypeId
                join ea in this.unitOfWork.DbContext.Set<ErrandArea>() on cpp.ErrandAreaId equals ea.ErrandAreaId

                group new
                {
                    cdp.ContractContractId,

                    ContractProcurementPlanDescription = cpp.Description,

                    ContractDifferentiatedPositionName = cdp.Name,
                    ContractDifferentiatedPositionRankedOffersCount = cdp.RankedOffersCount,
                    ContractDifferentiatedPositionSubmittedOffersCount = cdp.SubmittedOffersCount,
                    ContractDifferentiatedPositionErrandTypeCode = et.Code,
                    ContractDifferentiatedPositionErrandAreaCode = ea.Code,
                }
                by cdp.ContractContractId into g

                select g.FirstOrDefault();

            return (
                from c in this.GetContractsQuery(programmeId)

                join cc in this.unitOfWork.DbContext.Set<ContractContract>() on c.ContractId equals cc.ContractId

                join cdp in firstDifferentiatedPositions on cc.ContractContractId equals cdp.ContractContractId

                select new ContractContractDO
                {
                    ContractId = c.ContractId,
                    ContractContractId = cc.ContractContractId,
                    ContractContractorId = cc.ContractContractorId,

                    Number = cc.Number,
                    SignDate = cc.SignDate,
                    EndDate = cc.EndDate,
                    TotalFundedValue = cc.TotalFundedValue,
                    NumberAnnexes = cc.NumberAnnexes,
                    CurrentAnnexTotalAmount = cc.CurrentAnnexTotalAmount,

                    ContractProcurementPlanDescription = cdp.ContractProcurementPlanDescription,

                    ContractDifferentiatedPositionName = cdp.ContractDifferentiatedPositionName,
                    ContractDifferentiatedPositionRankedOffersCount = cdp.ContractDifferentiatedPositionRankedOffersCount,
                    ContractDifferentiatedPositionSubmittedOffersCount = cdp.ContractDifferentiatedPositionSubmittedOffersCount,
                    ContractDifferentiatedPositionErrandTypeCode = cdp.ContractDifferentiatedPositionErrandTypeCode,
                    ContractDifferentiatedPositionErrandAreaCode = cdp.ContractDifferentiatedPositionErrandAreaCode,
                }).ToList();
        }

        private List<ContractSubcontractDO> GetSubcontracts(int programmeId)
        {
            var firstDifferentiatedPositions =
                from cdp in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>()

                group new
                {
                    cdp.ContractContractId,
                }
                by cdp.ContractContractId into g

                select g.FirstOrDefault();

            return (
                from c in this.GetContractsQuery(programmeId)

                join cc in this.unitOfWork.DbContext.Set<ContractContract>() on c.ContractId equals cc.ContractId

                // filter out conctractContracts without differentiated positions
                join cdp in firstDifferentiatedPositions on cc.ContractContractId equals cdp.ContractContractId

                join csc in this.unitOfWork.DbContext.Set<ContractSubcontract>() on cc.ContractContractId equals csc.ContractContractId

                select new ContractSubcontractDO
                {
                    ContractId = c.ContractId,
                    ContractContractId = cc.ContractContractId,
                    ContractContractorId = cc.ContractContractorId,
                    Number = csc.Number,
                    Amount = csc.Amount,
                    Type = csc.Type,
                }).ToList();
        }

        private List<EntityDO> GetContractContractors(int programmeId)
        {
            var financialCorrectionsByContractor =
                from fc in this.unitOfWork.DbContext.Set<FinancialCorrection>()
                join cc in this.unitOfWork.DbContext.Set<ContractContract>() on fc.ContractContractId equals cc.ContractContractId
                group fc.FinancialCorrectionId by cc.ContractContractorId into g
                select new
                {
                    ContractContractorId = g.Key,
                };

            var financialCorrectionsBySubcontractor =
                from fc in this.unitOfWork.DbContext.Set<FinancialCorrection>()
                join csc in this.unitOfWork.DbContext.Set<ContractSubcontract>() on fc.ContractContractId equals csc.ContractContractId
                group fc.FinancialCorrectionId by csc.ContractContractorId into g
                select new
                {
                    ContractContractorId = g.Key,
                };

            return (
                from c in this.GetContractsQuery(programmeId)

                join cctor in this.unitOfWork.DbContext.Set<Domain.Contracts.ContractContractor>() on c.ContractId equals cctor.ContractId

                join co in this.unitOfWork.DbContext.Set<Country>() on cctor.SeatCountryId equals co.CountryId into g0
                from co in g0.DefaultIfEmpty()

                join s in this.unitOfWork.DbContext.Set<Settlement>() on cctor.SeatSettlementId equals s.SettlementId into g1
                from s in g1.DefaultIfEmpty()

                join fc in financialCorrectionsByContractor on cctor.ContractContractorId equals fc.ContractContractorId into g2
                from fc in g2.DefaultIfEmpty()

                join fsc in financialCorrectionsBySubcontractor on cctor.ContractContractorId equals fsc.ContractContractorId into g3
                from fsc in g3.DefaultIfEmpty()

                select new EntityDO
                {
                    ContractId = c.ContractId,
                    ContractContractorId = cctor.ContractContractorId,
                    Uin = cctor.Uin,
                    Name = cctor.Name,
                    HasVatRegistration = cctor.VATRegistration == YesNoNonApplicable.Yes,
                    SeatCountryCode = co.NutsCode,
                    SeatSettlement = s.Name,
                    SeatPostCode = cctor.SeatPostCode,
                    SeatStreet = cctor.SeatStreet,
                    SeatAddress = cctor.SeatAddress,
                    HasFinancialCorrection = fc != null || fsc != null,
                }).ToList();
        }

        private List<CostSupportingDocumentDO> GetCostSupportingDocuments(int programmeId)
        {
            return (
                from c in this.GetContractsQuery(programmeId)

                join cr in this.unitOfWork.DbContext.Set<ContractReport>() on c.ContractId equals cr.ContractId

                join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on cr.ContractReportId equals csd.ContractReportId

                join csdbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on csd.ContractReportFinancialCSDId equals csdbi.ContractReportFinancialCSDId

                join cctor in this.unitOfWork.DbContext.Set<Domain.Contracts.ContractContractor>() on csd.CompanyGid equals cctor.Gid into g0
                from cctor in g0.DefaultIfEmpty()

                join cp in this.unitOfWork.DbContext.Set<Domain.Contracts.ContractPartner>() on csd.CompanyGid equals cp.Gid into g1
                from cp in g1.DefaultIfEmpty()

                where cr.Status == ContractReportStatus.Accepted

                group new
                {
                    csdbi.TotalAmount,
                }
                by new
                {
                    c.ContractId,
                    ContractorUin = cctor.Uin ?? cp.Uin ?? c.CompanyUin,
                    csd.ContractReportFinancialCSDId,
                    csd.Number,
                    csd.Date,
                    csd.PaymentDate,
                    csd.Description,
                }
                into g
                select new CostSupportingDocumentDO
                {
                    ContractId = g.Key.ContractId,
                    ContractorUin = g.Key.ContractorUin,
                    ContractReportFinancialCSDId = g.Key.ContractReportFinancialCSDId,
                    Date = g.Key.Date,
                    PaymentDate = g.Key.PaymentDate,
                    Description = g.Key.Description,
                    Amount = g.Sum(t => t.TotalAmount),
                }).ToList();
        }

        private class ContractDO
        {
            public int ContractId { get; set; }

            public string RegNumber { get; set; }

            public ContractExecutionStatus? ExecutionStatus { get; set; }

            public string Name { get; set; }

            public DateTime? StartDate { get; set; }

            public DateTime? CompletionDate { get; set; }

            public DateTime? TerminationDate { get; set; }

            public decimal? TotalEuAmount { get; set; }

            public decimal? TotalBgAmount { get; set; }

            public decimal? TotalSelfAmount { get; set; }

            public decimal? TotalAmount { get; set; }

            public string Xml { get; set; }

            public string GetId()
            {
                return Regex.Replace(this.RegNumber, @"-C\d+$", string.Empty);
            }
        }

        private class EntityDO
        {
            public int ContractId { get; set; }

            public int ContractContractorId { get; set; }

            public string Uin { get; set; }

            public string Name { get; set; }

            public bool HasVatRegistration { get; set; }

            public string SeatCountryCode { get; set; }

            public string SeatSettlement { get; set; }

            public string SeatPostCode { get; set; }

            public string SeatStreet { get; set; }

            public string SeatAddress { get; set; }

            public bool HasFinancialCorrection { get; set; }

            public string GetId()
            {
                return this.Uin;
            }

            public entity GetArachneEntity()
            {
                bool isBgEntity = false;
                address address = null;
                if (Enum.TryParse<iso31662>(this.SeatCountryCode, true, out var country))
                {
                    if (country == iso31662.BG)
                    {
                        isBgEntity = true;

                        address = new address
                        {
                            Street = this.SeatStreet.TruncateWithEllipsis(255),
                            Number = string.Empty,
                            ZipCode = this.SeatPostCode.TruncateWithEllipsis(16),
                            City = this.SeatSettlement.TruncateWithEllipsis(150),
                            Country = country,
                        };
                    }
                    else
                    {
                        address = new address
                        {
                            Street = this.SeatAddress.TruncateWithEllipsis(255),
                            Number = string.Empty,
                            ZipCode = string.Empty,
                            City = string.Empty,
                            Country = country,
                        };
                    }
                }

                return new entity
                {
                    Id = this.GetId(),
                    Name = this.Name.TruncateWithEllipsis(200),
                    VAT = this.HasVatRegistration ? (isBgEntity ? ("BG" + this.Uin) : this.Uin).Truncate(20) : (string)null,
                    Type = (int)ArachneEntityType.Unknown,
                    CorrProcurementProcedure = this.HasFinancialCorrection,
                    CorrProcurementProcedureSpecified = true,
                    TurnoverSpecified = false,
                    Address = address,
                };
            }
        }

        private class ContractContractDO
        {
            public int ContractId { get; set; }

            public int ContractContractId { get; set; }

            public int ContractContractorId { get; set; }

            public string Number { get; set; }

            public DateTime SignDate { get; set; }

            public DateTime EndDate { get; set; }

            public decimal TotalFundedValue { get; set; }

            public int NumberAnnexes { get; set; }

            public decimal CurrentAnnexTotalAmount { get; set; }

            public string ContractProcurementPlanDescription { get; set; }

            public string ContractDifferentiatedPositionName { get; set; }

            public int? ContractDifferentiatedPositionRankedOffersCount { get; set; }

            public int? ContractDifferentiatedPositionSubmittedOffersCount { get; set; }

            public string ContractDifferentiatedPositionErrandTypeCode { get; set; }

            public string ContractDifferentiatedPositionErrandAreaCode { get; set; }

            public string GetId()
            {
                return this.Number;
            }
        }

        private class ContractSubcontractDO
        {
            public int ContractId { get; set; }

            public int ContractContractId { get; set; }

            public int ContractContractorId { get; set; }

            public string Number { get; set; }

            public decimal Amount { get; set; }

            public ContractSubcontractType Type { get; set; }

            public string GetId()
            {
                return this.Number;
            }
        }

        private class ProgrammeDO
        {
            public string Code { get; set; }

            public string ManagingAuthority { get; set; }
        }

        private class CostSupportingDocumentDO
        {
            public int ContractId { get; set; }

            public string ContractorUin { get; set; }

            public int ContractReportFinancialCSDId { get; set; }

            public DateTime Date { get; set; }

            public DateTime PaymentDate { get; set; }

            public string Description { get; set; }

            public decimal Amount { get; set; }

            public string GetId()
            {
                return this.ContractReportFinancialCSDId.ToString();
            }
        }
    }
}
