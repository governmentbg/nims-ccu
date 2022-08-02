using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Eumis.Common.Json;
using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Contracts.Views;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;

namespace Eumis.Data.Contracts.Repositories
{
    public interface IContractsRepository : IAggregateRepository<Contract>
    {
        Contract Find(Guid gid);

        Task<Contract> FindAsync(Guid gid, CancellationToken ct);

        Contract FindByRegNumber(string regNumber);

        IList<ContractVO> GetContracts(
            int[] programmeIds,
            int? programmePriorityId,
            int? procedureId = null,
            bool includeDrafts = false,
            string contractNumber = null,
            int? userId = null);

        IList<ContractPhysicalExecutionActivityVO> GetContractPhysicalExecutionActivitiesForProjectDossier(int contractId);

        IList<string> CanDeleteContract(int contractId);

        IList<ContractVO> GetUserAvailableContracts(int[] programmeIds, int userId, string contractNumber);

        PagePVO<ContractPVO> GetPortalContractsForRegistration(int contractRegistrationId, int offset = 0, int? limit = null);

        PagePVO<ContractPVO> GetPortalContractsForAccessCode(int contractId, int offset = 0, int? limit = null);

        ContractPVO GetPortalContractForRegistration(Guid contractGid, int contractRegistrationId);

        ContractPVO GetPortalContractForAccessCode(Guid contractGid, int contractId);

        int GetProgrammeId(int contractId);

        ContractStatus GetContractStatus(int contractId);

        int GetProcedureId(int contractId);

        string GetRegNumber(int contractId);

        bool ProjectHasContractForProgramme(int projectId, int programmeId);

        IList<ContractContractRegistrationsVO> GetContractContractRegistrations(int contractId);

        int GetContractContractRegistrationId(Guid contractGid, int contractRegistrationId);

        IList<string> GetContractAccessCodesEmails(Guid contractGid);

        Task<VwAccessCode> GetContractAccessCodeAsync(string email, string regNumber, string code);

        int GetContractId(Guid contractGid);

        Task<int> GetContractIdAsync(Guid contractGid, CancellationToken ct);

        int GetContractContractContractId(int contractContractId);

        ContractDataDO GetContractData(int projectId, int programmeId);

        ContractDataDO GetContractData(int contractId);

        bool IsContractNumExisting(string contractNum, int? procedureId = null, int? projectId = null, int? programmeId = null);

        IList<InterventionCategory> GetInterventionCategories(IList<Tuple<Dimension, string>> categories);

        IList<ContractItemVO> GetContractItems(int programmeId, int[] exceptIds);

        IList<ContractContractItemVO> GetContractContractItems(int contractId, int[] exceptIds);

        IList<ContractGrantDocumentsVO> GetContractGrantDocuments(int certReportId);

        IList<ContractProcurementDocumentsVO> GetContractProcurementDocuments(int certReportId);

        PagePVO<ContractDifferentiatedPositionPVO> GetAnnouncedContractDifferentiatedPositions(
            int offset = 0,
            int? limit = null,
            string dpName = null,
            string name = null,
            string companyName = null,
            DateTime? offersDeadlineDate = null,
            DateTime? noticeDate = null,
            string sortBy = null,
            SortOrder? sortOrder = null);

        PagePVO<ContractDifferentiatedPositionPVO> GetArchivedContractDifferentiatedPositions(
            int offset = 0,
            int? limit = null,
            string dpName = null,
            string name = null,
            string companyName = null);

        ContractDifferentiatedPositionPVO GetContractDifferentiatedPosition(Guid dpGid);

        ContractDifferentiatedPosition GetContractDifferentiatedPosition(int contractDifferentiatedPositionId);

        ContractDifferentiatedPosition FindContractDifferentiatedPosition(Guid dpGid);

        (string ContractVersionXml, string ContractProcurementXml, Guid ContractGid, Guid ProcurementsGid, Guid PlanGid, int ContractDifferentiatedPositionId) GetInfoForContractDifferentiatedPosition(Guid dpGid);

        IList<(int contractId, string regNumber, int programmeId)> GetEnteredContractData();

        ContractIndicator GetContractIndicator(int contractId, int contractIndicatorId);

        IList<ContractUserVO> GetContractUsers(int contractId);

        bool IsUserAssociatedWithContract(int contractId, int userId);

        bool IsUserAssociatedWithAnyContract(int userId);

        ContractInfoVO GetContractInfo(int contractId);

        int GetContractProgrammePriority(int contractId);
    }
}
