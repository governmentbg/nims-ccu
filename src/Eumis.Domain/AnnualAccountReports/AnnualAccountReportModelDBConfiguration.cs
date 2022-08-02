using Eumis.Common.Db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.AnnualAccountReports
{
    public class AnnualAccountReportModelDBConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AnnualAccountReportMap());
            modelBuilder.Configurations.Add(new AnnualAccountReportCertificationDocumentMap());
            modelBuilder.Configurations.Add(new AnnualAccountReportAuditDocumentMap());
            modelBuilder.Configurations.Add(new AnnualAccountReportCertReportMap());
            modelBuilder.Configurations.Add(new AnnualAccountReportContractReportCorrectionMap());
            modelBuilder.Configurations.Add(new AnnualAccountReportFinancialCorrectionCSDMap());
            modelBuilder.Configurations.Add(new AnnualAccountReportCertCorrectionMap());
            modelBuilder.Configurations.Add(new AnnualAccountReportCertFinancialCorrectionCSDMap());
            modelBuilder.Configurations.Add(new AnnualAccountReportCertRevalidationCorrectionMap());
            modelBuilder.Configurations.Add(new AnnualAccountReportCertRevalidationFinancialCorrectionCSDMap());
            modelBuilder.Configurations.Add(new AnnualAccountReportAppendixMap());
        }
    }
}
