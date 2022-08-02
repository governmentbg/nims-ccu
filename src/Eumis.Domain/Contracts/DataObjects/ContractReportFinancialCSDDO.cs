using Eumis.Common.Json;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportFinancialCSDDO
    {
        public ContractReportFinancialCSDDO()
        {
        }

        public ContractReportFinancialCSDDO(ContractReportFinancialCSD contractReportFinancialCSD)
        {
            this.ContractReportFinancialCSDId = contractReportFinancialCSD.ContractReportFinancialCSDId;
            this.ContractReportFinancialId = contractReportFinancialCSD.ContractReportFinancialId;
            this.ContractReportId = contractReportFinancialCSD.ContractReportId;
            this.ContractId = contractReportFinancialCSD.ContractId;

            this.Type = contractReportFinancialCSD.Type;
            this.Description = contractReportFinancialCSD.Description;
            this.Number = contractReportFinancialCSD.Number;
            this.Date = contractReportFinancialCSD.Date;
            this.PaymentDate = contractReportFinancialCSD.PaymentDate;
            this.CompanyType = contractReportFinancialCSD.CompanyType;
            this.CompanyName = contractReportFinancialCSD.CompanyName;
            this.CompanyUin = contractReportFinancialCSD.CompanyUin;
            this.CompanyUinType = contractReportFinancialCSD.CompanyUinType;
            this.ContractContractorName = contractReportFinancialCSD.ContractContractorName;

            this.Files = contractReportFinancialCSD.Files
                .Select(t => new FileWithDescriptionDO()
                {
                    Key = t.BlobKey,
                    Name = t.Name,
                    Description = t.Description,
                })
                .ToList();

            this.ModifyDate = contractReportFinancialCSD.ModifyDate;
            this.CreateDate = contractReportFinancialCSD.CreateDate;
            this.Version = contractReportFinancialCSD.Version;
        }

        public int ContractReportFinancialCSDId { get; set; }

        public int ContractReportFinancialId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public CostSupportingDocumentType Type { get; set; }

        public string Description { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public DateTime PaymentDate { get; set; }

        public CostSupportingDocumentCompanyType CompanyType { get; set; }

        public string CompanyName { get; set; }

        public string CompanyUin { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public UinType? CompanyUinType { get; set; }

        public string ContractContractorName { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public IList<FileWithDescriptionDO> Files { get; set; }
    }
}