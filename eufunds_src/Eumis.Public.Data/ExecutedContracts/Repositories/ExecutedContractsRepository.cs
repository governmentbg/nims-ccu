using Autofac.Extras.Attributed;
using Eumis.Public.Data.Core;
using Eumis.Public.Data.ExecutedContracts.ViewObjects;
using Eumis.Public.Data.Linq;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.Contracts;
using Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.FinancialCorrections;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.Programmes;
using Eumis.Public.Domain.Entities.Umis.Procedures;
using System.Linq;

namespace Eumis.Public.Data.ExecutedContracts.Repositories
{
    internal class ExecutedContractsRepository : Repository, IExecutedContractsRepository
    {
        public ExecutedContractsRepository([WithKey(DbKey.Umis)]IUnitOfWork uow)
            : base(uow)
        {
        }

        public PageVO<ExecutedContractVO> GetExecutedContracts(int? programmeId, int? procedureId, int? companyId, int offset, int? limit)
        {
            var contractPredicate = PredicateBuilder.True<Contract>()
                .AndEquals(c => c.ExecutionStatus, ContractExecutionStatus.Ended);

            if (programmeId != null)
            {
                contractPredicate = contractPredicate
                    .AndEquals(c => c.ProgrammeId, programmeId);
            }

            if (procedureId != null)
            {
                contractPredicate = contractPredicate
                    .AndEquals(c => c.ProcedureId, procedureId);
            }

            if (companyId != null)
            {
                contractPredicate = contractPredicate
                    .AndEquals(c => c.CompanyId, companyId);
            }

            var contracts = (from c in this.unitOfWork.DbContext.Set<Contract>().Where(contractPredicate)
                             join icv in this.unitOfWork.DbContext.Set<ContractVersionXml>() on c.ContractId equals icv.ContractId
                             join acv in this.unitOfWork.DbContext.Set<ContractVersionXml>() on c.ContractId equals acv.ContractId
                             join proc in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals proc.ProcedureId
                             join prog in this.unitOfWork.DbContext.Set<Programme>() on c.ProgrammeId equals prog.MapNodeId
                             join ct in this.unitOfWork.DbContext.Set<CompanyType>() on c.CompanyTypeId equals ct.CompanyTypeId
                             join clt in this.unitOfWork.DbContext.Set<CompanyLegalType>() on c.CompanyLegalTypeId equals clt.CompanyLegalTypeId
                             join cst in this.unitOfWork.DbContext.Set<CompanySizeType>() on c.CompanySizeTypeId equals cst.CompanySizeTypeId

                             join fc in this.unitOfWork.DbContext.Set<FinancialCorrection>().Where(fc => fc.Status == FinancialCorrectionStatus.Entered) on c.ContractId equals fc.ContractId into j1
                             from fc in j1.DefaultIfEmpty()
                             join fcv in this.unitOfWork.DbContext.Set<FinancialCorrectionVersion>() on fc.FinancialCorrectionId equals fcv.FinancialCorrectionId into j2
                             from fcv in j2.DefaultIfEmpty()

                             where icv.OrderNum == 1 && acv.Status == ContractVersionStatus.Active

                             group new
                             {
                                 TotalAmount = fcv.TotalAmount ?? 0,
                             }
                             by new
                             {
                                 ContractId = c.ContractId,
                                 ContractName = c.Name,
                                 ContractNameAlt = c.NameEN,
                                 ContractRegNumber = c.RegNumber,
                                 ProgrammeName = prog.Name,
                                 ProgrammeNameAlt = prog.NameAlt,
                                 ProcedureName = proc.Name,
                                 ProcedureNameAlt = proc.NameAlt,
                                 CompanyUin = c.CompanyUin,
                                 CompanyUinType = c.CompanyUinType,
                                 CompanyName = c.CompanyName,
                                 CompanyNameAlt = c.CompanyNameAlt,
                                 CompanyTypeName = ct.Name,
                                 CompanyTypeNameAlt = ct.NameAlt,
                                 CompanyLegalTypeName = clt.Name,
                                 CompanyLegalTypeNameAlt = clt.NameAlt,
                                 CompanySizeTypeName = cst.Name,
                                 CompanySizeTypeNameAlt = cst.NameAlt,
                                 ContractDuration = c.Duration,
                                 InitialContractDate = icv.ContractDate,
                                 InitialCompletionDate = icv.CompletionDate,
                                 ActualCompletionDate = acv.CompletionDate,
                                 ContractExecutionStatus = c.ExecutionStatus,
                             }
                             into g
                             select new
                             {
                                 ContractId = g.Key.ContractId,
                                 ContractName = g.Key.ContractName,
                                 ContractNameAlt = g.Key.ContractNameAlt,
                                 ContractRegNumber = g.Key.ContractRegNumber,
                                 ProgrammeName = g.Key.ProgrammeName,
                                 ProgrammeNameAlt = g.Key.ProgrammeNameAlt,
                                 ProcedureName = g.Key.ProcedureName,
                                 ProcedureNameAlt = g.Key.ProcedureNameAlt,
                                 CompanyUin = g.Key.CompanyUin,
                                 CompanyUinType = g.Key.CompanyUinType,
                                 CompanyName = g.Key.CompanyName,
                                 CompanyNameAlt = g.Key.CompanyNameAlt,
                                 CompanyTypeName = g.Key.CompanyTypeName,
                                 CompanyTypeNameAlt = g.Key.CompanyTypeNameAlt,
                                 CompanyLegalTypeName = g.Key.CompanyLegalTypeName,
                                 CompanyLegalTypeNameAlt = g.Key.CompanyLegalTypeNameAlt,
                                 CompanySizeTypeName = g.Key.CompanySizeTypeName,
                                 CompanySizeTypeNameAlt = g.Key.CompanySizeTypeNameAlt,
                                 ContractDuration = g.Key.ContractDuration,
                                 InitialContractDate = g.Key.InitialContractDate,
                                 InitialCompletionDate = g.Key.InitialCompletionDate,
                                 ContractExecutionStatus = g.Key.ContractExecutionStatus,
                                 ActualCompletionDate = g.Key.ActualCompletionDate,
                                 FinancialCorrectionTotalAmount = g.Sum(c => c.TotalAmount),
                             })
                             .Where(c => c.FinancialCorrectionTotalAmount <= 0 && c.InitialCompletionDate >= c.ActualCompletionDate)
                             .Select(c => new ExecutedContractVO()
                             {
                                 ContractId = c.ContractId,
                                 ContractName = c.ContractName,
                                 ContractNameAlt = c.ContractNameAlt,
                                 ContractRegNumber = c.ContractRegNumber,
                                 ProgrammeName = c.ProgrammeName,
                                 ProgrammeNameAlt = c.ProgrammeNameAlt,
                                 ProcedureName = c.ProcedureName,
                                 ProcedureNameAlt = c.ProcedureNameAlt,
                                 CompanyUin = c.CompanyUin,
                                 CompanyUinType = c.CompanyUinType,
                                 CompanyName = c.CompanyName,
                                 CompanyNameAlt = c.CompanyNameAlt,
                                 CompanyTypeName = c.CompanyTypeName,
                                 CompanyTypeNameAlt = c.CompanyTypeNameAlt,
                                 CompanyLegalTypeName = c.CompanyLegalTypeName,
                                 CompanyLegalTypeNameAlt = c.CompanyLegalTypeNameAlt,
                                 CompanySizeTypeName = c.CompanySizeTypeName,
                                 CompanySizeTypeNameAlt = c.CompanySizeTypeNameAlt,
                                 ContractDuration = c.ContractDuration,
                                 InitialContractDate = c.InitialContractDate,
                                 InitialCompletionDate = c.InitialCompletionDate,
                                 ActualCompletionDate = c.ActualCompletionDate,
                                 ContractExecutionStatus = c.ContractExecutionStatus,
                             })
                             .ToList();

            var contractsWithOffsetAndLimit = contracts
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            var result = new PageVO<ExecutedContractVO>() { Count = contracts.Count(), Results = contractsWithOffsetAndLimit };

            return result;
        }
    }
}
