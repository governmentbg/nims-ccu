using System.Collections.Generic;
using System.IO;
using System.Linq;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.ContractReports.ViewObjects;
using Eumis.Domain.Contracts.ContractReportMicros;

namespace Eumis.ApplicationServices.Services.ContractReportMicro
{
    public class ContractReportMicroType4Parser : ContractReportMicroParserBase, IContractReportMicroType4Parser
    {
        private static readonly Dictionary<int, Dictionary<int, string>> MicroType4Headers = new Dictionary<int, Dictionary<int, string>>
        {
            {
                1,
                new Dictionary<int, string>
                {
                    { 1,  "ОБЛАСТ" },
                    { 2,  "ОБЩИНА" },
                    { 3,  "КОЛИЧЕСТВО ПЛОДОВЕ И ЗЕЛЕНЧУЦИ" },
                    { 6,  "КОЛИЧЕСТВО МЕСО, ЯЙЦА, РИБА И РИБНИ ПРОДУКТИ" },
                    { 10, "КОЛИЧЕСТВО БРАШНО, ХЛЯБ, КАРТОФИ, ОРИЗ И ДРУГИ СЪДЪРЖАЩИ СКОРБЯЛА ПРОДУКТИ" },
                    { 16, "КОЛИЧЕСТВО ЗАХАР" },
                    { 17, "КОЛИЧЕСТВО МЛЕЧНИ ПРОДУКТИ" },
                    { 18, "КОЛИЧЕСТВО МАЗНИНИ, МАСЛА" },
                    { 19, "КОЛИЧЕСТВО ГОТОВИ ХРАНИ, ДРУГИ ХРАНИ (КОИТО НЕ ПОПАДАТ В ПРЕДХОДНИТЕ КАТЕГОРИИ)" },
                    { 22, "ОБЩ БРОЙ РАЗПРЕДЕЛЕНИ ЯСТИЯ ИЗЦЯЛО ФИНАНСИРАНИ ПО ОП" },
                    { 23, "ОБЩ БРОЙ РАЗПРЕДЕЛЕНИ ПАКЕТИ С ХРАНА, ИЗЦЯЛО ФИНАНСИРАНИ ПО ОП" },
                }
            },
            {
                2,
                new Dictionary<int, string>
                {
                    { 3,  "Плодове (кг.)" },
                    { 4,  "Зеленчуци (кг.)" },
                    { 5,  "Общо" },
                    { 6,  "Месо (кг.)" },
                    { 7,  "Яйца (кг.)" },
                    { 8,  "Риба и рибни продукти (кг.)" },
                    { 9,  "Общо" },
                    { 10, "Брашно (кг.)" },
                    { 11, "Хляб (кг.)" },
                    { 12, "Картофи (кг.)" },
                    { 13, "Ориз (кг.)" },
                    { 14, "Други съдържащи скорбяла продукти (кг.)" },
                    { 15, "Общо" },
                    { 16, "Захар (кг.)" },
                    { 17, "Млечни продукти (кг.)" },
                    { 18, "Мазнина, масла (кг.)" },
                    { 19, "Готови храни (кг.)" },
                    { 20, "Други храни (които не попадат в предходните категории) (кг.)" },
                    { 21, "Общо" },
                    { 22, "Брой ястия" },
                    { 23, "Брой пакети" },
                }
            },
        };

        public ContractReportMicroType4Parser(
            IContractReportMicrosDistrictNomsRepository contractReportMicrosDistrictNomsRepository,
            IContractReportMicrosMunicipalityNomsRepository contractReportMicrosMunicipalityNomsRepository,
            IContractReportMicrosSettlementNomsRepository contractReportMicrosSettlementNomsRepository)
            : base(
                  contractReportMicrosDistrictNomsRepository,
                  contractReportMicrosMunicipalityNomsRepository,
                  contractReportMicrosSettlementNomsRepository)
        {
        }

        public IList<ContractReportMicrosType4Item> ParseExcel(
            int contractReportMicroId,
            Stream excelStream,
            out IList<string> errors)
        {
            errors = new List<string>();

            var rows = this.ReadExcel(excelStream, 23);

            int rowNumber = 0;
            IList<ContractReportMicrosType4Item> items = new List<ContractReportMicrosType4Item>();
            foreach (var row in rows)
            {
                rowNumber++;

                if (rowNumber == 1)
                {
                    if (!this.AreHeadersValid(row, MicroType4Headers[1]))
                    {
                        errors.Add(FileNotMatchingTemplateError);
                        return null;
                    }

                    continue;
                }
                else if (rowNumber == 2)
                {
                    if (!this.AreHeadersValid(row, MicroType4Headers[2]))
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

                var item = new ContractReportMicrosType4Item
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

                decimal? fruitAmounts = null;
                if (this.GetDecimal(row, 3, out fruitAmounts))
                {
                    item.FruitAmounts = fruitAmounts;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Плодове (кг.)",
                        "очаквана стойност: число"));
                }

                decimal? vegetableAmounts = null;
                if (this.GetDecimal(row, 4, out vegetableAmounts))
                {
                    item.VegetableAmounts = vegetableAmounts;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Зеленчуци (кг.)",
                        "очаквана стойност: число"));
                }

                decimal? group1TotalAmounts = null;
                if (this.GetDecimal(row, 5, out group1TotalAmounts))
                {
                    item.Group1TotalAmounts = group1TotalAmounts;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Общо количество плодове и зеленчуци",
                        "очаквана стойност: число"));
                }

                if (group1TotalAmounts != (fruitAmounts + vegetableAmounts))
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Общо количество плодове и зеленчуци",
                        "стойността не съвпада със сумата на колоните 'Плодове' и 'Зеленчуци'"));
                }

                decimal? meatAmounts = null;
                if (this.GetDecimal(row, 6, out meatAmounts))
                {
                    item.MeatAmounts = meatAmounts;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Месо (кг.)",
                        "очаквана стойност: число"));
                }

                decimal? eggAmounts = null;
                if (this.GetDecimal(row, 7, out eggAmounts))
                {
                    item.EggAmounts = eggAmounts;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Яйца (кг.)",
                        "очаквана стойност: число"));
                }

                decimal? fishAmounts = null;
                if (this.GetDecimal(row, 8, out fishAmounts))
                {
                    item.FishAmounts = fishAmounts;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Риба и рибни продукти (кг.)",
                        "очаквана стойност: число"));
                }

                decimal? group2TotalAmounts = null;
                if (this.GetDecimal(row, 9, out group2TotalAmounts))
                {
                    item.Group2TotalAmounts = group2TotalAmounts;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Общо количество месо, яйца, риба и рибни продукти",
                        "очаквана стойност: число"));
                }

                if (group2TotalAmounts != (meatAmounts + eggAmounts + fishAmounts))
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Общо количество месо, яйца, риба и рибни продукти",
                        "стойността не съвпада със сумата на колоните 'Месо', 'Яйца' и 'Риба и рибни продукти'"));
                }

                decimal? flourAmounts = null;
                if (this.GetDecimal(row, 10, out flourAmounts))
                {
                    item.FlourAmounts = flourAmounts;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Брашно (кг.)",
                        "очаквана стойност: число"));
                }

                decimal? breadAmounts = null;
                if (this.GetDecimal(row, 11, out breadAmounts))
                {
                    item.BreadAmounts = breadAmounts;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Хляб (кг.)",
                        "очаквана стойност: число"));
                }

                decimal? potatoAmounts = null;
                if (this.GetDecimal(row, 12, out potatoAmounts))
                {
                    item.PotatoAmounts = potatoAmounts;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Картофи (кг.)",
                        "очаквана стойност: число"));
                }

                decimal? riceAmounts = null;
                if (this.GetDecimal(row, 13, out riceAmounts))
                {
                    item.RiceAmounts = riceAmounts;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Ориз (кг.)",
                        "очаквана стойност: число"));
                }

                decimal? starchProductAmounts = null;
                if (this.GetDecimal(row, 14, out starchProductAmounts))
                {
                    item.StarchProductAmounts = starchProductAmounts;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Други съдържащи скорбяла продукти (кг.)",
                        "очаквана стойност: число"));
                }

                decimal? group3TotalAmounts = null;
                if (this.GetDecimal(row, 15, out group3TotalAmounts))
                {
                    item.Group3TotalAmounts = group3TotalAmounts;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Общо количество брашно, хляб, картофи, ориз и други съдържащи скорбяла продукти",
                        "очаквана стойност: число"));
                }

                if (group3TotalAmounts != (flourAmounts + breadAmounts + potatoAmounts + riceAmounts + starchProductAmounts))
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Общо количество брашно, хляб, картофи, ориз и други съдържащи скорбяла продукти",
                        "стойността не съвпада със сумата на колоните 'Брашно', 'Хляб', 'Картофи', 'Ориз' и 'Други съдържащи скорбяла продукти'"));
                }

                decimal? sugarAmounts = null;
                if (this.GetDecimal(row, 16, out sugarAmounts))
                {
                    item.SugarAmounts = sugarAmounts;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Захар (кг.)",
                        "очаквана стойност: число"));
                }

                decimal? milkProductAmounts = null;
                if (this.GetDecimal(row, 17, out milkProductAmounts))
                {
                    item.MilkProductAmounts = milkProductAmounts;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Млечни продукти (кг.)",
                        "очаквана стойност: число"));
                }

                decimal? fatsOrOilsAmounts = null;
                if (this.GetDecimal(row, 18, out fatsOrOilsAmounts))
                {
                    item.FatsOrOilsAmounts = fatsOrOilsAmounts;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Мазнина, масла (кг.)",
                        "очаквана стойност: число"));
                }

                decimal? fastFoodAmounts = null;
                if (this.GetDecimal(row, 19, out fastFoodAmounts))
                {
                    item.FastFoodAmounts = fastFoodAmounts;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Готови храни (кг.)",
                        "очаквана стойност: число"));
                }

                decimal? otherFoodAmounts = null;
                if (this.GetDecimal(row, 20, out otherFoodAmounts))
                {
                    item.OtherFoodAmounts = otherFoodAmounts;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Други храни (които не попадат в предходните категории) (кг.)",
                        "очаквана стойност: число"));
                }

                decimal? group4TotalAmounts = null;
                if (this.GetDecimal(row, 21, out group4TotalAmounts))
                {
                    item.Group4TotalAmounts = group4TotalAmounts;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Общо количество готови храни и други храни",
                        "очаквана стойност: число"));
                }

                if (group4TotalAmounts != (fastFoodAmounts + otherFoodAmounts))
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Общо количество готови храни и други храни",
                        "стойността не съвпада със сумата на колоните 'Готови храни' и 'Други храни (които не попадат в предходните категории)'"));
                }

                int? totalDishesCount = null;
                if (this.GetInteger(row, 22, out totalDishesCount))
                {
                    item.TotalDishesCount = totalDishesCount;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Брой ястия",
                        "очаквана стойност: цяло число"));
                }

                int? totalPackagesCount = null;
                if (this.GetInteger(row, 23, out totalPackagesCount))
                {
                    item.TotalPackagesCount = totalPackagesCount;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Брой пакети",
                        "очаквана стойност: цяло число"));
                }

                items.Add(item);
            }

            return items;
        }
    }
}
