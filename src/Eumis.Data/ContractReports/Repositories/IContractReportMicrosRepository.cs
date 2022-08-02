using Eumis.Data.ContractReports.PortalViewObjects;
using Eumis.Domain.Contracts.ContractReportMicros;
using Eumis.Domain.Contracts.ViewObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.ContractReports.Repositories
{
    public interface IContractReportMicrosRepository : IAggregateRepository<ContractReportMicro>
    {
        IList<ContractReportMicro> FindAll(int contractReportId);

        Task<IList<ContractReportMicro>> FindAllAsync(int contractReportId, CancellationToken ct);

        ContractReportMicro Find(Guid gid);

        ContractReportMicro FindForUpdate(Guid gid, byte[] version);

        ContractReportMicro GetActualContractReportMicro(int contractReportId, ContractReportMicroType type);

        ContractReportMicroType GetMicroType(Guid gid);

        ContractReportMicroType GetMicroType(int microId);

        IList<ContractReportMicroVO> GetContractReportMicros(int contractReportId);

        ContractReportMicroItemsPVO<ContractReportMicroType1ItemPVO> GetPortalType1Items(Guid gid, int offset = 0, int? limit = null);

        ContractReportMicroItemsPVO<ContractReportMicroType2ItemPVO> GetPortalType2Items(Guid gid, int offset = 0, int? limit = null);

        ContractReportMicroItemsPVO<ContractReportMicroType3ItemPVO> GetPortalType3Items(Guid gid, int offset = 0, int? limit = null);

        ContractReportMicroItemsPVO<ContractReportMicroType4ItemPVO> GetPortalType4Items(Guid gid, int offset = 0, int? limit = null);

        bool CheckMicroHasFile(Guid gid, Guid fileKey);

        int GetNextVersionNum(int contractId, ContractReportMicroType type);

        Task<int> GetNextVersionNumAsync(int contractId, ContractReportMicroType type, CancellationToken ct);

        int GetNextVersionSubNum(int contractReportId, ContractReportMicroType type);

        Task<int> GetNextVersionSubNumAsync(int contractReportId, ContractReportMicroType type, CancellationToken ct);

        int GetContractReportId(int microId);

        Task CopyMicrodataType1ItemsAsync(int oldMicrodataId, int newMicrodataId, CancellationToken ct);

        Task CopyMicrodataType2ItemsAsync(int oldMicrodataId, int newMicrodataId, CancellationToken ct);

        Task CopyMicrodataType3ItemsAsync(int oldMicrodataId, int newMicrodataId, CancellationToken ct);

        Task CopyMicrodataType4ItemsAsync(int oldMicrodataId, int newMicrodataId, CancellationToken ct);
    }
}
