using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class FakeProcedureCommunicator : IProcedureCommunicator
    {
        IList<ContractProcedureProgrammesTree> IProcedureCommunicator.GetActiveProcedureProgrammesTree()
        {
            #region ProcedureProgrammesTree

            return new List<ContractProcedureProgrammesTree>
            {
                new ContractProcedureProgrammesTree
                {
                    code = "2014BG05SFOP001",
                    name = "Добро управление",
                    programmePriorities = new List<TreeProgrammePriority>
                    {
                        new TreeProgrammePriority
                        {
                            code = "2014BG05SFOP001-1",
                            name = "Административно обслужване и е-управление",
                            procedures = new List<TreeProcedure>
                            {
                                new TreeProcedure
                                {
                                    gid = new Guid("cae66cb6-4075-4acf-b0a4-643858379e79"),
                                    name = "Голяма тестова процедура"
                                }
                            }
                        },
                        new TreeProgrammePriority
                        {
                            code = "2014BG05SFOP001-2",
                            name =
                                "Ефективно и професионално управление в партньорство с гражданското общество и бизнеса",
                            procedures = new List<TreeProcedure>
                            {
                                new TreeProcedure
                                {
                                    gid = new Guid("cae66cb6-4075-4acf-b0a4-643858379e79"),
                                    name = "Голяма тестова процедура"
                                }
                            }
                        },
                        new TreeProgrammePriority
                        {
                            code = "2014BG05SFOP001-3",
                            name = "Прозрачна и ефективна съдебна система"
                        },
                        new TreeProgrammePriority
                        {
                            code = "2014BG05SFOP001-4",
                            name =
                                "Техническа помощ за структурите на администрацията, участващи в управлението и усвояването на ЕСИФ"
                        },
                        new TreeProgrammePriority
                        {
                            code = "2014BG05SFOP001-5",
                            name = "Техническа помощ"
                        }
                    }
                },

                new ContractProcedureProgrammesTree
                {
                    code ="2014BG16M1OP001",
                    name ="Транспорт и транспортна инфраструктура",
                    programmePriorities = new List<TreeProgrammePriority>
                    {
                        new TreeProgrammePriority
                        {
                            code = "2014BG05SFOP001-1",
                            name = "Административно обслужване и е-управление",
                            procedures = new List<TreeProcedure>
                            {
                                new TreeProcedure
                                {
                                    gid = new Guid("cae66cb6-4075-4acf-b0a4-643858379e79"),
                                    name = "Голяма тестова процедура",
                                    statusText = "Активна"
                                }
                            }
                        },
                        new TreeProgrammePriority
                        {
                            code = "2014BG05SFOP001-2",
                            name =
                                "Ефективно и професионално управление в партньорство с гражданското общество и бизнеса",
                            procedures = new List<TreeProcedure>
                            {
                                new TreeProcedure
                                {
                                    gid = new Guid("cae66cb6-4075-4acf-b0a4-643858379e79"),
                                    name = "Голяма тестова процедура",
                                    statusText = "Активна"
                                }
                            }
                        },
                        new TreeProgrammePriority
                        {
                            code = "2014BG05SFOP001-3",
                            name = "Прозрачна и ефективна съдебна система"
                        },
                        new TreeProgrammePriority
                        {
                            code = "2014BG05SFOP001-4",
                            name =
                                "Техническа помощ за структурите на администрацията, участващи в управлението и усвояването на ЕСИФ"
                        },
                        new TreeProgrammePriority
                        {
                            code = "2014BG05SFOP001-5",
                            name = "Техническа помощ"
                        }
                    }
                }
            };

            #endregion
        }

        IList<ContractProcedureProgrammesTree> IProcedureCommunicator.GetEndedProcedureProgrammesTree()
        {
            #region ProcedureProgrammesTree

            return new List<ContractProcedureProgrammesTree>
            {
            };

            #endregion
        }

        IList<ContractProcedureProgrammesTree> IProcedureCommunicator.GetPublicDiscussionProcedureProgrammesTree()
        {
            #region ProcedureProgrammesTree

            return new List<ContractProcedureProgrammesTree>
            {
            };

            #endregion
        }

        IList<ContractProcedureProgrammesTree> IProcedureCommunicator.GetArchivedPublicDiscussionProcedureProgrammesTree()
        {
            #region ProcedureProgrammesTree

            return new List<ContractProcedureProgrammesTree>
            {
            };

            #endregion
        }

        ContractProcedure IProcedureCommunicator.GetProcedureAppData(Guid procedureGid)
        {
            #region Programmes

            var programmes = new List<ContractProgramme>()
            {
                new ContractProgramme()
                {
                    programmeCode = "1",
                    programmeName = "Добро управление 2014-2020",
                    programmePriorities = new List<ContractProgrammePriority>()
                    {
                        new ContractProgrammePriority()
                        {
                            programmePriorityCode = "1",
                            programmePriorityName = "Административно обслужване и е-управление"
                        }
                    },
                    budgetExpenseTypes = new List<ContractBudgetExpenseType>()
                    {
                        new ContractBudgetExpenseType()
                        {
                            gid = Guid.NewGuid().ToString(),
                            name = "Разходи за организация и управление",
                            orderNum = 1,
                            expenses = new List<ContractExpense>()
                            {
                                new ContractExpense()
                                {
                                    gid = Guid.NewGuid().ToString(),
                                    name = "Възнаграждения (брутни суми, вкл.и дължимите от работодателя осигурителни вноски)",
                                    orderNum = 1,
                                    aidMode = new ContractEnumNomenclature(){value = "1", description = "aidMode1"},
                                    programmePriorityCode = "2014BG05M9OP001-1",
                                    financeSource = new ContractEnumNomenclature(){value = "1", description= "europeanSocialFund"},
                                    details = new List<ContractExpenseDetails>()
                                    {
                                        new ContractExpenseDetails()
                                        {
                                            gid = Guid.NewGuid().ToString(),
                                            note = "Разходи за ръководител на проекта",
                                            orderNum = 1
                                        },
                                        new ContractExpenseDetails()
                                        {
                                            gid = Guid.NewGuid().ToString(),
                                            note = "Технически и финансов персонал",
                                            orderNum = 2
                                        },
                                        new ContractExpenseDetails()
                                        {
                                            gid = Guid.NewGuid().ToString(),
                                            note = "Административен/помощен персонал",
                                            orderNum = 3
                                        }
                                    }
                                },
                                new ContractExpense()
                                {
                                    gid = Guid.NewGuid().ToString(),
                                    name = "Разходи за командировки",
                                    orderNum = 2,
                                    aidMode = new ContractEnumNomenclature(){value = "2", description = "aidMode2"},
                                    programmePriorityCode = "2014BG05M9OP001-1",
                                    financeSource = new ContractEnumNomenclature(){value = "1", description= "europeanSocialFund"},
                                    details = new List<ContractExpenseDetails>()
                                    {
                                        new ContractExpenseDetails()
                                        {
                                            gid = Guid.NewGuid().ToString(),
                                            note = "Дневни",
                                            orderNum = 1
                                        },
                                        new ContractExpenseDetails()
                                        {
                                            gid = Guid.NewGuid().ToString(),
                                            note = "Квартирни",
                                            orderNum = 2
                                        },
                                        new ContractExpenseDetails()
                                        {
                                            gid = Guid.NewGuid().ToString(),
                                            note = "Пътни",
                                            orderNum = 3
                                        }
                                    }
                                }
                            }
                        },
                        new ContractBudgetExpenseType()
                        {
                            gid = Guid.NewGuid().ToString(),
                            name = "Разходи за възнаграждения",
                            orderNum = 2,
                            expenses = new List<ContractExpense>()
                            {
                                new ContractExpense()
                                {
                                    gid = Guid.NewGuid().ToString(),
                                    name = "Възнаграждения (брутни суми, вкл.и дължимите от работодателя осигурителни вноски)",
                                    orderNum = 3,
                                    aidMode = new ContractEnumNomenclature(){value = "3", description = "aidMode3"},
                                    programmePriorityCode = "2014BG05M9OP001-1",
                                    financeSource = new ContractEnumNomenclature(){value = "1", description= "europeanSocialFund"},
                                    details = new List<ContractExpenseDetails>()
                                    {
                                        new ContractExpenseDetails()
                                        {
                                            gid = Guid.NewGuid().ToString(),
                                            note = "Възнаграждения и осигуровки от страна на работодателя за експерти, пряко свързани с основните дейности",
                                            orderNum = 1
                                        }
                                    }
                                },
                                new ContractExpense()
                                {
                                    gid = Guid.NewGuid().ToString(),
                                    name = "Разходи за командировки",
                                    orderNum = 4,
                                    aidMode = new ContractEnumNomenclature(){value = "4", description = "aidMode4"},
                                    programmePriorityCode = "2014BG05M9OP001-1",
                                    financeSource = new ContractEnumNomenclature(){value = "1", description= "europeanSocialFund"},
                                    details = new List<ContractExpenseDetails>()
                                    {
                                        new ContractExpenseDetails()
                                        {
                                            gid = Guid.NewGuid().ToString(),
                                            note = "Дневни",
                                            orderNum = 1
                                        },
                                        new ContractExpenseDetails()
                                        {
                                            gid = Guid.NewGuid().ToString(),
                                            note = "Квартирни",
                                            orderNum = 2
                                        },
                                        new ContractExpenseDetails()
                                        {
                                            gid = Guid.NewGuid().ToString(),
                                            note = "Пътни",
                                            orderNum = 3
                                        }
                                    }
                                }
                            }
                        }
                    },
                    indicators = new List<ContractIndicator>()
                    {
                        new ContractIndicator()
                        {
                            aggregatedReport = "report1",
                            aggregatedTarget = "target1",
                            gid = "gid1",
                            hasGenderDivision = false,
                            isActive = true,
                            measureName = "measure1",
                            kindName = "kind1",
                            name = "name1",
                            typeName = "type1",
                            trendName = "trend1"
                        },
                        new ContractIndicator()
                        {
                            aggregatedReport = "report2",
                            aggregatedTarget = "target2",
                            gid = "gid2",
                            hasGenderDivision = true,
                            isActive = true,
                            measureName = "measure2",
                            kindName = "kind2",
                            name = "name2",
                            typeName = "type2",
                            trendName = "trend2"
                        },
                        new ContractIndicator()
                        {
                            aggregatedReport = "report3",
                            aggregatedTarget = "target3",
                            gid = "gid3",
                            hasGenderDivision = false,
                            isActive = true,
                            measureName = "measure3",
                            kindName = "kind3",
                            name = "name3",
                            typeName = "type3",
                            trendName = "trend3"
                        }

                    }
                },
                new ContractProgramme()
                {
                    programmeCode = "2",
                    programmeName = "Транспорт и транспортна инфраструктура 2014-2020",
                    programmePriorities = new List<ContractProgrammePriority>()
                    {
                        new ContractProgrammePriority()
                        {
                            programmePriorityCode = "2",
                            programmePriorityName = "Развитие на пътната инфраструктура по „основната” Трансевропейска транспортна мрежа"
                        }
                    },
                    budgetExpenseTypes = new List<ContractBudgetExpenseType>()
                    {
                        new ContractBudgetExpenseType()
                        {
                            gid = Guid.NewGuid().ToString(),
                            name = "Разходи за организация и управление",
                            orderNum = 1,
                            expenses = new List<ContractExpense>()
                            {
                                new ContractExpense()
                                {
                                    gid = Guid.NewGuid().ToString(),
                                    name = "Възнаграждения (брутни суми, вкл.и дължимите от работодателя осигурителни вноски)",
                                    orderNum = 1,
                                    aidMode = new ContractEnumNomenclature(){value = "1", description = "aidMode1"},
                                    programmePriorityCode = "2014BG05M9OP001-1",
                                    financeSource = new ContractEnumNomenclature(){value = "1", description= "europeanSocialFund"},
                                    details = new List<ContractExpenseDetails>()
                                    {
                                        new ContractExpenseDetails()
                                        {
                                            gid = Guid.NewGuid().ToString(),
                                            note = "Разходи за ръководител на проекта",
                                            orderNum = 1
                                        },
                                        new ContractExpenseDetails()
                                        {
                                            gid = Guid.NewGuid().ToString(),
                                            note = "Технически и финансов персонал",
                                            orderNum = 2
                                        },
                                        new ContractExpenseDetails()
                                        {
                                            gid = Guid.NewGuid().ToString(),
                                            note = "Административен/помощен персонал",
                                            orderNum = 3
                                        }
                                    }
                                },
                                new ContractExpense()
                                {
                                    gid = Guid.NewGuid().ToString(),
                                    name = "Разходи за командировки",
                                    orderNum = 2,
                                    aidMode = new ContractEnumNomenclature(){value = "2", description = "aidMode2"},
                                    programmePriorityCode = "2014BG05M9OP001-1",
                                    financeSource = new ContractEnumNomenclature(){value = "1", description= "europeanSocialFund"},
                                    details = new List<ContractExpenseDetails>()
                                    {
                                        new ContractExpenseDetails()
                                        {
                                            gid = Guid.NewGuid().ToString(),
                                            note = "Дневни",
                                            orderNum = 1
                                        },
                                        new ContractExpenseDetails()
                                        {
                                            gid = Guid.NewGuid().ToString(),
                                            note = "Квартирни",
                                            orderNum = 2
                                        },
                                        new ContractExpenseDetails()
                                        {
                                            gid = Guid.NewGuid().ToString(),
                                            note = "Пътни",
                                            orderNum = 3
                                        }
                                    }
                                }
                            }
                        },
                        new ContractBudgetExpenseType()
                        {
                            gid = Guid.NewGuid().ToString(),
                            name = "Разходи за възнаграждения",
                            orderNum = 2,
                            expenses = new List<ContractExpense>()
                            {
                                new ContractExpense()
                                {
                                    gid = Guid.NewGuid().ToString(),
                                    name = "Възнаграждения (брутни суми, вкл.и дължимите от работодателя осигурителни вноски)",
                                    orderNum = 3,
                                    aidMode = new ContractEnumNomenclature(){value = "3", description = "aidMode3"},
                                    programmePriorityCode = "2014BG05M9OP001-1",
                                    financeSource = new ContractEnumNomenclature(){value = "1", description= "europeanSocialFund"},
                                    details = new List<ContractExpenseDetails>()
                                    {
                                        new ContractExpenseDetails()
                                        {
                                            gid = Guid.NewGuid().ToString(),
                                            note = "Възнаграждения и осигуровки от страна на работодателя за експерти, пряко свързани с основните дейности",
                                            orderNum = 1
                                        }
                                    }
                                },
                                new ContractExpense()
                                {
                                    gid = Guid.NewGuid().ToString(),
                                    name = "Разходи за командировки",
                                    orderNum = 4,
                                    aidMode = new ContractEnumNomenclature(){value = "4", description = "aidMode4"},
                                    programmePriorityCode = "2014BG05M9OP001-1",
                                    financeSource = new ContractEnumNomenclature(){value = "1", description= "europeanSocialFund"},
                                    details = new List<ContractExpenseDetails>()
                                    {
                                        new ContractExpenseDetails()
                                        {
                                            gid = Guid.NewGuid().ToString(),
                                            note = "Дневни",
                                            orderNum = 1
                                        },
                                        new ContractExpenseDetails()
                                        {
                                            gid = Guid.NewGuid().ToString(),
                                            note = "Квартирни",
                                            orderNum = 2
                                        },
                                        new ContractExpenseDetails()
                                        {
                                            gid = Guid.NewGuid().ToString(),
                                            note = "Пътни",
                                            orderNum = 3
                                        }
                                    }
                                }
                            }
                        }
                    },
                    indicators = new List<ContractIndicator>()
                    {
                        new ContractIndicator()
                        {
                            gid = "9f7e97e3-bc2e-4a9d-a8df-9a09d20a1272",
                            name = "Безработни лица на възраст 15-29 години включително, с постоянен адрес в населено място извън област София - град",
                            kindName = "Основен",
                            trendName = "Увеличение",
                            typeName = "Изпълнение",
                            measureName = "Брой"
                        },
                        new ContractIndicator()
                        {
                            gid = "26eded23-08e2-48e8-8486-6a3e0ed7df53",
                            name = "Безработни участници, които завършват подкрепена от ИМЗ интервенция",
                            kindName = "Основен",
                            trendName = "Увеличение",
                            typeName = "Резултат",
                            measureName = "Брой"
                        }
                    }
                }
            };

            #endregion

            #region Procedure

            return new ContractProcedure()
            {
                procedureName = "Интегриране на уязвимите групи на пазара на труда",
                procedureCode = "BG051PO001-1.001",

                programmes = programmes,

                specFields = new List<ContractSpecField>()
                {
                    new ContractSpecField()
                    {
                        gid = "1",
                        title = "Поле 1",
                        description = "Field 1"
                    },
                    new ContractSpecField()
                    {
                        gid = "2",
                        title = "Поле 2",
                        description = "Field 2"
                    }
                },

                applicationDocs = new List<ContractApplicationDoc>()
                {
                    new ContractApplicationDoc()
                    {
                        gid="1",
                        isOriginal = true,
                        isRequired = true,
                        isSignatureRequired = true,
                        name = "Документ 1"
                    },
                    new ContractApplicationDoc()
                    {
                        gid="2",
                        isOriginal = true,
                        isRequired = false,
                        isSignatureRequired = false,
                        name = "Документ 2"
                    },
                    new ContractApplicationDoc()
                    {
                        gid="3",
                        isOriginal = false,
                        isRequired = true,
                        isSignatureRequired = true,
                        name = "Документ 3"
                    },
                    new ContractApplicationDoc()
                    {
                        gid="4",
                        isOriginal = false,
                        isRequired = false,
                        isSignatureRequired = false,
                        name = "Документ 4"
                    },
                    new ContractApplicationDoc()
                    {
                        gid="5",
                        isOriginal = false,
                        isRequired = true,
                        isSignatureRequired = true,
                        name = "Документ 5"
                    }
                }
            };

            #endregion
        }

        ContractProcedureInfo IProcedureCommunicator.GetProcedureInfo(Guid procedureGid)
        {
            #region ProcedureInfo

            return new ContractProcedureInfo()
            {
                description = @"\r\n<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam efficitur euismod risus et placerat. Nullam posuere suscipit odio vel sollicitudin. Vestibulum vitae ipsum vitae libero euismod euismod. Proin tortor nulla, ullamcorper id mollis posuere, tempus sit amet massa. Aliquam erat volutpat. Vestibulum sed suscipit nisi, quis dignissim est. Ut blandit, ante eget feugiat accumsan, nisi lacus tempor tellus, ut lacinia ante neque ac nunc. Aenean aliquam tellus augue, vel varius neque pretium id. Nulla consectetur porttitor dictum. Aliquam tincidunt risus ac urna pellentesque mattis. Pellentesque volutpat fringilla nulla nec luctus. Phasellus id porta tortor, sed egestas tellus. Nulla luctus malesuada dapibus.</p>\r\n<p>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Donec mollis porttitor ipsum, non ornare erat vehicula vitae. Etiam vestibulum volutpat fermentum. Nullam congue sed leo ac aliquet. Curabitur eleifend nisl metus, in pharetra dolor dignissim rhoncus. Sed pretium accumsan nibh ac tempor. Maecenas maximus nibh vel porta elementum. Aenean auctor diam sed commodo elementum. Ut fermentum, orci ut cursus dignissim, ante justo eleifend ipsum, a bibendum massa lorem at velit. In id lobortis eros. Quisque vel lorem urna. Fusce sagittis mollis felis, a ultricies tortor sollicitudin a. Suspendisse pulvinar tortor ac arcu viverra, quis cursus eros porttitor. Sed dolor tellus, gravida ac consectetur vel, pellentesque sed augue. Donec ultrices sapien ac porta dapibus. Nullam tincidunt erat quis imperdiet suscipit.</p>\r\n<p>Suspendisse sit amet lectus sed urna tincidunt pulvinar. Vivamus lectus lorem, ornare ac iaculis at, dignissim sed ligula. Ut accumsan dui non porta sodales. Nunc luctus posuere justo, id dignissim quam porta nec. Phasellus sed arcu sit amet dolor tempor vulputate et id augue. In sed neque a orci vestibulum congue. Sed semper mi quis mollis semper. Sed sed sem orci. Aenean consectetur tellus in mi convallis, sit amet facilisis diam tristique. Fusce arcu tellus, malesuada quis quam vitae, lobortis consequat libero. Praesent et tristique sem. Cras vestibulum ipsum enim, a dignissim eros fermentum non.</p>\r\n<p>Nunc sagittis leo vel feugiat luctus. Quisque lectus nibh, feugiat et enim ac, mollis imperdiet purus. Pellentesque pretium, leo sit amet dignissim venenatis, nibh dui sagittis ligula, nec congue arcu urna sed massa. Nulla eget risus turpis. Duis a viverra purus. Integer eu hendrerit turpis, vitae ornare ante. Duis nec nisi id magna interdum tempor in vel ex.</p>\r\n<p>Pellentesque semper, nisl ac fringilla facilisis, augue nibh pulvinar libero, ac pellentesque tortor justo vehicula enim. Morbi ac volutpat risus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor ligula sit amet orci congue mattis. Nullam sed ligula quis velit interdum dignissim ac eu odio. Praesent tristique condimentum libero ultrices tincidunt. Nunc scelerisque nibh ac egestas sollicitudin. Fusce in nibh id magna viverra elementum at porta arcu. Integer mattis venenatis ultricies. Suspendisse justo turpis, venenatis non velit eu, fringilla scelerisque sapien. Quisque vitae velit maximus, sollicitudin erat scelerisque, pretium sapien. Aliquam vestibulum et arcu eu elementum. Duis sit amet nunc vitae enim varius vestibulum. In placerat nibh quis condimentum posuere. In sed facilisis orci. Vivamus porta ex aliquet, convallis elit sit amet, rutrum libero.</p>\r\n",
                applicationGuidelines = new List<ApplicationGuideline>()
                {
                    new ApplicationGuideline
                    {
                        gid = new Guid("12fbbc05-fff7-4107-a5e8-e6e44d405d12"),
                        name = "Покана",
                        description = "",
                        filename = "blank.pdf",
                        blobKey = new Guid("cba9ac93-8d88-4d5b-b92e-0aefe1446ea6")
                    },
                    new ApplicationGuideline
                    {
                         gid = new Guid("530ab535-6927-483f-8ffc-78c9e08d346c"),
                         name = "Образец на Договор",
                         description = "",
                         filename = "blank.docx",
                         blobKey = new Guid("2baa4378-ce0a-48c2-9c8b-baf57ef879b5")
                    },   
                    new ApplicationGuideline{    
                         gid = new Guid("44c3113c-3bf9-49ee-897e-3fd2146bb8bb"),
                         name = "Насоки за кандидатстване",
                         description = "",
                         filename = "blank.zip",
                         blobKey = new Guid("193daaa2-abad-40d9-bd39-4b16537964dd")
                    }
                }
            };

            #endregion
        }

        ContractProcedureInfo IProcedureCommunicator.GetProcedurePublicDiscussionInfo(Guid procedureGid)
        {
            #region ProcedureInfo

            return new ContractProcedureInfo()
            {
                description = @"\r\n<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam efficitur euismod risus et placerat. Nullam posuere suscipit odio vel sollicitudin. Vestibulum vitae ipsum vitae libero euismod euismod. Proin tortor nulla, ullamcorper id mollis posuere, tempus sit amet massa. Aliquam erat volutpat. Vestibulum sed suscipit nisi, quis dignissim est. Ut blandit, ante eget feugiat accumsan, nisi lacus tempor tellus, ut lacinia ante neque ac nunc. Aenean aliquam tellus augue, vel varius neque pretium id. Nulla consectetur porttitor dictum. Aliquam tincidunt risus ac urna pellentesque mattis. Pellentesque volutpat fringilla nulla nec luctus. Phasellus id porta tortor, sed egestas tellus. Nulla luctus malesuada dapibus.</p>\r\n<p>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Donec mollis porttitor ipsum, non ornare erat vehicula vitae. Etiam vestibulum volutpat fermentum. Nullam congue sed leo ac aliquet. Curabitur eleifend nisl metus, in pharetra dolor dignissim rhoncus. Sed pretium accumsan nibh ac tempor. Maecenas maximus nibh vel porta elementum. Aenean auctor diam sed commodo elementum. Ut fermentum, orci ut cursus dignissim, ante justo eleifend ipsum, a bibendum massa lorem at velit. In id lobortis eros. Quisque vel lorem urna. Fusce sagittis mollis felis, a ultricies tortor sollicitudin a. Suspendisse pulvinar tortor ac arcu viverra, quis cursus eros porttitor. Sed dolor tellus, gravida ac consectetur vel, pellentesque sed augue. Donec ultrices sapien ac porta dapibus. Nullam tincidunt erat quis imperdiet suscipit.</p>\r\n<p>Suspendisse sit amet lectus sed urna tincidunt pulvinar. Vivamus lectus lorem, ornare ac iaculis at, dignissim sed ligula. Ut accumsan dui non porta sodales. Nunc luctus posuere justo, id dignissim quam porta nec. Phasellus sed arcu sit amet dolor tempor vulputate et id augue. In sed neque a orci vestibulum congue. Sed semper mi quis mollis semper. Sed sed sem orci. Aenean consectetur tellus in mi convallis, sit amet facilisis diam tristique. Fusce arcu tellus, malesuada quis quam vitae, lobortis consequat libero. Praesent et tristique sem. Cras vestibulum ipsum enim, a dignissim eros fermentum non.</p>\r\n<p>Nunc sagittis leo vel feugiat luctus. Quisque lectus nibh, feugiat et enim ac, mollis imperdiet purus. Pellentesque pretium, leo sit amet dignissim venenatis, nibh dui sagittis ligula, nec congue arcu urna sed massa. Nulla eget risus turpis. Duis a viverra purus. Integer eu hendrerit turpis, vitae ornare ante. Duis nec nisi id magna interdum tempor in vel ex.</p>\r\n<p>Pellentesque semper, nisl ac fringilla facilisis, augue nibh pulvinar libero, ac pellentesque tortor justo vehicula enim. Morbi ac volutpat risus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor ligula sit amet orci congue mattis. Nullam sed ligula quis velit interdum dignissim ac eu odio. Praesent tristique condimentum libero ultrices tincidunt. Nunc scelerisque nibh ac egestas sollicitudin. Fusce in nibh id magna viverra elementum at porta arcu. Integer mattis venenatis ultricies. Suspendisse justo turpis, venenatis non velit eu, fringilla scelerisque sapien. Quisque vitae velit maximus, sollicitudin erat scelerisque, pretium sapien. Aliquam vestibulum et arcu eu elementum. Duis sit amet nunc vitae enim varius vestibulum. In placerat nibh quis condimentum posuere. In sed facilisis orci. Vivamus porta ex aliquet, convallis elit sit amet, rutrum libero.</p>\r\n",
                applicationGuidelines = new List<ApplicationGuideline>()
                {
                    new ApplicationGuideline
                    {
                        gid = new Guid("12fbbc05-fff7-4107-a5e8-e6e44d405d12"),
                        name = "Покана",
                        description = "",
                        filename = "blank.pdf",
                        blobKey = new Guid("cba9ac93-8d88-4d5b-b92e-0aefe1446ea6")
                    },
                    new ApplicationGuideline
                    {
                         gid = new Guid("530ab535-6927-483f-8ffc-78c9e08d346c"),
                         name = "Образец на Договор",
                         description = "",
                         filename = "blank.docx",
                         blobKey = new Guid("2baa4378-ce0a-48c2-9c8b-baf57ef879b5")
                    },
                    new ApplicationGuideline{
                         gid = new Guid("44c3113c-3bf9-49ee-897e-3fd2146bb8bb"),
                         name = "Насоки за кандидатстване",
                         description = "",
                         filename = "blank.zip",
                         blobKey = new Guid("193daaa2-abad-40d9-bd39-4b16537964dd")
                    }
                }
            };

            #endregion
        }

        ContractProcedure IProcedureCommunicator.GetProcedureActualAppData(Guid procedureGid)
        {
            throw new NotImplementedException();
        }

        public List<ContractProcedureDiscussionsInfo> GetProcedureDiscussionsInfo(Guid id, string searchTerm, int limit, int offset)
        {
            throw new NotImplementedException();
        }

        public void SubmitProcedurePublicDiscussionComment(Guid procedureGid, string accessToken, string senderEmail, string senderName, string commentMessage)
        {

        }

        public void SubmitProcedureDiscussionQuestion(Guid procedureGid, string accessToken, string questionMessage)
        {
            throw new NotImplementedException();
        }
    }
}
