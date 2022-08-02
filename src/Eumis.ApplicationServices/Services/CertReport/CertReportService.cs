using Eumis.Common.Db;
using Eumis.Data.CertReports.Repositories;
using Eumis.Data.ContractReportAdvancePaymentAmounts.Repositories;
using Eumis.Data.ContractReportCertCorrections.Repositories;
using Eumis.Data.ContractReportCorrections.Repositories;
using Eumis.Data.ContractReportFinancialCertCorrections.Repositories;
using Eumis.Data.ContractReportFinancialCorrections.Repositories;
using Eumis.Data.ContractReportFinancialCSDs.Repositories;
using Eumis.Data.ContractReportFinancialRevalidations.Repositories;
using Eumis.Data.ContractReportRevalidations.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Debts.Repositories;
using Eumis.Data.ReimbursedAmounts.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain;
using Eumis.Domain.CertReports;
using Eumis.Domain.CertReports.DataObjects;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Eumis.ApplicationServices.Services.CertReport
{
    public class CertReportService : ICertReportService
    {
        private IUnitOfWork unitOfWork;
        private IPermissionsRepository permissionsRepository;
        private ICertReportsRepository certReportsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportFinancialCorrectionsRepository contractReportFinancialCorrectionsRepository;
        private IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository;
        private IContractReportFinancialCorrectionCSDsRepository contractReportFinancialCorrectionCSDsRepository;
        private IContractReportPaymentChecksRepository contractReportPaymentChecksRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IContractReportAdvancePaymentAmountsRepository contractReportAdvancePaymentAmountsRepository;
        private IContractReportCorrectionsRepository contractReportCorrectionsRepository;
        private IContractReportFinancialRevalidationCSDsRepository contractReportFinancialRevalidationCSDsRepository;
        private IContractReportRevalidationsRepository contractReportRevalidationsRepository;
        private IContractReportFinancialCertCorrectionCSDsRepository contractReportFinancialCertCorrectionCSDsRepository;
        private IContractReportCertCorrectionsRepository contractReportCertCorrectionsRepository;
        private IContractDebtsRepository contractDebtsRepository;
        private IDebtReimbursedAmountsRepository debtReimbursedAmountsRepository;
        private ICertReportSnapshotsRepository certReportSnapshotsRepository;
        private IUsersRepository usersRepository;
        private IContractReportFinancialCSDsRepository contractReportFinancialCSDsRepository;
        private IContractReportFinancialsRepository contractReportFinancialsRepository;
        private IContractReportFinancialRevalidationsRepository contractReportFinancialRevalidationsRepository;
        private IContractReportFinancialCertCorrectionsRepository contractReportFinancialCertCorrectionsRepository;
        private IContractsRepository contractsRepository;
        private IContractDebtVersionsRepository contractDebtVersionsRepository;

        public CertReportService(
            IUnitOfWork unitOfWork,
            IPermissionsRepository permissionsRepository,
            ICertReportsRepository certReportsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportFinancialCorrectionsRepository contractReportFinancialCorrectionsRepository,
            IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository,
            IContractReportAdvancePaymentAmountsRepository contractReportAdvancePaymentAmountsRepository,
            IContractReportFinancialCorrectionCSDsRepository contractReportFinancialCorrectionCSDsRepository,
            IContractReportPaymentChecksRepository contractReportPaymentChecksRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IContractReportCorrectionsRepository contractReportCorrectionsRepository,
            IContractReportFinancialRevalidationCSDsRepository contractReportFinancialRevalidationCSDsRepository,
            IContractReportRevalidationsRepository contractReportRevalidationsRepository,
            IContractReportFinancialCertCorrectionCSDsRepository contractReportFinancialCertCorrectionCSDsRepository,
            IContractReportCertCorrectionsRepository contractReportCertCorrectionsRepository,
            IContractDebtsRepository contractDebtsRepository,
            IDebtReimbursedAmountsRepository debtReimbursedAmountsRepository,
            ICertReportSnapshotsRepository certReportSnapshotsRepository,
            IUsersRepository usersRepository,
            IContractReportFinancialCSDsRepository contractReportFinancialCSDsRepository,
            IContractReportFinancialsRepository contractReportFinancialsRepository,
            IContractReportFinancialRevalidationsRepository contractReportFinancialRevalidationsRepository,
            IContractReportFinancialCertCorrectionsRepository contractReportFinancialCertCorrectionsRepository,
            IContractsRepository contractsRepository,
            IContractDebtVersionsRepository contractDebtVersionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.permissionsRepository = permissionsRepository;
            this.certReportsRepository = certReportsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportFinancialCorrectionsRepository = contractReportFinancialCorrectionsRepository;
            this.contractReportFinancialCSDBudgetItemsRepository = contractReportFinancialCSDBudgetItemsRepository;
            this.contractReportAdvancePaymentAmountsRepository = contractReportAdvancePaymentAmountsRepository;
            this.contractReportFinancialCorrectionCSDsRepository = contractReportFinancialCorrectionCSDsRepository;
            this.contractReportPaymentChecksRepository = contractReportPaymentChecksRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.contractReportCorrectionsRepository = contractReportCorrectionsRepository;
            this.contractReportFinancialRevalidationCSDsRepository = contractReportFinancialRevalidationCSDsRepository;
            this.contractReportRevalidationsRepository = contractReportRevalidationsRepository;
            this.contractReportFinancialCertCorrectionCSDsRepository = contractReportFinancialCertCorrectionCSDsRepository;
            this.contractReportCertCorrectionsRepository = contractReportCertCorrectionsRepository;
            this.contractDebtsRepository = contractDebtsRepository;
            this.debtReimbursedAmountsRepository = debtReimbursedAmountsRepository;
            this.certReportSnapshotsRepository = certReportSnapshotsRepository;
            this.usersRepository = usersRepository;
            this.contractReportFinancialCSDsRepository = contractReportFinancialCSDsRepository;
            this.contractReportFinancialsRepository = contractReportFinancialsRepository;
            this.contractReportFinancialRevalidationsRepository = contractReportFinancialRevalidationsRepository;
            this.contractReportFinancialCertCorrectionsRepository = contractReportFinancialCertCorrectionsRepository;
            this.contractsRepository = contractsRepository;
            this.contractDebtVersionsRepository = contractDebtVersionsRepository;
        }

        #region CertReport

        public Domain.CertReports.CertReport CreateCertReport(int programmeId, DateTime regDate, DateTime dateFrom, DateTime dateTo, CertReportType type, string reportNumber)
        {
            var newCertReport = new Domain.CertReports.CertReport(
                programmeId,
                this.certReportsRepository.GetNextOrderNum(programmeId),
                regDate,
                dateFrom,
                dateTo,
                type,
                reportNumber);

            this.certReportsRepository.Add(newCertReport);

            this.unitOfWork.Save();

            return newCertReport;
        }

        public void UpdateCertReport(int certReportId, byte[] version, DateTime regDate, DateTime dateFrom, DateTime dateTo, string reportNumber)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            certReport.UpdateAttributes(regDate, dateFrom, dateTo, reportNumber);

            this.unitOfWork.Save();
        }

        public IList<string> CanDeleteCertReport(int certReportId)
        {
            var errors = new List<string>();

            var certReport = this.certReportsRepository.Find(certReportId);

            if (certReport.OrderVersionNum > 1)
            {
                errors.Add("Не можете да изтриете доклада, защото това не е първа версия");
            }

            if (certReport.CertReportDocuments.Any())
            {
                errors.Add("Не можете да изтриете доклада, защото към него има прикачени документи");
            }

            if (this.contractReportFinancialCSDBudgetItemsRepository.HasCertContractReportFinancialCSDBudgetItems(certReportId))
            {
                errors.Add("Не можете да изтриете доклада, защото към него има прикачени искания за плащания");
            }

            if (this.contractReportAdvancePaymentAmountsRepository.HasCertContractReportAdvancePaymentAmounts(certReportId))
            {
                errors.Add("Не можете да изтриете доклада, защото към него има прикачени искания за плащания по чл.131");
            }

            if (this.contractReportFinancialCorrectionCSDsRepository.HasCertContractReportFinancialCorrectionCSDs(certReportId))
            {
                errors.Add("Не можете да изтриете доклада, защото към него има прикачени корекции на верифициарни суми на ниво РОД");
            }

            if (this.contractReportCorrectionsRepository.HasCertContractReportCorrections(certReportId))
            {
                errors.Add("Не можете да изтриете доклада, защото към него има прикачени корекции на верифициарни суми на други нива");
            }

            if (this.contractReportFinancialRevalidationCSDsRepository.HasCertContractReportFinancialRevalidationCSDs(certReportId))
            {
                errors.Add("Не можете да изтриете доклада, защото към него има прикачени препотвърждавания на верифициарни суми на ниво РОД");
            }

            if (this.contractReportRevalidationsRepository.HasCertContractReportRevalidations(certReportId))
            {
                errors.Add("Не можете да изтриете доклада, защото към него има прикачени препотвърждавания на верифициарни суми на други нива");
            }

            if (this.contractReportFinancialCertCorrectionCSDsRepository.HasCertContractReportFinancialCertCorrectionCSDs(certReportId))
            {
                errors.Add("Не можете да изтриете доклада, защото към него има прикачени корекции на сертифицирани суми на ниво РОД");
            }

            if (this.contractReportCertCorrectionsRepository.HasCertContractReportCertCorrections(certReportId))
            {
                errors.Add("Не можете да изтриете доклада, защото към него има прикачени корекции на сертифицирани суми на други нива");
            }

            if (this.contractDebtsRepository.HasCertContractReportContractDebts(certReportId))
            {
                errors.Add("Не можете да изтриете доклада, защото към него има прикачени дългове");
            }

            return errors;
        }

        public void DeleteCertReport(int certReportId, byte[] version)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            this.certReportsRepository.Remove(certReport);

            this.unitOfWork.Save();
        }

        public IList<string> CanChangeCertReportStatusToFinalStatus(int certReportId)
        {
            var errors = new List<string>();

            var certReport = this.certReportsRepository.Find(certReportId);

            if (this.contractReportFinancialCSDBudgetItemsRepository.HasCertDraftContractReportFinancialCSDBudgetItems(certReportId))
            {
                errors.Add("Не можете да промените статуса на доклада на 'Одобрен', 'Частично одобрен' или 'Неодобрен', защото има " +
                    "неприключени разходооправдателни документи в секция 'ИП'");
            }

            if (this.contractReportAdvancePaymentAmountsRepository.HasCertDraftContractReportAdvancePaymentAmounts(certReportId))
            {
                errors.Add("Не можете да промените статуса на доклада на 'Одобрен', 'Частично одобрен'или 'Неодобрен', защото има " +
                    "неприключени авансови плащания в секция 'ИП по чл. 131'");
            }

            if (this.contractReportFinancialCorrectionCSDsRepository.HasCertDraftContractReportFinancialCorrectionCSDs(certReportId))
            {
                errors.Add("Не можете да промените статуса на доклада на 'Одобрен', 'Частично одобрен'или 'Неодобрен', защото има " +
                    "неприключени разходооправдателни документи в секция 'Корекции(ВС) по РОД'");
            }

            if (this.contractReportCorrectionsRepository.HasCertDraftContractReportCorrections(certReportId))
            {
                errors.Add("Не можете да промените статуса на доклада на 'Одобрен', 'Частично одобрен'или 'Неодобрен', защото има " +
                    "неприключени разходооправдателни документи в секция 'Корекции(ВС)'");
            }

            if (this.contractReportFinancialRevalidationCSDsRepository.HasCertDraftContractReportFinancialRevalidationCSDs(certReportId))
            {
                errors.Add("Не можете да промените статуса на доклада на 'Одобрен', 'Частично одобрен'или 'Неодобрен', защото има " +
                    "неприключени разходооправдателни документи в секция 'Препотвърждаване по РОД'");
            }

            if (this.contractReportRevalidationsRepository.HasCertDraftContractReportRevalidations(certReportId))
            {
                errors.Add("Не можете да промените статуса на доклада на 'Одобрен', 'Частично одобрен'или 'Неодобрен', защото има " +
                    "неприключени разходооправдателни документи в секция 'Препотвърждаване'");
            }

            return errors;
        }

        public int? ChangeCertReportStatus(int certReportId, byte[] version, CertReportStatus status, string statusNote = null)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);
            var resetChildItemsStatus = false;

            Action<List<CertReportStatus>> validateStatuses = (s) =>
            {
                if (!s.Where(x => x == certReport.Status).Any())
                {
                    throw new DomainException("CertReport status transition not allowed");
                }
            };

            Action<CertReportStatus> validateStatus = (s) =>
            {
                validateStatuses(new List<CertReportStatus> { s });
            };

            switch (status)
            {
                case CertReportStatus.Draft:
                    validateStatus(CertReportStatus.Ended);
                    break;
                case CertReportStatus.Ended:
                    validateStatus(CertReportStatus.Draft);
                    break;
                case CertReportStatus.Unchecked:
                    validateStatuses(new List<CertReportStatus> { CertReportStatus.Ended, CertReportStatus.Approved, CertReportStatus.PartialyApproved, CertReportStatus.Unapproved });
                    resetChildItemsStatus = certReport.Status == CertReportStatus.Ended;
                    break;
                case CertReportStatus.Approved:
                    validateStatus(CertReportStatus.Unchecked);
                    break;
                case CertReportStatus.PartialyApproved:
                    validateStatus(CertReportStatus.Unchecked);
                    break;
                case CertReportStatus.Unapproved:
                    validateStatus(CertReportStatus.Unchecked);
                    break;
                case CertReportStatus.Returned:
                    validateStatus(CertReportStatus.Unchecked);
                    break;
            }

            if (resetChildItemsStatus)
            {
                var currentDate = DateTime.Now;
                var budgetItems = this.contractReportFinancialCSDBudgetItemsRepository.FindAllByCertReport(certReportId);
                var advancePaymentAmounts = this.contractReportAdvancePaymentAmountsRepository.FindAllByCertReport(certReportId);
                var correctionCSDs = this.contractReportFinancialCorrectionCSDsRepository.FindAllByCertReport(certReportId);
                var corrections = this.contractReportCorrectionsRepository.FindAllByCertReport(certReportId);
                var revalidationCSDs = this.contractReportFinancialRevalidationCSDsRepository.FindAllByCertReport(certReportId);
                var revalidations = this.contractReportRevalidationsRepository.FindAllByCertReport(certReportId);

                foreach (var budgetItem in budgetItems)
                {
                    budgetItem.CertStatus = ContractReportFinancialCSDBudgetItemCertStatus.Draft;
                    budgetItem.ModifyDate = currentDate;
                }

                foreach (var advancePaymentAmount in advancePaymentAmounts)
                {
                    advancePaymentAmount.CertStatus = ContractReportAdvancePaymentAmountCertStatus.Draft;
                    advancePaymentAmount.ModifyDate = currentDate;
                }

                foreach (var correctionCSD in correctionCSDs)
                {
                    correctionCSD.CertStatus = ContractReportFinancialCorrectionCSDCertStatus.Draft;
                    correctionCSD.ModifyDate = currentDate;
                }

                foreach (var correction in corrections)
                {
                    correction.CertStatus = ContractReportCorrectionCertStatus.Draft;
                    correction.ModifyDate = currentDate;
                }

                foreach (var revalidationCSD in revalidationCSDs)
                {
                    revalidationCSD.CertStatus = ContractReportFinancialRevalidationCSDCertStatus.Draft;
                    revalidationCSD.ModifyDate = currentDate;
                }

                foreach (var revalidation in revalidations)
                {
                    revalidation.CertStatus = ContractReportRevalidationCertStatus.Draft;
                    revalidation.ModifyDate = currentDate;
                }

                this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCSDBudgetItem>(budgetItems, t => t.CertStatus, t => t.ModifyDate);
                this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportAdvancePaymentAmount>(advancePaymentAmounts, t => t.CertStatus, t => t.ModifyDate);
                this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCorrectionCSD>(correctionCSDs, t => t.CertStatus, t => t.ModifyDate);
                this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportCorrection>(corrections, t => t.CertStatus, t => t.ModifyDate);
                this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialRevalidationCSD>(revalidationCSDs, t => t.CertStatus, t => t.ModifyDate);
                this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportRevalidation>(revalidations, t => t.CertStatus, t => t.ModifyDate);
            }
            else if (status == CertReportStatus.Approved || status == CertReportStatus.PartialyApproved || status == CertReportStatus.Unapproved)
            {
                if (this.CanChangeCertReportStatusToFinalStatus(certReportId).Any())
                {
                    throw new DomainException("Cannot change CertReport status to 'Approved' or 'PartialyApproved' or 'Unapproved'");
                }

                if (status == CertReportStatus.Approved || status == CertReportStatus.PartialyApproved)
                {
                    certReport.ApprovalDate = DateTime.Now;
                }
            }

            certReport.ChangeStatus(status);

            // make the current CertReport draft and create a new one with a snapshot
            Eumis.Domain.CertReports.CertReport newCertReport = null;
            if (status == CertReportStatus.Returned)
            {
                var currentDate = DateTime.Now;
                newCertReport = new Eumis.Domain.CertReports.CertReport()
                {
                    ProgrammeId = certReport.ProgrammeId,
                    OrderNum = certReport.OrderNum,
                    OrderVersionNum = certReport.OrderVersionNum,
                    RegDate = certReport.RegDate,
                    DateFrom = certReport.DateFrom,
                    DateTo = certReport.DateTo,
                    Status = CertReportStatus.Returned,
                    StatusNote = statusNote,
                    Type = certReport.Type,
                    ApprovalDate = certReport.ApprovalDate,
                    CertReportOriginId = certReport.CertReportId,
                    CreateDate = currentDate,
                    ModifyDate = currentDate,
                    CertReportNumber = certReport.CertReportNumber,
                };

                certReport.Status = CertReportStatus.Draft;
                certReport.OrderVersionNum = certReport.OrderVersionNum + 1;

                this.certReportsRepository.Add(newCertReport);

                this.unitOfWork.Save();

                var newCertReportSnapshotResults = this.CreateCertReportSnapshot(certReport.CertReportId);

                var groupedAmounts = this.certReportsRepository.GetCertReportAmountsGroupedByCertReport().Where(t => t.CertReportId == certReport.CertReportId).SingleOrDefault();

                var newCertReportSnapshot = new CertReportSnapshot(
                    newCertReport.CertReportId,
                    newCertReportSnapshotResults.Item1,
                    groupedAmounts != null ? groupedAmounts.ApprovedEuAmount : null,
                    groupedAmounts != null ? groupedAmounts.ApprovedBgAmount : null,
                    groupedAmounts != null ? groupedAmounts.ApprovedBfpTotalAmount : null,
                    groupedAmounts != null ? groupedAmounts.ApprovedSelfAmount : null,
                    groupedAmounts != null ? groupedAmounts.ApprovedTotalAmount : null,
                    groupedAmounts != null ? groupedAmounts.CertifiedEuAmount : null,
                    groupedAmounts != null ? groupedAmounts.CertifiedBgAmount : null,
                    groupedAmounts != null ? groupedAmounts.CertifiedBfpTotalAmount : null,
                    groupedAmounts != null ? groupedAmounts.CertifiedSelfAmount : null,
                    groupedAmounts != null ? groupedAmounts.CertifiedTotalAmount : null);

                newCertReportSnapshot.CertReportSnapshotFiles = newCertReportSnapshotResults.Item2.Select(t => new CertReportSnapshotFile()
                {
                    BlobKey = t,
                }).ToList();

                this.certReportSnapshotsRepository.Add(newCertReportSnapshot);
            }

            this.unitOfWork.Save();

            if (newCertReport != null)
            {
                return newCertReport.CertReportId;
            }
            else
            {
                return null;
            }
        }

        // TODO: consider refactoring this method and removing the regions
        [SuppressMessage("", "SA1123:DoNotPlaceRegionsWithinElements", Justification = "This method is too big and requires refactoring.")]
        private Tuple<CertReportSnapshotJson, IList<Guid>> CreateCertReportSnapshot(int certReportId)
        {
            var blobKeys = new List<Guid>();
            var certReport = this.certReportsRepository.Find(certReportId);

            #region certReportPayments
            var certReportPayments = this.certReportsRepository.GetCertReportPayments(certReportId);

            foreach (var certReportPayment in certReportPayments)
            {
                var paymentCheck = this.contractReportPaymentChecksRepository.GetActualContractReportPaymentCheck(certReportPayment.ContractReportId);

                var payment = this.contractReportPaymentsRepository.Find(paymentCheck.ContractReportPaymentId);

                string paymentCheckCheckedByUser = string.Empty;

                if (paymentCheck.CheckedByUserId.HasValue)
                {
                    var user = this.usersRepository.Find(paymentCheck.CheckedByUserId.Value);
                    paymentCheckCheckedByUser = $"{user.Fullname} ({user.Username})";
                }

                certReportPayment.ContractReportPaymentCheck = new ContractReportPaymentCheckDO(paymentCheck, payment, paymentCheckCheckedByUser);
                blobKeys.Add(paymentCheck.File.Key);

                var budgetItems = this.contractReportFinancialCSDBudgetItemsRepository.GetContractReportFinancialCSDBudgetItems(certReportPayment.ContractReportId, isAttachedToCertReport: true, certReportId: certReportId);

                foreach (var budgetItem in budgetItems)
                {
                    var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(budgetItem.ContractReportFinancialCSDBudgetItemId);

                    var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

                    string checkedByUser = string.Empty;
                    string techCheckedByUser = string.Empty;
                    string certCheckedByUser = string.Empty;

                    if (financialCSDBudgetItem.CheckedByUserId.HasValue)
                    {
                        var user = this.usersRepository.Find(financialCSDBudgetItem.CheckedByUserId.Value);
                        checkedByUser = $"{user.Fullname} ({user.Username})";
                    }

                    if (financialCSDBudgetItem.TechCheckedByUserId.HasValue)
                    {
                        var user = this.usersRepository.Find(financialCSDBudgetItem.TechCheckedByUserId.Value);
                        techCheckedByUser = $"{user.Fullname} ({user.Username})";
                    }

                    if (financialCSDBudgetItem.CertCheckedByUserId.HasValue)
                    {
                        var user = this.usersRepository.Find(financialCSDBudgetItem.CertCheckedByUserId.Value);
                        certCheckedByUser = $"{user.Fullname} ({user.Username})";
                    }

                    budgetItem.ContractReportFinancialCSDBudgetItem = new ContractReportFinancialCSDBudgetItemDO(financialCSDBudgetItem, financialCSD, checkedByUser, techCheckedByUser, certCheckedByUser);
                    blobKeys.AddRange(financialCSD.Files.Select(t => t.BlobKey));
                }

                certReportPayment.ContractReportFinancialCSDBudgetItems = budgetItems;
            }
            #endregion //certReportPayments

            #region certReportAdvancePayments
            var certReportAdvancePayments = this.certReportsRepository.GetCertReportAdvancePayments(certReportId);
            foreach (var certReportAdvancePayment in certReportAdvancePayments)
            {
                var paymentCheck = this.contractReportPaymentChecksRepository.GetActualContractReportPaymentCheck(certReportAdvancePayment.ContractReportId);

                var payment = this.contractReportPaymentsRepository.Find(paymentCheck.ContractReportPaymentId);

                string checkedByUser = string.Empty;

                if (paymentCheck.CheckedByUserId.HasValue)
                {
                    var user = this.usersRepository.Find(paymentCheck.CheckedByUserId.Value);
                    checkedByUser = $"{user.Fullname} ({user.Username})";
                }

                certReportAdvancePayment.ContractReportPaymentCheck = new ContractReportPaymentCheckDO(paymentCheck, payment, checkedByUser);
                blobKeys.Add(paymentCheck.File.Key);

                var advancePaymentAmounts = this.contractReportAdvancePaymentAmountsRepository.GetContractReportAdvancePaymentAmounts(certReportAdvancePayment.ContractReportId, isAttachedToCertReport: true, certReportId: certReportId);

                foreach (var advancePaymentAmount in advancePaymentAmounts)
                {
                    var contractReportAdvancePaymentAmount = this.contractReportAdvancePaymentAmountsRepository.Find(advancePaymentAmount.ContractReportAdvancePaymentAmountId);

                    var payment1 = this.contractReportPaymentsRepository.Find(contractReportAdvancePaymentAmount.ContractReportPaymentId);

                    string checkedByUser1 = string.Empty;
                    string certCheckedByUser = string.Empty;

                    if (contractReportAdvancePaymentAmount.CheckedByUserId.HasValue)
                    {
                        var user = this.usersRepository.Find(contractReportAdvancePaymentAmount.CheckedByUserId.Value);
                        checkedByUser1 = $"{user.Fullname} ({user.Username})";
                    }

                    if (contractReportAdvancePaymentAmount.CertCheckedByUserId.HasValue)
                    {
                        var user = this.usersRepository.Find(contractReportAdvancePaymentAmount.CertCheckedByUserId.Value);
                        certCheckedByUser = $"{user.Fullname} ({user.Username})";
                    }

                    advancePaymentAmount.ContractReportAdvancePaymentAmount = new ContractReportAdvancePaymentAmountDO(contractReportAdvancePaymentAmount, payment1, checkedByUser1, certCheckedByUser);
                }

                certReportAdvancePayment.ContractReportAdvancePaymentAmounts = advancePaymentAmounts;
            }
            #endregion //certReportAdvancePayments

            #region certReportFinancialCorrections
            var certReportFinancialCorrections = this.certReportsRepository.GetCertReportFinancialCorrections(certReportId);

            foreach (var certReportFinancialCorrection in certReportFinancialCorrections)
            {
                var financialCorrection = this.contractReportFinancialCorrectionsRepository.Find(certReportFinancialCorrection.ContractReportFinancialCorrectionId);

                var financial = this.contractReportFinancialsRepository.Find(financialCorrection.ContractReportFinancialId);

                var payment = this.contractReportPaymentsRepository.GetActualContractReportPayment(financialCorrection.ContractReportId);

                string username = string.Empty;

                if (financialCorrection.CheckedByUserId.HasValue)
                {
                    var user = this.usersRepository.Find(financialCorrection.CheckedByUserId.Value);
                    username = $"{user.Fullname} ({user.Username})";
                }

                certReportFinancialCorrection.ContractReportFinancialCorrection = new ContractReportFinancialCorrectionDO(financialCorrection, financial, payment, username);
                blobKeys.Add(financialCorrection.File.Key);

                var correctionCSDs = this.contractReportFinancialCorrectionCSDsRepository.GetContractReportFinancialCorrectionCSDs(certReportFinancialCorrection.ContractReportFinancialCorrectionId, isAttachedToCertReport: true, certReportId: certReportId);

                foreach (var correctionCSD in correctionCSDs)
                {
                    var financialCorrectionCSD = this.contractReportFinancialCorrectionCSDsRepository.Find(correctionCSD.ContractReportFinancialCorrectionCSDId);

                    var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(financialCorrectionCSD.ContractReportFinancialCSDBudgetItemId);

                    var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

                    string checkedByUser = string.Empty;
                    string budgetItemCheckedByUser = string.Empty;
                    string budgetItemTechCheckedByUser = string.Empty;
                    string certCheckedByUser = string.Empty;

                    if (financialCorrectionCSD.CheckedByUserId.HasValue)
                    {
                        var user = this.usersRepository.Find(financialCorrectionCSD.CheckedByUserId.Value);
                        checkedByUser = $"{user.Fullname} ({user.Username})";
                    }

                    if (financialCSDBudgetItem.CheckedByUserId.HasValue)
                    {
                        var user = this.usersRepository.Find(financialCSDBudgetItem.CheckedByUserId.Value);
                        budgetItemCheckedByUser = $"{user.Fullname} ({user.Username})";
                    }

                    if (financialCSDBudgetItem.TechCheckedByUserId.HasValue)
                    {
                        var user = this.usersRepository.Find(financialCSDBudgetItem.TechCheckedByUserId.Value);
                        budgetItemTechCheckedByUser = $"{user.Fullname} ({user.Username})";
                    }

                    if (financialCorrectionCSD.CertCheckedByUserId.HasValue)
                    {
                        var user = this.usersRepository.Find(financialCorrectionCSD.CertCheckedByUserId.Value);
                        certCheckedByUser = $"{user.Fullname} ({user.Username})";
                    }

                    correctionCSD.ContractReportFinancialCorrectionCSD = new ContractReportFinancialCorrectionCSDDO(
                        financialCorrectionCSD,
                        checkedByUser,
                        financialCSDBudgetItem,
                        financialCSD,
                        budgetItemCheckedByUser,
                        budgetItemTechCheckedByUser,
                        certCheckedByUser);

                    blobKeys.AddRange(financialCSD.Files.Select(t => t.BlobKey));
                }

                certReportFinancialCorrection.ContractReportFinancialCorrectionCSDs = correctionCSDs;
            }
            #endregion //certReportFinancialCorrections

            #region certReportCorrections
            var certReportCorrections = this.certReportsRepository.GetCertReportCorrections(certReportId);

            foreach (var certReportCorrection in certReportCorrections)
            {
                var contractReportCorrection = this.contractReportCorrectionsRepository.Find(certReportCorrection.ContractReportCorrectionId);

                string certCheckedByUser = string.Empty;

                if (contractReportCorrection.CertCheckedByUserId.HasValue)
                {
                    var user = this.usersRepository.Find(contractReportCorrection.CertCheckedByUserId.Value);
                    certCheckedByUser = $"{user.Fullname} ({user.Username})";
                }

                if (contractReportCorrection.Type == ContractReportCorrectionType.PaymentVerified || contractReportCorrection.Type == ContractReportCorrectionType.AdvanceCovered)
                {
                    var payment = this.contractReportPaymentsRepository.Find(contractReportCorrection.ContractReportPaymentId.Value);

                    var paymentCheck = this.contractReportPaymentChecksRepository.GetActualContractReportPaymentCheck(payment.ContractReportId);

                    string paymentCheckedByUser = string.Empty;

                    if (paymentCheck.CheckedByUserId.HasValue)
                    {
                        var user = this.usersRepository.Find(paymentCheck.CheckedByUserId.Value);
                        paymentCheckedByUser = $"{user.Fullname} ({user.Username})";
                    }

                    certReportCorrection.ContractReportCorrection = new ContractReportCorrectionDO(contractReportCorrection, certCheckedByUser, payment, paymentCheck, paymentCheckedByUser);
                    blobKeys.Add(paymentCheck.File.Key);
                }
                else
                {
                    certReportCorrection.ContractReportCorrection = new ContractReportCorrectionDO(contractReportCorrection, certCheckedByUser);
                }
            }
            #endregion //certReportCorrections

            #region certReportFinancialRevalidations
            var certReportFinancialRevalidations = this.certReportsRepository.GetCertReportFinancialRevalidations(certReportId);

            foreach (var certReportFinancialRevalidation in certReportFinancialRevalidations)
            {
                var financialRevalidation = this.contractReportFinancialRevalidationsRepository.Find(certReportFinancialRevalidation.ContractReportFinancialRevalidationId);

                var financial = this.contractReportFinancialsRepository.Find(financialRevalidation.ContractReportFinancialId);

                var payment = this.contractReportPaymentsRepository.GetActualContractReportPayment(financialRevalidation.ContractReportId);

                string username = string.Empty;

                if (financialRevalidation.CheckedByUserId.HasValue)
                {
                    var user = this.usersRepository.Find(financialRevalidation.CheckedByUserId.Value);
                    username = $"{user.Fullname} ({user.Username})";
                }

                certReportFinancialRevalidation.ContractReportFinancialRevalidation = new ContractReportFinancialRevalidationDO(financialRevalidation, financial, payment, username);
                blobKeys.Add(financialRevalidation.File.Key);

                var revalidationCSDs = this.contractReportFinancialRevalidationCSDsRepository.GetContractReportFinancialRevalidationCSDs(certReportFinancialRevalidation.ContractReportFinancialRevalidationId, isAttachedToCertReport: true, certReportId: certReportId);

                foreach (var revalidationCSD in revalidationCSDs)
                {
                    var financialRevalidationCSD = this.contractReportFinancialRevalidationCSDsRepository.Find(revalidationCSD.ContractReportFinancialRevalidationCSDId);

                    var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(financialRevalidationCSD.ContractReportFinancialCSDBudgetItemId);

                    var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

                    string checkedByUser = string.Empty;
                    string budgetItemCheckedByUser = string.Empty;
                    string budgetItemTechCheckedByUser = string.Empty;
                    string certCheckedByUser = string.Empty;

                    if (financialRevalidationCSD.CheckedByUserId.HasValue)
                    {
                        var user = this.usersRepository.Find(financialRevalidationCSD.CheckedByUserId.Value);
                        checkedByUser = $"{user.Fullname} ({user.Username})";
                    }

                    if (financialCSDBudgetItem.CheckedByUserId.HasValue)
                    {
                        var user = this.usersRepository.Find(financialCSDBudgetItem.CheckedByUserId.Value);
                        budgetItemCheckedByUser = $"{user.Fullname} ({user.Username})";
                    }

                    if (financialCSDBudgetItem.TechCheckedByUserId.HasValue)
                    {
                        var user = this.usersRepository.Find(financialCSDBudgetItem.TechCheckedByUserId.Value);
                        budgetItemTechCheckedByUser = $"{user.Fullname} ({user.Username})";
                    }

                    if (financialRevalidationCSD.CertCheckedByUserId.HasValue)
                    {
                        var user = this.usersRepository.Find(financialRevalidationCSD.CertCheckedByUserId.Value);
                        certCheckedByUser = $"{user.Fullname} ({user.Username})";
                    }

                    revalidationCSD.ContractReportFinancialRevalidationCSD = new ContractReportFinancialRevalidationCSDDO(
                        financialRevalidationCSD,
                        checkedByUser,
                        financialCSDBudgetItem,
                        financialCSD,
                        budgetItemCheckedByUser,
                        budgetItemTechCheckedByUser,
                        certCheckedByUser);

                    blobKeys.AddRange(financialCSD.Files.Select(t => t.BlobKey));
                }

                certReportFinancialRevalidation.ContractReportFinancialRevalidationCSDs = revalidationCSDs;
            }

            #endregion certReportFinancialRevalidations

            #region certReportRevalidations
            var certReportRevalidations = this.certReportsRepository.GetCertReportRevalidations(certReportId);

            foreach (var certReportRevalidation in certReportRevalidations)
            {
                var contractReportRevalidation = this.contractReportRevalidationsRepository.Find(certReportRevalidation.ContractReportRevalidationId);

                string certCheckedByUser = string.Empty;

                if (contractReportRevalidation.CertCheckedByUserId.HasValue)
                {
                    var user = this.usersRepository.Find(contractReportRevalidation.CertCheckedByUserId.Value);
                    certCheckedByUser = $"{user.Fullname} ({user.Username})";
                }

                if (contractReportRevalidation.Type == ContractReportRevalidationType.PaymentRevalidated)
                {
                    var payment = this.contractReportPaymentsRepository.Find(contractReportRevalidation.ContractReportPaymentId.Value);

                    var paymentCheck = this.contractReportPaymentChecksRepository.GetActualContractReportPaymentCheck(payment.ContractReportId);

                    string paymentCheckedByUser = string.Empty;

                    if (paymentCheck.CheckedByUserId.HasValue)
                    {
                        var user = this.usersRepository.Find(paymentCheck.CheckedByUserId.Value);
                        paymentCheckedByUser = $"{user.Fullname} ({user.Username})";
                    }

                    certReportRevalidation.ContractReportRevalidation = new ContractReportRevalidationDO(contractReportRevalidation, certCheckedByUser, payment, paymentCheck, paymentCheckedByUser);
                }
                else
                {
                    certReportRevalidation.ContractReportRevalidation = new ContractReportRevalidationDO(contractReportRevalidation, certCheckedByUser);
                }
            }
            #endregion certReportRevalidations

            #region certReportFinancialCertCorrections
            var certReportFinancialCertCorrections = this.certReportsRepository.GetCertReportFinancialCertCorrections(certReportId);

            foreach (var certReportFinancialCertCorrection in certReportFinancialCertCorrections)
            {
                var financialCertCorrection = this.contractReportFinancialCertCorrectionsRepository.Find(certReportFinancialCertCorrection.ContractReportFinancialCertCorrectionId);

                var financial = this.contractReportFinancialsRepository.Find(financialCertCorrection.ContractReportFinancialId);

                var payment = this.contractReportPaymentsRepository.GetActualContractReportPayment(financialCertCorrection.ContractReportId);

                string username = string.Empty;

                if (financialCertCorrection.CheckedByUserId.HasValue)
                {
                    var user = this.usersRepository.Find(financialCertCorrection.CheckedByUserId.Value);
                    username = $"{user.Fullname} ({user.Username})";
                }

                certReportFinancialCertCorrection.ContractReportFinancialCertCorrection = new ContractReportFinancialCertCorrectionDO(financialCertCorrection, financial, payment, username);
                blobKeys.Add(financialCertCorrection.File.Key);

                var certCorrectionCSDs = this.contractReportFinancialCertCorrectionCSDsRepository.GetContractReportFinancialCertCorrectionCSDs(certReportFinancialCertCorrection.ContractReportFinancialCertCorrectionId, isAttachedToCertReport: true, certReportId: certReportId);

                foreach (var certCorrectionCSD in certCorrectionCSDs)
                {
                    var financialCertCorrectionCSD = this.contractReportFinancialCertCorrectionCSDsRepository.Find(certCorrectionCSD.ContractReportFinancialCertCorrectionCSDId);

                    var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(financialCertCorrectionCSD.ContractReportFinancialCSDBudgetItemId);

                    var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

                    string checkedByUser = string.Empty;
                    string budgetItemCheckedByUser = string.Empty;
                    string budgetItemTechCheckedByUser = string.Empty;
                    string certCheckedByUser = string.Empty;

                    if (financialCertCorrectionCSD.CheckedByUserId.HasValue)
                    {
                        var user = this.usersRepository.Find(financialCertCorrectionCSD.CheckedByUserId.Value);
                        checkedByUser = $"{user.Fullname} ({user.Username})";
                    }

                    if (financialCSDBudgetItem.CheckedByUserId.HasValue)
                    {
                        var user = this.usersRepository.Find(financialCSDBudgetItem.CheckedByUserId.Value);
                        budgetItemCheckedByUser = $"{user.Fullname} ({user.Username})";
                    }

                    if (financialCSDBudgetItem.TechCheckedByUserId.HasValue)
                    {
                        var user = this.usersRepository.Find(financialCSDBudgetItem.TechCheckedByUserId.Value);
                        budgetItemTechCheckedByUser = $"{user.Fullname} ({user.Username})";
                    }

                    certCorrectionCSD.ContractReportFinancialCertCorrectionCSD = new ContractReportFinancialCertCorrectionCSDDO(
                        financialCertCorrectionCSD,
                        checkedByUser,
                        financialCSDBudgetItem,
                        financialCSD,
                        budgetItemCheckedByUser,
                        budgetItemTechCheckedByUser);

                    blobKeys.AddRange(financialCSD.Files.Select(t => t.BlobKey));
                }

                certReportFinancialCertCorrection.ContractReportFinancialCertCorrectionCSDs = certCorrectionCSDs;
            }

            #endregion //certReportFinancialCertCorrections

            #region certReportCertCorrections
            var certReportCertCorrections = this.certReportsRepository.GetCertReportCertCorrections(certReportId);

            foreach (var certReportCertCorrection in certReportCertCorrections)
            {
                var contractReportCertCorrection = this.contractReportCertCorrectionsRepository.Find(certReportCertCorrection.ContractReportCertCorrectionId);

                if (contractReportCertCorrection.Type == ContractReportCertCorrectionType.PaymentCertified)
                {
                    var payment = this.contractReportPaymentsRepository.Find(contractReportCertCorrection.ContractReportPaymentId.Value);

                    var paymentCheck = this.contractReportPaymentChecksRepository.GetActualContractReportPaymentCheck(payment.ContractReportId);

                    string checkedByUser = string.Empty;

                    if (paymentCheck.CheckedByUserId.HasValue)
                    {
                        var user = this.usersRepository.Find(paymentCheck.CheckedByUserId.Value);
                        checkedByUser = $"{user.Fullname} ({user.Username})";
                    }

                    certReportCertCorrection.ContractReportCertCorrection = new ContractReportCertCorrectionDO(contractReportCertCorrection, payment, paymentCheck, checkedByUser);
                }
                else
                {
                    certReportCertCorrection.ContractReportCertCorrection = new ContractReportCertCorrectionDO(contractReportCertCorrection);
                }
            }

            #endregion //certReportCertCorrections

            #region certReportDocuments

            var certReportDocuments = this.certReportsRepository.GetCertReportDocuments(certReportId);

            foreach (var certReportDocument in certReportDocuments)
            {
                var document = certReport.FindCertReportDocument(certReportDocument.CertReportDocumentId);

                certReportDocument.CertReportDocument = new CertReportDocumentDO(document, certReport.Version);

                blobKeys.Add(document.BlobKey.Value);
            }

            #endregion //certReportDocuments

            #region certReportCertificationDocuments

            var certReportCertificationDocuments = this.certReportsRepository.GetCertReportCertificationDocuments(certReportId);

            foreach (var certReportCertificationDocument in certReportCertificationDocuments)
            {
                var document = certReport.FindCertReportCertificationDocument(certReportCertificationDocument.CertReportCertificationDocumentId);

                certReportCertificationDocument.CertReportCertificationDocument = new CertReportCertificationDocumentDO(document, certReport.Version);

                blobKeys.Add(document.BlobKey.Value);
            }

            #endregion //certReportCertificationDocuments

            #region attachedCertReports
            var attachedCertReports = this.certReportsRepository.GetCertReportAttachedCertReports(certReportId);
            #endregion attachedCertReports

            #region certReportContractDebts
            var certReportContractDebts = this.certReportsRepository.GetCertReportContractDebts(certReportId);

            foreach (var certReportContractDebt in certReportContractDebts)
            {
                var contractDebt = this.contractDebtsRepository.Find(certReportContractDebt.ContractDebtId);
                var contract = this.contractsRepository.Find(contractDebt.ContractId);
                var contractDebtVersion = this.contractDebtVersionsRepository.GetActualVersion(certReportContractDebt.ContractDebtId);
                var createdByUser = this.usersRepository.GetUserFullname(contractDebtVersion.CreatedByUserId);

                int? certReportOrderNum = null;
                if (contractDebt.CertReportId.HasValue)
                {
                    certReportOrderNum = this.certReportsRepository.GetOrderNum(contractDebt.CertReportId.Value);
                }

                certReportContractDebt.CertReportContractDebt = new CertReportContractDebtDO(contractDebt, contract, contractDebtVersion, createdByUser, certReportOrderNum);
            }
            #endregion certReportContractDebts

            #region certReportDebtReimbursedAmounts
            var certReportDebtReimbursedAmounts = this.certReportsRepository.GetCertReportDebtReimbursedAmounts(certReportId);

            foreach (var certReportDebtReimbursedAmount in certReportDebtReimbursedAmounts)
            {
                var reimbursedAmount = this.debtReimbursedAmountsRepository.Find(certReportDebtReimbursedAmount.AmountId);
                var contractDebtInfo = this.contractDebtsRepository.GetInfo(reimbursedAmount.ContractDebtId);
                var reimbursedAmountBasicData = this.debtReimbursedAmountsRepository.GetBasicData(certReportDebtReimbursedAmount.AmountId);

                certReportDebtReimbursedAmount.CertReportDebtReimbursedAmount = new CertReportDebtReimbursedAmountDO(reimbursedAmount, contractDebtInfo, reimbursedAmountBasicData);
            }
            #endregion certReportDebtReimbursedAmounts

            #region certReportSummary
            var certReportIntermediateFinalEligibleProgrammePriorityExpenses = this.certReportsRepository.GetCertReportIntermediateFinalEligibleProgrammePriorityExpenses(certReportId);

            var certReportApprovedAmountsCorrections = this.certReportsRepository.GetCertReportApprovedAmountsCorrections(certReportId);

            var certReportIntermediateFinalStateAidPaidAdvancePayments = this.certReportsRepository.GetCertReportIntermediateFinalStateAidPaidAdvancePayments(certReportId);

            var certReportReaffirmedCostsByAdministrativeAuthority = this.certReportsRepository.GetCertReportReaffirmedCostsByAdministrativeAuthority(certReportId);

            var certReportProgrammePaidContributionInfoForFinancialInstruments = this.certReportsRepository.GetCertReportProgrammePaidContributionInfoForFinancialInstruments(certReportId);
            #endregion //certReportSummary

            return new Tuple<CertReportSnapshotJson, IList<Guid>>(
                new CertReportSnapshotJson()
                {
                    CertReportPayments = certReportPayments,
                    CertReportAdvancePayments = certReportAdvancePayments,
                    CertReportFinancialCorrections = certReportFinancialCorrections,
                    CertReportCorrections = certReportCorrections,
                    CertReportFinancialRevalidations = certReportFinancialRevalidations,
                    CertReportRevalidations = certReportRevalidations,
                    CertReportFinancialCertCorrections = certReportFinancialCertCorrections,
                    CertReportCertCorrections = certReportCertCorrections,
                    CertReportDocuments = certReportDocuments,
                    CertReportCertificationDocuments = certReportCertificationDocuments,
                    AttachedCertReports = attachedCertReports,
                    CertReportContractDebts = certReportContractDebts,
                    CertReportDebtReimbursedAmounts = certReportDebtReimbursedAmounts,
                    CertReportIntermediateFinalEligibleProgrammePriorityExpenses = certReportIntermediateFinalEligibleProgrammePriorityExpenses,
                    CertReportApprovedAmountsCorrections = certReportApprovedAmountsCorrections,
                    CertReportIntermediateFinalStateAidPaidAdvancePayments = certReportIntermediateFinalStateAidPaidAdvancePayments,
                    CertReportReaffirmedCostsByAdministrativeAuthority = certReportReaffirmedCostsByAdministrativeAuthority,
                    CertReportProgrammePaidContributionInfoForFinancialInstruments = certReportProgrammePaidContributionInfoForFinancialInstruments,
                },
                blobKeys);
        }

        #endregion //CertReport

        #region CertReportDocuments

        public void CreateCertReportContractDebt(int certReportId, byte[] version, int[] contractDebtIds)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var contractDebts = new List<Eumis.Domain.Debts.ContractDebt>();
            var debtReimbursedAmounts = new List<Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.DebtReimbursedAmount>();

            foreach (var contractDebtId in contractDebtIds)
            {
                contractDebts.Add(this.contractDebtsRepository.Find(contractDebtId));
                debtReimbursedAmounts.AddRange(this.debtReimbursedAmountsRepository.FindAllForDebt(contractDebtId).Where(t => !t.CertReportId.HasValue && t.Status == Domain.MonitoringFinancialControl.ReimbursedAmounts.ReimbursedAmountStatus.Entered));
            }

            foreach (var contractDebt in contractDebts)
            {
                contractDebt.CertReportId = certReportId;
            }

            foreach (var debtReimbursedAmount in debtReimbursedAmounts)
            {
                debtReimbursedAmount.CertReportId = certReportId;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Debts.ContractDebt>(contractDebts, t => t.CertReportId);
            this.unitOfWork.BulkUpdate<Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.DebtReimbursedAmount>(debtReimbursedAmounts, t => t.CertReportId);

            this.unitOfWork.Save();
        }

        public void CreateCertReportDebtReimbursedAmount(int certReportId, byte[] version, int[] debtReimbursedAmountIds)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var debtReimbursedAmounts = new List<Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.DebtReimbursedAmount>();

            foreach (var debtReimbursedAmountId in debtReimbursedAmountIds)
            {
                debtReimbursedAmounts.Add(this.debtReimbursedAmountsRepository.Find(debtReimbursedAmountId));
            }

            foreach (var debtReimbursedAmount in debtReimbursedAmounts)
            {
                debtReimbursedAmount.CertReportId = certReportId;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.DebtReimbursedAmount>(debtReimbursedAmounts, t => t.CertReportId);

            this.unitOfWork.Save();
        }

        public void CreateCertReportPayment(int certReportId, byte[] version, int[] contractReportIds)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var budgetItems = new List<Eumis.Domain.Contracts.ContractReportFinancialCSDBudgetItem>();

            foreach (var contractReportId in contractReportIds)
            {
                budgetItems.AddRange(this.contractReportFinancialCSDBudgetItemsRepository.FindAllUnattached(contractReportId));
            }

            foreach (var budgetItem in budgetItems)
            {
                budgetItem.CertReportId = certReportId;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCSDBudgetItem>(budgetItems, t => t.CertReportId);

            this.unitOfWork.Save();
        }

        public void CreateCertReportAdvancePayment(int certReportId, byte[] version, int[] contractReportIds)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var advancePaymentAmounts = new List<Eumis.Domain.Contracts.ContractReportAdvancePaymentAmount>();

            foreach (var contractReportId in contractReportIds)
            {
                advancePaymentAmounts.AddRange(this.contractReportAdvancePaymentAmountsRepository.FindAll(contractReportId));
            }

            foreach (var advancePaymentAmount in advancePaymentAmounts)
            {
                advancePaymentAmount.CertReportId = certReportId;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportAdvancePaymentAmount>(advancePaymentAmounts, t => t.CertReportId);

            this.unitOfWork.Save();
        }

        public void CreateCertReportFinancialCorrection(int certReportId, byte[] version, int[] contractReportFinancialCorrectionIds)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var correctionCSDs = new List<Eumis.Domain.Contracts.ContractReportFinancialCorrectionCSD>();

            foreach (var contractReportFinancialCorrectionId in contractReportFinancialCorrectionIds)
            {
                correctionCSDs.AddRange(this.contractReportFinancialCorrectionCSDsRepository.FindAllUnattached(contractReportFinancialCorrectionId));
            }

            foreach (var correctionCSD in correctionCSDs)
            {
                correctionCSD.CertReportId = certReportId;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCorrectionCSD>(correctionCSDs, t => t.CertReportId);

            this.unitOfWork.Save();
        }

        public void CreateCertReportCorrection(int certReportId, byte[] version, int[] contractReportCorrectionIds)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var contractReportCorrections = new List<Eumis.Domain.Contracts.ContractReportCorrection>();

            foreach (var contractReportCorrectionId in contractReportCorrectionIds)
            {
                contractReportCorrections.Add(this.contractReportCorrectionsRepository.Find(contractReportCorrectionId));
            }

            foreach (var contractReportCorrection in contractReportCorrections)
            {
                contractReportCorrection.CertReportId = certReportId;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportCorrection>(contractReportCorrections, t => t.CertReportId);

            this.unitOfWork.Save();
        }

        public void CreateCertReportFinancialRevalidation(int certReportId, byte[] version, int[] contractReportFinancialRevalidationIds)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var revalidationCSDs = new List<Eumis.Domain.Contracts.ContractReportFinancialRevalidationCSD>();

            foreach (var contractReportFinancialRevalidationId in contractReportFinancialRevalidationIds)
            {
                revalidationCSDs.AddRange(this.contractReportFinancialRevalidationCSDsRepository.FindAllUnattached(contractReportFinancialRevalidationId));
            }

            foreach (var revalidationCSD in revalidationCSDs)
            {
                revalidationCSD.CertReportId = certReportId;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialRevalidationCSD>(revalidationCSDs, t => t.CertReportId);

            this.unitOfWork.Save();
        }

        public void CreateCertReportRevalidation(int certReportId, byte[] version, int[] contractReportRevalidationIds)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var contractReportRevalidations = new List<Eumis.Domain.Contracts.ContractReportRevalidation>();

            foreach (var contractReportRevalidationId in contractReportRevalidationIds)
            {
                contractReportRevalidations.Add(this.contractReportRevalidationsRepository.Find(contractReportRevalidationId));
            }

            foreach (var contractReportRevalidation in contractReportRevalidations)
            {
                contractReportRevalidation.CertReportId = certReportId;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportRevalidation>(contractReportRevalidations, t => t.CertReportId);

            this.unitOfWork.Save();
        }

        public void CreateCertReportFinancialCertCorrection(int certReportId, byte[] version, int[] contractReportFinancialCertCorrectionIds)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var certCorrectionCSDs = new List<Eumis.Domain.Contracts.ContractReportFinancialCertCorrectionCSD>();

            foreach (var contractReportFinancialCertCorrectionId in contractReportFinancialCertCorrectionIds)
            {
                certCorrectionCSDs.AddRange(this.contractReportFinancialCertCorrectionCSDsRepository.FindAllUnattached(contractReportFinancialCertCorrectionId));
            }

            foreach (var certCorrectionCSD in certCorrectionCSDs)
            {
                certCorrectionCSD.CertReportId = certReportId;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCertCorrectionCSD>(certCorrectionCSDs, t => t.CertReportId);

            this.unitOfWork.Save();
        }

        public void CreateCertReportCertCorrection(int certReportId, byte[] version, int[] contractReportCertCorrectionIds)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var contractReportCertCorrections = new List<Eumis.Domain.Contracts.ContractReportCertCorrection>();

            foreach (var contractReportCertCorrectionId in contractReportCertCorrectionIds)
            {
                contractReportCertCorrections.Add(this.contractReportCertCorrectionsRepository.Find(contractReportCertCorrectionId));
            }

            foreach (var contractReportCertCorrection in contractReportCertCorrections)
            {
                contractReportCertCorrection.CertReportId = certReportId;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportCertCorrection>(contractReportCertCorrections, t => t.CertReportId);

            this.unitOfWork.Save();
        }

        public void DeleteCertReportContractDebt(int certReportId, byte[] version, int contractDebtId)
        {
            if (this.CanDeleteCertReportContractDebt(certReportId, contractDebtId).Any())
            {
                throw new Exception("Cannot delete CertReportContractDebt");
            }

            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var contractDebt = this.contractDebtsRepository.Find(contractDebtId);

            var debtReimbursedAmounts = new List<Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.DebtReimbursedAmount>();

            debtReimbursedAmounts.AddRange(this.debtReimbursedAmountsRepository.FindAllForDebt(contractDebtId).Where(t => t.CertReportId == certReportId));

            contractDebt.CertReportId = null;

            foreach (var debtReimbursedAmount in debtReimbursedAmounts)
            {
                debtReimbursedAmount.CertReportId = null;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.DebtReimbursedAmount>(debtReimbursedAmounts, t => t.CertReportId);

            this.unitOfWork.Save();
        }

        public void DeleteCertReportDebtReimbursedAmount(int certReportId, byte[] version, int debtReimbursedAmountId)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var debtReimbursedAmount = this.debtReimbursedAmountsRepository.Find(debtReimbursedAmountId);

            debtReimbursedAmount.CertReportId = null;

            this.unitOfWork.Save();
        }

        public void DeleteCertReportPayment(int certReportId, byte[] version, int contractReportId)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var budgetItems = this.contractReportFinancialCSDBudgetItemsRepository.FindAllByCertReport(certReportId, contractReportId);

            foreach (var budgetItem in budgetItems)
            {
                budgetItem.CertReportId = null;
                budgetItem.UncertifiedApprovedEuAmount = null;
                budgetItem.UncertifiedApprovedBgAmount = null;
                budgetItem.UncertifiedApprovedBfpTotalAmount = null;
                budgetItem.UncertifiedApprovedSelfAmount = null;
                budgetItem.UncertifiedApprovedTotalAmount = null;
                budgetItem.CertifiedApprovedEuAmount = null;
                budgetItem.CertifiedApprovedBgAmount = null;
                budgetItem.CertifiedApprovedBfpTotalAmount = null;
                budgetItem.CertifiedApprovedSelfAmount = null;
                budgetItem.CertifiedApprovedTotalAmount = null;
                budgetItem.CertStatus = null;
                budgetItem.CertCheckedByUserId = null;
                budgetItem.CertCheckedDate = null;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCSDBudgetItem>(
                budgetItems,
                t => t.CertReportId,
                t => t.UncertifiedApprovedEuAmount,
                t => t.UncertifiedApprovedBgAmount,
                t => t.UncertifiedApprovedBfpTotalAmount,
                t => t.UncertifiedApprovedSelfAmount,
                t => t.UncertifiedApprovedTotalAmount,
                t => t.CertifiedApprovedEuAmount,
                t => t.CertifiedApprovedBgAmount,
                t => t.CertifiedApprovedBfpTotalAmount,
                t => t.CertifiedApprovedSelfAmount,
                t => t.CertifiedApprovedTotalAmount,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate);

            this.unitOfWork.Save();
        }

        public void DeleteCertReportAdvancePayment(int certReportId, byte[] version, int contractReportId)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var advancePaymentAmounts = this.contractReportAdvancePaymentAmountsRepository.FindAll(contractReportId);

            foreach (var advancePaymentAmount in advancePaymentAmounts)
            {
                advancePaymentAmount.CertReportId = null;
                advancePaymentAmount.CertStatus = null;
                advancePaymentAmount.CertCheckedByUserId = null;
                advancePaymentAmount.CertCheckedDate = null;
                advancePaymentAmount.UncertifiedApprovedEuAmount = null;
                advancePaymentAmount.UncertifiedApprovedBgAmount = null;
                advancePaymentAmount.UncertifiedApprovedBfpTotalAmount = null;
                advancePaymentAmount.CertifiedApprovedEuAmount = null;
                advancePaymentAmount.CertifiedApprovedBgAmount = null;
                advancePaymentAmount.CertifiedApprovedBfpTotalAmount = null;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportAdvancePaymentAmount>(
                advancePaymentAmounts,
                t => t.CertReportId,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.UncertifiedApprovedEuAmount,
                t => t.UncertifiedApprovedBgAmount,
                t => t.UncertifiedApprovedBfpTotalAmount,
                t => t.CertifiedApprovedEuAmount,
                t => t.CertifiedApprovedBgAmount,
                t => t.CertifiedApprovedBfpTotalAmount);

            this.unitOfWork.Save();
        }

        public void DeleteCertReportFinancialCorrection(int certReportId, byte[] version, int contractReportFinancialCorrectionId)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var correctionCSDs = this.contractReportFinancialCorrectionCSDsRepository.FindAllByCertReport(certReportId, contractReportFinancialCorrectionId);

            foreach (var correctionCSD in correctionCSDs)
            {
                correctionCSD.CertReportId = null;
                correctionCSD.CertStatus = null;
                correctionCSD.CertCheckedByUserId = null;
                correctionCSD.CertCheckedDate = null;
                correctionCSD.UncertifiedCorrectedApprovedEuAmount = null;
                correctionCSD.UncertifiedCorrectedApprovedBgAmount = null;
                correctionCSD.UncertifiedCorrectedApprovedBfpTotalAmount = null;
                correctionCSD.UncertifiedCorrectedApprovedSelfAmount = null;
                correctionCSD.UncertifiedCorrectedApprovedTotalAmount = null;
                correctionCSD.CertifiedCorrectedApprovedEuAmount = null;
                correctionCSD.CertifiedCorrectedApprovedBgAmount = null;
                correctionCSD.CertifiedCorrectedApprovedBfpTotalAmount = null;
                correctionCSD.CertifiedCorrectedApprovedSelfAmount = null;
                correctionCSD.CertifiedCorrectedApprovedTotalAmount = null;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCorrectionCSD>(
                correctionCSDs,
                t => t.CertReportId,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.UncertifiedCorrectedApprovedEuAmount,
                t => t.UncertifiedCorrectedApprovedBgAmount,
                t => t.UncertifiedCorrectedApprovedBfpTotalAmount,
                t => t.UncertifiedCorrectedApprovedSelfAmount,
                t => t.UncertifiedCorrectedApprovedTotalAmount,
                t => t.CertifiedCorrectedApprovedEuAmount,
                t => t.CertifiedCorrectedApprovedBgAmount,
                t => t.CertifiedCorrectedApprovedBfpTotalAmount,
                t => t.CertifiedCorrectedApprovedSelfAmount,
                t => t.CertifiedCorrectedApprovedTotalAmount);

            this.unitOfWork.Save();
        }

        public void DeleteCertReportCorrection(int certReportId, byte[] version, int contractReportCorrectionId)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var contractReportCorrection = this.contractReportCorrectionsRepository.Find(contractReportCorrectionId);

            contractReportCorrection.CertReportId = null;
            contractReportCorrection.CertStatus = null;
            contractReportCorrection.CertCheckedByUserId = null;
            contractReportCorrection.CertCheckedDate = null;
            contractReportCorrection.UncertifiedCorrectedApprovedEuAmount = null;
            contractReportCorrection.UncertifiedCorrectedApprovedBgAmount = null;
            contractReportCorrection.UncertifiedCorrectedApprovedBfpTotalAmount = null;
            contractReportCorrection.UncertifiedCorrectedApprovedCrossAmount = null;
            contractReportCorrection.UncertifiedCorrectedApprovedSelfAmount = null;
            contractReportCorrection.UncertifiedCorrectedApprovedTotalAmount = null;
            contractReportCorrection.CertifiedCorrectedApprovedEuAmount = null;
            contractReportCorrection.CertifiedCorrectedApprovedBgAmount = null;
            contractReportCorrection.CertifiedCorrectedApprovedBfpTotalAmount = null;
            contractReportCorrection.CertifiedCorrectedApprovedCrossAmount = null;
            contractReportCorrection.CertifiedCorrectedApprovedSelfAmount = null;

            this.unitOfWork.Save();
        }

        public void DeleteCertReportFinancialRevalidation(int certReportId, byte[] version, int contractReportFinancialRevalidationId)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var revalidationCSDs = this.contractReportFinancialRevalidationCSDsRepository.FindAllByCertReport(certReportId, contractReportFinancialRevalidationId);

            foreach (var revalidationCSD in revalidationCSDs)
            {
                revalidationCSD.CertReportId = null;
                revalidationCSD.CertStatus = null;
                revalidationCSD.CertCheckedByUserId = null;
                revalidationCSD.CertCheckedDate = null;
                revalidationCSD.UncertifiedRevalidatedEuAmount = null;
                revalidationCSD.UncertifiedRevalidatedBgAmount = null;
                revalidationCSD.UncertifiedRevalidatedBfpTotalAmount = null;
                revalidationCSD.UncertifiedRevalidatedSelfAmount = null;
                revalidationCSD.UncertifiedRevalidatedTotalAmount = null;
                revalidationCSD.CertifiedRevalidatedEuAmount = null;
                revalidationCSD.CertifiedRevalidatedBgAmount = null;
                revalidationCSD.CertifiedRevalidatedBfpTotalAmount = null;
                revalidationCSD.CertifiedRevalidatedSelfAmount = null;
                revalidationCSD.CertifiedRevalidatedTotalAmount = null;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialRevalidationCSD>(
                revalidationCSDs,
                t => t.CertReportId,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.UncertifiedRevalidatedEuAmount,
                t => t.UncertifiedRevalidatedBgAmount,
                t => t.UncertifiedRevalidatedBfpTotalAmount,
                t => t.UncertifiedRevalidatedSelfAmount,
                t => t.UncertifiedRevalidatedTotalAmount,
                t => t.CertifiedRevalidatedEuAmount,
                t => t.CertifiedRevalidatedBgAmount,
                t => t.CertifiedRevalidatedBfpTotalAmount,
                t => t.CertifiedRevalidatedSelfAmount,
                t => t.CertifiedRevalidatedTotalAmount);

            this.unitOfWork.Save();
        }

        public void DeleteCertReportRevalidation(int certReportId, byte[] version, int contractReportRevalidationId)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var contractReportRevalidation = this.contractReportRevalidationsRepository.Find(contractReportRevalidationId);

            contractReportRevalidation.CertReportId = null;
            contractReportRevalidation.CertStatus = null;
            contractReportRevalidation.CertCheckedByUserId = null;
            contractReportRevalidation.CertCheckedDate = null;
            contractReportRevalidation.UncertifiedRevalidatedEuAmount = null;
            contractReportRevalidation.UncertifiedRevalidatedBgAmount = null;
            contractReportRevalidation.UncertifiedRevalidatedBfpTotalAmount = null;
            contractReportRevalidation.UncertifiedRevalidatedCrossAmount = null;
            contractReportRevalidation.UncertifiedRevalidatedSelfAmount = null;
            contractReportRevalidation.UncertifiedRevalidatedTotalAmount = null;
            contractReportRevalidation.CertifiedRevalidatedEuAmount = null;
            contractReportRevalidation.CertifiedRevalidatedBgAmount = null;
            contractReportRevalidation.CertifiedRevalidatedBfpTotalAmount = null;
            contractReportRevalidation.CertifiedRevalidatedCrossAmount = null;
            contractReportRevalidation.CertifiedRevalidatedSelfAmount = null;
            contractReportRevalidation.CertifiedRevalidatedTotalAmount = null;

            this.unitOfWork.Save();
        }

        public void DeleteCertReportFinancialCertCorrection(int certReportId, byte[] version, int contractReportFinancialCertCorrectionId)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var certCorrectionCSDs = this.contractReportFinancialCertCorrectionCSDsRepository.FindAllByCertReport(certReportId, contractReportFinancialCertCorrectionId);

            foreach (var certCorrectionCSD in certCorrectionCSDs)
            {
                certCorrectionCSD.CertReportId = null;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCertCorrectionCSD>(certCorrectionCSDs, t => t.CertReportId);

            this.unitOfWork.Save();
        }

        public void DeleteCertReportCertCorrection(int certReportId, byte[] version, int contractReportCertCorrectionId)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var contractReportCertCorrection = this.contractReportCertCorrectionsRepository.Find(contractReportCertCorrectionId);

            contractReportCertCorrection.CertReportId = null;

            this.unitOfWork.Save();
        }

        public IList<string> CanDeleteCertReportContractDebt(int certReportId, int contractDebtId)
        {
            var errors = new List<string>();
            var reimbursedAmounts = this.debtReimbursedAmountsRepository.FindAllForDebt(contractDebtId);
            if (reimbursedAmounts.Where(t => t.CertReportId.HasValue && t.CertReportId.Value != certReportId).Any())
            {
                errors.Add("Не можете да изтриете дълга, защото има възстановени суми към него, включени в друг доклад по сертификация");
            }

            return errors;
        }

        #endregion //CertReportDocuments

        #region CertReportDocumentsPartialInclusion

        public void CreateCertReportPaymentCSDs(int certReportId, byte[] version, int contractReportId, int[] contractReportFinancialCSDBudgetItemIds)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var budgetItems = this.contractReportFinancialCSDBudgetItemsRepository.FindAll(contractReportId, contractReportFinancialCSDBudgetItemIds);

            foreach (var budgetItem in budgetItems)
            {
                budgetItem.CertReportId = certReportId;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCSDBudgetItem>(budgetItems, t => t.CertReportId);

            this.unitOfWork.Save();
        }

        public void CreateCertReportAdvancePaymentAmounts(int certReportId, byte[] version, int contractReportId, int[] contractReportAdvancePaymentAmountIds)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var advancePaymentAmounts = this.contractReportAdvancePaymentAmountsRepository.FindAll(contractReportId, contractReportAdvancePaymentAmountIds);

            foreach (var advancePaymentAmount in advancePaymentAmounts)
            {
                advancePaymentAmount.CertReportId = certReportId;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportAdvancePaymentAmount>(advancePaymentAmounts, t => t.CertReportId);

            this.unitOfWork.Save();
        }

        public void CreateCertReportFinancialCorrectionCSDs(int certReportId, byte[] version, int contractReportFinancialCorrectionId, int[] contractReportFinancialCorrectionCSDIds)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var correctionCSDs = this.contractReportFinancialCorrectionCSDsRepository.FindAll(contractReportFinancialCorrectionId, contractReportFinancialCorrectionCSDIds);

            foreach (var correctionCSD in correctionCSDs)
            {
                correctionCSD.CertReportId = certReportId;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCorrectionCSD>(correctionCSDs, t => t.CertReportId);

            this.unitOfWork.Save();
        }

        public void CreateCertReportFinancialRevalidationCSDs(int certReportId, byte[] version, int contractReportFinancialRevalidationId, int[] contractReportFinancialRevalidationCSDIds)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var revalidationCSDs = this.contractReportFinancialRevalidationCSDsRepository.FindAll(contractReportFinancialRevalidationId, contractReportFinancialRevalidationCSDIds);

            foreach (var revalidationCSD in revalidationCSDs)
            {
                revalidationCSD.CertReportId = certReportId;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialRevalidationCSD>(revalidationCSDs, t => t.CertReportId);

            this.unitOfWork.Save();
        }

        public void CreateCertReportFinancialCertCorrectionCSDs(int certReportId, byte[] version, int contractReportFinancialCertCorrectionId, int[] contractReportFinancialCertCorrectionCSDIds)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var certCorrectionCSDs = this.contractReportFinancialCertCorrectionCSDsRepository.FindAll(contractReportFinancialCertCorrectionId, contractReportFinancialCertCorrectionCSDIds);

            foreach (var certCorrectionCSD in certCorrectionCSDs)
            {
                certCorrectionCSD.CertReportId = certReportId;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCertCorrectionCSD>(certCorrectionCSDs, t => t.CertReportId);

            this.unitOfWork.Save();
        }

        public void DeleteCertReportPaymentCSDs(int certReportId, byte[] version, int contractReportId, int[] contractReportFinancialCSDBudgetItemIds)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var budgetItems = this.contractReportFinancialCSDBudgetItemsRepository.FindAll(contractReportId, contractReportFinancialCSDBudgetItemIds);

            foreach (var budgetItem in budgetItems)
            {
                budgetItem.CertReportId = null;
                budgetItem.UncertifiedApprovedEuAmount = null;
                budgetItem.UncertifiedApprovedBgAmount = null;
                budgetItem.UncertifiedApprovedBfpTotalAmount = null;
                budgetItem.UncertifiedApprovedSelfAmount = null;
                budgetItem.UncertifiedApprovedTotalAmount = null;
                budgetItem.CertifiedApprovedEuAmount = null;
                budgetItem.CertifiedApprovedBgAmount = null;
                budgetItem.CertifiedApprovedBfpTotalAmount = null;
                budgetItem.CertifiedApprovedSelfAmount = null;
                budgetItem.CertifiedApprovedTotalAmount = null;
                budgetItem.CertStatus = null;
                budgetItem.CertCheckedByUserId = null;
                budgetItem.CertCheckedDate = null;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCSDBudgetItem>(
                budgetItems,
                t => t.CertReportId,
                t => t.UncertifiedApprovedEuAmount,
                t => t.UncertifiedApprovedBgAmount,
                t => t.UncertifiedApprovedBfpTotalAmount,
                t => t.UncertifiedApprovedSelfAmount,
                t => t.UncertifiedApprovedTotalAmount,
                t => t.CertifiedApprovedEuAmount,
                t => t.CertifiedApprovedBgAmount,
                t => t.CertifiedApprovedBfpTotalAmount,
                t => t.CertifiedApprovedSelfAmount,
                t => t.CertifiedApprovedTotalAmount,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate);

            this.unitOfWork.Save();
        }

        public void DeleteCertReportAdvancePaymentAmounts(int certReportId, byte[] version, int contractReportId, int[] contractReportAdvancePaymentAmountIds)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var advancePaymentAmounts = this.contractReportAdvancePaymentAmountsRepository.FindAll(contractReportId, contractReportAdvancePaymentAmountIds);

            foreach (var advancePaymentAmount in advancePaymentAmounts)
            {
                advancePaymentAmount.CertReportId = null;
                advancePaymentAmount.CertStatus = null;
                advancePaymentAmount.CertCheckedByUserId = null;
                advancePaymentAmount.CertCheckedDate = null;
                advancePaymentAmount.UncertifiedApprovedEuAmount = null;
                advancePaymentAmount.UncertifiedApprovedBgAmount = null;
                advancePaymentAmount.UncertifiedApprovedBfpTotalAmount = null;
                advancePaymentAmount.CertifiedApprovedEuAmount = null;
                advancePaymentAmount.CertifiedApprovedBgAmount = null;
                advancePaymentAmount.CertifiedApprovedBfpTotalAmount = null;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportAdvancePaymentAmount>(
                advancePaymentAmounts,
                t => t.CertReportId,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.UncertifiedApprovedEuAmount,
                t => t.UncertifiedApprovedBgAmount,
                t => t.UncertifiedApprovedBfpTotalAmount,
                t => t.CertifiedApprovedEuAmount,
                t => t.CertifiedApprovedBgAmount,
                t => t.CertifiedApprovedBfpTotalAmount);

            this.unitOfWork.Save();
        }

        public void DeleteCertReportFinancialCorrectionCSDs(int certReportId, byte[] version, int contractReportFinancialCorrectionId, int[] contractReportFinancialCorrectionCSDIds)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var correctionCSDs = this.contractReportFinancialCorrectionCSDsRepository.FindAll(contractReportFinancialCorrectionId, contractReportFinancialCorrectionCSDIds);

            foreach (var correctionCSD in correctionCSDs)
            {
                correctionCSD.CertReportId = null;
                correctionCSD.CertStatus = null;
                correctionCSD.CertCheckedByUserId = null;
                correctionCSD.CertCheckedDate = null;
                correctionCSD.UncertifiedCorrectedApprovedEuAmount = null;
                correctionCSD.UncertifiedCorrectedApprovedBgAmount = null;
                correctionCSD.UncertifiedCorrectedApprovedBfpTotalAmount = null;
                correctionCSD.UncertifiedCorrectedApprovedSelfAmount = null;
                correctionCSD.UncertifiedCorrectedApprovedTotalAmount = null;
                correctionCSD.CertifiedCorrectedApprovedEuAmount = null;
                correctionCSD.CertifiedCorrectedApprovedBgAmount = null;
                correctionCSD.CertifiedCorrectedApprovedBfpTotalAmount = null;
                correctionCSD.CertifiedCorrectedApprovedSelfAmount = null;
                correctionCSD.CertifiedCorrectedApprovedTotalAmount = null;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCorrectionCSD>(
                correctionCSDs,
                t => t.CertReportId,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.UncertifiedCorrectedApprovedEuAmount,
                t => t.UncertifiedCorrectedApprovedBgAmount,
                t => t.UncertifiedCorrectedApprovedBfpTotalAmount,
                t => t.UncertifiedCorrectedApprovedSelfAmount,
                t => t.UncertifiedCorrectedApprovedTotalAmount,
                t => t.CertifiedCorrectedApprovedEuAmount,
                t => t.CertifiedCorrectedApprovedBgAmount,
                t => t.CertifiedCorrectedApprovedBfpTotalAmount,
                t => t.CertifiedCorrectedApprovedSelfAmount,
                t => t.CertifiedCorrectedApprovedTotalAmount);

            this.unitOfWork.Save();
        }

        public void DeleteCertReportFinancialRevalidationCSDs(int certReportId, byte[] version, int contractReportFinancialRevalidationId, int[] contractReportFinancialRevalidationCSDIds)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var revalidationCSDs = this.contractReportFinancialRevalidationCSDsRepository.FindAll(contractReportFinancialRevalidationId, contractReportFinancialRevalidationCSDIds);

            foreach (var revalidationCSD in revalidationCSDs)
            {
                revalidationCSD.CertReportId = null;
                revalidationCSD.CertStatus = null;
                revalidationCSD.CertCheckedByUserId = null;
                revalidationCSD.CertCheckedDate = null;
                revalidationCSD.UncertifiedRevalidatedEuAmount = null;
                revalidationCSD.UncertifiedRevalidatedBgAmount = null;
                revalidationCSD.UncertifiedRevalidatedBfpTotalAmount = null;
                revalidationCSD.UncertifiedRevalidatedSelfAmount = null;
                revalidationCSD.UncertifiedRevalidatedTotalAmount = null;
                revalidationCSD.CertifiedRevalidatedEuAmount = null;
                revalidationCSD.CertifiedRevalidatedBgAmount = null;
                revalidationCSD.CertifiedRevalidatedBfpTotalAmount = null;
                revalidationCSD.CertifiedRevalidatedSelfAmount = null;
                revalidationCSD.CertifiedRevalidatedTotalAmount = null;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialRevalidationCSD>(
                revalidationCSDs,
                t => t.CertReportId,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.UncertifiedRevalidatedEuAmount,
                t => t.UncertifiedRevalidatedBgAmount,
                t => t.UncertifiedRevalidatedBfpTotalAmount,
                t => t.UncertifiedRevalidatedSelfAmount,
                t => t.UncertifiedRevalidatedTotalAmount,
                t => t.CertifiedRevalidatedEuAmount,
                t => t.CertifiedRevalidatedBgAmount,
                t => t.CertifiedRevalidatedBfpTotalAmount,
                t => t.CertifiedRevalidatedSelfAmount,
                t => t.CertifiedRevalidatedTotalAmount);

            this.unitOfWork.Save();
        }

        public void DeleteCertReportFinancialCertCorrectionCSDs(int certReportId, byte[] version, int contractReportFinancialCertCorrectionId, int[] contractReportFinancialCertCorrectionCSDIds)
        {
            var certReport = this.certReportsRepository.FindForUpdate(certReportId, version);

            certReport.AssertIsDraft();

            var certCorrectionCSDs = this.contractReportFinancialCertCorrectionCSDsRepository.FindAll(contractReportFinancialCertCorrectionId, contractReportFinancialCertCorrectionCSDIds);

            foreach (var certCorrectionCSD in certCorrectionCSDs)
            {
                certCorrectionCSD.CertReportId = null;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCertCorrectionCSD>(certCorrectionCSDs, t => t.CertReportId);

            this.unitOfWork.Save();
        }

        #endregion //CertReportDocumentsPartialInclusion
    }
}
