using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.SapInterfaces.SapDocuments;
using Eumis.Common;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Log;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Counters;
using Eumis.Data.Debts.Repositories;
using Eumis.Data.SapInterfaces.Repositories;
using Eumis.Domain;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.SapInterfaces;

namespace Eumis.ApplicationServices.Services.SapInterfaces
{
    public class SapFileService : ISapFileService
    {
        private static readonly Regex ContractRegNumberRegex = new Regex(@"(.+)-C\d\d", RegexOptions.Compiled);
        private static readonly Regex ContractRegNumberEndsWithDigit = new Regex(@"\d\d$", RegexOptions.Compiled);

        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private ISapFilesRepository sapFilesRepository;
        private ISapSchemasRepository sapSchemasRepository;
        private IContractsRepository contractsRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IContractDebtsRepository contractDebtsRepository;
        private ICountersRepository countersRepository;
        private IBlobServerCommunicator blobServerCommunicator;
        private ILogger logger;

        public SapFileService(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            ISapFilesRepository sapFilesRepository,
            ISapSchemasRepository sapSchemasRepository,
            IContractsRepository contractsRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IContractDebtsRepository contractDebtsRepository,
            ICountersRepository countersRepository,
            IBlobServerCommunicator blobServerCommunicator,
            ILogger logger)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.sapFilesRepository = sapFilesRepository;
            this.sapSchemasRepository = sapSchemasRepository;
            this.contractsRepository = contractsRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.contractDebtsRepository = contractDebtsRepository;
            this.countersRepository = countersRepository;
            this.blobServerCommunicator = blobServerCommunicator;
            this.logger = logger;
        }

        public SapFile CreateSapFile(Guid fileKey, string fileName, SapFileType type)
        {
            if (type != SapFileType.PaidAmounts && type != SapFileType.DistributedLimits)
            {
                throw new ArgumentException("Type of the sap file is not specified", nameof(type));
            }

            var (success, sapSchemaId, sapDocument, xmlContent) = this.ExtractAndValidateSapDocument(fileKey, type);

            if (!success)
            {
                return null;
            }

            var sapFile = new SapFile(
                type,
                sapSchemaId,
                fileKey,
                fileName,
                sapDocument.SapKey,
                sapDocument.Date
                    .AddHours(sapDocument.Time.Hour)
                    .AddMinutes(sapDocument.Time.Minute)
                    .AddSeconds(sapDocument.Time.Second)
                    .AddMilliseconds(sapDocument.Time.Millisecond),
                sapDocument.SapUser,
                xmlContent,
                this.accessContext.UserId);
            this.sapFilesRepository.Add(sapFile);
            this.unitOfWork.Save();

            if (type == SapFileType.PaidAmounts)
            {
                sapFile = this.CreateSapPaidAmounts(sapDocument, sapFile);
            }

            if (type == SapFileType.DistributedLimits)
            {
                sapFile = this.CreateSapDistributedLimits(sapDocument, sapFile);
            }

            return sapFile;
        }

        private SapFile CreateSapDistributedLimits(SapDocument sapDocument, SapFile sapFile)
        {
            Func<string, string> trimEndingLetters = (regNumber) => ContractRegNumberEndsWithDigit.Match(regNumber).Success ? regNumber : regNumber.Substring(0, regNumber.Length - 2);

            var sapFileContractRegNums = new HashSet<string>(sapDocument.ContractCollection.Select(c => trimEndingLetters(c.ContractSapNum)).Distinct());
            var contractsData = this.contractsRepository.GetEnteredContractData();

            Func<string, string> getRegNumberSignificantPart = (regNumber) => ContractRegNumberRegex.Match(regNumber).Groups[1].Value;
            var contracts = contractsData
                .Select(cd =>
                    new
                    {
                        RegNumberSignificantPart = getRegNumberSignificantPart(cd.regNumber),
                        ContractId = cd.contractId,
                        ProgrammeId = cd.programmeId,
                        RegNumber = cd.regNumber,
                    })
                .Where(cd => sapFileContractRegNums.Contains(cd.RegNumberSignificantPart))
                .ToDictionary(cd => cd.RegNumberSignificantPart, cd => cd);

            var sapFileContractIds = contracts.Select(c => c.Value.ContractId).ToArray();

            var payments = this.contractReportPaymentsRepository.GetActualContractReportPaymentsData(sapFileContractIds);

            IList<SapDistributedLimit> distributedLimits = new List<SapDistributedLimit>();
            foreach (var sapContract in sapDocument.ContractCollection)
            {
                List<string> contractErrors = new List<string>();
                List<string> contractWarnings = new List<string>();

                int? programmeId = null;
                int? contractId = null;

                string sapNumber = trimEndingLetters(sapContract.ContractSapNum);

                if (contracts.TryGetValue(sapNumber, out var contractData))
                {
                    programmeId = contractData.ProgrammeId;
                    contractId = contractData.ContractId;
                }
                else
                {
                    contractErrors.Add("Договорът е без съответствие в ИСУН.");
                }

                SapDistributedLimitEuFund? euFund = null;
                switch (sapContract.EuFund)
                {
                    case "ADVANCEEU":
                        euFund = SapDistributedLimitEuFund.AdvanceEu;
                        break;
                    case "ADVANCEBG":
                        euFund = SapDistributedLimitEuFund.AdvanceBg;
                        break;
                    case "INTERIMEU":
                        euFund = SapDistributedLimitEuFund.InterimEu;
                        break;
                    case "INTERIMBG":
                        euFund = SapDistributedLimitEuFund.InterimBg;
                        break;
                    case "FINALEU":
                        euFund = SapDistributedLimitEuFund.FinalEu;
                        break;
                    case "FINALBG":
                        euFund = SapDistributedLimitEuFund.FinalBg;
                        break;
                    case null:
                        contractErrors.Add("Фондът не е специфициран.");
                        break;
                    default:
                        contractErrors.Add("Фондът не може да бъде разпознат.");
                        break;
                }

                int? programmePriorityId = null;

                foreach (var sapReqPayment in sapContract.ReqPaymentCollection)
                {
                    List<string> reqPaymentErrors = new List<string>();
                    List<string> reqPaymentWarnings = new List<string>();

                    int? paymentId = null;
                    if (contractId.HasValue)
                    {
                        var paymentValue = sapReqPayment.ContractPaymentCollection.Select(p => p.Comment).Distinct().FirstOrDefault();

                        if (!string.IsNullOrWhiteSpace(paymentValue) &&
                            int.TryParse(paymentValue, out var paymentNum))
                        {
                            var paymentData = payments.SingleOrDefault(p => p.Item1 == contractId && p.Item2 == paymentNum);
                            if (paymentData != null)
                            {
                                paymentId = paymentData.Item3;
                            }
                            else
                            {
                                reqPaymentWarnings.Add($"За този договор не съществува искане за плащане с номер \"{paymentNum}\" и такова няма да бъде попълнено.");
                            }
                        }
                        else
                        {
                            reqPaymentWarnings.Add($"Не е посочен номер за искане на плащане или посоченият номер не може да се интерпретира като число.");
                        }
                    }

                    foreach (var sapPaymentGroup in sapReqPayment.ContractPaymentCollection.GroupBy(sp => sp.SAPDate))
                    {
                        List<string> paymentErrors = new List<string>();
                        List<string> paymentWarnings = new List<string>();

                        var sapDate = sapPaymentGroup.Key;

                        Func<ContractPayment, bool> isStorno = cp =>
                                !string.IsNullOrWhiteSpace(cp.StornoCode) &&
                                cp.StornoCode != "2" &&
                                cp.StornoCode != "02";

                        int stornoCount = sapPaymentGroup
                            .Where(cp => isStorno(cp))
                            .Count();
                        if (stornoCount > 0)
                        {
                            paymentErrors.Add($"Записи за сторнирани документи не се импортират в ИСУН. {stornoCount} ContractPayment запис/а ще бъдат игнорирани.");
                        }

                        Func<ContractPayment, bool> isBgn = cp => cp.Currency == Currency.BGN;

                        int nonBgnCount = sapPaymentGroup
                            .Where(cp => !isBgn(cp))
                            .Count();
                        if (nonBgnCount > 0)
                        {
                            paymentErrors.Add($"Записи във валути различни от BGN не се импортират в ИСУН. {nonBgnCount} ContractPayment запис/а ще бъдат игнорирани.");
                        }

                        var filteredPayments = sapPaymentGroup.Where(cp => !isStorno(cp) && isBgn(cp));

                        decimal paidBfpBgAmount = 0;
                        decimal paidBfpEuAmount = 0;

                        var unknownPayments = filteredPayments.Where(cp => !string.IsNullOrEmpty(cp.FinanceSource));

                        if (filteredPayments.Count() > 1 && euFund.HasValue && SapDistributedLimit.BgAmountsFunds.Contains(euFund.Value))
                        {
                            paymentWarnings.Add($"На {sapDate:dd.MM.yyyy} има повече от един ContractPayment запис класифициран като BG(\"Финансиране от НФ\") в едно искане за плащане и сумите ще бъдат сумирани.");
                        }

                        if (filteredPayments.Count() > 1 && euFund.HasValue && SapDistributedLimit.EuAmountsFunds.Contains(euFund.Value))
                        {
                            paymentWarnings.Add($"На {sapDate:dd.MM.yyyy} има повече от един ContractPayment запис класифициран като EU(\"Финансиране от ЕС\") в едно искане за плащане и сумите ще бъдат сумирани.");
                        }

                        if (unknownPayments.Count() > 0)
                        {
                            var unknownPaymentsText = string.Join(", ", unknownPayments.Select(cp => cp.FinanceSource).ToArray());
                            paymentWarnings.Add($"На {sapDate:dd.MM.yyyy} има ContractPayment записи с неизвестен финансов източник({unknownPaymentsText}) и ще бъдат игнорирани.");
                        }
                        else
                        {
                            if (euFund.HasValue && SapDistributedLimit.BgAmountsFunds.Contains(euFund.Value))
                            {
                                paidBfpBgAmount = filteredPayments
                                    .Select(x => x.PayedAmount)
                                    .DefaultIfEmpty()
                                    .Sum();
                            }

                            if (euFund.HasValue && SapDistributedLimit.EuAmountsFunds.Contains(euFund.Value))
                            {
                                paidBfpEuAmount = filteredPayments
                                    .Select(x => x.PayedAmount)
                                    .DefaultIfEmpty()
                                    .Sum();
                            }
                        }

                        var paymentTypes = filteredPayments.Select(cp => cp.PaymentType).Distinct();
                        if (paymentTypes.Count() > 1)
                        {
                            paymentWarnings.Add($"За преводите на {sapDate:dd.MM.yyyy} се среща повече от един тип на плащане и ще бъде използван първия ненулев.");
                        }

                        PaymentType? paymentType = paymentTypes.Where(pt => pt.HasValue).FirstOrDefault();

                        SapPaymentType sapPaymentType;
                        if (paymentType.HasValue && paymentType != PaymentType.Undefined)
                        {
                            sapPaymentType = (SapPaymentType)paymentType.Value;
                        }
                        else
                        {
                            paymentWarnings.Add("Не е посочен тип на плащането и за \"Основание на плащане\" ще бъде избрано \"Верифицирани разходи по ново искане за плащане\"");

                            sapPaymentType = SapPaymentType.Intermediate;
                        }

                        var supportedPaymentTypes = SapPaidAmount.PaidAmountTypes.Concat(SapPaidAmount.ReimbursementTypes);
                        if (!supportedPaymentTypes.Contains(sapPaymentType))
                        {
                            paymentErrors.Add($"Посоченият тип на плащане не се поддържа.");
                        }

                        var accDates = filteredPayments.Select(cp => cp.AccDate).Distinct();
                        if (accDates.Count() > 1)
                        {
                            paymentWarnings.Add($"За преводите на {sapDate:dd.MM.yyyy} се среща повече от една AccDate дата и ще бъде използвана първата.");
                        }

                        DateTime accDate = accDates.FirstOrDefault();

                        var bankDates = filteredPayments.Select(cp => cp.BankDate).Distinct();
                        if (bankDates.Count() > 1)
                        {
                            paymentWarnings.Add($"За преводите на {sapDate:dd.MM.yyyy} се среща повече от една BankDate дата и ще бъде използвана първата.");
                        }

                        DateTime bankDate = bankDates.FirstOrDefault();

                        distributedLimits.Add(new SapDistributedLimit
                        {
                            ProgrammeId = programmeId,
                            ProgrammePriorityId = programmePriorityId,
                            ContractSapNum = sapNumber,
                            ContractId = contractId,
                            ContractReportPaymentNum = sapReqPayment.ContractPaymentCollection.Select(p => p.Comment).Distinct().FirstOrDefault(),
                            ContractReportPaymentId = paymentId,
                            ContractReportPaymentDate = sapReqPayment.ReqPaymentDate,
                            PaidBfpBgAmount = paidBfpBgAmount,
                            PaidBfpEuAmount = paidBfpEuAmount,
                            PaymentType = sapPaymentType,
                            AccDate = accDate,
                            BankDate = bankDate,
                            SapDate = sapDate,
                            Comment = string.Join("\n", filteredPayments.Select(cp => cp.Comment)),
                            HasWarning = contractWarnings.Any() || reqPaymentWarnings.Any() || paymentWarnings.Any(),
                            Warnings = string.Join(SapPaidAmount.ERRORS_SEPARATOR, contractWarnings.Concat(reqPaymentWarnings).Concat(paymentWarnings).ToArray()),
                            HasError = contractErrors.Any() || reqPaymentErrors.Any() || paymentErrors.Any(),
                            Errors = string.Join(SapPaidAmount.ERRORS_SEPARATOR, contractErrors.Concat(reqPaymentErrors).Concat(paymentErrors).ToArray()),
                        });
                    }
                }
            }

            var groupedDistributedLimits = (from dl in distributedLimits
                                            group dl by new
                                            {
                                                dl.ContractId,
                                                dl.ContractSapNum,
                                                dl.ProgrammeId,
                                                dl.ProgrammePriorityId,
                                                dl.ContractReportPaymentNum,
                                            }
                                            into g
                                            select new SapDistributedLimit
                                            {
                                                AccDate = g.Max(x => x.AccDate),
                                                ActuallyPaidAmountId = g.Max(x => x.ActuallyPaidAmountId),
                                                BankDate = g.Max(x => x.BankDate),
                                                Comment = g.Max(x => x.Comment),
                                                ContractId = g.Key.ContractId,
                                                ContractReportPaymentDate = g.Max(x => x.ContractReportPaymentDate),
                                                ContractReportPaymentId = g.Max(x => x.ContractReportPaymentId),
                                                ContractReportPaymentNum = g.Key.ContractReportPaymentNum,
                                                ContractSapNum = g.Key.ContractSapNum,
                                                Currency = SapPaidAmountCurrency.BGN,
                                                Errors = g.Max(x => x.Errors),
                                                HasError = g.Max(x => x.HasError),
                                                HasWarning = g.Max(x => x.HasWarning),
                                                IsImported = false,
                                                PaidBfpBgAmount = g.Sum(x => x.PaidBfpBgAmount),
                                                PaidBfpEuAmount = g.Sum(x => x.PaidBfpEuAmount),
                                                PaymentType = g.Max(x => x.PaymentType),
                                                ProgrammeId = g.Key.ProgrammeId,
                                                ProgrammePriorityId = g.Key.ProgrammePriorityId,
                                                SapDate = g.Max(x => x.SapDate),
                                                SapFileId = sapFile.SapFileId,
                                                StornoCode = g.Max(x => x.StornoCode),
                                                StornoDescr = g.Max(x => x.StornoDescr),
                                                Warnings = g.Max(x => x.Warnings),
                                            }).ToList();
            this.unitOfWork.BulkInsert<SapDistributedLimit>(groupedDistributedLimits);

            return sapFile;
        }

        private SapFile CreateSapPaidAmounts(SapDocument sapDocument, SapFile sapFile)
        {
            var sapFileContractRegNums = new HashSet<string>(sapDocument.ContractCollection.Select(c => c.ContractSapNum));
            var contractsData = this.contractsRepository.GetEnteredContractData();

            Func<string, string> getRegNumberSignificantPart = (regNumber) => ContractRegNumberRegex.Match(regNumber).Groups[1].Value;
            var contracts = contractsData
                .Select(cd =>
                    new
                    {
                        RegNumberSignificantPart = getRegNumberSignificantPart(cd.regNumber),
                        ContractId = cd.contractId,
                        ProgrammeId = cd.programmeId,
                        RegNumber = cd.regNumber,
                    })
                .Where(cd => sapFileContractRegNums.Contains(cd.RegNumberSignificantPart))
                .ToDictionary(cd => cd.RegNumberSignificantPart, cd => cd);

            var sapFileContractIds = contracts.Select(c => c.Value.ContractId).ToArray();
            var payments = this.contractReportPaymentsRepository.GetActualContractReportPaymentsData(sapFileContractIds);
            var debts = this.contractDebtsRepository.GetContractDebtsData(sapFileContractIds);

            IList<SapPaidAmount> paidAmounts = new List<SapPaidAmount>();
            foreach (var sapContract in sapDocument.ContractCollection)
            {
                List<string> contractErrors = new List<string>();
                List<string> contractWarnings = new List<string>();

                int? programmeId = null;
                int? contractId = null;
                if (contracts.TryGetValue(sapContract.ContractSapNum, out var contractData))
                {
                    programmeId = contractData.ProgrammeId;
                    contractId = contractData.ContractId;
                }
                else
                {
                    contractErrors.Add("Договорът е без съответствие в ИСУН.");
                }

                contractWarnings.Add("Фондът не е специфициран.");

                int? programmePriorityId = null;

                foreach (var sapReqPayment in sapContract.ReqPaymentCollection)
                {
                    List<string> reqPaymentErrors = new List<string>();
                    List<string> reqPaymentWarnings = new List<string>();

                    int? paymentId = null;
                    if (contractId.HasValue)
                    {
                        if (!string.IsNullOrWhiteSpace(sapReqPayment.ReqPaymentNum) &&
                            int.TryParse(sapReqPayment.ReqPaymentNum, out var paymentNum))
                        {
                            var paymentData = payments.SingleOrDefault(p => p.Item1 == contractId && p.Item2 == paymentNum);
                            if (paymentData != null)
                            {
                                paymentId = paymentData.Item3;
                            }
                            else
                            {
                                reqPaymentWarnings.Add($"За този договор не съществува искане за плащане с номер \"{paymentNum}\" и такова няма да бъде попълнено.");
                            }
                        }
                        else
                        {
                            reqPaymentWarnings.Add($"Не е посочен номер за искане на плащане или посоченият номер не може да се интерпретира като число.");
                        }
                    }

                    foreach (var sapPaymentGroup in sapReqPayment.ContractPaymentCollection.GroupBy(sp => sp.SAPDate))
                    {
                        List<string> paymentErrors = new List<string>();
                        List<string> paymentWarnings = new List<string>();

                        var sapDate = sapPaymentGroup.Key;

                        Func<ContractPayment, bool> isStorno = cp =>
                                !string.IsNullOrWhiteSpace(cp.StornoCode) &&
                                cp.StornoCode != "2" &&
                                cp.StornoCode != "02";

                        int stornoCount = sapPaymentGroup
                            .Where(cp => isStorno(cp))
                            .Count();
                        if (stornoCount > 0)
                        {
                            paymentErrors.Add($"Записи за сторнирани документи не се импортират в ИСУН. {stornoCount} ContractPayment запис/а ще бъдат игнорирани.");
                        }

                        Func<ContractPayment, bool> isBgn = cp => cp.Currency == Currency.BGN;

                        int nonBgnCount = sapPaymentGroup
                            .Where(cp => !isBgn(cp))
                            .Count();
                        if (nonBgnCount > 0)
                        {
                            paymentErrors.Add($"Записи във валути различни от BGN не се импортират в ИСУН. {nonBgnCount} ContractPayment запис/а ще бъдат игнорирани.");
                        }

                        var filteredPayments = sapPaymentGroup.Where(cp => !isStorno(cp) && isBgn(cp));

                        var bgPayments = filteredPayments.Where(cp => cp.FinanceSource == "BG");
                        var euPayments = filteredPayments.Where(cp => cp.FinanceSource == "EU");
                        var unknownPayments = filteredPayments.Where(cp => cp.FinanceSource != "BG" && cp.FinanceSource != "EU");

                        if (bgPayments.Count() > 1)
                        {
                            paymentWarnings.Add($"На {sapDate:dd.MM.yyyy} има повече от един ContractPayment запис класифициран като BG(\"Финансиране от НФ\") в едно искане за плащане и сумите ще бъдат сумирани.");
                        }

                        if (euPayments.Count() > 1)
                        {
                            paymentWarnings.Add($"На {sapDate:dd.MM.yyyy} има повече от един ContractPayment запис класифициран като EU(\"Финансиране от ЕС\") в едно искане за плащане и сумите ще бъдат сумирани.");
                        }

                        if (unknownPayments.Count() > 1)
                        {
                            var unknownPaymentsText = string.Join(", ", unknownPayments.Select(cp => cp.FinanceSource).ToArray());
                            paymentWarnings.Add($"На {sapDate:dd.MM.yyyy} има ContractPayment записи с неизвестен финансов източник({unknownPaymentsText}) и ще бъдат игнорирани.");
                        }

                        decimal paidBfpBgAmount = bgPayments
                            .Select(cp => cp.PayedAmount)
                            .DefaultIfEmpty()
                            .Sum();
                        decimal paidBfpEuAmount = euPayments
                            .Select(cp => cp.PayedAmount)
                            .DefaultIfEmpty()
                            .Sum();

                        var paymentTypes = bgPayments.Concat(euPayments).Select(cp => cp.PaymentType).Distinct();
                        if (paymentTypes.Count() > 1)
                        {
                            paymentWarnings.Add($"За преводите на {sapDate:dd.MM.yyyy} се среща повече от един тип на плащане и ще бъде използван първия ненулев.");
                        }

                        PaymentType? paymentType = paymentTypes.Where(pt => pt.HasValue).FirstOrDefault();

                        SapPaymentType sapPaymentType;
                        if (paymentType.HasValue && paymentType != PaymentType.Undefined)
                        {
                            sapPaymentType = (SapPaymentType)paymentType.Value;
                        }
                        else
                        {
                            paymentWarnings.Add("Не е посочен тип на плащането и за \"Основание на плащане\" ще бъде избрано \"Верифицирани разходи по ново искане за плащане\"");

                            sapPaymentType = SapPaymentType.Intermediate;
                        }

                        var supportedPaymentTypes = SapPaidAmount.PaidAmountTypes.Concat(SapPaidAmount.ReimbursementTypes);
                        if (!supportedPaymentTypes.Contains(sapPaymentType))
                        {
                            paymentErrors.Add($"Посоченият тип на плащане не се поддържа.");
                        }

                        int? debtId = null;
                        if (contractId.HasValue &&
                            SapPaidAmount.ReimbursementTypes.Contains(sapPaymentType))
                        {
                            if (paymentId.HasValue)
                            {
                                var debtData = debts.FirstOrDefault(d => d.Item1 == contractId && d.Item2 == paymentId);
                                if (debtData != null)
                                {
                                    debtId = debtData.Item3;
                                }
                                else
                                {
                                    paymentErrors.Add("Типът на плащане е относно възстановяване, но няма намерен дълг, който отговаря на избрания договор и искане за плащане в ИСУН.");
                                }
                            }
                            else
                            {
                                paymentErrors.Add("Типът на плащане е относно възстановяване, но посочено искане за плащане.");
                            }
                        }

                        var accDates = bgPayments.Concat(euPayments).Select(cp => cp.AccDate).Distinct();
                        if (accDates.Count() > 1)
                        {
                            paymentWarnings.Add($"За преводите на {sapDate:dd.MM.yyyy} се среща повече от една AccDate дата и ще бъде използвана първата.");
                        }

                        DateTime accDate = accDates.FirstOrDefault();

                        var bankDates = bgPayments.Concat(euPayments).Select(cp => cp.BankDate).Distinct();
                        if (bankDates.Count() > 1)
                        {
                            paymentWarnings.Add($"За преводите на {sapDate:dd.MM.yyyy} се среща повече от една BankDate дата и ще бъде използвана първата.");
                        }

                        DateTime bankDate = bankDates.FirstOrDefault();

                        paidAmounts.Add(new SapPaidAmount
                        {
                            SapFileId = sapFile.SapFileId,
                            IsImported = false,
                            ProgrammeId = programmeId,
                            ProgrammePriorityId = programmePriorityId,
                            ContractSapNum = sapContract.ContractSapNum,
                            ContractId = contractId,
                            ContractDebtId = debtId,
                            ContractReportPaymentNum = sapReqPayment.ReqPaymentNum,
                            ContractReportPaymentId = paymentId,
                            ContractReportPaymentDate = sapReqPayment.ReqPaymentDate,
                            PaidBfpBgAmount = paidBfpBgAmount,
                            PaidBfpEuAmount = paidBfpEuAmount,
                            Currency = SapPaidAmountCurrency.BGN,
                            PaymentType = sapPaymentType,
                            AccDate = accDate,
                            BankDate = bankDate,
                            SapDate = sapDate,
                            Comment = string.Join("\n", filteredPayments.Select(cp => cp.Comment)),
                            HasWarning = contractWarnings.Any() || reqPaymentWarnings.Any() || paymentWarnings.Any(),
                            Warnings = string.Join(SapPaidAmount.ERRORS_SEPARATOR, contractWarnings.Concat(reqPaymentWarnings).Concat(paymentWarnings).ToArray()),
                            HasError = contractErrors.Any() || reqPaymentErrors.Any() || paymentErrors.Any(),
                            Errors = string.Join(SapPaidAmount.ERRORS_SEPARATOR, contractErrors.Concat(reqPaymentErrors).Concat(paymentErrors).ToArray()),
                        });
                    }
                }
            }

            this.unitOfWork.BulkInsert<SapPaidAmount>(paidAmounts);

            return sapFile;
        }

        public void ImportSapFile(SapFile sapFile)
        {
            switch (sapFile.Type)
            {
                case SapFileType.PaidAmounts:
                    this.ImportPaidAmounts(sapFile);
                    break;
                case SapFileType.DistributedLimits:
                    this.ImportDistributedLimits(sapFile);
                    break;
                default:
                    throw new DomainException("Unknown sap file type");
            }
        }

        private void ImportPaidAmounts(SapFile sapFile)
        {
            sapFile.MarkAsImported();
            this.unitOfWork.Save();

            var sapPaidAmounts = this.sapFilesRepository.GetCorrectPaidAmounts(sapFile.SapFileId);
            var paidAmounts = new List<Domain.MonitoringFinancialControl.ActuallyPaidAmount>();
            var paidAmountsPerContractId = new Dictionary<int, List<Domain.MonitoringFinancialControl.ActuallyPaidAmount>>();
            var reimbursedAmounts = new List<DebtReimbursedAmount>();
            var reimbursedAmountsPerContractDebtId = new Dictionary<int, List<DebtReimbursedAmount>>();

            foreach (var sapPaidAmount in sapPaidAmounts)
            {
                if (SapPaidAmount.PaidAmountTypes.Contains(sapPaidAmount.PaymentType.Value) && sapPaidAmount.ContractReportPaymentId.HasValue)
                {
                    var apa = this.CreatePaidAmount(sapPaidAmount);
                    paidAmounts.Add(apa);

                    if (!paidAmountsPerContractId.ContainsKey(apa.ContractId))
                    {
                        paidAmountsPerContractId.Add(apa.ContractId, new List<Domain.MonitoringFinancialControl.ActuallyPaidAmount>());
                    }

                    paidAmountsPerContractId[apa.ContractId].Add(apa);

                    sapPaidAmount.ActuallyPaidAmountId = apa.ActuallyPaidAmountId;
                    sapPaidAmount.IsImported = true;
                }
                else if (SapPaidAmount.ReimbursementTypes.Contains(sapPaidAmount.PaymentType.Value) && sapPaidAmount.ContractReportPaymentId.HasValue)
                {
                    var ra = this.AddReimbursedAmount(sapPaidAmount);
                    reimbursedAmounts.Add(ra);

                    if (!reimbursedAmountsPerContractDebtId.ContainsKey(ra.ContractDebtId))
                    {
                        reimbursedAmountsPerContractDebtId.Add(ra.ContractDebtId, new List<DebtReimbursedAmount>());
                    }

                    reimbursedAmountsPerContractDebtId[ra.ContractDebtId].Add(ra);

                    sapPaidAmount.ReimbursedAmountId = ra.ReimbursedAmountId;
                    sapPaidAmount.IsImported = true;
                }
            }

            foreach ((var contractId, var apaList) in paidAmountsPerContractId)
            {
                this.countersRepository.CreateActuallyPaidAmountCounter(contractId);
                var regNums = this.countersRepository.GetNextNActuallyPaidAmountNumbers(contractId, apaList.Count);
                var regNumsQueue = new Queue<string>(regNums);
                foreach (var apa in apaList)
                {
                    apa.ChangeStatusToEntered(regNumsQueue.Dequeue());
                }
            }

            foreach ((var contractDebtId, var raList) in reimbursedAmountsPerContractDebtId)
            {
                this.countersRepository.CreateDebtReimbursedAmountCounter(contractDebtId);
                var regNums = this.countersRepository.GetNextNDebtReimbursedAmountNumbers(contractDebtId, raList.Count);
                var regNumsQueue = new Queue<string>(regNums);
                foreach (var ra in raList)
                {
                    ra.ChangeStatusToEntered(regNumsQueue.Dequeue());
                }
            }

            this.unitOfWork.BulkInsert<Domain.MonitoringFinancialControl.ActuallyPaidAmount>(paidAmounts);
            this.unitOfWork.BulkInsert<DebtReimbursedAmount>(reimbursedAmounts);
            this.unitOfWork.BulkUpdate<SapPaidAmount>(sapPaidAmounts, spa => spa.IsImported, spa => spa.ActuallyPaidAmountId, spa => spa.ReimbursedAmountId);
        }

        private void ImportDistributedLimits(SapFile sapFile)
        {
            sapFile.MarkAsImported();
            this.unitOfWork.Save();

            var distributedLimits = this.sapFilesRepository.GetCorrectDistributedLimits(sapFile.SapFileId);
            var paidAmounts = new List<Domain.MonitoringFinancialControl.ActuallyPaidAmount>();
            var paidAmountsPerContractId = new Dictionary<int, List<Domain.MonitoringFinancialControl.ActuallyPaidAmount>>();

            foreach (var limit in distributedLimits)
            {
                if (SapPaidAmount.PaidAmountTypes.Contains(limit.PaymentType.Value) && limit.ContractReportPaymentId.HasValue)
                {
                    var apa = this.CreatePaidAmount(limit);
                    paidAmounts.Add(apa);

                    if (!paidAmountsPerContractId.ContainsKey(apa.ContractId))
                    {
                        paidAmountsPerContractId.Add(apa.ContractId, new List<Domain.MonitoringFinancialControl.ActuallyPaidAmount>());
                    }

                    paidAmountsPerContractId[apa.ContractId].Add(apa);

                    limit.ActuallyPaidAmountId = apa.ActuallyPaidAmountId;
                    limit.IsImported = true;
                }
            }

            foreach ((var contractId, var apaList) in paidAmountsPerContractId)
            {
                this.countersRepository.CreateActuallyPaidAmountCounter(contractId);
                var regNums = this.countersRepository.GetNextNActuallyPaidAmountNumbers(contractId, apaList.Count);
                var regNumsQueue = new Queue<string>(regNums);
                foreach (var apa in apaList)
                {
                    apa.ChangeStatusToEntered(regNumsQueue.Dequeue());
                }
            }

            this.unitOfWork.BulkInsert<Domain.MonitoringFinancialControl.ActuallyPaidAmount>(paidAmounts);
            this.unitOfWork.BulkUpdate<SapDistributedLimit>(distributedLimits, spa => spa.IsImported, spa => spa.ActuallyPaidAmountId);
        }

        public void DeleteSapFile(SapFile sapFile)
        {
            if (sapFile.Status != SapFileStatus.New)
            {
                throw new DomainException("Cannot delete SapFile when not in 'New' status");
            }

            this.unitOfWork.BulkDelete<SapPaidAmount>(spa => spa.SapFileId == sapFile.SapFileId);
            this.unitOfWork.BulkDelete<SapDistributedLimit>(spa => spa.SapFileId == sapFile.SapFileId);

            this.sapFilesRepository.Remove(sapFile);
            this.unitOfWork.Save();
        }

        private Domain.MonitoringFinancialControl.ActuallyPaidAmount CreatePaidAmount(SapPaidAmount sapPaidAmount)
        {
            PaymentReason? paymentReason = null;
            switch (sapPaidAmount.PaymentType)
            {
                case SapPaymentType.Advance:
                    paymentReason = PaymentReason.AdvancePayment;
                    break;
                case SapPaymentType.Intermediate:
                case SapPaymentType.Final:
                case SapPaymentType.Transfer:
                    paymentReason = PaymentReason.ApprovedExpenses;
                    break;
            }

            return new Domain.MonitoringFinancialControl.ActuallyPaidAmount(
                sapPaidAmount.SapFileId,
                sapPaidAmount.ProgrammeId.Value,
                sapPaidAmount.ProgrammePriorityId.Value,
                sapPaidAmount.ContractId.Value,
                sapPaidAmount.ContractReportPaymentId,
                paymentReason.Value,
                sapPaidAmount.ContractReportPaymentDate,
                sapPaidAmount.Comment,
                sapPaidAmount.PaidBfpEuAmount,
                sapPaidAmount.PaidBfpBgAmount,
                (decimal?)null);
        }

        private Domain.MonitoringFinancialControl.ActuallyPaidAmount CreatePaidAmount(SapDistributedLimit sapDistributedLimit)
        {
            PaymentReason? paymentReason = null;
            switch (sapDistributedLimit.PaymentType)
            {
                case SapPaymentType.Advance:
                    paymentReason = PaymentReason.AdvancePayment;
                    break;
                case SapPaymentType.Intermediate:
                case SapPaymentType.Final:
                case SapPaymentType.Transfer:
                    paymentReason = PaymentReason.ApprovedExpenses;
                    break;
            }

            return new Domain.MonitoringFinancialControl.ActuallyPaidAmount(
                sapDistributedLimit.SapFileId,
                sapDistributedLimit.ProgrammeId.Value,
                sapDistributedLimit.ProgrammePriorityId.Value,
                sapDistributedLimit.ContractId.Value,
                sapDistributedLimit.ContractReportPaymentId,
                paymentReason.Value,
                sapDistributedLimit.ContractReportPaymentDate,
                sapDistributedLimit.Comment,
                sapDistributedLimit.PaidBfpEuAmount,
                sapDistributedLimit.PaidBfpBgAmount,
                (decimal?)null);
        }

        private DebtReimbursedAmount AddReimbursedAmount(SapPaidAmount sapPaidAmount)
        {
            decimal? principalBfpEuAmount = null;
            decimal? principalBfpBgAmount = null;
            decimal? interestBfpEuAmount = null;
            decimal? interestBfpBgAmount = null;

            if (sapPaidAmount.PaymentType == SapPaymentType.Interest)
            {
                interestBfpEuAmount = sapPaidAmount.PaidBfpEuAmount;
                interestBfpBgAmount = sapPaidAmount.PaidBfpBgAmount;
            }
            else
            {
                principalBfpEuAmount = sapPaidAmount.PaidBfpEuAmount;
                principalBfpBgAmount = sapPaidAmount.PaidBfpBgAmount;
            }

            return new DebtReimbursedAmount(
                sapPaidAmount.SapFileId,
                sapPaidAmount.ProgrammeId.Value,
                sapPaidAmount.ContractId.Value,
                sapPaidAmount.ContractDebtId.Value,
                sapPaidAmount.AccDate.Value,
                ReimbursementType.ExGratia,
                Reimbursement.Bank,
                principalBfpEuAmount,
                principalBfpBgAmount,
                interestBfpEuAmount,
                interestBfpBgAmount,
                sapPaidAmount.Comment);
        }

        private (bool success, int sapSchemaId, SapDocument sapDocument, string xmlContent) ExtractAndValidateSapDocument(Guid fileKey, SapFileType type)
        {
            var activeSchema = this.sapSchemasRepository.GetActiveSchema(type);

            var schemas = new XmlSchemaSet();
            using (var sr = new StringReader(activeSchema.Content))
            using (XmlReader r = XmlReader.Create(sr))
            {
                schemas.Add(string.Empty, r);
            }

            bool xmlError = false;
            var settings = new XmlReaderSettings
            {
                Schemas = schemas,
                ValidationType = ValidationType.Schema,
            };

            settings.ValidationEventHandler += (s, e) =>
            {
                xmlError = true;
                this.logger.Log(LogLevel.Error, e.Message);
            };

            string xmlContent;
            SapDocument sapDocument;
            using (var blobStream = this.blobServerCommunicator.GetBlobStream(fileKey, true))
            {
                blobStream.Seek(0, SeekOrigin.Begin);
                using (var r = XmlReader.Create(blobStream, settings))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(SapDocument));
                    try
                    {
                        sapDocument = (SapDocument)ser.Deserialize(r);
                    }
                    catch (InvalidOperationException)
                    {
                        // TODO find a way to check the xml schema before this exception occurs
                        return (false, default(int), default(SapDocument), default(string));
                    }
                }

                blobStream.Seek(0, SeekOrigin.Begin);
                using (var r = XmlReader.Create(blobStream))
                using (var sw = new StringWriter())
                {
                    using (var w = XmlWriter.Create(sw, new XmlWriterSettings { OmitXmlDeclaration = true }))
                    {
                        w.WriteNode(r, true);
                    }

                    xmlContent = sw.ToString();
                }
            }

            if (xmlError)
            {
                return (false, default(int), default(SapDocument), default(string));
            }

            return (true, activeSchema.SapSchemaId, sapDocument, xmlContent);
        }
    }
}
