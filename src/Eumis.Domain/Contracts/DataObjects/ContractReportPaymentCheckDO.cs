using Eumis.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportPaymentCheckDO
    {
        public ContractReportPaymentCheckDO()
        {
            this.ContractReportPaymentCheckAmounts = new List<ContractReportPaymentCheckAmountDO>();
        }

        public ContractReportPaymentCheckDO(ContractReportPaymentCheck contractReportPaymentCheck, ContractReportPayment contractReportPayment, string checkedByUser)
        {
            this.ContractReportPaymentCheckId = contractReportPaymentCheck.ContractReportPaymentCheckId;
            this.ContractReportPaymentId = contractReportPaymentCheck.ContractReportPaymentId;
            this.ContractReportId = contractReportPaymentCheck.ContractReportId;
            this.ContractId = contractReportPaymentCheck.ContractId;

            this.OrderNum = contractReportPaymentCheck.OrderNum;
            this.Status = contractReportPaymentCheck.Status;
            this.Approval = contractReportPaymentCheck.Approval;
            this.PaymentType = contractReportPaymentCheck.PaymentType;

            if (contractReportPaymentCheck.File != null)
            {
                this.File = new FileDO
                {
                    Key = contractReportPaymentCheck.File.Key,
                    Name = contractReportPaymentCheck.File.FileName,
                };
            }

            this.CheckedByUser = checkedByUser;
            this.CheckedDate = contractReportPaymentCheck.CheckedDate;

            this.ContractReportPayment = new ContractReportPaymentDO(contractReportPayment);

            this.ContractReportPaymentCheckAmounts = contractReportPaymentCheck.ContractReportPaymentCheckAmounts.Select(t => new ContractReportPaymentCheckAmountDO(t)).ToList();

            this.CreateDate = contractReportPaymentCheck.CreateDate;
            this.Version = contractReportPaymentCheck.Version;
        }

        public int ContractReportPaymentCheckId { get; set; }

        public int ContractReportPaymentId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        public ContractReportPaymentCheckStatus Status { get; set; }

        public ContractReportPaymentCheckApproval? Approval { get; set; }

        public ContractReportPaymentType PaymentType { get; set; }

        public FileDO File { get; set; }

        public string CheckedByUser { get; set; }

        public DateTime? CheckedDate { get; set; }

        public ContractReportPaymentDO ContractReportPayment { get; set; }

        public IList<ContractReportPaymentCheckAmountDO> ContractReportPaymentCheckAmounts { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
