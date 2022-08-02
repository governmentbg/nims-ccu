using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Db;
using Eumis.Data.CertReports.Repositories;
using Eumis.Domain.CertReports.ViewObjects;
using Eumis.Domain.CertReports.ViewObjects.SummaryVOs;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.CertReports.Controllers
{
    [RoutePrefix("api/certReports/{certReportId:int}/summary")]
    public class CertReportSummaryController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private ICertReportsRepository certReportsRepository;

        public CertReportSummaryController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            ICertReportsRepository certReportsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.certReportsRepository = certReportsRepository;
        }

        [Route("intermediateFinalEligibleProgrammePriorityExpenses")]
        public CertReportEligibleProgrammePriorityExpensesResultVO GetCertReportIntermediateFinalEligibleProgrammePriorityExpenses(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.certReportsRepository.GetCertReportIntermediateFinalEligibleProgrammePriorityExpenses(certReportId);
        }

        [Route("approvedAmountsCorrections")]
        public CertReportApprovedAmountsCorrectionsResultVO GetCertReportApprovedAmountsCorrections(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.certReportsRepository.GetCertReportApprovedAmountsCorrections(certReportId);
        }

        [Route("intermediateFinalStateAidPaidAdvancePayments")]
        public CertReportStateAidPaidAdvancePaymentsResultVO GetCertReportIntermediateFinalStateAidPaidAdvancePayments(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.certReportsRepository.GetCertReportIntermediateFinalStateAidPaidAdvancePayments(certReportId);
        }

        [Route("reaffirmedCostsByAdministrativeAuthority")]
        public CertReportReaffirmedCostsByAdministrativeAuthorityResultVO GetCertReportReaffirmedCostsByAdministrativeAuthority(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.certReportsRepository.GetCertReportReaffirmedCostsByAdministrativeAuthority(certReportId);
        }

        [Route("programmePaidContributionInfoForFinancialInstrument")]
        public CertReportProgrammePaidContributionInfoForFinancialInstrumentsResultVO GetCertReportProgrammePaidContributionInfoForFinancialInstruments(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.certReportsRepository.GetCertReportProgrammePaidContributionInfoForFinancialInstruments(certReportId);
        }

        [Route("annex4A")]
        public CertReportAnnex4aResultVO GetAnnex4A(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.certReportsRepository.GetAnnex4A(certReportId);
        }
    }
}
