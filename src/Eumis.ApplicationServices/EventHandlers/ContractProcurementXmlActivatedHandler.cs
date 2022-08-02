using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.Companies;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using Eumis.Domain.Events;
using Eumis.Domain.NonAggregates;
using Eumis.Rio;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class ContractProcurementXmlActivatedHandler : Eumis.Domain.Core.EventHandler<ContractProcurementXmlActivatedEvent>
    {
        private IContractsRepository contractsRepository;
        private IContractProcurementsRepository contractProcurementsRepository;
        private IEntityCodeNomsRepository<Country, EntityCodeNomVO> countryNomsRepository;
        private ISettlementNomsRepository settlementNomsRepository;
        private IEntityCodeNomsRepository<ErrandType, EntityCodeNomVO> errandTypeNomsRepository;
        private IEntityCodeNomsRepository<ErrandArea, EntityCodeNomVO> errandAreaNomsRepository;
        private IEntityGidNomsRepository<ErrandLegalAct, EntityGidNomVO> errandLegalActNomsRepository;

        public ContractProcurementXmlActivatedHandler(
            IContractsRepository contractsRepository,
            IContractProcurementsRepository contractProcurementsRepository,
            IEntityCodeNomsRepository<Country, EntityCodeNomVO> countryNomsRepository,
            ISettlementNomsRepository settlementNomsRepository,
            IEntityCodeNomsRepository<ErrandType, EntityCodeNomVO> errandTypeNomsRepository,
            IEntityCodeNomsRepository<ErrandArea, EntityCodeNomVO> errandAreaNomsRepository,
            IEntityGidNomsRepository<ErrandLegalAct, EntityGidNomVO> errandLegalActNomsRepository)
        {
            this.contractsRepository = contractsRepository;
            this.contractProcurementsRepository = contractProcurementsRepository;
            this.countryNomsRepository = countryNomsRepository;
            this.settlementNomsRepository = settlementNomsRepository;
            this.errandTypeNomsRepository = errandTypeNomsRepository;
            this.errandAreaNomsRepository = errandAreaNomsRepository;
            this.errandLegalActNomsRepository = errandLegalActNomsRepository;
        }

        public override void Handle(ContractProcurementXmlActivatedEvent e)
        {
            var contract = this.contractsRepository.Find(e.ContractId);
            var contractProcurement = this.contractProcurementsRepository.Find(e.ContractProcurementXmlId);
            var doc = contractProcurement.GetDocument();
            bool hasToUpdateXml = false;

            if (doc.Contractors != null && doc.Contractors.ContractorCollection != null)
            {
                foreach (var c in doc.Contractors.ContractorCollection)
                {
                    contract.AddOrUpdateContractor(
                        Guid.Parse(c.gid),
                        c.isActive,
                        c.Uin,
                        c.GetEnum<Rio.Contractor, UinType>(c1 => c1.UinType.Id).Value,
                        c.Name,
                        c.NameEN,
                        c.GetPublicNomId(d => d.Seat.Country, this.countryNomsRepository.GetNomIdByCode),
                        c.GetPublicNomId(d => d.Seat.Settlement, this.settlementNomsRepository.GetNomIdByCode),
                        c.Get(d => d.Seat.PostCode),
                        c.Get(d => d.Seat.Street),
                        c.Get(d => d.Seat.FullAddress),
                        c.GetEnum<Rio.Contractor, YesNoNonApplicable>(c1 => c1.VATRegistration.Value).Value);
                }
            }

            if (doc.ContractContractors != null && doc.ContractContractors.ContractContractorCollection != null)
            {
                foreach (var cc in doc.ContractContractors.ContractContractorCollection)
                {
                    var subcontracts = new List<Tuple<Guid, ContractSubcontractType, DateTime, string, decimal>>();
                    if (cc.SubcontractorMemberCollection != null)
                    {
                        foreach (var sc in cc.SubcontractorMemberCollection)
                        {
                            subcontracts.Add(
                                Tuple.Create(
                                    Guid.Parse(sc.Contractor.Id),
                                    sc.GetEnum<Rio.SubcontractorMember, ContractSubcontractType>(c1 => c1.Type.Value).Value,
                                    sc.ContractDate.Value,
                                    sc.ContractNumber,
                                    sc.ContractAmount));
                        }
                    }

                    var activities = new List<Tuple<Guid?, Guid>>();
                    if (cc.ActivitiesBudgetDetailsRefCollection != null)
                    {
                        foreach (var a in cc.ActivitiesBudgetDetailsRefCollection)
                        {
                            activities.Add(
                                Tuple.Create(
                                    (a.ContractActivity == null || string.IsNullOrEmpty(a.ContractActivity.Id)) ? (Guid?)null : Guid.Parse(a.ContractActivity.Id),
                                    Guid.Parse(a.BudgetDetail.Id)));
                        }
                    }

                    contract.AddOrUpdateContract(
                        Guid.Parse(cc.gid),
                        cc.isActive,
                        cc.SignDate.Value,
                        cc.Number,
                        cc.TotalAmountExcludingVAT,
                        cc.VATAmountIfEligible,
                        cc.TotalFundedValue,
                        int.Parse(cc.NumberAnnexes),
                        cc.CurrentAnnexTotalAmount,
                        cc.Comment,
                        cc.StartDate.Value,
                        cc.EndDate.Value,
                        cc.HasSubcontractorMember,
                        Guid.Parse(cc.Contractor.Id),
                        subcontracts,
                        activities);
                }
            }

            if (doc.ProcurementPlans != null && doc.ProcurementPlans.ProcurementPlanCollection != null)
            {
                var existingPlansAndPositions =
                    doc.ProcurementPlans.ProcurementPlanCollection
                    .ToDictionary(
                        p => Guid.Parse(p.gid),
                        p => p.DifferentiatedPositionCollection == null ?
                            new List<Guid>() :
                            p.DifferentiatedPositionCollection.Select(dp => Guid.Parse(dp.gid)).ToList());

                contract.ClearMissingPlansAndPositions(existingPlansAndPositions);

                foreach (var plan in doc.ProcurementPlans.ProcurementPlanCollection)
                {
                    var planGid = Guid.Parse(plan.gid);

                    if (plan.IsAnnounced && !plan.AnnouncedDate.HasValue)
                    {
                        hasToUpdateXml = true;
                        plan.AnnouncedDate = DateTime.Now;
                        plan.NoticeDate = DateTime.Now;
                    }

                    if (plan.IsTerminated && !plan.TerminatedDate.HasValue)
                    {
                        hasToUpdateXml = true;
                        plan.TerminatedDate = DateTime.Now;
                    }

                    contract.AddOrUpdateContractProcurementPlan(
                        planGid,
                        plan.BFPContractPlan.Name,
                        plan.BFPContractPlan.GetPublicNomId(d => d.ErrandArea, this.errandAreaNomsRepository.GetNomIdByCode).Value,
                        plan.BFPContractPlan.GetPrivateNomId(d => d.ErrandLegalAct, this.errandLegalActNomsRepository.GetNomIdByGid).Value,
                        plan.BFPContractPlan.GetPublicNomId(d => d.ErrandType, this.errandTypeNomsRepository.GetNomIdByCode).Value,
                        plan.BFPContractPlan.Description,
                        plan.ExpectedAmount,
                        plan.PPANumber,
                        plan.NoticeDate,
                        plan.OffersDeadlineDate,
                        plan.AnnouncedDate,
                        plan.TerminatedDate);

                    if (plan.DifferentiatedPositionCollection != null)
                    {
                        foreach (var pos in plan.DifferentiatedPositionCollection)
                        {
                            contract.AddOrUpdateContractDifferentiatedPosition(
                                planGid,
                                Guid.Parse(pos.gid),
                                pos.ContractContractor == null || string.IsNullOrWhiteSpace(pos.ContractContractor.Id) ?
                                    (Guid?)null :
                                    Guid.Parse(pos.ContractContractor.Id),
                                pos.Name,
                                string.IsNullOrWhiteSpace(pos.SubmittedOffersCount) ?
                                    (int?)null :
                                    int.Parse(pos.SubmittedOffersCount),
                                string.IsNullOrWhiteSpace(pos.RankedOffersCount) ?
                                    (int?)null :
                                    int.Parse(pos.RankedOffersCount),
                                pos.Comment);
                        }
                    }
                }
            }

            if (hasToUpdateXml)
            {
                contractProcurement.SetXml(doc);
            }
        }
    }
}
