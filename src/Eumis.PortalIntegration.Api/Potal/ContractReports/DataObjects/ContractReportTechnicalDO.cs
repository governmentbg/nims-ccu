using Eumis.Domain.Contracts;
using Eumis.Domain.Procedures.ProcedureContractReportDocuments;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Potal.ContractReports.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.PortalIntegration.Api.Potal.ContractProcurements.DataObjects
{
    public class ContractReportTechnicalDO : XmlDO
    {
        public ContractReportTechnicalDO()
        {
        }

        public ContractReportTechnicalDO(ContractReportTechnical contractReportTechnical, string loadedXml = null, IList<string> canEnterErrors = null)
        {
            this.VersionNum = contractReportTechnical.VersionNum;
            this.VersionSubNum = contractReportTechnical.VersionSubNum;
            this.Status = new EnumDO<ContractReportTechnicalStatus>()
            {
                Value = contractReportTechnical.Status,
                Description = contractReportTechnical.Status,
            };
            this.StatusNote = contractReportTechnical.StatusNote;
            this.CreateDate = contractReportTechnical.CreateDate;
            this.CanEnterErrors = canEnterErrors;

            if (string.IsNullOrEmpty(loadedXml))
            {
                this.Xml = contractReportTechnical.Xml;
            }
            else
            {
                this.Xml = loadedXml;
            }

            this.Gid = contractReportTechnical.Gid;
            this.ModifyDate = contractReportTechnical.ModifyDate;
            this.Version = contractReportTechnical.Version;
        }

        public ContractReportTechnicalDO(
            ContractReportTechnical contractReportTechnical,
            IList<ProcedureContractReportDocument> procedureContractReportDocuments,
            string loadedXml = null,
            IList<string> canEnterErrors = null)
            : this(contractReportTechnical, loadedXml, canEnterErrors)
        {
            this.ProcedureContractReportTechnicalDocuments = procedureContractReportDocuments.Select(crd => new ContractReportDocumentDO()
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

        public EnumDO<ContractReportTechnicalStatus> Status { get; set; }

        public string StatusNote { get; set; }

        public DateTime CreateDate { get; set; }

        public IList<string> CanEnterErrors { get; set; }

        public IList<ContractReportDocumentDO> ProcedureContractReportTechnicalDocuments { get; set; }
    }
}
