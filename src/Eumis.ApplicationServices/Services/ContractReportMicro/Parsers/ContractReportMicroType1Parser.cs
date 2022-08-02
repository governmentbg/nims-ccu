using System.Collections.Generic;
using System.IO;
using System.Linq;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.ContractReports.ViewObjects;
using Eumis.Domain.Contracts.ContractReportMicros;

namespace Eumis.ApplicationServices.Services.ContractReportMicro
{
    public class ContractReportMicroType1Parser : ContractReportMicroParserBase, IContractReportMicroType1Parser
    {
        private static readonly Dictionary<int, string> MicroType1Headers = new Dictionary<int, string>
        {
            { 1,  "ОБЛАСТ" },
            { 2,  "ОБЩИНА" },
            { 3,  "ОБЩ БРОЙ НА ЛИЦАТА, ПОЛУЧАВАЩИ ХРАНИТЕЛНИ ПРОДУКТИ ЗА ПОДПОМАГАНЕ" },
            { 4,  "ДЕЦА на възраст 15 години или по-малки" },
            { 5,  "ЛИЦА на възраст 65 години или по-възрастни" },
            { 6,  "ЖЕНИ" },
            { 7,  "МИГРАНТИ" },
            { 8,  "ЧУЖДИ ГРАЖДАНИ" },
            { 9,  "МАЛЦИНСТВА" },
            { 10, "РОМИ" },
            { 11, "ХОРА С УВРЕЖДАНИЯ" },
            { 12, "БЕЗДОМНИ ЛИЦА" },
        };

        public ContractReportMicroType1Parser(
            IContractReportMicrosDistrictNomsRepository contractReportMicrosDistrictNomsRepository,
            IContractReportMicrosMunicipalityNomsRepository contractReportMicrosMunicipalityNomsRepository,
            IContractReportMicrosSettlementNomsRepository contractReportMicrosSettlementNomsRepository)
            : base(
                  contractReportMicrosDistrictNomsRepository,
                  contractReportMicrosMunicipalityNomsRepository,
                  contractReportMicrosSettlementNomsRepository)
        {
        }

        public IList<ContractReportMicrosType1Item> ParseExcel(
            int contractReportMicroId,
            Stream excelStream,
            out IList<string> errors)
        {
            errors = new List<string>();

            var rows = this.ReadExcel(excelStream, 12);

            int rowNumber = 0;
            IList<ContractReportMicrosType1Item> items = new List<ContractReportMicrosType1Item>();
            foreach (var row in rows)
            {
                rowNumber++;

                if (rowNumber < 5)
                {
                    continue;
                }
                else if (rowNumber == 5)
                {
                    if (!this.AreHeadersValid(row, MicroType1Headers))
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

                var item = new ContractReportMicrosType1Item
                {
                    ContractReportMicroId = contractReportMicroId,
                };

                int? districtId = null;
                if (this.GetDistrictId(row, 1, out districtId))
                {
                    item.DistrictId = districtId;
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "ОБЛАСТ"));
                }

                ContractReportMicrosMunicipalityNomVO municipality = null;
                if (this.GetMunicipality(row, 2, out municipality))
                {
                    if (municipality != null &&
                        item.DistrictId.HasValue &&
                        municipality.DistrictId != item.DistrictId)
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "ОБЩИНА",
                            "избраната община не се намира в тази област"));
                    }
                    else
                    {
                        item.MunicipalityId = municipality == null ? (int?)null : municipality.NomValueId;
                    }
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "ОБЩИНА"));
                }

                int? totalCount = null;
                if (this.GetInteger(row, 3, out totalCount))
                {
                    item.TotalCount = totalCount;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "ОБЩ БРОЙ НА ЛИЦАТА, ПОЛУЧАВАЩИ ХРАНИТЕЛНИ ПРОДУКТИ ЗА ПОДПОМАГАНЕ",
                        "очаквана стойност: цяло число"));
                }

                int? childrensCount = null;
                if (this.GetInteger(row, 4, out childrensCount))
                {
                    item.ChildrensCount = childrensCount;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "ДЕЦА на възраст 15 години или по-малки",
                        "очаквана стойност: цяло число"));
                }

                int? seniorsCount = null;
                if (this.GetInteger(row, 5, out seniorsCount))
                {
                    item.SeniorsCount = seniorsCount;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "ЛИЦА на възраст 65 години или по-възрастни",
                        "очаквана стойност: цяло число"));
                }

                int? femalesCount = null;
                if (this.GetInteger(row, 6, out femalesCount))
                {
                    item.FemalesCount = femalesCount;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "ЖЕНИ",
                        "очаквана стойност: цяло число"));
                }

                int? emigrantsCount = null;
                if (this.GetInteger(row, 7, out emigrantsCount))
                {
                    item.EmigrantsCount = emigrantsCount;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "МИГРАНТИ",
                        "очаквана стойност: цяло число"));
                }

                int? foreignCitizensCount = null;
                if (this.GetInteger(row, 8, out foreignCitizensCount))
                {
                    item.ForeignCitizensCount = foreignCitizensCount;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "ЧУЖДИ ГРАЖДАНИ",
                        "очаквана стойност: цяло число"));
                }

                int? minoritiesCount = null;
                if (this.GetInteger(row, 9, out minoritiesCount))
                {
                    item.MinoritiesCount = minoritiesCount;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "МАЛЦИНСТВА",
                        "очаквана стойност: цяло число"));
                }

                int? gypsiesCount = null;
                if (this.GetInteger(row, 10, out gypsiesCount))
                {
                    item.GypsiesCount = gypsiesCount;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "РОМИ",
                        "очаквана стойност: цяло число"));
                }

                int? disabledPersonsCount = null;
                if (this.GetInteger(row, 11, out disabledPersonsCount))
                {
                    item.DisabledPersonsCount = disabledPersonsCount;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "ХОРА С УВРЕЖДАНИЯ",
                        "очаквана стойност: цяло число"));
                }

                int? homelessCount = null;
                if (this.GetInteger(row, 12, out homelessCount))
                {
                    item.HomelessCount = homelessCount;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "БЕЗДОМНИ ЛИЦА",
                        "очаквана стойност: цяло число"));
                }

                items.Add(item);
            }

            return items;
        }
    }
}
