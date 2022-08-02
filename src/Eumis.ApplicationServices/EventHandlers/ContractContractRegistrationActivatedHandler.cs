﻿using Eumis.Common.Email;
using Eumis.Data.ContractRegistrations.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Emails.Repositories;
using Eumis.Domain.Core;
using Eumis.Domain.Emails;
using Eumis.Domain.Events;
using Newtonsoft.Json.Linq;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class ContractContractRegistrationActivatedHandler : EventHandler<ContractContractRegistrationActivatedEvent>
    {
        private IContractsRepository contractsRepository;
        private IContractRegistrationsRepository contractRegistrationsRepository;
        private IEmailsRepository emailsRepository;

        public ContractContractRegistrationActivatedHandler(
            IContractsRepository contractsRepository,
            IContractRegistrationsRepository contractRegistrationsRepository,
            IEmailsRepository emailsRepository)
        {
            this.contractsRepository = contractsRepository;
            this.contractRegistrationsRepository = contractRegistrationsRepository;
            this.emailsRepository = emailsRepository;
        }

        public override void Handle(ContractContractRegistrationActivatedEvent e)
        {
            var contract = this.contractsRepository.Find(e.ContractId);
            var regEmail = this.contractRegistrationsRepository.GetRegistrationEmail(e.ContractRegistrationId);

            Email email = new Email(
                regEmail,
                EmailTemplate.ContractContracRegistrationActivatedMessage,
                JObject.FromObject(
                    new
                    {
                        email = regEmail,
                        contractGid = contract.Gid,
                        contractNum = contract.RegNumber,
                    }));

            this.emailsRepository.Add(email);
        }
    }
}
