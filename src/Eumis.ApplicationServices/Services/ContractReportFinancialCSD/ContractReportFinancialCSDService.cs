using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportFinancialCSDs.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Eumis.Rio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.ApplicationServices.Services.ContractReportFinancialCSD
{
    public class ContractReportFinancialCSDService : IContractReportFinancialCSDService
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractProcurementsRepository contractProcurementsRepository;
        private IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository;
        private IContractReportPaymentChecksRepository contractReportPaymentChecksRepository;
        private IContractReportFinancialCSDsRepository contractReportFinancialCSDsRepository;
        private IContractReportFinancialsRepository contractReportFinancialsRepository;

        public ContractReportFinancialCSDService(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractProcurementsRepository contractProcurementsRepository,
            IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository,
            IContractReportPaymentChecksRepository contractReportPaymentChecksRepository,
            IContractReportFinancialCSDsRepository contractReportFinancialCSDsRepository,
            IContractReportFinancialsRepository contractReportFinancialsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractProcurementsRepository = contractProcurementsRepository;
            this.contractReportFinancialCSDBudgetItemsRepository = contractReportFinancialCSDBudgetItemsRepository;
            this.contractReportPaymentChecksRepository = contractReportPaymentChecksRepository;
            this.contractReportFinancialCSDsRepository = contractReportFinancialCSDsRepository;
            this.contractReportFinancialsRepository = contractReportFinancialsRepository;
        }

        public void CreateContractReportFinancialCSDAndBudgetItems(ContractReportFinancial finance)
        {
            var contract = this.contractsRepository.Find(finance.ContractId);

            var budgetItemsLevel3IdsEuPercents = contract.ContractBudgetLevel3Amounts
                .ToDictionary(g => g.Gid.ToString(), g =>
                {
                    decimal percent = BfpCalculator.GetBgPercent(g.CurrentEuAmount, g.CurrentBgAmount);
                    return new Tuple<int, decimal>(g.ContractBudgetLevel3AmountId, percent);
                });

            var partners = contract.ContractPartners
                .ToDictionary(g => g.Gid, g => new Tuple<string, UinType, string>(
                    g.Uin,
                    g.UinType,
                    g.Name));

            var contractors = contract.ContractContractors
                .ToDictionary(g => g.Gid, g => new Tuple<string, UinType, string>(
                    g.Uin,
                    g.UinType,
                    g.Name));

            var costSupportingDocumentCollection = finance.GetDocument().CostSupportingDocuments.CostSupportingDocumentCollection.Where(csd => csd.IsLocked == false);
            IList<Eumis.Domain.Contracts.ContractReportFinancialCSD> newContractReportFinancialCSDs = new List<Eumis.Domain.Contracts.ContractReportFinancialCSD>();
            IList<Eumis.Domain.Contracts.ContractReportFinancialCSDFile> newContractReportFinancialCSDFiles = new List<Eumis.Domain.Contracts.ContractReportFinancialCSDFile>();
            IList<Eumis.Domain.Contracts.ContractReportFinancialCSDBudgetItem> newContractReportFinancialCSDBudgetItems = new List<Eumis.Domain.Contracts.ContractReportFinancialCSDBudgetItem>();

            CostSupportingDocumentCompanyType? companyType = null;
            Guid? companyGid = null;
            string companyUin = null;
            UinType? companyUinType = null;
            string companyName = null;

            Guid? contractContractorGid = null;
            string contractContractorName = null;

            foreach (var costSupportingDocument in costSupportingDocumentCollection)
            {
                companyGid = null;
                companyUin = null;
                companyUinType = null;
                companyName = null;
                contractContractorGid = null;
                contractContractorName = null;

                if (costSupportingDocument.CompanyType == CompanyTypeNomenclature.Beneficiary)
                {
                    companyType = CostSupportingDocumentCompanyType.Beneficiary;
                    companyGid = Guid.Parse(costSupportingDocument.Beneficiary.Id);
                    companyUin = contract.CompanyUin;
                    companyUinType = contract.CompanyUinType;
                    companyName = contract.CompanyName;
                }
                else if (costSupportingDocument.CompanyType == CompanyTypeNomenclature.Partner)
                {
                    companyType = CostSupportingDocumentCompanyType.Partner;
                    var pGid = Guid.Parse(costSupportingDocument.Partner.Id);
                    var partner = partners[pGid];

                    companyGid = pGid;
                    companyUin = partner.Item1;
                    companyUinType = partner.Item2;
                    companyName = partner.Item3;
                }
                else if (costSupportingDocument.CompanyType == CompanyTypeNomenclature.Contractor)
                {
                    companyType = CostSupportingDocumentCompanyType.Contractor;
                    var cGid = Guid.Parse(costSupportingDocument.Contractor.Id);
                    var contractor = contractors[cGid];

                    companyGid = cGid;
                    companyUin = contractor.Item1;
                    companyUinType = contractor.Item2;
                    companyName = contractor.Item3;

                    // this is the ContractContract Gid and Name when the CompanyType is Contractor
                    var contractContractor = costSupportingDocument.ContractContractor;
                    contractContractorGid = contractContractor != null ?
                        (string.IsNullOrEmpty(contractContractor.Id) ? (Guid?)null : Guid.Parse(contractContractor.Id)) :
                        (Guid?)null;
                    contractContractorName = contractContractor != null ?
                        (string.IsNullOrEmpty(contractContractor.Name) ? null : contractContractor.Name) :
                        null;
                }

                var newContractReportFinancialCSD = new Eumis.Domain.Contracts.ContractReportFinancialCSD(
                    finance.ContractReportFinancialId,
                    finance.ContractReportId,
                    finance.ContractId,
                    costSupportingDocument.GetEnum<Rio.CostSupportingDocument, CostSupportingDocumentType>(t => t.CostSupportingDocumentType.Value).Value,
                    costSupportingDocument.CostSupportingDocumentDescription,
                    costSupportingDocument.Number,
                    costSupportingDocument.Date.Value,
                    costSupportingDocument.PaymentDate.Value,
                    companyType.Value,
                    companyGid.Value,
                    companyUin,
                    companyUinType.Value,
                    companyName,
                    contractContractorGid,
                    contractContractorName);

                newContractReportFinancialCSDs.Add(newContractReportFinancialCSD);

                var documentCollection = costSupportingDocument.AttachedDocumentCollection
                    .Where(ad => !string.IsNullOrWhiteSpace(ad.AttachedDocumentContent.BlobContentId));
                foreach (var costSupportingDocumentFile in documentCollection)
                {
                    var newContractReportFinancialCSDFile = new Eumis.Domain.Contracts.ContractReportFinancialCSDFile()
                    {
                        ContractReportFinancialCSDId = newContractReportFinancialCSD.ContractReportFinancialCSDId,
                        ContractReportFinancialId = finance.ContractReportFinancialId,
                        BlobKey = new Guid(costSupportingDocumentFile.AttachedDocumentContent.BlobContentId),
                        Name = costSupportingDocumentFile.AttachedDocumentContent.FileName,
                        Description = costSupportingDocumentFile.Description,
                    };

                    newContractReportFinancialCSDFiles.Add(newContractReportFinancialCSDFile);
                }

                foreach (var costSupportingDocumentBudgetItem in costSupportingDocument.FinanceReportBudgetItemDataCollection)
                {
                    var level3IdEuPercent = budgetItemsLevel3IdsEuPercents[costSupportingDocumentBudgetItem.BudgetDetail.Id];
                    var bgAmount = BfpCalculator.GetBgAmount(costSupportingDocumentBudgetItem.GrandAmount, level3IdEuPercent.Item2);
                    var euAmount = BfpCalculator.GetEuAmount(costSupportingDocumentBudgetItem.GrandAmount, level3IdEuPercent.Item2);

                    var newContractReportFinancialCSDBudgetItem = new Eumis.Domain.Contracts.ContractReportFinancialCSDBudgetItem(
                        newContractReportFinancialCSD.ContractReportFinancialCSDId,
                        finance.ContractReportFinancialId,
                        finance.ContractReportId,
                        finance.ContractId,
                        level3IdEuPercent.Item1,
                        Guid.Parse(costSupportingDocumentBudgetItem.BudgetDetail.Id),
                        costSupportingDocumentBudgetItem.BudgetDetail.Name,
                        costSupportingDocumentBudgetItem.ContractActivity != null ?
                            (string.IsNullOrEmpty(costSupportingDocumentBudgetItem.ContractActivity.Id) ? (Guid?)null : Guid.Parse(costSupportingDocumentBudgetItem.ContractActivity.Id)) :
                            (Guid?)null,
                        costSupportingDocumentBudgetItem.ContractActivity != null ?
                            (string.IsNullOrEmpty(costSupportingDocumentBudgetItem.ContractActivity.Name) ? null : costSupportingDocumentBudgetItem.ContractActivity.Name) :
                            null,
                        0,
                        0,
                        costSupportingDocumentBudgetItem.GrandAmount,
                        costSupportingDocumentBudgetItem.SelfAmount,
                        costSupportingDocumentBudgetItem.GetEnum<Rio.FinanceReportBudgetItemData, YesNoNonApplicable>(t => t.CrossFinancing.Value).Value,
                        costSupportingDocumentBudgetItem.IsVatAmount,
                        costSupportingDocumentBudgetItem.TotalAmount,
                        costSupportingDocumentBudgetItem.UnitDefinition,
                        string.IsNullOrEmpty(costSupportingDocumentBudgetItem.ProducedUnitsCount) ? (int?)null : int.Parse(costSupportingDocumentBudgetItem.ProducedUnitsCount),
                        costSupportingDocumentBudgetItem.UnitCost,
                        costSupportingDocumentBudgetItem.GetEnum<Rio.FinanceReportBudgetItemData, YesNoNonApplicable>(t => t.InsideEU.Value).Value,
                        costSupportingDocumentBudgetItem.GetEnum<Rio.FinanceReportBudgetItemData, YesNoNonApplicable>(t => t.OutsideEU.Value).Value,
                        costSupportingDocumentBudgetItem.GetEnum<Rio.FinanceReportBudgetItemData, YesNoNonApplicable>(t => t.AdvancePayment.Value).Value,
                        costSupportingDocumentBudgetItem.GetEnum<Rio.FinanceReportBudgetItemData, YesNoNonApplicable>(t => t.ContributionNature.Value).Value);

                    newContractReportFinancialCSDBudgetItems.Add(newContractReportFinancialCSDBudgetItem);
                }
            }

            this.unitOfWork.BulkInsert<Eumis.Domain.Contracts.ContractReportFinancialCSD>(newContractReportFinancialCSDs);

            this.unitOfWork.BulkInsert<Eumis.Domain.Contracts.ContractReportFinancialCSDFile>(newContractReportFinancialCSDFiles);

            this.unitOfWork.BulkInsert<Eumis.Domain.Contracts.ContractReportFinancialCSDBudgetItem>(newContractReportFinancialCSDBudgetItems);
        }

        public void DeleteContractReportFinancialCSDAndBudgetItemsInDraft(ContractReportFinancial finance)
        {
            var contractReportFinancialCSDIds = this.contractReportFinancialCSDsRepository.GetContractReportFinancialCSDsInDraft(finance.ContractReportFinancialId);

            var endedContractReportFinancialCSDIds = this.GetContractReportFinancialEndedCSDs(finance);
            contractReportFinancialCSDIds.ToList().AddRange(endedContractReportFinancialCSDIds);

            if (contractReportFinancialCSDIds.Any())
            {
                this.unitOfWork.BulkDelete<Eumis.Domain.Contracts.ContractReportFinancialCSDBudgetItem>(p => contractReportFinancialCSDIds.Contains(p.ContractReportFinancialCSDId));

                this.unitOfWork.BulkDelete<Eumis.Domain.Contracts.ContractReportFinancialCSDFile>(p => contractReportFinancialCSDIds.Contains(p.ContractReportFinancialCSDId));

                this.unitOfWork.BulkDelete<Eumis.Domain.Contracts.ContractReportFinancialCSD>(p => contractReportFinancialCSDIds.Contains(p.ContractReportFinancialCSDId));
            }
        }

        public ContractReportFinancialCSDBudgetItem UpdateContractReportFinancialCSDBudgetItem(
            int contractReportFinancialCSDBudgetItemId,
            byte[] version,
            bool? costSupportingDocumentApproved,
            string notes,
            decimal euAmount,
            decimal bgAmount,
            decimal? unapprovedEuAmount,
            decimal? unapprovedBgAmount,
            decimal? unapprovedBfpTotalAmount,
            decimal? unapprovedSelfAmount,
            decimal? unapprovedTotalAmount,
            decimal? unapprovedByCorrectionEuAmount,
            decimal? unapprovedByCorrectionBgAmount,
            decimal? unapprovedByCorrectionBfpTotalAmount,
            decimal? unapprovedByCorrectionSelfAmount,
            decimal? unapprovedByCorrectionTotalAmount,
            decimal? approvedEuAmount,
            decimal? approvedBgAmount,
            decimal? approvedBfpTotalAmount,
            decimal? approvedSelfAmount,
            decimal? approvedTotalAmount,
            CorrectionType? correctionType,
            int? financialCorrectionId,
            int? irregularityId)
        {
            var contractReportFinancialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.FindForUpdate(contractReportFinancialCSDBudgetItemId, version);

            var contractReport = this.contractReportsRepository.Find(contractReportFinancialCSDBudgetItem.ContractReportId);

            if (contractReport.Status != ContractReportStatus.Unchecked)
            {
                throw new DomainException("Cannot edit a ContractReportFinancialCSD when the ContractReport is in status other than 'Unchecked'");
            }

            this.AssertIsDraftContractReportFinancialCSDBudgetItem(contractReportFinancialCSDBudgetItem.Status);

            contractReportFinancialCSDBudgetItem.UpdateAttributes(
                costSupportingDocumentApproved,
                notes,
                euAmount,
                bgAmount,
                unapprovedEuAmount,
                unapprovedBgAmount,
                unapprovedBfpTotalAmount,
                unapprovedSelfAmount,
                unapprovedTotalAmount,
                unapprovedByCorrectionEuAmount,
                unapprovedByCorrectionBgAmount,
                unapprovedByCorrectionBfpTotalAmount,
                unapprovedByCorrectionSelfAmount,
                unapprovedByCorrectionTotalAmount,
                approvedEuAmount,
                approvedBgAmount,
                approvedBfpTotalAmount,
                approvedSelfAmount,
                approvedTotalAmount,
                correctionType,
                financialCorrectionId,
                irregularityId);

            this.unitOfWork.Save();

            return contractReportFinancialCSDBudgetItem;
        }

        public void ChangeContractReportFinancialCSDBudgetItemStatus(
            int contractReportFinancialCSDBudgetItemId,
            byte[] version,
            ContractReportFinancialCSDBudgetItemStatus status)
        {
            var contractReportFinancialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.FindForUpdate(contractReportFinancialCSDBudgetItemId, version);

            var contractReport = this.contractReportsRepository.Find(contractReportFinancialCSDBudgetItem.ContractReportId);

            if (contractReport.Status != ContractReportStatus.Unchecked)
            {
                throw new DomainException("Cannot edit a ContractReportFinancialCSD when the ContractReport is in status other than 'Unchecked'");
            }

            contractReportFinancialCSDBudgetItem.Status = status;
            contractReportFinancialCSDBudgetItem.ModifyDate = DateTime.Now;

            if (status == ContractReportFinancialCSDBudgetItemStatus.Ended)
            {
                contractReportFinancialCSDBudgetItem.CheckedByUserId = this.accessContext.UserId;
                contractReportFinancialCSDBudgetItem.CheckedDate = DateTime.Now;
            }
            else if (status == ContractReportFinancialCSDBudgetItemStatus.Draft)
            {
                contractReportFinancialCSDBudgetItem.CheckedByUserId = null;
                contractReportFinancialCSDBudgetItem.CheckedDate = null;
            }

            contractReportFinancialCSDBudgetItem.ModifyDate = DateTime.Now;

            var contractReportFinancialCSD = this.contractReportFinancialCSDsRepository.Find(contractReportFinancialCSDBudgetItem.ContractReportFinancialCSDId);

            var areAllBudgetItemsSameStatus = this.AreAllBudgetItemsSameStatus(contractReportFinancialCSD.ContractReportFinancialCSDId, contractReportFinancialCSD.ContractReportId, status);

            if (areAllBudgetItemsSameStatus)
            {
                var contractReportFinancial = this.contractReportFinancialsRepository.FindWithoutIncludes(contractReportFinancialCSD.ContractReportFinancialId);

                contractReportFinancial.ChangeCSDPortalAccessibility(
                    status,
                    contractReportFinancialCSDBudgetItem.BudgetDetailGid,
                    contractReportFinancialCSDBudgetItem.ContractActivityGid,
                    contractReportFinancialCSDBudgetItem.TotalAmount,
                    contractReportFinancialCSD.Date,
                    contractReportFinancialCSD.Number,
                    contractReportFinancialCSD.CompanyGid);
            }

            this.unitOfWork.Save();
        }

        private bool AreAllBudgetItemsSameStatus(int contractReportFinancialCSDId, int contractReportId, ContractReportFinancialCSDBudgetItemStatus status)
        {
            return this.contractReportFinancialCSDBudgetItemsRepository.FindAll(contractReportId)
                .Where(bi => bi.ContractReportFinancialCSDId == contractReportFinancialCSDId)
                .All(bi => bi.Status == status);
        }

        public IList<string> CanChangeContractReportFinancialCSDBudgetItemStatusToEnded(int contractReportFinancialCSDBudgetItemId)
        {
            var errors = new List<string>();

            var contractReportFinancialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(contractReportFinancialCSDBudgetItemId);

            if (!contractReportFinancialCSDBudgetItem.CostSupportingDocumentApproved.HasValue)
            {
                errors.Add("Полето 'Съгласие' трябва да е попълнено");
            }

            if (!contractReportFinancialCSDBudgetItem.UnapprovedBgAmount.HasValue ||
                !contractReportFinancialCSDBudgetItem.UnapprovedEuAmount.HasValue ||
                !contractReportFinancialCSDBudgetItem.UnapprovedBfpTotalAmount.HasValue ||
                !contractReportFinancialCSDBudgetItem.UnapprovedSelfAmount.HasValue ||
                !contractReportFinancialCSDBudgetItem.UnapprovedTotalAmount.HasValue)
            {
                errors.Add("Всички полета от секция 'Неверифицирана сума на разходооправдателния документ за конкретния бюджетен ред и дейност' трябва да са попълнени");
            }

            if (!contractReportFinancialCSDBudgetItem.UnapprovedByCorrectionBgAmount.HasValue ||
                !contractReportFinancialCSDBudgetItem.UnapprovedByCorrectionEuAmount.HasValue ||
                !contractReportFinancialCSDBudgetItem.UnapprovedByCorrectionBfpTotalAmount.HasValue ||
                !contractReportFinancialCSDBudgetItem.UnapprovedByCorrectionSelfAmount.HasValue ||
                !contractReportFinancialCSDBudgetItem.UnapprovedByCorrectionTotalAmount.HasValue)
            {
                errors.Add("Всички полета от секция 'Неверифицирана сума на разходооправдателен документ по наложена финансова корекция за конкретния бюджетен ред и дейност' трябва да са попълнени");
            }

            return errors;
        }

        public IList<string> CanChangeContractReportFinancialCSDBudgetItemStatusToDraft(int contractReportFinancialCSDBudgetItemId)
        {
            var errors = new List<string>();

            var contractReportFinancialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(contractReportFinancialCSDBudgetItemId);

            if (this.contractReportPaymentChecksRepository.FindAll(contractReportFinancialCSDBudgetItem.ContractReportId).Where(t => t.Status != ContractReportPaymentCheckStatus.Archived).Any())
            {
                errors.Add("Всички верифицирани ИП трябва да бъдат със статус 'Архивирано', за да можете да промените статуса на разходооправдателния документ на 'Чернова'");
            }

            return errors;
        }

        public void TechCheckContractReportFinancialCSDBudgetItem(int contractReportFinancialCSDBudgetItemId, byte[] version)
        {
            var contractReportFinancialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.FindForUpdate(contractReportFinancialCSDBudgetItemId, version);

            var contractReport = this.contractReportsRepository.Find(contractReportFinancialCSDBudgetItem.ContractReportId);

            if (contractReport.Status != ContractReportStatus.Unchecked)
            {
                throw new DomainException("Cannot edit a ContractReportFinancialCSD when the ContractReport is in status other than 'Unchecked'");
            }

            this.AssertIsDraftContractReportFinancialCSDBudgetItem(contractReportFinancialCSDBudgetItem.Status);

            contractReportFinancialCSDBudgetItem.TechCheckedByUserId = this.accessContext.UserId;
            contractReportFinancialCSDBudgetItem.TechCheckedDate = DateTime.Now;
            contractReportFinancialCSDBudgetItem.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();
        }

        private void AssertIsDraftContractReportFinancialCSDBudgetItem(ContractReportFinancialCSDBudgetItemStatus status)
        {
            if (status != ContractReportFinancialCSDBudgetItemStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportFinancialCSDBudgetItem when not in 'Draft' status");
            }
        }

        public void UpdateContractReportFinancialEndedCSDs(int oldContractReportFinancialId, int newContractReportFinancialId, int contractReportId)
        {
            var contractReportFinancialCSDBudgetItems = this.contractReportFinancialCSDBudgetItemsRepository
                .FindAll(contractReportId)
                .Where(crfbi => crfbi.ContractReportFinancialId == oldContractReportFinancialId);

            var contractReportFinancialCSDFiles = this.contractReportFinancialCSDsRepository.GetContractReportFinancialCSDFiles(oldContractReportFinancialId);

            var contractReportFinancialCSDs = this.contractReportFinancialCSDsRepository.FindAll(oldContractReportFinancialId);

            foreach (var budgetitem in contractReportFinancialCSDBudgetItems)
            {
                budgetitem.ContractReportFinancialId = newContractReportFinancialId;
                budgetitem.ModifyDate = DateTime.Now;
            }

            foreach (var csdFile in contractReportFinancialCSDFiles)
            {
                csdFile.ContractReportFinancialId = newContractReportFinancialId;
            }

            foreach (var csd in contractReportFinancialCSDs)
            {
                csd.ContractReportFinancialId = newContractReportFinancialId;
                csd.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.Save();
        }

        private IList<int> GetContractReportFinancialEndedCSDs(ContractReportFinancial financialReport)
        {
            var csdsToDelete = new List<int>();

            var contractReportFinancialCSDs = this.contractReportFinancialCSDsRepository.FindAll(financialReport.ContractReportFinancialId);

            foreach (var csd in contractReportFinancialCSDs)
            {
                if (this.AreAllBudgetItemsSameStatus(csd.ContractReportFinancialCSDId, csd.ContractReportId, ContractReportFinancialCSDBudgetItemStatus.Ended))
                {
                    List<Tuple<string, string, decimal>> budgetItemsData = this.contractReportFinancialCSDBudgetItemsRepository
                        .FindAll(csd.ContractReportId)
                        .Where(bi => bi.ContractReportFinancialCSDId == csd.ContractReportFinancialCSDId)
                        .Select(bi => new Tuple<string, string, decimal>(
                            item1: bi.BudgetDetailGid.ToString(),
                            item2: bi.ContractActivityGid?.ToString(),
                            item3: bi.TotalAmount))
                        .Distinct()
                        .ToList();

                    var isFinancialCSDToDelete = financialReport.IsFinancialCSDToDelete(csd.Date, csd.Number, csd.CompanyGid, budgetItemsData);

                    if (isFinancialCSDToDelete)
                    {
                        csdsToDelete.Add(csd.ContractReportFinancialCSDId);
                    }
                }
            }

            return csdsToDelete;
        }
    }
}
