using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;

namespace Eumis.ApplicationServices.Services.ContractReport
{
    public interface IContractReportService
    {
        // get
        string GetContractReportFinancialXmlForEdit(ContractReportFinancial finance);

        Task<string> GetContractReportFinancialXmlForEditAsync(ContractReportFinancial finance, CancellationToken ct);

        string GetContractReportTechnicalXmlForEdit(ContractReportTechnical tecnical);

        Task<string> GetContractReportTechnicalXmlForEditAsync(ContractReportTechnical tecnical, CancellationToken ct);

        // create
        IList<string> CanCreateContractReport(int contractId);

        IList<string> CanCreateContractReportFinancial(int contractReportId);

        Task<IList<string>> CanCreateContractReportFinancialAsync(int contractReportId, CancellationToken ct);

        IList<string> CanCreateContractReportPayment(int contractReportId, ContractReportPaymentType type);

        Task<IList<string>> CanCreateContractReportPaymentAsync(int contractReportId, ContractReportPaymentType type, CancellationToken ct);

        IList<string> CanCreateContractReportTechnical(int contractReportId);

        IList<string> CanCreateContractReportFinancialCheck(int contractReportId);

        IList<string> CanCreateContractReportPaymentCheck(int contractReportId);

        IList<string> CanCreateContractReportTechnicalCheck(int contractReportId);

        Task<IList<string>> CanCreateContractReportAsync(Guid contractGid, CancellationToken ct);

        Task<IList<string>> CanCreateContractReportFinancialAsync(Guid contractReportGid, CancellationToken ct);

        IList<string> CanCreateContractReportPayment(Guid contractReportGid, ContractReportPaymentType type);

        Task<IList<string>> CanCreateContractReportTechnicalAsync(Guid contractReportGid, CancellationToken ct);

        Task<IList<string>> CanCopyContractReportAsync(Guid contractReportGid, CancellationToken ct);

        Task<IList<string>> HasAdvanceVerificationPaymentAsync(Guid contractReportGid, CancellationToken ct);

        Task<IList<string>> CanEditSentContractReportAsync(Guid contractGid, CancellationToken ct);

        Eumis.Domain.Contracts.ContractReport CreateContractReport(
            int contractId,
            Domain.Contracts.ContractReportType reportType,
            string otherRegistration,
            string storagePlace,
            DateTime? submitDate,
            DateTime? submitDeadline);

        Eumis.Domain.Contracts.ContractReportFinancial CreateContractReportFinancial(int contractReportId);

        Task<ContractReportFinancial> CreateContractReportFinancialAsync(int contractReportId, CancellationToken ct);

        Eumis.Domain.Contracts.ContractReportPayment CreateContractReportPayment(int contractReportId, ContractReportPaymentType type);

        Eumis.Domain.Contracts.ContractReportTechnical CreateContractReportTechnical(int contractReportId);

        Task<ContractReportTechnical> CreateContractReportTechnicalAsync(int contractReportId, CancellationToken ct);

        Eumis.Domain.Contracts.ContractReportFinancialCheck CreateContractReportFinancialCheck(int contractReportId);

        Eumis.Domain.Contracts.ContractReportPaymentCheck CreateContractReportPaymentCheck(int contractReportId);

        Eumis.Domain.Contracts.ContractReportTechnicalCheck CreateContractReportTechnicalCheck(int contractReportId);

        Task<Eumis.Domain.Contracts.ContractReport> CreateContractReportAsync(
            Guid contractGid,
            Domain.Contracts.ContractReportType reportType,
            string otherRegistration,
            string storagePlace,
            DateTime? submitDate,
            DateTime? submitDeadline,
            CancellationToken ct);

        Task<Eumis.Domain.Contracts.ContractReport> CopyContractReportAsync(Guid contractReportGid, CancellationToken ct);

        Task<ContractReportFinancial> CreateContractReportFinancialAsync(Guid contractReportGid, CancellationToken ct);

        Eumis.Domain.Contracts.ContractReportPayment CreateContractReportPayment(Guid contractReportGid, ContractReportPaymentType type);

        Task<ContractReportTechnical> CreateContractReportTechnicalAsync(Guid contractReportGid, CancellationToken ct);

        // update
        Eumis.Domain.Contracts.ContractReport UpdateContractReport(
            int contractReportId,
            byte[] version,
            ContractReportType contractReportType,
            string otherRegistration,
            string storagePlace,
            DateTime? submitDate,
            DateTime? submitDeadline);

        Eumis.Domain.Contracts.ContractReport UpdateContractReportCheck(
            int contractReportId,
            byte[] version,
            DateTime? checkedDate);

        Eumis.Domain.Contracts.ContractReportFinancial UpdateContractReportFinancial(int contractReportId, int contractReportFinancialId, byte[] version, string xml);

        Eumis.Domain.Contracts.ContractReportPayment UpdateContractReportPayment(int contractReportId, int contractReportPaymentId, byte[] version, string xml);

        Eumis.Domain.Contracts.ContractReportTechnical UpdateContractReportTechnical(int contractReportId, int contractReportTechnicalId, byte[] version, string xml);

        Task<ContractReportTechnical> UpdateContractReportTechnicalAsync(int contractReportId, int contractReportTechnicalId, byte[] version, string xml, CancellationToken ct);

        Eumis.Domain.Contracts.ContractReportFinancialCheck UpdateContractReportFinancialCheck(
            int contractReportFinancialCheckId,
            byte[] version,
            ContractReportFinancialCheckApproval? approval,
            Guid? blobKey,
            DateTime? checkedDate);

        Eumis.Domain.Contracts.ContractReportTechnicalCheck UpdateContractReportTechnicalCheck(
            int contractReportTechnicalCheckId,
            byte[] version,
            ContractReportTechnicalCheckApproval? approval,
            Guid? blobKey,
            DateTime? checkedDate);

        Eumis.Domain.Contracts.ContractReportPaymentCheck UpdateContractReportPaymentCheck(
            int contractReportPaymentCheckId,
            byte[] version,
            ContractReportPaymentCheckApproval? approval,
            Guid? blobKey,
            DateTime? checkedDate,
            IList<ContractReportPaymentCheckAmountDO> amounts);

        Task<Eumis.Domain.Contracts.ContractReport> UpdateContractReportAsync(
            Guid contractReportGid,
            byte[] version,
            ContractReportType contractReportType,
            string otherRegistration,
            string storagePlace,
            DateTime? submitDate,
            DateTime? submitDeadline,
            CancellationToken ct);

        Task<ContractReportFinancial> UpdateContractReportFinancialAsync(Guid contractReportFinancialGid, byte[] version, string xml, CancellationToken ct);

        Eumis.Domain.Contracts.ContractReportPayment UpdateContractReportPayment(Guid contractReportPaymentGid, byte[] version, string xml);

        Task<ContractReportTechnical> UpdateContractReportTechnicalAsync(Guid contractReportTechnicalGid, byte[] version, string xml, CancellationToken ct);

        Task<IList<string>> CanChangeContractReportTypeAsync(Guid contractReportGid, ContractReportType newContractReportType, CancellationToken ct);

        IList<string> CanChangeContractReportType(int contractReporId, ContractReportType newContractReportType);

        // delete
        IList<string> CanDeleteContractReport(int contractReportId);

        Task<IList<string>> CanDeleteContractReportAsync(int contractReportId, CancellationToken ct);

        Task<IList<string>> CanDeleteContractReportAsync(Guid contractReportGid, CancellationToken ct);

        Eumis.Domain.Contracts.ContractReport DeleteContractReport(int contractReportId, byte[] version);

        Eumis.Domain.Contracts.ContractReportFinancial DeleteContractReportFinancial(int contractReportId, int contractReportFinancialId, byte[] version);

        Task<ContractReportFinancial> DeleteContractReportFinancialAsync(int contractReportId, int contractReportFinancialId, byte[] version, CancellationToken ct);

        Eumis.Domain.Contracts.ContractReportPayment DeleteContractReportPayment(int contractReportId, int contractReportPaymentId, byte[] version);

        Eumis.Domain.Contracts.ContractReportTechnical DeleteContractReportTechnical(int contractReportId, int contractReportTechnicalId, byte[] version);

        Task<ContractReportTechnical> DeleteContractReportTechnicalAsync(int contractReportId, int contractReportTechnicalId, byte[] version, CancellationToken ct);

        Eumis.Domain.Contracts.ContractReportFinancialCheck DeleteContractReportFinancialCheck(int contractReportId, int contractReportFinancialCheckId, byte[] version);

        Eumis.Domain.Contracts.ContractReportPaymentCheck DeleteContractReportPaymentCheck(int contractReportId, int contractReportPaymentCheckId, byte[] version);

        Eumis.Domain.Contracts.ContractReportTechnicalCheck DeleteContractReportTechnicalCheck(int contractReportId, int contractReportTechnicalCheckId, byte[] version);

        Task<Eumis.Domain.Contracts.ContractReport> DeleteContractReportAsync(Guid contractReportGid, byte[] version, CancellationToken ct);

        Task<ContractReportFinancial> DeleteContractReportFinancialAsync(Guid contractReportFinancialGid, byte[] version, CancellationToken ct);

        Eumis.Domain.Contracts.ContractReportPayment DeleteContractReportPayment(Guid contractReportPaymentGid, byte[] version);

        Task<ContractReportTechnical> DeleteContractReportTechnicalAsync(Guid contractReportTechnicalGid, byte[] version, CancellationToken ct);

        // statuses
        Task<IList<string>> CanDraftContractReportAsync(Guid contractReportGid, CancellationToken ct);

        IList<string> CanEnterContractReport(int contractReportId);

        Task<IList<string>> CanEnterContractReportAsync(int contractReportId, CancellationToken ct);

        Task<IList<string>> CanEnterContractReportAsync(Guid contractReportGid, CancellationToken ct);

        IList<string> CanAcceptContractReport(int contractReportId);

        IList<string> CanRefuseContractReport(int contractReportId);

        IList<string> CanChangeContractReportStatusToUnchecked(int contractReportId);

        IList<string> CanReturnContractReportStatusToUnchecked(int contractReportId);

        IList<string> CanChangeContractReportFinancialStatusToEntered(int contractReportId);

        Task<IList<string>> CanChangeContractReportFinancialStatusToEnteredAsync(int contractReportId, CancellationToken ct);

        IList<string> CanChangeContractReportPaymentStatusToEntered(int contractReportId);

        IList<string> CanChangeContractReportTechnicalStatusToEntered(int contractReportId);

        Task<IList<string>> CanChangeContractReportTechnicalStatusToEnteredAsync(int contractReportId, CancellationToken ct);

        IList<string> CanChangeContractReportFinancialStatusToReturned(int contractReportId, int contractReportFinancialId);

        IList<string> CanChangeContractReportPaymentStatusToReturned(int contractReportId);

        IList<string> CanChangeContractReportTechnicalStatusToReturned(int contractReportId);

        IList<string> CanChangeContractReportFinancialCheckStatusToActive(int contractReportFinancialCheckId);

        IList<string> CanChangeContractReportTechnicalCheckStatusToActive(int contractReportTechnicalCheckId);

        IList<string> CanChangeContractReportPaymentCheckStatusToActive(int contractReportPaymentCheckId);

        IList<string> CanChangeContractReportPaymentCheckStatusToArchived(int contractReportPaymentCheckId);

        Eumis.Domain.Contracts.ContractReport ChangeContractReportStatus(int contractReportId, byte[] version, ContractReportStatus status, string statusNote = null);

        Eumis.Domain.Contracts.ContractReport ReturnContractReportStatusToUnchecked(int contractReportId, byte[] version);

        Eumis.Domain.Contracts.ContractReportFinancial ChangeContractReportFinancialStatus(int contractReportId, int contractReportFinancialId, byte[] version, ContractReportFinancialStatus status, int? contractRegistrationId = null);

        Task<ContractReportFinancial> ChangeContractReportFinancialStatusAsync(
            int contractReportId,
            int contractReportFinancialId,
            byte[] version,
            ContractReportFinancialStatus status,
            int? contractRegistrationId,
            CancellationToken ct);

        Task<ContractReportFinancial> ChangeContractReportFinancialStatusAsync(
            Guid contractReportFinancialGid,
            byte[] version,
            ContractReportFinancialStatus status,
            int? contractRegistrationId,
            CancellationToken ct);

        Eumis.Domain.Contracts.ContractReportPayment ChangeContractReportPaymentStatus(int contractReportId, int contractReportPaymentId, byte[] version, ContractReportPaymentStatus status, int? contractRegistrationId = null);

        Eumis.Domain.Contracts.ContractReportPayment ChangeContractReportPaymentStatus(Guid contractReportPaymentGid, byte[] version, ContractReportPaymentStatus status, int? contractRegistrationId = null);

        Eumis.Domain.Contracts.ContractReportTechnical ChangeContractReportTechnicalStatus(int contractReportId, int contractReportTechnicalId, byte[] version, ContractReportTechnicalStatus status, int? contractRegistrationId = null);

        Task<ContractReportTechnical> ChangeContractReportTechnicalStatusAsync(
            int contractReportId,
            int contractReportTechnicalId,
            byte[] version,
            ContractReportTechnicalStatus status,
            int? contractRegistrationId,
            CancellationToken ct);

        Task<ContractReportTechnical> ChangeContractReportTechnicalStatusAsync(Guid contractReportTechnicalGid, byte[] version, ContractReportTechnicalStatus status, int? contractRegistrationId, CancellationToken ct);

        Eumis.Domain.Contracts.ContractReportTechnical ChangeContractReportTechnicalStatusToReturned(int contractReportId, int contractReportTechnicalId, byte[] version, string statusNote);

        Eumis.Domain.Contracts.ContractReportFinancial ChangeContractReportFinancialStatusToReturned(int contractReportId, int contractReportFinancialId, byte[] version, string statusNote);

        Eumis.Domain.Contracts.ContractReportPayment ChangeContractReportPaymentStatusToReturned(int contractReportId, int contractReportPaymentId, byte[] version, string statusNote);

        Eumis.Domain.Contracts.ContractReportFinancialCheck ChangeContractReportFinancialCheckStatus(int contractReportId, int contractReportFinancialCheckId, byte[] version, ContractReportFinancialCheckStatus status);

        Eumis.Domain.Contracts.ContractReportPaymentCheck ChangeContractReportPaymentCheckStatus(int contractReportId, int contractReportPaymentCheckId, byte[] version, ContractReportPaymentCheckStatus status);

        Eumis.Domain.Contracts.ContractReportTechnicalCheck ChangeContractReportTechnicalCheckStatus(int contractReportId, int contractReportTechnicalCheckId, byte[] version, ContractReportTechnicalCheckStatus status);

        Task<Eumis.Domain.Contracts.ContractReport> ChangeContractReportStatusAsync(Guid contractReportGid, byte[] version, ContractReportStatus status, int? contractRegistrationId, CancellationToken ct);

        // attached contract report financial corrections
        IList<string> CanAttachContractReportFinancialCorrection(int contractReportId);

        Eumis.Domain.Contracts.ContractReportAttachedFinancialCorrection AttachContractReportFinancialCorrection(int contractReportId, int contractReportFinancialCorrectionId, byte[] version);

        Eumis.Domain.Contracts.ContractReportAttachedFinancialCorrection DetachContractReportFinancialCorrection(int contractReportId, int contractReportFinancialCorrectionId, byte[] version);
    }
}
