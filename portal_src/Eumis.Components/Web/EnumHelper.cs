using System.Linq;
using Eumis.Common.Extensions;
using Eumis.Portal.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Eumis.Common;

namespace Eumis.Components.Web
{
    public static class EnumHelper
    {
        /// <summary>
        /// Извлича падащо поле
        /// </summary>
        /// <typeparam name="T">тип на масива от елементи</typeparam>
        /// <param name="enumerable">масив от елементи</param>
        /// <param name="valueSelector">стойност</param>
        /// <param name="textSelector">текст</param>
        /// <param name="isSelected">избран ли е</param>
        /// <param name="skipEmpty">да имали надпис за избор</param>
        /// <returns>масив от елементи за падашото поле</returns>
        public static IEnumerable<SerializableSelectListItem> GetSelectList<T>(this IEnumerable<T> enumerable, Func<T, string> valueSelector, Func<T, string> textSelector, Func<T, bool> isSelected = null, bool skipEmpty = false)
        {
            List<SerializableSelectListItem> list = new List<SerializableSelectListItem>();
            if (!skipEmpty)
            {
                list.Add(new SerializableSelectListItem() { Value = String.Empty, Text = UITextConstants.DropDownEmptyItemText });
            }

            foreach (T item in enumerable)
            {
                list.Add(
                    new SerializableSelectListItem()
                    {
                        Selected = isSelected != null ? isSelected(item) : false,
                        Value = valueSelector(item),
                        Text = textSelector(item),
                    });
            }

            return list;
        }

        public static IEnumerable<SerializableSelectListItem> GetCountries()
        {
            return DataCache.GetAllCountries().GetSelectList(d => d.CountryId.ToString(), d => d.Name, skipEmpty: true);
        }

        public static IEnumerable<SerializableSelectListItem> GetDistricts()
        {
            return DataCache.GetAllDistricts().GetSelectList(d => d.DistrictId.ToString(), d => d.Name, skipEmpty: true);
        }

        public static IEnumerable<SerializableSelectListItem> GetMunicipalities()
        {
            return DataCache.GetAllMunicipalities().GetSelectList(d => d.MunicipalityId.ToString(), d => d.Name, skipEmpty: true);
        }

        public static IEnumerable<SerializableSelectListItem> GetSettlements()
        {
            return DataCache.GetAllSettlements().Take(20).GetSelectList(d => d.SettlementId.ToString(), d => d.Name, skipEmpty: true);
        }

        public static IEnumerable<SerializableSelectListItem> GetKidCodes()
        {
            return DataCache.GetAllKidCodes().GetSelectList(d => d.KidCodeId.ToString(), d => d.Name, skipEmpty: true);
        }

        public static IEnumerable<SerializableSelectListItem> GetCompanyTypes()
        {
            return DataCache.GetAllCompanyTypes().GetSelectList(d => d.CompanyTypeId.ToString(), d => d.Name, skipEmpty: true);
        }

        public static IEnumerable<SerializableSelectListItem> GetCompanyLegalTypes()
        {
            return DataCache.GetAllCompanyLegalTypes().GetSelectList(d => d.CompanyLegalTypeId.ToString(), d => d.Name, skipEmpty: true);
        }

        public static IEnumerable<SerializableSelectListItem> GetCompanySizeTypes()
        {
            return DataCache.GetAllCompanySizeTypes().GetSelectList(d => d.CompanySizeTypeId.ToString(), d => d.Name, skipEmpty: true);
        }

        public static IEnumerable<SerializableSelectListItem> GetMunicipalitiesByDistrict(string parentId)
        {
            return DataCache
                .GetMunicipalitiesByDistrict(int.Parse(parentId))
                .GetSelectList(m => m.MunicipalityId.ToString(), m => m.Name);
        }

        public static IEnumerable<SerializableSelectListItem> GetSettlementsByMunicipality(string parentId)
        {
            return DataCache
                .GetSettlementsByMunicipality(int.Parse(parentId))
                .GetSelectList(s => s.SettlementId.ToString(), s => s.Name);
        }

        public static IEnumerable<SerializableSelectListItem> GetProtectedZones()
        {
            return DataCache.GetAllProtectedZones().GetSelectList(d => d.ProtectedZoneId.ToString(), d => d.Name, skipEmpty: false);
        }

        public static IEnumerable<SerializableSelectListItem> GetNuts1s()
        {
            return DataCache.GetAllNuts1s().GetSelectList(d => d.Nuts1Id.ToString(), d => d.Name, skipEmpty: false);
        }

        public static IEnumerable<SerializableSelectListItem> GetNuts2s()
        {
            return DataCache.GetAllNuts2s().GetSelectList(d => d.Nuts2Id.ToString(), d => d.Name, skipEmpty: false);
        }
    }
}
