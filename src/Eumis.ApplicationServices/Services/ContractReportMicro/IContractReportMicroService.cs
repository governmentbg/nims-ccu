using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Eumis.Domain.Contracts.ContractReportMicros;

namespace Eumis.ApplicationServices.Services.ContractReportMicro
{
    public interface IContractReportMicroService
    {
        // ContractReportMicro
        IList<string> CanCreateContractReportMicro(int contractReportId, ContractReportMicroType type);

        Task<IList<string>> CanCreateContractReportMicroAsync(int contractReportId, ContractReportMicroType type, CancellationToken ct);

        Domain.Contracts.ContractReportMicros.ContractReportMicro CreateContractReportMicro(Domain.Contracts.ContractReport contractReport, ContractReportMicroType type, Domain.Contracts.Source source);

        Task<Domain.Contracts.ContractReportMicros.ContractReportMicro> CreateContractReportMicroAsync(Domain.Contracts.ContractReport contractReport, ContractReportMicroType type, Domain.Contracts.Source source, CancellationToken ct);

        IList<string> UpdateContractReportMicro(Domain.Contracts.ContractReportMicros.ContractReportMicro micro, Guid excelBlobKey, string excelName, out IList<string> warnings);

        IList<string> UpdateContractReportMicroWithSimevCode(Domain.Contracts.ContractReportMicros.ContractReportMicro micro, string simevCode, out IList<string> warnings);

        void ChangeContractReportMicroStatusToReturned(Domain.Contracts.ContractReportMicros.ContractReportMicro oldMicro, string statusNote);

        IList<string> CanChangeContractReportMicroStatusToReturned(int contractReportId, ContractReportMicroType type);

        void ChangeContractReportMicroStatus(Domain.Contracts.ContractReportMicros.ContractReportMicro contractReportMicro, ContractReportMicroStatus status, int? contractRegistrationId = null);

        Task ChangeContractReportMicroStatusAsync(Domain.Contracts.ContractReportMicros.ContractReportMicro contractReportMicro, ContractReportMicroStatus status, int? contractRegistrationId, CancellationToken ct);

        void ChangeContractReportNewVersionMicroStatusToActual(Domain.Contracts.ContractReportMicros.ContractReportMicro draftMicro, Domain.Contracts.ContractReportMicros.ContractReportMicro actualMicro, string note);

        void DeleteContractReportMicro(Domain.Contracts.ContractReportMicros.ContractReportMicro contractReportMicro);

        IList<string> CanChangeContractReportMicroStatusToEntered(Domain.Contracts.ContractReportMicros.ContractReportMicro contractReportMicro);

        Task<IList<string>> CanChangeContractReportMicroStatusToEnteredAsync(Domain.Contracts.ContractReportMicros.ContractReportMicro contractReportMicro, CancellationToken ct);

        IList<string> CanCreateContractReportMicroNewVersion(Domain.Contracts.ContractReportMicros.ContractReportMicro micro);

        void CreateContractReportMicroNewVersion(Domain.Contracts.ContractReportMicros.ContractReportMicro oldMicro);

        IList<string> UpdateContractReportMicroNewVersion(Domain.Contracts.ContractReportMicros.ContractReportMicro micro, Guid excelBlobKey, string excelName, out IList<string> warnings);

        IList<string> CanDeleteContractReportMicroNewVersion(Domain.Contracts.ContractReportMicros.ContractReportMicro micro);

        void DeleteContractReportMicroNewVersion(Domain.Contracts.ContractReportMicros.ContractReportMicro micro);

        // ContractReportMicroCheck
        ContractReportMicroCheck CreateContractReportMicroCheck(int contractReportId, ContractReportMicroType type);

        IList<string> CanCreateContractReportMicroCheck(int contractReportId, ContractReportMicroType type);

        void UpdateContractReportMicroCheck(int contractReportMicroCheckId, byte[] version, ContractReportMicroCheckApproval? approval, DateTime? checkedDate, Guid? blobKey);

        void ActivateContractReportMicroCheck(int contractReportMicroCheckId, byte[] version);

        void DeleteContractReportMicroCheck(int contractReportId, int contractReportMicroCheckId, byte[] version);
    }
}
