using Eumis.Data.AnnualAccountReports.ViewObjects;
using Eumis.Domain.AnnualAccountReports;
using Eumis.Domain.AnnualAccountReports.ViewObjects;
using Eumis.Domain.CertReports.ViewObjects;
using Eumis.Domain.Contracts.ViewObjects;
using System;
using System.Collections.Generic;

namespace Eumis.Data.AnnualAccountReports.Repositories
{
    public interface IAnnualAccountReportsRepository : IAggregateRepository<AnnualAccountReport>
    {
        IList<AnnualAccountReportVO> GetAnnualAccountReports(int[] programmeIds);

        int GetProgrammeId(int annualAccountReportId);

        AnnualAccountReportInfoVO GetInfo(int annualAccountReportId);

        int GetNextOrderNum(int programmeId);

        IList<AnnualAccountReportCertificationDocumentVO> GetAnnualAccountReportCertificationDocuments(int annualAccountReportId);

        IList<AnnualAccountReportAuditDocumentVO> GetAnnualAccountReportAuditDocuments(int annualAccountReportId);

        IList<CertReportVO> GetCertReportsForAnnualAccountReportAttachedCertReports(int annualAccountReport);

        IList<CertReportVO> GetAnnualAccountReportAttachedCertReports(int annualAccountReportId);

        IList<ContractReportFinancialCorrectionVO> GetContractReportCorrectionsForAnnualAccountReportFinancialCorrections(int annualAccountReport);

        IList<AnnualAccountReportFinancialCorrectionVO> GetAnnualAccountReportFinancialCorrections(int annualAccountReportId);

        int[] FindAllUnattachedFinancialCorrectionCSDs(int contractReportFinancialCorrectionId);

        int[] FindAllAttachedFinancialCorrectionCSDs(int annualAccountReportId, int contractReportFinancialCorrectionId);

        IList<ContractReportFinancialCorrectionCSDsVO> GetContractReportFinancialCorrectionCSDs(int annualAccountReportId, int contractReportFinancialCorrectionId);

        IList<ContractReportFinancialCorrectionCSDsVO> GetFinancialCorrectionCSDsForContractReportFinancialCorrectionCSDs(int annualAccountReportId, int contractReportFinancialCorrectionId);

        int[] FindFinancialCorrectionCSDs(int[] contractReportFinancialCorrectionCSDIds);

        IList<AnnualAccountReportCorrectionVO> GetContractReportCorrectionsForAnnualAccountReportCorrections(int annualAccountReportId);

        IList<AnnualAccountReportCorrectionVO> GetAnnualAccountReportCorrections(int annualAccountReportId);

        int[] FindAllAttachedCertFinancialCorrectionCSDs(int annualAccountReportId, int contractReportFinancialCorrectionId);

        int[] FindAllUnattachedCertFinancialCorrectionCSDs(int contractReportCertAuthorityFinancialCorrectionId);

        int[] FindCertAuthorityFinancialCorrectionIds(int[] contractReportFinancialCorrectionCSDIds);

        IList<ContractReportCertAuthorityFinancialCorrectionVO> GetContractReportCorrectionsForAnnualAccountReportCertFinancialCorrections(int annualAccountReport);

        IList<AnnualAccountReportCertFinancialCorrectionVO> GetAnnualAccountReportCertFinancialCorrections(int annualAccountReportId);

        List<ContractReportCertAuthorityFinancialCorrectionCSDsVO> GetContractReportCertAuthorityFinancialCorrectionCSDs(int annualAccountReportId, int contractReportCertAuthorityFinancialCorrectionId);

        List<ContractReportCertAuthorityFinancialCorrectionCSDsVO> GetFinancialCorrectionCSDsForContractReportCertAuthorityFinancialCorrectionCSDs(int contractReportCertAuthorityFinancialCorrectionId);

        IList<ContractReportCertAuthorityCorrectionVO> GetContractReportCorrectionsForAnnualAccountReportCertCorrections(int annualAccountReportId);

        IList<ContractReportCertAuthorityCorrectionVO> GetAnnualAccountReportCertCorrections(int annualAccountReportId);

        IList<AnnualAccountReportAppendixVO> GetAnnualAccountReportAppendices(int annualAccountReportId, AnnualAccountReportAppendixType type);

        IList<int> GetUnattachedCertReports(AnnualAccountReport annualAccountReport);

        int[] GetUnattachedFinancialCorrectionsCSDs(int[] attachedCertReports);

        int[] GetUnattachedCorrections(int[] attachedCertReports);

        IList<ContractReportRevalidationCertAuthorityCorrectionVO> GetUnattachedContractReportRevalidationCorrections(int annualAccountReportId);

        IList<ContractReportRevalidationCertAuthorityCorrectionVO> GetAnnualAccountReportCertRevalidationCorrections(int annualAccountReportId);

        IList<AnnualAccountReportCertRevalidationFinancialCorrectionVO> GetAnnualAccountReportCertRevalidationFinancialCorrections(int annualAccountReportId);

        IList<ContractReportRevalidationCertAuthorityFinancialCorrectionVO> GetContractReportCorrectionsForAnnualAccountReportCertRevalidationFinancialCorrections(int annualAccountReportId);

        int[] GetAllUnattachedCertRevalidationFinancialCorrectionCSDs(int contractReportRevalidationCertAuthorityFinancialCorrectionId);

        int[] GetAllAttachedCertRevalidationFinancialCorrectionCSDs(int annualAccountReportId, int contractReportRevalidationCertAuthorityFinancialCorrectionId);

        IList<ContractReportRevalidationCertAuthorityFinancialCorrectionCSDsVO> GetContractReportRevalidationCertAuthorityFinancialCorrectionCSDs(
            int annualAccountReportId,
            int contractReportRevalidationCertAuthorityFinancialCorrectionId);

        IList<ContractReportRevalidationCertAuthorityFinancialCorrectionCSDsVO> GetFinancialCorrectionCSDsForContractReportCertAuthorityRevalidationFinancialCorrectionCSDs(int contractReportRevalidationCertAuthorityFinancialCorrectionId);
    }
}
