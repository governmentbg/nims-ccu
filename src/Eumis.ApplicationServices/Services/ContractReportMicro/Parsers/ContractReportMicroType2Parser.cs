using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.ContractReports.ViewObjects;
using Eumis.Domain.Contracts.ContractReportMicros;

namespace Eumis.ApplicationServices.Services.ContractReportMicro
{
    public class ContractReportMicroType2Parser : ContractReportMicroParserBase, IContractReportMicroType2Parser
    {
        private static readonly Regex UinRegex = new Regex(@"\d{10}");

        private static readonly Dictionary<int, string> MicroType2Headers = new Dictionary<int, string>
        {
            { 1,  "№" },
            { 2,  "Име" },
            { 3,  "Презиме" },
            { 4,  "Фамилия" },
            { 5,  "ЕГН" },
            { 6,  "Пол" },
            { 7,  "Възраст" },
            { 8,  "Статут на пазара на труда" },
            { 9,  "Степен на завършено образование" },
            { 10, "Настоящ адрес (област)" },
            { 11, "Настоящ адрес (населено място)" },
            { 12, "Телефон за контакт" },
            { 13, "E-mail" },
            { 14, "Мигрант" },
            { 15, "Участник с произход от друга държава" },
            { 16, "Малцинства" },
            { 17, "Роми" },
            { 18, "Хора с увреждания" },
            { 19, "Бездомни или засегнати от жилищно изключване" },
            { 20, "Други хора в неравностойно положение" },
            { 21, "Участници, които живеят в безработни домакинства" },
            { 22, "Участници, които живеят в безработни домакинства с деца на издръжка" },
            { 23, "Участници, които живеят в едночленно домакинство с деца на издръжка" },
            { 24, "Дата на включване в дейности (година, месец, ден)" },
            { 27, "Дейност" },
            { 28, "Място на изпълнение на дейността (област)" },
            { 29, "Място на изпълнение на дейността (населено място)" },
            { 30, "Статут на участие" },
            { 31, "Дата на напускане на дейности (година, месец, ден)" },
            { 34, "Причини за отпадане" },
            { 35, "Статут на участника при излизане от операцията" },
        };

        public ContractReportMicroType2Parser(
            IContractReportMicrosDistrictNomsRepository contractReportMicrosDistrictNomsRepository,
            IContractReportMicrosMunicipalityNomsRepository contractReportMicrosMunicipalityNomsRepository,
            IContractReportMicrosSettlementNomsRepository contractReportMicrosSettlementNomsRepository)
            : base(
                  contractReportMicrosDistrictNomsRepository,
                  contractReportMicrosMunicipalityNomsRepository,
                  contractReportMicrosSettlementNomsRepository)
        {
        }

        public IList<ContractReportMicrosType2Item> ParseExcel(
            int contractReportMicroId,
            Stream excelStream,
            out IList<string> errors,
            out IList<string> warnings)
        {
            errors = new List<string>();
            warnings = new List<string>();

            var existingUins = new Dictionary<string, int>();

            var rows = this.ReadExcel(excelStream, 35);

            int rowNumber = 0;
            IList<ContractReportMicrosType2Item> items = new List<ContractReportMicrosType2Item>();
            foreach (var row in rows)
            {
                rowNumber++;

                if (rowNumber < 2)
                {
                    continue;
                }
                else if (rowNumber == 2)
                {
                    if (!this.AreHeadersValid(row, MicroType2Headers))
                    {
                        errors.Add(FileNotMatchingTemplateError);
                        return null;
                    }

                    continue;
                }
                else if (!row.Any(c => !string.IsNullOrWhiteSpace(c)))
                {
                    break;
                }

                var item = new ContractReportMicrosType2Item
                {
                    ContractReportMicroId = contractReportMicroId,
                };

                string numberStr = row[1];
                if (string.IsNullOrWhiteSpace(numberStr))
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Номер",
                        "задължително поле"));
                }
                else if (numberStr.Length > 200)
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Номер",
                        "полето може да съдържа максимум 200 символа"));
                }
                else
                {
                    item.Number = numberStr;
                }

                string firstNameStr = row[2];
                if (string.IsNullOrWhiteSpace(firstNameStr))
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Име",
                        "задължително поле"));
                }

                if (firstNameStr.Length > 200)
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Име",
                        "полето може да съдържа максимум 200 символа"));
                }
                else
                {
                    item.FirstName = firstNameStr;
                }

                string middleNameStr = row[3];
                if (string.IsNullOrWhiteSpace(middleNameStr))
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Презиме",
                        "задължително поле"));
                }
                else if (middleNameStr.Length > 200)
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Презиме",
                        "полето може да съдържа максимум 200 символа"));
                }
                else
                {
                    item.MiddleName = middleNameStr;
                }

                string lastNameStr = row[4];
                if (string.IsNullOrWhiteSpace(lastNameStr))
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Фамилия",
                        "задължително поле"));
                }
                else if (lastNameStr.Length > 200)
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Фамилия",
                        "полето може да съдържа максимум 200 символа"));
                }
                else
                {
                    item.LastName = lastNameStr;
                }

                var uinStr = row[5];
                if (string.IsNullOrWhiteSpace(uinStr))
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "ЕГН",
                        "задължително поле"));
                }
                else if (!UinRegex.IsMatch(uinStr))
                {
                    errors.Add(this.GetError(rowNumber, "ЕГН"));
                }
                else
                {
                    if (existingUins.ContainsKey(uinStr))
                    {
                        warnings.Add($"Стойността в полето \"ЕГН\" на ред \"{rowNumber}\" се дублира със стойността на ред \"{existingUins[uinStr]}\"");
                    }
                    else
                    {
                        existingUins.Add(uinStr, rowNumber);
                    }

                    item.Uin = uinStr;
                }

                ContractReportMicroType2ItemGender? gender = null;
                if (this.GetEnum<ContractReportMicroType2ItemGender>(row, 6, out gender))
                {
                    if (gender.HasValue)
                    {
                        item.Gender = gender;
                    }
                    else
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Пол",
                            "задължително поле"));
                    }
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Пол"));
                }

                int? age = null;
                if (this.GetInteger(row, 7, out age))
                {
                    if (age.HasValue)
                    {
                        item.Age = age;
                    }
                    else
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Възраст",
                            "задължително поле"));
                    }
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Възраст",
                        "очаквана стойност: цяло число"));
                }

                ContractReportMicroType2ItemOccupation? occupation = null;
                if (this.GetEnum<ContractReportMicroType2ItemOccupation>(row, 8, out occupation))
                {
                    if (occupation.HasValue)
                    {
                        item.Occupation = occupation;
                    }
                    else
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Статут на пазара на труда",
                            "задължително поле"));
                    }
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Статут на пазара на труда"));
                }

                ContractReportMicroType2ItemEducation? education = null;
                if (this.GetEnum<ContractReportMicroType2ItemEducation>(row, 9, out education))
                {
                    if (education.HasValue)
                    {
                        item.Education = education;
                    }
                    else
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Степен на завършено образование",
                            "задължително поле"));
                    }
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Степен на завършено образование"));
                }

                int? addrDistrictId = null;
                if (this.GetDistrictId(row, 10, out addrDistrictId))
                {
                    if (addrDistrictId.HasValue)
                    {
                        item.AddressDistrictId = addrDistrictId;
                    }
                    else
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Настоящ адрес (област)",
                            "задължително поле"));
                    }
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Настоящ адрес (област)"));
                }

                ContractReportMicrosSettlementNomVO addrSettlement = null;
                if (this.GetSettlement(row, 11, item.AddressDistrictId, out addrSettlement))
                {
                    if (addrSettlement != null &&
                        item.AddressDistrictId.HasValue &&
                        addrSettlement.DistrictId != item.AddressDistrictId)
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Настоящ адрес (населено място)",
                            "избраното населено място не се намира в тази област"));
                    }
                    else if (addrSettlement == null)
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Настоящ адрес (населено място)",
                            "задължително поле"));
                    }
                    else
                    {
                        item.AddressSettlementId = addrSettlement.NomValueId;
                    }
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Настоящ адрес (населено място)"));
                }

                string phoneStr = row[12];
                if (string.IsNullOrWhiteSpace(phoneStr) || phoneStr.Length <= 200)
                {
                    item.Phone = phoneStr;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Телефон за контакт",
                        "полето може да съдържа максимум 200 символа"));
                }

                string emailStr = row[13];
                if (string.IsNullOrWhiteSpace(emailStr) || emailStr.Length <= 200)
                {
                    item.Email = emailStr;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "e-mail",
                        "полето може да съдържа максимум 200 символа"));
                }

                bool? isEmigrant = null;
                if (this.GetBoolean(row, 14, out isEmigrant))
                {
                    if (isEmigrant.HasValue)
                    {
                        item.IsEmigrant = isEmigrant;
                    }
                    else
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Мигрант",
                            "задължително поле"));
                    }
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Мигрант"));
                }

                bool? isForeigner = null;
                if (this.GetBoolean(row, 15, out isForeigner))
                {
                    if (isForeigner.HasValue)
                    {
                        item.IsForeigner = isForeigner;
                    }
                    else
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Участник с произход от друга държава",
                            "задължително поле"));
                    }
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Участник с произход от друга държава"));
                }

                bool? isMinority = null;
                if (this.GetBoolean(row, 16, out isMinority))
                {
                    if (isMinority.HasValue)
                    {
                        item.IsMinority = isMinority;
                    }
                    else
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Малцинства",
                            "задължително поле"));
                    }
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Малцинства"));
                }

                bool? isGypsy = null;
                if (this.GetBoolean(row, 17, out isGypsy))
                {
                    if (isGypsy.HasValue)
                    {
                        item.IsGypsy = isGypsy;
                    }
                    else
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Роми",
                            "задължително поле"));
                    }
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Роми"));
                }

                bool? isDisabledPerson = null;
                if (this.GetBoolean(row, 18, out isDisabledPerson))
                {
                    if (isDisabledPerson.HasValue)
                    {
                        item.IsDisabledPerson = isDisabledPerson;
                    }
                    else
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Хора с увреждания",
                            "задължително поле"));
                    }
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Хора с увреждания"));
                }

                bool? isHomeless = null;
                if (this.GetBoolean(row, 19, out isHomeless))
                {
                    if (isHomeless.HasValue)
                    {
                        item.IsHomeless = isHomeless;
                    }
                    else
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Бездомни / засегнати от жилищно изключване",
                            "задължително поле"));
                    }
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Бездомни / засегнати от жилищно изключване"));
                }

                var disadvantagedPerson = row[20];
                if (string.IsNullOrWhiteSpace(disadvantagedPerson))
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Други хора в неравностойно положение",
                        "задължително поле"));
                }
                else
                {
                    item.DisadvantagedPerson = disadvantagedPerson;
                }

                bool? isLivingInUnemployedHousehold = null;
                if (this.GetBoolean(row, 21, out isLivingInUnemployedHousehold))
                {
                    if (isLivingInUnemployedHousehold.HasValue)
                    {
                        item.IsLivingInUnemployedHousehold = isLivingInUnemployedHousehold;
                    }
                    else
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Участници, които живеят в безработни домакинства",
                            "задължително поле"));
                    }
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Участници, които живеят в безработни домакинства"));
                }

                bool? isLivingInUnemployedHouseholdWithChildren = null;
                if (this.GetBoolean(row, 22, out isLivingInUnemployedHouseholdWithChildren))
                {
                    if (isLivingInUnemployedHouseholdWithChildren.HasValue)
                    {
                        item.IsLivingInUnemployedHouseholdWithChildren = isLivingInUnemployedHouseholdWithChildren;
                    }
                    else
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Участници, които живеят в безработни домакинства с деца на издръжка",
                            "задължително поле"));
                    }
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Участници, които живеят в безработни домакинства с деца на издръжка"));
                }

                bool? isLivingInFamilyOfOneWithChildren = null;
                if (this.GetBoolean(row, 23, out isLivingInFamilyOfOneWithChildren))
                {
                    if (isLivingInFamilyOfOneWithChildren.HasValue)
                    {
                        item.IsLivingInFamilyOfOneWithChildren = isLivingInFamilyOfOneWithChildren;
                    }
                    else
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Участници, които живеят в едночленно домакинство с деца на издръжка",
                            "задължително поле"));
                    }
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Участници, които живеят в едночленно домакинство с деца на издръжка"));
                }

                DateTime? joiningDate = null;
                if (this.GetDate(row, 24, 25, 26, out joiningDate))
                {
                    if (joiningDate.HasValue)
                    {
                        item.JoiningDate = joiningDate;
                    }
                    else
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Дата на включване в дейности",
                            "задължително поле"));
                    }
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Дата на включване в дейности"));
                }

                // the activity can be a value from a predefined nomenclature or any text
                var activity = row[27];
                if (string.IsNullOrWhiteSpace(activity) || activity == EmptyNomStr)
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Дейност",
                        "задължително поле"));
                }
                else
                {
                    item.Activity = activity;
                }

                int? activityDistrictId = null;
                if (this.GetDistrictId(row, 28, out activityDistrictId))
                {
                    if (activityDistrictId.HasValue)
                    {
                        item.ActivityPlaceDistrictId = activityDistrictId;
                    }
                    else
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Място на изпълнение на дейността (област)",
                            "задължително поле"));
                    }
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Място на изпълнение на дейността (област)"));
                }

                ContractReportMicrosSettlementNomVO activitySettlement = null;
                if (this.GetSettlement(row, 29, item.ActivityPlaceDistrictId, out activitySettlement))
                {
                    if (activitySettlement != null &&
                        item.ActivityPlaceDistrictId.HasValue &&
                        activitySettlement.DistrictId != item.ActivityPlaceDistrictId)
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Място на изпълнение на дейността (населено място)",
                            "избраното населено място не се намира в тази област"));
                    }
                    else if (activitySettlement == null)
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Място на изпълнение на дейността (населено място)",
                            "задължително поле"));
                    }
                    else
                    {
                        item.ActivityPlaceSettlementId = activitySettlement.NomValueId;
                    }
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Място на изпълнение на дейността (населено място)"));
                }

                ContractReportMicroType2ItemParticipationState? participationState = null;
                if (this.GetEnum(row, 30, out participationState))
                {
                    item.ParticipationState = participationState;
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Статут на участие"));
                }

                DateTime? leavingDate = null;
                if (this.GetDate(row, 31, 32, 33, out leavingDate))
                {
                    item.LeavingDate = leavingDate;
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Дата на напускане на дейности"));
                }

                ContractReportMicroType2ItemCancelationReason? cancelationReason = null;
                if (this.GetEnum(row, 34, out cancelationReason))
                {
                    item.CancelationReason = cancelationReason;
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Причини за отпадане"));
                }

                ContractReportMicroType2ItemLeavingState? leavingState = null;
                if (this.GetEnum(row, 35, out leavingState))
                {
                    item.LeavingState = leavingState;
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Статут на участника при излизане от операцията"));
                }

                items.Add(item);
            }

            return items;
        }
    }
}
