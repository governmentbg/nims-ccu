using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Domain.AnnualAccountReports;
using Eumis.Domain.Contracts;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.OperationalMap.ProcedureManuals;
using Eumis.Domain.Procedures;
using Eumis.Domain.Procedures.ProcedureContractReportDocuments;
using Eumis.Domain.Projects;

namespace Eumis.Data.Core.Relations
{
    internal class RelationsRepository : Repository, IRelationsRepository
    {
        public RelationsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool AnnualAccountReportHasContractReportCorrection(int annualAccountReportId, int contractReportCorrectionId)
        {
            return this.unitOfWork.DbContext.Set<AnnualAccountReportContractReportCorrection>()
                .Any(rc => rc.AnnualAccountReportId == annualAccountReportId && rc.ContractReportCorrectionId == contractReportCorrectionId);
        }

        public void AssertAnnualAccountReportHasContractReportCorrection(int annualAccountReportId, int contractReportCorrectionId)
            => this.Assert(this.AnnualAccountReportHasContractReportCorrection(annualAccountReportId, contractReportCorrectionId));

        public bool AnnualAccountReportHasContractReportFinancialCorrection(int annualAccountReportId, int contractReportFinancialCorrectionId)
        {
            return (from arfc in this.unitOfWork.DbContext.Set<AnnualAccountReportFinancialCorrectionCSD>()
                    join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>() on arfc.ContractReportFinancialCorrectionCSDId equals csd.ContractReportFinancialCorrectionCSDId
                    where arfc.AnnualAccountReportId == annualAccountReportId && csd.ContractReportFinancialCorrectionId == contractReportFinancialCorrectionId
                    select arfc).Any();
        }

        public void AssertAnnualAccountReportHasContractReportFinancialCorrection(int annualAccountReportId, int contractReportFinancialCorrectionId)
            => this.Assert(this.AnnualAccountReportHasContractReportFinancialCorrection(annualAccountReportId, contractReportFinancialCorrectionId));

        public bool AnnualAccountReportHasContractReportFinancialCorrectionCSD(int annualAccountReportId, int? contractReportFinancialCorrectionId = null, int? contractReportFinancialCorrectionCSDId = null)
        {
            if (!contractReportFinancialCorrectionId.HasValue && !contractReportFinancialCorrectionCSDId.HasValue)
            {
                throw new ArgumentException("At least one of the nullable parameters must be set");
            }

            var corrections = from arfc in this.unitOfWork.DbContext.Set<AnnualAccountReportFinancialCorrectionCSD>()
                              join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>() on arfc.ContractReportFinancialCorrectionCSDId equals csd.ContractReportFinancialCorrectionCSDId
                              where arfc.AnnualAccountReportId == annualAccountReportId
                              select csd;

            if (contractReportFinancialCorrectionId.HasValue)
            {
                corrections = corrections.Where(c => c.ContractReportFinancialCorrectionId == contractReportFinancialCorrectionId);
            }

            if (contractReportFinancialCorrectionCSDId.HasValue)
            {
                corrections = corrections.Where(c => c.ContractReportFinancialCorrectionCSDId == contractReportFinancialCorrectionCSDId);
            }

            return corrections.Any();
        }

        public void AssertAnnualAccountReportHasContractReportFinancialCorrectionCSD(int annualAccountReportId, int? contractReportFinancialCorrectionId = null, int? contractReportFinancialCorrectionCSDId = null)
            => this.Assert(this.AnnualAccountReportHasContractReportFinancialCorrectionCSD(annualAccountReportId, contractReportFinancialCorrectionId, contractReportFinancialCorrectionCSDId));

        public bool AnnualAccountReportHasCertifiedFinancialCorrectionCSD(int annualAccountReportId, int? contractReportCertAuthorityFinancialCorrectionId = null, int? contractReportCertAuthorityFinancialCorrectionCSDId = null)
        {
            if (!contractReportCertAuthorityFinancialCorrectionId.HasValue && !contractReportCertAuthorityFinancialCorrectionCSDId.HasValue)
            {
                throw new ArgumentException("At least one of the nullable parameters must be set");
            }

            var corrections = from arfc in this.unitOfWork.DbContext.Set<AnnualAccountReportCertFinancialCorrectionCSD>()
                              join csd in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrectionCSD>() on arfc.ContractReportCertAuthorityFinancialCorrectionCSDId equals csd.ContractReportCertAuthorityFinancialCorrectionCSDId
                              where arfc.AnnualAccountReportId == annualAccountReportId
                              select csd;

            if (contractReportCertAuthorityFinancialCorrectionId.HasValue)
            {
                corrections = corrections.Where(c => c.ContractReportCertAuthorityFinancialCorrectionId == contractReportCertAuthorityFinancialCorrectionId);
            }

            if (contractReportCertAuthorityFinancialCorrectionCSDId.HasValue)
            {
                corrections = corrections.Where(c => c.ContractReportCertAuthorityFinancialCorrectionCSDId == contractReportCertAuthorityFinancialCorrectionCSDId);
            }

            return corrections.Any();
        }

        public void AssertAnnualAccountReportHasCertifiedFinancialCorrectionCSD(int annualAccountReportId, int? contractReportCertAuthorityFinancialCorrectionId = null, int? contractReportCertAuthorityFinancialCorrectionCSDId = null)
            => this.Assert(this.AnnualAccountReportHasCertifiedFinancialCorrectionCSD(annualAccountReportId, contractReportCertAuthorityFinancialCorrectionId, contractReportCertAuthorityFinancialCorrectionCSDId));

        public bool AnnualAccountReportHasCertifiedCorrection(int annualAccountReportId, int contractReportCertifiedCorrectionId)
        {
            return this.unitOfWork.DbContext.Set<AnnualAccountReportCertCorrection>()
                .Any(rc => rc.AnnualAccountReportId == annualAccountReportId && rc.ContractReportCertAuthorityCorrectionId == contractReportCertifiedCorrectionId);
        }

        public void AssertAnnualAccountReportHasCertifiedCorrection(int annualAccountReportId, int contractReportCertifiedCorrectionId)
            => this.Assert(this.AnnualAccountReportHasCertifiedCorrection(annualAccountReportId, contractReportCertifiedCorrectionId));

        public bool AnnualAccountReportHasCertifiedFinancialCorrection(int annualAccountReportId, int certifiedFinancialCorrectionId)
        {
            return (from arcc in this.unitOfWork.DbContext.Set<AnnualAccountReportCertFinancialCorrectionCSD>()
                    join csd in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrectionCSD>() on arcc.ContractReportCertAuthorityFinancialCorrectionCSDId equals csd.ContractReportCertAuthorityFinancialCorrectionCSDId
                    where arcc.AnnualAccountReportId == annualAccountReportId && csd.ContractReportCertAuthorityFinancialCorrectionId == certifiedFinancialCorrectionId
                    select arcc).Any();
        }

        public void AssertAnnualAccountReportHasCertifiedFinancialCorrection(int annualAccountReportId, int certifiedFinancialCorrectionId)
            => this.Assert(this.AnnualAccountReportHasCertifiedFinancialCorrection(annualAccountReportId, certifiedFinancialCorrectionId));

        public bool AnnualAccountReportHasCertifiedRevalidationCorrection(int annualAccountReportId, int contractReportCertifiedRevalidationCorrectionId)
        {
            return this.unitOfWork.DbContext.Set<AnnualAccountReportCertRevalidationCorrection>()
                .Any(rc => rc.AnnualAccountReportId == annualAccountReportId && rc.ContractReportRevalidationCertAuthorityCorrectionId == contractReportCertifiedRevalidationCorrectionId);
        }

        public void AssertAnnualAccountReportHasCertifiedRevalidationCorrection(int annualAccountReportId, int contractReportCertifiedRevalidationCorrectionId)
            => this.Assert(this.AnnualAccountReportHasCertifiedRevalidationCorrection(annualAccountReportId, contractReportCertifiedRevalidationCorrectionId));

        public bool AnnualAccountReportHasAttachedCertReport(int annualAccountReportId, int attachedCertReportId)
        {
            return this.unitOfWork.DbContext.Set<AnnualAccountReportCertReport>()
                .Any(rc => rc.AnnualAccountReportId == annualAccountReportId && rc.CertReportId == attachedCertReportId);
        }

        public void AssertAnnualAccountReportHasAttachedCertReport(int annualAccountReportId, int attachedCertReportId)
            => this.Assert(this.AnnualAccountReportHasAttachedCertReport(annualAccountReportId, attachedCertReportId));

        public bool AnnualAccountReportHasAuditDocument(int annualAccountReportId, int documentId)
        {
            return this.unitOfWork.DbContext.Set<AnnualAccountReportAuditDocument>()
                .Any(ad => ad.AnnualAccountReportId == annualAccountReportId && ad.AnnualAccountReportAuditDocumentId == documentId);
        }

        public void AssertAnnualAccountReportHasAuditDocument(int annualAccountReportId, int documentId)
            => this.Assert(this.AnnualAccountReportHasAuditDocument(annualAccountReportId, documentId));

        public bool AnnualAccountReportHasCertificationDocument(int annualAccountReportId, int documentId)
        {
            return this.unitOfWork.DbContext.Set<AnnualAccountReportCertificationDocument>()
                .Any(cd => cd.AnnualAccountReportId == annualAccountReportId && cd.AnnualAccountReportCertificationDocumentId == documentId);
        }

        public void AssertAnnualAccountReportHasCertificationDocument(int annualAccountReportId, int documentId)
            => this.Assert(this.AnnualAccountReportHasCertificationDocument(annualAccountReportId, documentId));

        public bool AnnualAccountReportHasAppendix(int annualAccountReportId, int appendixId)
        {
            return this.unitOfWork.DbContext.Set<AnnualAccountReportAppendix>()
                .Any(ax => ax.AnnualAccountReportId == annualAccountReportId && ax.AnnualAccountReportAppendixId == appendixId);
        }

        public void AssertAnnualAccountReportHasAppendix(int annualAccountReportId, int appendixId)
            => this.Assert(this.AnnualAccountReportHasAppendix(annualAccountReportId, appendixId));

        public bool ProjectHasContract(int projectId, int contractId)
        {
            return this.unitOfWork.DbContext.Set<Contract>()
                .Any(c => c.ContractId == contractId && c.ProjectId == projectId);
        }

        public void AssertProjectHasContract(int projectId, int contractId)
            => this.Assert(this.ProjectHasContract(projectId, contractId));

        public bool ContractHasSpendingPlan(int contractId, int spendingPlanId)
        {
            return this.unitOfWork.DbContext.Set<ContractSpendingPlanXml>().Any(csp =>
                csp.ContractSpendingPlanXmlId == spendingPlanId && csp.ContractId == contractId);
        }

        public void AssertContractHasSpendingPlan(int contractId, int spendingPlanId)
            => this.Assert(this.ContractHasSpendingPlan(contractId, spendingPlanId));

        public bool ContractHasAccessCode(int contractId, int accessCodeId)
        {
            return this.unitOfWork.DbContext.Set<ContractAccessCode>().Any(cac =>
                cac.ContractAccessCodeId == accessCodeId && cac.ContractId == contractId);
        }

        public void AssertContractHasAccessCode(int contractId, int accessCodeId)
            => this.Assert(this.ContractHasAccessCode(contractId, accessCodeId));

        public bool ContractHasProcurement(int contractId, int procurementId)
        {
            return this.unitOfWork.DbContext.Set<ContractProcurementXml>().Any(cp =>
                cp.ContractProcurementXmlId == procurementId && cp.ContractId == contractId);
        }

        public void AssertContractHasProcurement(int contractId, int procurementId)
            => this.Assert(this.ContractHasProcurement(contractId, procurementId));

        public bool ProjectHasProjectCommunication(int projectId, int communicationId)
        {
            return this.unitOfWork.DbContext.Set<ProjectCommunication>().Any(pc =>
                pc.ProjectCommunicationId == communicationId && pc.ProjectId == projectId);
        }

        public void AssertProjectHasProjectCommunication(int projectId, int communicationId)
            => this.Assert(this.ProjectHasProjectCommunication(projectId, communicationId));

        public bool ProjectHasManagingAuthorityCommunication(int projectId, int communicationId)
        {
            return this.unitOfWork.DbContext.Set<ProjectManagingAuthorityCommunication>().Any(pc =>
                pc.ProjectCommunicationId == communicationId && pc.ProjectId == projectId);
        }

        public void AssertProjectHasManagingAuthorityCommunication(int projectId, int communicationId)
            => this.Assert(this.ProjectHasManagingAuthorityCommunication(projectId, communicationId));

        public bool ProjectCommunicationHasProjectCommunicationFile(int communicationId, int projectCommunicationFileId)
        {
            return this.unitOfWork.DbContext.Set<ProjectCommunicationFile>().Any(pcf =>
                pcf.ProjectCommunicationFileId == projectCommunicationFileId && pcf.ProjectCommunicationId == communicationId);
        }

        public void AssertProjectCommunicationHasProjectCommunicationFile(int communicationId, int projectCommunicationFileId)
            => this.Assert(this.ProjectCommunicationHasProjectCommunicationFile(communicationId, projectCommunicationFileId));

        public bool ProjectHasProjectFile(int projectId, int projectFileId)
        {
            return (from pvx in this.unitOfWork.DbContext.Set<ProjectVersionXml>()
                    join pf in this.unitOfWork.DbContext.Set<ProjectFile>() on pvx.ProjectVersionXmlId equals pf.ProjectVersionXmlId
                    where pf.ProjectFileId == projectFileId && pvx.ProjectId == projectId
                    select pf).Any();
        }

        public void AssertProjectHasProjectFile(int projectId, int projectFileId)
            => this.Assert(this.ProjectHasProjectFile(projectId, projectFileId));

        public bool ContractHasCommunication(int contractId, int communicationId)
        {
            return this.unitOfWork.DbContext.Set<ContractCommunicationXml>().Any(c =>
                c.ContractCommunicationXmlId == communicationId && c.ContractId == contractId);
        }

        public void AssertContractHasCommunication(int contractId, int communicationId)
            => this.Assert(this.ContractHasCommunication(contractId, communicationId));

        public bool CertReportHasContractReportCertCorrection(int certReportId, int contractReportCertCorrectionId)
        {
            return this.unitOfWork.DbContext.Set<ContractReportCertCorrection>().Any(crcc =>
                crcc.ContractReportCertCorrectionId == contractReportCertCorrectionId && crcc.CertReportId == certReportId);
        }

        public void AssertCertReportHasContractReportCertCorrection(int certReportId, int contractReportCertCorrectionId)
            => this.Assert(this.CertReportHasContractReportCertCorrection(certReportId, contractReportCertCorrectionId));

        public bool CertReportHasContractReportCorrection(int certReportId, int contractReportCorrectionId)
        {
            return this.unitOfWork.DbContext.Set<ContractReportCorrection>().Any(crc =>
                crc.CertReportId == certReportId && crc.ContractReportCorrectionId == contractReportCorrectionId);
        }

        public void AssertCertReportHasContractReportCorrection(int certReportId, int contractReportCorrectionId)
            => this.Assert(this.CertReportHasContractReportCorrection(certReportId, contractReportCorrectionId));

        public bool ContractHasContractReportFinancialCorrection(int contractReportId, int contractReportFinancialCorrectionId)
        {
            return this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>().Any(crfc =>
                crfc.ContractReportFinancialCorrectionId == contractReportFinancialCorrectionId && crfc.ContractReportId == contractReportId);
        }

        public void AssertContractHasContractReportFinancialCorrection(int contractReportId, int contractReportFinancialCorrectionId)
            => this.Assert(this.ContractHasContractReportFinancialCorrection(contractReportId, contractReportFinancialCorrectionId));

        public bool ContractReportHasContractReportFinancialRevalidation(int contractReportId, int contractReportFinancialRevalidationId)
        {
            return this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidation>().Any(crfr =>
                crfr.ContractReportFinancialRevalidationId == contractReportFinancialRevalidationId && crfr.ContractReportId == contractReportId);
        }

        public void AssertContractReportHasContractReportFinancialRevalidation(int contractReportId, int contractReportFinancialRevalidationId)
            => this.Assert(this.ContractReportHasContractReportFinancialRevalidation(contractReportId, contractReportFinancialRevalidationId));

        public bool ContractReportHasContractReportPaymentCheck(int contractReportId, int contractReportPaymentCheckId)
        {
            return this.unitOfWork.DbContext.Set<ContractReportPaymentCheck>().Any(crpc =>
                crpc.ContractReportPaymentCheckId == contractReportPaymentCheckId && crpc.ContractReportId == contractReportId);
        }

        public void AssertContractReportHasContractReportPaymentCheck(int contractReportId, int contractReportPaymentCheckId)
            => this.Assert(this.ContractReportHasContractReportPaymentCheck(contractReportId, contractReportPaymentCheckId));

        public bool CertReportHasAdvancePaymentAmount(int certReportId, int? contractReportId = null, int? contractReportAdvancePaymentAmountId = null)
        {
            if (!contractReportId.HasValue && !contractReportAdvancePaymentAmountId.HasValue)
            {
                throw new ArgumentException("At least one of the nullable parameters must be set");
            }

            var predicate = PredicateBuilder.True<ContractReportAdvancePaymentAmount>();
            predicate.And(crapa => crapa.CertReportId == certReportId);

            if (contractReportId.HasValue)
            {
                predicate.And(crapa => crapa.ContractReportId == contractReportId);
            }

            if (contractReportAdvancePaymentAmountId.HasValue)
            {
                predicate.And(crapa => crapa.ContractReportAdvancePaymentAmountId == contractReportAdvancePaymentAmountId);
            }

            return this.unitOfWork.DbContext.Set<ContractReportAdvancePaymentAmount>().Any(predicate);
        }

        public void AssertCertReportHasAdvancePaymentAmount(int certReportId, int? contractReportId = null, int? contractReportAdvancePaymentAmountId = null)
            => this.Assert(this.CertReportHasAdvancePaymentAmount(certReportId, contractReportId, contractReportAdvancePaymentAmountId));

        public bool CertReportHasFinancialCorrectionCSD(int certReportId, int? contractReportFinancialCorrectionId = null, int? contractReportFinancialCorrectionCSDId = null)
        {
            if (!contractReportFinancialCorrectionId.HasValue && !contractReportFinancialCorrectionCSDId.HasValue)
            {
                throw new ArgumentException("At least one of the nullable parameters must be set");
            }

            var predicate = PredicateBuilder.True<ContractReportFinancialCorrectionCSD>();
            predicate.And(crfc => crfc.CertReportId == certReportId);

            if (contractReportFinancialCorrectionId.HasValue)
            {
                predicate.And(crfc => crfc.ContractReportFinancialCorrectionId == contractReportFinancialCorrectionId);
            }

            if (contractReportFinancialCorrectionCSDId.HasValue)
            {
                predicate.And(crfc => crfc.ContractReportFinancialCorrectionCSDId == contractReportFinancialCorrectionCSDId);
            }

            return this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>().Any(predicate);
        }

        public void AssertCertReportHasFinancialCorrectionCSD(int certReportId, int? contractReportFinancialCorrectionId = null, int? contractReportFinancialCorrectionCSDId = null)
            => this.Assert(this.CertReportHasFinancialCorrectionCSD(certReportId, contractReportFinancialCorrectionId, contractReportFinancialCorrectionCSDId));

        public bool CertReportHasFinancialCertCorrectionCSD(int certReportId, int? contractReportFinancialCertCorrectionId = null, int? contractReportFinancialCertCorrectionCSDId = null)
        {
            if (!contractReportFinancialCertCorrectionId.HasValue && !contractReportFinancialCertCorrectionCSDId.HasValue)
            {
                throw new ArgumentException("At least one of the nullable parameters must be set");
            }

            var predicate = PredicateBuilder.True<ContractReportFinancialCertCorrectionCSD>();
            predicate.And(crfcc => crfcc.CertReportId == certReportId);

            if (contractReportFinancialCertCorrectionId.HasValue)
            {
                predicate.And(crfcc => crfcc.ContractReportFinancialCertCorrectionId == contractReportFinancialCertCorrectionId);
            }

            if (contractReportFinancialCertCorrectionCSDId.HasValue)
            {
                predicate.And(crfcc => crfcc.ContractReportFinancialCertCorrectionCSDId == contractReportFinancialCertCorrectionCSDId);
            }

            return this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrectionCSD>().Any(predicate);
        }

        public void AssertCertReportHasFinancialCertCorrectionCSD(int certReportId, int? contractReportFinancialCertCorrectionId = null, int? contractReportFinancialCertCorrectionCSDId = null)
            => this.Assert(this.CertReportHasFinancialCertCorrectionCSD(certReportId, contractReportFinancialCertCorrectionId, contractReportFinancialCertCorrectionCSDId));

        public bool CertReportHasFinancialCSDBudgetItem(int certReportId, int? contractReportId = null, int? contractReportFinancialCSDBudgetItemId = null)
        {
            if (!contractReportId.HasValue && !contractReportFinancialCSDBudgetItemId.HasValue)
            {
                throw new ArgumentException("At least one of the nullable parameters must be set");
            }

            var predicate = PredicateBuilder.True<ContractReportFinancialCSDBudgetItem>();
            predicate.And(crfcbi => crfcbi.CertReportId == certReportId);

            if (contractReportId.HasValue)
            {
                predicate.And(crfcbi => crfcbi.ContractReportId == contractReportId);
            }

            if (contractReportFinancialCSDBudgetItemId.HasValue)
            {
                predicate.And(crfcbi => crfcbi.ContractReportFinancialCSDBudgetItemId == contractReportFinancialCSDBudgetItemId);
            }

            return this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>().Any(predicate);
        }

        public void AssertCertReportHasFinancialCSDBudgetItem(int certReportId, int? contractReportId = null, int? contractReportFinancialCSDBudgetItemId = null)
            => this.Assert(this.CertReportHasFinancialCSDBudgetItem(certReportId, contractReportId, contractReportFinancialCSDBudgetItemId));

        public bool CertReportHasContractReportRevalidation(int certReportId, int contractReportRevalidationId)
        {
            return this.unitOfWork.DbContext.Set<ContractReportRevalidation>().Any(crr =>
                crr.CertReportId == certReportId && crr.ContractReportRevalidationId == contractReportRevalidationId);
        }

        public void AssertCertReportHasContractReportRevalidation(int certReportId, int contractReportRevalidationId)
            => this.Assert(this.CertReportHasContractReportRevalidation(certReportId, contractReportRevalidationId));

        public bool CertReportHasFinancialRevalidationCSD(int certReportId, int? contractReportFinancialRevalidationId = null, int? contractReportFinancialRevalidationCSDId = null)
        {
            if (!contractReportFinancialRevalidationId.HasValue && !contractReportFinancialRevalidationCSDId.HasValue)
            {
                throw new ArgumentException("At least one of the nullable parameters must be set");
            }

            var predicate = PredicateBuilder.True<ContractReportFinancialRevalidationCSD>();
            predicate.And(crfr => crfr.CertReportId == certReportId);

            if (contractReportFinancialRevalidationId.HasValue)
            {
                predicate.And(crfr => crfr.ContractReportFinancialRevalidationId == contractReportFinancialRevalidationId);
            }

            if (contractReportFinancialRevalidationCSDId.HasValue)
            {
                predicate.And(crfr => crfr.ContractReportFinancialRevalidationCSDId == contractReportFinancialRevalidationCSDId);
            }

            return this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>().Any(predicate);
        }

        public void AssertCertReportHasFinancialRevalidationCSD(int certReportId, int? contractReportFinancialRevalidationId = null, int? contractReportFinancialRevalidationCSDId = null)
            => this.Assert(this.CertReportHasFinancialRevalidationCSD(certReportId, contractReportFinancialRevalidationId, contractReportFinancialRevalidationCSDId));

        public bool ContractReportFinancialCorrectionHasFinancialCSDBudgetItem(int contractReportFinancialCorrectionId, int contractReportFinancialCSDBudgetItemId)
        {
            return (from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>()
                    join csdbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on crfc.ContractReportFinancialId equals csdbi.ContractReportFinancialId
                    where crfc.ContractReportFinancialCorrectionId == contractReportFinancialCorrectionId &&
                          csdbi.ContractReportFinancialCSDBudgetItemId == contractReportFinancialCSDBudgetItemId
                    select crfc).Any();
        }

        public void AssertContractReportFinancialCorrectionHasFinancialCSDBudgetItem(int contractReportFinancialCorrectionId, int contractReportFinancialCSDBudgetItemId)
            => this.Assert(this.ContractReportFinancialCorrectionHasFinancialCSDBudgetItem(contractReportFinancialCorrectionId, contractReportFinancialCSDBudgetItemId));

        public bool ContractReportFinancialRevalidationHasFinancialCSDBudgetItem(int contractReportFinancialRevalidationId, int contractReportFinancialCSDBudgetItemId)
        {
            return this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>().Any(frcsd =>
                frcsd.ContractReportFinancialRevalidationId == contractReportFinancialRevalidationId &&
                frcsd.ContractReportFinancialCSDBudgetItemId == contractReportFinancialCSDBudgetItemId);
        }

        public void AssertContractReportFinancialRevalidationHasFinancialCSDBudgetItem(int contractReportFinancialRevalidationId, int contractReportFinancialCSDBudgetItemId)
            => this.Assert(this.ContractReportFinancialRevalidationHasFinancialCSDBudgetItem(contractReportFinancialRevalidationId, contractReportFinancialCSDBudgetItemId));

        public bool ContractReportFinancialCertCorrectionHasFinancialCSDBudgetItem(int contractReportFinancialCertCorrectionId, int contractReportFinancialCSDBudgetItemId)
        {
            return (from crfcc in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrection>()
                    join csdbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on crfcc.ContractReportFinancialId equals csdbi.ContractReportFinancialId
                    where crfcc.ContractReportFinancialCertCorrectionId == contractReportFinancialCertCorrectionId &&
                          csdbi.ContractReportFinancialCSDBudgetItemId == contractReportFinancialCSDBudgetItemId
                    select crfcc).Any();
        }

        public void AssertContractReportFinancialCertCorrectionHasFinancialCSDBudgetItem(int contractReportFinancialCertCorrectionId, int contractReportFinancialCSDBudgetItemId)
            => this.Assert(this.ContractReportFinancialCertCorrectionHasFinancialCSDBudgetItem(contractReportFinancialCertCorrectionId, contractReportFinancialCSDBudgetItemId));

        public bool ContractReportCertAuthorityFinancialCorrectionHasFinancialCSDBudgetItem(int contractReportCertAuthorityFinancialCorrectionId, int contractReportFinancialCSDBudgetItemId)
        {
            return (from crcafcc in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrection>()
                    join csdbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on crcafcc.ContractReportFinancialId equals csdbi.ContractReportFinancialId
                    where crcafcc.ContractReportCertAuthorityFinancialCorrectionId == contractReportCertAuthorityFinancialCorrectionId &&
                          csdbi.ContractReportFinancialCSDBudgetItemId == contractReportFinancialCSDBudgetItemId
                    select crcafcc).Any();
        }

        public void AssertContractReportCertAuthorityFinancialCorrectionHasFinancialCSDBudgetItem(int contractReportCertAuthorityFinancialCorrectionId, int contractReportFinancialCSDBudgetItemId)
            => this.Assert(this.ContractReportCertAuthorityFinancialCorrectionHasFinancialCSDBudgetItem(contractReportCertAuthorityFinancialCorrectionId, contractReportFinancialCSDBudgetItemId));

        public bool ContractReportRevalidationCertAuthorityFinancialCorrectionHasFinancialCSDBudgetItem(int contractReportRevalidationCertAuthorityFinancialCorrectionId, int contractReportFinancialCSDBudgetItemId)
        {
            return (from crrcafc in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityFinancialCorrection>()
                    join csdbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on crrcafc.ContractReportFinancialId equals csdbi.ContractReportFinancialId
                    where crrcafc.ContractReportRevalidationCertAuthorityFinancialCorrectionId == contractReportRevalidationCertAuthorityFinancialCorrectionId &&
                          csdbi.ContractReportFinancialCSDBudgetItemId == contractReportFinancialCSDBudgetItemId
                    select crrcafc).Any();
        }

        public void AssertContractReportRevalidationCertAuthorityFinancialCorrectionHasFinancialCSDBudgetItem(int contractReportRevalidationCertAuthorityFinancialCorrectionId, int contractReportFinancialCSDBudgetItemId)
            => this.Assert(this.ContractReportRevalidationCertAuthorityFinancialCorrectionHasFinancialCSDBudgetItem(contractReportRevalidationCertAuthorityFinancialCorrectionId, contractReportFinancialCSDBudgetItemId));

        public bool ContractReportTechnicalCorrectionHasContractReportIndicator(int contractReportTechnicalCorrectionId, int contractReportIndicatorId)
        {
            return (from crtc in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrection>()
                    join cri in this.unitOfWork.DbContext.Set<ContractReportIndicator>() on crtc.ContractReportTechnicalId equals cri.ContractReportTechnicalId
                    where crtc.ContractReportTechnicalCorrectionId == contractReportTechnicalCorrectionId &&
                          cri.ContractReportIndicatorId == contractReportIndicatorId
                    select crtc).Any();
        }

        public void AssertContractReportTechnicalCorrectionHasContractReportIndicator(int contractReportTechnicalCorrectionId, int contractReportIndicatorId)
            => this.Assert(this.ContractReportTechnicalCorrectionHasContractReportIndicator(contractReportTechnicalCorrectionId, contractReportIndicatorId));

        public bool ContractHasVersion(int contractId, int versionId)
        {
            return this.unitOfWork.DbContext.Set<ContractVersionXml>().Any(cv => cv.ContractId == contractId && cv.ContractVersionXmlId == versionId);
        }

        public void AssertContractHasVersion(int contractId, int versionId)
            => this.Assert(this.ContractHasVersion(contractId, versionId));

        public bool ЕvalSessionHasЕvaluation(int evalSessionId, int evaluationId)
        {
            return this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Any(ese => ese.EvalSessionId == evalSessionId && ese.EvalSessionEvaluationId == evaluationId);
        }

        public void AssertЕvalSessionHasЕvaluation(int evalSessionId, int evaluationId)
            => this.Assert(this.ЕvalSessionHasЕvaluation(evalSessionId, evaluationId));

        public bool ЕvalSessionHasStanding(int evalSessionId, int standingId)
        {
            return this.unitOfWork.DbContext.Set<EvalSessionStandingProject>().Any(essp => essp.EvalSessionId == evalSessionId && essp.EvalSessionStandingId == standingId);
        }

        public void AssertЕvalSessionHasStanding(int evalSessionId, int standingId)
            => this.Assert(this.ЕvalSessionHasStanding(evalSessionId, standingId));

        public bool EvalSessionHasResult(int evalSessionId, int resultId)
        {
            return this.unitOfWork.DbContext.Set<EvalSessionResult>().Any(esr => esr.EvalSessionId == evalSessionId && esr.EvalSessionResultId == resultId);
        }

        public void AssertEvalSessionHasResult(int evalSessionId, int resultId)
            => this.Assert(this.EvalSessionHasResult(evalSessionId, resultId));

        public bool ProcedureHasContractReportDocument(int procedureId, int contractReportDocumentId)
        {
            return this.unitOfWork.DbContext.Set<ProcedureContractReportDocument>().Any(pcrd => pcrd.ProcedureId == procedureId && pcrd.ProcedureContractReportDocumentId == contractReportDocumentId);
        }

        public void AssertProcedureHasContractReportDocument(int procedureId, int contractReportDocumentId)
        => this.Assert(this.ProcedureHasContractReportDocument(procedureId, contractReportDocumentId));

        public bool ProgrammeHasProcedureManual(int programmeId, int programmeProcedureManualId)
        {
            return this.unitOfWork.DbContext.Set<ProgrammeProcedureManual>().Any(pm => pm.MapNodeId == programmeId && pm.ProgrammeProcedureManualId == programmeProcedureManualId);
        }

        public void AssertProgrammeHasProcedureManual(int programmeId, int programmeProcedureManualId)
            => this.Assert(this.ProgrammeHasProcedureManual(programmeId, programmeProcedureManualId));

        public bool AnnualAccountReportHasCertifiedRevalidationFinancialCorrectionCSD(int annualAccountReportId, int? contractReportRevalidationCertAuthorityFinancialCorrectionId = null, int? contractReportRevalidationCertAuthorityFinancialCorrectionCSDId = null)
        {
            if (!contractReportRevalidationCertAuthorityFinancialCorrectionId.HasValue && !contractReportRevalidationCertAuthorityFinancialCorrectionCSDId.HasValue)
            {
                throw new ArgumentException("At least one of the nullable parameters must be set");
            }

            var corrections = from arfc in this.unitOfWork.DbContext.Set<AnnualAccountReportCertRevalidationFinancialCorrectionCSD>()
                              join csd in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityFinancialCorrectionCSD>() on arfc.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId equals csd.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId
                              where arfc.AnnualAccountReportId == annualAccountReportId
                              select csd;

            if (contractReportRevalidationCertAuthorityFinancialCorrectionId.HasValue)
            {
                corrections = corrections.Where(c => c.ContractReportRevalidationCertAuthorityFinancialCorrectionId == contractReportRevalidationCertAuthorityFinancialCorrectionId);
            }

            if (contractReportRevalidationCertAuthorityFinancialCorrectionCSDId.HasValue)
            {
                corrections = corrections.Where(c => c.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId == contractReportRevalidationCertAuthorityFinancialCorrectionCSDId);
            }

            return corrections.Any();
        }

        public void AssertAnnualAccountReportHasCertifiedRevalidationFinancialCorrectionCSD(int annualAccountReportId, int? contractReportRevalidationCertAuthorityFinancialCorrectionId = null, int? contractReportRevalidationCertAuthorityFinancialCorrectionCSDId = null)
            => this.Assert(this.AnnualAccountReportHasCertifiedRevalidationFinancialCorrectionCSD(annualAccountReportId, contractReportRevalidationCertAuthorityFinancialCorrectionId, contractReportRevalidationCertAuthorityFinancialCorrectionCSDId));

        public void AssertProcedureHasDeclaration(int procedureId, int procedureDeclarationId)
            => this.Assert(this.ProcedureHasDeclaration(procedureId, procedureDeclarationId));

        public bool ProcedureHasDeclaration(int procedureId, int procedureDeclarationId)
        {
            return this.unitOfWork.DbContext.Set<ProcedureAppFormDeclaration>().Any(d => d.ProcedureId == procedureId && d.ProcedureDeclarationId == procedureDeclarationId);
        }

        private void Assert(bool assertion, [CallerMemberName]string methodName = null)
        {
            if (!assertion)
            {
                throw new DataException($"Relation assertion failed - {methodName}");
            }
        }
    }
}
