using System;
using Eumis.Common.Crypto;

namespace Eumis.Domain.Contracts
{
    public partial class ContractAccessCode
    {
        public void UpdateAttributes(
            string firstName,
            string lastName,
            string position,
            string identifier,
            bool isActive,
            bool canReadContracts,
            bool canReadProcurements,
            bool canWriteProcurements,
            bool canReadTechnicalPlan,
            bool canWriteTechnicalPlan,
            bool canReadFinancialPlan,
            bool canWriteFinancialPlan,
            bool canReadMicrodata,
            bool canWriteMicrodata,
            bool canReadPaymentRequest,
            bool canWritePaymentRequest,
            bool canReadCommunication,
            bool canReadSpendingPlan,
            bool canWriteSpendingPlan)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Position = position;
            this.Identifier = identifier;
            this.IsActive = isActive;
            this.CanReadContracts = canReadContracts;
            this.CanReadProcurements = canReadProcurements;
            this.CanWriteProcurements = canWriteProcurements;
            this.CanReadTechnicalPlan = canReadTechnicalPlan;
            this.CanWriteTechnicalPlan = canWriteTechnicalPlan;
            this.CanReadFinancialPlan = canReadFinancialPlan;
            this.CanWriteFinancialPlan = canWriteFinancialPlan;
            this.CanReadMicrodata = canReadMicrodata;
            this.CanWriteMicrodata = canWriteMicrodata;
            this.CanReadPaymentRequest = canReadPaymentRequest;
            this.CanWritePaymentRequest = canWritePaymentRequest;
            this.CanReadCommunication = canReadCommunication;
            this.CanReadSpendingPlan = canReadSpendingPlan;
            this.CanWriteSpendingPlan = canWriteSpendingPlan;

            this.ModifyDate = DateTime.Now;
        }

        private string GenerateRandomCode()
        {
            return CryptoUtils.GetRandomAlphanumericString(4);
        }

        public void Activate()
        {
            this.IsActive = true;
            this.ModifyDate = DateTime.Now;
        }

        public void Deactivate()
        {
            this.IsActive = false;
            this.ModifyDate = DateTime.Now;
        }
    }
}
