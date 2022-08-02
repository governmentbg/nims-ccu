using DocumentFormat.OpenXml.Packaging;
using Eumis.ApplicationServices.Communicators;
using Eumis.Common.Auth;
using Eumis.Common.Config;
using Eumis.Common.Db;
using Eumis.Common.Log;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ContractReportMicros;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.ApplicationServices.Services.ContractReportMicro
{
    public class ContractReportMicroService : IContractReportMicroService
    {
        private const string SimevInvalidEsfCodeError = "INVALID_ESF_CODE";

        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private ILogger logger;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportMicrosRepository contractReportMicrosRepository;
        private IContractReportMicroChecksRepository contractReportMicroChecksRepository;
        private IContractReportMicroType1Parser contractReportMicroType1Parser;
        private IContractReportMicroType2Parser contractReportMicroType2Parser;
        private IContractReportMicroType3Parser contractReportMicroType3Parser;
        private IContractReportMicroType4Parser contractReportMicroType4Parser;
        private IBlobServerCommunicator blobServerCommunicator;

        public ContractReportMicroService(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            ILogger logger,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportMicrosRepository contractReportMicrosRepository,
            IContractReportMicroChecksRepository contractReportMicroChecksRepository,
            IContractReportMicroType1Parser contractReportMicroType1Parser,
            IContractReportMicroType2Parser contractReportMicroType2Parser,
            IContractReportMicroType3Parser contractReportMicroType3Parser,
            IContractReportMicroType4Parser contractReportMicroType4Parser,
            IBlobServerCommunicator blobServerCommunicator)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.logger = logger;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportMicrosRepository = contractReportMicrosRepository;
            this.contractReportMicroChecksRepository = contractReportMicroChecksRepository;
            this.contractReportMicroType1Parser = contractReportMicroType1Parser;
            this.contractReportMicroType2Parser = contractReportMicroType2Parser;
            this.contractReportMicroType3Parser = contractReportMicroType3Parser;
            this.contractReportMicroType4Parser = contractReportMicroType4Parser;
            this.blobServerCommunicator = blobServerCommunicator;
        }

        public IList<string> CanCreateContractReportMicro(int contractReportId, ContractReportMicroType type)
        {
            var errors = new List<string>();

            if (this.contractReportMicrosRepository.FindAll(contractReportId).Where(m => m.Type == type).Any())
            {
                errors.Add("Не можете да създадете нови микроданни от избрания тип, защото вече съществуват такива към този пакет отчетни документи");
            }

            return errors;
        }

        public async Task<IList<string>> CanCreateContractReportMicroAsync(int contractReportId, ContractReportMicroType type, CancellationToken ct)
        {
            var errors = new List<string>();

            if ((await this.contractReportMicrosRepository.FindAllAsync(contractReportId, ct)).Where(m => m.Type == type).Any())
            {
                errors.Add("Не можете да създадете нови микроданни от избрания тип, защото вече съществуват такива към този пакет отчетни документи");
            }

            return errors;
        }

        public Domain.Contracts.ContractReportMicros.ContractReportMicro CreateContractReportMicro(Domain.Contracts.ContractReport contractReport, ContractReportMicroType type, Source source)
        {
            if (this.CanCreateContractReportMicro(contractReport.ContractReportId, type).Any())
            {
                throw new InvalidOperationException("Cannot create contract report micro data.");
            }

            var hasReturnedDocuments = this.contractReportsRepository.HasReturnedContractReportDocuments(contractReport.ContractReportId);

            if (contractReport.Status != ContractReportStatus.Draft && !hasReturnedDocuments)
            {
                throw new DomainException("Cannot create micro when contractReport not in 'Draft' status and doesn't have returned documents");
            }

            var versionNum = this.contractReportMicrosRepository.GetNextVersionNum(contractReport.ContractId, type);
            var versionSubNum = this.contractReportMicrosRepository.GetNextVersionSubNum(contractReport.ContractReportId, type);
            var micro = new Domain.Contracts.ContractReportMicros.ContractReportMicro(
                contractReport.ContractId,
                contractReport.ContractReportId,
                versionNum,
                versionSubNum,
                type,
                source);

            this.contractReportMicrosRepository.Add(micro);
            this.unitOfWork.Save();

            return micro;
        }

        public async Task<Domain.Contracts.ContractReportMicros.ContractReportMicro> CreateContractReportMicroAsync(Domain.Contracts.ContractReport contractReport, ContractReportMicroType type, Source source, CancellationToken ct)
        {
            if ((await this.CanCreateContractReportMicroAsync(contractReport.ContractReportId, type, ct)).Any())
            {
                throw new InvalidOperationException("Cannot create contract report micro data.");
            }

            var hasReturnedDocuments = await this.contractReportsRepository.HasReturnedContractReportDocumentsAsync(contractReport.ContractReportId, ct);

            if (contractReport.Status != ContractReportStatus.Draft && !hasReturnedDocuments)
            {
                throw new DomainException("Cannot create micro when contractReport not in 'Draft' status and doesn't have returned documents");
            }

            var versionNum = await this.contractReportMicrosRepository.GetNextVersionNumAsync(contractReport.ContractId, type, ct);
            var versionSubNum = await this.contractReportMicrosRepository.GetNextVersionSubNumAsync(contractReport.ContractReportId, type, ct);
            var micro = new Domain.Contracts.ContractReportMicros.ContractReportMicro(
                contractReport.ContractId,
                contractReport.ContractReportId,
                versionNum,
                versionSubNum,
                type,
                source);

            this.contractReportMicrosRepository.Add(micro);
            await this.unitOfWork.SaveAsync(ct);

            return micro;
        }

        public IList<string> UpdateContractReportMicro(Domain.Contracts.ContractReportMicros.ContractReportMicro micro, Guid excelBlobKey, string excelName, out IList<string> warnings)
        {
            var contractReport = this.contractReportsRepository.Find(micro.ContractReportId);

            return this.UpdateContractReportMicro(contractReport, micro, excelBlobKey, excelName, false, out warnings, false);
        }

        public IList<string> UpdateContractReportMicroNewVersion(Domain.Contracts.ContractReportMicros.ContractReportMicro micro, Guid excelBlobKey, string excelName, out IList<string> warnings)
        {
            var contractReport = this.contractReportsRepository.Find(micro.ContractReportId);

            return this.UpdateContractReportMicro(contractReport, micro, excelBlobKey, excelName, false, out warnings, true);
        }

        public IList<string> UpdateContractReportMicroWithSimevCode(Domain.Contracts.ContractReportMicros.ContractReportMicro micro, string simevCode, out IList<string> warnings)
        {
            warnings = new List<string>();

            if (micro.Type != ContractReportMicroType.Type2)
            {
                throw new InvalidOperationException("Invalid contract report micro type. Update with simev code requires type 2.");
            }

            var contract = this.contractsRepository.Find(micro.ContractId);
            var contractReport = this.contractReportsRepository.Find(micro.ContractReportId);

            var (fileKey, filename, error) = this.CopySimevExcelFileToBlobServer(simevCode, contract.RegNumber, contractReport.OrderNum.ToString());

            if (!string.IsNullOrEmpty(error))
            {
                return new List<string>() { error };
            }

            var errors = this.UpdateContractReportMicro(contractReport, micro, fileKey.Value, filename, true, out warnings, false);

            if (errors.Any())
            {
                this.logger.Log(LogLevel.Warn, $"Excel validation failing for simev file. Validation errors:\r\n{string.Join("\r\n", errors)}");
            }

            return errors;
        }

        public void ChangeContractReportMicroStatusToReturned(Domain.Contracts.ContractReportMicros.ContractReportMicro oldMicro, string statusNote)
        {
            var contractReport = this.contractReportsRepository.Find(oldMicro.ContractReportId);

            contractReport.AssertIsUncheckedContractReport();

            if (this.CanChangeContractReportMicroStatusToReturned(oldMicro.ContractReportId, oldMicro.Type).Any())
            {
                throw new DomainException("Cannot change ContractReportMicro status to 'Returned'");
            }

            oldMicro.ReturnMicro(statusNote);

            var newContractReportMicro = new Domain.Contracts.ContractReportMicros.ContractReportMicro(
                this.contractReportMicrosRepository.GetNextVersionSubNum(oldMicro.ContractReportId, oldMicro.Type),
                oldMicro);

            newContractReportMicro.Source = Source.Beneficiary;
            this.contractReportMicrosRepository.Add(newContractReportMicro);

            this.unitOfWork.Save();
        }

        public void CreateContractReportMicroNewVersion(Domain.Contracts.ContractReportMicros.ContractReportMicro oldMicro)
        {
            var contractReport = this.contractReportsRepository.Find(oldMicro.ContractReportId);

            if (this.CanCreateContractReportMicroNewVersion(oldMicro).Any())
            {
                throw new DomainException("Cannot create new version of ContractReportMicro");
            }

            var newContractReportMicro = new Domain.Contracts.ContractReportMicros.ContractReportMicro(
                this.contractReportMicrosRepository.GetNextVersionSubNum(oldMicro.ContractReportId, oldMicro.Type),
                oldMicro);

            newContractReportMicro.Source = Source.AdministrativeAuthority;

            this.contractReportMicrosRepository.Add(newContractReportMicro);

            this.unitOfWork.Save();
        }

        public IList<string> CanChangeContractReportMicroStatusToReturned(int contractReportId, ContractReportMicroType type)
        {
            var errors = new List<string>();

            if (this.contractReportMicroChecksRepository.HasContractReportMicroCheckInProgress(contractReportId, type))
            {
                errors.Add("Не можете да промените статуса на метаданните на 'Върнат', защото има проверка към тях със статус 'Чернова'");
            }

            if (this.contractReportMicrosRepository.FindAll(contractReportId).Where(x => x.Type == type && x.Status == ContractReportMicroStatus.Draft).Any())
            {
                errors.Add("Не можете да промените статуса на метаданните на 'Върнат', защото вече има създадена версия със статус 'Чернова'");
            }

            return errors;
        }

        public IList<string> CanDeleteContractReportMicroNewVersion(Domain.Contracts.ContractReportMicros.ContractReportMicro micro)
        {
            var errors = new List<string>();

            if (micro.Status != ContractReportMicroStatus.Draft)
            {
                errors.Add("Можете да изтриете микроданни само със статус 'Чернова'");
            }

            if (micro.Type != ContractReportMicroType.Type2)
            {
                errors.Add("Можете да изтриете само микроданни ЕСФ");
            }

            if (micro.Source != Source.AdministrativeAuthority)
            {
                errors.Add("Можете да изтриете микроданни само ако са въведени от УО");
            }

            bool previousVersionIsActual = this.contractReportMicrosRepository.FindAll(micro.ContractReportId).Where(x => x.Type == ContractReportMicroType.Type2 && x.Status == ContractReportMicroStatus.Actual && x.VersionSubNum == (micro.VersionSubNum - 1)).Any();
            if (!previousVersionIsActual)
            {
                errors.Add("Можете да изтриете микроданни ЕСФ само ако предходната версия е със статус 'Актуален'");
            }

            return errors;
        }

        public IList<string> CanCreateContractReportMicroNewVersion(Domain.Contracts.ContractReportMicros.ContractReportMicro micro)
        {
            var errors = new List<string>();

            if (micro.Status != ContractReportMicroStatus.Actual)
            {
                errors.Add("Можете да добавите нова версия, само от версия със статус 'Актуален'");
            }

            if (micro.Type != ContractReportMicroType.Type2)
            {
                errors.Add("Можете да добавяте нова версия само за микроданни ЕСФ");
            }

            var draftMicros = this.contractReportMicrosRepository.FindAll(micro.ContractReportId).Where(x => x.Type == ContractReportMicroType.Type2 && x.Status == ContractReportMicroStatus.Draft);
            if (draftMicros.Any())
            {
                errors.Add("Вече има създадена версия със статус Чернова");
            }

            return errors;
        }

        public void ChangeContractReportMicroStatus(Domain.Contracts.ContractReportMicros.ContractReportMicro contractReportMicro, ContractReportMicroStatus status, int? contractRegistrationId)
        {
            var contractReport = this.contractReportsRepository.Find(contractReportMicro.ContractReportId);

            contractReport.AssertIsDraftOrSentCheckedOrUncheckedContractReport();

            if (status == ContractReportMicroStatus.Entered && this.CanChangeContractReportMicroStatusToEntered(contractReportMicro).Any())
            {
                throw new DomainException("ContractReportFinancial status transition not allowed");
            }

            if (status == ContractReportMicroStatus.Actual && contractRegistrationId.HasValue)
            {
                contractReportMicro.SenderContractRegistrationId = contractRegistrationId.Value;
            }

            contractReportMicro.SetStatus(status);

            this.unitOfWork.Save();
        }

        public async Task ChangeContractReportMicroStatusAsync(Domain.Contracts.ContractReportMicros.ContractReportMicro contractReportMicro, ContractReportMicroStatus status, int? contractRegistrationId, CancellationToken ct)
        {
            var contractReport = await this.contractReportsRepository.FindWithoutIncludesAsync(contractReportMicro.ContractReportId, ct);

            contractReport.AssertIsDraftOrSentCheckedOrUncheckedContractReport();

            if (status == ContractReportMicroStatus.Entered && (await this.CanChangeContractReportMicroStatusToEnteredAsync(contractReportMicro, ct)).Any())
            {
                throw new DomainException("ContractReportFinancial status transition not allowed");
            }

            if (status == ContractReportMicroStatus.Actual && contractRegistrationId.HasValue)
            {
                contractReportMicro.SenderContractRegistrationId = contractRegistrationId.Value;
            }

            contractReportMicro.SetStatus(status);

            await this.unitOfWork.SaveAsync(ct);
        }

        public void ChangeContractReportNewVersionMicroStatusToActual(Domain.Contracts.ContractReportMicros.ContractReportMicro draftMicro, Domain.Contracts.ContractReportMicros.ContractReportMicro actualMicro, string note)
        {
            if (actualMicro != null)
            {
                if (actualMicro.VersionSubNum != draftMicro.VersionSubNum - 1)
                {
                    throw new DomainException("Cannot change ContractReportMicro status due VersionSubNum mismatch");
                }

                actualMicro.ReturnMicro(note);
            }

            draftMicro.AssertIsDraft();

            draftMicro.SetStatus(ContractReportMicroStatus.Actual);

            this.unitOfWork.Save();
        }

        public IList<string> CanChangeContractReportMicroStatusToEntered(Domain.Contracts.ContractReportMicros.ContractReportMicro contractReportMicro)
        {
            var errors = new List<string>();

            var contractReport = this.contractReportsRepository.Find(contractReportMicro.ContractReportId);

            if (contractReport.Status == ContractReportStatus.Draft && this.contractReportsRepository.HasContractReportInProgress(contractReport.ContractId, contractReport.ContractReportId))
            {
                errors.Add("Не можете да въведете микроданните, защото съществува друг пакет отчетни документи, който не е в статус 'Приет' или 'Отхвърлен'");
            }

            if (!contractReportMicro.ExcelBlobKey.HasValue)
            {
                errors.Add("Не може да въведете микроданни, без да сте качили excel файл.");
            }

            return errors;
        }

        public async Task<IList<string>> CanChangeContractReportMicroStatusToEnteredAsync(Domain.Contracts.ContractReportMicros.ContractReportMicro contractReportMicro, CancellationToken ct)
        {
            var errors = new List<string>();

            var contractReport = await this.contractReportsRepository.FindWithoutIncludesAsync(contractReportMicro.ContractReportId, ct);

            if (contractReport.Status == ContractReportStatus.Draft && this.contractReportsRepository.HasContractReportInProgress(contractReport.ContractId, contractReport.ContractReportId))
            {
                errors.Add("Не можете да въведете микроданните, защото съществува друг пакет отчетни документи, който не е в статус 'Приет' или 'Отхвърлен'");
            }

            if (!contractReportMicro.ExcelBlobKey.HasValue)
            {
                errors.Add("Не може да въведете микроданни, без да сте качили excel файл.");
            }

            return errors;
        }

        public void DeleteContractReportMicro(Domain.Contracts.ContractReportMicros.ContractReportMicro contractReportMicro)
        {
            if (contractReportMicro.Status != ContractReportMicroStatus.Draft)
            {
                throw new DomainException("Cannot delete ContractReportMicro when not in 'Draft' status");
            }

            if (contractReportMicro.VersionSubNum != 1)
            {
                throw new DomainException("Cannot delete ContractReportMicro when it's not first sub version");
            }

            this.unitOfWork.BulkDelete<ContractReportMicrosType1Item>(i => i.ContractReportMicroId == contractReportMicro.ContractReportMicroId);
            this.unitOfWork.BulkDelete<ContractReportMicrosType2Item>(i => i.ContractReportMicroId == contractReportMicro.ContractReportMicroId);
            this.unitOfWork.BulkDelete<ContractReportMicrosType3Item>(i => i.ContractReportMicroId == contractReportMicro.ContractReportMicroId);
            this.unitOfWork.BulkDelete<ContractReportMicrosType4Item>(i => i.ContractReportMicroId == contractReportMicro.ContractReportMicroId);
            this.unitOfWork.Save();

            this.contractReportMicrosRepository.Remove(contractReportMicro);
            this.unitOfWork.Save();
        }

        public void DeleteContractReportMicroNewVersion(Domain.Contracts.ContractReportMicros.ContractReportMicro contractReportMicro)
        {
            if (this.CanDeleteContractReportMicroNewVersion(contractReportMicro).Any())
            {
                throw new InvalidOperationException("Cannot delete contract report micro data.");
            }

            this.unitOfWork.BulkDelete<ContractReportMicrosType2Item>(i => i.ContractReportMicroId == contractReportMicro.ContractReportMicroId);
            this.unitOfWork.Save();

            this.contractReportMicrosRepository.Remove(contractReportMicro);
            this.unitOfWork.Save();
        }

        public ContractReportMicroCheck CreateContractReportMicroCheck(int contractReportId, ContractReportMicroType type)
        {
            var actualMicro = this.contractReportMicrosRepository.GetActualContractReportMicro(contractReportId, type);
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsUncheckedContractReport();

            if (this.CanCreateContractReportMicroCheck(contractReportId, type).Any())
            {
                throw new DomainException("Requirements for creating are not met.");
            }

            var newContractReportMicroCheck = new ContractReportMicroCheck(
                actualMicro.ContractReportMicroId,
                actualMicro.ContractReportId,
                actualMicro.ContractId,
                this.contractReportMicroChecksRepository.GetNextOrderNum(actualMicro.ContractReportMicroId),
                this.accessContext.UserId);

            this.contractReportMicroChecksRepository.Add(newContractReportMicroCheck);

            this.unitOfWork.Save();

            return newContractReportMicroCheck;
        }

        public IList<string> CanCreateContractReportMicroCheck(int contractReportId, ContractReportMicroType type)
        {
            var errors = new List<string>();

            var actualMicro = this.contractReportMicrosRepository.GetActualContractReportMicro(contractReportId, type);
            var microChecks = this.contractReportMicroChecksRepository.FindAll(contractReportId, type);

            if (actualMicro == null)
            {
                errors.Add("Не можете да създадете нова проверка на микроданни, защото няма актуалени микроданни от този тип.");
            }

            if (microChecks.Where(t => t.Status == ContractReportMicroCheckStatus.Draft).Any())
            {
                errors.Add("Не можете да създадете нова проверка на микроданни, защото съществува проверка на същия тип микроданни със статус 'Чернова'");
            }

            return errors;
        }

        public void UpdateContractReportMicroCheck(int contractReportMicroCheckId, byte[] version, ContractReportMicroCheckApproval? approval, DateTime? checkedDate, Guid? blobKey)
        {
            var microCheck = this.contractReportMicroChecksRepository.FindForUpdate(contractReportMicroCheckId, version);
            var contractReport = this.contractReportsRepository.Find(microCheck.ContractReportId);

            contractReport.AssertIsUncheckedContractReport();

            microCheck.UpdateAttributes(approval, checkedDate, blobKey);

            this.unitOfWork.Save();
        }

        public void ActivateContractReportMicroCheck(int contractReportMicroCheckId, byte[] version)
        {
            var microCheck = this.contractReportMicroChecksRepository.FindForUpdate(contractReportMicroCheckId, version);
            var contractReport = this.contractReportsRepository.Find(microCheck.ContractReportId);

            contractReport.AssertIsUncheckedContractReport();

            var microType = this.contractReportMicrosRepository.GetMicroType(microCheck.ContractReportMicroId);
            var actualMicroCheck = this.contractReportMicroChecksRepository.GetActualContractReportMicroCheck(microCheck.ContractReportId, microType);
            if (actualMicroCheck != null)
            {
                actualMicroCheck.ArchiveCheck();
            }

            microCheck.ActivateCheck(this.accessContext.UserId);

            this.unitOfWork.Save();
        }

        public void DeleteContractReportMicroCheck(int contractReportId, int contractReportMicroCheckId, byte[] version)
        {
            var microCheck = this.contractReportMicroChecksRepository.FindForUpdate(contractReportMicroCheckId, version);
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsUncheckedContractReport();

            if (microCheck.Status != ContractReportMicroCheckStatus.Draft)
            {
                throw new DomainException("Cannot delete a ContractReportMicroCheck with status different from 'Draft'");
            }

            this.contractReportMicroChecksRepository.Remove(microCheck);

            this.unitOfWork.Save();
        }

        private IList<string> UpdateContractReportMicro(Domain.Contracts.ContractReport contractReport, Domain.Contracts.ContractReportMicros.ContractReportMicro micro, Guid excelBlobKey, string excelName, bool isFromExternalSystem, out IList<string> warnings, bool updateNewVersion)
        {
            warnings = new List<string>();

            if (!updateNewVersion)
            {
                contractReport.AssertIsDraftOrUncheckedContractReport();
            }

            micro.UpdateExcel(excelBlobKey, excelName, isFromExternalSystem);

            this.unitOfWork.BulkDelete<ContractReportMicrosType1Item>(i => i.ContractReportMicroId == micro.ContractReportMicroId);
            this.unitOfWork.BulkDelete<ContractReportMicrosType2Item>(i => i.ContractReportMicroId == micro.ContractReportMicroId);
            this.unitOfWork.BulkDelete<ContractReportMicrosType3Item>(i => i.ContractReportMicroId == micro.ContractReportMicroId);
            this.unitOfWork.BulkDelete<ContractReportMicrosType4Item>(i => i.ContractReportMicroId == micro.ContractReportMicroId);
            this.unitOfWork.Save();

            IList<string> errors = new List<string>();
            try
            {
                using (var excelStream = this.blobServerCommunicator.GetBlobStream(excelBlobKey, true))
                {
                    switch (micro.Type)
                    {
                        case ContractReportMicroType.Type1:
                            {
                                var items = this.contractReportMicroType1Parser.ParseExcel(micro.ContractReportMicroId, excelStream, out errors);
                                if (errors.Count == 0)
                                {
                                    this.unitOfWork.BulkInsert<ContractReportMicrosType1Item>(items);
                                }
                            }

                            break;
                        case ContractReportMicroType.Type2:
                            {
                                var items = this.contractReportMicroType2Parser.ParseExcel(micro.ContractReportMicroId, excelStream, out errors, out warnings);
                                if (errors.Count == 0)
                                {
                                    this.unitOfWork.BulkInsert<ContractReportMicrosType2Item>(items);
                                }
                            }

                            break;
                        case ContractReportMicroType.Type3:
                            {
                                var items = this.contractReportMicroType3Parser.ParseExcel(micro.ContractReportMicroId, excelStream, out errors);
                                if (errors.Count == 0)
                                {
                                    this.unitOfWork.BulkInsert<ContractReportMicrosType3Item>(items);
                                }
                            }

                            break;
                        case ContractReportMicroType.Type4:
                            {
                                var items = this.contractReportMicroType4Parser.ParseExcel(micro.ContractReportMicroId, excelStream, out errors);
                                if (errors.Count == 0)
                                {
                                    this.unitOfWork.BulkInsert<ContractReportMicrosType4Item>(items);
                                }
                            }

                            break;
                        default:
                            throw new InvalidOperationException("Invalid contract report micro type.");
                    }
                }
            }
            catch (OpenXmlPackageException e)
            {
                if (e.Message.Contains("Invalid Hyperlink: Malformed URI is embedded as a hyperlink in the document"))
                {
                    errors.Add("Файлът съдържа невалиден/и имейл адрес/и!");
                    return errors;
                }

                throw;
            }
            catch (FileFormatException)
            {
                errors.Add("Невалиден формат на файла.");
                return errors;
            }

            this.unitOfWork.Save();
            return errors;
        }

        private (Guid? fileKey, string filename, string error) CopySimevExcelFileToBlobServer(
            string simevCode,
            string contractRegistrationNumber,
            string cotractReportOrderNumber)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response =
                    client.PostAsync(
                        new Uri(
                            ConfigurationManager.AppSettings.GetWithEnv("Eumis.ApplicationServices:SimevServerLocation"),
                            UriKind.Absolute),
                        new StringContent(
                            JsonConvert.SerializeObject(new
                            {
                                esfCode = simevCode,
                                contractRegistrationNumber,
                                cotractReportOrderNumber,
                            }),
                            Encoding.UTF8,
                            "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    var filename = response.Content.Headers.ContentDisposition.FileName;

                    var stream = response.Content.ReadAsStreamAsync().Result;

                    var blobInfo = this.blobServerCommunicator.UploadBlob(filename, stream);

                    return (blobInfo.FileKey, filename, null);
                }
                else
                {
                    string errorResponse = response.Content.ReadAsStringAsync().Result;
                    string errorCode;
                    string errorMessage;

                    try
                    {
                        var error = JsonConvert.DeserializeObject<JObject>(errorResponse);
                        errorCode = error.SelectToken("errorCode").Value<string>();
                        errorMessage = error.SelectToken("errorMessage").Value<string>();
                    }
                    catch
                    {
                        throw new Exception("Unexpected response received: " + errorResponse);
                    }

                    if (errorCode == SimevInvalidEsfCodeError)
                    {
                        return (null, null, "Липсват данни за подадения код в СИМЕВ!");
                    }
                    else
                    {
                        throw new Exception($"Unable to copy esf excel file. Simev errorCode: \"{errorCode}\", errorMessage: \"{errorMessage}\".");
                    }
                }
            }
        }
    }
}
