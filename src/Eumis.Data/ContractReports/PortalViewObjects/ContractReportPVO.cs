using System;
using System.Collections.Generic;
using Eumis.Data.ContractReports.PortalViewObjects;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;

namespace Eumis.Data.Contracts.PortalViewObjects
{
    public class ContractReportPVO
    {
        public Guid Gid { get; set; }

        public EnumPVO<ContractReportType> ContractReportType { get; set; }

        public EnumPVO<ContractReportStatus> Status { get; set; }

        public string StatusNote { get; set; }

        public int OrderNum { get; set; }

        public EnumPVO<Source> Source { get; set; }

        public string OtherRegistration { get; set; }

        public string StoragePlace { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime? SubmitDeadline { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public IList<ContractReportFinancialPVO> ContractReportFinancials { get; set; }

        public IList<ContractReportTechnicalPVO> ContractReportTechnicals { get; set; }

        public IList<ContractReportPaymentPVO> ContractReportPayments { get; set; }

        public IList<ContractReportMicroPVO> ContractReportType1Micros { get; set; }

        public IList<ContractReportMicroPVO> ContractReportType2Micros { get; set; }

        public IList<ContractReportMicroPVO> ContractReportType3Micros { get; set; }

        public IList<ContractReportMicroPVO> ContractReportType4Micros { get; set; }

        public bool HasReturnedDocuments { get; set; }
    }
}
