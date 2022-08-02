using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.Companies.Repositories;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.Contracts;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using Eumis.Public.Resources;
using Eumis.Public.Web.Models.Company;

namespace Eumis.Public.Web.Controllers
{
    public partial class CompanyController : BaseWithExportController
    {
        private readonly IUmisRepository umisRepository;
        private readonly ICompaniesRepository companiesRepository;

        public CompanyController(
            IMapsRepository mapsRepository,
            IInfrastructureRepository infrastructureRepository,
            ICompaniesRepository companiesRepository,
            IUmisRepository umisRepository)
            : base(mapsRepository, infrastructureRepository)
        {
            this.umisRepository = umisRepository;
            this.companiesRepository = companiesRepository;
        }

        [DecryptParametersAttribute(IdsParamName =
            new string[]
            {
                    "uin",
            })]
        public virtual ActionResult Index(string uin, UinType uinType, CompanyEnumType type, bool isHistoric = false)
        {
            var model = this.InitializeModel(uin, uinType, isHistoric, type);
            return this.View(model);
        }

        public override ExportTemplate RenderTemplate()
        {
            var template = new ExportTemplate("company");
            template.Sheet.Name = "company";

            string uin = string.Empty;
            UinType uinType = UinType.Eik;
            bool isHistoric = false;
            CompanyEnumType type = CompanyEnumType.Beneficiary;

            if (!string.IsNullOrEmpty(this.Request.QueryString["uin"]))
            {
                uin = ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["uin"]);
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["uinType"]))
            {
                uinType = (UinType)Enum.Parse(typeof(UinType), this.Request.QueryString["uinType"]);
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["isHistoric"]))
            {
                isHistoric = Convert.ToBoolean(this.Request.QueryString["isHistoric"]);
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["type"]))
            {
                type = (CompanyEnumType)Enum.Parse(typeof(CompanyEnumType), this.Request.QueryString["type"]);
            }

            var company = this.InitializeModel(uin, uinType, isHistoric, type);

            var table = new ExportTable(Texts.Company_Index_Identification);
            var headerRow = new ExportRow();

            for (int i = 0; i < 2; i++)
            {
                headerRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            headerRow.Cells[0].Value = Texts.Company_Index_Name;
            headerRow.Cells[1].Value = Texts.Company_Index_Seat;

            table.Rows.Add(headerRow);
            var row = new ExportRow();

            row.Cells.Add(company.Name.ToExportCell());
            row.Cells.Add(company.Seat.ToExportCell());

            table.Rows.Add(row);

            template.Sheet.Tables.Add(table);

            if (company.Type == CompanyEnumType.Beneficiary)
            {
                table = new ExportTable(Texts.Company_Index_BeneficiaryProjectsList);
                headerRow = new ExportRow();

                for (int i = 0; i < 8; i++)
                {
                    headerRow.Cells.Add(new ExportCell { IsHeader = true });
                }

                headerRow.Cells[0].Value = Texts.Company_Index_ProjectName;
                headerRow.Cells[1].Value = Texts.Company_Index_Start;
                headerRow.Cells[2].Value = Texts.Company_Index_TotalAmount;
                headerRow.Cells[3].Value = Texts.Company_Index_BFP;
                headerRow.Cells[4].Value = Texts.Project_Search_FinancinBeneficiary;
                headerRow.Cells[5].Value = Texts.Global_Payed;
                headerRow.Cells[6].Value = Texts.Company_Index_Duration;
                headerRow.Cells[7].Value = Texts.Company_Index_BFPContractStatus;

                table.Rows.Add(headerRow);

                foreach (var contract in company.Projects.BeneficaryProjects)
                {
                    row = new ExportRow();

                    row.Cells.Add(contract.TransName.ToExportCell());
                    row.Cells.Add(contract.StartDate.ToExportCell());
                    row.Cells.Add(contract.ContractedTotalAmount.ToExportCell());
                    row.Cells.Add(contract.ContractedBFPAmount.ToExportCell());
                    row.Cells.Add(contract.ContractedSelfAmount.ToExportCell());
                    row.Cells.Add(contract.PaidTotalAmount.ToExportCell());
                    row.Cells.Add(contract.MonthsDuration.ToExportCell());
                    row.Cells.Add(contract.StatusDescription.ToExportCell());

                    table.Rows.Add(row);
                }

                template.Sheet.Tables.Add(table);
            }

            if (company.Type == CompanyEnumType.Partner)
            {
                table = new ExportTable(Texts.Company_Index_PartnerProjectsList);
                headerRow = new ExportRow();

                for (int i = 0; i < 4; i++)
                {
                    headerRow.Cells.Add(new ExportCell { IsHeader = true });
                }

                headerRow.Cells[0].Value = Texts.Company_Index_ProjectName;
                headerRow.Cells[1].Value = Texts.Company_Index_Start;
                headerRow.Cells[2].Value = Texts.Company_Index_Duration;
                headerRow.Cells[3].Value = Texts.Company_Index_BFPContractStatus;

                table.Rows.Add(headerRow);

                foreach (var contract in company.Projects.PartnerProjects)
                {
                    row = new ExportRow();

                    row.Cells.Add(contract.TransName.ToExportCell());
                    row.Cells.Add(contract.StartDate.ToExportCell());
                    row.Cells.Add(contract.MonthsDuration.ToExportCell());
                    row.Cells.Add(contract.StatusDescription.ToExportCell());

                    table.Rows.Add(row);
                }

                template.Sheet.Tables.Add(table);
            }

            if (company.Type == CompanyEnumType.Contractor)
            {
                table = new ExportTable(Texts.Company_Index_ContractorProjectsList);
                headerRow = new ExportRow();

                for (int i = 0; i < 4; i++)
                {
                    headerRow.Cells.Add(new ExportCell { IsHeader = true });
                }

                headerRow.Cells[0].Value = Texts.Company_Index_ProjectName;
                headerRow.Cells[1].Value = Texts.Company_Index_EumisNumber;
                headerRow.Cells[2].Value = Texts.Company_Index_ContractCount;
                headerRow.Cells[3].Value = Texts.Company_Index_ContractAmount;

                table.Rows.Add(headerRow);

                foreach (var contract in company.Projects.ContractorProjects)
                {
                    row = new ExportRow();

                    row.Cells.Add(contract.TransName.ToExportCell());
                    row.Cells.Add(contract.RegNumber.ToExportCell());
                    row.Cells.Add(contract.ContractCount.ToExportCell());
                    row.Cells.Add(contract.ContractAmount.ToExportCell());

                    table.Rows.Add(row);
                }

                template.Sheet.Tables.Add(table);
            }

            if (company.Type == CompanyEnumType.Subcontractor)
            {
                table = new ExportTable(Texts.Company_Index_SubcontractorProjectsList);
                headerRow = new ExportRow();

                for (int i = 0; i < 4; i++)
                {
                    headerRow.Cells.Add(new ExportCell { IsHeader = true });
                }

                headerRow.Cells[0].Value = Texts.Company_Index_ProjectName;
                headerRow.Cells[1].Value = Texts.Company_Index_EumisNumber;
                headerRow.Cells[2].Value = Texts.Company_Index_ContractCount;
                headerRow.Cells[3].Value = Texts.Company_Index_ContractAmount;

                table.Rows.Add(headerRow);

                foreach (var contract in company.Projects.SubcontractorProjects)
                {
                    row = new ExportRow();

                    row.Cells.Add(contract.TransName.ToExportCell());
                    row.Cells.Add(contract.RegNumber.ToExportCell());
                    row.Cells.Add(contract.ContractCount.ToExportCell());
                    row.Cells.Add(contract.ContractAmount.ToExportCell());

                    table.Rows.Add(row);
                }

                template.Sheet.Tables.Add(table);
            }

            if (company.Type == CompanyEnumType.Member)
            {
                table = new ExportTable(Texts.Company_Index_MemberProjectsList);
                headerRow = new ExportRow();

                for (int i = 0; i < 4; i++)
                {
                    headerRow.Cells.Add(new ExportCell { IsHeader = true });
                }

                headerRow.Cells[0].Value = Texts.Company_Index_ProjectName;
                headerRow.Cells[1].Value = Texts.Company_Index_EumisNumber;
                headerRow.Cells[2].Value = Texts.Company_Index_ContractCount;
                headerRow.Cells[3].Value = Texts.Company_Index_ContractAmount;

                table.Rows.Add(headerRow);

                foreach (var contract in company.Projects.MemberProjects)
                {
                    row = new ExportRow();

                    row.Cells.Add(contract.TransName.ToExportCell());
                    row.Cells.Add(contract.RegNumber.ToExportCell());
                    row.Cells.Add(contract.ContractCount.ToExportCell());
                    row.Cells.Add(contract.ContractAmount.ToExportCell());

                    table.Rows.Add(row);
                }

                template.Sheet.Tables.Add(table);
            }

            template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
            {
                { 1, 300 },
                { 2, 300 },
                { 3, 150 },
                { 4, 400 },
                { 5, 200 },
                { 6, 100 },
                { 7, 300 },
                { 8, 350 },
            };

            template.Sheet.EndNotes.Add(Texts.Global_Note + ":");
            template.Sheet.EndNotes.Add(Texts.Global_NoteUnderlined);
            template.Sheet.EndNotes.Add(Texts.Global_NoteBGN);

            return template;
        }

        private CompanyDetailsVM InitializeModel(string uin, UinType uinType, bool isHistoric, CompanyEnumType type)
        {
            CompanyDetailsVM vm = new CompanyDetailsVM();

            vm.Type = type;
            vm.Uin = uin;
            vm.UinType = uinType;
            vm.IsHistoric = isHistoric;

            CompanyVO company = new CompanyVO();

            if (type == CompanyEnumType.Beneficiary)
            {
                company = this.umisRepository.GetContractBeneficiary(uin, uinType, isHistoric);
                vm.Projects = this.companiesRepository.GetBeneficaryProjects(uin);
            }
            else if (type == CompanyEnumType.Partner)
            {
                company = this.umisRepository.GetContractPartner(uin, uinType, isHistoric);
                vm.Projects = this.companiesRepository.GetPartnerProjects(uin);
            }
            else if (type == CompanyEnumType.Contractor)
            {
                company = this.umisRepository.GetContractContractor(uin, uinType, isHistoric);
                vm.Projects = this.companiesRepository.GetContractorProjects(uin);
            }
            else if (type == CompanyEnumType.Subcontractor)
            {
                company = this.umisRepository.GetContractSubcontractor(ContractSubcontractType.Subcontractor, uin, uinType, isHistoric);
                vm.Projects = this.companiesRepository.GetSubcontractorProjects(uin);
            }
            else if (type == CompanyEnumType.Member)
            {
                company = this.umisRepository.GetContractSubcontractor(ContractSubcontractType.Member, uin, uinType, isHistoric);
                vm.Projects = this.companiesRepository.GetMemberProjects(uin);
            }

            vm.Name = company.TransFullName;
            vm.Seat = company.SeatFullName;

            return vm;
        }
    }
}
