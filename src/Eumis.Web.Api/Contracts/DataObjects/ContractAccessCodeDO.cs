using Eumis.Domain.Contracts;
using System;

namespace Eumis.Web.Api.Contracts.DataObjects
{
    public class ContractAccessCodeDO
    {
        public ContractAccessCodeDO()
        {
        }

        public ContractAccessCodeDO(ContractAccessCode accessCode, bool isSuperUser)
        {
            this.ContractAccessCodeId = accessCode.ContractAccessCodeId;
            this.ContractId = accessCode.ContractId;
            this.Gid = accessCode.Gid;
            this.Code = isSuperUser ? accessCode.Code : string.Empty;
            this.FirstName = accessCode.FirstName;
            this.LastName = accessCode.LastName;
            this.Position = accessCode.Position;
            this.Identifier = accessCode.Identifier;
            this.Email = accessCode.Email;
            this.IsActive = accessCode.IsActive;

            this.ShowAccessCode = isSuperUser;

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

            this.CreateDate = accessCode.CreateDate;
            this.ModifyDate = accessCode.ModifyDate;
            this.Version = accessCode.Version;
        }

        public int ContractAccessCodeId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public string Code { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public string Identifier { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public AccessCodePermissionDO Permissions { get; set; }

        public bool ShowAccessCode { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
