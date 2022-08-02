using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.AnnualAccountReports.Repositories;
using Eumis.Data.OperationalMap.ProgrammePriorities.Repositories;
using Eumis.Domain;
using Eumis.Domain.AnnualAccountReports;
using Eumis.Domain.AnnualAccountReports.DataObjects;

namespace Eumis.ApplicationServices.Services.AnnualAccountReport
{
    public class AnnualAccountReportService : IAnnualAccountReportService
    {
        private IUnitOfWork unitOfWork;
        private IAnnualAccountReportsRepository annualAccountReportsRepository;
        private IProgrammePrioritiesRepository programmePrioritiesRepository;

        public AnnualAccountReportService(
            IUnitOfWork unitOfWork,
            IAnnualAccountReportsRepository annualAccountReportsRepository,
            IProgrammePrioritiesRepository programmePrioritiesRepository)
        {
            this.unitOfWork = unitOfWork;
            this.annualAccountReportsRepository = annualAccountReportsRepository;
            this.programmePrioritiesRepository = programmePrioritiesRepository;
        }

        #region AnnualAccountReport

        public IList<string> CanDeleteAnnualAccountReport(int annualAccountReportId)
        {
            var errors = new List<string>();

            var annualAccountReport = this.annualAccountReportsRepository.Find(annualAccountReportId);

            if (annualAccountReport.Status != AnnualAccountReportStatus.Draft)
            {
                errors.Add("Не можете да изтриете счетоводния отчет, защото статуса е различен от 'Чернова'");
            }

            if (annualAccountReport.OrderVersionNum > 1)
            {
                errors.Add("Не можете да изтриете счетоводния отчет, защото това не е първа версия");
            }

            if (annualAccountReport.AuditDocuments.Any())
            {
                errors.Add("Не можете да изтриете счетоводния отчет, защото към него има прикачени документи на ОО");
            }

            if (annualAccountReport.CertificationDocuments.Any())
            {
                errors.Add("Не можете да изтриете счетоводния отчет, защото към него има прикачени документи на СО");
            }

            return errors;
        }

        public Domain.AnnualAccountReports.AnnualAccountReport CreateAnnualAccountReport(
            int programmeId,
            DateTime regDate,
            AnnualAccountReportPeriod accountPeriod)
        {
            var newAnnualAccountReport = new Domain.AnnualAccountReports.AnnualAccountReport(
                programmeId,
                this.annualAccountReportsRepository.GetNextOrderNum(programmeId),
                regDate,
                accountPeriod);

            this.annualAccountReportsRepository.Add(newAnnualAccountReport);

            var programmePriorities = this.programmePrioritiesRepository.GetProgrammePriorityItems(programmeId, Array.Empty<int>());
            newAnnualAccountReport.AddAnnualAccountReportAppendices(programmePriorities.Select(x => x.ItemId).ToArray());

            var unattachedCertReports = this.annualAccountReportsRepository.GetUnattachedCertReports(newAnnualAccountReport);
            newAnnualAccountReport.AddCertReports(unattachedCertReports);
            this.unitOfWork.Save();

            var attachedCertReports = newAnnualAccountReport.CertReports.Select(x => x.CertReportId).ToArray();

            var unattachedFinancialCorrectionsCSDs = this.annualAccountReportsRepository.GetUnattachedFinancialCorrectionsCSDs(attachedCertReports);
            newAnnualAccountReport.AddFinnancialCorrectionsCSDs(unattachedFinancialCorrectionsCSDs);

            var unattachedCorrections = this.annualAccountReportsRepository.GetUnattachedCorrections(attachedCertReports);
            newAnnualAccountReport.AddContractReportCorrections(unattachedCorrections);

            this.unitOfWork.Save();

            return newAnnualAccountReport;
        }

        public void DeleteAnnualAccountReport(int annualAccountReportId, byte[] vers)
        {
            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, vers);

            annualAccountReport.AssertIsDraft();

            this.annualAccountReportsRepository.Remove(annualAccountReport);

            this.unitOfWork.Save();
        }

        public void UpdateAnnualAccountReport(int annualAccountReportId, byte[] version, DateTime? regDate, DateTime? approvalDate)
        {
            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, version);

            annualAccountReport.AssertIsDraft();

            var programmePriorities = this.programmePrioritiesRepository.GetProgrammePriorityItems(annualAccountReport.ProgrammeId, Array.Empty<int>());
            annualAccountReport.UpdateAttributes(regDate, approvalDate, programmePriorities.Select(x => x.ItemId).ToArray());

            this.unitOfWork.Save();
        }

        public IList<string> CanChangeStatus(int annualAccountReportId, AnnualAccountReportStatus status)
        {
            List<string> errors = new List<string>();

            var annualAccountReport = this.annualAccountReportsRepository.FindWithoutIncludes(annualAccountReportId);

            switch (status)
            {
                case AnnualAccountReportStatus.Draft:
                    if (annualAccountReport.Status == AnnualAccountReportStatus.Draft)
                    {
                        errors.Add("Отчетът вече е в статус \"Чернова\"");
                    }

                    break;
                case AnnualAccountReportStatus.Ended:
                    break;
                case AnnualAccountReportStatus.Opened:
                    if (annualAccountReport.Status == AnnualAccountReportStatus.Opened)
                    {
                        errors.Add("Отчетът вече е в статус \"Отключен\"");
                    }

                    if (annualAccountReport.Status == AnnualAccountReportStatus.Draft)
                    {
                        errors.Add("Отчетът не е в статус \"Приключен\"");
                    }

                    break;
                default:
                    break;
            }

            return errors;
        }

        public int? ChangeAnnualAccountReportStatus(int annualAccountReportId, byte[] version, AnnualAccountReportStatus status, string statusNote = null)
        {
            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, version);

            Action<AnnualAccountReportStatus> validateStatus = (s) =>
            {
                if (annualAccountReport.Status != s)
                {
                    throw new DomainException("AnnualAccountReport status transition not allowed");
                }
            };

            switch (status)
            {
                case AnnualAccountReportStatus.Draft:
                    validateStatus(AnnualAccountReportStatus.Ended);
                    break;
                case AnnualAccountReportStatus.Ended:
                    validateStatus(AnnualAccountReportStatus.Draft);
                    break;
                case AnnualAccountReportStatus.Opened:
                    validateStatus(AnnualAccountReportStatus.Ended);
                    break;
            }

            annualAccountReport.Status = status;
            annualAccountReport.ModifyDate = DateTime.Now;

            // make the current annualAccountReport returned and create a new one with a status draft
            Eumis.Domain.AnnualAccountReports.AnnualAccountReport newAnnualAccountReport = null;
            if (status == AnnualAccountReportStatus.Opened)
            {
                var currentDate = DateTime.Now;
                newAnnualAccountReport = new Eumis.Domain.AnnualAccountReports.AnnualAccountReport()
                {
                    ProgrammeId = annualAccountReport.ProgrammeId,
                    OrderNum = annualAccountReport.OrderNum,
                    OrderVersionNum = annualAccountReport.OrderVersionNum + 1,
                    RegDate = annualAccountReport.RegDate,
                    Status = AnnualAccountReportStatus.Draft,
                    StatusNote = statusNote,
                    AccountPeriod = annualAccountReport.AccountPeriod,
                    ApprovalDate = annualAccountReport.ApprovalDate,
                    CreateDate = currentDate,
                    ModifyDate = currentDate,
                };

                annualAccountReport.Status = AnnualAccountReportStatus.Opened;

                this.annualAccountReportsRepository.Add(newAnnualAccountReport);

                this.unitOfWork.Save();

                newAnnualAccountReport.AddCertReports(annualAccountReport.CertReports.Select(cr => cr.CertReportId).ToList());
                newAnnualAccountReport.AddContractReportCorrections(annualAccountReport.Corrections.Select(c => c.ContractReportCorrectionId).ToArray());
                newAnnualAccountReport.AddContractReportCertCorrections(annualAccountReport.CertifiedCorrections.Select(cc => cc.ContractReportCertAuthorityCorrectionId).ToArray());

                annualAccountReport.AuditDocuments.ToList().ForEach(d => newAnnualAccountReport.CreateAnnualAccountReportAuditDocument(d.Name, d.Description, d.BlobKey));
                annualAccountReport.CertificationDocuments.ToList().ForEach(d => newAnnualAccountReport.CreateAnnualAccountReportCertificationDocument(d.Name, d.Description, d.BlobKey));

                newAnnualAccountReport.AddCertFinnancialCorrectionsCSDs(annualAccountReport.CertifiedFinancialCorrectionCSDs.Select(csd => csd.ContractReportCertAuthorityFinancialCorrectionCSDId).ToArray());
                newAnnualAccountReport.AddFinnancialCorrectionsCSDs(annualAccountReport.FinancialCorrectionCSDs.Select(csd => csd.ContractReportFinancialCorrectionCSDId).ToArray());

                annualAccountReport.Appendices.ToList().ForEach(a => newAnnualAccountReport.CreateNewAppendix(new AnnualAccountReportAppendixCreateDO()
                {
                    ProgrammePriorityId = a.ProgrammePriorityId,
                    Type = a.Type,
                    Comment = a.Comment,
                }));
            }

            this.unitOfWork.Save();

            if (newAnnualAccountReport != null)
            {
                return newAnnualAccountReport.AnnualAccountReportId;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region AnnualAccountReportFinancialCorrectionCSDs

        public void DeleteAnnualAccountReportFinancialCorrection(int annualAccountReportId, byte[] version, int contractReportFinancialCorrectionId)
        {
            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, version);
            annualAccountReport.AssertIsDraft();

            var attachedCsds = this.annualAccountReportsRepository.FindAllAttachedFinancialCorrectionCSDs(annualAccountReportId, contractReportFinancialCorrectionId);
            annualAccountReport.DeleteContractReportFinancialCorrectionCSDs(attachedCsds);

            this.unitOfWork.Save();
        }

        public void CreateAnnualAccountReportFinancialCorrection(int annualAccountReportId, byte[] version, int[] contractReportFinancialCorrectionIds)
        {
            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, version);
            annualAccountReport.AssertIsDraft();

            var unattachedCsds = new List<int>();

            foreach (var contractReportFinancialCorrectionId in contractReportFinancialCorrectionIds)
            {
                unattachedCsds.AddRange(this.annualAccountReportsRepository.FindAllUnattachedFinancialCorrectionCSDs(contractReportFinancialCorrectionId));
            }

            annualAccountReport.AddFinnancialCorrectionsCSDs(unattachedCsds.ToArray());

            this.unitOfWork.Save();
        }

        public void CreateAnnualAccountReportFinancialCorrectionCSDs(int annualAccountReportId, byte[] version, int contractReportFinancialCorrectionId, int[] contractReportFinancialCorrectionCSDIds)
        {
            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, version);
            annualAccountReport.AssertIsDraft();

            var csds = this.annualAccountReportsRepository.FindFinancialCorrectionCSDs(contractReportFinancialCorrectionCSDIds);

            annualAccountReport.AddFinnancialCorrectionsCSDs(csds);

            this.unitOfWork.Save();
        }

        #endregion AnnualAccountReportFinancialCorrectionCSDs

        #region AnnualAccountReportCertifiedFinancialCorrectionCSDs

        public void DeleteAnnualAccountReportCertFinancialCorrection(int annualAccountReportId, byte[] version, int contractReportFinancialCorrectionId)
        {
            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, version);
            annualAccountReport.AssertIsDraft();

            var attachedCsds = this.annualAccountReportsRepository.FindAllAttachedCertFinancialCorrectionCSDs(annualAccountReportId, contractReportFinancialCorrectionId);
            annualAccountReport.DeleteCertFinancialCorrectionCSDs(attachedCsds);

            this.unitOfWork.Save();
        }

        public void CreateAnnualAccountReportCertFinancialCorrectionCSDs(int annualAccountReportId, byte[] version, int contractReportFinancialCorrectionId, int[] contractReportFinancialCorrectionCSDIds)
        {
            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, version);
            annualAccountReport.AssertIsDraft();

            annualAccountReport.AddCertFinnancialCorrectionsCSDs(contractReportFinancialCorrectionCSDIds);

            this.unitOfWork.Save();
        }

        public void CreateAnnualAccountReportCertFinancialCorrection(int annualAccountReportId, byte[] version, int[] contractReportFinancialCorrectionIds)
        {
            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, version);
            annualAccountReport.AssertIsDraft();
            var unattachedCsds = new List<int>();
            foreach (var contractReportFinancialCorrectionId in contractReportFinancialCorrectionIds)
            {
                unattachedCsds.AddRange(this.annualAccountReportsRepository.FindAllUnattachedCertFinancialCorrectionCSDs(contractReportFinancialCorrectionId));
            }

            annualAccountReport.AddCertFinnancialCorrectionsCSDs(unattachedCsds.ToArray());

            this.unitOfWork.Save();
        }

        #endregion AnnualAccountReportCertifiedFinancialCorrectionCSDs

        #region AnnualAccountReportCertifiedRevalidationFinancialCorrectionCSDs

        public void CreateAnnualAccountReportCertRevalidationFinancialCorrection(int annualAccountReportId, byte[] version, int[] contractReportRevalidationCertAuthorityFinancialCorrectionIds)
        {
            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, version);
            annualAccountReport.AssertIsDraft();
            var unattachedCsds = new List<int>();
            foreach (var id in contractReportRevalidationCertAuthorityFinancialCorrectionIds)
            {
                unattachedCsds.AddRange(this.annualAccountReportsRepository.GetAllUnattachedCertRevalidationFinancialCorrectionCSDs(id));
            }

            annualAccountReport.AddCertRevalidationFinnancialCorrectionsCSDs(unattachedCsds.ToArray());

            this.unitOfWork.Save();
        }

        public void DeleteAnnualAccountReportCertRevalidationFinancialCorrection(int annualAccountReportId, byte[] version, int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, version);
            annualAccountReport.AssertIsDraft();

            var attachedCsds = this.annualAccountReportsRepository.GetAllAttachedCertRevalidationFinancialCorrectionCSDs(annualAccountReportId, contractReportRevalidationCertAuthorityFinancialCorrectionId);
            annualAccountReport.DeleteCertRevalidationFinancialCorrectionCSDs(attachedCsds);

            this.unitOfWork.Save();
        }

        #endregion AnnualAccountReportCertifiedRevalidationFinancialCorrectionCSDs

        #region Appendices

        public AnnualAccountReportAppendixDO GetAnnualAccountReportAppendix(int annualAccountReportId, int appendixId)
        {
            var annualAccountReport = this.annualAccountReportsRepository.Find(annualAccountReportId);

            var annualAccountReportAppendix = annualAccountReport.FindAnnualAccountReportAppendix(appendixId);
            var programmePriority = this.programmePrioritiesRepository.Find(annualAccountReportAppendix.ProgrammePriorityId);

            return new AnnualAccountReportAppendixDO(annualAccountReportAppendix, programmePriority, annualAccountReport.Version);
        }

        #endregion Appendices
    }
}
