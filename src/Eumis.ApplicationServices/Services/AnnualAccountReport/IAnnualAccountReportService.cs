using Eumis.Domain.AnnualAccountReports;
using Eumis.Domain.AnnualAccountReports.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.ApplicationServices.Services.AnnualAccountReport
{
    public interface IAnnualAccountReportService
    {
        Eumis.Domain.AnnualAccountReports.AnnualAccountReport CreateAnnualAccountReport(
            int programmeId,
            DateTime regDate,
            AnnualAccountReportPeriod accountPeriod);

        IList<string> CanDeleteAnnualAccountReport(int annualAccountReportId);

        void DeleteAnnualAccountReport(int annualAccountReportId, byte[] vers);

        void UpdateAnnualAccountReport(int annualAccountReportId, byte[] version, DateTime? regDate, DateTime? approvalDate);

        int? ChangeAnnualAccountReportStatus(int certReportId, byte[] version, AnnualAccountReportStatus status, string statusNote = null);

        void CreateAnnualAccountReportFinancialCorrection(int annualAccountReportId, byte[] version, int[] contractReportFinancialCorrectionIds);

        void CreateAnnualAccountReportFinancialCorrectionCSDs(int annualAccountReportId, byte[] vers, int contractReportFinancialCorrectionId, int[] contractReportFinancialCorrectionCSDIds);

        void DeleteAnnualAccountReportFinancialCorrection(int annualAccountReportId, byte[] vers, int contractReportFinancialCorrectionId);

        void CreateAnnualAccountReportCertFinancialCorrectionCSDs(int annualAccountReportId, byte[] vers, int contractReportFinancialCorrectionId, int[] contractReportFinancialCorrectionCSDIds);

        void DeleteAnnualAccountReportCertFinancialCorrection(int annualAccountReportId, byte[] vers, int contractReportFinancialCorrectionId);

        void CreateAnnualAccountReportCertFinancialCorrection(int annualAccountReportId, byte[] version, int[] contractReportFinancialCorrectionIds);

        AnnualAccountReportAppendixDO GetAnnualAccountReportAppendix(int annualAccountReportId, int appendixId);

        IList<string> CanChangeStatus(int annualAccountReportId, AnnualAccountReportStatus draft);

        void CreateAnnualAccountReportCertRevalidationFinancialCorrection(int annualAccountReportId, byte[] version, int[] contractReportFinancialCorrectionIds);

        void DeleteAnnualAccountReportCertRevalidationFinancialCorrection(int annualAccountReportId, byte[] version, int contractReportRevalidationCertAuthorityFinancialCorrectionId);
    }
}
