using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.EvalSessions
{
    public class EvalSessionsModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EvalSessionMap());
            modelBuilder.Configurations.Add(new EvalSessionUserMap());
            modelBuilder.Configurations.Add(new EvalSessionProjectMap());
            modelBuilder.Configurations.Add(new EvalSessionSheetMap());
            modelBuilder.Configurations.Add(new EvalSessionDistributionMap());
            modelBuilder.Configurations.Add(new EvalSessionSheetXmlMap());
            modelBuilder.Configurations.Add(new EvalSessionSheetXmlFileMap());
            modelBuilder.Configurations.Add(new EvalSessionDistributionUserMap());
            modelBuilder.Configurations.Add(new EvalSessionDistributionProjectMap());
            modelBuilder.Configurations.Add(new EvalSessionEvaluationMap());
            modelBuilder.Configurations.Add(new EvalSessionEvaluationSheetMap());
            modelBuilder.Configurations.Add(new EvalSessionDocumentMap());
            modelBuilder.Configurations.Add(new EvalSessionStandingMap());
            modelBuilder.Configurations.Add(new EvalSessionStandingProjectMap());
            modelBuilder.Configurations.Add(new EvalSessionProjectStandingMap());
            modelBuilder.Configurations.Add(new EvalSessionProjectStandingEvaluationMap());
            modelBuilder.Configurations.Add(new EvalSessionReportMap());
            modelBuilder.Configurations.Add(new EvalSessionReportProjectMap());
            modelBuilder.Configurations.Add(new EvalSessionReportProjectPartnerMap());
            modelBuilder.Configurations.Add(new EvalSessionStandpointMap());
            modelBuilder.Configurations.Add(new EvalSessionStandpointXmlMap());
            modelBuilder.Configurations.Add(new EvalSessionStandpointXmlFileMap());
            modelBuilder.Configurations.Add(new EvalSessionProjectStandingRejectionReasonMap());
            modelBuilder.Configurations.Add(new EvalSessionAdminAdmissProjectMap());
            modelBuilder.Configurations.Add(new EvalSessionAdminAdmissResultMap());
        }
    }
}
