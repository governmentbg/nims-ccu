using Eumis.Domain.AnnualAccountReports.DataObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.AnnualAccountReports
{
    public partial class AnnualAccountReport
    {
        private static readonly (AnnualAccountReportPeriod AccountingPeriod, DateTime DateFrom, DateTime DateTo)[] AccountingPeriods = new ValueTuple<AnnualAccountReportPeriod, DateTime, DateTime>[]
        {
            ValueTuple.Create(AnnualAccountReportPeriod.Year2014, DateTime.Parse("2014-01-01"), DateTime.Parse("2015-06-30")),
            ValueTuple.Create(AnnualAccountReportPeriod.Year2015, DateTime.Parse("2015-07-01"), DateTime.Parse("2016-06-30")),
            ValueTuple.Create(AnnualAccountReportPeriod.Year2016, DateTime.Parse("2016-07-01"), DateTime.Parse("2017-06-30")),
            ValueTuple.Create(AnnualAccountReportPeriod.Year2017, DateTime.Parse("2017-07-01"), DateTime.Parse("2018-06-30")),
            ValueTuple.Create(AnnualAccountReportPeriod.Year2018, DateTime.Parse("2018-07-01"), DateTime.Parse("2019-06-30")),
            ValueTuple.Create(AnnualAccountReportPeriod.Year2019, DateTime.Parse("2019-07-01"), DateTime.Parse("2020-06-30")),
            ValueTuple.Create(AnnualAccountReportPeriod.Year2020, DateTime.Parse("2020-07-01"), DateTime.Parse("2021-06-30")),
            ValueTuple.Create(AnnualAccountReportPeriod.Year2021, DateTime.Parse("2021-07-01"), DateTime.Parse("2022-06-30")),
            ValueTuple.Create(AnnualAccountReportPeriod.Year2022, DateTime.Parse("2022-07-01"), DateTime.Parse("2023-06-30")),
            ValueTuple.Create(AnnualAccountReportPeriod.Year2023, DateTime.Parse("2023-07-01"), DateTime.Parse("2024-06-30")),
        };

        public DateTime GetDateFrom()
        {
            return AccountingPeriods
                    .Where(x => x.AccountingPeriod == this.AccountPeriod)
                    .Select(x => x.DateFrom)
                    .Single();
        }

        public DateTime GetDateTo()
        {
            return AccountingPeriods
                    .Where(x => x.AccountingPeriod == this.AccountPeriod)
                    .Select(x => x.DateTo)
                    .Single();
        }

        public void AssertIsDraft()
        {
            if (this.Status != AnnualAccountReportStatus.Draft)
            {
                throw new DomainException("Cannot edit AnnualAccountReport when not in 'Draft' status");
            }
        }

        public void UpdateAttributes(DateTime? regDate, DateTime? approvalDate, int[] programmePriorities)
        {
            this.RegDate = regDate.Value;
            this.ApprovalDate = approvalDate;

            this.UpdateAnnualAccountReportAppendices(programmePriorities);

            this.ModifyDate = DateTime.Now;
        }

        #region CertificationDocuments

        public AnnualAccountReportCertificationDocument CreateAnnualAccountReportCertificationDocument(
            string name,
            string description,
            Guid? blobKey)
        {
            var newAnnualAccountReportCertificationDocument = new AnnualAccountReportCertificationDocument()
            {
                AnnualAccountReportId = this.AnnualAccountReportId,
                Name = name,
                Description = description,
                BlobKey = blobKey,
            };

            this.CertificationDocuments.Add(newAnnualAccountReportCertificationDocument);
            this.ModifyDate = DateTime.Now;

            return newAnnualAccountReportCertificationDocument;
        }

        public void RemoveAnnualAccountReportCertificationDocument(int documentId)
        {
            var annualAccountReportCertificationDocument = this.FindAnnualAccountReportCertificationDocument(documentId);

            this.CertificationDocuments.Remove(annualAccountReportCertificationDocument);
            this.ModifyDate = DateTime.Now;
        }

        public AnnualAccountReportCertificationDocument FindAnnualAccountReportCertificationDocument(int documentId)
        {
            var document = this.CertificationDocuments.Single(d => d.AnnualAccountReportCertificationDocumentId == documentId);

            if (document == null)
            {
                throw new DomainObjectNotFoundException("Cannot find AnnualAccountReportCertificationDocument with id " + documentId);
            }

            return document;
        }

        public void UpdateAnnualAccountReportCertificationDocument(
            int documentId,
            string name,
            string description,
            Guid? blobKey)
        {
            var annualAccountReportCertificationDocument = this.FindAnnualAccountReportCertificationDocument(documentId);

            annualAccountReportCertificationDocument.SetAttributes(
                name,
                description,
                blobKey);

            this.ModifyDate = DateTime.Now;
        }

        #endregion

        #region AuditDocuments

        public AnnualAccountReportAuditDocument CreateAnnualAccountReportAuditDocument(
            string name,
            string description,
            Guid? blobKey)
        {
            var newAnnualAccountReportAuditDocument = new AnnualAccountReportAuditDocument()
            {
                AnnualAccountReportId = this.AnnualAccountReportId,
                Name = name,
                Description = description,
                BlobKey = blobKey,
            };

            this.AuditDocuments.Add(newAnnualAccountReportAuditDocument);
            this.ModifyDate = DateTime.Now;

            return newAnnualAccountReportAuditDocument;
        }

        public void RemoveAnnualAccountReportAuditDocument(int documentId)
        {
            var annualAccountReportAuditDocument = this.FindAnnualAccountReportAuditDocument(documentId);

            this.AuditDocuments.Remove(annualAccountReportAuditDocument);
            this.ModifyDate = DateTime.Now;
        }

        public AnnualAccountReportAuditDocument FindAnnualAccountReportAuditDocument(int documentId)
        {
            var document = this.AuditDocuments.Single(d => d.AnnualAccountReportAuditDocumentId == documentId);

            if (document == null)
            {
                throw new DomainObjectNotFoundException("Cannot find AnnualAccountReportAuditDocument with id " + documentId);
            }

            return document;
        }

        public void UpdateAnnualAccountReportAuditDocument(int documentId, string name, string description, Guid? blobKey)
        {
            var annualAccountReportAuditDocument = this.FindAnnualAccountReportAuditDocument(documentId);

            annualAccountReportAuditDocument.SetAttributes(
                name,
                description,
                blobKey);

            this.ModifyDate = DateTime.Now;
        }

        #endregion

        #region AnnualAccountReportCertReport

        public AnnualAccountReportCertReport FindCertReportAttachedCertReport(int attachedCertReportId)
        {
            var attachedCertReport = this.CertReports.Single(d => d.CertReportId == attachedCertReportId);

            if (attachedCertReport == null)
            {
                throw new DomainObjectNotFoundException("Cannot find CertReportAttachedCertReport with id " + attachedCertReportId);
            }

            return attachedCertReport;
        }

        public void AttachCertReport(int attachedCertReportId)
        {
            this.AssertIsDraft();

            var newAttachedCertReport = new AnnualAccountReportCertReport()
            {
                AnnualAccountReportId = this.AnnualAccountReportId,
                CertReportId = attachedCertReportId,
            };

            this.CertReports.Add(newAttachedCertReport);
            this.ModifyDate = DateTime.Now;
        }

        public void RemoveAttachedCertReport(int attachedCertReportId)
        {
            this.AssertIsDraft();

            var attachedCertReport = this.FindCertReportAttachedCertReport(attachedCertReportId);

            this.CertReports.Remove(attachedCertReport);
            this.ModifyDate = DateTime.Now;
        }

        public void AddCertReports(IList<int> unattachedCertReports)
        {
            foreach (var certReportId in unattachedCertReports)
            {
                this.AttachCertReport(certReportId);
            }
        }

        #endregion CertReportCertReport

        #region AnnualAccountReportContractReportCorrection

        public AnnualAccountReportContractReportCorrection FindAnnualAccountReportContractReportCorrection(int correctionId)
        {
            var correction = this.Corrections.Single(d => d.ContractReportCorrectionId == correctionId);

            if (correction == null)
            {
                throw new DomainObjectNotFoundException("Cannot find AnnualAccountReportContractReportCorrection with id " + correctionId);
            }

            return correction;
        }

        public void RemoveContractReportCorrection(int correctionId)
        {
            this.AssertIsDraft();

            var correction = this.FindAnnualAccountReportContractReportCorrection(correctionId);

            this.Corrections.Remove(correction);
            this.ModifyDate = DateTime.Now;
        }

        public void AddContractReportCorrections(int[] contractReportCorrectionIds)
        {
            this.AssertIsDraft();
            foreach (var correctionId in contractReportCorrectionIds)
            {
                this.Corrections.Add(new AnnualAccountReportContractReportCorrection()
                {
                    AnnualAccountReportId = this.AnnualAccountReportId,
                    ContractReportCorrectionId = correctionId,
                });
            }

            this.ModifyDate = DateTime.Now;
        }

        #endregion AnnualAccountReportContractReportCorrection

        #region AnnualAccountReportFinnancialCorrectionsCSD

        public void AddFinnancialCorrectionsCSDs(int[] contractReportFinancialCorrectionCSDIds)
        {
            this.AssertIsDraft();

            foreach (var csdId in contractReportFinancialCorrectionCSDIds)
            {
                this.FinancialCorrectionCSDs.Add(new AnnualAccountReportFinancialCorrectionCSD()
                {
                    AnnualAccountReportId = this.AnnualAccountReportId,
                    ContractReportFinancialCorrectionCSDId = csdId,
                });
            }

            this.ModifyDate = DateTime.Now;
        }

        public void DeleteContractReportFinancialCorrectionCSDs(int[] contractReportFinancialCorrectionCSDIds)
        {
            this.AssertIsDraft();

            foreach (var csdid in contractReportFinancialCorrectionCSDIds)
            {
                this.FinancialCorrectionCSDs.Remove(this.FindAnnualAccountReportFinancialCorrectionCSD(csdid));
            }

            this.ModifyDate = DateTime.Now;
        }

        public AnnualAccountReportFinancialCorrectionCSD FindAnnualAccountReportFinancialCorrectionCSD(int csdId)
        {
            var financialCorrection = this.FinancialCorrectionCSDs.Single(d => d.ContractReportFinancialCorrectionCSDId == csdId);

            if (financialCorrection == null)
            {
                throw new DomainObjectNotFoundException("Cannot find AnnualAccountReportFinancialCorrectionCSD with id " + csdId);
            }

            return financialCorrection;
        }

        #endregion AnnualAccountReportFinnancialCorrectionsCSD

        #region AnnualAccountCertifiedFinnancialCorrectionsCSD

        public void AddCertFinnancialCorrectionsCSDs(int[] unattachedCsds)
        {
            this.AssertIsDraft();

            foreach (var unattachedCsd in unattachedCsds)
            {
                this.CertifiedFinancialCorrectionCSDs.Add(new AnnualAccountReportCertFinancialCorrectionCSD()
                {
                    AnnualAccountReportId = this.AnnualAccountReportId,
                    ContractReportCertAuthorityFinancialCorrectionCSDId = unattachedCsd,
                });
            }

            this.ModifyDate = DateTime.Now;
        }

        public void DeleteCertFinancialCorrectionCSDs(int[] certFinancialCorrectionCSDIds)
        {
            this.AssertIsDraft();

            foreach (var csdid in certFinancialCorrectionCSDIds)
            {
                this.CertifiedFinancialCorrectionCSDs.Remove(this.FindAnnualAccountReportCertFinancialCorrectionCSD(csdid));
            }

            this.ModifyDate = DateTime.Now;
        }

        public AnnualAccountReportCertFinancialCorrectionCSD FindAnnualAccountReportCertFinancialCorrectionCSD(int csdId)
        {
            var financialCorrection = this.CertifiedFinancialCorrectionCSDs.Single(d => d.ContractReportCertAuthorityFinancialCorrectionCSDId == csdId);

            if (financialCorrection == null)
            {
                throw new DomainObjectNotFoundException("Cannot find AnnualAccountReportCertFinancialCorrectionCSD with id " + csdId);
            }

            return financialCorrection;
        }

        #endregion AnnualAccountCertifiedFinnancialCorrectionsCSD

        #region AnnualAccountReportCertAuthorityCorrection

        public AnnualAccountReportCertCorrection FindAnnualAccountReportContractReportCertCorrection(int certCorrectionId)
        {
            var correction = this.CertifiedCorrections.Single(d => d.ContractReportCertAuthorityCorrectionId == certCorrectionId);

            if (correction == null)
            {
                throw new DomainObjectNotFoundException("Cannot find AnnualAccountReportContractReportCorrection with id " + certCorrectionId);
            }

            return correction;
        }

        public void RemoveContractReportCertCorrection(int certCorrectionId)
        {
            this.AssertIsDraft();

            var certCorrection = this.FindAnnualAccountReportContractReportCertCorrection(certCorrectionId);

            this.CertifiedCorrections.Remove(certCorrection);
            this.ModifyDate = DateTime.Now;
        }

        public void AddContractReportCertCorrections(int[] certCorrectionIds)
        {
            this.AssertIsDraft();
            foreach (var correctionId in certCorrectionIds)
            {
                this.CertifiedCorrections.Add(new AnnualAccountReportCertCorrection()
                {
                    AnnualAccountReportId = this.AnnualAccountReportId,
                    ContractReportCertAuthorityCorrectionId = correctionId,
                });
            }

            this.ModifyDate = DateTime.Now;
        }

        #endregion AnnualAccountReportCertAuthorityCorrection

        #region AnnualAccountReportCertRevalidationCorrection

        public void AddContractReportCertRevalidationCorrections(int[] certRevalidationCorrectionIds)
        {
            if (certRevalidationCorrectionIds.Length == 0)
            {
                return;
            }

            this.AssertIsDraft();
            foreach (var correctionId in certRevalidationCorrectionIds)
            {
                this.CertifiedRevalidationCorrections.Add(new AnnualAccountReportCertRevalidationCorrection()
                {
                    AnnualAccountReportId = this.AnnualAccountReportId,
                    ContractReportRevalidationCertAuthorityCorrectionId = correctionId,
                });
            }

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveContractReportCertRevalidationCorrection(int certRevalidationCorrectionId)
        {
            this.AssertIsDraft();

            var certRevalidationCorrection = this.FindAnnualAccountReportContractReportCertRevalidationCorrection(certRevalidationCorrectionId);

            this.CertifiedRevalidationCorrections.Remove(certRevalidationCorrection);
            this.ModifyDate = DateTime.Now;
        }

        private AnnualAccountReportCertRevalidationCorrection FindAnnualAccountReportContractReportCertRevalidationCorrection(int certRevalidationCorrectionId)
        {
            var correction = this.CertifiedRevalidationCorrections.Single(d => d.ContractReportRevalidationCertAuthorityCorrectionId == certRevalidationCorrectionId);

            if (correction == null)
            {
                throw new DomainObjectNotFoundException("Cannot find AnnualAccountReportContractReportRevalidationCorrection with id " + certRevalidationCorrectionId);
            }

            return correction;
        }

        #endregion AnnualAccountReportCertRevalidationCorrection

        #region AnnualAccountCertRevalidationFinnancialCorrectionsCSD

        public void AddCertRevalidationFinnancialCorrectionsCSDs(int[] unattachedCsds)
        {
            this.AssertIsDraft();

            foreach (var unattachedCsd in unattachedCsds)
            {
                this.CertifiedRevalidationFinancialCorrectionCSDs.Add(new AnnualAccountReportCertRevalidationFinancialCorrectionCSD()
                {
                    AnnualAccountReportId = this.AnnualAccountReportId,
                    ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId = unattachedCsd,
                });
            }

            this.ModifyDate = DateTime.Now;
        }

        public void DeleteCertRevalidationFinancialCorrectionCSDs(int[] certRevalidationFinancialCorrectionCSDIds)
        {
            this.AssertIsDraft();

            foreach (var csdid in certRevalidationFinancialCorrectionCSDIds)
            {
                this.CertifiedRevalidationFinancialCorrectionCSDs.Remove(this.FindAnnualAccountReportCertRevalidationFinancialCorrectionCSD(csdid));
            }

            this.ModifyDate = DateTime.Now;
        }

        private AnnualAccountReportCertRevalidationFinancialCorrectionCSD FindAnnualAccountReportCertRevalidationFinancialCorrectionCSD(int csdId)
        {
            var financialCorrection = this.CertifiedRevalidationFinancialCorrectionCSDs.Single(d => d.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId == csdId);

            if (financialCorrection == null)
            {
                throw new DomainObjectNotFoundException("Cannot find AnnualAccountReportCertRevalidationFinancialCorrectionCSD with id " + csdId);
            }

            return financialCorrection;
        }

        #endregion AnnualAccountCertRevalidationFinnancialCorrectionsCSD

        #region Appendices

        public void AddAnnualAccountReportAppendices(int[] programmePriorities)
        {
            foreach (int programmePriorityId in programmePriorities)
            {
                this.AddAppendixPair(programmePriorityId);
            }
        }

        public void CreateNewAppendix(AnnualAccountReportAppendixCreateDO appendixCreateDO)
        {
            this.Appendices.Add(new AnnualAccountReportAppendix()
            {
                AnnualAccountReportId = this.AnnualAccountReportId,
                ProgrammePriorityId = appendixCreateDO.ProgrammePriorityId,
                Type = appendixCreateDO.Type,
                Comment = appendixCreateDO.Comment,
            });
        }

        public void UpdateAnnualAccountReportAppendices(int[] programmePriorities)
        {
            foreach (int programmePriorityId in programmePriorities)
            {
                var foundItems = this.Appendices.Where(x => x.ProgrammePriorityId == programmePriorityId);
                if (!foundItems.Any())
                {
                    this.AddAppendixPair(programmePriorityId);
                }
            }

            var obsoleteItems = this.Appendices.Where(x => !programmePriorities.Contains(x.ProgrammePriorityId));
            foreach (var item in obsoleteItems)
            {
                this.Appendices.Remove(item);
            }
        }

        private void AddAppendixPair(int programmePriorityId)
        {
            this.Appendices.Add(new AnnualAccountReportAppendix()
            {
                AnnualAccountReportId = this.AnnualAccountReportId,
                ProgrammePriorityId = programmePriorityId,
                Type = AnnualAccountReportAppendixType.Appendix5,
            });

            this.Appendices.Add(new AnnualAccountReportAppendix()
            {
                AnnualAccountReportId = this.AnnualAccountReportId,
                ProgrammePriorityId = programmePriorityId,
                Type = AnnualAccountReportAppendixType.Appendix8,
            });
        }

        public AnnualAccountReportAppendix FindAnnualAccountReportAppendix(int annualAccountReportAppendixId)
        {
            var appendix = this.Appendices.Single(d => d.AnnualAccountReportAppendixId == annualAccountReportAppendixId);

            if (appendix == null)
            {
                throw new DomainObjectNotFoundException("Cannot find AnnualAccountReportAppendix with id " + annualAccountReportAppendixId);
            }

            return appendix;
        }

        public void UpdateAnnualAccountReportAppendix(int appendixId, string comment)
        {
            var appendix = this.Appendices.Where(x => x.AnnualAccountReportAppendixId == appendixId).Single();
            appendix.Comment = comment;

            this.ModifyDate = DateTime.Now;
        }

        #endregion Appendices

    }
}
