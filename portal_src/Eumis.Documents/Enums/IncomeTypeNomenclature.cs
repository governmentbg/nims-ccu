using Eumis.Common;
using Eumis.Common.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eumis.Documents.Enums
{
    [Serializable]
    public class IncomeTypeNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.IncomeTypeNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.Bulgarian));
            }
        }

        public string NameEN
        {
            get
            {
                return App_LocalResources.IncomeTypeNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.English));
            }
        }

        public string Id { get; set; }

        public string ResourceKey { get; set; }

        public static readonly IncomeTypeNomenclature Taxes = new IncomeTypeNomenclature { Id = "taxes", ResourceKey = "Taxes" };
        public static readonly IncomeTypeNomenclature LoansInterestIncome = new IncomeTypeNomenclature { Id = "loansInterestIncome", ResourceKey = "LoansInterestIncome" };
        public static readonly IncomeTypeNomenclature LoansFeesAndCommissions = new IncomeTypeNomenclature { Id = "loansFeesAndCommissions", ResourceKey = "LoansFeesAndCommissions" };
        public static readonly IncomeTypeNomenclature GuaranteeFees = new IncomeTypeNomenclature { Id = "guaranteeFees", ResourceKey = "GuaranteeFees" };
        public static readonly IncomeTypeNomenclature OtherGuaranteeFeesAndCommissions = new IncomeTypeNomenclature { Id = "otherGuaranteeFeesAndCommissions", ResourceKey = "OtherGuaranteeFeesAndCommissions" };
        public static readonly IncomeTypeNomenclature Dividends = new IncomeTypeNomenclature { Id = "dividends", ResourceKey = "Dividends" };
        public static readonly IncomeTypeNomenclature RevenuesFromCapitalInvestmentSales = new IncomeTypeNomenclature { Id = "revenuesFromCapitalInvestmentSales", ResourceKey = "RevenuesFromCapitalInvestmentSales" };
        public static readonly IncomeTypeNomenclature InterestIncomeFromQuasiCapitalInstruments = new IncomeTypeNomenclature { Id = "interestIncomeFromQuasiCapitalInstruments", ResourceKey = "InterestIncomeFromQuasiCapitalInstruments" };
        public static readonly IncomeTypeNomenclature DividendsFromQuasiCapitalInstruments = new IncomeTypeNomenclature { Id = "dividensFromQuasiCapitalInstruments", ResourceKey = "DividensFromQuasiCapitalInstruments" };
        public static readonly IncomeTypeNomenclature ProfitFromSalesOfQuasiCapitalInstruments = new IncomeTypeNomenclature { Id = "profitFromSalesOfQuasiCapitalInstruments", ResourceKey = "ProfitFromSalesOfQuasiCapitalInstruments" };
        public static readonly IncomeTypeNomenclature InterestIncomeFromManagement = new IncomeTypeNomenclature { Id = "interestIncomeFromManagement", ResourceKey = "InterestIncomeFromManagement" };
        public static readonly IncomeTypeNomenclature DividendsFromManagement = new IncomeTypeNomenclature { Id = "dividendsFromManagement", ResourceKey = "DividendsFromManagement" };
        public static readonly IncomeTypeNomenclature ProfitFromSalesOfCapitalInvestments = new IncomeTypeNomenclature { Id = "profitFromSalesOfCapitalInvestments", ResourceKey = "ProfitFromSalesOfCapitalInvestments" };
        public static readonly IncomeTypeNomenclature OtherProfitsFromFinancialInstruments = new IncomeTypeNomenclature { Id = "otherProfitsFromFinancialInstruments", ResourceKey = "OtherProfitsFromFinancialInstruments" };
        public static readonly IncomeTypeNomenclature InterestOnBankAccounts = new IncomeTypeNomenclature { Id = "interestOnBankAccounts", ResourceKey = "InterestOnBankAccounts" };

        public IEnumerable<LocalizedSelectListItem> GetItems()
        {
            return new List<IncomeTypeNomenclature>() {
                Taxes,
                LoansInterestIncome,
                LoansFeesAndCommissions,
                GuaranteeFees,
                OtherGuaranteeFeesAndCommissions,
                Dividends,
                RevenuesFromCapitalInvestmentSales,
                InterestIncomeFromQuasiCapitalInstruments,
                ProfitFromSalesOfQuasiCapitalInstruments,
                InterestIncomeFromManagement,
                DividendsFromManagement,
                ProfitFromSalesOfCapitalInvestments,
                OtherProfitsFromFinancialInstruments,
                InterestOnBankAccounts
            }.Select(e => new LocalizedSelectListItem() { Name = e.Name, Value = e.Id, NameEN = e.NameEN }).ToList(); ;
        }
    }
}
