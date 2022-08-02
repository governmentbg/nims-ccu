using Eumis.Common.Db;
using Eumis.Data.Irregularities.ViewObjects;
using Eumis.Data.Linq;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using Eumis.Domain.Irregularities;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Irregularities.Repositories
{
    internal class IrregularitySignalsRepository : AggregateRepository<IrregularitySignal>, IIrregularitySignalsRepository
    {
        public IrregularitySignalsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<IrregularitySignal, object>>[] Includes
        {
            get
            {
                return new Expression<Func<IrregularitySignal, object>>[]
                {
                    irrs => irrs.Documents,
                    irrs => irrs.InvolvedPersons,
                };
            }
        }

        public IList<IrregularitySignalVO> GetIrregularitySignals(
            int[] programmeIds,
            int userId,
            int? contractId = null,
            IrregularitySignalStatus? status = null)
        {
            var basePredicate = PredicateBuilder.True<IrregularitySignal>()
                .AndEquals(irrs => irrs.ContractId, contractId)
                .AndEquals(irrs => irrs.Status, status);

            var externalVerificatorIrregulatySignals = from cu in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.UserId == userId)
                                                       join irs in this.unitOfWork.DbContext.Set<IrregularitySignal>().Where(basePredicate) on cu.ContractId equals irs.ContractId
                                                       select irs;

            var predicate = basePredicate
                .And(irrs => programmeIds.Contains(irrs.ProgrammeId));

            return (from irrs in this.unitOfWork.DbContext.Set<IrregularitySignal>().Where(predicate).Union(externalVerificatorIrregulatySignals)
                    join irr in this.unitOfWork.DbContext.Set<Irregularity>() on irrs.IrregularitySignalId equals irr.IrregularitySignalId into g1
                    from irr in g1.DefaultIfEmpty()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on irrs.ContractId equals c.ContractId into g2
                    from c in g2.DefaultIfEmpty()
                    join p in this.unitOfWork.DbContext.Set<Project>() on irrs.ProjectId equals p.ProjectId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on irrs.ProgrammeId equals pr.MapNodeId
                    orderby irrs.CreateDate descending
                    select new IrregularitySignalVO
                    {
                        IrregularitySignalId = irrs.IrregularitySignalId,
                        IrregularitySignalRegNumber = irrs.RegNumber,
                        ProgrammeName = pr.Name,
                        ContractRegNumber = c.RegNumber,
                        ProjectRegNumber = p.RegNumber,
                        StatusDescr = irrs.Status,
                        Status = irrs.Status,
                        IsIrregularityFound = irr != null && irr.Status == IrregularityStatus.Entered,
                        IrregularityRegNumber = irr.RegNumber,
                    })
                    .Distinct()
                    .ToList();
        }

        public IrregularitySignalInfoVO GetInfo(int irregularitySignalId)
        {
            return (from irrs in this.unitOfWork.DbContext.Set<IrregularitySignal>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on irrs.ContractId equals c.ContractId into g1
                    from c in g1.DefaultIfEmpty()
                    join p in this.unitOfWork.DbContext.Set<Project>() on irrs.ProjectId equals p.ProjectId
                    where irrs.IrregularitySignalId == irregularitySignalId
                    select new IrregularitySignalInfoVO
                    {
                        ContractNum = c.RegNumber,
                        Status = irrs.Status,
                        StatusDescr = irrs.Status,
                        ProjectNum = p.RegNumber,
                    }).Single();
        }

        public IrregularitySignalBasicDataVO GetBasicData(int irregularitySignalId)
        {
            return (from irrs in this.unitOfWork.DbContext.Set<IrregularitySignal>()
                    join irr in this.unitOfWork.DbContext.Set<Irregularity>() on irrs.IrregularitySignalId equals irr.IrregularitySignalId into g1
                    from irr in g1.DefaultIfEmpty()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on irrs.ContractId equals c.ContractId into g2
                    from c in g2.DefaultIfEmpty()
                    join p in this.unitOfWork.DbContext.Set<Project>() on irrs.ProjectId equals p.ProjectId
                    where irrs.IrregularitySignalId == irregularitySignalId
                    select new IrregularitySignalBasicDataVO
                    {
                        SignalId = irrs.IrregularitySignalId,
                        SignalRegNumber = irrs.RegNumber,
                        IsIrregularityFound = irr != null && irr.Status == IrregularityStatus.Entered,
                        IrregularityNumber = irr.RegNumber,
                        Status = irrs.Status,
                        ProgrammeId = irrs.ProgrammeId,
                        ProcedureId = irrs.ProcedureId,

                        ProjectName = p.Name,
                        ProjectRegNumber = p.RegNumber,
                        CompanyName = p.CompanyName,
                        CompanyUin = p.CompanyUin,
                        CompanyUinType = p.CompanyUinType,

                        ContractName = c.Name,
                        ContractRegNumber = c.RegNumber,
                        BeneficiaryName = c.CompanyName,
                        BeneficiaryUin = c.CompanyUin,
                        BeneficiaryUinType = c.CompanyUinType,

                        IsActivated = irrs.IsActivated,
                        DeleteNote = irrs.DeleteNote,
                        Version = irrs.Version,
                    }).Single();
        }

        public IList<IrregularityInvolvedPersonVO> GetInvolvedPersons(int irregularitySignalId)
        {
            return (from iip in this.unitOfWork.DbContext.Set<IrregularitySignalInvolvedPerson>()
                    where iip.IrregularitySignalId == irregularitySignalId
                    orderby iip.IrregularitySignalInvolvedPersonId descending
                    select new
                    {
                        iip.IrregularitySignalInvolvedPersonId,
                        iip.LegalType,
                        iip.Uin,
                        iip.UinType,
                        iip.FirstName,
                        iip.MiddleName,
                        iip.LastName,
                        iip.CompanyName,
                        iip.TradeName,
                        iip.HoldingName,
                    }).ToList()
                    .Select(o => new IrregularityInvolvedPersonVO
                    {
                        PersonId = o.IrregularitySignalInvolvedPersonId,
                        Uin = o.Uin,
                        UinType = o.UinType,
                        LegalType = o.LegalType,
                        Name = o.LegalType == InvolvedPersonLegalType.Person ?
                            string.Format("{0} {1} {2}", o.FirstName, o.MiddleName, o.LastName) :
                            null,
                        CompanyName = o.CompanyName,
                        TradeName = o.TradeName,
                        HoldingName = o.HoldingName,
                    }).ToList();
        }

        public IList<IrregularityDocVO> GetDocuments(int irregularitySignalId)
        {
            return (from isd in this.unitOfWork.DbContext.Set<IrregularitySignalDoc>()
                    where isd.IrregularitySignalId == irregularitySignalId
                    orderby isd.IrregularitySignalDocId descending
                    select new IrregularityDocVO
                    {
                        DocumentId = isd.IrregularitySignalDocId,
                        Description = isd.Description,
                        File = new FileVO
                        {
                            Key = isd.FileKey,
                            Name = isd.FileName,
                        },
                    }).ToList();
        }

        public int GetProgrammeId(int irregularitySignalId)
        {
            return (from a in this.unitOfWork.DbContext.Set<IrregularitySignal>()
                    where a.IrregularitySignalId == irregularitySignalId
                    select a.ProgrammeId).Single();
        }

        public bool HasAssociatedIrregularity(int irregularitySignalId)
        {
            return (from irr in this.unitOfWork.DbContext.Set<Irregularity>()
                    where irr.IrregularitySignalId == irregularitySignalId
                    select irr.IrregularityId).Any();
        }

        public bool HasAssociatedNonRemovedIrregularity(int irregularitySignalId)
        {
            return (from irr in this.unitOfWork.DbContext.Set<Irregularity>()
                    where irr.IrregularitySignalId == irregularitySignalId && irr.Status != IrregularityStatus.Removed
                    select irr.IrregularityId).Any();
        }

        public bool HasNonRemovedIrregularityWithTheSameNumber(int programmeId, int irregularitySignalId, string signalNumber)
        {
            return (from irr in this.unitOfWork.DbContext.Set<IrregularitySignal>()
                    where irr.ProgrammeId == programmeId && irr.Status != IrregularitySignalStatus.Removed && irr.Status != IrregularitySignalStatus.Draft && irr.RegNumber == signalNumber
                    select irr.RegNumber).Any();
        }

        public bool HasRemovedIrregularityWithTheSameNumber(int programmeId, int irregularitySignalId, string signalNumber)
        {
            return (from irr in this.unitOfWork.DbContext.Set<IrregularitySignal>()
                    where irr.ProgrammeId == programmeId && irr.Status == IrregularitySignalStatus.Removed && irr.RegNumber == signalNumber
                    select irr.RegNumber).Any();
        }

        public new void Remove(IrregularitySignal signal)
        {
            if (signal.Status != IrregularitySignalStatus.Draft || signal.IsActivated)
            {
                throw new DomainValidationException("Cannot delete nondraft irregularity signal or signal which has been set to ended.");
            }

            base.Remove(signal);
        }

        public IList<IrregularitySignalVO> GetIrregularitySignalsForProjectDossier(int projectId, int? contractId)
        {
            return (from irrs in this.unitOfWork.DbContext.Set<IrregularitySignal>()
                    join irr in this.unitOfWork.DbContext.Set<Irregularity>() on irrs.IrregularitySignalId equals irr.IrregularitySignalId into g1
                    from irr in g1.DefaultIfEmpty()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on irrs.ContractId equals c.ContractId into g2
                    from c in g2.DefaultIfEmpty()
                    join p in this.unitOfWork.DbContext.Set<Project>() on irrs.ProjectId equals p.ProjectId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on irrs.ProgrammeId equals pr.MapNodeId
                    where irrs.ProjectId == projectId &&
                          (!irrs.ContractId.HasValue || (contractId.HasValue && irrs.ContractId == contractId.Value)) &&
                          irrs.Status != IrregularitySignalStatus.Draft
                    orderby irrs.CreateDate descending
                    select new IrregularitySignalVO
                    {
                        IrregularitySignalId = irrs.IrregularitySignalId,
                        ProgrammeName = pr.Name,
                        ContractRegNumber = c.RegNumber,
                        ProjectRegNumber = p.RegNumber,
                        StatusDescr = irrs.Status,
                        Status = irrs.Status,
                        IsIrregularityFound = irr != null && irr.Status == IrregularityStatus.Entered,
                    }).ToList();
        }

        public IList<IrregularitySignalRegisterItemVO> GetIrregularitySignalRegister(
            int[] programmeIds,
            int userId,
            int? irregularitySignalId = null)
        {
            var basePredicate = PredicateBuilder.True<IrregularitySignal>()
                .AndEquals(irrs => irrs.IrregularitySignalId, irregularitySignalId);

            var externalVerificatorIrregularitySingnals = from cu in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.UserId == userId)
                                                          join rs in this.unitOfWork.DbContext.Set<IrregularitySignal>().Where(basePredicate) on cu.ContractId equals rs.ContractId
                                                          where rs.Status == IrregularitySignalStatus.Active || rs.Status == IrregularitySignalStatus.Ended
                                                          select rs;

            var predicate = basePredicate
                .And(irrs => programmeIds.Contains(irrs.ProgrammeId));

            return (from irrs in this.unitOfWork.DbContext.Set<IrregularitySignal>().Where(predicate).Union(externalVerificatorIrregularitySingnals)
                    join irr in this.unitOfWork.DbContext.Set<Irregularity>() on irrs.IrregularitySignalId equals irr.IrregularitySignalId into g1
                    from irr in g1.DefaultIfEmpty()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on irrs.ContractId equals c.ContractId into g2
                    from c in g2.DefaultIfEmpty()
                    join p in this.unitOfWork.DbContext.Set<Project>() on irrs.ProjectId equals p.ProjectId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on irrs.ProgrammeId equals pr.MapNodeId
                    where irrs.Status == IrregularitySignalStatus.Active || irrs.Status == IrregularitySignalStatus.Ended
                    orderby irrs.CreateDate descending
                    select new IrregularitySignalRegisterItemVO
                    {
                        IrregularitySignalId = irrs.IrregularitySignalId,
                        IrregularitySignalRegNumber = irrs.RegNumber,
                        MASystemRegDate = irrs.MASystemRegDate,
                        ProgrammeName = pr.Name,
                        ContractName = c.Name,
                        ContractRegNumber = c.RegNumber,
                        ProjectName = p.Name,
                        ProjectRegNumber = p.RegNumber,
                        ViolationDesrc = irrs.ViolationDesrc,
                        SignalSource = irrs.SignalSource,
                        Actions = irrs.Actions,
                        Status = irrs.Status,
                        ActRegNum = irrs.ActRegNum,
                        ActRegDate = irrs.ActRegDate,
                        Comment = irrs.Comment,
                        IsIrregularityFound = irr != null && irr.Status == IrregularityStatus.Entered,
                        IrregularityRegNumber = irr != null ? irr.RegNumber : null,
                    })
                    .Distinct()
                    .ToList();
        }

        public IList<IrregularitySignalRegisterInvolvedPersonVO> GetSignalReportInvolvedPersons(int[] irregularitySignalIds)
        {
            return (from isp in this.unitOfWork.DbContext.Set<IrregularitySignalInvolvedPerson>()
                    join isig in this.unitOfWork.DbContext.Set<IrregularitySignal>() on isp.IrregularitySignalId equals isig.IrregularitySignalId
                    where irregularitySignalIds.Contains(isig.IrregularitySignalId)
                    select new IrregularitySignalRegisterInvolvedPersonVO
                    {
                        PersonId = isp.IrregularitySignalInvolvedPersonId,
                        IrregularitySignalId = isp.IrregularitySignalId,
                        IrregularitySignalRegNum = isig.RegNumber,
                        Uin = isp.Uin,
                        UinType = isp.UinType,
                        LegalType = isp.LegalType,
                        FirstName = isp.FirstName,
                        MiddleName = isp.MiddleName,
                        LastName = isp.LastName,
                        CompanyName = isp.CompanyName,
                        HoldingName = isp.HoldingName,
                        TradeName = isp.TradeName,
                    }).ToList();
        }

        public int? GetContractId(int irregularitySignalId)
        {
            return (from a in this.unitOfWork.DbContext.Set<IrregularitySignal>()
                    where a.IrregularitySignalId == irregularitySignalId
                    select a.ContractId).Single();
        }
    }
}
