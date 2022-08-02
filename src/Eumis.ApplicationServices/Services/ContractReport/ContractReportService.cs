using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.Services.Contract;
using Eumis.ApplicationServices.Services.ContractReportAdvanceNVPaymentAmount;
using Eumis.ApplicationServices.Services.ContractReportAdvancePaymentAmount;
using Eumis.ApplicationServices.Services.ContractReportFinancialCSD;
using Eumis.ApplicationServices.Services.ContractReportIndicator;
using Eumis.ApplicationServices.Services.ContractReportMicro;
using Eumis.ApplicationServices.Services.ContractReportTechnicalMember;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportAdvanceNVPaymentAmounts.Repositories;
using Eumis.Data.ContractReportAdvancePaymentAmounts.Repositories;
using Eumis.Data.ContractReportFinancialCSDs.Repositories;
using Eumis.Data.ContractReportIndicators.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ContractReportMicros;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Users.ProgrammePermissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Eumis.ApplicationServices.Services.ContractReport
{
    public class ContractReportService : IContractReportService
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private IDocumentRestApiCommunicator documentRestApiCommunicator;

        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportFinancialsRepository contractReportFinancialsRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IContractReportTechnicalsRepository contractReportTechnicalsRepository;
        private IContractReportMicrosRepository contractReportMicrosRepository;
        private IContractReportFinancialChecksRepository contractReportFinancialChecksRepository;
        private IContractReportPaymentChecksRepository contractReportPaymentChecksRepository;
        private IContractReportTechnicalChecksRepository contractReportTechnicalChecksRepository;
        private IContractReportMicroChecksRepository contractReportMicroChecksRepository;
        private IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository;
        private IContractReportIndicatorsRepository contractReportIndicatorsRepository;
        private IContractReportAdvancePaymentAmountsRepository contractReportAdvancePaymentAmountsRepository;
        private IContractReportAdvanceNVPaymentAmountsRepository contractReportAdvanceNVPaymentAmountsRepository;
        private IContractReportFinancialCSDsRepository contractReportFinancialCSDsRepository;

        private IContractVersionsRepository contractVersionsRepository;
        private IContractProcurementsRepository contractProcurementsRepository;
        private IProceduresRepository proceduresRepository;

        private IContractService contractService;
        private IContractReportMicroService contractReportMicroService;
        private IContractReportFinancialCSDService contractReportFinancialCSDService;
        private IContractReportIndicatorService contractReportIndicatorService;
        private IContractReportAdvancePaymentAmountService contractReportAdvancePaymentAmountService;
        private IContractReportAdvanceNVPaymentAmountService contractReportAdvanceNVPaymentAmountService;
        private IContractReportTechnicalMemberService contractReportTechnicalMemberService;

        public ContractReportService(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            IDocumentRestApiCommunicator documentRestApiCommunicator,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportFinancialsRepository contractReportFinancialsRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IContractReportTechnicalsRepository contractReportTechnicalsRepository,
            IContractReportMicrosRepository contractReportMicrosRepository,
            IContractReportFinancialChecksRepository contractReportFinancialChecksRepository,
            IContractReportPaymentChecksRepository contractReportPaymentChecksRepository,
            IContractReportTechnicalChecksRepository contractReportTechnicalChecksRepository,
            IContractReportMicroChecksRepository contractReportMicroChecksRepository,
            IContractVersionsRepository contractVersionsRepository,
            IContractProcurementsRepository contractProcurementsRepository,
            IProceduresRepository proceduresRepository,
            IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository,
            IContractReportIndicatorsRepository contractReportIndicatorsRepository,
            IContractReportAdvancePaymentAmountsRepository contractReportAdvancePaymentAmountsRepository,
            IContractReportAdvanceNVPaymentAmountsRepository contractReportAdvanceNVPaymentAmountsRepository,
            IContractReportFinancialCSDsRepository contractReportFinancialCSDsRepository,
            IContractService contractService,
            IContractReportMicroService contractReportMicroService,
            IContractReportFinancialCSDService contractReportFinancialCSDService,
            IContractReportIndicatorService contractReportIndicatorService,
            IContractReportAdvancePaymentAmountService contractReportAdvancePaymentAmountService,
            IContractReportAdvanceNVPaymentAmountService contractReportAdvanceNVPaymentAmountService,
            IContractReportTechnicalMemberService contractReportTechnicalMemberService)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.documentRestApiCommunicator = documentRestApiCommunicator;

            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportFinancialsRepository = contractReportFinancialsRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.contractReportTechnicalsRepository = contractReportTechnicalsRepository;
            this.contractReportMicrosRepository = contractReportMicrosRepository;

            this.contractReportFinancialChecksRepository = contractReportFinancialChecksRepository;
            this.contractReportPaymentChecksRepository = contractReportPaymentChecksRepository;
            this.contractReportTechnicalChecksRepository = contractReportTechnicalChecksRepository;
            this.contractReportMicroChecksRepository = contractReportMicroChecksRepository;
            this.contractReportFinancialCSDBudgetItemsRepository = contractReportFinancialCSDBudgetItemsRepository;
            this.contractReportIndicatorsRepository = contractReportIndicatorsRepository;
            this.contractReportAdvancePaymentAmountsRepository = contractReportAdvancePaymentAmountsRepository;
            this.contractReportAdvanceNVPaymentAmountsRepository = contractReportAdvanceNVPaymentAmountsRepository;
            this.contractReportFinancialCSDsRepository = contractReportFinancialCSDsRepository;

            this.contractVersionsRepository = contractVersionsRepository;
            this.contractProcurementsRepository = contractProcurementsRepository;
            this.proceduresRepository = proceduresRepository;

            this.contractService = contractService;
            this.contractReportMicroService = contractReportMicroService;
            this.contractReportFinancialCSDService = contractReportFinancialCSDService;
            this.contractReportIndicatorService = contractReportIndicatorService;
            this.contractReportAdvancePaymentAmountService = contractReportAdvancePaymentAmountService;
            this.contractReportAdvanceNVPaymentAmountService = contractReportAdvanceNVPaymentAmountService;
            this.contractReportTechnicalMemberService = contractReportTechnicalMemberService;
        }

        public string GetContractReportFinancialXmlForEdit(ContractReportFinancial finance)
        {
            var activeContractProcurement = this.contractProcurementsRepository.GetActiveProcurementOrDefault(finance.ContractId);

            var lastFinanceReport = this.contractReportFinancialsRepository.GetLastContractReportFinancial(finance.ContractId);
            string lastFinanceReportXml = lastFinanceReport != null ? lastFinanceReport.Xml : null;

            var advanceVerificationPayment = this.contractReportPaymentsRepository.GetLastAdvanceVerificationContractReportPayment(finance.ContractId);
            string advanceVerificationPaymentXml = advanceVerificationPayment != null ? advanceVerificationPayment.Xml : null;

            var approvedCumulativeCSDBudgetAmounts = this.contractReportFinancialCSDBudgetItemsRepository.GetPortalContractReportFinancialCSDBudgetItems(finance.ContractId);

            return this.documentRestApiCommunicator.LoadContractReportFinancial(
                finance.Xml,
                activeContractProcurement != null ? activeContractProcurement.Xml : null,
                lastFinanceReportXml,
                advanceVerificationPaymentXml,
                approvedCumulativeCSDBudgetAmounts);
        }

        public async Task<string> GetContractReportFinancialXmlForEditAsync(ContractReportFinancial finance, CancellationToken ct)
        {
            var activeContractProcurement = await this.contractProcurementsRepository.GetActiveProcurementOrDefaultAsync(finance.ContractId, ct);

            var lastFinanceReport = await this.contractReportFinancialsRepository.GetLastContractReportFinancialAsync(finance.ContractId, ct);
            string lastFinanceReportXml = lastFinanceReport != null ? lastFinanceReport.Xml : null;

            var advanceVerificationPayment = await this.contractReportPaymentsRepository.GetLastAdvanceVerificationContractReportPaymentAsync(finance.ContractId, ct);
            string advanceVerificationPaymentXml = advanceVerificationPayment != null ? advanceVerificationPayment.Xml : null;

            var approvedCumulativeCSDBudgetAmounts = await this.contractReportFinancialCSDBudgetItemsRepository.GetPortalContractReportFinancialCSDBudgetItemsAsync(
                finance.ContractId,
                ct);

            var result = await this.documentRestApiCommunicator.LoadContractReportFinancialAsync(
                finance.Xml,
                activeContractProcurement != null ? activeContractProcurement.Xml : null,
                lastFinanceReportXml,
                advanceVerificationPaymentXml,
                approvedCumulativeCSDBudgetAmounts);

            return result;
        }

        public string GetContractReportTechnicalXmlForEdit(ContractReportTechnical tecnical)
        {
            var activeContractProcurement = this.contractProcurementsRepository.GetActiveProcurementOrDefault(tecnical.ContractId);
            var lastTechnicalReport = this.contractReportTechnicalsRepository.GetLastContractReportTechnical(tecnical.ContractId);
            string lastTechnicalReportXml = lastTechnicalReport != null ? lastTechnicalReport.Xml : null;

            var lastTechnicalReportIndicators = lastTechnicalReport != null ?
                this.contractReportIndicatorsRepository.GetPortalContractReportIndicators(lastTechnicalReport.ContractReportTechnicalId) :
                null;

            return this.documentRestApiCommunicator.LoadContractReportTechnical(
                tecnical.Xml,
                activeContractProcurement != null ? activeContractProcurement.Xml : null,
                lastTechnicalReportXml,
                lastTechnicalReportIndicators);
        }

        public async Task<string> GetContractReportTechnicalXmlForEditAsync(ContractReportTechnical tecnical, CancellationToken ct)
        {
            var activeContractProcurement = await this.contractProcurementsRepository.GetActiveProcurementOrDefaultAsync(tecnical.ContractId, ct);
            var lastTechnicalReport = await this.contractReportTechnicalsRepository.GetLastContractReportTechnicalAsync(tecnical.ContractId, ct);
            string lastTechnicalReportXml = lastTechnicalReport != null ? lastTechnicalReport.Xml : null;

            var lastTechnicalReportIndicators = lastTechnicalReport != null ?
                await this.contractReportIndicatorsRepository.GetPortalContractReportIndicatorsAsync(lastTechnicalReport.ContractReportTechnicalId, ct) :
                null;

            var result = await this.documentRestApiCommunicator.LoadContractReportTechnicalAsync(
                tecnical.Xml,
                activeContractProcurement != null ? activeContractProcurement.Xml : null,
                lastTechnicalReportXml,
                lastTechnicalReportIndicators);

            return result;
        }

        public IList<string> CanCreateContractReport(int contractId)
        {
            var errors = new List<string>();

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ContractReportPermissions.CanWrite);
            var canCreate = programmeIds.Contains(this.contractsRepository.GetProgrammeId(contractId));
            if (!canCreate)
            {
                errors.Add("Нямате права за създаване на пакет отчетни документи към съответната програма.");
            }

            var canCreateContractReport = this.contractReportsRepository.CanCreate(contractId);
            if (!canCreateContractReport)
            {
                errors.Add("Вече съществува пакет със статус 'Чернова', 'Въведен' или 'Приключен', свързан с избрания договор" +
                    "(ако не е видим в списъка, то той е създаден от бенефициента).");
            }
            else
            {
                if (this.contractVersionsRepository.HasContractBlockingVersionsInProgress(contractId))
                {
                    errors.Add("Договорът има неприключени Изменение или Промяна.");
                }
            }

            return errors;
        }

        public IList<string> CanCreateContractReportFinancial(int contractReportId)
        {
            var errors = new List<string>();

            var contractReport = this.contractReportsRepository.Find(contractReportId);

            if (contractReport.ReportType != ContractReportType.Financial && contractReport.ReportType != ContractReportType.PaymentFinancial && contractReport.ReportType != ContractReportType.PaymentTechnicalFinancial)
            {
                errors.Add("Типът на пакета не е 'Искане за плащане и финансов отчет' или 'Искане за плащане, технически отчет, финансов отчет'.");
            }
            else if (this.contractReportFinancialsRepository.FindAll(contractReportId).Any())
            {
                errors.Add("Вече съществува финансов отчет към този пакет отчетни документи.");
            }

            return errors;
        }

        public async Task<IList<string>> CanCreateContractReportFinancialAsync(int contractReportId, CancellationToken ct)
        {
            var errors = new List<string>();

            var contractReport = await this.contractReportsRepository.FindWithoutIncludesAsync(contractReportId, ct);

            if (contractReport.ReportType != ContractReportType.PaymentFinancial && contractReport.ReportType != ContractReportType.PaymentTechnicalFinancial)
            {
                errors.Add("Типът на пакета не е 'Искане за плащане и финансов отчет' или 'Искане за плащане, технически отчет, финансов отчет'.");
            }
            else if (this.contractReportFinancialsRepository.FindAll(contractReportId).Any())
            {
                errors.Add("Вече съществува финансов отчет към този пакет отчетни документи.");
            }

            return errors;
        }

        public IList<string> CanCreateContractReportPayment(int contractReportId, ContractReportPaymentType type)
        {
            var errors = new List<string>();
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            if ((type == ContractReportPaymentType.Advance || type == ContractReportPaymentType.AdvanceVerification) && contractReport.ReportType != ContractReportType.AdvancePayment)
            {
                errors.Add("Типът на пакета не е 'Авансово искане за плащане'.");
            }
            else if ((type == ContractReportPaymentType.Intermediate || type == ContractReportPaymentType.Final) && contractReport.ReportType != ContractReportType.PaymentFinancial && contractReport.ReportType != ContractReportType.PaymentTechnicalFinancial)
            {
                errors.Add("Типът на пакета не е 'Искане за плащане и финансов отчет' или 'Искане за плащане, технически отчет, финансов отчет'.");
            }
            else if (type == ContractReportPaymentType.AdvanceVerification && !this.contractReportPaymentsRepository.CanCreateAdvanceVerificationPayment(contractReport.ContractId))
            {
                errors.Add("Вече съществува искане за плащане от тип 'Авансово, подлежащо на верификация съгласно чл. 131 от Регламент ЕС 1303/2013'" +
                    " към този договор в друг пакет отчетни документи със статус 'Приет'.");
            }
            else if (this.contractReportPaymentsRepository.FindAll(contractReportId).Any())
            {
                errors.Add("Вече съществува искане за плащане към този пакет отчетни документи.");
            }

            return errors;
        }

        public async Task<IList<string>> CanCreateContractReportPaymentAsync(int contractReportId, ContractReportPaymentType type, CancellationToken ct)
        {
            var errors = new List<string>();
            var contractReport = await this.contractReportsRepository.FindWithoutIncludesAsync(contractReportId, ct);

            if ((type == ContractReportPaymentType.Advance || type == ContractReportPaymentType.AdvanceVerification) && contractReport.ReportType != ContractReportType.AdvancePayment)
            {
                errors.Add("Типът на пакета не е 'Авансово искане за плащане'.");
            }
            else if ((type == ContractReportPaymentType.Intermediate || type == ContractReportPaymentType.Final) && contractReport.ReportType != ContractReportType.PaymentFinancial && contractReport.ReportType != ContractReportType.PaymentTechnicalFinancial)
            {
                errors.Add("Типът на пакета не е 'Искане за плащане и финансов отчет' или 'Искане за плащане, технически отчет, финансов отчет'.");
            }
            else if (type == ContractReportPaymentType.AdvanceVerification && !this.contractReportPaymentsRepository.CanCreateAdvanceVerificationPayment(contractReport.ContractId))
            {
                errors.Add("Вече съществува искане за плащане от тип 'Авансово, подлежащо на верификация съгласно чл. 131 от Регламент ЕС 1303/2013'" +
                    " към този договор в друг пакет отчетни документи със статус 'Приет'.");
            }
            else if ((await this.contractReportPaymentsRepository.FindAllAsync(contractReportId, ct)).Any())
            {
                errors.Add("Вече съществува искане за плащане към този пакет отчетни документи.");
            }

            return errors;
        }

        public IList<string> CanCreateContractReportTechnical(int contractReportId)
        {
            var errors = new List<string>();

            var contractReport = this.contractReportsRepository.Find(contractReportId);

            if (contractReport.ReportType != ContractReportType.Technical && contractReport.ReportType != ContractReportType.PaymentTechnicalFinancial)
            {
                errors.Add("Типът на пакета не е 'Технически отчет' или 'Искане за плащане, технически отчет, финансов отчет'.");
            }
            else if (this.contractReportTechnicalsRepository.FindAll(contractReportId).Any())
            {
                errors.Add("Вече съществува технически отчет към този пакет отчетни документи.");
            }

            return errors;
        }

        public async Task<IList<string>> CanCreateContractReportTechnicalAsync(int contractReportId, CancellationToken ct)
        {
            var errors = new List<string>();

            var contractReport = await this.contractReportsRepository.FindWithoutIncludesAsync(contractReportId, ct);

            if (contractReport.ReportType != ContractReportType.Technical && contractReport.ReportType != ContractReportType.PaymentTechnicalFinancial)
            {
                errors.Add("Типът на пакета не е 'Технически отчет' или 'Искане за плащане, технически отчет, финансов отчет'.");
            }
            else if (this.contractReportTechnicalsRepository.FindAll(contractReportId).Any())
            {
                errors.Add("Вече съществува технически отчет към този пакет отчетни документи.");
            }

            return errors;
        }

        public IList<string> CanCreateContractReportFinancialCheck(int contractReportId)
        {
            var errors = new List<string>();

            var actualFinancial = this.contractReportFinancialsRepository.GetActualContractReportFinancial(contractReportId);
            var financialChecks = this.contractReportFinancialChecksRepository.FindAll(contractReportId);

            if (actualFinancial == null)
            {
                errors.Add("Няма актуален финансов отчет.");
            }

            if (financialChecks.Where(t => t.Status == ContractReportFinancialCheckStatus.Draft).Any())
            {
                errors.Add("Вече съществува проверка на финансов отчет със статус 'Чернова'.");
            }

            return errors;
        }

        public IList<string> CanCreateContractReportPaymentCheck(int contractReportId)
        {
            var errors = new List<string>();

            var actualPayment = this.contractReportPaymentsRepository.GetActualContractReportPayment(contractReportId);
            var paymentChecks = this.contractReportPaymentChecksRepository.FindAll(contractReportId);

            if (actualPayment == null)
            {
                errors.Add("Няма актуално искане за плащане.");
            }

            if (paymentChecks.Where(t => t.Status == ContractReportPaymentCheckStatus.Draft).Any())
            {
                errors.Add("Вече съществува верифицирано искане за плащане със статус 'Чернова'.");
            }

            if (this.contractReportFinancialCSDBudgetItemsRepository.HasDraftContractReportFinancialCSDBudgetItem(contractReportId))
            {
                errors.Add("Всички разходооправдателни документи трябва да са със статус 'Приключен'.");
            }

            if (this.contractReportAdvancePaymentAmountsRepository.HasDraftContractReportAdvancePaymentAmounts(contractReportId))
            {
                errors.Add("Всички верифицирани авансови плащания трябва да са със статус 'Приключен'.");
            }

            if (this.contractReportAdvanceNVPaymentAmountsRepository.HasDraftContractReportAdvanceNVPaymentAmounts(contractReportId))
            {
                errors.Add("Всички авансови плащания трябва да са със статус 'Приключен'.");
            }

            return errors;
        }

        public IList<string> CanCreateContractReportTechnicalCheck(int contractReportId)
        {
            var errors = new List<string>();

            var actualTechnical = this.contractReportTechnicalsRepository.GetActualContractReportTechnical(contractReportId);
            var technicalChecks = this.contractReportTechnicalChecksRepository.FindAll(contractReportId);

            if (actualTechnical == null)
            {
                errors.Add("Няма актуален технически отчет.");
            }

            if (technicalChecks.Where(t => t.Status == ContractReportTechnicalCheckStatus.Draft).Any())
            {
                errors.Add("Вече съществува проверка на технически отчет със статус 'Чернова'.");
            }

            return errors;
        }

        private IList<string> CanCreateContractReport(Domain.Contracts.Contract contract)
        {
            var errors = new List<string>();
            var canCreateContractReport = this.contractReportsRepository.CanCreate(contract.ContractId);
            if (!canCreateContractReport)
            {
                errors.Add("Вече съществува пакет със статус 'Чернова' или 'Приключен', свързан с избрания договор" +
                    "(ако не е видим в списъка, то той е създаден от управляващия орган).");
            }
            else
            {
                if (this.contractVersionsRepository.HasContractBlockingVersionsInProgress(contract.ContractId))
                {
                    errors.Add("Договорът има неприключени Изменение или Промяна.");
                }
            }

            return errors;
        }

        private async Task<IList<string>> CanCreateContractReportAsync(Domain.Contracts.Contract contract, CancellationToken ct)
        {
            var errors = new List<string>();
            var canCreateContractReport = await this.contractReportsRepository.CanCreateAsync(contract.ContractId, ct);
            if (!canCreateContractReport)
            {
                errors.Add("Вече съществува пакет със статус 'Чернова' или 'Приключен', свързан с избрания договор" +
                    "(ако не е видим в списъка, то той е създаден от управляващия орган).");
            }
            else
            {
                if (await this.contractVersionsRepository.HasContractBlockingVersionsInProgressAsync(contract.ContractId, ct))
                {
                    errors.Add("Договорът има неприключени Изменение или Промяна.");
                }
            }

            return errors;
        }

        public async Task<IList<string>> CanCreateContractReportAsync(Guid contractGid, CancellationToken ct)
        {
            IList<string> errors;
            var contract = await this.contractsRepository.FindAsync(contractGid, ct);
            errors = this.CanCreateContractReport(contract);

            return errors;
        }

        public async Task<IList<string>> CanCopyContractReportAsync(Guid contractReportGid, CancellationToken ct)
        {
            var contractReport = await this.contractReportsRepository.FindAsync(contractReportGid, ct);
            var contract = await this.contractsRepository.FindWithoutIncludesAsync(contractReport.ContractId, ct);

            var canCreateErrors = await this.CanCreateContractReportAsync(contract, ct);

            return canCreateErrors;
        }

        public async Task<IList<string>> HasAdvanceVerificationPaymentAsync(Guid contractReportGid, CancellationToken ct)
        {
            var errors = new List<string>();

            var contractReport = await this.contractReportsRepository.FindAsync(contractReportGid, ct);

            if (contractReport.ReportType == ContractReportType.AdvancePayment)
            {
                if (await this.contractReportsRepository.HasAdvanceVerificationPaymentAsync(contractReport.ContractId, contractReport.ContractReportId, ct))
                {
                    errors.Add("Вече съществува искане за плащане от тип 'Авансово, подлежащо на верификация съгласно чл. 131 от Регламент ЕС 1303/2013'" +
                        " към този договор в друг пакет отчетни документи със статус 'Приет'.");
                }
            }

            return errors;
        }

        public async Task<IList<string>> CanEditSentContractReportAsync(Guid contractGid, CancellationToken ct)
        {
            var errors = new List<string>();
            var contract = await this.contractsRepository.FindAsync(contractGid, ct);
            if (!await this.contractReportsRepository.CanEditSentContractReportAsync(contract.ContractId, ct))
            {
                errors.Add("Не можете да редактирате пакет отчетни документи със статус 'Подаден'");
            }

            return errors;
        }

        public IList<string> CanChangeContractReportStatusToUnchecked(int contractReportId)
        {
            var errors = new List<string>();
            if (!this.contractReportsRepository.CanChangeContractReportStatusToUnchecked(contractReportId))
            {
                errors.Add("Можете да вкарате в проверка само поредни пакети отчетни документи");
            }

            return errors;
        }

        private IList<string> CanChangeContractReportType(Domain.Contracts.ContractReport contractReport, ContractReportType newContractReportType)
        {
            var errors = new List<string>();

            if (contractReport.ReportType == newContractReportType)
            {
                return errors;
            }

            switch (newContractReportType)
            {
                case ContractReportType.AdvancePayment:
                    if (this.contractReportsRepository.ContractReportHasTechnical(contractReport.ContractReportId))
                    {
                        errors.Add("За да промените вида на пакета отчетни документи е необходимо да премахнете документ 'Технически отчет'");
                    }

                    if (this.contractReportsRepository.ContractReportHasFinancial(contractReport.ContractReportId))
                    {
                        errors.Add("За да промените типа на пакета отчетни документи е необходимо да премахнете документ 'Финансов отчет'");
                    }

                    if (this.contractReportsRepository.ContractReportHasPayment(contractReport.ContractReportId))
                    {
                        errors.Add("За да промените типа на пакета отчетни документи е необходимо да премахнете документ 'Искане за плащане'");
                    }

                    break;
                case ContractReportType.Technical:

                    if (this.contractReportsRepository.ContractReportHasFinancial(contractReport.ContractReportId))
                    {
                        errors.Add("За да промените типа на пакета отчетни документи е необходимо да премахнете документ 'Финансов отчет'");
                    }

                    if (this.contractReportsRepository.ContractReportHasPayment(contractReport.ContractReportId) ||
                        this.contractReportsRepository.ContractReportHasAdvancePayment(contractReport.ContractReportId))
                    {
                        errors.Add("За да промените типа на пакета отчетни документи е необходимо да премахнете документ 'Искане за плащане'");
                    }

                    break;
                case ContractReportType.PaymentTechnicalFinancial:
                    if (this.contractReportsRepository.ContractReportHasAdvancePayment(contractReport.ContractReportId))
                    {
                        errors.Add("За да промените типа на пакета отчетни документи е необходимо да премахнете документ 'Авансово искане за плащане'");
                    }

                    break;
                case ContractReportType.PaymentFinancial:
                    if (this.contractReportsRepository.ContractReportHasAdvancePayment(contractReport.ContractReportId))
                    {
                        errors.Add("За да промените типа на пакета отчетни документи е необходимо да премахнете документ 'Авансово искане за плащане'");
                    }

                    if (this.contractReportsRepository.ContractReportHasTechnical(contractReport.ContractReportId))
                    {
                        errors.Add("За да промените типа на пакета отчетни документи е необходимо да премахнете документ 'Технически отчет'");
                    }

                    break;
                default:
                    throw new ArgumentException("Unsupported ContractReportType", nameof(newContractReportType));
            }

            return errors;
        }

        private async Task<IList<string>> CanChangeContractReportTypeAsync(Domain.Contracts.ContractReport contractReport, ContractReportType newContractReportType, CancellationToken ct)
        {
            var errors = new List<string>();

            if (contractReport.ReportType == newContractReportType)
            {
                return errors;
            }

            switch (newContractReportType)
            {
                case ContractReportType.AdvancePayment:
                    if (await this.contractReportsRepository.ContractReportHasTechnicalAsync(contractReport.ContractReportId, ct))
                    {
                        errors.Add("За да промените вида на пакета отчетни документи е необходимо да премахнете документ 'Технически отчет'");
                    }

                    if (await this.contractReportsRepository.ContractReportHasFinancialAsync(contractReport.ContractReportId, ct))
                    {
                        errors.Add("За да промените типа на пакета отчетни документи е необходимо да премахнете документ 'Финансов отчет'");
                    }

                    if (this.contractReportsRepository.ContractReportHasPayment(contractReport.ContractReportId))
                    {
                        errors.Add("За да промените типа на пакета отчетни документи е необходимо да премахнете документ 'Искане за плащане'");
                    }

                    break;
                case ContractReportType.Technical:

                    if (await this.contractReportsRepository.ContractReportHasFinancialAsync(contractReport.ContractReportId, ct))
                    {
                        errors.Add("За да промените типа на пакета отчетни документи е необходимо да премахнете документ 'Финансов отчет'");
                    }

                    if (await this.contractReportsRepository.ContractReportHasPaymentAsync(contractReport.ContractReportId, ct) ||
                        await this.contractReportsRepository.ContractReportHasAdvancePaymentAsync(contractReport.ContractReportId, ct))
                    {
                        errors.Add("За да промените типа на пакета отчетни документи е необходимо да премахнете документ 'Искане за плащане'");
                    }

                    break;
                case ContractReportType.PaymentTechnicalFinancial:
                    if (await this.contractReportsRepository.ContractReportHasAdvancePaymentAsync(contractReport.ContractReportId, ct))
                    {
                        errors.Add("За да промените типа на пакета отчетни документи е необходимо да премахнете документ 'Авансово искане за плащане'");
                    }

                    break;
                case ContractReportType.PaymentFinancial:
                    if (await this.contractReportsRepository.ContractReportHasAdvancePaymentAsync(contractReport.ContractReportId, ct))
                    {
                        errors.Add("За да промените типа на пакета отчетни документи е необходимо да премахнете документ 'Авансово искане за плащане'");
                    }

                    if (await this.contractReportsRepository.ContractReportHasTechnicalAsync(contractReport.ContractReportId, ct))
                    {
                        errors.Add("За да промените типа на пакета отчетни документи е необходимо да премахнете документ 'Технически отчет'");
                    }

                    break;
                default:
                    throw new ArgumentException("Unsupported ContractReportType", nameof(newContractReportType));
            }

            return errors;
        }

        public async Task<IList<string>> CanCreateContractReportFinancialAsync(Guid contractReportGid, CancellationToken ct)
        {
            var contractReport = await this.contractReportsRepository.FindAsync(contractReportGid, ct);

            return await this.CanCreateContractReportFinancialAsync(contractReport.ContractReportId, ct);
        }

        public IList<string> CanCreateContractReportPayment(Guid contractReportGid, ContractReportPaymentType type)
        {
            return this.CanCreateContractReportPayment(this.contractReportsRepository.Find(contractReportGid).ContractReportId, type);
        }

        public async Task<IList<string>> CanCreateContractReportTechnicalAsync(Guid contractReportGid, CancellationToken ct)
        {
            return await this.CanCreateContractReportTechnicalAsync((await this.contractReportsRepository.FindAsync(contractReportGid, ct)).ContractReportId, ct);
        }

        public async Task<IList<string>> CanChangeContractReportTypeAsync(Guid contractReportGid, ContractReportType newContractReportType, CancellationToken ct)
        {
            var contractReport = await this.contractReportsRepository.FindAsync(contractReportGid, ct);
            return await this.CanChangeContractReportTypeAsync(contractReport, newContractReportType, ct);
        }

        public IList<string> CanChangeContractReportType(int contractReporId, ContractReportType newContractReportType)
        {
            return this.CanChangeContractReportType(this.contractReportsRepository.Find(contractReporId), newContractReportType);
        }

        public Eumis.Domain.Contracts.ContractReport CreateContractReport(
            int contractId,
            Domain.Contracts.ContractReportType reportType,
            string otherRegistration,
            string storagePlace,
            DateTime? submitDate,
            DateTime? submitDeadline)
        {
            if (this.CanCreateContractReport(contractId).Count != 0)
            {
                throw new DomainException("Cannot create ContractReport");
            }

            var newContractReport = new Eumis.Domain.Contracts.ContractReport(
                contractId,
                reportType,
                Source.AdministrativeAuthority,
                this.contractReportsRepository.GetNextOrderNumber(contractId),
                otherRegistration,
                storagePlace,
                submitDate,
                submitDeadline);

            this.contractReportsRepository.Add(newContractReport);

            this.unitOfWork.Save();

            return newContractReport;
        }

        public async Task<Eumis.Domain.Contracts.ContractReport> CopyContractReportAsync(Guid contractReportGid, CancellationToken ct)
        {
            var contractReport = await this.contractReportsRepository.FindAsync(contractReportGid, ct);

            var newContractReport = new Domain.Contracts.ContractReport(
                    contractReport.ContractId,
                    contractReport.ReportType,
                    Source.Beneficiary,
                    await this.contractReportsRepository.GetNextOrderNumberAsync(contractReport.ContractId, ct),
                    contractReport.OtherRegistration,
                    contractReport.StoragePlace,
                    null,
                    null);

            this.contractReportsRepository.Add(newContractReport);

            await this.unitOfWork.SaveAsync(ct);

            switch (newContractReport.ReportType)
            {
                case ContractReportType.AdvancePayment:
                    await this.CopyContractReportPaymentAsync(contractReport, newContractReport, ct);
                    break;
                case ContractReportType.Technical:
                    await this.CopyContractReportTechnicalAsync(contractReport, newContractReport, ct);
                    break;
                case ContractReportType.PaymentFinancial:
                    await this.CopyContractReportPaymentAsync(contractReport, newContractReport, ct);
                    await this.CopyContractReportFinancialAsync(contractReport, newContractReport, ct);
                    break;
                case ContractReportType.PaymentTechnicalFinancial:
                    await this.CopyContractReportTechnicalAsync(contractReport, newContractReport, ct);
                    await this.CopyContractReportPaymentAsync(contractReport, newContractReport, ct);
                    await this.CopyContractReportFinancialAsync(contractReport, newContractReport, ct);
                    break;
            }

            await this.CopyContractReportMicrodataAsync(contractReport, newContractReport, ct);
            return newContractReport;
        }

        public Eumis.Domain.Contracts.ContractReportFinancial CreateContractReportFinancial(int contractReportId)
        {
            var contractReport = this.contractReportsRepository.Find(contractReportId);
            contractReport.AssertIsDraftContractReport();

            var contract = this.contractsRepository.Find(contractReport.ContractId);

            if (this.CanCreateContractReportFinancial(contractReportId).Any())
            {
                throw new DomainException("Cannot create ContractReportFinancial");
            }

            if (this.contractVersionsRepository.HasContractBlockingVersionsInProgress(contractReport.ContractId))
            {
                throw new Exception("Cannot create ContractReportFinancial if there are contract blocking versions in progress.");
            }

            var activeContractVersion = this.contractVersionsRepository.GetActiveVersion(contractReport.ContractId);
            var activeContractProcurement = this.contractProcurementsRepository.GetActiveProcurementOrDefault(contractReport.ContractId);

            var lastFinanceReport = this.contractReportFinancialsRepository.GetLastContractReportFinancial(contract.ContractId);
            string lastFinanceReportXml = lastFinanceReport != null ? lastFinanceReport.Xml : null;

            var advanceVerificationPayment = this.contractReportPaymentsRepository.GetLastAdvanceVerificationContractReportPayment(contract.ContractId);
            string advanceVerificationPaymentXml = advanceVerificationPayment != null ? advanceVerificationPayment.Xml : null;

            var approvedCumulativeCSDBudgetAmounts = this.contractReportFinancialCSDBudgetItemsRepository.GetPortalContractReportFinancialCSDBudgetItems(contractReport.ContractId);

            var versionNum = this.contractReportFinancialsRepository.GetNextVersionNum(contract.ContractId);
            var versionSubNum = this.contractReportFinancialsRepository.GetNextVersionSubNum(contractReportId);
            var newContractReportFinancial = new Eumis.Domain.Contracts.ContractReportFinancial(
                contractReport.ContractId,
                contractReportId,
                versionNum,
                versionSubNum,
                this.documentRestApiCommunicator.CreateContractReportFinancial(
                    contract.Gid,
                    contractReport.Gid,
                    activeContractVersion.Xml,
                    lastFinanceReportXml,
                    advanceVerificationPaymentXml,
                    activeContractProcurement?.Xml,
                    approvedCumulativeCSDBudgetAmounts,
                    contract.RegNumber,
                    versionNum.ToString(),
                    versionSubNum.ToString()));

            this.contractReportFinancialsRepository.Add(newContractReportFinancial);

            this.unitOfWork.Save();

            return newContractReportFinancial;
        }

        public async Task<ContractReportFinancial> CreateContractReportFinancialAsync(int contractReportId, CancellationToken ct)
        {
            var contractReport = await this.contractReportsRepository.FindWithoutIncludesAsync(contractReportId, ct);
            contractReport.AssertIsDraftContractReport();

            var contract = await this.contractsRepository.FindWithoutIncludesAsync(contractReport.ContractId, ct);

            if ((await this.CanCreateContractReportFinancialAsync(contractReportId, ct)).Any())
            {
                throw new DomainException("Cannot create ContractReportFinancial");
            }

            if (await this.contractVersionsRepository.HasContractBlockingVersionsInProgressAsync(contractReport.ContractId, ct))
            {
                throw new Exception("Cannot create ContractReportFinancial if there are contract blocking versions in progress.");
            }

            var activeContractVersion = await this.contractVersionsRepository.GetActiveVersionAsync(contractReport.ContractId, ct);
            var activeContractProcurement = await this.contractProcurementsRepository.GetActiveProcurementOrDefaultAsync(contractReport.ContractId, ct);

            var lastFinanceReport = await this.contractReportFinancialsRepository.GetLastContractReportFinancialAsync(contract.ContractId, ct);
            string lastFinanceReportXml = lastFinanceReport != null ? lastFinanceReport.Xml : null;

            var advanceVerificationPayment = await this.contractReportPaymentsRepository.GetLastAdvanceVerificationContractReportPaymentAsync(contract.ContractId, ct);
            string advanceVerificationPaymentXml = advanceVerificationPayment != null ? advanceVerificationPayment.Xml : null;

            var approvedCumulativeCSDBudgetAmounts = await this.contractReportFinancialCSDBudgetItemsRepository.GetPortalContractReportFinancialCSDBudgetItemsAsync(contractReport.ContractId, ct);

            var versionNum = await this.contractReportFinancialsRepository.GetNextVersionNumAsync(contract.ContractId, ct);
            var versionSubNum = await this.contractReportFinancialsRepository.GetNextVersionSubNumAsync(contractReportId, ct);
            var newContractReportFinancial = new Eumis.Domain.Contracts.ContractReportFinancial(
                contractReport.ContractId,
                contractReportId,
                versionNum,
                versionSubNum,
                await this.documentRestApiCommunicator.CreateContractReportFinancialAsync(
                    contract.Gid,
                    contractReport.Gid,
                    activeContractVersion.Xml,
                    lastFinanceReportXml,
                    advanceVerificationPaymentXml,
                    activeContractProcurement?.Xml,
                    approvedCumulativeCSDBudgetAmounts,
                    contract.RegNumber,
                    versionNum.ToString(),
                    versionSubNum.ToString()));

            this.contractReportFinancialsRepository.Add(newContractReportFinancial);

            await this.unitOfWork.SaveAsync(ct);

            return newContractReportFinancial;
        }

        private async Task CopyContractReportFinancialAsync(Domain.Contracts.ContractReport contractReport, ContractReportFinancial originFinancialReport, CancellationToken ct)
        {
            contractReport.AssertIsDraftContractReport();

            var contract = await this.contractsRepository.FindWithoutIncludesAsync(contractReport.ContractId, ct);

            if ((await this.CanCreateContractReportFinancialAsync(contractReport.ContractReportId, ct)).Any())
            {
                throw new DomainException("Cannot create ContractReportFinancial");
            }

            if (await this.contractVersionsRepository.HasContractBlockingVersionsInProgressAsync(contractReport.ContractId, ct))
            {
                throw new Exception("Cannot create ContractReportFinancial if there are contract blocking versions in progress.");
            }

            var activeContractVersion = await this.contractVersionsRepository.GetActiveVersionAsync(contractReport.ContractId, ct);
            var activeContractProcurement = await this.contractProcurementsRepository.GetActiveProcurementOrDefaultAsync(contractReport.ContractId, ct);

            var lastFinanceReport = await this.contractReportFinancialsRepository.GetLastContractReportFinancialAsync(contract.ContractId, ct);
            string lastFinanceReportXml = lastFinanceReport != null ? lastFinanceReport.Xml : null;

            var advanceVerificationPayment = await this.contractReportPaymentsRepository.GetLastAdvanceVerificationContractReportPaymentAsync(contract.ContractId, ct);
            string advanceVerificationPaymentXml = advanceVerificationPayment != null ? advanceVerificationPayment.Xml : null;

            var approvedCumulativeCSDBudgetAmounts = await this.contractReportFinancialCSDBudgetItemsRepository.GetPortalContractReportFinancialCSDBudgetItemsAsync(contractReport.ContractId, ct);

            var versionNum = await this.contractReportFinancialsRepository.GetNextVersionNumAsync(contract.ContractId, ct);
            var versionSubNum = await this.contractReportFinancialsRepository.GetNextVersionSubNumAsync(contractReport.ContractReportId, ct);
            var newContractReportFinancial = new Eumis.Domain.Contracts.ContractReportFinancial(
                contractReport.ContractId,
                contractReport.ContractReportId,
                versionNum,
                versionSubNum,
                await this.documentRestApiCommunicator.CopyContractReportFinancialAsync(
                    contract.Gid,
                    contractReport.Gid,
                    activeContractVersion.Xml,
                    lastFinanceReportXml,
                    advanceVerificationPaymentXml,
                    activeContractProcurement?.Xml,
                    approvedCumulativeCSDBudgetAmounts,
                    originFinancialReport.Xml,
                    contract.RegNumber,
                    versionNum.ToString(),
                    versionSubNum.ToString()));

            this.contractReportFinancialsRepository.Add(newContractReportFinancial);

            await this.unitOfWork.SaveAsync(ct);
        }

        public Eumis.Domain.Contracts.ContractReportPayment CreateContractReportPayment(int contractReportId, ContractReportPaymentType type)
        {
            var contractReport = this.contractReportsRepository.Find(contractReportId);
            contractReport.AssertIsDraftContractReport();

            var contract = this.contractsRepository.Find(contractReport.ContractId);

            if (this.CanCreateContractReportPayment(contractReportId, type).Any())
            {
                throw new DomainException("Cannot create CreateContractReportPayment");
            }

            if (this.contractVersionsRepository.HasContractBlockingVersionsInProgress(contractReport.ContractId))
            {
                throw new Exception("Cannot create ContractReportPayment if there are contract blocking versions in progress.");
            }

            var activeContractVersion = this.contractVersionsRepository.GetActiveVersion(contractReport.ContractId);
            var activeContractProcurement = this.contractProcurementsRepository.GetActiveProcurementOrDefault(contractReport.ContractId);

            var versionNum = this.contractReportPaymentsRepository.GetNextVersionNum(contract.ContractId);
            var versionSubNum = this.contractReportPaymentsRepository.GetNextVersionSubNum(contractReportId);
            var newContractReportPayment = new Eumis.Domain.Contracts.ContractReportPayment(
                contractReport.ContractId,
                contractReportId,
                type,
                versionNum,
                versionSubNum,
                this.documentRestApiCommunicator.CreateContractReportPayment(
                    contract.Gid,
                    contractReport.Gid,
                    type,
                    activeContractVersion.Xml,
                    activeContractProcurement?.Xml,
                    contract.RegNumber,
                    versionNum.ToString(),
                    versionSubNum.ToString()));

            this.contractReportPaymentsRepository.Add(newContractReportPayment);

            this.unitOfWork.Save();

            return newContractReportPayment;
        }

        private async Task CopyContractReportPaymentAsync(Domain.Contracts.ContractReport contractReport, ContractReportPayment originPayment, CancellationToken ct)
        {
            contractReport.AssertIsDraftContractReport();

            var contract = await this.contractsRepository.FindWithoutIncludesAsync(contractReport.ContractId, ct);
            var paymentType = originPayment.PaymentType.Value;

            if ((await this.CanCreateContractReportPaymentAsync(contractReport.ContractReportId, paymentType, ct)).Any())
            {
                throw new DomainException("Cannot create CreateContractReportPayment");
            }

            if (await this.contractVersionsRepository.HasContractBlockingVersionsInProgressAsync(contractReport.ContractId, ct))
            {
                throw new Exception("Cannot create ContractReportPayment if there are contract blocking versions in progress.");
            }

            var activeContractVersion = await this.contractVersionsRepository.GetActiveVersionAsync(contractReport.ContractId, ct);
            var activeContractProcurement = await this.contractProcurementsRepository.GetActiveProcurementOrDefaultAsync(contractReport.ContractId, ct);

            var versionNum = await this.contractReportPaymentsRepository.GetNextVersionNumAsync(contract.ContractId, ct);
            var versionSubNum = await this.contractReportPaymentsRepository.GetNextVersionSubNumAsync(contractReport.ContractReportId, ct);
            var newContractReportPayment = new Eumis.Domain.Contracts.ContractReportPayment(
                contractReport.ContractId,
                contractReport.ContractReportId,
                paymentType,
                versionNum,
                versionSubNum,
                await this.documentRestApiCommunicator.CopyContractReportPaymentAsync(
                    contract.Gid,
                    contractReport.Gid,
                    paymentType,
                    activeContractVersion.Xml,
                    activeContractProcurement?.Xml,
                    originPayment.Xml,
                    contract.RegNumber,
                    versionNum.ToString(),
                    versionSubNum.ToString()));

            this.contractReportPaymentsRepository.Add(newContractReportPayment);

            await this.unitOfWork.SaveAsync(ct);
        }

        public Eumis.Domain.Contracts.ContractReportTechnical CreateContractReportTechnical(int contractReportId)
        {
            var contractReport = this.contractReportsRepository.Find(contractReportId);
            contractReport.AssertIsDraftContractReport();

            var contract = this.contractsRepository.Find(contractReport.ContractId);

            if (this.CanCreateContractReportTechnical(contractReportId).Any())
            {
                throw new DomainException("Cannot create ContractReportTechnical");
            }

            if (this.contractVersionsRepository.HasContractBlockingVersionsInProgress(contractReport.ContractId))
            {
                throw new Exception("Cannot create ContractReportTechnical if there are contract blocking versions in progress");
            }

            var activeContractVersion = this.contractVersionsRepository.GetActiveVersion(contractReport.ContractId);
            var activeContractProcurement = this.contractProcurementsRepository.GetActiveProcurementOrDefault(contractReport.ContractId);
            var lastTechnicalReport = this.contractReportTechnicalsRepository.GetLastContractReportTechnical(contract.ContractId);
            string lastTechnicalReportXml = lastTechnicalReport?.Xml;

            var versionNum = this.contractReportTechnicalsRepository.GetNextVersionNum(contract.ContractId);
            var versionSubNum = this.contractReportTechnicalsRepository.GetNextVersionSubNum(contractReportId);

            var lastTechnicalReportIndicators = lastTechnicalReport != null ?
                this.contractReportIndicatorsRepository.GetPortalContractReportIndicators(lastTechnicalReport.ContractReportTechnicalId) :
                null;

            var newContractReportTechnical = new Eumis.Domain.Contracts.ContractReportTechnical(
                contractReport.ContractId,
                contractReportId,
                versionNum,
                versionSubNum,
                this.documentRestApiCommunicator.CreateContractReportTechnical(
                    contract.Gid,
                    contractReport.Gid,
                    activeContractVersion.Xml,
                    activeContractProcurement?.Xml,
                    lastTechnicalReportXml,
                    lastTechnicalReportIndicators,
                    contract.RegNumber,
                    versionNum.ToString(),
                    versionSubNum.ToString()));

            this.contractReportTechnicalsRepository.Add(newContractReportTechnical);

            this.unitOfWork.Save();

            return newContractReportTechnical;
        }

        public async Task<Eumis.Domain.Contracts.ContractReportTechnical> CreateContractReportTechnicalAsync(int contractReportId, CancellationToken ct)
        {
            var contractReport = await this.contractReportsRepository.FindWithoutIncludesAsync(contractReportId, ct);
            contractReport.AssertIsDraftContractReport();

            var contract = await this.contractsRepository.FindWithoutIncludesAsync(contractReport.ContractId, ct);

            if (this.CanCreateContractReportTechnical(contractReportId).Any())
            {
                throw new DomainException("Cannot create ContractReportTechnical");
            }

            if (this.contractVersionsRepository.HasContractBlockingVersionsInProgress(contractReport.ContractId))
            {
                throw new Exception("Cannot create ContractReportTechnical if there are contract blocking versions in progress");
            }

            var activeContractVersion = await this.contractVersionsRepository.GetActiveVersionAsync(contractReport.ContractId, ct);
            var activeContractProcurement = await this.contractProcurementsRepository.GetActiveProcurementOrDefaultAsync(contractReport.ContractId, ct);
            var lastTechnicalReport = await this.contractReportTechnicalsRepository.GetLastContractReportTechnicalAsync(contract.ContractId, ct);
            string lastTechnicalReportXml = lastTechnicalReport?.Xml;

            var versionNum = this.contractReportTechnicalsRepository.GetNextVersionNum(contract.ContractId);
            var versionSubNum = this.contractReportTechnicalsRepository.GetNextVersionSubNum(contractReportId);

            var lastTechnicalReportIndicators = lastTechnicalReport != null ?
                await this.contractReportIndicatorsRepository.GetPortalContractReportIndicatorsAsync(lastTechnicalReport.ContractReportTechnicalId, ct) :
                null;

            var newContractReportTechnical = new Eumis.Domain.Contracts.ContractReportTechnical(
                contractReport.ContractId,
                contractReportId,
                versionNum,
                versionSubNum,
                await this.documentRestApiCommunicator.CreateContractReportTechnicalAsync(
                    contract.Gid,
                    contractReport.Gid,
                    activeContractVersion.Xml,
                    activeContractProcurement?.Xml,
                    lastTechnicalReportXml,
                    lastTechnicalReportIndicators,
                    contract.RegNumber,
                    versionNum.ToString(),
                    versionSubNum.ToString()));

            this.contractReportTechnicalsRepository.Add(newContractReportTechnical);

            await this.unitOfWork.SaveAsync(ct);

            return newContractReportTechnical;
        }

        private async Task CopyContractReportTechnicalAsync(Domain.Contracts.ContractReport contractReport, ContractReportTechnical originTechnicalReport, CancellationToken ct)
        {
            contractReport.AssertIsDraftContractReport();

            var contract = await this.contractsRepository.FindWithoutIncludesAsync(contractReport.ContractId, ct);

            if ((await this.CanCreateContractReportTechnicalAsync(contractReport.ContractReportId, ct)).Any())
            {
                throw new DomainException("Cannot create ContractReportTechnical");
            }

            if (await this.contractVersionsRepository.HasContractBlockingVersionsInProgressAsync(contractReport.ContractId, ct))
            {
                throw new Exception("Cannot create ContractReportTechnical if there are contract blocking versions in progress.");
            }

            var activeContractVersion = await this.contractVersionsRepository.GetActiveVersionAsync(contractReport.ContractId, ct);
            var activeContractProcurement = await this.contractProcurementsRepository.GetActiveProcurementOrDefaultAsync(contractReport.ContractId, ct);

            var lastTechnicalReport = await this.contractReportTechnicalsRepository.GetLastContractReportTechnicalAsync(contract.ContractId, ct);
            string lastTechnicalReportXml = lastTechnicalReport?.Xml;

            var versionNum = await this.contractReportTechnicalsRepository.GetNextVersionNumAsync(contract.ContractId, ct);
            var versionSubNum = await this.contractReportTechnicalsRepository.GetNextVersionSubNumAsync(contractReport.ContractReportId, ct);

            var lastTechnicalReportIndicators = lastTechnicalReport != null ?
                await this.contractReportIndicatorsRepository.GetPortalContractReportIndicatorsAsync(lastTechnicalReport.ContractReportTechnicalId, ct) :
                null;

            var newContractReportTechnical = new Eumis.Domain.Contracts.ContractReportTechnical(
                contractReport.ContractId,
                contractReport.ContractReportId,
                versionNum,
                versionSubNum,
                await this.documentRestApiCommunicator.CopyContractReportTechnicalAsync(
                    contract.Gid,
                    contractReport.Gid,
                    activeContractVersion.Xml,
                    activeContractProcurement?.Xml,
                    lastTechnicalReportXml,
                    lastTechnicalReportIndicators,
                    originTechnicalReport.Xml,
                    contract.RegNumber,
                    versionNum.ToString(),
                    versionSubNum.ToString()));

            this.contractReportTechnicalsRepository.Add(newContractReportTechnical);

            await this.unitOfWork.SaveAsync(ct);
        }

        public Eumis.Domain.Contracts.ContractReportFinancialCheck CreateContractReportFinancialCheck(int contractReportId)
        {
            var actualFinancial = this.contractReportFinancialsRepository.GetActualContractReportFinancial(contractReportId);
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsUncheckedContractReport();

            var newContractReportFinancialCheck = new Eumis.Domain.Contracts.ContractReportFinancialCheck(
                actualFinancial.ContractReportFinancialId,
                contractReport.ContractReportId,
                contractReport.ContractId,
                this.contractReportFinancialChecksRepository.GetNextOrderNum(actualFinancial.ContractReportFinancialId));

            this.contractReportFinancialChecksRepository.Add(newContractReportFinancialCheck);

            this.unitOfWork.Save();

            return newContractReportFinancialCheck;
        }

        public Eumis.Domain.Contracts.ContractReportPaymentCheck CreateContractReportPaymentCheck(int contractReportId)
        {
            var actualPayment = this.contractReportPaymentsRepository.GetActualContractReportPayment(contractReportId);
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsUncheckedContractReport();

            var paymentCheck = new Eumis.Domain.Contracts.ContractReportPaymentCheck(
                actualPayment.ContractReportPaymentId,
                contractReport.ContractReportId,
                contractReport.ContractId,
                this.contractReportPaymentChecksRepository.GetNextOrderNum(actualPayment.ContractReportPaymentId),
                actualPayment.PaymentType.Value);

            this.contractReportPaymentChecksRepository.Add(paymentCheck);

            this.unitOfWork.Save();

            this.CreateContractReportPaymentCheckAmounts(contractReportId, paymentCheck);

            this.unitOfWork.Save();

            return paymentCheck;
        }

        private void CreateContractReportPaymentCheckAmounts(int contractReportId, ContractReportPaymentCheck paymentCheck)
        {
            if (paymentCheck.PaymentType == ContractReportPaymentType.Advance)
            {
                var advanceNVPaymentAmounts = this.contractReportAdvanceNVPaymentAmountsRepository.GetContractReportAdvanceNVPaymentAmounts(contractReportId);
                foreach (var advanceNVPaymentAmount in advanceNVPaymentAmounts)
                {
                    paymentCheck.AddContractReportPaymentCheckAmount(
                        advanceNVPaymentAmount.ProgrammePriorityId,
                        0m,
                        0m,
                        0m,
                        0m,
                        0m,
                        0m,
                        advanceNVPaymentAmount.EuAmount.Value,
                        advanceNVPaymentAmount.BgAmount.Value,
                        advanceNVPaymentAmount.BfpTotalAmount.Value,
                        0m);
                }
            }
            else if (paymentCheck.PaymentType == ContractReportPaymentType.AdvanceVerification)
            {
                var advancePaymentAmounts = this.contractReportAdvancePaymentAmountsRepository.GetContractReportAdvancePaymentAmounts(contractReportId);
                foreach (var advancePaymentAmount in advancePaymentAmounts)
                {
                    paymentCheck.AddContractReportPaymentCheckAmount(
                        advancePaymentAmount.ProgrammePriorityId,
                        advancePaymentAmount.ApprovedEuAmount.Value,
                        advancePaymentAmount.ApprovedBgAmount.Value,
                        advancePaymentAmount.ApprovedBfpTotalAmount.Value,
                        0m,
                        0m,
                        advancePaymentAmount.ApprovedBfpTotalAmount.Value,
                        advancePaymentAmount.ApprovedEuAmount.Value,
                        advancePaymentAmount.ApprovedBgAmount.Value,
                        advancePaymentAmount.ApprovedBfpTotalAmount.Value,
                        0m);
                }
            }
            else if (paymentCheck.PaymentType == ContractReportPaymentType.Intermediate || paymentCheck.PaymentType == ContractReportPaymentType.Final)
            {
                var csdBudgetItems = this.contractReportFinancialCSDBudgetItemsRepository.GetContractReportFinancialCSDBudgetItems(contractReportId)
                    .Select(t => new
                    {
                        t.ProgrammePriorityId,
                        t.AdvancePayment,
                        t.CrossFinancing,
                        BfpTotalAmount = t.ApprovedBfpTotalAmount,
                        SelfAmount = t.ApprovedSelfAmount,
                        TotalAmount = t.ApprovedTotalAmount,
                    })
                    .ToList();

                var contractReportAttachedFinancialCorrectionSignCorrectedAmounts = this.contractReportsRepository.GetContractReportAttachedFinancialCorrectionSignCorrectedAmounts(contractReportId)
                    .Select(t => new
                    {
                        t.ProgrammePriorityId,
                        t.AdvancePayment,
                        t.CrossFinancing,
                        BfpTotalAmount = -1 * (int)t.Sign * t.CorrectedApprovedBfpTotalAmount,
                        SelfAmount = -1 * (int)t.Sign * t.CorrectedApprovedSelfAmount,
                        TotalAmount = -1 * (int)t.Sign * t.CorrectedApprovedTotalAmount,
                    })
                    .ToList();

                var unionResults = csdBudgetItems.Concat(contractReportAttachedFinancialCorrectionSignCorrectedAmounts);
                var groupedResults = unionResults.GroupBy(t => new { t.ProgrammePriorityId });

                decimal approvedBfpTotalAmount = 0m;
                decimal approvedSelfAmount = 0m;
                decimal approvedTotalAmount = 0m;
                decimal paidBfpTotalAmount = 0m;

                foreach (var groupedResult in groupedResults)
                {
                    approvedBfpTotalAmount = 0m;
                    approvedSelfAmount = 0m;
                    approvedTotalAmount = 0m;
                    paidBfpTotalAmount = 0m;

                    foreach (var gr in groupedResult)
                    {
                        if (gr.AdvancePayment != Domain.Core.YesNoNonApplicable.Yes)
                        {
                            approvedBfpTotalAmount += gr.BfpTotalAmount.Value;
                            approvedSelfAmount += gr.SelfAmount.Value;
                            approvedTotalAmount += gr.TotalAmount.Value;
                        }
                    }

                    paidBfpTotalAmount = approvedBfpTotalAmount;

                    paymentCheck.AddContractReportPaymentCheckAmount(
                        groupedResult.Key.ProgrammePriorityId,
                        0,
                        approvedBfpTotalAmount,
                        approvedBfpTotalAmount,
                        0,
                        approvedSelfAmount,
                        approvedTotalAmount,
                        0,
                        paidBfpTotalAmount,
                        paidBfpTotalAmount,
                        0);
                }
            }
            else
            {
                throw new DomainException("Unsupported ContractReportPaymentType");
            }
        }

        private async Task CreateContractReportPaymentCheckAmountsAsync(int contractReportId, ContractReportPaymentCheck paymentCheck, CancellationToken ct)
        {
            if (paymentCheck.PaymentType == ContractReportPaymentType.Advance)
            {
                var advanceNVPaymentAmounts = await this.contractReportAdvanceNVPaymentAmountsRepository.GetContractReportAdvanceNVPaymentAmountsAsync(contractReportId, ct);
                foreach (var advanceNVPaymentAmount in advanceNVPaymentAmounts)
                {
                    paymentCheck.AddContractReportPaymentCheckAmount(
                        advanceNVPaymentAmount.ProgrammePriorityId,
                        0m,
                        0m,
                        0m,
                        0m,
                        0m,
                        0m,
                        advanceNVPaymentAmount.EuAmount.Value,
                        advanceNVPaymentAmount.BgAmount.Value,
                        advanceNVPaymentAmount.BfpTotalAmount.Value,
                        0m);
                }
            }
            else if (paymentCheck.PaymentType == ContractReportPaymentType.AdvanceVerification)
            {
                var advancePaymentAmounts = await this.contractReportAdvancePaymentAmountsRepository.GetContractReportAdvancePaymentAmountsAsync(contractReportId, ct);
                foreach (var advancePaymentAmount in advancePaymentAmounts)
                {
                    paymentCheck.AddContractReportPaymentCheckAmount(
                        advancePaymentAmount.ProgrammePriorityId,
                        advancePaymentAmount.ApprovedEuAmount.Value,
                        advancePaymentAmount.ApprovedBgAmount.Value,
                        advancePaymentAmount.ApprovedBfpTotalAmount.Value,
                        0m,
                        0m,
                        advancePaymentAmount.ApprovedBfpTotalAmount.Value,
                        advancePaymentAmount.ApprovedEuAmount.Value,
                        advancePaymentAmount.ApprovedBgAmount.Value,
                        advancePaymentAmount.ApprovedBfpTotalAmount.Value,
                        0m);
                }
            }
            else if (paymentCheck.PaymentType == ContractReportPaymentType.Intermediate || paymentCheck.PaymentType == ContractReportPaymentType.Final)
            {
                var csdBudgetItems = (await this.contractReportFinancialCSDBudgetItemsRepository.GetContractReportFinancialCSDBudgetItemsAsync(contractReportId, ct, string.Empty, string.Empty))
                    .Select(t => new
                    {
                        t.ProgrammePriorityId,
                        t.AdvancePayment,
                        t.CrossFinancing,
                        EuAmount = t.ApprovedEuAmount,
                        BgAmount = t.ApprovedBgAmount,
                        BfpTotalAmount = t.ApprovedBfpTotalAmount,
                        SelfAmount = t.ApprovedSelfAmount,
                        TotalAmount = t.ApprovedTotalAmount,
                    })
                    .ToList();

                var contractReportAttachedFinancialCorrectionSignCorrectedAmounts = (await this.contractReportsRepository.GetContractReportAttachedFinancialCorrectionSignCorrectedAmountsAsync(contractReportId, ct))
                    .Select(t => new
                    {
                        t.ProgrammePriorityId,
                        t.AdvancePayment,
                        t.CrossFinancing,
                        EuAmount = -1 * (int)t.Sign * t.CorrectedApprovedEuAmount,
                        BgAmount = -1 * (int)t.Sign * t.CorrectedApprovedBgAmount,
                        BfpTotalAmount = -1 * (int)t.Sign * t.CorrectedApprovedBfpTotalAmount,
                        SelfAmount = -1 * (int)t.Sign * t.CorrectedApprovedSelfAmount,
                        TotalAmount = -1 * (int)t.Sign * t.CorrectedApprovedTotalAmount,
                    })
                    .ToList();

                var unionResults = csdBudgetItems.Concat(contractReportAttachedFinancialCorrectionSignCorrectedAmounts);
                var groupedResults = unionResults.GroupBy(t => new { t.ProgrammePriorityId });

                decimal approvedEuAmount = 0m;
                decimal approvedBgAmount = 0m;
                decimal approvedBfpTotalAmount = 0m;
                decimal approvedCrossAmount = 0m;
                decimal approvedSelfAmount = 0m;
                decimal approvedTotalAmount = 0m;
                decimal paidEuAmount = 0m;
                decimal paidBgAmount = 0m;
                decimal paidBfpTotalAmount = 0m;
                decimal paidCrossAmount = 0m;

                foreach (var groupedResult in groupedResults)
                {
                    approvedEuAmount = 0m;
                    approvedBgAmount = 0m;
                    approvedBfpTotalAmount = 0m;
                    approvedCrossAmount = 0m;
                    approvedSelfAmount = 0m;
                    approvedTotalAmount = 0m;
                    paidEuAmount = 0m;
                    paidBgAmount = 0m;
                    paidBfpTotalAmount = 0m;
                    paidCrossAmount = 0m;

                    foreach (var gr in groupedResult)
                    {
                        if (gr.AdvancePayment != Domain.Core.YesNoNonApplicable.Yes)
                        {
                            approvedEuAmount += gr.EuAmount.Value;
                            approvedBgAmount += gr.BgAmount.Value;
                            approvedBfpTotalAmount += gr.BfpTotalAmount.Value;
                            approvedSelfAmount += gr.SelfAmount.Value;
                            approvedTotalAmount += gr.TotalAmount.Value;

                            if (gr.CrossFinancing == Domain.Core.YesNoNonApplicable.Yes)
                            {
                                approvedCrossAmount += gr.BfpTotalAmount.Value;
                            }
                        }
                    }

                    paidEuAmount = approvedEuAmount;
                    paidBgAmount = approvedBgAmount;
                    paidBfpTotalAmount = approvedBfpTotalAmount;
                    paidCrossAmount = approvedCrossAmount;

                    paymentCheck.AddContractReportPaymentCheckAmount(
                        groupedResult.Key.ProgrammePriorityId,
                        approvedEuAmount,
                        approvedBgAmount,
                        approvedBfpTotalAmount,
                        approvedCrossAmount,
                        approvedSelfAmount,
                        approvedTotalAmount,
                        paidEuAmount,
                        paidBgAmount,
                        paidBfpTotalAmount,
                        paidCrossAmount);
                }
            }
            else
            {
                throw new DomainException("Unsupported ContractReportPaymentType");
            }
        }

        public Eumis.Domain.Contracts.ContractReportTechnicalCheck CreateContractReportTechnicalCheck(int contractReportId)
        {
            var actualTechnical = this.contractReportTechnicalsRepository.GetActualContractReportTechnical(contractReportId);
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsUncheckedContractReport();

            var newContractReportTechnicalCheck = new Eumis.Domain.Contracts.ContractReportTechnicalCheck(
                actualTechnical.ContractReportTechnicalId,
                contractReport.ContractReportId,
                contractReport.ContractId,
                this.contractReportTechnicalChecksRepository.GetNextOrderNum(actualTechnical.ContractReportTechnicalId));

            this.contractReportTechnicalChecksRepository.Add(newContractReportTechnicalCheck);

            this.unitOfWork.Save();

            return newContractReportTechnicalCheck;
        }

        public async Task<Eumis.Domain.Contracts.ContractReport> CreateContractReportAsync(
            Guid contractGid,
            Domain.Contracts.ContractReportType reportType,
            string otherRegistration,
            string storagePlace,
            DateTime? submitDate,
            DateTime? submitDeadline,
            CancellationToken ct)
        {
            if ((await this.CanCreateContractReportAsync(contractGid, ct)).Any())
            {
                throw new DomainException("Cannot create ContractReport");
            }

            var contract = await this.contractsRepository.FindAsync(contractGid, ct);

            var newContractReport = new Eumis.Domain.Contracts.ContractReport(
                contract.ContractId,
                reportType,
                Source.Beneficiary,
                this.contractReportsRepository.GetNextOrderNumber(contract.ContractId),
                otherRegistration,
                storagePlace,
                submitDate,
                submitDeadline);

            this.contractReportsRepository.Add(newContractReport);

            await this.unitOfWork.SaveAsync(ct);

            return newContractReport;
        }

        public async Task<ContractReportFinancial> CreateContractReportFinancialAsync(Guid contractReportGid, CancellationToken ct)
        {
            var contractReport = await this.contractReportsRepository.FindAsync(contractReportGid, ct);
            var contractReportFinancial = await this.CreateContractReportFinancialAsync(contractReport.ContractReportId, ct);

            return contractReportFinancial;
        }

        public Eumis.Domain.Contracts.ContractReportPayment CreateContractReportPayment(Guid contractReportGid, ContractReportPaymentType type)
        {
            return this.CreateContractReportPayment(this.contractReportsRepository.Find(contractReportGid).ContractReportId, type);
        }

        public async Task<Eumis.Domain.Contracts.ContractReportTechnical> CreateContractReportTechnicalAsync(Guid contractReportGid, CancellationToken ct)
        {
            var contractReport = await this.contractReportsRepository.FindAsync(contractReportGid, ct);
            return await this.CreateContractReportTechnicalAsync(contractReport.ContractReportId, ct);
        }

        public Eumis.Domain.Contracts.ContractReport UpdateContractReport(
            int contractReportId,
            byte[] version,
            ContractReportType contractReportType,
            string otherRegistration,
            string storagePlace,
            DateTime? submitDate,
            DateTime? submitDeadline)
        {
            return this.UpdateContractReportInt(
                this.contractReportsRepository.FindForUpdate(contractReportId, version),
                contractReportType,
                otherRegistration,
                storagePlace,
                submitDate,
                submitDeadline);
        }

        private Eumis.Domain.Contracts.ContractReport UpdateContractReportInt(
            Eumis.Domain.Contracts.ContractReport contractReport,
            ContractReportType contractReportType,
            string otherRegistration,
            string storagePlace,
            DateTime? submitDate,
            DateTime? submitDeadline)
        {
            contractReport.AssertIsDraftContractReport();

            contractReport.UpdateAttributes(
                contractReportType,
                otherRegistration,
                storagePlace,
                submitDate,
                submitDeadline);

            this.unitOfWork.Save();

            return contractReport;
        }

        private async Task<Eumis.Domain.Contracts.ContractReport> UpdateContractReportIntAsync(
            Eumis.Domain.Contracts.ContractReport contractReport,
            ContractReportType contractReportType,
            string otherRegistration,
            string storagePlace,
            DateTime? submitDate,
            DateTime? submitDeadline,
            CancellationToken ct)
        {
            contractReport.AssertIsDraftContractReport();

            contractReport.UpdateAttributes(
                contractReportType,
                otherRegistration,
                storagePlace,
                submitDate,
                submitDeadline);

            await this.unitOfWork.SaveAsync(ct);

            return contractReport;
        }

        public Eumis.Domain.Contracts.ContractReport UpdateContractReportCheck(
            int contractReportId,
            byte[] version,
            DateTime? checkedDate)
        {
            var contractReport = this.contractReportsRepository.FindForUpdate(contractReportId, version);

            contractReport.AssertIsUncheckedContractReport();

            contractReport.UpdateCheckAttributes(checkedDate);

            this.unitOfWork.Save();

            return contractReport;
        }

        public Eumis.Domain.Contracts.ContractReportFinancial UpdateContractReportFinancial(int contractReportId, int contractReportFinancialId, byte[] version, string xml)
        {
            var finance = this.contractReportFinancialsRepository.FindForUpdate(contractReportFinancialId, version);

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            contractReport.AssertIsDraftOrUncheckedContractReport();

            this.AssertIsDraftContractReportFinancial(finance.Status);

            finance.SetXml(xml);

            this.unitOfWork.Save();

            return finance;
        }

        public async Task<ContractReportFinancial> UpdateContractReportFinancialAsync(int contractReportId, int contractReportFinancialId, byte[] version, string xml, CancellationToken ct)
        {
            var finance = await this.contractReportFinancialsRepository.FindForUpdateAsync(contractReportFinancialId, version, ct);

            var contractReport = await this.contractReportsRepository.FindWithoutIncludesAsync(contractReportId, ct);
            contractReport.AssertIsDraftOrUncheckedContractReport();

            this.AssertIsDraftContractReportFinancial(finance.Status);

            finance.SetXml(xml);

            await this.unitOfWork.SaveAsync(ct);

            return finance;
        }

        public Eumis.Domain.Contracts.ContractReportPayment UpdateContractReportPayment(int contractReportId, int contractReportPaymentId, byte[] version, string xml)
        {
            var payment = this.contractReportPaymentsRepository.FindForUpdate(contractReportPaymentId, version);
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsDraftOrUncheckedContractReport();

            this.AssertIsDraftContractReportPayment(payment.Status);

            payment.SetXml(xml);

            this.unitOfWork.Save();

            return payment;
        }

        public Eumis.Domain.Contracts.ContractReportTechnical UpdateContractReportTechnical(int contractReportId, int contractReportTechnicalId, byte[] version, string xml)
        {
            var technical = this.contractReportTechnicalsRepository.FindForUpdate(contractReportTechnicalId, version);
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsDraftOrUncheckedContractReport();

            this.AssertIsDraftContractReportTechnical(technical.Status);

            technical.SetXml(xml);

            this.unitOfWork.Save();

            return technical;
        }

        public async Task<ContractReportTechnical> UpdateContractReportTechnicalAsync(int contractReportId, int contractReportTechnicalId, byte[] version, string xml, CancellationToken ct)
        {
            var technical = await this.contractReportTechnicalsRepository.FindForUpdateAsync(contractReportTechnicalId, version, ct);
            var contractReport = await this.contractReportsRepository.FindWithoutIncludesAsync(contractReportId, ct);

            contractReport.AssertIsDraftOrUncheckedContractReport();

            this.AssertIsDraftContractReportTechnical(technical.Status);

            technical.SetXml(xml);

            await this.unitOfWork.SaveAsync(ct);

            return technical;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialCheck UpdateContractReportFinancialCheck(
            int contractReportFinancialCheckId,
            byte[] version,
            ContractReportFinancialCheckApproval? approval,
            Guid? blobKey,
            DateTime? checkedDate)
        {
            var financialCheck = this.contractReportFinancialChecksRepository.FindForUpdate(contractReportFinancialCheckId, version);
            var contractReport = this.contractReportsRepository.Find(financialCheck.ContractReportId);

            contractReport.AssertIsUncheckedContractReport();

            this.AssertIsDraftContractReportFinancialCheck(financialCheck.Status);

            financialCheck.UpdateAttributes(
                approval,
                blobKey,
                checkedDate);

            this.unitOfWork.Save();

            return financialCheck;
        }

        public Eumis.Domain.Contracts.ContractReportTechnicalCheck UpdateContractReportTechnicalCheck(
            int contractReportTechnicalCheckId,
            byte[] version,
            ContractReportTechnicalCheckApproval? approval,
            Guid? blobKey,
            DateTime? checkedDate)
        {
            var technicalCheck = this.contractReportTechnicalChecksRepository.FindForUpdate(contractReportTechnicalCheckId, version);
            var contractReport = this.contractReportsRepository.Find(technicalCheck.ContractReportId);

            contractReport.AssertIsUncheckedContractReport();

            this.AssertIsDraftContractReportTechnicalCheck(technicalCheck.Status);

            technicalCheck.UpdateAttributes(
                approval,
                blobKey,
                checkedDate);

            this.unitOfWork.Save();

            return technicalCheck;
        }

        public Eumis.Domain.Contracts.ContractReportPaymentCheck UpdateContractReportPaymentCheck(
            int contractReportPaymentCheckId,
            byte[] version,
            ContractReportPaymentCheckApproval? approval,
            Guid? blobKey,
            DateTime? checkedDate,
            IList<ContractReportPaymentCheckAmountDO> amounts)
        {
            var paymentCheck = this.contractReportPaymentChecksRepository.FindForUpdate(contractReportPaymentCheckId, version);
            var contractReport = this.contractReportsRepository.Find(paymentCheck.ContractReportId);

            contractReport.AssertIsUncheckedContractReport();

            this.AssertIsDraftContractReportPaymentCheck(paymentCheck.Status);

            paymentCheck.UpdateAttributes(
                approval,
                blobKey,
                checkedDate);

            paymentCheck.UpdateContractReportPaymentCheckAmounts(amounts);

            this.unitOfWork.Save();

            return paymentCheck;
        }

        public async Task<Eumis.Domain.Contracts.ContractReport> UpdateContractReportAsync(
            Guid contractReportGid,
            byte[] version,
            ContractReportType contractReportType,
            string otherRegistration,
            string storagePlace,
            DateTime? submitDate,
            DateTime? submitDeadline,
            CancellationToken ct)
        {
            return await this.UpdateContractReportIntAsync(
                await this.contractReportsRepository.FindForUpdateAsync(contractReportGid, version, ct),
                contractReportType,
                otherRegistration,
                storagePlace,
                submitDate,
                submitDeadline,
                ct);
        }

        public async Task<ContractReportFinancial> UpdateContractReportFinancialAsync(Guid contractReportFinancialGid, byte[] version, string xml, CancellationToken ct)
        {
            var financial = await this.contractReportFinancialsRepository.FindAsync(contractReportFinancialGid, ct);
            return await this.UpdateContractReportFinancialAsync(financial.ContractReportId, financial.ContractReportFinancialId, version, xml, ct);
        }

        public Eumis.Domain.Contracts.ContractReportPayment UpdateContractReportPayment(Guid contractReportPaymentGid, byte[] version, string xml)
        {
            var report = this.contractReportPaymentsRepository.Find(contractReportPaymentGid);
            return this.UpdateContractReportPayment(report.ContractReportId, report.ContractReportPaymentId, version, xml);
        }

        public async Task<ContractReportTechnical> UpdateContractReportTechnicalAsync(Guid contractReportTechnicalGid, byte[] version, string xml, CancellationToken ct)
        {
            var technical = await this.contractReportTechnicalsRepository.FindAsync(contractReportTechnicalGid, ct);
            return await this.UpdateContractReportTechnicalAsync(technical.ContractReportId, technical.ContractReportTechnicalId, version, xml, ct);
        }

        // delete
        public IList<string> CanDeleteContractReport(int contractReportId)
        {
            var errors = new List<string>();

            var finances = this.contractReportFinancialsRepository.FindAll(contractReportId);
            if (finances.Any())
            {
                errors.Add("Не може да се изтрие пакет, към който има въведени финансови отчети.");
            }

            var payments = this.contractReportPaymentsRepository.FindAll(contractReportId);
            if (payments.Any())
            {
                errors.Add("Не може да се изтрие пакет, към който има въведени искания за плащане.");
            }

            var technicals = this.contractReportTechnicalsRepository.FindAll(contractReportId);
            if (technicals.Any())
            {
                errors.Add("Не може да се изтрие пакет, към който има въведени технически отчети.");
            }

            var micros = this.contractReportMicrosRepository.FindAll(contractReportId);
            if (micros.Any())
            {
                errors.Add("Не може да се изтрие пакет, към който има въведени микроданни.");
            }

            return errors;
        }

        public async Task<IList<string>> CanDeleteContractReportAsync(int contractReportId, CancellationToken ct)
        {
            var errors = new List<string>();

            var finances = await this.contractReportFinancialsRepository.FindAllAsync(contractReportId, ct);
            if (finances.Any())
            {
                errors.Add("Не може да се изтрие пакет, към който има въведени финансови отчети.");
            }

            var payments = await this.contractReportPaymentsRepository.FindAllAsync(contractReportId, ct);
            if (payments.Any())
            {
                errors.Add("Не може да се изтрие пакет, към който има въведени искания за плащане.");
            }

            var technicals = await this.contractReportTechnicalsRepository.FindAllAsync(contractReportId, ct);
            if (technicals.Any())
            {
                errors.Add("Не може да се изтрие пакет, към който има въведени технически отчети.");
            }

            var micros = await this.contractReportMicrosRepository.FindAllAsync(contractReportId, ct);
            if (micros.Any())
            {
                errors.Add("Не може да се изтрие пакет, към който има въведени микроданни.");
            }

            return errors;
        }

        public async Task<IList<string>> CanDeleteContractReportAsync(Guid contractReportGid, CancellationToken ct)
        {
            var contractReport = await this.contractReportsRepository.FindAsync(contractReportGid, ct);
            return await this.CanDeleteContractReportAsync(contractReport.ContractReportId, ct);
        }

        public Eumis.Domain.Contracts.ContractReport DeleteContractReport(int contractReportId, byte[] version)
        {
            var report = this.contractReportsRepository.FindForUpdate(contractReportId, version);

            report.AssertIsDraftContractReport();

            if (this.CanDeleteContractReport(contractReportId).Count != 0)
            {
                throw new DomainException("Cannot remove ContractReport when there is an existing ContractReportFinancial or ContractReportPayment or ContractReportTechnical");
            }

            this.contractReportsRepository.Remove(report);

            this.unitOfWork.Save();

            return report;
        }

        public async Task<Eumis.Domain.Contracts.ContractReport> DeleteContractReportAsync(int contractReportId, byte[] version, CancellationToken ct)
        {
            var report = await this.contractReportsRepository.FindForUpdateAsync(contractReportId, version, ct);

            report.AssertIsDraftContractReport();

            if ((await this.CanDeleteContractReportAsync(contractReportId, ct)).Any())
            {
                throw new DomainException("Cannot remove ContractReport when there is an existing ContractReportFinancial or ContractReportPayment or ContractReportTechnical");
            }

            this.contractReportsRepository.Remove(report);

            await this.unitOfWork.SaveAsync(ct);

            return report;
        }

        public Eumis.Domain.Contracts.ContractReportFinancial DeleteContractReportFinancial(int contractReportId, int contractReportFinancialId, byte[] version)
        {
            var finance = this.contractReportFinancialsRepository.FindForUpdate(contractReportFinancialId, version);

            this.AssertIsDraftContractReportFinancial(finance.Status);

            var finances = this.contractReportFinancialsRepository.FindAll(contractReportId).Where(t => t.ContractReportFinancialId != finance.ContractReportFinancialId);
            if (finances.Any())
            {
                throw new DomainException("Cannot delete a ContractReportFinancial when there are more than one ContractReportFinancial");
            }

            this.contractReportFinancialsRepository.Remove(finance);

            this.unitOfWork.Save();

            return finance;
        }

        public async Task<ContractReportFinancial> DeleteContractReportFinancialAsync(int contractReportId, int contractReportFinancialId, byte[] version, CancellationToken ct)
        {
            var finance = await this.contractReportFinancialsRepository.FindForUpdateAsync(contractReportFinancialId, version, ct);

            this.AssertIsDraftContractReportFinancial(finance.Status);

            var contractReportFinancials = await this.contractReportFinancialsRepository.FindAllAsync(contractReportId, ct);

            var finances = contractReportFinancials.Where(t => t.ContractReportFinancialId != finance.ContractReportFinancialId);
            if (finances.Any())
            {
                throw new DomainException("Cannot delete a ContractReportFinancial when there are more than one ContractReportFinancial");
            }

            this.contractReportFinancialsRepository.Remove(finance);

            await this.unitOfWork.SaveAsync(ct);

            return finance;
        }

        public Eumis.Domain.Contracts.ContractReportPayment DeleteContractReportPayment(int contractReportId, int contractReportPaymentId, byte[] version)
        {
            var payment = this.contractReportPaymentsRepository.FindForUpdate(contractReportPaymentId, version);

            this.AssertIsDraftContractReportPayment(payment.Status);

            var payments = this.contractReportPaymentsRepository.FindAll(contractReportId).Where(t => t.ContractReportPaymentId != payment.ContractReportPaymentId);
            if (payments.Any())
            {
                throw new DomainException("Cannot delete a ContractReportPayment when there are more than one ContractReportPayment");
            }

            this.contractReportPaymentsRepository.Remove(payment);

            this.unitOfWork.Save();

            return payment;
        }

        public Eumis.Domain.Contracts.ContractReportTechnical DeleteContractReportTechnical(int contractReportId, int contractReportTechnicalId, byte[] version)
        {
            var technical = this.contractReportTechnicalsRepository.FindForUpdate(contractReportTechnicalId, version);

            this.AssertIsDraftContractReportTechnical(technical.Status);

            var technicals = this.contractReportTechnicalsRepository.FindAll(contractReportId).Where(t => t.ContractReportTechnicalId != technical.ContractReportTechnicalId);
            if (technicals.Any())
            {
                throw new DomainException("Cannot delete a ContractReportTechnical when there are more than one ContractReportTechnical");
            }

            this.contractReportTechnicalsRepository.Remove(technical);

            this.unitOfWork.Save();

            return technical;
        }

        public async Task<ContractReportTechnical> DeleteContractReportTechnicalAsync(int contractReportId, int contractReportTechnicalId, byte[] version, CancellationToken ct)
        {
            var technical = await this.contractReportTechnicalsRepository.FindForUpdateAsync(contractReportTechnicalId, version, ct);

            this.AssertIsDraftContractReportTechnical(technical.Status);

            var technicals = (await this.contractReportTechnicalsRepository.FindAllAsync(contractReportId, ct)).Where(t => t.ContractReportTechnicalId != technical.ContractReportTechnicalId);
            if (technicals.Any())
            {
                throw new DomainException("Cannot delete a ContractReportTechnical when there are more than one ContractReportTechnical");
            }

            this.contractReportTechnicalsRepository.Remove(technical);

            await this.unitOfWork.SaveAsync(ct);

            return technical;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialCheck DeleteContractReportFinancialCheck(int contractReportId, int contractReportFinancialCheckId, byte[] version)
        {
            var financialCheck = this.contractReportFinancialChecksRepository.FindForUpdate(contractReportFinancialCheckId, version);
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsUncheckedContractReport();

            if (financialCheck.Status != ContractReportFinancialCheckStatus.Draft)
            {
                throw new DomainException("Cannot delete a ContractReportFinancialCheck with status different from 'Draft'");
            }

            this.contractReportFinancialChecksRepository.Remove(financialCheck);

            this.unitOfWork.Save();

            return financialCheck;
        }

        public Eumis.Domain.Contracts.ContractReportPaymentCheck DeleteContractReportPaymentCheck(int contractReportId, int contractReportPaymentCheckId, byte[] version)
        {
            var paymentCheck = this.contractReportPaymentChecksRepository.FindForUpdate(contractReportPaymentCheckId, version);
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsUncheckedContractReport();

            if (paymentCheck.Status != ContractReportPaymentCheckStatus.Draft)
            {
                throw new DomainException("Cannot delete a ContractReportPaymentCheck with status different from 'Draft'");
            }

            this.contractReportPaymentChecksRepository.Remove(paymentCheck);

            this.unitOfWork.Save();

            return paymentCheck;
        }

        public Eumis.Domain.Contracts.ContractReportTechnicalCheck DeleteContractReportTechnicalCheck(int contractReportId, int contractReportTechnicalCheckId, byte[] version)
        {
            var technicalCheck = this.contractReportTechnicalChecksRepository.FindForUpdate(contractReportTechnicalCheckId, version);
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsUncheckedContractReport();

            if (technicalCheck.Status != ContractReportTechnicalCheckStatus.Draft)
            {
                throw new DomainException("Cannot delete a ContractReportTechnicalCheck with status different from 'Draft'");
            }

            this.contractReportTechnicalChecksRepository.Remove(technicalCheck);

            this.unitOfWork.Save();

            return technicalCheck;
        }

        public async Task<Eumis.Domain.Contracts.ContractReport> DeleteContractReportAsync(Guid contractReportGid, byte[] version, CancellationToken ct)
        {
            var contractReport = await this.contractReportsRepository.FindAsync(contractReportGid, ct);
            return await this.DeleteContractReportAsync(contractReport.ContractReportId, version, ct);
        }

        public async Task<ContractReportFinancial> DeleteContractReportFinancialAsync(Guid contractReportFinancialGid, byte[] version, CancellationToken ct)
        {
            var finance = await this.contractReportFinancialsRepository.FindAsync(contractReportFinancialGid, ct);
            return await this.DeleteContractReportFinancialAsync(finance.ContractReportId, finance.ContractReportFinancialId, version, ct);
        }

        public Eumis.Domain.Contracts.ContractReportPayment DeleteContractReportPayment(Guid contractReportPaymentGid, byte[] version)
        {
            var payment = this.contractReportPaymentsRepository.Find(contractReportPaymentGid);
            return this.DeleteContractReportPayment(payment.ContractReportId, payment.ContractReportPaymentId, version);
        }

        public async Task<ContractReportTechnical> DeleteContractReportTechnicalAsync(Guid contractReportTechnicalGid, byte[] version, CancellationToken ct)
        {
            var technical = await this.contractReportTechnicalsRepository.FindAsync(contractReportTechnicalGid, ct);
            return await this.DeleteContractReportTechnicalAsync(technical.ContractReportId, technical.ContractReportTechnicalId, version, ct);
        }

        // statuses
        public async Task<IList<string>> CanDraftContractReportAsync(Guid contractReportGid, CancellationToken ct)
        {
            var errors = new List<string>();

            var contractReport = await this.contractReportsRepository.FindAsync(contractReportGid, ct);

            if (contractReport.Status != ContractReportStatus.SentChecked)
            {
                errors.Add("Пакетът трябва да е в статус 'Приключен'.");
            }

            return errors;
        }

        public IList<string> CanEnterContractReport(int contractReportId)
        {
            var errors = new List<string>();

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            var contractReportType = contractReport.ReportType;

            var finances = this.contractReportFinancialsRepository.FindAll(contractReportId);
            var payments = this.contractReportPaymentsRepository.FindAll(contractReportId);
            var technicals = this.contractReportTechnicalsRepository.FindAll(contractReportId);
            var micros = this.contractReportMicrosRepository.FindAll(contractReportId);
            var microsType1 = micros.Where(m => m.Type == ContractReportMicroType.Type1);
            var microsType2 = micros.Where(m => m.Type == ContractReportMicroType.Type2);
            var microsType3 = micros.Where(m => m.Type == ContractReportMicroType.Type3);
            var microsType4 = micros.Where(m => m.Type == ContractReportMicroType.Type4);

            if (finances.Any() && finances.Single().Status != ContractReportFinancialStatus.Entered)
            {
                errors.Add("Финансовият отчет трябва да е в статус 'Въведен'.");
            }

            if (payments.Any() && payments.Single().Status != ContractReportPaymentStatus.Entered)
            {
                errors.Add("Искането за плащане трябва да е в статус 'Въведено'.");
            }

            if (technicals.Any() && technicals.Single().Status != ContractReportTechnicalStatus.Entered)
            {
                errors.Add("Техническият отчет трябва да бъде в статус 'Въведен'.");
            }

            if (microsType1.Any() && microsType1.Single().Status != ContractReportMicroStatus.Entered)
            {
                errors.Add("Метаданните участници (ФЕПНЛ) трябва да бъдат в статус 'Въведен'.");
            }

            if (microsType2.Any() && microsType2.Single().Status != ContractReportMicroStatus.Entered)
            {
                errors.Add("Метаданните участници (ЕСФ) трябва да бъдат в статус 'Въведен'.");
            }

            if (microsType3.Any() && microsType3.Single().Status != ContractReportMicroStatus.Entered)
            {
                errors.Add("Метаданните хранителни продукти трябва да бъдат в статус 'Въведен'.");
            }

            if (microsType4.Any() && microsType4.Single().Status != ContractReportMicroStatus.Entered)
            {
                errors.Add("Метаданните на АСП трябва да бъдат в статус 'Въведен'.");
            }

            if (contractReportType == ContractReportType.AdvancePayment && !payments.Any())
            {
                errors.Add("Трябва да има поне едно авансово искане за плащане.");
            }

            if (contractReportType == ContractReportType.Technical && !technicals.Any())
            {
                errors.Add("Трябва да има поне един технически отчет.");
            }

            if (contractReportType == ContractReportType.PaymentFinancial && (!payments.Any() || !finances.Any()))
            {
                errors.Add("Трябва да има поне един финансов отчет и едно искане за плащане.");
            }

            if (contractReportType == ContractReportType.PaymentTechnicalFinancial && (!payments.Any() || !finances.Any() || !technicals.Any()))
            {
                errors.Add("Трябва да има поне един технически отчет и един финансов отчет и едно искане за плащане.");
            }

            if (contractReport.Source == Source.AdministrativeAuthority && !contractReport.SubmitDate.HasValue)
            {
                errors.Add("Полето 'Дата на представяне' трябва да е попълнено.");
            }

            return errors;
        }

        public async Task<IList<string>> CanEnterContractReportAsync(int contractReportId, CancellationToken ct)
        {
            var errors = new List<string>();

            var contractReport = await this.contractReportsRepository.FindWithoutIncludesAsync(contractReportId, ct);
            var contractReportType = contractReport.ReportType;

            var finances = await this.contractReportFinancialsRepository.FindAllAsync(contractReportId, ct);
            var payments = await this.contractReportPaymentsRepository.FindAllAsync(contractReportId, ct);
            var technicals = await this.contractReportTechnicalsRepository.FindAllAsync(contractReportId, ct);
            var micros = await this.contractReportMicrosRepository.FindAllAsync(contractReportId, ct);
            var microsType1 = micros.Where(m => m.Type == ContractReportMicroType.Type1);
            var microsType2 = micros.Where(m => m.Type == ContractReportMicroType.Type2);
            var microsType3 = micros.Where(m => m.Type == ContractReportMicroType.Type3);
            var microsType4 = micros.Where(m => m.Type == ContractReportMicroType.Type4);

            if (finances.Any() && finances.Single().Status != ContractReportFinancialStatus.Entered)
            {
                errors.Add("Финансовият отчет трябва да е в статус 'Въведен'.");
            }

            if (payments.Any() && payments.Single().Status != ContractReportPaymentStatus.Entered)
            {
                errors.Add("Искането за плащане трябва да е в статус 'Въведено'.");
            }

            if (technicals.Any() && technicals.Single().Status != ContractReportTechnicalStatus.Entered)
            {
                errors.Add("Техническият отчет трябва да бъде в статус 'Въведен'.");
            }

            if (microsType1.Any() && microsType1.Single().Status != ContractReportMicroStatus.Entered)
            {
                errors.Add("Метаданните участници (ФЕПНЛ) трябва да бъдат в статус 'Въведен'.");
            }

            if (microsType2.Any() && microsType2.Single().Status != ContractReportMicroStatus.Entered)
            {
                errors.Add("Метаданните участници (ЕСФ) трябва да бъдат в статус 'Въведен'.");
            }

            if (microsType3.Any() && microsType3.Single().Status != ContractReportMicroStatus.Entered)
            {
                errors.Add("Метаданните хранителни продукти трябва да бъдат в статус 'Въведен'.");
            }

            if (microsType4.Any() && microsType4.Single().Status != ContractReportMicroStatus.Entered)
            {
                errors.Add("Метаданните на АСП трябва да бъдат в статус 'Въведен'.");
            }

            if (contractReportType == ContractReportType.AdvancePayment && !payments.Any())
            {
                errors.Add("Трябва да има поне едно авансово искане за плащане.");
            }

            if (contractReportType == ContractReportType.Technical && !technicals.Any())
            {
                errors.Add("Трябва да има поне един технически отчет.");
            }

            if (contractReportType == ContractReportType.PaymentFinancial && (!payments.Any() || !finances.Any()))
            {
                errors.Add("Трябва да има поне един финансов отчет и едно искане за плащане.");
            }

            if (contractReportType == ContractReportType.PaymentTechnicalFinancial && (!payments.Any() || !finances.Any() || !technicals.Any()))
            {
                errors.Add("Трябва да има поне един технически отчет и един финансов отчет и едно искане за плащане.");
            }

            if (contractReport.Source == Source.AdministrativeAuthority && !contractReport.SubmitDate.HasValue)
            {
                errors.Add("Полето 'Дата на представяне' трябва да е попълнено.");
            }

            return errors;
        }

        public async Task<IList<string>> CanEnterContractReportAsync(Guid contractReportGid, CancellationToken ct)
        {
            var contractReport = await this.contractReportsRepository.FindAsync(contractReportGid, ct);
            return await this.CanEnterContractReportAsync(contractReport.ContractReportId, ct);
        }

        public IList<string> CanAcceptContractReport(int contractReportId)
        {
            var errors = new List<string>();

            errors.AddRange(this.CanAcceptOrRefuseContractReport(contractReportId));

            if (this.contractReportFinancialCSDBudgetItemsRepository.HasDraftContractReportFinancialCSDBudgetItem(contractReportId))
            {
                errors.Add("Всички разходооправдателни документи трябва да са със статус 'Приключен'.");
            }

            if (this.contractReportIndicatorsRepository.HasDraftContractReportIndicators(contractReportId))
            {
                errors.Add("Всички верифицирани индикатори трябва да са със статус 'Приключен'.");
            }

            if (this.contractReportAdvancePaymentAmountsRepository.HasDraftContractReportAdvancePaymentAmounts(contractReportId))
            {
                errors.Add("Всички верифицирани авансови плащания трябва да са със статус 'Приключен'.");
            }

            if (this.contractReportAdvanceNVPaymentAmountsRepository.HasDraftContractReportAdvanceNVPaymentAmounts(contractReportId))
            {
                errors.Add("Всички авансови плащания трябва да са със статус 'Приключен'.");
            }

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            if (!contractReport.CheckedDate.HasValue)
            {
                errors.Add("Полето 'Дата на одобрение' трябва да е попълнено, за да приемете пакета");
            }

            return errors;
        }

        public IList<string> CanRefuseContractReport(int contractReportId)
        {
            return new List<string>();
        }

        public IList<string> CanReturnContractReportStatusToUnchecked(int contractReportId)
        {
            var errors = new List<string>();

            errors.AddRange(this.contractReportsRepository.CanReturnContractReportStatusToUnchecked(contractReportId));

            return errors;
        }

        private IList<string> CanAcceptOrRefuseContractReport(int contractReportId)
        {
            var errors = new List<string>();

            var finances = this.contractReportFinancialsRepository.FindAll(contractReportId);
            var payments = this.contractReportPaymentsRepository.FindAll(contractReportId);
            var technicals = this.contractReportTechnicalsRepository.FindAll(contractReportId);
            var micros = this.contractReportMicrosRepository.FindAll(contractReportId);
            var microsType1 = micros.Where(m => m.Type == ContractReportMicroType.Type1);
            var microsType2 = micros.Where(m => m.Type == ContractReportMicroType.Type2);
            var microsType3 = micros.Where(m => m.Type == ContractReportMicroType.Type3);
            var microsType4 = micros.Where(m => m.Type == ContractReportMicroType.Type4);

            if (finances.Any())
            {
                var financialChecks = this.contractReportFinancialChecksRepository.FindAll(contractReportId);
                if (!finances.Where(t => t.Status == ContractReportFinancialStatus.Actual).Any())
                {
                    errors.Add("Трябва да има финансов отчет със статус 'Актуален'.");
                }
                else
                {
                    var actualFinance = finances.Where(t => t.Status == ContractReportFinancialStatus.Actual).Single();
                    if (!financialChecks.Where(t => t.ContractReportFinancialId == actualFinance.ContractReportFinancialId).Any())
                    {
                        errors.Add("Трябва да има поне една актуална проверка на актуалния финансов отчет.");
                    }
                }

                if (financialChecks.Where(t => t.Status == ContractReportFinancialCheckStatus.Draft).Any())
                {
                    errors.Add("Не трябва да има проверка на финансов отчет със статус 'Чернова'.");
                }
            }

            if (payments.Any())
            {
                var paymentChecks = this.contractReportPaymentChecksRepository.FindAll(contractReportId);
                if (!payments.Where(t => t.Status == ContractReportPaymentStatus.Actual).Any())
                {
                    errors.Add("Трябва да има искане за плащане със статус 'Актуално'.");
                }
                else
                {
                    var actualPayment = payments.Where(t => t.Status == ContractReportPaymentStatus.Actual).Single();
                    if (!paymentChecks.Where(t => t.ContractReportPaymentId == actualPayment.ContractReportPaymentId && t.Status == ContractReportPaymentCheckStatus.Active).Any())
                    {
                        errors.Add("Трябва да има поне едно актуално верифицирано искане за плащане към актуалното искане за плащане.");
                    }
                }

                if (paymentChecks.Where(t => t.Status == ContractReportPaymentCheckStatus.Draft).Any())
                {
                    errors.Add("Не трябва да има верифицирано искане за плащане със статус 'Чернова'.");
                }

                var actualPaymentCheck = paymentChecks.Where(t => t.Status == ContractReportPaymentCheckStatus.Active).SingleOrDefault();

                if (actualPaymentCheck != null)
                {
                    var areAmountsEqual = true;
                    var dummyPaymentCheck = new Eumis.Domain.Contracts.ContractReportPaymentCheck(
                        actualPaymentCheck.ContractReportPaymentId,
                        actualPaymentCheck.ContractReportId,
                        actualPaymentCheck.ContractId,
                        actualPaymentCheck.OrderNum,
                        actualPaymentCheck.PaymentType);

                    this.CreateContractReportPaymentCheckAmounts(contractReportId, dummyPaymentCheck);

                    areAmountsEqual = this.AreContractReportPaymentCheckAmountsEqual(dummyPaymentCheck.ContractReportPaymentCheckAmounts, actualPaymentCheck.ContractReportPaymentCheckAmounts);

                    if (areAmountsEqual)
                    {
                        areAmountsEqual = this.AreContractReportPaymentCheckAmountsEqual(actualPaymentCheck.ContractReportPaymentCheckAmounts, dummyPaymentCheck.ContractReportPaymentCheckAmounts);
                    }

                    if (!areAmountsEqual)
                    {
                        errors.Add("Актуалното верифицирано искане за плащане съдържа невалидни верифицирани стойностни, в следствие на създаване " +
                            "на нови верифицирани РОД и/или добавяне/премахване на свързани корекции. Моля, създайте ново верифицирано искане за плащане!");
                    }
                }
            }

            if (technicals.Any())
            {
                var technicalChecks = this.contractReportTechnicalChecksRepository.FindAll(contractReportId);
                if (!technicals.Where(t => t.Status == ContractReportTechnicalStatus.Actual).Any())
                {
                    errors.Add("Трябва да има технически отчет със статус 'Актуален'.");
                }
                else
                {
                    var actualTechnical = technicals.Where(t => t.Status == ContractReportTechnicalStatus.Actual).Single();
                    if (!technicalChecks.Where(t => t.ContractReportTechnicalId == actualTechnical.ContractReportTechnicalId).Any())
                    {
                        errors.Add("Трябва да има поне една актуална проверка на актуалния технически отчет.");
                    }
                }

                if (technicalChecks.Where(t => t.Status == ContractReportTechnicalCheckStatus.Draft).Any())
                {
                    errors.Add("Не трябва да има проверка на технически отчет със статус 'Чернова'.");
                }
            }

            if (microsType1.Any())
            {
                var microChecks = this.contractReportMicroChecksRepository.FindAll(contractReportId, ContractReportMicroType.Type1);
                if (!microsType1.Where(t => t.Status == ContractReportMicroStatus.Actual).Any())
                {
                    errors.Add("Трябва да има микроданни участници (ФЕПНЛ) със статус 'Актуален'.");
                }
                else
                {
                    var actualMicro = microsType1.Where(t => t.Status == ContractReportMicroStatus.Actual).Single();
                    if (!microChecks.Where(t => t.ContractReportMicroId == actualMicro.ContractReportMicroId).Any())
                    {
                        errors.Add("Трябва да има поне една актуална проверка на актуалните микроданни участници (ФЕПНЛ).");
                    }
                }

                if (microChecks.Where(t => t.Status == ContractReportMicroCheckStatus.Draft).Any())
                {
                    errors.Add("Не трябва да има проверка на микроданни участници (ФЕПНЛ) със статус 'Чернова'.");
                }
            }

            if (microsType2.Any())
            {
                var microChecks = this.contractReportMicroChecksRepository.FindAll(contractReportId, ContractReportMicroType.Type2);
                if (!microsType2.Where(t => t.Status == ContractReportMicroStatus.Actual).Any())
                {
                    errors.Add("Трябва да има микроданни участници (ЕСФ) със статус 'Актуален'.");
                }
                else
                {
                    var actualMicro = microsType2.Where(t => t.Status == ContractReportMicroStatus.Actual).Single();
                    if (!microChecks.Where(t => t.ContractReportMicroId == actualMicro.ContractReportMicroId).Any())
                    {
                        errors.Add("Трябва да има поне една актуална проверка на актуалните микроданни участници (ЕСФ).");
                    }
                }

                if (microChecks.Where(t => t.Status == ContractReportMicroCheckStatus.Draft).Any())
                {
                    errors.Add("Не трябва да има проверка на микроданни участници (ЕСФ) със статус 'Чернова'.");
                }
            }

            if (microsType3.Any())
            {
                var microChecks = this.contractReportMicroChecksRepository.FindAll(contractReportId, ContractReportMicroType.Type3);
                if (!microsType3.Where(t => t.Status == ContractReportMicroStatus.Actual).Any())
                {
                    errors.Add("Трябва да има микроданни хранителни продукти със статус 'Актуален'.");
                }
                else
                {
                    var actualMicro = microsType3.Where(t => t.Status == ContractReportMicroStatus.Actual).Single();
                    if (!microChecks.Where(t => t.ContractReportMicroId == actualMicro.ContractReportMicroId).Any())
                    {
                        errors.Add("Трябва да има поне една актуална проверка на актуалните микроданни хранителни продукти.");
                    }
                }

                if (microChecks.Where(t => t.Status == ContractReportMicroCheckStatus.Draft).Any())
                {
                    errors.Add("Не трябва да има проверка на микроданни хранителни продукти със статус 'Чернова'.");
                }
            }

            if (microsType4.Any())
            {
                var microChecks = this.contractReportMicroChecksRepository.FindAll(contractReportId, ContractReportMicroType.Type4);
                if (!microsType4.Where(t => t.Status == ContractReportMicroStatus.Actual).Any())
                {
                    errors.Add("Трябва да има микроданни на АСП със статус 'Актуален'.");
                }
                else
                {
                    var actualMicro = microsType4.Where(t => t.Status == ContractReportMicroStatus.Actual).Single();
                    if (!microChecks.Where(t => t.ContractReportMicroId == actualMicro.ContractReportMicroId).Any())
                    {
                        errors.Add("Трябва да има поне една актуална проверка на актуалните микроданни на АСП.");
                    }
                }

                if (microChecks.Where(t => t.Status == ContractReportMicroCheckStatus.Draft).Any())
                {
                    errors.Add("Не трябва да има проверка на микроданни на АСП със статус 'Чернова'.");
                }
            }

            return errors;
        }

        private async Task<IList<string>> CanAcceptOrRefuseContractReportAsync(int contractReportId, CancellationToken ct)
        {
            var errors = new List<string>();

            var finances = await this.contractReportFinancialsRepository.FindAllAsync(contractReportId, ct);
            var payments = await this.contractReportPaymentsRepository.FindAllAsync(contractReportId, ct);
            var technicals = await this.contractReportTechnicalsRepository.FindAllAsync(contractReportId, ct);
            var micros = await this.contractReportMicrosRepository.FindAllAsync(contractReportId, ct);
            var microsType1 = micros.Where(m => m.Type == ContractReportMicroType.Type1);
            var microsType2 = micros.Where(m => m.Type == ContractReportMicroType.Type2);
            var microsType3 = micros.Where(m => m.Type == ContractReportMicroType.Type3);
            var microsType4 = micros.Where(m => m.Type == ContractReportMicroType.Type4);

            if (finances.Any())
            {
                var financialChecks = await this.contractReportFinancialChecksRepository.FindAllAsync(contractReportId, ct);
                if (!finances.Where(t => t.Status == ContractReportFinancialStatus.Actual).Any())
                {
                    errors.Add("Трябва да има финансов отчет със статус 'Актуален'.");
                }
                else
                {
                    var actualFinance = finances.Where(t => t.Status == ContractReportFinancialStatus.Actual).Single();
                    if (!financialChecks.Where(t => t.ContractReportFinancialId == actualFinance.ContractReportFinancialId).Any())
                    {
                        errors.Add("Трябва да има поне една актуална проверка на актуалния финансов отчет.");
                    }
                }

                if (financialChecks.Where(t => t.Status == ContractReportFinancialCheckStatus.Draft).Any())
                {
                    errors.Add("Не трябва да има проверка на финансов отчет със статус 'Чернова'.");
                }
            }

            if (payments.Any())
            {
                var paymentChecks = await this.contractReportPaymentChecksRepository.FindAllAsync(contractReportId, ct);
                if (!payments.Where(t => t.Status == ContractReportPaymentStatus.Actual).Any())
                {
                    errors.Add("Трябва да има искане за плащане със статус 'Актуално'.");
                }
                else
                {
                    var actualPayment = payments.Where(t => t.Status == ContractReportPaymentStatus.Actual).Single();
                    if (!paymentChecks.Where(t => t.ContractReportPaymentId == actualPayment.ContractReportPaymentId && t.Status == ContractReportPaymentCheckStatus.Active).Any())
                    {
                        errors.Add("Трябва да има поне едно актуално верифицирано искане за плащане към актуалното искане за плащане.");
                    }
                }

                if (paymentChecks.Where(t => t.Status == ContractReportPaymentCheckStatus.Draft).Any())
                {
                    errors.Add("Не трябва да има верифицирано искане за плащане със статус 'Чернова'.");
                }

                var actualPaymentCheck = paymentChecks.Where(t => t.Status == ContractReportPaymentCheckStatus.Active).SingleOrDefault();

                if (actualPaymentCheck != null)
                {
                    var areAmountsEqual = true;
                    var dummyPaymentCheck = new Eumis.Domain.Contracts.ContractReportPaymentCheck(
                        actualPaymentCheck.ContractReportPaymentId,
                        actualPaymentCheck.ContractReportId,
                        actualPaymentCheck.ContractId,
                        actualPaymentCheck.OrderNum,
                        actualPaymentCheck.PaymentType);

                    this.CreateContractReportPaymentCheckAmounts(contractReportId, dummyPaymentCheck);

                    areAmountsEqual = this.AreContractReportPaymentCheckAmountsEqual(dummyPaymentCheck.ContractReportPaymentCheckAmounts, actualPaymentCheck.ContractReportPaymentCheckAmounts);

                    if (areAmountsEqual)
                    {
                        areAmountsEqual = this.AreContractReportPaymentCheckAmountsEqual(actualPaymentCheck.ContractReportPaymentCheckAmounts, dummyPaymentCheck.ContractReportPaymentCheckAmounts);
                    }

                    if (!areAmountsEqual)
                    {
                        errors.Add("Актуалното верифицирано искане за плащане съдържа невалидни верифицирани стойностни, в следствие на създаване " +
                            "на нови верифицирани РОД и/или добавяне/премахване на свързани корекции. Моля, създайте ново верифицирано искане за плащане!");
                    }
                }
            }

            if (technicals.Any())
            {
                var technicalChecks = this.contractReportTechnicalChecksRepository.FindAll(contractReportId);
                if (!technicals.Where(t => t.Status == ContractReportTechnicalStatus.Actual).Any())
                {
                    errors.Add("Трябва да има технически отчет със статус 'Актуален'.");
                }
                else
                {
                    var actualTechnical = technicals.Where(t => t.Status == ContractReportTechnicalStatus.Actual).Single();
                    if (!technicalChecks.Where(t => t.ContractReportTechnicalId == actualTechnical.ContractReportTechnicalId).Any())
                    {
                        errors.Add("Трябва да има поне една актуална проверка на актуалния технически отчет.");
                    }
                }

                if (technicalChecks.Where(t => t.Status == ContractReportTechnicalCheckStatus.Draft).Any())
                {
                    errors.Add("Не трябва да има проверка на технически отчет със статус 'Чернова'.");
                }
            }

            if (microsType1.Any())
            {
                var microChecks = this.contractReportMicroChecksRepository.FindAll(contractReportId, ContractReportMicroType.Type1);
                if (!microsType1.Where(t => t.Status == ContractReportMicroStatus.Actual).Any())
                {
                    errors.Add("Трябва да има микроданни участници (ФЕПНЛ) със статус 'Актуален'.");
                }
                else
                {
                    var actualMicro = microsType1.Where(t => t.Status == ContractReportMicroStatus.Actual).Single();
                    if (!microChecks.Where(t => t.ContractReportMicroId == actualMicro.ContractReportMicroId).Any())
                    {
                        errors.Add("Трябва да има поне една актуална проверка на актуалните микроданни участници (ФЕПНЛ).");
                    }
                }

                if (microChecks.Where(t => t.Status == ContractReportMicroCheckStatus.Draft).Any())
                {
                    errors.Add("Не трябва да има проверка на микроданни участници (ФЕПНЛ) със статус 'Чернова'.");
                }
            }

            if (microsType2.Any())
            {
                var microChecks = this.contractReportMicroChecksRepository.FindAll(contractReportId, ContractReportMicroType.Type2);
                if (!microsType2.Where(t => t.Status == ContractReportMicroStatus.Actual).Any())
                {
                    errors.Add("Трябва да има микроданни участници (ЕСФ) със статус 'Актуален'.");
                }
                else
                {
                    var actualMicro = microsType2.Where(t => t.Status == ContractReportMicroStatus.Actual).Single();
                    if (!microChecks.Where(t => t.ContractReportMicroId == actualMicro.ContractReportMicroId).Any())
                    {
                        errors.Add("Трябва да има поне една актуална проверка на актуалните микроданни участници (ЕСФ).");
                    }
                }

                if (microChecks.Where(t => t.Status == ContractReportMicroCheckStatus.Draft).Any())
                {
                    errors.Add("Не трябва да има проверка на микроданни участници (ЕСФ) със статус 'Чернова'.");
                }
            }

            if (microsType3.Any())
            {
                var microChecks = this.contractReportMicroChecksRepository.FindAll(contractReportId, ContractReportMicroType.Type3);
                if (!microsType3.Where(t => t.Status == ContractReportMicroStatus.Actual).Any())
                {
                    errors.Add("Трябва да има микроданни хранителни продукти със статус 'Актуален'.");
                }
                else
                {
                    var actualMicro = microsType3.Where(t => t.Status == ContractReportMicroStatus.Actual).Single();
                    if (!microChecks.Where(t => t.ContractReportMicroId == actualMicro.ContractReportMicroId).Any())
                    {
                        errors.Add("Трябва да има поне една актуална проверка на актуалните микроданни хранителни продукти.");
                    }
                }

                if (microChecks.Where(t => t.Status == ContractReportMicroCheckStatus.Draft).Any())
                {
                    errors.Add("Не трябва да има проверка на микроданни хранителни продукти със статус 'Чернова'.");
                }
            }

            if (microsType4.Any())
            {
                var microChecks = this.contractReportMicroChecksRepository.FindAll(contractReportId, ContractReportMicroType.Type4);
                if (!microsType4.Where(t => t.Status == ContractReportMicroStatus.Actual).Any())
                {
                    errors.Add("Трябва да има микроданни на АСП със статус 'Актуален'.");
                }
                else
                {
                    var actualMicro = microsType4.Where(t => t.Status == ContractReportMicroStatus.Actual).Single();
                    if (!microChecks.Where(t => t.ContractReportMicroId == actualMicro.ContractReportMicroId).Any())
                    {
                        errors.Add("Трябва да има поне една актуална проверка на актуалните микроданни на АСП.");
                    }
                }

                if (microChecks.Where(t => t.Status == ContractReportMicroCheckStatus.Draft).Any())
                {
                    errors.Add("Не трябва да има проверка на микроданни на АСП със статус 'Чернова'.");
                }
            }

            return errors;
        }

        private bool AreContractReportPaymentCheckAmountsEqual(IEnumerable<ContractReportPaymentCheckAmount> firstAmounts, IEnumerable<ContractReportPaymentCheckAmount> secondAmounts)
        {
            ContractReportPaymentCheckAmount secondAmount = null;

            foreach (var firstAmount in firstAmounts)
            {
                secondAmount = secondAmounts
                    .Where(t => t.ProgrammePriorityId == firstAmount.ProgrammePriorityId)
                    .SingleOrDefault();

                if (secondAmount == null)
                {
                    return false;
                }
                else
                {
                    if (firstAmount.ApprovedEuAmount != secondAmount.ApprovedEuAmount ||
                       firstAmount.ApprovedBgAmount != secondAmount.ApprovedBgAmount ||
                       firstAmount.ApprovedBfpTotalAmount != secondAmount.ApprovedBfpTotalAmount ||
                       firstAmount.ApprovedCrossAmount != secondAmount.ApprovedCrossAmount ||
                       firstAmount.ApprovedSelfAmount != secondAmount.ApprovedSelfAmount ||
                       firstAmount.ApprovedTotalAmount != secondAmount.ApprovedTotalAmount)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public IList<string> CanChangeContractReportFinancialStatusToEntered(int contractReportId)
        {
            var errors = new List<string>();

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            if (contractReport.Status == ContractReportStatus.Draft && this.contractReportsRepository.HasContractReportInProgress(contractReport.ContractId, contractReportId))
            {
                errors.Add("Съществува друг пакет отчетни документи, който не е в статус 'Приет' или 'Отхвърлен'.");
            }
            else if (contractReport.Status == ContractReportStatus.Draft && this.contractReportsRepository.HasContractReportDraft(contractReport.ContractId, contractReportId))
            {
                errors.Add("Съществува друг пакет отчетни документи, който е в статус 'Чернова'.");
            }

            return errors;
        }

        public async Task<IList<string>> CanChangeContractReportFinancialStatusToEnteredAsync(int contractReportId, CancellationToken ct)
        {
            var errors = new List<string>();

            var contractReport = await this.contractReportsRepository.FindWithoutIncludesAsync(contractReportId, ct);

            if (contractReport.Status == ContractReportStatus.Draft && await this.contractReportsRepository.HasContractReportInProgressAsync(contractReport.ContractId, contractReportId, ct))
            {
                errors.Add("Съществува друг пакет отчетни документи, който не е в статус 'Приет' или 'Отхвърлен'.");
            }
            else if (contractReport.Status == ContractReportStatus.Draft && await this.contractReportsRepository.HasContractReportDraftAsync(contractReport.ContractId, contractReportId, ct))
            {
                errors.Add("Съществува друг пакет отчетни документи, който е в статус 'Чернова'.");
            }

            return errors;
        }

        public IList<string> CanChangeContractReportPaymentStatusToEntered(int contractReportId)
        {
            var errors = new List<string>();

            var contractReport = this.contractReportsRepository.Find(contractReportId);

            if (contractReport.Status == ContractReportStatus.Draft && this.contractReportsRepository.HasContractReportInProgress(contractReport.ContractId, contractReportId))
            {
                errors.Add("Съществува друг пакет отчетни документи, който не е в статус 'Приет' или 'Отхвърлен'.");
            }
            else if (contractReport.Status == ContractReportStatus.Draft && this.contractReportsRepository.HasContractReportDraft(contractReport.ContractId, contractReportId))
            {
                errors.Add("Съществува друг пакет отчетни документи, който е в статус 'Чернова'.");
            }

            return errors;
        }

        public IList<string> CanChangeContractReportTechnicalStatusToEntered(int contractReportId)
        {
            var errors = new List<string>();

            var contractReport = this.contractReportsRepository.Find(contractReportId);

            if (contractReport.Status == ContractReportStatus.Draft && this.contractReportsRepository.HasContractReportInProgress(contractReport.ContractId, contractReportId))
            {
                errors.Add("Съществува друг пакет отчетни документи, който не е в статус 'Приет' или 'Отхвърлен'.");
            }
            else if (contractReport.Status == ContractReportStatus.Draft && this.contractReportsRepository.HasContractReportDraft(contractReport.ContractId, contractReportId))
            {
                errors.Add("Съществува друг пакет отчетни документи, който е в статус 'Чернова'.");
            }

            return errors;
        }

        public async Task<IList<string>> CanChangeContractReportTechnicalStatusToEnteredAsync(int contractReportId, CancellationToken ct)
        {
            var errors = new List<string>();

            var contractReport = await this.contractReportsRepository.FindWithoutIncludesAsync(contractReportId, ct);

            if (contractReport.Status == ContractReportStatus.Draft && this.contractReportsRepository.HasContractReportInProgress(contractReport.ContractId, contractReportId))
            {
                errors.Add("Съществува друг пакет отчетни документи, който не е в статус 'Приет' или 'Отхвърлен'.");
            }
            else if (contractReport.Status == ContractReportStatus.Draft && this.contractReportsRepository.HasContractReportDraft(contractReport.ContractId, contractReportId))
            {
                errors.Add("Съществува друг пакет отчетни документи, който е в статус 'Чернова'.");
            }

            return errors;
        }

        public IList<string> CanChangeContractReportFinancialStatusToReturned(int contractReportId, int contractReportFinancialId)
        {
            var errors = new List<string>();

            if (this.contractReportFinancialChecksRepository.HasContractReportFinancialCheckInProgress(contractReportId))
            {
                errors.Add("Има проверка към финансовия отчет със статус 'Чернова'.");
            }

            var result = this.AreAllBudgetItemsSameStatus(contractReportId, contractReportFinancialId);
            if (result.Any())
            {
                errors.Add($"В раздел Верифицирани РОД за следните РОД има редове от бюджета с различен статус:\r\n\t{string.Join("\r\n\t", result)}");
            }

            var financialReportCSDs = this.contractReportFinancialCSDsRepository.FindAll(contractReportFinancialId);

            if (financialReportCSDs.Count > 0)
            {
                var areAllBudgetItemsEnded = this.contractReportFinancialCSDBudgetItemsRepository
                    .FindAll(contractReportId)
                    .Where(bi => bi.ContractReportFinancialId == contractReportFinancialId)
                    .All(csd => csd.Status == ContractReportFinancialCSDBudgetItemStatus.Ended);

                if (areAllBudgetItemsEnded)
                {
                    errors.Add("Отчетът не може да бъде върнат, тъй като всички РОД са приключени и не може да бъдат коригирани.");
                }
            }

            return errors;
        }

        private IList<string> AreAllBudgetItemsSameStatus(int contractReportId, int contractReportFinancialId)
        {
            var budgetItems = this.contractReportFinancialCSDBudgetItemsRepository.FindAll(contractReportId)
                .Where(bi => bi.ContractReportFinancialId == contractReportFinancialId);

            var csdIds = budgetItems.Select(bi => bi.ContractReportFinancialCSDId).Distinct();

            var result = new List<string>();

            foreach (var csdId in csdIds)
            {
                var budgetItemsByCSD = budgetItems.Where(bi => bi.ContractReportFinancialCSDId == csdId);
                var status = budgetItemsByCSD.First().Status;

                if (budgetItemsByCSD.Any(bi => bi.Status != status))
                {
                    var contractReportFinancialCSD = this.contractReportFinancialCSDsRepository.FindWithoutIncludes(csdId);

                    result.Add($"{contractReportFinancialCSD.Number}/{contractReportFinancialCSD.Date.ToString("dd.MM.yyyy")}/{contractReportFinancialCSD.CompanyName};");
                }
            }

            return result;
        }

        public IList<string> CanChangeContractReportPaymentStatusToReturned(int contractReportId)
        {
            var errors = new List<string>();

            if (this.contractReportPaymentChecksRepository.HasContractReportPaymentCheckInProgress(contractReportId))
            {
                errors.Add("Има проверка към искането за плащане със статус 'Чернова'.");
            }

            return errors;
        }

        public IList<string> CanChangeContractReportTechnicalStatusToReturned(int contractReportId)
        {
            var errors = new List<string>();

            if (this.contractReportTechnicalChecksRepository.HasContractReportTechnicalCheckInProgress(contractReportId))
            {
                errors.Add("Има проверка към техническия отчет със статус 'Чернова'.");
            }

            return errors;
        }

        public IList<string> CanChangeContractReportFinancialCheckStatusToActive(int contractReportFinancialCheckId)
        {
            var errors = new List<string>();

            var financialCheck = this.contractReportFinancialChecksRepository.Find(contractReportFinancialCheckId);
            if (!financialCheck.Approval.HasValue)
            {
                errors.Add("Полето 'Одобрение' трябва да е попълнено.");
            }

            if (!financialCheck.BlobKey.HasValue)
            {
                errors.Add("Полето 'Файл' трябва да е попълнено.");
            }

            if (!financialCheck.CheckedDate.HasValue)
            {
                errors.Add("Полето 'Дата на проверка' трябва да е попълнено.");
            }

            return errors;
        }

        public IList<string> CanChangeContractReportTechnicalCheckStatusToActive(int contractReportTechnicalCheckId)
        {
            var errors = new List<string>();

            var technicalCheck = this.contractReportTechnicalChecksRepository.Find(contractReportTechnicalCheckId);
            if (!technicalCheck.Approval.HasValue)
            {
                errors.Add("Полето 'Одобрение' трябва да е попълнено.");
            }

            if (!technicalCheck.BlobKey.HasValue)
            {
                errors.Add("Полето 'Файл' трябва да е попълнено.");
            }

            if (!technicalCheck.CheckedDate.HasValue)
            {
                errors.Add("Полето 'Дата на проверка' трябва да е попълнено.");
            }

            return errors;
        }

        public IList<string> CanChangeContractReportPaymentCheckStatusToActive(int contractReportPaymentCheckId)
        {
            var errors = new List<string>();

            var paymentCheck = this.contractReportPaymentChecksRepository.Find(contractReportPaymentCheckId);
            if (!paymentCheck.Approval.HasValue)
            {
                errors.Add("Полето 'Одобрение' трябва да е попълнено.");
            }

            if (!paymentCheck.CheckedDate.HasValue)
            {
                errors.Add("Полето 'Дата на верифициране' трябва да е попълнено.");
            }

            bool approvedHasValue = true;
            bool paidHasValue = true;

            foreach (var crpca in paymentCheck.ContractReportPaymentCheckAmounts)
            {
                if (!crpca.ApprovedEuAmount.HasValue ||
                    !crpca.ApprovedBgAmount.HasValue ||
                    !crpca.ApprovedBfpTotalAmount.HasValue ||
                    !crpca.ApprovedSelfAmount.HasValue ||
                    !crpca.ApprovedTotalAmount.HasValue)
                {
                    approvedHasValue = false;
                }

                if (!crpca.PaidEuAmount.HasValue ||
                    !crpca.PaidBgAmount.HasValue ||
                    !crpca.PaidBfpTotalAmount.HasValue)
                {
                    paidHasValue = false;
                }
            }

            if (!approvedHasValue)
            {
                errors.Add("Всички полета от секция/и 'Стойност на верифицираните средства' трябва да са попълнени.");
            }

            if (!paidHasValue)
            {
                errors.Add("Всички полета от секция/и 'Стойност на сумите за плащане' трябва да са попълнени.");
            }

            return errors;
        }

        public IList<string> CanChangeContractReportPaymentCheckStatusToArchived(int contractReportPaymentCheckId)
        {
            var errors = new List<string>();

            var paymentCheck = this.contractReportPaymentChecksRepository.Find(contractReportPaymentCheckId);

            if (this.contractReportPaymentChecksRepository.HasContractReportPaymentCheckInProgress(paymentCheck.ContractReportId))
            {
                errors.Add("Има верифицирано искане за плащане със статус 'Чернова'.");
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReport ChangeContractReportStatus(int contractReportId, byte[] version, ContractReportStatus status, string statusNote)
        {
            return this.ChangeContractReportStatus(contractReportId, version, status, statusNote, null);
        }

        private Eumis.Domain.Contracts.ContractReport ChangeContractReportStatus(int contractReportId, byte[] version, ContractReportStatus status, string statusNote, int? contractRegistrationId)
        {
            var report = this.contractReportsRepository.FindForUpdate(contractReportId, version);

            Action<ContractReportStatus> validateStatus = (s) =>
            {
                if (report.Status != s)
                {
                    throw new DomainException("ContractReport status transition not allowed");
                }
            };

            switch (status)
            {
                case ContractReportStatus.Draft:
                    if (report.Status != ContractReportStatus.Entered && report.Status != ContractReportStatus.SentChecked)
                    {
                        throw new DomainException("ContractReport status transition not allowed");
                    }

                    break;
                case ContractReportStatus.Entered:
                    validateStatus(ContractReportStatus.Draft);
                    break;
                case ContractReportStatus.SentChecked:
                    if (report.Status != ContractReportStatus.Draft && report.Status != ContractReportStatus.Entered)
                    {
                        throw new DomainException("ContractReport status transition not allowed");
                    }

                    break;
                case ContractReportStatus.Unchecked:
                    validateStatus(ContractReportStatus.SentChecked);
                    break;
                case ContractReportStatus.Accepted:
                    validateStatus(ContractReportStatus.Unchecked);
                    break;
                case ContractReportStatus.Refused:
                    validateStatus(ContractReportStatus.Unchecked);
                    break;
            }

            if (status == ContractReportStatus.Entered && this.CanEnterContractReport(contractReportId).Count != 0)
            {
                throw new DomainException("Cannot change status to 'Entered' of ContractReport");
            }

            if (status == ContractReportStatus.SentChecked && report.Source == Source.Beneficiary && this.CanEnterContractReport(contractReportId).Count != 0)
            {
                throw new DomainException("Cannot change status to 'SentChecked' of ContractReport");
            }

            if (status == ContractReportStatus.SentChecked)
            {
                var payments = this.contractReportPaymentsRepository.FindAll(contractReportId);
                var technicals = this.contractReportTechnicalsRepository.FindAll(contractReportId);
                var finances = this.contractReportFinancialsRepository.FindAll(contractReportId);

                if (report.Source == Source.Beneficiary)
                {
                    report.SubmitDate = DateTime.Now.Date;
                }

                if (payments.Any())
                {
                    var payment = payments.Single();

                    payment.SetAttachedDocumentsActivationDate();
                    payment.SubmitDate = report.SubmitDate;

                    if (contractRegistrationId.HasValue && report.Source == Source.Beneficiary)
                    {
                        payment.SenderContractRegistrationId = contractRegistrationId;
                    }
                }

                if (technicals.Any())
                {
                    var technical = technicals.Single();

                    technical.SetAttachedDocumentsActivationDate();
                    technical.SubmitDate = report.SubmitDate;

                    if (contractRegistrationId.HasValue && report.Source == Source.Beneficiary)
                    {
                        technical.SenderContractRegistrationId = contractRegistrationId;
                    }
                }

                if (finances.Any())
                {
                    var finance = finances.Single();
                    finance.SubmitDate = report.SubmitDate;

                    finance.SetAttachedDocumentsActivationDate();

                    if (contractRegistrationId.HasValue && report.Source == Source.Beneficiary)
                    {
                        finance.SenderContractRegistrationId = contractRegistrationId;
                    }

                    if (status == ContractReportStatus.SentChecked && report.ReportType == ContractReportType.Financial)
                    {
                        finance.Status = ContractReportFinancialStatus.Actual;
                    }
                }
            }

            if (status == ContractReportStatus.Unchecked)
            {
                var finances = this.contractReportFinancialsRepository.FindAll(contractReportId);
                var payments = this.contractReportPaymentsRepository.FindAll(contractReportId);
                var technicals = this.contractReportTechnicalsRepository.FindAll(contractReportId);

                if (finances.Any())
                {
                    var finance = finances.Single();
                    this.ChangeContractReportFinancialStatus(contractReportId, finance.ContractReportFinancialId, finance.Version, ContractReportFinancialStatus.Actual, null);
                }

                if (payments.Any())
                {
                    var payment = payments.Single();
                    this.ChangeContractReportPaymentStatus(contractReportId, payment.ContractReportPaymentId, payment.Version, ContractReportPaymentStatus.Actual, null);
                }

                if (technicals.Any())
                {
                    var technical = technicals.Single();
                    this.ChangeContractReportTechnicalStatus(contractReportId, technical.ContractReportTechnicalId, technical.Version, ContractReportTechnicalStatus.Actual, null);
                }
            }

            if (status == ContractReportStatus.Accepted && this.CanAcceptOrRefuseContractReport(contractReportId).Any())
            {
                throw new DomainException("Cannot change status to 'Accepted' or 'Refused' of ContractReport");
            }

            if (status == ContractReportStatus.Unchecked && !this.contractReportsRepository.CanChangeContractReportStatusToUnchecked(contractReportId))
            {
                throw new DomainException("Cannot change status to 'Unchecked' becouse previous ContractReport is not finished");
            }

            if (status == ContractReportStatus.SentChecked && report.ReportType == ContractReportType.Financial)
            {
                report.SetStatus(ContractReportStatus.Unchecked, "Автоматично въведен");
            }
            else
            {
                report.SetStatus(status, statusNote);
            }

            this.unitOfWork.Save();

            if (status == ContractReportStatus.SentChecked && report.ReportType == ContractReportType.Financial)
            {
                var newContractReportFinancialCheck = this.CreateContractReportFinancialCheck(contractReportId);
                newContractReportFinancialCheck.Approval = ContractReportFinancialCheckApproval.Approved;
                newContractReportFinancialCheck.Status = ContractReportFinancialCheckStatus.Active;
                newContractReportFinancialCheck.CheckedDate = DateTime.Now;

                report.SetStatus(ContractReportStatus.Accepted, "Автоматично одобрен");
                report.CheckedDate = DateTime.Now;
            }

            this.unitOfWork.Save();

            return report;
        }

        private async Task<Eumis.Domain.Contracts.ContractReport> ChangeContractReportStatusAsync(int contractReportId, byte[] version, ContractReportStatus status, string statusNote, int? contractRegistrationId, CancellationToken ct)
        {
            var report = await this.contractReportsRepository.FindForUpdateAsync(contractReportId, version, ct);

            Action<ContractReportStatus> validateStatus = (s) =>
            {
                if (report.Status != s)
                {
                    throw new DomainException("ContractReport status transition not allowed");
                }
            };

            switch (status)
            {
                case ContractReportStatus.Draft:
                    if (report.Status != ContractReportStatus.Entered && report.Status != ContractReportStatus.SentChecked)
                    {
                        throw new DomainException("ContractReport status transition not allowed");
                    }

                    break;
                case ContractReportStatus.Entered:
                    validateStatus(ContractReportStatus.Draft);
                    break;
                case ContractReportStatus.SentChecked:
                    if (report.Status != ContractReportStatus.Draft && report.Status != ContractReportStatus.Entered)
                    {
                        throw new DomainException("ContractReport status transition not allowed");
                    }

                    break;
                case ContractReportStatus.Unchecked:
                    validateStatus(ContractReportStatus.SentChecked);
                    break;
                case ContractReportStatus.Accepted:
                    validateStatus(ContractReportStatus.Unchecked);
                    break;
                case ContractReportStatus.Refused:
                    validateStatus(ContractReportStatus.Unchecked);
                    break;
            }

            if (status == ContractReportStatus.Entered && this.CanEnterContractReport(contractReportId).Count != 0)
            {
                throw new DomainException("Cannot change status to 'Entered' of ContractReport");
            }

            if (status == ContractReportStatus.SentChecked && report.Source == Source.Beneficiary && this.CanEnterContractReport(contractReportId).Count != 0)
            {
                throw new DomainException("Cannot change status to 'SentChecked' of ContractReport");
            }

            if (status == ContractReportStatus.SentChecked)
            {
                var payments = await this.contractReportPaymentsRepository.FindAllAsync(contractReportId, ct);
                var technicals = await this.contractReportTechnicalsRepository.FindAllAsync(contractReportId, ct);
                var finances = await this.contractReportFinancialsRepository.FindAllAsync(contractReportId, ct);

                var micros = await this.contractReportMicrosRepository.FindAllAsync(contractReportId, ct);
                var microsType1 = micros.Where(m => m.Type == ContractReportMicroType.Type1);
                var microsType2 = micros.Where(m => m.Type == ContractReportMicroType.Type2);
                var microsType3 = micros.Where(m => m.Type == ContractReportMicroType.Type3);
                var microsType4 = micros.Where(m => m.Type == ContractReportMicroType.Type4);

                if (report.Source == Source.Beneficiary)
                {
                    report.SubmitDate = DateTime.Now.Date;
                }

                if (payments.Any())
                {
                    var payment = payments.Single();

                    payment.SetAttachedDocumentsActivationDate();
                    payment.SubmitDate = report.SubmitDate;

                    if (contractRegistrationId.HasValue && report.Source == Source.Beneficiary)
                    {
                        payment.SenderContractRegistrationId = contractRegistrationId;
                    }
                }

                if (technicals.Any())
                {
                    var technical = technicals.Single();

                    technical.SetAttachedDocumentsActivationDate();
                    technical.SubmitDate = report.SubmitDate;

                    if (contractRegistrationId.HasValue && report.Source == Source.Beneficiary)
                    {
                        technical.SenderContractRegistrationId = contractRegistrationId;
                    }
                }

                if (finances.Any())
                {
                    var finance = finances.Single();
                    finance.SubmitDate = report.SubmitDate;

                    finance.SetAttachedDocumentsActivationDate();

                    if (contractRegistrationId.HasValue && report.Source == Source.Beneficiary)
                    {
                        finance.SenderContractRegistrationId = contractRegistrationId;
                    }
                }

                if (microsType1.Any())
                {
                    var mt1 = microsType1.Single();
                    if (contractRegistrationId.HasValue && report.Source == Source.Beneficiary)
                    {
                        mt1.SenderContractRegistrationId = contractRegistrationId;
                    }
                }

                if (microsType2.Any())
                {
                    var mt2 = microsType2.Single();
                    if (contractRegistrationId.HasValue && report.Source == Source.Beneficiary)
                    {
                        mt2.SenderContractRegistrationId = contractRegistrationId;
                    }
                }

                if (microsType3.Any())
                {
                    var mt3 = microsType3.Single();
                    if (contractRegistrationId.HasValue && report.Source == Source.Beneficiary)
                    {
                        mt3.SenderContractRegistrationId = contractRegistrationId;
                    }
                }

                if (microsType4.Any())
                {
                    var mt4 = microsType4.Single();
                    if (contractRegistrationId.HasValue && report.Source == Source.Beneficiary)
                    {
                        mt4.SenderContractRegistrationId = contractRegistrationId;
                    }
                }
            }

            if (status == ContractReportStatus.Unchecked)
            {
                var finances = await this.contractReportFinancialsRepository.FindAllAsync(contractReportId, ct);
                var payments = await this.contractReportPaymentsRepository.FindAllAsync(contractReportId, ct);
                var technicals = await this.contractReportTechnicalsRepository.FindAllAsync(contractReportId, ct);
                var micros = await this.contractReportMicrosRepository.FindAllAsync(contractReportId, ct);

                if (finances.Any())
                {
                    var finance = finances.Single();
                    await this.ChangeContractReportFinancialStatusAsync(contractReportId, finance.ContractReportFinancialId, finance.Version, ContractReportFinancialStatus.Actual, null, ct);
                }

                if (payments.Any())
                {
                    var payment = payments.Single();
                    this.ChangeContractReportPaymentStatus(contractReportId, payment.ContractReportPaymentId, payment.Version, ContractReportPaymentStatus.Actual, null);
                }

                if (technicals.Any())
                {
                    var technical = technicals.Single();
                    await this.ChangeContractReportTechnicalStatusAsync(contractReportId, technical.ContractReportTechnicalId, technical.Version, ContractReportTechnicalStatus.Actual, null, ct);
                }

                if (micros.Any())
                {
                    var microType1 = micros.SingleOrDefault(m => m.Type == ContractReportMicroType.Type1);
                    if (microType1 != null)
                    {
                        await this.contractReportMicroService.ChangeContractReportMicroStatusAsync(microType1, ContractReportMicroStatus.Actual, (int?)null, ct);
                    }

                    var microType2 = micros.SingleOrDefault(m => m.Type == ContractReportMicroType.Type2);
                    if (microType2 != null)
                    {
                        await this.contractReportMicroService.ChangeContractReportMicroStatusAsync(microType2, ContractReportMicroStatus.Actual, (int?)null, ct);
                    }

                    var microType3 = micros.SingleOrDefault(m => m.Type == ContractReportMicroType.Type3);
                    if (microType3 != null)
                    {
                        await this.contractReportMicroService.ChangeContractReportMicroStatusAsync(microType3, ContractReportMicroStatus.Actual, (int?)null, ct);
                    }

                    var microType4 = micros.SingleOrDefault(m => m.Type == ContractReportMicroType.Type4);
                    if (microType4 != null)
                    {
                        await this.contractReportMicroService.ChangeContractReportMicroStatusAsync(microType4, ContractReportMicroStatus.Actual, (int?)null, ct);
                    }
                }
            }

            if (status == ContractReportStatus.Accepted && this.CanAcceptOrRefuseContractReport(contractReportId).Any())
            {
                throw new DomainException("Cannot change status to 'Accepted' or 'Refused' of ContractReport");
            }

            if (status == ContractReportStatus.Unchecked && !this.contractReportsRepository.CanChangeContractReportStatusToUnchecked(contractReportId))
            {
                throw new DomainException("Cannot change status to 'Unchecked' becouse previous ContractReport is not finished");
            }

            report.SetStatus(status, statusNote);

            await this.unitOfWork.SaveAsync(ct);

            return report;
        }

        public Eumis.Domain.Contracts.ContractReport ReturnContractReportStatusToUnchecked(int contractReportId, byte[] version)
        {
            if (this.CanReturnContractReportStatusToUnchecked(contractReportId).Any())
            {
                throw new DomainException("Cannot change status to 'Unchecked' of ContractReport");
            }

            var report = this.contractReportsRepository.FindForUpdate(contractReportId, version);

            if (report.Status != ContractReportStatus.Accepted && report.Status != ContractReportStatus.Refused)
            {
                throw new DomainException("ContractReport status transition not allowed");
            }

            if (report.Status == ContractReportStatus.Refused)
            {
                report.StatusNote = null;
            }

            report.SetStatus(ContractReportStatus.Unchecked);

            this.unitOfWork.Save();

            return report;
        }

        public Eumis.Domain.Contracts.ContractReportFinancial ChangeContractReportFinancialStatus(int contractReportId, int contractReportFinancialId, byte[] version, ContractReportFinancialStatus status, int? contractRegistrationId)
        {
            var finance = this.contractReportFinancialsRepository.FindForUpdate(contractReportFinancialId, version);
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsDraftOrSentCheckedOrUncheckedContractReport();

            switch (status)
            {
                case ContractReportFinancialStatus.Draft:
                    if (finance.Status != ContractReportFinancialStatus.Entered)
                    {
                        throw new DomainException("ContractReportFinancial status transition not allowed");
                    }

                    break;
                case ContractReportFinancialStatus.Entered:
                    if (finance.Status != ContractReportFinancialStatus.Draft && finance.Status != ContractReportFinancialStatus.Actual)
                    {
                        throw new DomainException("ContractReportFinancial status transition not allowed");
                    }

                    break;
                case ContractReportFinancialStatus.Actual:
                    if (finance.Status != ContractReportFinancialStatus.Draft && finance.Status != ContractReportFinancialStatus.Entered)
                    {
                        throw new DomainException("ContractReportFinancial status transition not allowed");
                    }

                    this.contractReportFinancialCSDService.CreateContractReportFinancialCSDAndBudgetItems(finance);

                    if (!finance.SubmitDate.HasValue)
                    {
                        finance.SubmitDate = DateTime.Now.Date;
                    }

                    if (contractRegistrationId.HasValue)
                    {
                        finance.SenderContractRegistrationId = contractRegistrationId.Value;
                    }

                    break;
            }

            if (status == ContractReportFinancialStatus.Entered && this.CanChangeContractReportFinancialStatusToEntered(contractReportId).Any())
            {
                throw new DomainException("ContractReportFinancial status transition not allowed");
            }

            finance.ChangeStatus(status);

            this.unitOfWork.Save();

            return finance;
        }

        public async Task<ContractReportFinancial> ChangeContractReportFinancialStatusAsync(
            int contractReportId,
            int contractReportFinancialId,
            byte[] version,
            ContractReportFinancialStatus status,
            int? contractRegistrationId,
            CancellationToken ct)
        {
            var finance = await this.contractReportFinancialsRepository.FindForUpdateAsync(contractReportFinancialId, version, ct);
            var contractReport = await this.contractReportsRepository.FindWithoutIncludesAsync(contractReportId, ct);

            contractReport.AssertIsDraftOrSentCheckedOrUncheckedContractReport();

            switch (status)
            {
                case ContractReportFinancialStatus.Draft:
                    if (finance.Status != ContractReportFinancialStatus.Entered)
                    {
                        throw new DomainException("ContractReportFinancial status transition not allowed");
                    }

                    break;
                case ContractReportFinancialStatus.Entered:
                    if (finance.Status != ContractReportFinancialStatus.Draft && finance.Status != ContractReportFinancialStatus.Actual)
                    {
                        throw new DomainException("ContractReportFinancial status transition not allowed");
                    }

                    break;
                case ContractReportFinancialStatus.Actual:
                    if (finance.Status != ContractReportFinancialStatus.Draft && finance.Status != ContractReportFinancialStatus.Entered)
                    {
                        throw new DomainException("ContractReportFinancial status transition not allowed");
                    }

                    this.contractReportFinancialCSDService.CreateContractReportFinancialCSDAndBudgetItems(finance);

                    if (!finance.SubmitDate.HasValue)
                    {
                        finance.SubmitDate = DateTime.Now.Date;
                    }

                    if (contractRegistrationId.HasValue)
                    {
                        finance.SenderContractRegistrationId = contractRegistrationId.Value;
                    }

                    break;
            }

            if (status == ContractReportFinancialStatus.Entered && this.CanChangeContractReportFinancialStatusToEntered(contractReportId).Any())
            {
                throw new DomainException("ContractReportFinancial status transition not allowed");
            }

            finance.ChangeStatus(status);

            await this.unitOfWork.SaveAsync(ct);

            return finance;
        }

        public Eumis.Domain.Contracts.ContractReportPayment ChangeContractReportPaymentStatus(int contractReportId, int contractReportPaymentId, byte[] version, ContractReportPaymentStatus status, int? contractRegistrationId)
        {
            var payment = this.contractReportPaymentsRepository.FindForUpdate(contractReportPaymentId, version);
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsDraftOrSentCheckedOrUncheckedContractReport();

            switch (status)
            {
                case ContractReportPaymentStatus.Draft:
                    if (payment.Status != ContractReportPaymentStatus.Entered)
                    {
                        throw new DomainException("ContractReportPayment status transition not allowed");
                    }

                    break;
                case ContractReportPaymentStatus.Entered:
                    if (payment.Status != ContractReportPaymentStatus.Draft && payment.Status != ContractReportPaymentStatus.Actual)
                    {
                        throw new DomainException("ContractReportPayment status transition not allowed");
                    }

                    break;
                case ContractReportPaymentStatus.Actual:
                    if (payment.Status != ContractReportPaymentStatus.Draft && payment.Status != ContractReportPaymentStatus.Entered)
                    {
                        throw new DomainException("ContractReportPayment status transition not allowed");
                    }

                    if (contractReport.ReportType == ContractReportType.AdvancePayment && payment.PaymentType.Value == ContractReportPaymentType.AdvanceVerification)
                    {
                        this.contractReportAdvancePaymentAmountService.CreateContractReportAdvancePaymentAmounts(payment);
                    }

                    if (contractReport.ReportType == ContractReportType.AdvancePayment && payment.PaymentType.Value == ContractReportPaymentType.Advance)
                    {
                        this.contractReportAdvanceNVPaymentAmountService.CreateContractReportAdvanceNVPaymentAmounts(payment);
                    }

                    if (!payment.SubmitDate.HasValue)
                    {
                        payment.SubmitDate = DateTime.Now.Date;
                    }

                    if (contractRegistrationId.HasValue)
                    {
                        payment.SenderContractRegistrationId = contractRegistrationId.Value;
                    }

                    break;
            }

            if (status == ContractReportPaymentStatus.Entered && this.CanChangeContractReportPaymentStatusToEntered(contractReportId).Any())
            {
                throw new DomainException("ContractReportPayment status transition not allowed");
            }

            payment.ChangeStatus(status);

            this.unitOfWork.Save();

            return payment;
        }

        public Eumis.Domain.Contracts.ContractReportTechnical ChangeContractReportTechnicalStatus(int contractReportId, int contractReportTechnicalId, byte[] version, ContractReportTechnicalStatus status, int? contractRegistrationId)
        {
            var technical = this.contractReportTechnicalsRepository.FindForUpdate(contractReportTechnicalId, version);
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsDraftOrSentCheckedOrUncheckedContractReport();

            switch (status)
            {
                case ContractReportTechnicalStatus.Draft:
                    if (technical.Status != ContractReportTechnicalStatus.Entered)
                    {
                        throw new DomainException("ContractReportTechnical status transition not allowed");
                    }

                    break;
                case ContractReportTechnicalStatus.Entered:
                    if (technical.Status != ContractReportTechnicalStatus.Draft && technical.Status != ContractReportTechnicalStatus.Actual)
                    {
                        throw new DomainException("ContractReportTechnical status transition not allowed");
                    }

                    break;
                case ContractReportTechnicalStatus.Actual:
                    if (technical.Status != ContractReportTechnicalStatus.Draft && technical.Status != ContractReportTechnicalStatus.Entered)
                    {
                        throw new DomainException("ContractReportTechnical status transition not allowed");
                    }

                    this.CreateContractReportTechnicalDetails(technical);

                    if (contractRegistrationId.HasValue)
                    {
                        technical.SenderContractRegistrationId = contractRegistrationId.Value;
                    }

                    break;
            }

            if (status == ContractReportTechnicalStatus.Entered && this.CanChangeContractReportTechnicalStatusToEntered(contractReportId).Any())
            {
                throw new DomainException("ContractReportTechnical status transition not allowed");
            }

            technical.ChangeStatus(status);

            this.unitOfWork.Save();

            return technical;
        }

        public async Task<ContractReportTechnical> ChangeContractReportTechnicalStatusAsync(
            int contractReportId,
            int contractReportTechnicalId,
            byte[] version,
            ContractReportTechnicalStatus status,
            int? contractRegistrationId,
            CancellationToken ct)
        {
            var technical = await this.contractReportTechnicalsRepository.FindForUpdateAsync(contractReportTechnicalId, version, ct);
            var contractReport = await this.contractReportsRepository.FindWithoutIncludesAsync(contractReportId, ct);

            contractReport.AssertIsDraftOrSentCheckedOrUncheckedContractReport();

            switch (status)
            {
                case ContractReportTechnicalStatus.Draft:
                    if (technical.Status != ContractReportTechnicalStatus.Entered)
                    {
                        throw new DomainException("ContractReportTechnical status transition not allowed");
                    }

                    break;
                case ContractReportTechnicalStatus.Entered:
                    if (technical.Status != ContractReportTechnicalStatus.Draft && technical.Status != ContractReportTechnicalStatus.Actual)
                    {
                        throw new DomainException("ContractReportTechnical status transition not allowed");
                    }

                    break;
                case ContractReportTechnicalStatus.Actual:
                    if (technical.Status != ContractReportTechnicalStatus.Draft && technical.Status != ContractReportTechnicalStatus.Entered)
                    {
                        throw new DomainException("ContractReportTechnical status transition not allowed");
                    }

                    await this.CreateContractReportTechnicalDetailsAsync(technical, ct);

                    if (contractRegistrationId.HasValue)
                    {
                        technical.SenderContractRegistrationId = contractRegistrationId.Value;
                    }

                    break;
            }

            if (status == ContractReportTechnicalStatus.Entered && this.CanChangeContractReportTechnicalStatusToEntered(contractReportId).Any())
            {
                throw new DomainException("ContractReportTechnical status transition not allowed");
            }

            technical.ChangeStatus(status);

            await this.unitOfWork.SaveAsync(ct);

            return technical;
        }

        private void CreateContractReportTechnicalDetails(ContractReportTechnical technicalReport)
        {
            this.contractReportIndicatorService.CreateContractReportIndicators(technicalReport);
            this.contractReportTechnicalMemberService.CreateContractReportTechnicalMembers(technicalReport);

            if (!technicalReport.SubmitDate.HasValue)
            {
                technicalReport.SubmitDate = DateTime.Now.Date;
            }
        }

        private async Task CreateContractReportTechnicalDetailsAsync(ContractReportTechnical technicalReport, CancellationToken ct)
        {
            await this.contractReportIndicatorService.CreateContractReportIndicatorsAsync(technicalReport, ct);
            this.contractReportTechnicalMemberService.CreateContractReportTechnicalMembers(technicalReport);

            if (!technicalReport.SubmitDate.HasValue)
            {
                technicalReport.SubmitDate = DateTime.Now.Date;
            }
        }

        private void DeleteContractReportTechnicalDetails(ContractReportTechnical technicalReport)
        {
            this.contractReportIndicatorService.DeleteContractReportIndicatorsInDraft(technicalReport);
            this.contractReportTechnicalMemberService.DeleteContractReportTechnicalMembers(technicalReport);
            technicalReport.SubmitDate = null;
        }

        public Eumis.Domain.Contracts.ContractReportFinancial ChangeContractReportFinancialStatusToReturned(int contractReportId, int contractReportFinancialId, byte[] version, string statusNote)
        {
            var oldFinancial = this.contractReportFinancialsRepository.FindForUpdate(contractReportFinancialId, version);
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsUncheckedContractReport();

            if (oldFinancial.Status != ContractReportFinancialStatus.Actual)
            {
                throw new DomainException("ContractReportFinancial status transition not allowed");
            }

            if (this.CanChangeContractReportFinancialStatusToReturned(contractReportId, contractReportFinancialId).Any())
            {
                throw new DomainException("Cannot change ContractReportFinancial status to 'Returned'");
            }

            oldFinancial.ChangeStatus(ContractReportFinancialStatus.Returned, statusNote);

            this.contractReportFinancialCSDService.DeleteContractReportFinancialCSDAndBudgetItemsInDraft(oldFinancial);

            var newContractReportFinancial = new Eumis.Domain.Contracts.ContractReportFinancial(
                contractReport.ContractId,
                contractReportId,
                this.contractReportFinancialsRepository.GetNextVersionSubNum(contractReportId),
                oldFinancial);

            this.contractReportFinancialsRepository.Add(newContractReportFinancial);

            this.unitOfWork.Save();

            this.contractReportFinancialCSDService.UpdateContractReportFinancialEndedCSDs(oldFinancial.ContractReportFinancialId, newContractReportFinancial.ContractReportFinancialId, contractReportId);

            return newContractReportFinancial;
        }

        public Eumis.Domain.Contracts.ContractReportPayment ChangeContractReportPaymentStatusToReturned(int contractReportId, int contractReportPaymentId, byte[] version, string statusNote)
        {
            var oldPayment = this.contractReportPaymentsRepository.FindForUpdate(contractReportPaymentId, version);
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsUncheckedContractReport();

            if (oldPayment.Status != ContractReportPaymentStatus.Actual)
            {
                throw new DomainException("ContractReportPayment status transition not allowed");
            }

            if (this.CanChangeContractReportPaymentStatusToReturned(contractReportId).Any())
            {
                throw new DomainException("Cannot change ContractReportPayment status to 'Returned'");
            }

            oldPayment.ChangeStatus(ContractReportPaymentStatus.Returned, statusNote);

            if (contractReport.ReportType == ContractReportType.AdvancePayment && oldPayment.PaymentType.Value == ContractReportPaymentType.AdvanceVerification)
            {
                this.contractReportAdvancePaymentAmountService.DeleteContractReportAdvancePaymentAmounts(oldPayment);
            }

            if (contractReport.ReportType == ContractReportType.AdvancePayment && oldPayment.PaymentType.Value == ContractReportPaymentType.Advance)
            {
                this.contractReportAdvanceNVPaymentAmountService.DeleteContractReportAdvanceNVPaymentAmounts(oldPayment);
            }

            var newContractReportPayment = new Eumis.Domain.Contracts.ContractReportPayment(
                contractReport.ContractId,
                contractReportId,
                this.contractReportPaymentsRepository.GetNextVersionSubNum(contractReportId),
                oldPayment);

            this.contractReportPaymentsRepository.Add(newContractReportPayment);

            this.unitOfWork.Save();

            return newContractReportPayment;
        }

        public Eumis.Domain.Contracts.ContractReportTechnical ChangeContractReportTechnicalStatusToReturned(int contractReportId, int contractReportTechnicalId, byte[] version, string statusNote)
        {
            var oldTechnical = this.contractReportTechnicalsRepository.FindForUpdate(contractReportTechnicalId, version);
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsUncheckedContractReport();

            if (oldTechnical.Status != ContractReportTechnicalStatus.Actual)
            {
                throw new DomainException("ContractReportTechnical status transition not allowed");
            }

            oldTechnical.ChangeStatus(ContractReportTechnicalStatus.Returned, statusNote);

            this.contractReportIndicatorService.DeleteContractReportIndicatorsInDraft(oldTechnical);
            this.contractReportTechnicalMemberService.DeleteContractReportTechnicalMembers(oldTechnical);

            var newContractReportTechnical = new Eumis.Domain.Contracts.ContractReportTechnical(
                contractReport.ContractId,
                contractReportId,
                this.contractReportTechnicalsRepository.GetNextVersionSubNum(contractReportId),
                oldTechnical);

            this.contractReportTechnicalsRepository.Add(newContractReportTechnical);

            this.unitOfWork.Save();

            this.contractReportIndicatorService.UpdateContractReportEndedIndicators(oldTechnical.ContractReportId, oldTechnical.ContractReportTechnicalId, newContractReportTechnical.ContractReportTechnicalId);

            return newContractReportTechnical;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialCheck ChangeContractReportFinancialCheckStatus(int contractReportId, int contractReportFinancialCheckId, byte[] version, ContractReportFinancialCheckStatus status)
        {
            var financialCheck = this.contractReportFinancialChecksRepository.FindForUpdate(contractReportFinancialCheckId, version);
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsUncheckedContractReport();

            if (status == ContractReportFinancialCheckStatus.Active)
            {
                if (this.CanChangeContractReportFinancialCheckStatusToActive(contractReportFinancialCheckId).Any())
                {
                    throw new DomainException("Cannot change ContractReportFinancialCheck status to 'Active'");
                }

                var actualFinancialCheck = this.contractReportFinancialChecksRepository.GetActualContractReportFinancialCheck(contractReportId);

                if (actualFinancialCheck != null)
                {
                    actualFinancialCheck.Status = ContractReportFinancialCheckStatus.Archived;
                }

                financialCheck.CheckedByUserId = this.accessContext.UserId;
            }

            financialCheck.Status = status;
            financialCheck.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return financialCheck;
        }

        public Eumis.Domain.Contracts.ContractReportPaymentCheck ChangeContractReportPaymentCheckStatus(int contractReportId, int contractReportPaymentCheckId, byte[] version, ContractReportPaymentCheckStatus status)
        {
            var paymentCheck = this.contractReportPaymentChecksRepository.FindForUpdate(contractReportPaymentCheckId, version);
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsUncheckedContractReport();

            if (status == ContractReportPaymentCheckStatus.Active)
            {
                if (paymentCheck.Status != ContractReportPaymentCheckStatus.Draft)
                {
                    throw new DomainException("Cannot change ContractReportPaymentCheck status to 'Active'");
                }

                if (this.CanChangeContractReportPaymentCheckStatusToActive(contractReportPaymentCheckId).Any())
                {
                    throw new DomainException("Cannot change ContractReportPaymentCheck status to 'Active'");
                }

                var actualPaymentCheck = this.contractReportPaymentChecksRepository.GetActualContractReportPaymentCheck(contractReportId);

                if (actualPaymentCheck != null)
                {
                    actualPaymentCheck.Status = ContractReportPaymentCheckStatus.Archived;
                }

                paymentCheck.CheckedByUserId = this.accessContext.UserId;
                contractReport.CheckedDate = paymentCheck.CheckedDate;
            }

            if (status == ContractReportPaymentCheckStatus.Archived)
            {
                if (paymentCheck.Status != ContractReportPaymentCheckStatus.Active)
                {
                    throw new DomainException("Cannot change ContractReportPaymentCheck status to 'Archived'");
                }

                if (this.CanChangeContractReportPaymentCheckStatusToArchived(contractReportPaymentCheckId).Any())
                {
                    throw new DomainException("Cannot change ContractReportPaymentCheck status to 'Archived'");
                }
            }

            paymentCheck.Status = status;
            paymentCheck.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return paymentCheck;
        }

        public Eumis.Domain.Contracts.ContractReportTechnicalCheck ChangeContractReportTechnicalCheckStatus(int contractReportId, int contractReportTechnicalCheckId, byte[] version, ContractReportTechnicalCheckStatus status)
        {
            var technicalCheck = this.contractReportTechnicalChecksRepository.FindForUpdate(contractReportTechnicalCheckId, version);
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsUncheckedContractReport();

            if (status == ContractReportTechnicalCheckStatus.Active)
            {
                if (this.CanChangeContractReportTechnicalCheckStatusToActive(contractReportTechnicalCheckId).Any())
                {
                    throw new DomainException("Cannot change ContractReportTechnicalCheck status to 'Active'");
                }

                var actualTechnicalCheck = this.contractReportTechnicalChecksRepository.GetActualContractReportTechnicalCheck(contractReportId);

                if (actualTechnicalCheck != null)
                {
                    actualTechnicalCheck.Status = ContractReportTechnicalCheckStatus.Archived;
                }

                technicalCheck.CheckedByUserId = this.accessContext.UserId;
            }

            technicalCheck.Status = status;
            technicalCheck.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return technicalCheck;
        }

        public async Task<Eumis.Domain.Contracts.ContractReport> ChangeContractReportStatusAsync(Guid contractReportGid, byte[] version, ContractReportStatus status, int? contractRegistrationId, CancellationToken ct)
        {
            var contractReport = await this.contractReportsRepository.FindAsync(contractReportGid, ct);
            return this.ChangeContractReportStatus(contractReport.ContractReportId, version, status, null, contractRegistrationId);
        }

        public async Task<ContractReportFinancial> ChangeContractReportFinancialStatusAsync(Guid contractReportFinancialGid, byte[] version, ContractReportFinancialStatus status, int? contractRegistrationId, CancellationToken ct)
        {
            var financial = await this.contractReportFinancialsRepository.FindAsync(contractReportFinancialGid, ct);

            if (status == ContractReportFinancialStatus.Actual)
            {
                financial.SetAttachedDocumentsActivationDate();
            }

            var contractReportFinancial = await this.ChangeContractReportFinancialStatusAsync(financial.ContractReportId, financial.ContractReportFinancialId, version, status, contractRegistrationId, ct);

            return contractReportFinancial;
        }

        public Eumis.Domain.Contracts.ContractReportPayment ChangeContractReportPaymentStatus(Guid contractReportPaymentGid, byte[] version, ContractReportPaymentStatus status, int? contractRegistrationId)
        {
            var payment = this.contractReportPaymentsRepository.Find(contractReportPaymentGid);

            if (status == ContractReportPaymentStatus.Actual)
            {
                payment.SetAttachedDocumentsActivationDate();
            }

            return this.ChangeContractReportPaymentStatus(payment.ContractReportId, payment.ContractReportPaymentId, version, status, contractRegistrationId);
        }

        public async Task<ContractReportTechnical> ChangeContractReportTechnicalStatusAsync(Guid contractReportTechnicalGid, byte[] version, ContractReportTechnicalStatus status, int? contractRegistrationId, CancellationToken ct)
        {
            var technical = await this.contractReportTechnicalsRepository.FindAsync(contractReportTechnicalGid, ct);

            if (status == ContractReportTechnicalStatus.Actual)
            {
                technical.SetAttachedDocumentsActivationDate();
            }

            return await this.ChangeContractReportTechnicalStatusAsync(technical.ContractReportId, technical.ContractReportTechnicalId, version, status, contractRegistrationId, ct);
        }

        public IList<string> CanAttachContractReportFinancialCorrection(int contractReportId)
        {
            var errors = new List<string>();

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            if (contractReport.ReportType != ContractReportType.PaymentFinancial && contractReport.ReportType != ContractReportType.PaymentTechnicalFinancial)
            {
                errors.Add("Можете да добавите свързана корекция, само ако пакетът съдържа финансов отчет");
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportAttachedFinancialCorrection AttachContractReportFinancialCorrection(int contractReportId, int contractReportFinancialCorrectionId, byte[] version)
        {
            var contractReport = this.contractReportsRepository.FindForUpdate(contractReportId, version);

            contractReport.AssertIsUncheckedContractReport();

            var attachedCorrection = new ContractReportAttachedFinancialCorrection()
            {
                ContractReportId = contractReportId,
                ContractReportFinancialCorrectionId = contractReportFinancialCorrectionId,
                ContractId = contractReport.ContractId,
            };

            contractReport.ContractReportAttachedFinancialCorrections.Add(attachedCorrection);
            contractReport.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return attachedCorrection;
        }

        public Eumis.Domain.Contracts.ContractReportAttachedFinancialCorrection DetachContractReportFinancialCorrection(int contractReportId, int contractReportFinancialCorrectionId, byte[] version)
        {
            var contractReport = this.contractReportsRepository.FindForUpdate(contractReportId, version);

            contractReport.AssertIsUncheckedContractReport();

            var attachedCorrection = contractReport.ContractReportAttachedFinancialCorrections
                .Where(t => t.ContractReportFinancialCorrectionId == contractReportFinancialCorrectionId)
                .Single();

            contractReport.ContractReportAttachedFinancialCorrections.Remove(attachedCorrection);
            contractReport.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return attachedCorrection;
        }

        #region Private

        private void AssertIsDraftContractReportTechnical(ContractReportTechnicalStatus status)
        {
            if (status != ContractReportTechnicalStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportTechnical when not in 'Draft' status");
            }
        }

        private void AssertIsDraftContractReportPayment(ContractReportPaymentStatus status)
        {
            if (status != ContractReportPaymentStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportPayment when not in 'Draft' status");
            }
        }

        private void AssertIsDraftContractReportFinancial(ContractReportFinancialStatus status)
        {
            if (status != ContractReportFinancialStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportFinancial when not in 'Draft' status");
            }
        }

        private void AssertIsDraftContractReportFinancialCheck(ContractReportFinancialCheckStatus status)
        {
            if (status != ContractReportFinancialCheckStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportFinancialCheck when not in 'Draft' status");
            }
        }

        private void AssertIsDraftContractReportTechnicalCheck(ContractReportTechnicalCheckStatus status)
        {
            if (status != ContractReportTechnicalCheckStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportTechnicalCheck when not in 'Draft' status");
            }
        }

        private void AssertIsDraftContractReportPaymentCheck(ContractReportPaymentCheckStatus status)
        {
            if (status != ContractReportPaymentCheckStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportPaymentCheck when not in 'Draft' status");
            }
        }

        private async Task CopyContractReportPaymentAsync(Domain.Contracts.ContractReport oldContractReport, Domain.Contracts.ContractReport newContractReport, CancellationToken ct)
        {
            var allPayments = await this.contractReportPaymentsRepository.FindAllAsync(oldContractReport.ContractReportId, ct);

            var paymentForCopy = allPayments.Where(x => x.Status == ContractReportPaymentStatus.Actual || x.Status == ContractReportPaymentStatus.Entered).FirstOrDefault();

            if (paymentForCopy != null)
            {
                await this.CopyContractReportPaymentAsync(newContractReport, paymentForCopy, ct);
            }
        }

        private async Task CopyContractReportTechnicalAsync(Domain.Contracts.ContractReport oldContractReport, Domain.Contracts.ContractReport newContractReport, CancellationToken ct)
        {
            var allTechnicals = await this.contractReportTechnicalsRepository.FindAllAsync(oldContractReport.ContractReportId, ct);

            var technicalForCopy = allTechnicals.Where(x => x.Status == ContractReportTechnicalStatus.Actual || x.Status == ContractReportTechnicalStatus.Entered).FirstOrDefault();

            if (technicalForCopy != null)
            {
                await this.CopyContractReportTechnicalAsync(newContractReport, technicalForCopy, ct);
            }
        }

        private async Task CopyContractReportFinancialAsync(Domain.Contracts.ContractReport oldContractReport, Domain.Contracts.ContractReport newContractReport, CancellationToken ct)
        {
            var allFinancials = await this.contractReportFinancialsRepository.FindAllAsync(oldContractReport.ContractReportId, ct);

            var financialForCopy = allFinancials.Where(x => x.Status == ContractReportFinancialStatus.Actual || x.Status == ContractReportFinancialStatus.Entered).FirstOrDefault();

            if (financialForCopy != null)
            {
                await this.CopyContractReportFinancialAsync(newContractReport, financialForCopy, ct);
            }
        }

        private async Task CopyContractReportMicrodataAsync(Domain.Contracts.ContractReport oldContractReport, Domain.Contracts.ContractReport newContractReport, CancellationToken ct)
        {
            var oldMicros = await this.contractReportMicrosRepository.FindAllAsync(oldContractReport.ContractReportId, ct);

            var microdataType1 = oldMicros.Where(x => x.Type == ContractReportMicroType.Type1 && (x.Status == ContractReportMicroStatus.Actual || x.Status == ContractReportMicroStatus.Entered)).FirstOrDefault();
            var microdataType2 = oldMicros.Where(x => x.Type == ContractReportMicroType.Type2 && (x.Status == ContractReportMicroStatus.Actual || x.Status == ContractReportMicroStatus.Entered)).FirstOrDefault();
            var microdataType3 = oldMicros.Where(x => x.Type == ContractReportMicroType.Type3 && (x.Status == ContractReportMicroStatus.Actual || x.Status == ContractReportMicroStatus.Entered)).FirstOrDefault();
            var microdataType4 = oldMicros.Where(x => x.Type == ContractReportMicroType.Type4 && (x.Status == ContractReportMicroStatus.Actual || x.Status == ContractReportMicroStatus.Entered)).FirstOrDefault();

            if (microdataType1 != null)
            {
                var newMicrodataType1 = await this.contractReportMicroService.CreateContractReportMicroAsync(newContractReport, ContractReportMicroType.Type1, Source.Beneficiary, ct);
                newMicrodataType1.CopyExcelData(microdataType1);

                await this.contractReportMicrosRepository.CopyMicrodataType1ItemsAsync(microdataType1.ContractReportMicroId, newMicrodataType1.ContractReportMicroId, ct);
            }

            if (microdataType2 != null)
            {
                var newMicrodataType2 = await this.contractReportMicroService.CreateContractReportMicroAsync(newContractReport, ContractReportMicroType.Type2, Source.Beneficiary, ct);
                newMicrodataType2.CopyExcelData(microdataType2);

                await this.contractReportMicrosRepository.CopyMicrodataType2ItemsAsync(microdataType2.ContractReportMicroId, newMicrodataType2.ContractReportMicroId, ct);
            }

            if (microdataType3 != null)
            {
                var newMicrodataType3 = await this.contractReportMicroService.CreateContractReportMicroAsync(newContractReport, ContractReportMicroType.Type3, Source.Beneficiary, ct);
                newMicrodataType3.CopyExcelData(microdataType3);

                await this.contractReportMicrosRepository.CopyMicrodataType3ItemsAsync(microdataType3.ContractReportMicroId, newMicrodataType3.ContractReportMicroId, ct);
            }

            if (microdataType4 != null)
            {
                var newMicrodataType4 = await this.contractReportMicroService.CreateContractReportMicroAsync(newContractReport, ContractReportMicroType.Type4, Source.Beneficiary, ct);
                newMicrodataType4.CopyExcelData(microdataType4);

                await this.contractReportMicrosRepository.CopyMicrodataType4ItemsAsync(microdataType4.ContractReportMicroId, newMicrodataType4.ContractReportMicroId, ct);
            }

            await this.unitOfWork.SaveAsync(ct);
        }

        #endregion //Private
    }
}
