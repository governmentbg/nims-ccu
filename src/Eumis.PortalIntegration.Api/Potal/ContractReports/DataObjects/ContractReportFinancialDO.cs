using Eumis.Domain.Contracts;
using Eumis.Domain.Procedures.ProcedureContractReportDocuments;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Potal.ContractReports.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.PortalIntegration.Api.Potal.ContractProcurements.DataObjects
{
    public class ContractReportFinancialDO : XmlDO
    {
        public ContractReportFinancialDO()
        {
        }

        public ContractReportFinancialDO(ContractReportFinancial contractReportFinancial, string loadedXml = null, IList<string> canEnterErrors = null)
        {
            this.VersionNum = contractReportFinancial.VersionNum;
            this.VersionSubNum = contractReportFinancial.VersionSubNum;
            this.Status = new EnumDO<ContractReportFinancialStatus>()
            {
                Value = contractReportFinancial.Status,
                Description = contractReportFinancial.Status,
            };
            this.StatusNote = contractReportFinancial.StatusNote;
            this.CreateDate = contractReportFinancial.CreateDate;
            this.CanEnterErrors = canEnterErrors;

            if (string.IsNullOrEmpty(loadedXml))
            {
                this.Xml = contractReportFinancial.Xml;
            }
            else
            {
                this.Xml = loadedXml;
            }

            this.Gid = contractReportFinancial.Gid;
            this.ModifyDate = contractReportFinancial.ModifyDate;
            this.Version = contractReportFinancial.Version;
        }

        public ContractReportFinancialDO(
            ContractReportFinancial contractReportFinancial,
            IList<ProcedureContractReportDocument> procedureContractReportDocuments,
            string loadedXml = null,
            IList<string> canEnterErrors = null)
            : this(contractReportFinancial, loadedXml, canEnterErrors)
        {
            this.ProcedureContractReportFinancialDocuments = procedureContractReportDocuments.Select(crd => new ContractReportDocumentDO()
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

        public EnumDO<ContractReportFinancialStatus> Status { get; set; }

        public string StatusNote { get; set; }

        public DateTime CreateDate { get; set; }

        public IList<string> CanEnterErrors { get; set; }

        public IList<ContractReportDocumentDO> ProcedureContractReportFinancialDocuments { get; set; }
    }
}
