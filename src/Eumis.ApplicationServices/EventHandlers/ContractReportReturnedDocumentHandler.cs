using Eumis.Common.Email;
using Eumis.Common.Json;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Emails.Repositories;
using Eumis.Domain.Emails;
using Eumis.Domain.Events;
using Newtonsoft.Json.Linq;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class ContractReportReturnedDocumentHandler : Eumis.Domain.Core.EventHandler<ContractReportReturnedDocumentEvent>
    {
        private IEmailsRepository emailsRepository;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;

        public ContractReportReturnedDocumentHandler(
            IEmailsRepository emailsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractsRepository contractsRepository)
        {
            this.emailsRepository = emailsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractsRepository = contractsRepository;
        }

        public override void Handle(ContractReportReturnedDocumentEvent e)
        {
            var contractReport = this.contractReportsRepository.Find(e.ContractReportId);
            var contract = this.contractsRepository.Find(contractReport.ContractId);

            var contractRegistrations = this.contractsRepository.GetContractContractRegistrations(contractReport.ContractId);

            foreach (var cr in contractRegistrations)
            {
                if (cr.IsActive)
                {
                    Email email = new Email(
                        cr.Email,
                        EmailTemplate.ContractReportReturnedDocumentMessage,
                        JObject.FromObject(
                            new
                            {
                                documentType = e.ContractReportDocumentType.GetEnumDescription(),
                                documentNumber = $"{e.VersionNum}.{e.VersionSubNum}",
                                contractReportNumber = contractReport.OrderNum,
                                contractRegNumber = contract.RegNumber,
                                contractName = contract.Name,
                                contractCompanyName = contract.CompanyName,
                                contractCompanyUin = contract.CompanyUin,
                                email = cr.Email,
                            }));

                    this.emailsRepository.Add(email);
                }
            }
        }
    }
}
