using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Eumis.Documents.Enums;
using Eumis.Common;
using Eumis.Documents.Contracts;
using System.Linq;

namespace Eumis.Portal.Web.Areas.Report.Models.Package
{
    public class PackageEditVM
    {
        [Display(Name = "Тип")]
        public R_09991.EnumNomenclature ContractReportType { get; set; }

        [Display(Name = "Друга регистрация")]
        public string OtherRegistration { get; set; }

        [Display(Name = "Място на съхранение")]
        public string StoragePlace { get; set; }

        [Display(Name = "Дата на представяне")]
        public DateTime? SubmitDate { get; set; }

        [Display(Name = "Срок на представяне")]
        public DateTime? SubmitDeadline { get; set; }

        public int OrderNum { get; set; }

        public Guid Gid { get; set; }

        public string Version { get; set; }

        public bool IsEdit { get; set; }

        public IList<ContractProcedureApplicationSection> ProcedureApplicationSections { get; private set; }

        public bool IsBFPContractBudget { get; set; }

        public IList<SerializableSelectListItem> AvailableContractReportTypes { get; private set; }

        public PackageEditVM()
        {
            this.AvailableContractReportTypes = new List<SerializableSelectListItem>();
        }

        public PackageEditVM(IList<ContractProcedureApplicationSection> contractProcedureApplicationSections)
            : this()
        {
            this.ProcedureApplicationSections = contractProcedureApplicationSections;

            this.IsBFPContractBudget = false;

            this.LoadContractReportTypeNomenclature();
            
        }

        public PackageEditVM(ContractReportPVO report, IList<ContractProcedureApplicationSection> contractProcedureApplicationSections)
            : this(contractProcedureApplicationSections)
        {
            if(report != null)
            {
                if (report.ContractReportType != null)
                {
                    this.ContractReportType = new R_09991.EnumNomenclature() { Value = report.ContractReportType.value, Description = report.ContractReportType.description };
                }

                this.OtherRegistration = report.OtherRegistration;
                this.StoragePlace = report.StoragePlace;
                this.SubmitDate = report.SubmitDate;
                this.SubmitDeadline = report.SubmitDeadline;

                this.OrderNum = report.OrderNum;

                this.Gid = report.Gid;

                this.Version = Convert.ToBase64String(report.Version);

                this.IsEdit = true;
            }
        }

        public PackageEditVM(ContractReportPVO report)
            : this()
        {
            if (report != null)
            {
                if (report.ContractReportType != null)
                {
                    this.ContractReportType = new R_09991.EnumNomenclature() { Value = report.ContractReportType.value, Description = report.ContractReportType.description };
                }

                this.OtherRegistration = report.OtherRegistration;
                this.StoragePlace = report.StoragePlace;
                this.SubmitDate = report.SubmitDate;
                this.SubmitDeadline = report.SubmitDeadline;

                this.OrderNum = report.OrderNum;

                this.Gid = report.Gid;

                this.Version = Convert.ToBase64String(report.Version);

                this.IsEdit = true;
            }
        }

        private void LoadContractReportTypeNomenclature() 
        {
            if (IsBFPContractBudget)
            {
                this.AvailableContractReportTypes = Eumis.Common.Helpers.DataUtils.GetEnumSerializableSelectList<ContractReportType>().ToList();
            }
            else 
            {
                this.AvailableContractReportTypes.Add(this.SerializeEnum(Eumis.Documents.Contracts.ContractReportType.AdvancePayment));
                this.AvailableContractReportTypes.Add(this.SerializeEnum(Eumis.Documents.Contracts.ContractReportType.PaymentFinancial));

                if (this.ProcedureApplicationSections.Any(x => x.section.value.ToUpper().Equals(ApplicationSectionType.Team.Name.ToUpper()) && x.isSelected) ||
                    this.ProcedureApplicationSections.Any(x => x.section.value.ToUpper().Equals(ApplicationSectionType.Indicators.Name.ToUpper()) && x.isSelected) ||
                    this.ProcedureApplicationSections.Any(x => x.section.value.ToUpper().Equals(ApplicationSectionType.Activities.Name.ToUpper()) && x.isSelected))
                {
                    this.AvailableContractReportTypes.Add(this.SerializeEnum(Eumis.Documents.Contracts.ContractReportType.Technical));
                    this.AvailableContractReportTypes.Add(this.SerializeEnum(Eumis.Documents.Contracts.ContractReportType.PaymentTechnicalFinancial));
                }
            }



        }

        private SerializableSelectListItem SerializeEnum(ContractReportType contractReportType)
        {
            var listItem = new SerializableSelectListItem()
            { 
                Text = Eumis.Common.Helpers.DataUtils.GetEnumDescription(contractReportType),
                Value = contractReportType.ToString(),
            };

            return listItem;
        }
    }
}
