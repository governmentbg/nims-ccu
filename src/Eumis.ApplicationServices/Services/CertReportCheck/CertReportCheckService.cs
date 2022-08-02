using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.CertReports.Repositories;
using Eumis.Data.ContractReportAdvancePaymentAmounts.Repositories;
using Eumis.Data.ContractReportCorrections.Repositories;
using Eumis.Data.ContractReportFinancialCorrections.Repositories;
using Eumis.Data.ContractReportFinancialCSDs.Repositories;
using Eumis.Data.ContractReportFinancialRevalidations.Repositories;
using Eumis.Data.ContractReportRevalidations.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Domain;
using Eumis.Domain.CertReports;
using Eumis.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.ApplicationServices.Services.CertReportCheck
{
    internal class CertReportCheckService : ICertReportCheckService
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private ICertReportsRepository certReportsRepository;
        private IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository;
        private IContractReportFinancialCorrectionCSDsRepository contractReportFinancialCorrectionCSDsRepository;
        private IContractReportAdvancePaymentAmountsRepository contractReportAdvancePaymentAmountsRepository;
        private IContractReportFinancialRevalidationCSDsRepository contractReportFinancialRevalidationCSDsRepository;
        private IContractReportRevalidationsRepository contractReportRevalidationsRepository;
        private IContractReportCorrectionsRepository contractReportCorrectionsRepository;

        public CertReportCheckService(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            ICertReportsRepository certReportsRepository,
            IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository,
            IContractReportFinancialCorrectionCSDsRepository contractReportFinancialCorrectionCSDsRepository,
            IContractReportAdvancePaymentAmountsRepository contractReportAdvancePaymentAmountsRepository,
            IContractReportFinancialRevalidationCSDsRepository contractReportFinancialRevalidationCSDsRepository,
            IContractReportRevalidationsRepository contractReportRevalidationsRepository,
            IContractReportCorrectionsRepository contractReportCorrectionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.certReportsRepository = certReportsRepository;
            this.contractReportFinancialCSDBudgetItemsRepository = contractReportFinancialCSDBudgetItemsRepository;
            this.contractReportFinancialCorrectionCSDsRepository = contractReportFinancialCorrectionCSDsRepository;
            this.contractReportAdvancePaymentAmountsRepository = contractReportAdvancePaymentAmountsRepository;
            this.contractReportFinancialRevalidationCSDsRepository = contractReportFinancialRevalidationCSDsRepository;
            this.contractReportRevalidationsRepository = contractReportRevalidationsRepository;
            this.contractReportCorrectionsRepository = contractReportCorrectionsRepository;
        }

        public void UpdateContractReportFinancialCSDBudgetItem(
            int certReportId,
            int contractReportFinancialCSDBudgetItemId,
            byte[] version,
            decimal? uncertifiedApprovedEuAmount,
            decimal? uncertifiedApprovedBgAmount,
            decimal? uncertifiedApprovedBfpTotalAmount,
            decimal? uncertifiedApprovedSelfAmount,
            decimal? uncertifiedApprovedTotalAmount,
            decimal? certifiedApprovedEuAmount,
            decimal? certifiedApprovedBgAmount,
            decimal? certifiedApprovedBfpTotalAmount,
            decimal? certifiedApprovedSelfAmount,
            decimal? certifiedApprovedTotalAmount)
        {
            var contractReportFinancialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.FindForUpdate(contractReportFinancialCSDBudgetItemId, version);

            var certReport = this.certReportsRepository.Find(certReportId);

            if (certReport.Status != CertReportStatus.Unchecked)
            {
                throw new DomainException("Cannot edit a ContractReportFinancialCSD when the CertReport is in status other than 'Unchecked'");
            }

            this.AssertIsCertDraftContractReportFinancialCSDBudgetItem(contractReportFinancialCSDBudgetItem.CertStatus.Value);

            contractReportFinancialCSDBudgetItem.UpdateCertAttributes(
                uncertifiedApprovedEuAmount,
                uncertifiedApprovedBgAmount,
                uncertifiedApprovedBfpTotalAmount,
                uncertifiedApprovedSelfAmount,
                uncertifiedApprovedTotalAmount,
                certifiedApprovedEuAmount,
                certifiedApprovedBgAmount,
                certifiedApprovedBfpTotalAmount,
                certifiedApprovedSelfAmount,
                certifiedApprovedTotalAmount);

            this.unitOfWork.Save();
        }

        public IList<string> CanChangeContractReportFinancialCSDBudgetItemCertStatus(int contractReportFinancialCSDBudgetItemId)
        {
            var errors = new List<string>();

            var contractReportFinancialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(contractReportFinancialCSDBudgetItemId);

            if (!contractReportFinancialCSDBudgetItem.UncertifiedApprovedBgAmount.HasValue ||
                !contractReportFinancialCSDBudgetItem.UncertifiedApprovedEuAmount.HasValue ||
                !contractReportFinancialCSDBudgetItem.UncertifiedApprovedSelfAmount.HasValue ||
                !contractReportFinancialCSDBudgetItem.UncertifiedApprovedTotalAmount.HasValue)
            {
                errors.Add("Всички полета от секция 'Несертифицирана сума на разходооправдателния документ за конкретния бюджетен ред и дейност' трябва да са попълнени");
            }

            return errors;
        }

        public void ChangeContractReportFinancialCSDBudgetItemCertStatus(
            int contractReportFinancialCSDBudgetItemId,
            byte[] version,
            ContractReportFinancialCSDBudgetItemCertStatus status)
        {
            var contractReportFinancialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.FindForUpdate(contractReportFinancialCSDBudgetItemId, version);

            contractReportFinancialCSDBudgetItem.CertStatus = status;
            contractReportFinancialCSDBudgetItem.ModifyDate = DateTime.Now;

            if (status == ContractReportFinancialCSDBudgetItemCertStatus.Ended)
            {
                if (this.CanChangeContractReportFinancialCSDBudgetItemCertStatus(contractReportFinancialCSDBudgetItemId).Any())
                {
                    throw new DomainException("Cannot change ContractReportFinancialCSDBudgetItem cert status to 'Ended'");
                }

                contractReportFinancialCSDBudgetItem.CertCheckedByUserId = this.accessContext.UserId;
                contractReportFinancialCSDBudgetItem.CertCheckedDate = DateTime.Now;
            }

            this.unitOfWork.Save();
        }

        public void CertifyAllContractReportFinancialCSDBudgetItems(int certReportId)
        {
            var budgetItems = this.contractReportFinancialCSDBudgetItemsRepository.FindAllByCertReport(certReportId);

            foreach (var budgetItem in budgetItems)
            {
                budgetItem.UpdateCertAttributes(
                    0,
                    0,
                    0,
                    0,
                    0,
                    budgetItem.ApprovedEuAmount,
                    budgetItem.ApprovedBgAmount,
                    budgetItem.ApprovedBfpTotalAmount,
                    budgetItem.ApprovedSelfAmount,
                    budgetItem.ApprovedTotalAmount);

                budgetItem.CertStatus = ContractReportFinancialCSDBudgetItemCertStatus.Ended;
                budgetItem.CertCheckedByUserId = this.accessContext.UserId;
                budgetItem.CertCheckedDate = DateTime.Now;
                budgetItem.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCSDBudgetItem>(
                budgetItems,
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
                t => t.CertCheckedDate,
                t => t.ModifyDate);

            this.unitOfWork.Save();
        }

        public void CertifyAllContractReportFinancialCSDBudgetItems(int certReportId, int contractReportId)
        {
            var budgetItems = this.contractReportFinancialCSDBudgetItemsRepository.FindAllByCertReport(certReportId, contractReportId);

            foreach (var budgetItem in budgetItems)
            {
                budgetItem.UpdateCertAttributes(
                    0,
                    0,
                    0,
                    0,
                    0,
                    budgetItem.ApprovedEuAmount,
                    budgetItem.ApprovedBgAmount,
                    budgetItem.ApprovedBfpTotalAmount,
                    budgetItem.ApprovedSelfAmount,
                    budgetItem.ApprovedTotalAmount);

                budgetItem.CertStatus = ContractReportFinancialCSDBudgetItemCertStatus.Ended;
                budgetItem.CertCheckedByUserId = this.accessContext.UserId;
                budgetItem.CertCheckedDate = DateTime.Now;
                budgetItem.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCSDBudgetItem>(
                budgetItems,
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
                t => t.CertCheckedDate,
                t => t.ModifyDate);

            this.unitOfWork.Save();
        }

        public void UncertifyAllContractReportFinancialCSDBudgetItems(int certReportId)
        {
            var budgetItems = this.contractReportFinancialCSDBudgetItemsRepository.FindAllByCertReport(certReportId);

            foreach (var budgetItem in budgetItems)
            {
                budgetItem.UpdateCertAttributes(
                    budgetItem.ApprovedEuAmount,
                    budgetItem.ApprovedBgAmount,
                    budgetItem.ApprovedBfpTotalAmount,
                    budgetItem.ApprovedSelfAmount,
                    budgetItem.ApprovedTotalAmount,
                    0,
                    0,
                    0,
                    0,
                    0);

                budgetItem.CertStatus = ContractReportFinancialCSDBudgetItemCertStatus.Ended;
                budgetItem.CertCheckedByUserId = this.accessContext.UserId;
                budgetItem.CertCheckedDate = DateTime.Now;
                budgetItem.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCSDBudgetItem>(
                budgetItems,
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
                t => t.CertCheckedDate,
                t => t.ModifyDate);

            this.unitOfWork.Save();
        }

        public void UncertifyAllContractReportFinancialCSDBudgetItems(int certReportId, int contractReportId)
        {
            var budgetItems = this.contractReportFinancialCSDBudgetItemsRepository.FindAllByCertReport(certReportId, contractReportId);

            foreach (var budgetItem in budgetItems)
            {
                budgetItem.UpdateCertAttributes(
                      budgetItem.ApprovedEuAmount,
                      budgetItem.ApprovedBgAmount,
                      budgetItem.ApprovedBfpTotalAmount,
                      budgetItem.ApprovedSelfAmount,
                      budgetItem.ApprovedTotalAmount,
                      0,
                      0,
                      0,
                      0,
                      0);

                budgetItem.CertStatus = ContractReportFinancialCSDBudgetItemCertStatus.Ended;
                budgetItem.CertCheckedByUserId = this.accessContext.UserId;
                budgetItem.CertCheckedDate = DateTime.Now;
                budgetItem.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCSDBudgetItem>(
                budgetItems,
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
                t => t.CertCheckedDate,
                t => t.ModifyDate);

            this.unitOfWork.Save();
        }

        // ContractReportFinancialCorrectionCSD
        public void UpdateContractReportFinancialCorrectionCSD(
            int certReportId,
            int contractReportFinancialCorrectionCSDId,
            byte[] version,
            decimal? uncertifiedCorrectedApprovedEuAmount,
            decimal? uncertifiedCorrectedApprovedBgAmount,
            decimal? uncertifiedCorrectedApprovedBfpTotalAmount,
            decimal? uncertifiedCorrectedApprovedSelfAmount,
            decimal? uncertifiedCorrectedApprovedTotalAmount,
            decimal? certifiedCorrectedApprovedEuAmount,
            decimal? certifiedCorrectedApprovedBgAmount,
            decimal? certifiedCorrectedApprovedBfpTotalAmount,
            decimal? certifiedCorrectedApprovedSelfAmount,
            decimal? certifiedCorrectedApprovedTotalAmount)
        {
            var financialCorrectionCSD = this.contractReportFinancialCorrectionCSDsRepository.FindForUpdate(contractReportFinancialCorrectionCSDId, version);

            this.AssertIsCertDraftContractReportFinancialCorrectionCSD(financialCorrectionCSD.CertStatus.Value);

            financialCorrectionCSD.UpdateCertAttributes(
                uncertifiedCorrectedApprovedEuAmount,
                uncertifiedCorrectedApprovedBgAmount,
                uncertifiedCorrectedApprovedBfpTotalAmount,
                uncertifiedCorrectedApprovedSelfAmount,
                uncertifiedCorrectedApprovedTotalAmount,
                certifiedCorrectedApprovedEuAmount,
                certifiedCorrectedApprovedBgAmount,
                certifiedCorrectedApprovedBfpTotalAmount,
                certifiedCorrectedApprovedSelfAmount,
                certifiedCorrectedApprovedTotalAmount);

            this.unitOfWork.Save();
        }

        public IList<string> CanChangeContractReportFinancialCorrectionCSDCertStatusToEnded(int contractReportFinancialCorrectionCSDId)
        {
            var errors = new List<string>();

            var financialCorrectionCSD = this.contractReportFinancialCorrectionCSDsRepository.Find(contractReportFinancialCorrectionCSDId);

            if (!financialCorrectionCSD.UncertifiedCorrectedApprovedBgAmount.HasValue ||
                !financialCorrectionCSD.UncertifiedCorrectedApprovedEuAmount.HasValue ||
                !financialCorrectionCSD.UncertifiedCorrectedApprovedSelfAmount.HasValue ||
                !financialCorrectionCSD.UncertifiedCorrectedApprovedTotalAmount.HasValue)
            {
                errors.Add("Всички полета от секция 'Несертифицирана сума на коригиран разходооправдателен документ за конкретния бюджетен ред и дейност' трябва да са попълнени");
            }

            return errors;
        }

        public void ChangeContractReportFinancialCorrectionCSDCertStatus(int contractReportFinancialCorrectionCSDId, byte[] version, ContractReportFinancialCorrectionCSDCertStatus status)
        {
            var financialCorrectionCSD = this.contractReportFinancialCorrectionCSDsRepository.FindForUpdate(contractReportFinancialCorrectionCSDId, version);

            if (status == ContractReportFinancialCorrectionCSDCertStatus.Ended)
            {
                if (this.CanChangeContractReportFinancialCorrectionCSDCertStatusToEnded(contractReportFinancialCorrectionCSDId).Any())
                {
                    throw new DomainException("Cannot change ContractReportFinancialCorrectionCSD cert status to 'Ended'");
                }

                financialCorrectionCSD.CertCheckedByUserId = this.accessContext.UserId;
                financialCorrectionCSD.CertCheckedDate = DateTime.Now;
            }

            financialCorrectionCSD.CertStatus = status;
            financialCorrectionCSD.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();
        }

        public void CertifyAllContractReportFinancialCorrectionCSDs(int certReportId)
        {
            var financialCorrectionCSDs = this.contractReportFinancialCorrectionCSDsRepository.FindAllByCertReport(certReportId);

            foreach (var financialCorrectionCSD in financialCorrectionCSDs)
            {
                financialCorrectionCSD.UpdateCertAttributes(
                    0,
                    0,
                    0,
                    0,
                    0,
                    financialCorrectionCSD.CorrectedApprovedEuAmount,
                    financialCorrectionCSD.CorrectedApprovedBgAmount,
                    financialCorrectionCSD.CorrectedApprovedBfpTotalAmount,
                    financialCorrectionCSD.CorrectedApprovedSelfAmount,
                    financialCorrectionCSD.CorrectedApprovedTotalAmount);

                financialCorrectionCSD.CertStatus = ContractReportFinancialCorrectionCSDCertStatus.Ended;
                financialCorrectionCSD.CertCheckedByUserId = this.accessContext.UserId;
                financialCorrectionCSD.CertCheckedDate = DateTime.Now;
                financialCorrectionCSD.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCorrectionCSD>(
                financialCorrectionCSDs,
                t => t.UncertifiedCorrectedApprovedEuAmount,
                t => t.UncertifiedCorrectedApprovedBgAmount,
                t => t.UncertifiedCorrectedApprovedBfpTotalAmount,
                t => t.UncertifiedCorrectedApprovedSelfAmount,
                t => t.UncertifiedCorrectedApprovedTotalAmount,
                t => t.CertifiedCorrectedApprovedEuAmount,
                t => t.CertifiedCorrectedApprovedBgAmount,
                t => t.CertifiedCorrectedApprovedBfpTotalAmount,
                t => t.CertifiedCorrectedApprovedSelfAmount,
                t => t.CertifiedCorrectedApprovedTotalAmount,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.ModifyDate);

            this.unitOfWork.Save();
        }

        public void CertifyAllContractReportFinancialCorrectionCSDs(int certReportId, int contractReportFinancialCorrectionId)
        {
            var financialCorrectionCSDs = this.contractReportFinancialCorrectionCSDsRepository.FindAllByCertReport(certReportId, contractReportFinancialCorrectionId);

            foreach (var financialCorrectionCSD in financialCorrectionCSDs)
            {
                financialCorrectionCSD.UpdateCertAttributes(
                    0,
                    0,
                    0,
                    0,
                    0,
                    financialCorrectionCSD.CorrectedApprovedEuAmount,
                    financialCorrectionCSD.CorrectedApprovedBgAmount,
                    financialCorrectionCSD.CorrectedApprovedBfpTotalAmount,
                    financialCorrectionCSD.CorrectedApprovedSelfAmount,
                    financialCorrectionCSD.CorrectedApprovedTotalAmount);

                financialCorrectionCSD.CertStatus = ContractReportFinancialCorrectionCSDCertStatus.Ended;
                financialCorrectionCSD.CertCheckedByUserId = this.accessContext.UserId;
                financialCorrectionCSD.CertCheckedDate = DateTime.Now;
                financialCorrectionCSD.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCorrectionCSD>(
                financialCorrectionCSDs,
                t => t.UncertifiedCorrectedApprovedEuAmount,
                t => t.UncertifiedCorrectedApprovedBgAmount,
                t => t.UncertifiedCorrectedApprovedBfpTotalAmount,
                t => t.UncertifiedCorrectedApprovedSelfAmount,
                t => t.UncertifiedCorrectedApprovedTotalAmount,
                t => t.CertifiedCorrectedApprovedEuAmount,
                t => t.CertifiedCorrectedApprovedBgAmount,
                t => t.CertifiedCorrectedApprovedBfpTotalAmount,
                t => t.CertifiedCorrectedApprovedSelfAmount,
                t => t.CertifiedCorrectedApprovedTotalAmount,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.ModifyDate);

            this.unitOfWork.Save();
        }

        public void UncertifyAllContractReportFinancialCorrectionCSDs(int certReportId)
        {
            var financialCorrectionCSDs = this.contractReportFinancialCorrectionCSDsRepository.FindAllByCertReport(certReportId);

            foreach (var financialCorrectionCSD in financialCorrectionCSDs)
            {
                financialCorrectionCSD.UpdateCertAttributes(
                    financialCorrectionCSD.CorrectedApprovedEuAmount,
                    financialCorrectionCSD.CorrectedApprovedBgAmount,
                    financialCorrectionCSD.CorrectedApprovedBfpTotalAmount,
                    financialCorrectionCSD.CorrectedApprovedSelfAmount,
                    financialCorrectionCSD.CorrectedApprovedTotalAmount,
                    0,
                    0,
                    0,
                    0,
                    0);

                financialCorrectionCSD.CertStatus = ContractReportFinancialCorrectionCSDCertStatus.Ended;
                financialCorrectionCSD.CertCheckedByUserId = this.accessContext.UserId;
                financialCorrectionCSD.CertCheckedDate = DateTime.Now;
                financialCorrectionCSD.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCorrectionCSD>(
                financialCorrectionCSDs,
                t => t.UncertifiedCorrectedApprovedEuAmount,
                t => t.UncertifiedCorrectedApprovedBgAmount,
                t => t.UncertifiedCorrectedApprovedBfpTotalAmount,
                t => t.UncertifiedCorrectedApprovedSelfAmount,
                t => t.UncertifiedCorrectedApprovedTotalAmount,
                t => t.CertifiedCorrectedApprovedEuAmount,
                t => t.CertifiedCorrectedApprovedBgAmount,
                t => t.CertifiedCorrectedApprovedBfpTotalAmount,
                t => t.CertifiedCorrectedApprovedSelfAmount,
                t => t.CertifiedCorrectedApprovedTotalAmount,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.ModifyDate);

            this.unitOfWork.Save();
        }

        public void UncertifyAllContractReportFinancialCorrectionCSDs(int certReportId, int contractReportFinancialCorrectionId)
        {
            var financialCorrectionCSDs = this.contractReportFinancialCorrectionCSDsRepository.FindAllByCertReport(certReportId, contractReportFinancialCorrectionId);

            foreach (var financialCorrectionCSD in financialCorrectionCSDs)
            {
                financialCorrectionCSD.UpdateCertAttributes(
                    financialCorrectionCSD.CorrectedApprovedEuAmount,
                    financialCorrectionCSD.CorrectedApprovedBgAmount,
                    financialCorrectionCSD.CorrectedApprovedBfpTotalAmount,
                    financialCorrectionCSD.CorrectedApprovedSelfAmount,
                    financialCorrectionCSD.CorrectedApprovedTotalAmount,
                    0,
                    0,
                    0,
                    0,
                    0);

                financialCorrectionCSD.CertStatus = ContractReportFinancialCorrectionCSDCertStatus.Ended;
                financialCorrectionCSD.CertCheckedByUserId = this.accessContext.UserId;
                financialCorrectionCSD.CertCheckedDate = DateTime.Now;
                financialCorrectionCSD.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialCorrectionCSD>(
                financialCorrectionCSDs,
                t => t.UncertifiedCorrectedApprovedEuAmount,
                t => t.UncertifiedCorrectedApprovedBgAmount,
                t => t.UncertifiedCorrectedApprovedBfpTotalAmount,
                t => t.UncertifiedCorrectedApprovedSelfAmount,
                t => t.UncertifiedCorrectedApprovedTotalAmount,
                t => t.CertifiedCorrectedApprovedEuAmount,
                t => t.CertifiedCorrectedApprovedBgAmount,
                t => t.CertifiedCorrectedApprovedBfpTotalAmount,
                t => t.CertifiedCorrectedApprovedSelfAmount,
                t => t.CertifiedCorrectedApprovedTotalAmount,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.ModifyDate);

            this.unitOfWork.Save();
        }

        // ContractReportCorrection
        public void UpdateContractReportCorrection(
            int certReportId,
            int contractReportCorrectionId,
            byte[] version,
            decimal? uncertifiedCorrectedApprovedEuAmount,
            decimal? uncertifiedCorrectedApprovedBgAmount,
            decimal? uncertifiedCorrectedApprovedBfpTotalAmount,
            decimal? uncertifiedCorrectedApprovedCrossAmount,
            decimal? uncertifiedCorrectedApprovedSelfAmount,
            decimal? uncertifiedCorrectedApprovedTotalAmount,
            decimal? certifiedCorrectedApprovedEuAmount,
            decimal? certifiedCorrectedApprovedBgAmount,
            decimal? certifiedCorrectedApprovedBfpTotalAmount,
            decimal? certifiedCorrectedApprovedCrossAmount,
            decimal? certifiedCorrectedApprovedSelfAmount,
            decimal? certifiedCorrectedApprovedTotalAmount)
        {
            var correction = this.contractReportCorrectionsRepository.FindForUpdate(contractReportCorrectionId, version);

            this.AssertIsCertDraftContractReportCorrection(correction.CertStatus.Value);

            correction.UpdateCertAttributes(
                uncertifiedCorrectedApprovedEuAmount,
                uncertifiedCorrectedApprovedBgAmount,
                uncertifiedCorrectedApprovedBfpTotalAmount,
                uncertifiedCorrectedApprovedCrossAmount,
                uncertifiedCorrectedApprovedSelfAmount,
                uncertifiedCorrectedApprovedTotalAmount,
                certifiedCorrectedApprovedEuAmount,
                certifiedCorrectedApprovedBgAmount,
                certifiedCorrectedApprovedBfpTotalAmount,
                certifiedCorrectedApprovedCrossAmount,
                certifiedCorrectedApprovedSelfAmount,
                certifiedCorrectedApprovedTotalAmount);

            this.unitOfWork.Save();
        }

        public IList<string> CanChangeContractReportCorrectionCertStatusToEnded(int contractReportCorrectionId)
        {
            var errors = new List<string>();

            var correction = this.contractReportCorrectionsRepository.Find(contractReportCorrectionId);

            if (!correction.UncertifiedCorrectedApprovedBgAmount.HasValue ||
                !correction.UncertifiedCorrectedApprovedEuAmount.HasValue ||
                !correction.UncertifiedCorrectedApprovedSelfAmount.HasValue ||
                !correction.UncertifiedCorrectedApprovedTotalAmount.HasValue ||
                !correction.UncertifiedCorrectedApprovedCrossAmount.HasValue)
            {
                errors.Add("Всички полета от секция 'Несертифицирана сума на коригирания документ' трябва да са попълнени");
            }

            return errors;
        }

        public void ChangeContractReportCorrectionCertStatus(int contractReportCorrectionId, byte[] version, ContractReportCorrectionCertStatus status)
        {
            var correction = this.contractReportCorrectionsRepository.FindForUpdate(contractReportCorrectionId, version);

            if (status == ContractReportCorrectionCertStatus.Ended)
            {
                if (this.CanChangeContractReportCorrectionCertStatusToEnded(contractReportCorrectionId).Any())
                {
                    throw new DomainException("Cannot change ContractReportCorrection cert status to 'Ended'");
                }

                correction.CertCheckedByUserId = this.accessContext.UserId;
                correction.CertCheckedDate = DateTime.Now;
            }

            correction.CertStatus = status;
            correction.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();
        }

        public void CertifyAllContractReportCorrections(int certReportId)
        {
            var corrections = this.contractReportCorrectionsRepository.FindAllByCertReport(certReportId);

            foreach (var correction in corrections)
            {
                correction.UpdateCertAttributes(
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    correction.CorrectedApprovedEuAmount,
                    correction.CorrectedApprovedBgAmount,
                    correction.CorrectedApprovedBfpTotalAmount,
                    correction.CorrectedApprovedCrossAmount,
                    correction.CorrectedApprovedSelfAmount,
                    correction.CorrectedApprovedTotalAmount);

                correction.CertStatus = ContractReportCorrectionCertStatus.Ended;
                correction.CertCheckedByUserId = this.accessContext.UserId;
                correction.CertCheckedDate = DateTime.Now;
                correction.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportCorrection>(
                corrections,
                t => t.UncertifiedCorrectedApprovedEuAmount,
                t => t.UncertifiedCorrectedApprovedBgAmount,
                t => t.UncertifiedCorrectedApprovedBfpTotalAmount,
                t => t.UncertifiedCorrectedApprovedCrossAmount,
                t => t.UncertifiedCorrectedApprovedSelfAmount,
                t => t.UncertifiedCorrectedApprovedTotalAmount,
                t => t.CertifiedCorrectedApprovedEuAmount,
                t => t.CertifiedCorrectedApprovedBgAmount,
                t => t.CertifiedCorrectedApprovedBfpTotalAmount,
                t => t.CertifiedCorrectedApprovedCrossAmount,
                t => t.CertifiedCorrectedApprovedSelfAmount,
                t => t.CertifiedCorrectedApprovedTotalAmount,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.ModifyDate);

            this.unitOfWork.Save();
        }

        public void UncertifyAllContractReportCorrections(int certReportId)
        {
            var corrections = this.contractReportCorrectionsRepository.FindAllByCertReport(certReportId);

            foreach (var correction in corrections)
            {
                correction.UpdateCertAttributes(
                    correction.CorrectedApprovedEuAmount,
                    correction.CorrectedApprovedBgAmount,
                    correction.CorrectedApprovedBfpTotalAmount,
                    correction.CorrectedApprovedCrossAmount,
                    correction.CorrectedApprovedSelfAmount,
                    correction.CorrectedApprovedTotalAmount,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0);

                correction.CertStatus = ContractReportCorrectionCertStatus.Ended;
                correction.CertCheckedByUserId = this.accessContext.UserId;
                correction.CertCheckedDate = DateTime.Now;
                correction.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportCorrection>(
                corrections,
                t => t.UncertifiedCorrectedApprovedEuAmount,
                t => t.UncertifiedCorrectedApprovedBgAmount,
                t => t.UncertifiedCorrectedApprovedBfpTotalAmount,
                t => t.UncertifiedCorrectedApprovedSelfAmount,
                t => t.UncertifiedCorrectedApprovedTotalAmount,
                t => t.CertifiedCorrectedApprovedEuAmount,
                t => t.CertifiedCorrectedApprovedBgAmount,
                t => t.CertifiedCorrectedApprovedBfpTotalAmount,
                t => t.CertifiedCorrectedApprovedSelfAmount,
                t => t.CertifiedCorrectedApprovedTotalAmount,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.ModifyDate);

            this.unitOfWork.Save();
        }

        // ContractReportAdvancePaymentAmount
        public void UpdateContractReportAdvancePaymentAmount(
            int certReportId,
            int contractReportAdvancePaymentAmountId,
            byte[] version,
            decimal? unCertifiedApprovedEuAmount,
            decimal? unCertifiedApprovedBgAmount,
            decimal? unCertifiedApprovedBfpTotalAmount,
            decimal? certifiedApprovedEuAmount,
            decimal? certifiedApprovedBgAmount,
            decimal? certifiedApprovedBfpTotalAmount)
        {
            var advancePaymentAmount = this.contractReportAdvancePaymentAmountsRepository.FindForUpdate(contractReportAdvancePaymentAmountId, version);

            this.AssertIsCertDraftContractReportAdvancePaymentAmount(advancePaymentAmount.CertStatus.Value);

            advancePaymentAmount.UpdateCertAttributes(
                unCertifiedApprovedEuAmount,
                unCertifiedApprovedBgAmount,
                unCertifiedApprovedBfpTotalAmount,
                certifiedApprovedEuAmount,
                certifiedApprovedBgAmount,
                certifiedApprovedBfpTotalAmount);

            this.unitOfWork.Save();
        }

        public IList<string> CanChangeContractReportAdvancePaymentAmountCertStatusToEnded(int contractReportAdvancePaymentAmountId)
        {
            var errors = new List<string>();

            var advancePaymentAmount = this.contractReportAdvancePaymentAmountsRepository.Find(contractReportAdvancePaymentAmountId);
            if (!advancePaymentAmount.UncertifiedApprovedBfpTotalAmount.HasValue)
            {
                errors.Add("Не можете да промените статуса на сертифицираното АП на 'Приключен', защото полето 'Обща стойност на несертифицираните средства' трябва да е попълнено'");
            }

            return errors;
        }

        public void ChangeContractReportAdvancePaymentAmountCertStatus(int contractReportAdvancePaymentAmountId, byte[] version, ContractReportAdvancePaymentAmountCertStatus status)
        {
            var advancePaymentAmount = this.contractReportAdvancePaymentAmountsRepository.FindForUpdate(contractReportAdvancePaymentAmountId, version);

            if (status == ContractReportAdvancePaymentAmountCertStatus.Ended)
            {
                if (this.CanChangeContractReportAdvancePaymentAmountCertStatusToEnded(contractReportAdvancePaymentAmountId).Any())
                {
                    throw new DomainException("Cannot change ContractReportAdvancePaymentAmount cert status to 'Ended'");
                }

                advancePaymentAmount.CertCheckedByUserId = this.accessContext.UserId;
                advancePaymentAmount.CertCheckedDate = DateTime.Now;
            }

            advancePaymentAmount.CertStatus = status;
            advancePaymentAmount.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();
        }

        public void CertifyAllContractReportAdvancePaymentAmounts(int certReportId)
        {
            var advancePaymentAmounts = this.contractReportAdvancePaymentAmountsRepository.FindAllByCertReport(certReportId);

            foreach (var advancePaymentAmount in advancePaymentAmounts)
            {
                advancePaymentAmount.UpdateCertAttributes(
                    0,
                    0,
                    0,
                    advancePaymentAmount.ApprovedEuAmount,
                    advancePaymentAmount.ApprovedBgAmount,
                    advancePaymentAmount.ApprovedBfpTotalAmount);

                advancePaymentAmount.CertStatus = ContractReportAdvancePaymentAmountCertStatus.Ended;
                advancePaymentAmount.CertCheckedByUserId = this.accessContext.UserId;
                advancePaymentAmount.CertCheckedDate = DateTime.Now;
                advancePaymentAmount.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportAdvancePaymentAmount>(
                advancePaymentAmounts,
                t => t.UncertifiedApprovedEuAmount,
                t => t.UncertifiedApprovedBgAmount,
                t => t.UncertifiedApprovedBfpTotalAmount,
                t => t.CertifiedApprovedEuAmount,
                t => t.CertifiedApprovedBgAmount,
                t => t.CertifiedApprovedBfpTotalAmount,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.ModifyDate);

            this.unitOfWork.Save();
        }

        public void CertifyAllContractReportAdvancePaymentAmounts(int certReportId, int contractReportId)
        {
            var advancePaymentAmounts = this.contractReportAdvancePaymentAmountsRepository.FindAllByCertReport(certReportId, contractReportId);

            foreach (var advancePaymentAmount in advancePaymentAmounts)
            {
                advancePaymentAmount.UpdateCertAttributes(
                    0,
                    0,
                    0,
                    advancePaymentAmount.ApprovedEuAmount,
                    advancePaymentAmount.ApprovedBgAmount,
                    advancePaymentAmount.ApprovedBfpTotalAmount);

                advancePaymentAmount.CertStatus = ContractReportAdvancePaymentAmountCertStatus.Ended;
                advancePaymentAmount.CertCheckedByUserId = this.accessContext.UserId;
                advancePaymentAmount.CertCheckedDate = DateTime.Now;
                advancePaymentAmount.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportAdvancePaymentAmount>(
                advancePaymentAmounts,
                t => t.UncertifiedApprovedEuAmount,
                t => t.UncertifiedApprovedBgAmount,
                t => t.UncertifiedApprovedBfpTotalAmount,
                t => t.CertifiedApprovedEuAmount,
                t => t.CertifiedApprovedBgAmount,
                t => t.CertifiedApprovedBfpTotalAmount,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.ModifyDate);

            this.unitOfWork.Save();
        }

        public void UncertifyAllContractReportAdvancePaymentAmounts(int certReportId)
        {
            var advancePaymentAmounts = this.contractReportAdvancePaymentAmountsRepository.FindAllByCertReport(certReportId);

            foreach (var advancePaymentAmount in advancePaymentAmounts)
            {
                advancePaymentAmount.UpdateCertAttributes(
                    advancePaymentAmount.ApprovedEuAmount,
                    advancePaymentAmount.ApprovedBgAmount,
                    advancePaymentAmount.ApprovedBfpTotalAmount,
                    0,
                    0,
                    0);

                advancePaymentAmount.CertStatus = ContractReportAdvancePaymentAmountCertStatus.Ended;
                advancePaymentAmount.CertCheckedByUserId = this.accessContext.UserId;
                advancePaymentAmount.CertCheckedDate = DateTime.Now;
                advancePaymentAmount.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportAdvancePaymentAmount>(
                advancePaymentAmounts,
                t => t.UncertifiedApprovedEuAmount,
                t => t.UncertifiedApprovedBgAmount,
                t => t.UncertifiedApprovedBfpTotalAmount,
                t => t.CertifiedApprovedEuAmount,
                t => t.CertifiedApprovedBgAmount,
                t => t.CertifiedApprovedBfpTotalAmount,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.ModifyDate);

            this.unitOfWork.Save();
        }

        public void UncertifyAllContractReportAdvancePaymentAmounts(int certReportId, int contractReportId)
        {
            var advancePaymentAmounts = this.contractReportAdvancePaymentAmountsRepository.FindAllByCertReport(certReportId, contractReportId);

            foreach (var advancePaymentAmount in advancePaymentAmounts)
            {
                advancePaymentAmount.UpdateCertAttributes(
                    advancePaymentAmount.ApprovedEuAmount,
                    advancePaymentAmount.ApprovedBgAmount,
                    advancePaymentAmount.ApprovedBfpTotalAmount,
                    0,
                    0,
                    0);

                advancePaymentAmount.CertStatus = ContractReportAdvancePaymentAmountCertStatus.Ended;
                advancePaymentAmount.CertCheckedByUserId = this.accessContext.UserId;
                advancePaymentAmount.CertCheckedDate = DateTime.Now;
                advancePaymentAmount.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportAdvancePaymentAmount>(
                advancePaymentAmounts,
                t => t.UncertifiedApprovedEuAmount,
                t => t.UncertifiedApprovedBgAmount,
                t => t.UncertifiedApprovedBfpTotalAmount,
                t => t.CertifiedApprovedEuAmount,
                t => t.CertifiedApprovedBgAmount,
                t => t.CertifiedApprovedBfpTotalAmount,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.ModifyDate);

            this.unitOfWork.Save();
        }

        // ContractReportFinancialRevalidationCSD
        public void UpdateContractReportFinancialRevalidationCSD(
            int certReportId,
            int contractReportFinancialRevalidationCSDId,
            byte[] version,
            decimal? uncertifiedRevalidatedEuAmount,
            decimal? uncertifiedRevalidatedBgAmount,
            decimal? uncertifiedRevalidatedBfpTotalAmount,
            decimal? uncertifiedRevalidatedSelfAmount,
            decimal? uncertifiedRevalidatedTotalAmount,
            decimal? certifiedRevalidatedEuAmount,
            decimal? certifiedRevalidatedBgAmount,
            decimal? certifiedRevalidatedBfpTotalAmount,
            decimal? certifiedRevalidatedSelfAmount,
            decimal? certifiedRevalidatedTotalAmount)
        {
            var financialRevalidationCSD = this.contractReportFinancialRevalidationCSDsRepository.FindForUpdate(contractReportFinancialRevalidationCSDId, version);

            this.AssertIsCertDraftContractReportFinancialRevalidationCSD(financialRevalidationCSD.CertStatus.Value);

            financialRevalidationCSD.UpdateCertAttributes(
                uncertifiedRevalidatedEuAmount,
                uncertifiedRevalidatedBgAmount,
                uncertifiedRevalidatedBfpTotalAmount,
                uncertifiedRevalidatedSelfAmount,
                uncertifiedRevalidatedTotalAmount,
                certifiedRevalidatedEuAmount,
                certifiedRevalidatedBgAmount,
                certifiedRevalidatedBfpTotalAmount,
                certifiedRevalidatedSelfAmount,
                certifiedRevalidatedTotalAmount);

            this.unitOfWork.Save();
        }

        public IList<string> CanChangeContractReportFinancialRevalidationCSDCertStatusToEnded(int contractReportFinancialRevalidationCSDId)
        {
            var errors = new List<string>();

            var financialRevalidationCSD = this.contractReportFinancialRevalidationCSDsRepository.Find(contractReportFinancialRevalidationCSDId);

            if (!financialRevalidationCSD.UncertifiedRevalidatedBgAmount.HasValue ||
                !financialRevalidationCSD.UncertifiedRevalidatedEuAmount.HasValue ||
                !financialRevalidationCSD.UncertifiedRevalidatedSelfAmount.HasValue ||
                !financialRevalidationCSD.UncertifiedRevalidatedTotalAmount.HasValue)
            {
                errors.Add("Всички полета от секция 'Несертифицирана сума на препотвърден разходооправдателен документ за конкретния бюджетен ред и дейност' трябва да са попълнени");
            }

            return errors;
        }

        public void ChangeContractReportFinancialRevalidationCSDCertStatus(int contractReportFinancialRevalidationCSDId, byte[] version, ContractReportFinancialRevalidationCSDCertStatus status)
        {
            var financialRevalidationCSD = this.contractReportFinancialRevalidationCSDsRepository.FindForUpdate(contractReportFinancialRevalidationCSDId, version);

            if (status == ContractReportFinancialRevalidationCSDCertStatus.Ended)
            {
                if (this.CanChangeContractReportFinancialRevalidationCSDCertStatusToEnded(contractReportFinancialRevalidationCSDId).Any())
                {
                    throw new DomainException("Cannot change ContractReportFinancialRevalidationCSD cert status to 'Ended'");
                }

                financialRevalidationCSD.CertCheckedByUserId = this.accessContext.UserId;
                financialRevalidationCSD.CertCheckedDate = DateTime.Now;
            }

            financialRevalidationCSD.CertStatus = status;
            financialRevalidationCSD.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();
        }

        public void CertifyAllContractReportFinancialRevalidationCSDs(int certReportId)
        {
            var financialRevalidationCSDs = this.contractReportFinancialRevalidationCSDsRepository.FindAllByCertReport(certReportId);

            foreach (var financialRevalidationCSD in financialRevalidationCSDs)
            {
                financialRevalidationCSD.UpdateCertAttributes(
                    0,
                    0,
                    0,
                    0,
                    0,
                    financialRevalidationCSD.RevalidatedEuAmount,
                    financialRevalidationCSD.RevalidatedBgAmount,
                    financialRevalidationCSD.RevalidatedBfpTotalAmount,
                    financialRevalidationCSD.RevalidatedSelfAmount,
                    financialRevalidationCSD.RevalidatedTotalAmount);

                financialRevalidationCSD.CertStatus = ContractReportFinancialRevalidationCSDCertStatus.Ended;
                financialRevalidationCSD.CertCheckedByUserId = this.accessContext.UserId;
                financialRevalidationCSD.CertCheckedDate = DateTime.Now;
                financialRevalidationCSD.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialRevalidationCSD>(
                financialRevalidationCSDs,
                t => t.UncertifiedRevalidatedEuAmount,
                t => t.UncertifiedRevalidatedBgAmount,
                t => t.UncertifiedRevalidatedBfpTotalAmount,
                t => t.UncertifiedRevalidatedSelfAmount,
                t => t.UncertifiedRevalidatedTotalAmount,
                t => t.CertifiedRevalidatedEuAmount,
                t => t.CertifiedRevalidatedBgAmount,
                t => t.CertifiedRevalidatedBfpTotalAmount,
                t => t.CertifiedRevalidatedSelfAmount,
                t => t.CertifiedRevalidatedTotalAmount,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.ModifyDate);

            this.unitOfWork.Save();
        }

        public void CertifyAllContractReportFinancialRevalidationCSDs(int certReportId, int contractReportFinancialRevalidationId)
        {
            var financialRevalidationCSDs = this.contractReportFinancialRevalidationCSDsRepository.FindAllByCertReport(certReportId, contractReportFinancialRevalidationId);

            foreach (var financialRevalidationCSD in financialRevalidationCSDs)
            {
                financialRevalidationCSD.UpdateCertAttributes(
                    0,
                    0,
                    0,
                    0,
                    0,
                    financialRevalidationCSD.RevalidatedEuAmount,
                    financialRevalidationCSD.RevalidatedBgAmount,
                    financialRevalidationCSD.RevalidatedBfpTotalAmount,
                    financialRevalidationCSD.RevalidatedSelfAmount,
                    financialRevalidationCSD.RevalidatedTotalAmount);

                financialRevalidationCSD.CertStatus = ContractReportFinancialRevalidationCSDCertStatus.Ended;
                financialRevalidationCSD.CertCheckedByUserId = this.accessContext.UserId;
                financialRevalidationCSD.CertCheckedDate = DateTime.Now;
                financialRevalidationCSD.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialRevalidationCSD>(
                financialRevalidationCSDs,
                t => t.UncertifiedRevalidatedEuAmount,
                t => t.UncertifiedRevalidatedBgAmount,
                t => t.UncertifiedRevalidatedBfpTotalAmount,
                t => t.UncertifiedRevalidatedSelfAmount,
                t => t.UncertifiedRevalidatedTotalAmount,
                t => t.CertifiedRevalidatedEuAmount,
                t => t.CertifiedRevalidatedBgAmount,
                t => t.CertifiedRevalidatedBfpTotalAmount,
                t => t.CertifiedRevalidatedSelfAmount,
                t => t.CertifiedRevalidatedTotalAmount,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.ModifyDate);

            this.unitOfWork.Save();
        }

        public void UncertifyAllContractReportFinancialRevalidationCSDs(int certReportId)
        {
            var financialRevalidationCSDs = this.contractReportFinancialRevalidationCSDsRepository.FindAllByCertReport(certReportId);

            foreach (var financialRevalidationCSD in financialRevalidationCSDs)
            {
                financialRevalidationCSD.UpdateCertAttributes(
                    financialRevalidationCSD.RevalidatedEuAmount,
                    financialRevalidationCSD.RevalidatedBgAmount,
                    financialRevalidationCSD.RevalidatedBfpTotalAmount,
                    financialRevalidationCSD.RevalidatedSelfAmount,
                    financialRevalidationCSD.RevalidatedTotalAmount,
                    0,
                    0,
                    0,
                    0,
                    0);

                financialRevalidationCSD.CertStatus = ContractReportFinancialRevalidationCSDCertStatus.Ended;
                financialRevalidationCSD.CertCheckedByUserId = this.accessContext.UserId;
                financialRevalidationCSD.CertCheckedDate = DateTime.Now;
                financialRevalidationCSD.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialRevalidationCSD>(
                financialRevalidationCSDs,
                t => t.UncertifiedRevalidatedEuAmount,
                t => t.UncertifiedRevalidatedBgAmount,
                t => t.UncertifiedRevalidatedBfpTotalAmount,
                t => t.UncertifiedRevalidatedSelfAmount,
                t => t.UncertifiedRevalidatedTotalAmount,
                t => t.CertifiedRevalidatedEuAmount,
                t => t.CertifiedRevalidatedBgAmount,
                t => t.CertifiedRevalidatedBfpTotalAmount,
                t => t.CertifiedRevalidatedSelfAmount,
                t => t.CertifiedRevalidatedTotalAmount,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.ModifyDate);

            this.unitOfWork.Save();
        }

        public void UncertifyAllContractReportFinancialRevalidationCSDs(int certReportId, int contractReportFinancialRevalidationId)
        {
            var financialRevalidationCSDs = this.contractReportFinancialRevalidationCSDsRepository.FindAllByCertReport(certReportId, contractReportFinancialRevalidationId);

            foreach (var financialRevalidationCSD in financialRevalidationCSDs)
            {
                financialRevalidationCSD.UpdateCertAttributes(
                    financialRevalidationCSD.RevalidatedEuAmount,
                    financialRevalidationCSD.RevalidatedBgAmount,
                    financialRevalidationCSD.RevalidatedBfpTotalAmount,
                    financialRevalidationCSD.RevalidatedSelfAmount,
                    financialRevalidationCSD.RevalidatedTotalAmount,
                    0,
                    0,
                    0,
                    0,
                    0);

                financialRevalidationCSD.CertStatus = ContractReportFinancialRevalidationCSDCertStatus.Ended;
                financialRevalidationCSD.CertCheckedByUserId = this.accessContext.UserId;
                financialRevalidationCSD.CertCheckedDate = DateTime.Now;
                financialRevalidationCSD.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportFinancialRevalidationCSD>(
                financialRevalidationCSDs,
                t => t.UncertifiedRevalidatedEuAmount,
                t => t.UncertifiedRevalidatedBgAmount,
                t => t.UncertifiedRevalidatedBfpTotalAmount,
                t => t.UncertifiedRevalidatedSelfAmount,
                t => t.UncertifiedRevalidatedTotalAmount,
                t => t.CertifiedRevalidatedEuAmount,
                t => t.CertifiedRevalidatedBgAmount,
                t => t.CertifiedRevalidatedBfpTotalAmount,
                t => t.CertifiedRevalidatedSelfAmount,
                t => t.CertifiedRevalidatedTotalAmount,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.ModifyDate);

            this.unitOfWork.Save();
        }

        // ContractReportRevalidation
        public void UpdateContractReportRevalidation(
            int certReportId,
            int contractReportRevalidationId,
            byte[] version,
            decimal? uncertifiedRevalidatedEuAmount,
            decimal? uncertifiedRevalidatedBgAmount,
            decimal? uncertifiedRevalidatedBfpTotalAmount,
            decimal? uncertifiedRevalidatedCrossAmount,
            decimal? uncertifiedRevalidatedSelfAmount,
            decimal? uncertifiedRevalidatedTotalAmount,
            decimal? certifiedRevalidatedEuAmount,
            decimal? certifiedRevalidatedBgAmount,
            decimal? certifiedRevalidatedBfpTotalAmount,
            decimal? certifiedRevalidatedCrossAmount,
            decimal? certifiedRevalidatedSelfAmount,
            decimal? certifiedRevalidatedTotalAmount)
        {
            var revalidation = this.contractReportRevalidationsRepository.FindForUpdate(contractReportRevalidationId, version);

            this.AssertIsCertDraftContractReportRevalidation(revalidation.CertStatus.Value);

            revalidation.UpdateCertAttributes(
                uncertifiedRevalidatedEuAmount,
                uncertifiedRevalidatedBgAmount,
                uncertifiedRevalidatedBfpTotalAmount,
                uncertifiedRevalidatedCrossAmount,
                uncertifiedRevalidatedSelfAmount,
                uncertifiedRevalidatedTotalAmount,
                certifiedRevalidatedEuAmount,
                certifiedRevalidatedBgAmount,
                certifiedRevalidatedBfpTotalAmount,
                certifiedRevalidatedCrossAmount,
                certifiedRevalidatedSelfAmount,
                certifiedRevalidatedTotalAmount);

            this.unitOfWork.Save();
        }

        public IList<string> CanChangeContractReportRevalidationCertStatusToEnded(int contractReportRevalidationId)
        {
            var errors = new List<string>();

            var revalidation = this.contractReportRevalidationsRepository.Find(contractReportRevalidationId);

            if (!revalidation.UncertifiedRevalidatedBgAmount.HasValue ||
                !revalidation.UncertifiedRevalidatedEuAmount.HasValue ||
                !revalidation.UncertifiedRevalidatedSelfAmount.HasValue ||
                !revalidation.UncertifiedRevalidatedTotalAmount.HasValue ||
                !revalidation.UncertifiedRevalidatedCrossAmount.HasValue)
            {
                errors.Add("Всички полета от секция 'Несертифицирана сума на препотвърдения документ' трябва да са попълнени");
            }

            return errors;
        }

        public void ChangeContractReportRevalidationCertStatus(int contractReportRevalidationId, byte[] version, ContractReportRevalidationCertStatus status)
        {
            var revalidation = this.contractReportRevalidationsRepository.FindForUpdate(contractReportRevalidationId, version);

            if (status == ContractReportRevalidationCertStatus.Ended)
            {
                if (this.CanChangeContractReportRevalidationCertStatusToEnded(contractReportRevalidationId).Any())
                {
                    throw new DomainException("Cannot change ContractReportRevalidation cert status to 'Ended'");
                }

                revalidation.CertCheckedByUserId = this.accessContext.UserId;
                revalidation.CertCheckedDate = DateTime.Now;
            }

            revalidation.CertStatus = status;
            revalidation.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();
        }

        public void CertifyAllContractReportRevalidations(int certReportId)
        {
            var revalidations = this.contractReportRevalidationsRepository.FindAllByCertReport(certReportId);

            foreach (var revalidation in revalidations)
            {
                revalidation.UpdateCertAttributes(
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    revalidation.RevalidatedEuAmount,
                    revalidation.RevalidatedBgAmount,
                    revalidation.RevalidatedBfpTotalAmount,
                    revalidation.RevalidatedCrossAmount,
                    revalidation.RevalidatedSelfAmount,
                    revalidation.RevalidatedTotalAmount);

                revalidation.CertStatus = ContractReportRevalidationCertStatus.Ended;
                revalidation.CertCheckedByUserId = this.accessContext.UserId;
                revalidation.CertCheckedDate = DateTime.Now;
                revalidation.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportRevalidation>(
                revalidations,
                t => t.UncertifiedRevalidatedEuAmount,
                t => t.UncertifiedRevalidatedBgAmount,
                t => t.UncertifiedRevalidatedBfpTotalAmount,
                t => t.UncertifiedRevalidatedCrossAmount,
                t => t.UncertifiedRevalidatedSelfAmount,
                t => t.UncertifiedRevalidatedTotalAmount,
                t => t.CertifiedRevalidatedEuAmount,
                t => t.CertifiedRevalidatedBgAmount,
                t => t.CertifiedRevalidatedBfpTotalAmount,
                t => t.CertifiedRevalidatedCrossAmount,
                t => t.CertifiedRevalidatedSelfAmount,
                t => t.CertifiedRevalidatedTotalAmount,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.ModifyDate);

            this.unitOfWork.Save();
        }

        public void UncertifyAllContractReportRevalidations(int certReportId)
        {
            var revalidations = this.contractReportRevalidationsRepository.FindAllByCertReport(certReportId);

            foreach (var revalidation in revalidations)
            {
                revalidation.UpdateCertAttributes(
                    revalidation.RevalidatedEuAmount,
                    revalidation.RevalidatedBgAmount,
                    revalidation.RevalidatedBfpTotalAmount,
                    revalidation.RevalidatedCrossAmount,
                    revalidation.RevalidatedSelfAmount,
                    revalidation.RevalidatedTotalAmount,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0);

                revalidation.CertStatus = ContractReportRevalidationCertStatus.Ended;
                revalidation.CertCheckedByUserId = this.accessContext.UserId;
                revalidation.CertCheckedDate = DateTime.Now;
                revalidation.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.BulkUpdate<Eumis.Domain.Contracts.ContractReportRevalidation>(
                revalidations,
                t => t.UncertifiedRevalidatedEuAmount,
                t => t.UncertifiedRevalidatedBgAmount,
                t => t.UncertifiedRevalidatedBfpTotalAmount,
                t => t.UncertifiedRevalidatedCrossAmount,
                t => t.UncertifiedRevalidatedSelfAmount,
                t => t.UncertifiedRevalidatedTotalAmount,
                t => t.CertifiedRevalidatedEuAmount,
                t => t.CertifiedRevalidatedBgAmount,
                t => t.CertifiedRevalidatedBfpTotalAmount,
                t => t.CertifiedRevalidatedCrossAmount,
                t => t.CertifiedRevalidatedSelfAmount,
                t => t.CertifiedRevalidatedTotalAmount,
                t => t.CertStatus,
                t => t.CertCheckedByUserId,
                t => t.CertCheckedDate,
                t => t.ModifyDate);

            this.unitOfWork.Save();
        }

        private void AssertIsCertDraftContractReportCorrection(ContractReportCorrectionCertStatus status)
        {
            if (status != ContractReportCorrectionCertStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportCorrection when not in cert 'Draft' status");
            }
        }

        private void AssertIsCertDraftContractReportRevalidation(ContractReportRevalidationCertStatus status)
        {
            if (status != ContractReportRevalidationCertStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportRevalidation when not in cert 'Draft' status");
            }
        }

        private void AssertIsCertDraftContractReportFinancialRevalidationCSD(ContractReportFinancialRevalidationCSDCertStatus status)
        {
            if (status != ContractReportFinancialRevalidationCSDCertStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportFinancialRevalidationCSD when not in cert 'Draft' status");
            }
        }

        private void AssertIsCertDraftContractReportFinancialCorrectionCSD(ContractReportFinancialCorrectionCSDCertStatus status)
        {
            if (status != ContractReportFinancialCorrectionCSDCertStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportFinancialCorrectionCSD when not in cert 'Draft' status");
            }
        }

        private void AssertIsCertDraftContractReportFinancialCSDBudgetItem(ContractReportFinancialCSDBudgetItemCertStatus status)
        {
            if (status != ContractReportFinancialCSDBudgetItemCertStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportFinancialCSDBudgetItem when not in cert 'Draft' status");
            }
        }

        private void AssertIsCertDraftContractReportAdvancePaymentAmount(ContractReportAdvancePaymentAmountCertStatus status)
        {
            if (status != ContractReportAdvancePaymentAmountCertStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportAdvancePaymentAmount when not in cert 'Draft' status");
            }
        }
    }
}
