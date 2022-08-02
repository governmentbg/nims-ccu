using System.Collections.Generic;
using Eumis.Common.Email;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Emails.Repositories;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using Eumis.Domain.Emails;
using Eumis.Domain.Events;
using Eumis.Domain.Users;
using Eumis.Domain.Users.ProgrammePermissions;
using Newtonsoft.Json.Linq;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class ContractAuthorityCommunicationSentEventHandler : EventHandler<ContractAuthorityCommunicationSentEvent>
    {
        private IContractsRepository contractsRepository;
        private IUsersRepository usersRepository;
        private IEmailsRepository emailsRepository;
        private IProgrammesRepository programmesRepository;
        private IProceduresRepository proceduresRepository;
        private IContractCommunicationXmlsRepository contractCommunicationXmlsRepository;

        public ContractAuthorityCommunicationSentEventHandler(
            IContractsRepository contractsRepository,
            IUsersRepository usersRepository,
            IEmailsRepository emailsRepository,
            IProgrammesRepository programmesRepository,
            IProceduresRepository proceduresRepository,
            IContractCommunicationXmlsRepository contractCommunicationXmlsRepository)
        {
            this.contractsRepository = contractsRepository;
            this.usersRepository = usersRepository;
            this.emailsRepository = emailsRepository;
            this.programmesRepository = programmesRepository;
            this.proceduresRepository = proceduresRepository;
            this.contractCommunicationXmlsRepository = contractCommunicationXmlsRepository;
        }

        public override void Handle(ContractAuthorityCommunicationSentEvent e)
        {
            var contract = this.contractsRepository.Find(e.ContractId);
            var contractCommunication = this.contractCommunicationXmlsRepository.Find(e.ContractCommunicationXmlId);

            var programme = this.programmesRepository.Find(contract.ProgrammeId);
            var procedure = this.proceduresRepository.GetProcedureBasicData(contract.ProcedureId);

            var contractRegistrations = this.contractsRepository.GetContractContractRegistrations(e.ContractId);

            foreach (var cr in contractRegistrations)
            {
                if (cr.IsActive)
                {
                    Email email = new Email(
                        cr.Email,
                        EmailTemplate.ContractCommunicationSentMessage,
                        JObject.FromObject(
                            new
                            {
                                contractRegNumber = contract.RegNumber,
                                contractName = contract.Name,
                                contractCompanyName = contract.CompanyName,
                                contractCompanyUin = contract.CompanyUin,
                                contractCommunicationRegNumber = contractCommunication.RegNumber,
                                contractCommunicationType = contractCommunication.Type,
                                contractCommunicationSource = contractCommunication.Source,
                                programmeName = programme.Name,
                                programmeCode = programme.Code,
                                procedureName = procedure.Name,
                                procedureCode = procedure.Code,
                                email = cr.Email,
                            }));

                    this.emailsRepository.Add(email);
                }
            }
        }
    }
}
