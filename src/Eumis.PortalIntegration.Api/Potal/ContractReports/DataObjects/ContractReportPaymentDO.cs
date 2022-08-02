using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Eumis.Domain.Procedures.ProcedureContractReportDocuments;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Potal.ContractReports.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.PortalIntegration.Api.Potal.ContractProcurements.DataObjects
{
    public class ContractReportPaymentDO : XmlDO
    {
        public ContractReportPaymentDO()
        {
        }

        public ContractReportPaymentDO(
            ContractReportPayment contractReportPayment,
            bool? contractReportHasAdvanceVerificationPayment,
            IList<ProcedureContractReportDocument> procedureContractReportDocuments,
            IList<string> canEnterErrors)
        {
            this.VersionNum = contractReportPayment.VersionNum;
            this.VersionSubNum = contractReportPayment.VersionSubNum;
            this.Status = new EnumDO<ContractReportPaymentStatus>()
            {
                Value = contractReportPayment.Status,
                Description = contractReportPayment.Status,
            };
            this.StatusNote = contractReportPayment.StatusNote;
            this.CreateDate = contractReportPayment.CreateDate;
            this.ContractReportHasAdvanceVerificationPayment = contractReportHasAdvanceVerificationPayment;

            this.CanEnterErrors = canEnterErrors;

            this.Xml = contractReportPayment.Xml;
            this.Gid = contractReportPayment.Gid;
            this.ModifyDate = contractReportPayment.ModifyDate;
            this.Version = contractReportPayment.Version;

            this.ProcedureContractReportPaymentDocuments = procedureContractReportDocuments.Select(crd => new ContractReportDocumentDO()
            {
                Gid = crd.Gid,
                Name = crd.Name,
                Extension = crd.Extension,
                IsRequired = crd.IsRequired,
                IsActive = crd.IsActive,
            }).ToList();
        }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }

        public EnumDO<ContractReportPaymentStatus> Status { get; set; }

        public string StatusNote { get; set; }

        public DateTime CreateDate { get; set; }

        public bool? ContractReportHasAdvanceVerificationPayment { get; set; }

        public IList<string> CanEnterErrors { get; set; }

        public IList<ContractReportDocumentDO> ProcedureContractReportPaymentDocuments { get; set; }
    }
}
