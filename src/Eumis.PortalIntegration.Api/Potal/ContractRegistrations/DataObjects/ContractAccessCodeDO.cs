using Eumis.Domain.Contracts;
using System;

namespace Eumis.PortalIntegration.Api.Portal.ContractRegistrations.DataObjects
{
    public class ContractAccessCodeDO
    {
        public ContractAccessCodeDO()
        {
        }

        public ContractAccessCodeDO(ContractAccessCode accessCode)
        {
            this.Gid = accessCode.Gid;
            this.CreateDate = accessCode.CreateDate;
            this.Code = accessCode.Code;
            this.FirstName = accessCode.FirstName;
            this.LastName = accessCode.LastName;
            this.Position = accessCode.Position;
            this.Identifier = accessCode.Identifier;
            this.Email = accessCode.Email;
            this.IsActive = accessCode.IsActive;
            this.Permissions = new AccessCodePermissionDO
            {
                CanReadContracts = accessCode.CanReadContracts,
                CanReadProcurements = accessCode.CanReadProcurements,
                CanWriteProcurements = accessCode.CanWriteProcurements,
                CanReadTechnicalPlan = accessCode.CanReadTechnicalPlan,
                CanWriteTechnicalPlan = accessCode.CanWriteTechnicalPlan,
                CanReadFinancialPlan = accessCode.CanReadFinancialPlan,
                CanWriteFinancialPlan = accessCode.CanWriteFinancialPlan,
                CanReadMicrodata = accessCode.CanReadMicrodata,
                CanWriteMicrodata = accessCode.CanWriteMicrodata,
                CanReadPaymentRequest = accessCode.CanReadPaymentRequest,
                CanWritePaymentRequest = accessCode.CanWritePaymentRequest,
                CanReadCommunication = accessCode.CanReadCommunication,
                CanReadSpendingPlan = accessCode.CanReadSpendingPlan,
                CanWriteSpendingPlan = accessCode.CanWriteSpendingPlan,
            };

            this.Version = accessCode.Version;
        }

        public Guid Gid { get; set; }

        public string Code { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public string Identifier { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public AccessCodePermissionDO Permissions { get; set; }

        public DateTime CreateDate { get; set; }

        public byte[] Version { get; set; }
    }
}
