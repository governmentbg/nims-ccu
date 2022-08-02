using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Eumis.Documents.Contracts
{
    public class ContractPackageInfos
    {
        public List<ContractReportPVO> results { get; set; }
        public int count { get; set; }
        public bool canCreate { get; set; }
        public bool canEditSent { get; set; }
        public bool allowConcurrencyContractReports { get; set; }
    }

    public class ContractReportPVO
    {
        public Guid Gid { get; set; }

        public ContractEnumNomenclature ContractReportType { get; set; }

        public ContractEnumNomenclature Status { get; set; }

        public int OrderNum { get; set; }

        public string StatusNote { get; set; }

        public ContractEnumNomenclature Source { get; set; }

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

        public bool isBeneficiary
        {
            get
            {
                return this.Source != null && this.Source.value.ToLower() == Contracts.Source.Beneficiary.ToString().ToLower();
            }
        }

        public bool isDraft
        {
            get
            {
                return this.Status != null && this.Status.value.ToLower() == Contracts.ContractReportPortalStatus.Draft.ToString().ToLower();
            }
        }

        public bool isSentChecked
        {
            get
            {
                return this.Status != null && this.Status.value.ToLower() == Contracts.ContractReportPortalStatus.SentChecked.ToString().ToLower();
            }
        }
        
        public bool isCompleted
        {
            get
            {
                return this.Status != null
                    && this.Status.value.ToLower() == Contracts.ContractReportPortalStatus.Accepted.ToString().ToLower();
            }
        }

        public bool isRefused
        {
            get
            {
                return this.Status != null
                    && this.Status.value.ToLower() == Contracts.ContractReportPortalStatus.Refused.ToString().ToLower();
            }
        }

        public bool isReportTypeEquals(ContractReportType type)
        {
            return String.Equals(this.ContractReportType.value, type.ToString(), StringComparison.OrdinalIgnoreCase);
        }
    }

    #region enums

    public enum ContractReportType
    {
        [Description("Авансово искане за плащане")]
        AdvancePayment = 1,

        [Description("Технически отчет")]
        Technical = 2,

        [Description("Искане за плащане, технически отчет, финансов отчет")]
        PaymentTechnicalFinancial = 3,

        [Description("Искане за плащане и финансов отчет")]
        PaymentFinancial = 4
    }

    public enum ContractReportPortalStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведен")]
        Entered = 2,

        [Description("Приключен")]
        SentChecked = 3,

        [Description("В проверка")]
        Unchecked = 4,

        [Description("Приет")]
        Accepted = 5,

        [Description("Отхвърлен")]
        Refused = 6
    }

    public enum Source
    {
        [Description("Бенефициент")]
        Beneficiary = 1,

        [Description("ОО")]
        AdministrativeAuthority = 2,
    }

    public enum ContractReportFinancialStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведен")]
        Entered = 2,

        [Description("Актуален")]
        Actual = 3,

        [Description("Върнат")]
        Returned = 4
    }

    public enum ContractReportTechnicalStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведен")]
        Entered = 2,

        [Description("Актуален")]
        Actual = 3,

        [Description("Върнат")]
        Returned = 4
    }

    public enum ContractReportPaymentStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведен")]
        Entered = 2,

        [Description("Актуален")]
        Actual = 3,

        [Description("Върнат")]
        Returned = 4
    }

    public enum ContractReportMicroStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведен")]
        Entered = 2,

        [Description("Актуален")]
        Actual = 3,

        [Description("Върнат")]
        Returned = 4
    }

    #endregion

    #region documents

    public class ContractReportFinancialPVO
    {
        public Guid Gid { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public int VersionSubNum { get; set; }

        public ContractEnumNomenclature Status { get; set; }

        public string StatusNote { get; set; }

        public int VersionNum { get; set; }

        public byte[] Version { get; set; }

        public string RegNumber
        {
            get
            {
                return this.VersionNum + "." + this.VersionSubNum;
            }
        }

        public ContractReportFinancialStatus GetEnumStatus()
        {
            string value = this.Status.value.ToLower();

            if(value.Equals(ContractReportFinancialStatus.Draft.ToString().ToLower()))
                return ContractReportFinancialStatus.Draft;
            if(value.Equals(ContractReportFinancialStatus.Entered.ToString().ToLower()))
                return ContractReportFinancialStatus.Entered;
            if(value.Equals(ContractReportFinancialStatus.Actual.ToString().ToLower()))
                return ContractReportFinancialStatus.Actual;
            if(value.Equals(ContractReportFinancialStatus.Returned.ToString().ToLower()))
                return ContractReportFinancialStatus.Returned;

            throw new Exception("Wrong status");
        }

        public bool IsCompleted
        {
            get
            {
                return this.GetEnumStatus() == Eumis.Documents.Contracts.ContractReportFinancialStatus.Entered
                    || this.GetEnumStatus() == Eumis.Documents.Contracts.ContractReportFinancialStatus.Actual;
            }
        }
    }

    public class ContractReportTechnicalPVO
    {
        public Guid Gid { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public int VersionSubNum { get; set; }

        public ContractEnumNomenclature Status { get; set; }

        public string StatusNote { get; set; }

        public int VersionNum { get; set; }

        public byte[] Version { get; set; }

        public string RegNumber
        {
            get
            {
                return this.VersionNum + "." + this.VersionSubNum;
            }
        }

        public ContractReportTechnicalStatus GetEnumStatus()
        {
            string value = this.Status.value.ToLower();

            if (value.Equals(ContractReportTechnicalStatus.Draft.ToString().ToLower()))
                return ContractReportTechnicalStatus.Draft;
            if (value.Equals(ContractReportTechnicalStatus.Entered.ToString().ToLower()))
                return ContractReportTechnicalStatus.Entered;
            if (value.Equals(ContractReportTechnicalStatus.Actual.ToString().ToLower()))
                return ContractReportTechnicalStatus.Actual;
            if (value.Equals(ContractReportTechnicalStatus.Returned.ToString().ToLower()))
                return ContractReportTechnicalStatus.Returned;

            throw new Exception("Wrong status");
        }

        public bool IsCompleted
        {
            get
            {
                return this.GetEnumStatus() == Eumis.Documents.Contracts.ContractReportTechnicalStatus.Entered
                    || this.GetEnumStatus() == Eumis.Documents.Contracts.ContractReportTechnicalStatus.Actual;
            }
        }
    }

    public class ContractReportPaymentPVO
    {
        public Guid Gid { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public int VersionSubNum { get; set; }

        public ContractEnumNomenclature Status { get; set; }

        public string StatusNote { get; set; }

        public int VersionNum { get; set; }

        public byte[] Version { get; set; }

        public string RegNumber
        {
            get
            {
                return this.VersionNum + "." + this.VersionSubNum;
            }
        }


        public ContractReportPaymentStatus GetEnumStatus()
        {
            string value = this.Status.value.ToLower();

            if (value.Equals(ContractReportPaymentStatus.Draft.ToString().ToLower()))
                return ContractReportPaymentStatus.Draft;
            if (value.Equals(ContractReportPaymentStatus.Entered.ToString().ToLower()))
                return ContractReportPaymentStatus.Entered;
            if (value.Equals(ContractReportPaymentStatus.Actual.ToString().ToLower()))
                return ContractReportPaymentStatus.Actual;
            if (value.Equals(ContractReportPaymentStatus.Returned.ToString().ToLower()))
                return ContractReportPaymentStatus.Returned;

            throw new Exception("Wrong status");
        }

        public bool IsCompleted
        {
            get
            {
                return this.GetEnumStatus() == Eumis.Documents.Contracts.ContractReportPaymentStatus.Entered
                    || this.GetEnumStatus() == Eumis.Documents.Contracts.ContractReportPaymentStatus.Actual;
            }
        }
    }


    public class ContractReportMicroPVO
    {
        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }

        public ContractEnumNomenclature Status { get; set; }

        public string StatusNote { get; set; }

        public Guid Gid { get; set; }

        public Guid? ExcelBlobKey { get; set; }

        public string ExcelName { get; set; }

        public bool IsFromExternalSystem { get; set; }

        public byte[] Version { get; set; }

        public string RegNumber
        {
            get
            {
                return this.VersionNum + "." + this.VersionSubNum;
            }
        }

        public ContractReportMicroStatus GetEnumStatus()
        {
            string value = this.Status.value.ToLower();

            if (value.Equals(ContractReportMicroStatus.Draft.ToString().ToLower()))
                return ContractReportMicroStatus.Draft;
            if (value.Equals(ContractReportMicroStatus.Entered.ToString().ToLower()))
                return ContractReportMicroStatus.Entered;
            if (value.Equals(ContractReportMicroStatus.Actual.ToString().ToLower()))
                return ContractReportMicroStatus.Actual;
            if (value.Equals(ContractReportMicroStatus.Returned.ToString().ToLower()))
                return ContractReportMicroStatus.Returned;

            throw new Exception("Wrong status");
        }

        public bool IsCompleted
        {
            get
            {
                return this.GetEnumStatus() == Eumis.Documents.Contracts.ContractReportMicroStatus.Entered
                    || this.GetEnumStatus() == Eumis.Documents.Contracts.ContractReportMicroStatus.Actual;
            }
        }
    }

    #endregion

}