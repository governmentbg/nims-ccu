using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using Eumis.Domain.Test.ExpressionEngine.Extensions;
using Eumis.Rio;

namespace Eumis.Domain.Test.ExpressionEngine.TestData
{
    internal class BigTestProcedure
    {
        private static Procedure procedure = null;

        private static Project project = null;

        private static Dictionary<int, Dictionary<string, ProgrammeDetailsExpenseBudget>> level3DataDictionary = null;

        private Dictionary<int, string> programmeCodes = new Dictionary<int, string>
        {
            { 1, "2014BG05SFOP001" },
            { 2, "2014BG16M1OP001" },
            { 3, "2014BG16RFOP001" },
            { 4, "2014BG05M9OP001" },
            { 5, "2014BG16RFOP002" },
            { 6, "2014BG16M1OP002" },
            { 7, "2014BG05M2OP001" },
        };

        private Dictionary<int, string> programmePriorityCodes = new Dictionary<int, string>
        {
            { 8, "2014BG05SFOP001-1" },
            { 9, "2014BG05SFOP001-2" },
            { 10, "2014BG05SFOP001-3" },
            { 11, "2014BG05SFOP001-4" },
            { 12, "2014BG05SFOP001-5" },
            { 13, "2014BG16M1OP001-1" },
            { 14, "2014BG16M1OP001-2" },
            { 15, "2014BG16M1OP001-3" },
            { 16, "2014BG16M1OP001-4" },
            { 17, "2014BG16M1OP001-5" },
            { 18, "2014BG16RFOP001-1" },
            { 19, "2014BG16RFOP001-2" },
            { 20, "2014BG16RFOP001-3" },
            { 21, "2014BG16RFOP001-4" },
            { 22, "2014BG16RFOP001-5" },
            { 23, "2014BG16RFOP001-6" },
            { 24, "2014BG16RFOP001-7" },
            { 25, "2014BG16RFOP001-8" },
            { 26, "2014BG05M9OP001-1" },
            { 27, "2014BG05M9OP001-2" },
            { 28, "2014BG05M9OP001-3" },
            { 29, "2014BG05M9OP001-4" },
            { 30, "2014BG05M9OP001-5" },
            { 31, "2014BG16RFOP002-1" },
            { 32, "2014BG16RFOP002-2" },
            { 33, "2014BG16RFOP002-3" },
            { 34, "2014BG16RFOP002-4" },
            { 35, "2014BG16M1OP002-1" },
            { 36, "2014BG16M1OP002-2" },
            { 37, "2014BG16M1OP002-3" },
            { 38, "2014BG16M1OP002-4" },
            { 39, "2014BG16M1OP002-5" },
            { 40, "2014BG16M1OP002-6" },
            { 41, "2014BG05M2OP001-1" },
            { 42, "2014BG05M2OP001-2" },
            { 43, "2014BG05M2OP001-3" },
            { 44, "2014BG05M2OP001-4" },
        };

        public Procedure Procedure
        {
            get
            {
                if (procedure == null)
                {
                    procedure = this.CreateProcedure();
                }

                return procedure;
            }
        }

        public Project Project
        {
            get
            {
                if (project == null)
                {
                    project = this.CreateProject(this.Procedure, this.Level3DataDictionary);
                }

                return project;
            }
        }

        public ProcedureProgramme GoodGovernanceProgramme
        {
            get
            {
                return this.Procedure.ProcedureProgrammes.Single(e => e.ProgrammeId == 1);
            }
        }

        public ProcedureProgramme TransportProgramme
        {
            get
            {
                return this.Procedure.ProcedureProgrammes.Single(e => e.ProgrammeId == 2);
            }
        }

        public string GoodGovernanceProgrammeCode
        {
            get
            {
                return this.programmeCodes.Single(e => e.Key == 1).Value;
            }
        }

        public string TransportProgrammeCode
        {
            get
            {
                return this.programmeCodes.Single(e => e.Key == 2).Value;
            }
        }

        private Dictionary<int, Dictionary<string, ProgrammeDetailsExpenseBudget>> Level3DataDictionary
        {
            get
            {
                if (level3DataDictionary == null)
                {
                    level3DataDictionary = this.CreateLevel3DataDictionary();
                }

                return level3DataDictionary;
            }
        }

        private Dictionary<int, Dictionary<string, ProgrammeDetailsExpenseBudget>> CreateLevel3DataDictionary()
        {
            Dictionary<int, Dictionary<string, ProgrammeDetailsExpenseBudget>> level3DataDictionary = new Dictionary<int, Dictionary<string, ProgrammeDetailsExpenseBudget>>();

            Dictionary<string, ProgrammeDetailsExpenseBudget> programme1DataDictionary = new Dictionary<string, ProgrammeDetailsExpenseBudget>
            {
                {
                    "1.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2000.00m,
                        SelfAmount = 1000.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "1.2.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2000.00m,
                        SelfAmount = 1000.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "1.3.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2000.00m,
                        SelfAmount = 1000.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "1.4.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2000.00m,
                        SelfAmount = 1000.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "2.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 1500.00m,
                        SelfAmount = 500.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "2.2.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 1500.00m,
                        SelfAmount = 500.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "2.3.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 1500.00m,
                        SelfAmount = 500.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "3.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 600.00m,
                        SelfAmount = 100.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "3.2.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 600.00m,
                        SelfAmount = 100.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "4.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2000.00m,
                        SelfAmount = 200.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "4.2.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2000.00m,
                        SelfAmount = 200.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "4.3.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2000.00m,
                        SelfAmount = 200.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "5.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 300.00m,
                        SelfAmount = 100.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "6.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 300.00m,
                        SelfAmount = 100.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "7.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 300.00m,
                        SelfAmount = 100.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "7.2.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 300.00m,
                        SelfAmount = 100.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "9.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2500.00m,
                        SelfAmount = 600.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "10.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2500.00m,
                        SelfAmount = 600.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "12.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2500.00m,
                        SelfAmount = 600.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "12.2.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2500.00m,
                        SelfAmount = 600.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "13.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2500.00m,
                        SelfAmount = 600.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "13.2.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2500.00m,
                        SelfAmount = 600.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "15.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 1000.00m,
                        SelfAmount = 100.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "15.2.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 1000.00m,
                        SelfAmount = 100.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "15.3.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 1000.00m,
                        SelfAmount = 100.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "15.4.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 1000.00m,
                        SelfAmount = 100.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "15.5.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 1000.00m,
                        SelfAmount = 100.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "16.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 3000.00m,
                        SelfAmount = 2000.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "16.2.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 3000.00m,
                        SelfAmount = 2000.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "17.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 3000.00m,
                        SelfAmount = 2000.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "17.2.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 3000.00m,
                        SelfAmount = 2000.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "18.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 3000.00m,
                        SelfAmount = 2000.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "18.2.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 3000.00m,
                        SelfAmount = 2000.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "19.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2400.00m,
                        SelfAmount = 2000.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "20.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2400.00m,
                        SelfAmount = 2000.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "21.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2400.00m,
                        SelfAmount = 2000.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "22.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2400.00m,
                        SelfAmount = 2000.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "22.2.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2400.00m,
                        SelfAmount = 2000.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
            };

            level3DataDictionary.Add(1, programme1DataDictionary);

            Dictionary<string, ProgrammeDetailsExpenseBudget> programme2DataDictionary = new Dictionary<string, ProgrammeDetailsExpenseBudget>
            {
                {
                    "1.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2000.00m,
                        SelfAmount = 400.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "2.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2000.00m,
                        SelfAmount = 400.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "3.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2000.00m,
                        SelfAmount = 400.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "4.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2000.00m,
                        SelfAmount = 400.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
                {
                    "5.1.",
                    new ProgrammeDetailsExpenseBudget
                    {
                        GrandAmount = 2000.00m,
                        SelfAmount = 400.00m,
                        Nuts = new Location { FullPath = "BG, BG4, BG41, 22, 2246, 68134" },
                    }
                },
            };

            level3DataDictionary.Add(2, programme2DataDictionary);

            foreach (var programmeData in level3DataDictionary)
            {
                foreach (var level3Data in programmeData.Value.Select(e => e.Value))
                {
                    level3Data.TotalAmount = level3Data.GrandAmount + level3Data.SelfAmount;
                }
            }

            return level3DataDictionary;
        }

        private Procedure CreateProcedure()
        {
            Procedure procedure = new Procedure(
                1,
                ProcedureStatus.Draft,
                ApplicationFormType.Standard,
                ProcedureKind.Budget,
                2021,
                "2014BG05SFOP001-1.2014.001",
                "Голяма тестова процедура",
                "Big Test Procedure",
                string.Empty,
                string.Empty,
                AllowedRegistrationType.DigitalOrPaper,
                1200000.00m,
                null,
                null,
                2000000.00m,
                null,
                null,
                12,
                1,
                8,
                2668190.00m,
                true,
                new DateTime(2015, 02, 10),
                null);

            procedure.SetIdsForConstructorCreatedObjects(2, 3, 2);

            // ProcedureShares
            procedure.AddProcedureShareAndSetId(1, 9, 315000.00m, false, 4);
            procedure.AddProcedureShareAndSetId(2, 13, 100000.00m, false, 5);
            procedure.AddProcedureShareAndSetId(2, 14, 200000.00m, false, 6);

            // ProcedureSpecFields
            procedure.AddProcedureSpecFieldAndSetId("Допълнително поле 1", "Additional field 1", "Някакво описание.", "Some description", true, ProcedureSpecFieldMaxLength.Small, 2);
            procedure.AddProcedureSpecFieldAndSetId("Допълнително поле 2", "Additional field 2", "Някакво описание.", "Some description", true, ProcedureSpecFieldMaxLength.Middle, 3);
            procedure.AddProcedureSpecFieldAndSetId("Допълнително поле 3", "Additional field 3", "Някакво описание.", "Some description", false, ProcedureSpecFieldMaxLength.Large, 4);
            procedure.AddProcedureSpecFieldAndSetId("Допълнително поле 4", "Additional field 4", "Някакво описание.", "Some description", false, ProcedureSpecFieldMaxLength.VeryLarge, 5);

            // ProcedureApplicationDocs
            procedure.AddProcedureApplicationDocAndSetId(null, "Документ за подаване 1", true, true, 2);
            procedure.AddProcedureApplicationDocAndSetId(null, "Документ за подаване 2", true, false, 3);
            procedure.AddProcedureApplicationDocAndSetId(null, "Документ за подаване 3", false, true, 4);
            procedure.AddProcedureApplicationDocAndSetId(null, "Документ за подаване 4", false, false, 5);

            // ProcedureBudgetLevel1s
            procedure.AddProcedureBudgetLevel1AndSetId(1, 1, 3);
            procedure.AddProcedureBudgetLevel1AndSetId(1, 2, 4);
            procedure.AddProcedureBudgetLevel1AndSetId(1, 3, 5);
            procedure.AddProcedureBudgetLevel1AndSetId(1, 4, 6);
            procedure.AddProcedureBudgetLevel1AndSetId(1, 5, 7);
            procedure.AddProcedureBudgetLevel1AndSetId(1, 6, 8);
            procedure.AddProcedureBudgetLevel1AndSetId(1, 7, 9);
            procedure.AddProcedureBudgetLevel1AndSetId(2, 8, 10);
            procedure.AddProcedureBudgetLevel1AndSetId(2, 9, 11);

            // ProcedureBudgetLevel2s
            procedure.AddProcedureBudgetLevel2AndSetId(3, 1, 9, "Възнаграждения (брутни суми, вкл.и дължимите от работодателя осигурителни вноски)", ProcedureBudgetLevel2AidMode.NotApplicable, 7);
            procedure.AddProcedureBudgetLevel2AndSetId(3, 1, 8, "Разходи за командировки", ProcedureBudgetLevel2AidMode.StateAid, 8);
            procedure.AddProcedureBudgetLevel2AndSetId(4, 1, 9, "Възнаграждения (брутни суми, вкл.и дължимите от работодателя осигурителни вноски)", ProcedureBudgetLevel2AidMode.Deminimis, 9);
            procedure.AddProcedureBudgetLevel2AndSetId(4, 1, 9, "Разходи за командировки", ProcedureBudgetLevel2AidMode.Deminimis, 10);
            procedure.AddProcedureBudgetLevel2AndSetId(5, 1, 8, "Закупуване на оборудване, съоръжения и обзавеждане", ProcedureBudgetLevel2AidMode.NotApplicable, 11);
            procedure.AddProcedureBudgetLevel2AndSetId(5, 1, 8, "Закупуване на машини", ProcedureBudgetLevel2AidMode.StateAid, 12);
            procedure.AddProcedureBudgetLevel2AndSetId(5, 1, 8, "Закупуване на земя", ProcedureBudgetLevel2AidMode.StateAid, 13);
            procedure.AddProcedureBudgetLevel2AndSetId(5, 1, 8, "Други (моля уточнете)", ProcedureBudgetLevel2AidMode.Deminimis, 14);
            procedure.AddProcedureBudgetLevel2AndSetId(6, 1, 8, "Закупуване на патенти, лицензи, права", ProcedureBudgetLevel2AidMode.NotApplicable, 15);
            procedure.AddProcedureBudgetLevel2AndSetId(6, 1, 8, "Закупуване на софтуер", ProcedureBudgetLevel2AidMode.StateAid, 16);
            procedure.AddProcedureBudgetLevel2AndSetId(6, 1, 8, "Други (моля уточнете) ", ProcedureBudgetLevel2AidMode.StateAid, 17);
            procedure.AddProcedureBudgetLevel2AndSetId(7, 1, 8, "Разходи за СМР сгради", ProcedureBudgetLevel2AidMode.Deminimis, 18);
            procedure.AddProcedureBudgetLevel2AndSetId(7, 1, 8, "СМР инфраструктура", ProcedureBudgetLevel2AidMode.Deminimis, 19);
            procedure.AddProcedureBudgetLevel2AndSetId(7, 1, 8, "Други (моля уточнете)", ProcedureBudgetLevel2AidMode.NotApplicable, 20);
            procedure.AddProcedureBudgetLevel2AndSetId(8, 1, 8, "Разходи за проектиране, проучвания, анализи, стратегии, програми и т.н.", ProcedureBudgetLevel2AidMode.Deminimis, 21);
            procedure.AddProcedureBudgetLevel2AndSetId(8, 1, 8, "Разходи за организиране на и/или участие в събития (международни изложения, панаири и т.н.)", ProcedureBudgetLevel2AidMode.NotApplicable, 22);
            procedure.AddProcedureBudgetLevel2AndSetId(8, 1, 8, "Разходи за обучения", ProcedureBudgetLevel2AidMode.NotApplicable, 23);
            procedure.AddProcedureBudgetLevel2AndSetId(8, 1, 8, "Разходи за маркетинг и реклама", ProcedureBudgetLevel2AidMode.StateAid, 24);
            procedure.AddProcedureBudgetLevel2AndSetId(8, 1, 8, "Разходи за одит на проекта", ProcedureBudgetLevel2AidMode.StateAid, 25);
            procedure.AddProcedureBudgetLevel2AndSetId(8, 1, 8, "Други услуги - моля опишете", ProcedureBudgetLevel2AidMode.StateAid, 26);
            procedure.AddProcedureBudgetLevel2AndSetId(9, 1, 8, "Разходи за материали за публичност и визуализация", ProcedureBudgetLevel2AidMode.StateAid, 27);
            procedure.AddProcedureBudgetLevel2AndSetId(9, 1, 8, "Разходи за дейности по информиране и публичност за проекта, съфинансиран по Оперативната програма ", ProcedureBudgetLevel2AidMode.NotApplicable, 28);
            procedure.AddProcedureBudgetLevel2AndSetId(10, 2, 13, "Разходи за закупуване на материали ", ProcedureBudgetLevel2AidMode.NotApplicable, 29);
            procedure.AddProcedureBudgetLevel2AndSetId(10, 2, 13, "Разходи за закупуване на консумативи", ProcedureBudgetLevel2AidMode.NotApplicable, 30);
            procedure.AddProcedureBudgetLevel2AndSetId(11, 2, 13, "Разходи по стандартна таблица на разходите за единица продукт", ProcedureBudgetLevel2AidMode.StateAid, 31);
            procedure.AddProcedureBudgetLevel2AndSetId(11, 2, 13, "Еднократни суми, които не надхвърлят левовата равностойност на ProcedureBudgetLevel2AidMode.Deminimis00 000 евро публичен принос", ProcedureBudgetLevel2AidMode.NotApplicable, 32);
            procedure.AddProcedureBudgetLevel2AndSetId(11, 2, 13, "Финансиране с единна ставка", ProcedureBudgetLevel2AidMode.NotApplicable, 33);
            procedure.AddProcedureBudgetLevel2AndSetId(11, 2, 13, "Разходи за амортизация ", ProcedureBudgetLevel2AidMode.NotApplicable, 34);

            // ProcedureBudgetLevel3s
            procedure.AddProcedureBudgetLevel3AndSetId(7, "Разходи за ръководител на проекта", 4);
            procedure.AddProcedureBudgetLevel3AndSetId(7, "Технически и финансов персонал", 5);
            procedure.AddProcedureBudgetLevel3AndSetId(7, "Административен/помощен персонал", 6);
            procedure.AddProcedureBudgetLevel3AndSetId(7, "Други (моля уточнете)", 7);
            procedure.AddProcedureBudgetLevel3AndSetId(8, "Дневни", 8);
            procedure.AddProcedureBudgetLevel3AndSetId(8, "Квартирни", 9);
            procedure.AddProcedureBudgetLevel3AndSetId(8, "Пътни", 10);
            procedure.AddProcedureBudgetLevel3AndSetId(9, "Възнаграждения и осигуровки от страна на работодателя за експерти, пряко свързани с основните дейности", 11);
            procedure.AddProcedureBudgetLevel3AndSetId(9, "Други (моля уточнете)", 12);
            procedure.AddProcedureBudgetLevel3AndSetId(10, "Дневни", 13);
            procedure.AddProcedureBudgetLevel3AndSetId(10, "Квартирни", 14);
            procedure.AddProcedureBudgetLevel3AndSetId(10, "Пътни", 15);
            procedure.AddProcedureBudgetLevel3AndSetId(11, "Вид/ тип оборудване", 16);
            procedure.AddProcedureBudgetLevel3AndSetId(12, "Вид / тип машини", 17);
            procedure.AddProcedureBudgetLevel3AndSetId(13, "Покупка на земя", 18);
            procedure.AddProcedureBudgetLevel3AndSetId(13, "Разходи, съпътстващи покупката на земя", 19);
            procedure.AddProcedureBudgetLevel3AndSetId(15, "Конкретен вид / тип", 20);
            procedure.AddProcedureBudgetLevel3AndSetId(16, "Конкретен тип / вид", 21);
            procedure.AddProcedureBudgetLevel3AndSetId(18, "Изграждане на ПСОВ", 22);
            procedure.AddProcedureBudgetLevel3AndSetId(18, "Реконструкция на училище", 23);
            procedure.AddProcedureBudgetLevel3AndSetId(19, "Изграждане на водопровод", 24);
            procedure.AddProcedureBudgetLevel3AndSetId(19, "Изграждане на канализация", 25);
            procedure.AddProcedureBudgetLevel3AndSetId(21, "Разходи за прединвестиционни прочувания", 26);
            procedure.AddProcedureBudgetLevel3AndSetId(21, "разходи за АРП и финансов анализ", 27);
            procedure.AddProcedureBudgetLevel3AndSetId(21, "разходи за експертни анализи", 28);
            procedure.AddProcedureBudgetLevel3AndSetId(21, "разходи за проектиране", 29);
            procedure.AddProcedureBudgetLevel3AndSetId(21, "Разходи за правни / нотариални услуги", 30);
            procedure.AddProcedureBudgetLevel3AndSetId(22, "разходи за участие в събития", 31);
            procedure.AddProcedureBudgetLevel3AndSetId(22, "Разходи за организиране на събития", 32);
            procedure.AddProcedureBudgetLevel3AndSetId(23, "Разходи за организиране на обучения", 33);
            procedure.AddProcedureBudgetLevel3AndSetId(23, "Разходи за материали за обучения", 34);
            procedure.AddProcedureBudgetLevel3AndSetId(24, "Разходи за реклама в медиите", 35);
            procedure.AddProcedureBudgetLevel3AndSetId(24, "други", 36);
            procedure.AddProcedureBudgetLevel3AndSetId(25, "Разходи за одит", 37);
            procedure.AddProcedureBudgetLevel3AndSetId(26, "други", 38);
            procedure.AddProcedureBudgetLevel3AndSetId(27, "Разходи за изработване и разпространение на информационни и рекламни материали", 39);
            procedure.AddProcedureBudgetLevel3AndSetId(28, "Разходи за събития", 40);
            procedure.AddProcedureBudgetLevel3AndSetId(28, "Разходи за конференции", 41);
            procedure.AddProcedureBudgetLevel3AndSetId(29, "Разходи за конкретен вид/тип материали", 42);
            procedure.AddProcedureBudgetLevel3AndSetId(30, "разходи за конкретен вид тип консумативи", 43);
            procedure.AddProcedureBudgetLevel3AndSetId(31, "Разход от стандартната таблица за разходи", 44);
            procedure.AddProcedureBudgetLevel3AndSetId(32, "Конкретен разход до 100 000 евро", 45);
            procedure.AddProcedureBudgetLevel3AndSetId(33, "Конкретен разход с единна ставка", 46);

            return procedure;
        }

        private Project CreateProject(Procedure procedure, Dictionary<int, Dictionary<string, ProgrammeDetailsExpenseBudget>> level3DataDictionary)
        {
            Project project = new Project
            {
                Candidate = new Company
                {
                    IsPrivateLegal = false,
                    CompanySizeType = new PrivateNomenclature
                    {
                        Name = "Малко",
                    },
                },
                DirectionsBudgetContractCollection = new List<DirectionsBudgetContract>(),
            };

            foreach (var procedureProgrammes in procedure.ProcedureProgrammes)
            {
                DirectionsBudgetContract budgetContract = new DirectionsBudgetContract
                {
                    programmeCode = this.programmeCodes.Single(e => e.Key == procedureProgrammes.ProgrammeId).Value,
                    Budget = new Budget
                    {
                        ProgrammeBudgetCollection = new List<ProgrammeBudget>(),
                    },
                };

                int lev2Index = 0;

                foreach (var level1Item in procedureProgrammes.ProcedureBudgetLevel1)
                {
                    var programmeBudget = new ProgrammeBudget
                    {
                        gid = level1Item.Gid.ToString(),

                        ProgrammeExpenseBudgetCollection = new List<ProgrammeExpenseBudget>(),
                    };

                    foreach (var level2Item in level1Item.ProcedureBudgetLevel2)
                    {
                        lev2Index++;

                        var programmeExpenseBudget = new ProgrammeExpenseBudget
                        {
                            gid = level2Item.Gid.ToString(),
                            Name = level2Item.Name,
                            ProgrammePriorityCode = this.programmePriorityCodes.Single(e => e.Key == level2Item.ProcedureShare.ProgrammePriorityId).Value,
                        };

                        var amMemInfo = typeof(ProcedureBudgetLevel2AidMode).GetMember(level2Item.AidMode.ToString());
                        var amDescAttribute = amMemInfo[0].GetCustomAttributes(false).Where(o => o.GetType().Name == "DescriptionAttribute").Single();
                        var amDescription = (string)amDescAttribute.GetType().GetMethod("GetDescription").Invoke(amDescAttribute, Array.Empty<object>());
                        programmeExpenseBudget.AidMode = new EnumNomenclature
                        {
                            Description = amDescription,
                            Value = level2Item.AidMode.ToString(),
                        };

                        programmeExpenseBudget.ProgrammeDetailsExpenseBudgetCollection = new List<ProgrammeDetailsExpenseBudget>();

                        int lev3Index = 0;

                        foreach (var level3Item in level2Item.ProcedureBudgetLevel3)
                        {
                            lev3Index++;

                            string lev3Identifier = string.Format("{0}.{1}.", lev2Index, lev3Index);

                            var programmeDetailsExpenseBudget =
                                level3DataDictionary.Single(e => e.Key == procedureProgrammes.ProgrammeId).Value.Single(e => e.Key == lev3Identifier).Value;

                            programmeDetailsExpenseBudget.gid = level3Item.Gid.ToString();
                            programmeDetailsExpenseBudget.Name = level3Item.Note;

                            programmeExpenseBudget.ProgrammeDetailsExpenseBudgetCollection.Add(programmeDetailsExpenseBudget);
                        }

                        programmeBudget.ProgrammeExpenseBudgetCollection.Add(programmeExpenseBudget);
                    }

                    budgetContract.Budget.ProgrammeBudgetCollection.Add(programmeBudget);
                }

                project.DirectionsBudgetContractCollection.Add(budgetContract);
            }

            return project;
        }
    }
}
