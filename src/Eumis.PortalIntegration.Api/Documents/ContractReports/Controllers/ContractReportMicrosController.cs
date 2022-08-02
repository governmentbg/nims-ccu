using System;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ContractReportMicros;

namespace Eumis.PortalIntegration.Api.Documents.ContractReports.Controllers
{
    [RoutePrefix("api/contractReportMicros")]
    public class ContractReportMicrosController : ApiController
    {
        private IAuthorizer authorizer;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportMicrosRepository contractReportMicrosRepository;

        public ContractReportMicrosController(
            IAuthorizer authorizer,
            IContractReportsRepository contractReportsRepository,
            IContractReportMicrosRepository contractReportMicrosRepository)
        {
            this.authorizer = authorizer;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportMicrosRepository = contractReportMicrosRepository;
        }

        [Route("{contractReportMicroGid:guid}/items")]
        public IHttpActionResult GetContractReportMicroItems(Guid contractReportMicroGid, int offset = 0, int? limit = null)
        {
            var micro = this.contractReportMicrosRepository.Find(contractReportMicroGid);
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.View, micro.ContractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.View, micro.ContractReportId));

            this.AssertIsNotDraftOrEnteredFromBeneficiary(micro);

            switch (micro.Type)
            {
                case ContractReportMicroType.Type1:
                    return this.Ok(this.contractReportMicrosRepository.GetPortalType1Items(contractReportMicroGid, offset, limit));
                case ContractReportMicroType.Type2:
                    return this.Ok(this.contractReportMicrosRepository.GetPortalType2Items(contractReportMicroGid, offset, limit));
                case ContractReportMicroType.Type3:
                    return this.Ok(this.contractReportMicrosRepository.GetPortalType3Items(contractReportMicroGid, offset, limit));
                case ContractReportMicroType.Type4:
                    return this.Ok(this.contractReportMicrosRepository.GetPortalType4Items(contractReportMicroGid, offset, limit));
                default:
                    throw new DomainException("Invalid micto type");
            }
        }

        private void AssertIsNotDraftOrEnteredFromBeneficiary(ContractReportMicro micro)
        {
            if (micro.Source == Source.Beneficiary && (micro.Status == ContractReportMicroStatus.Draft || micro.Status == ContractReportMicroStatus.Entered))
            {
                throw new UnauthorizedAccessException("Cannot get ContractReportFinancial with status 'Draft' or 'Entered' when microdata has source 'Beneficiary'");
            }
        }
    }
}
