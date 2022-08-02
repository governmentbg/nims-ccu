using System.Collections.Generic;
using System.IO;
using System.Linq;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.ContractReports.ViewObjects;
using Eumis.Domain.Contracts.ContractReportMicros;

namespace Eumis.ApplicationServices.Services.ContractReportMicro
{
    public class ContractReportMicroType3Parser : ContractReportMicroParserBase, IContractReportMicroType3Parser
    {
        private static readonly Dictionary<int, string> MicroType3Headers = new Dictionary<int, string>
        {
            { 1,  "ОБЛАСТ" },
            { 2,  "ОБЩИНА" },
            { 3,  "Лютеница" },
            { 5,  "Доматено пюре" },
            { 7,  "Зелен грах" },
            { 9,  "Гювеч" },
            { 11, "Нектар" },
            { 13, "Компот" },
            { 15, "Конфитюр" },
            { 17, "Месни консерви" },
            { 19, "Рибни консерви" },
            { 21, "Пшенично брашно" },
            { 23, "Ориз" },
            { 25, "Макаронени изделия" },
            { 27, "Булгур" },
            { 29, "Зрял фасул" },
            { 31, "Леща" },
            { 33, "Обикновени бисквити" },
            { 35, "Вафли" },
            { 37, "Бяла/кафява захар" },
            { 39, "Пчелен мед" },
            { 41, "Олио" },
            { 43, "Локум" },
        };

        public ContractReportMicroType3Parser(
            IContractReportMicrosDistrictNomsRepository contractReportMicrosDistrictNomsRepository,
            IContractReportMicrosMunicipalityNomsRepository contractReportMicrosMunicipalityNomsRepository,
            IContractReportMicrosSettlementNomsRepository contractReportMicrosSettlementNomsRepository)
            : base(
                  contractReportMicrosDistrictNomsRepository,
                  contractReportMicrosMunicipalityNomsRepository,
                  contractReportMicrosSettlementNomsRepository)
        {
        }

        public IList<ContractReportMicrosType3Item> ParseExcel(
            int contractReportMicroId,
            Stream excelStream,
            out IList<string> errors)
        {
            errors = new List<string>();

            var rows = this.ReadExcel(excelStream, 44);

            int rowNumber = 0;
            IList<ContractReportMicrosType3Item> items = new List<ContractReportMicrosType3Item>();
            foreach (var row in rows)
            {
                rowNumber++;

                if (rowNumber == 1 || rowNumber == 3)
                {
                    continue;
                }
                else if (rowNumber == 2)
                {
                    if (!this.AreHeadersValid(row, MicroType3Headers))
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

                var item = new ContractReportMicrosType3Item
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

                decimal? ketchupTargetValue = null;
                if (this.GetDecimal(row, 3, out ketchupTargetValue))
                {
                    item.Ketchup.TargetValue = ketchupTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Лютеница - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? ketchupActualValue = null;
                if (this.GetDecimal(row, 4, out ketchupActualValue))
                {
                    item.Ketchup.ActualValue = ketchupActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Лютеница - достигната стойност",
                        "очаквана стойност: число"));
                }

                decimal? tomatoPasteTargetValue = null;
                if (this.GetDecimal(row, 5, out tomatoPasteTargetValue))
                {
                    item.TomatoPaste.TargetValue = tomatoPasteTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Доматено пюре - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? tomatoPasteActualValue = null;
                if (this.GetDecimal(row, 6, out tomatoPasteActualValue))
                {
                    item.TomatoPaste.ActualValue = tomatoPasteActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Доматено пюре - достигната стойност",
                        "очаквана стойност: число"));
                }

                decimal? greenPeasTargetValue = null;
                if (this.GetDecimal(row, 7, out greenPeasTargetValue))
                {
                    item.GreenPeas.TargetValue = greenPeasTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Зелен грах - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? greenPeasActualValue = null;
                if (this.GetDecimal(row, 8, out greenPeasActualValue))
                {
                    item.GreenPeas.ActualValue = greenPeasActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Зелен грах - достигната стойност",
                        "очаквана стойност: число"));
                }

                decimal? hotchPotchTargetValue = null;
                if (this.GetDecimal(row, 9, out hotchPotchTargetValue))
                {
                    item.HotchPotch.TargetValue = hotchPotchTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Гювеч - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? hotchPotchActualValue = null;
                if (this.GetDecimal(row, 10, out hotchPotchActualValue))
                {
                    item.HotchPotch.ActualValue = hotchPotchActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Гювеч - достигната стойност",
                        "очаквана стойност: число"));
                }

                decimal? nectarTargetValue = null;
                if (this.GetDecimal(row, 11, out nectarTargetValue))
                {
                    item.Nectar.TargetValue = nectarTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Нектар - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? nectarActualValue = null;
                if (this.GetDecimal(row, 12, out nectarActualValue))
                {
                    item.Nectar.ActualValue = nectarActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Нектар - достигната стойност",
                        "очаквана стойност: число"));
                }

                decimal? compoteTargetValue = null;
                if (this.GetDecimal(row, 13, out compoteTargetValue))
                {
                    item.Compote.TargetValue = compoteTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Компот - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? compoteActualValue = null;
                if (this.GetDecimal(row, 14, out compoteActualValue))
                {
                    item.Compote.ActualValue = compoteActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Компот - достигната стойност",
                        "очаквана стойност: число"));
                }

                decimal? jamTargetValue = null;
                if (this.GetDecimal(row, 15, out jamTargetValue))
                {
                    item.Jam.TargetValue = jamTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Конфитюр - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? jamActualValue = null;
                if (this.GetDecimal(row, 16, out jamActualValue))
                {
                    item.Jam.ActualValue = jamActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Конфитюр - достигната стойност",
                        "очаквана стойност: число"));
                }

                decimal? meatCanTargetValue = null;
                if (this.GetDecimal(row, 17, out meatCanTargetValue))
                {
                    item.MeatCan.TargetValue = meatCanTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Месни консерви - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? meatCanActualValue = null;
                if (this.GetDecimal(row, 18, out meatCanActualValue))
                {
                    item.MeatCan.ActualValue = meatCanActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Месни консерви - достигната стойност",
                        "очаквана стойност: число"));
                }

                decimal? fishCanTargetValue = null;
                if (this.GetDecimal(row, 19, out fishCanTargetValue))
                {
                    item.FishCan.TargetValue = fishCanTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Рибни консерви - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? fishCanActualValue = null;
                if (this.GetDecimal(row, 20, out fishCanActualValue))
                {
                    item.FishCan.ActualValue = fishCanActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Рибни консерви - достигната стойност",
                        "очаквана стойност: число"));
                }

                decimal? wheatFlourTargetValue = null;
                if (this.GetDecimal(row, 21, out wheatFlourTargetValue))
                {
                    item.WheatFlour.TargetValue = wheatFlourTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Пшенично брашно  - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? wheatFlourActualValue = null;
                if (this.GetDecimal(row, 22, out wheatFlourActualValue))
                {
                    item.WheatFlour.ActualValue = wheatFlourActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Пшенично брашно  - достигната стойност",
                        "очаквана стойност: число"));
                }

                decimal? riceTargetValue = null;
                if (this.GetDecimal(row, 23, out riceTargetValue))
                {
                    item.Rice.TargetValue = riceTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Ориз - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? riceActualValue = null;
                if (this.GetDecimal(row, 24, out riceActualValue))
                {
                    item.Rice.ActualValue = riceActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Ориз - достигната стойност",
                        "очаквана стойност: число"));
                }

                decimal? macaroniTargetValue = null;
                if (this.GetDecimal(row, 25, out macaroniTargetValue))
                {
                    item.Macaroni.TargetValue = macaroniTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Макаронени изделия - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? macaroniActualValue = null;
                if (this.GetDecimal(row, 26, out macaroniActualValue))
                {
                    item.Macaroni.ActualValue = macaroniActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Макаронени изделия - достигната стойност",
                        "очаквана стойност: число"));
                }

                decimal? bulgurTargetValue = null;
                if (this.GetDecimal(row, 27, out bulgurTargetValue))
                {
                    item.Bulgur.TargetValue = bulgurTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Булгур - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? bulgurActualValue = null;
                if (this.GetDecimal(row, 28, out bulgurActualValue))
                {
                    item.Bulgur.ActualValue = bulgurActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Булгур - достигната стойност",
                        "очаквана стойност: число"));
                }

                decimal? beansTargetValue = null;
                if (this.GetDecimal(row, 29, out beansTargetValue))
                {
                    item.Beans.TargetValue = beansTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Зрял фасул - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? beansActualValue = null;
                if (this.GetDecimal(row, 30, out beansActualValue))
                {
                    item.Beans.ActualValue = beansActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Зрял фасул - достигната стойност",
                        "очаквана стойност: число"));
                }

                decimal? lentilsTargetValue = null;
                if (this.GetDecimal(row, 31, out lentilsTargetValue))
                {
                    item.Lentils.TargetValue = lentilsTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Леща - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? lentilsActualValue = null;
                if (this.GetDecimal(row, 32, out lentilsActualValue))
                {
                    item.Lentils.ActualValue = lentilsActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Леща - достигната стойност",
                        "очаквана стойност: число"));
                }

                decimal? biscuitTargetValue = null;
                if (this.GetDecimal(row, 33, out biscuitTargetValue))
                {
                    item.Biscuit.TargetValue = biscuitTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Обикновени бисквити - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? biscuitActualValue = null;
                if (this.GetDecimal(row, 34, out biscuitActualValue))
                {
                    item.Biscuit.ActualValue = biscuitActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Обикновени бисквити - достигната стойност",
                        "очаквана стойност: число"));
                }

                decimal? waffleTargetValue = null;
                if (this.GetDecimal(row, 35, out waffleTargetValue))
                {
                    item.Waffle.TargetValue = waffleTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Вафли - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? waffleActualValue = null;
                if (this.GetDecimal(row, 36, out waffleActualValue))
                {
                    item.Waffle.ActualValue = waffleActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Вафли - достигната стойност",
                        "очаквана стойност: число"));
                }

                decimal? sugarTargetValue = null;
                if (this.GetDecimal(row, 37, out sugarTargetValue))
                {
                    item.Sugar.TargetValue = sugarTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Бяла/кафява захар - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? sugarActualValue = null;
                if (this.GetDecimal(row, 38, out sugarActualValue))
                {
                    item.Sugar.ActualValue = sugarActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Бяла/кафява захар - достигната стойност",
                        "очаквана стойност: число"));
                }

                decimal? honeyTargetValue = null;
                if (this.GetDecimal(row, 39, out honeyTargetValue))
                {
                    item.Honey.TargetValue = honeyTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Пчелен мед - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? honeyActualValue = null;
                if (this.GetDecimal(row, 40, out honeyActualValue))
                {
                    item.Honey.ActualValue = honeyActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Пчелен мед - достигната стойност",
                        "очаквана стойност: число"));
                }

                decimal? oilTargetValue = null;
                if (this.GetDecimal(row, 41, out oilTargetValue))
                {
                    item.Oil.TargetValue = oilTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Олио - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? oilActualValue = null;
                if (this.GetDecimal(row, 42, out oilActualValue))
                {
                    item.Oil.ActualValue = oilActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Олио - достигната стойност",
                        "очаквана стойност: число"));
                }

                decimal? lokumTargetValue = null;
                if (this.GetDecimal(row, 43, out lokumTargetValue))
                {
                    item.Lokum.TargetValue = lokumTargetValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Локум - целева стойност",
                        "очаквана стойност: число"));
                }

                decimal? lokumActualValue = null;
                if (this.GetDecimal(row, 44, out lokumActualValue))
                {
                    item.Lokum.ActualValue = lokumActualValue;
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Локум - достигната стойност",
                        "очаквана стойност: число"));
                }

                items.Add(item);
            }

            return items;
        }
    }
}
