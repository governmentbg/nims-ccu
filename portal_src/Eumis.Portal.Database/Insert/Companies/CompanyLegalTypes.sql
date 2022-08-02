PRINT 'Insert CompanyLegalTypes'
GO

SET IDENTITY_INSERT [CompanyLegalTypes] ON

INSERT INTO [CompanyLegalTypes]
    ([CompanyLegalTypeId], [Gid]                                  , [CompanyTypeId], [Name]                                                                           , [NameAlt]                                                              , [Alias]  , [Order])
VALUES
    --3, Държавни институции
    (1                   , N'3cf3548c-b161-462b-ad17-df719cf04459', 3              , N'Общинска администрация'                                                        , N'Municipal administration'                                            , NULL     , 5      ),
    (2                   , N'b6d4988f-a5a5-43c0-abe3-931d247e9cd4', 3              , N'Областна администрация'                                                        , N'Regional administration'                                             , NULL     , 4      ),
    (3                   , N'85d79ac2-25d3-4e8a-9139-345a002726a9', 3              , N'АМС / министерство'                                                            , N'ACM/ ministry'                                                       , NULL     , 1      ),
    (4                   , N'9cb317f6-14dd-4fc6-b54a-31ebe2c0dcd4', 3              , N'Изпълнителна агенция / административните структури, създадени с нормативен акт', N'Executive agency/ administrative structure established by legal act' , NULL     , 3      ),
    (5                   , N'2ac30eee-8360-4d0b-a29f-f206308e70e8', 3              , N'Държавна агенция / държавна комисия'                                           , N'State agency/ state commission'                                      , NULL     , 2      ),
    (6                   , N'5c6c076a-31ec-4e92-b42d-ac21f38fde37', 3              , N'Други'                                                                         , N'Others'                                                              , NULL     , 7      ),
    (35                  , N'BEF68C51-EB45-4C09-ADE6-0A639D9082A1', 3              , N'Специализирана териториална администрация'                                     , N'Specialized territorial administration'                              , NULL     , 6      ),

    --4, Компании
    (7                   , N'5f6b7f94-aaa0-4ebf-98f9-3b70df370b61', 4              , N'Акционерно дружество АД'                                                       , N'Joint Stock company - JSC (AD)'                                      , NULL     , 1      ),
    (8                   , N'8ba40325-8ab8-4c5b-a297-c362df0e6ee7', 4              , N'Еднолично акционерно дружество ЕАД'                                            , N'Single-member joint stock company – SMJSC (EAD)'                     , NULL     , 3      ),
    (9                   , N'2da6a748-fb37-45aa-ba25-0313f3752ec6', 4              , N'Дружество с ограничена отговорност ООД'                                        , N'Limited liability company – LLC (OOD)'                               , NULL     , 2      ),
    (10                  , N'22209cc1-2199-422a-b43d-ed0ddf75b2de', 4              , N'Еднолично дружество с ограничена отговорност ЕООД'                             , N'Single-member limited liability company – SMLLC (EOOD)'              , NULL     , 4      ),
    (11                  , N'd1f0fae2-fd1a-409d-be0e-1faafdb028f0', 4              , N'Едноличен търговец ЕТ'                                                         , N'Sole Proprietor – SP (ET)'                                           , NULL     , 5      ),
    (12                  , N'73ad5a47-34f5-4f10-bbc3-ac2382ac3fde', 4              , N'Командитно дружество КД'                                                       , N'Limited partnership – LP (KD)'                                       , NULL     , 6      ),
    (13                  , N'c7bb002f-2a44-4d13-a579-95328b2e8069', 4              , N'Командитно дружество с акции КДА'                                              , N'Limited partnership with a share capital - LPSC (KDA)'               , NULL     , 7      ),
    (14                  , N'3e8f7baa-5fe6-49df-9141-fd3e1b10b2f4', 4              , N'Събирателно дружество СД'                                                      , N'General partnership – GP (SD)'                                       , NULL     , 8      ),
    (15                  , N'aa803c47-aad1-4d76-bdc8-601b99d1ea75', 4              , N'Други'                                                                         , N'Others'                                                              , NULL     , 9      ),

    --1, Нестопански организации
    (16                  , N'eec7d81c-021b-43f1-bb1d-2bb4d4ff2539', 1              , N'Сдружение в обществена полза'                                                  , N'Public benefit association'                                          , NULL     , 1      ),
    (17                  , N'1b4bc930-cc67-4d80-b853-4ee303005205', 1              , N'Сдружение в частна полза'                                                      , N'Private benefit association'                                         , NULL     , 2      ),
    (18                  , N'79a7c775-aeb7-4896-8ca2-e2b8481b2ca3', 1              , N'Фондация в обществена полза'                                                   , N'Public benefit foundation'                                           , NULL     , 3      ),
    (19                  , N'9e046052-c970-4637-8afa-2278b9423e73', 1              , N'Фондация в частна полза'                                                       , N'Private benefit foundation'                                          , NULL     , 4      ),
    (20                  , N'1b946e27-ae10-4d7f-b5f6-a2c3ee5965b3', 1              , N'Читалище'                                                                      , N'Community center'                                                    , NULL     , 5      ),

    --2, Учебни заведения
    (21                  , N'75d0e786-6aad-465c-9467-052156ec15a7', 2              , N'Висше училище/университет'                                                     , N'Higher education establishment/ University'                          , NULL     , 1      ),
    (22                  , N'bde4e8fa-ff5e-4413-9ff2-ffb267a5329f', 2              , N'Училище'                                                                       , N'School'                                                              , NULL     , 2      ),
    (23                  , N'c6d291c3-fbd4-4607-b3d3-2671f79168ed', 2              , N'Детска градина/Детска ясла'                                                    , N'Kindergarten/ Nursery'                                               , NULL     , 3      ),

    --5, Съдебна система
    (24                  , N'3243d87e-4c6d-424e-903b-a19c749cf785', 5              , N'Съд'                                                                           , N'Court'                                                               , NULL     , 3      ),
    (25                  , N'097d5468-1b63-48bd-968a-b453932d5d6a', 5              , N'Следствие'                                                                     , N'Investigation'                                                       , NULL     , 5      ),
    (26                  , N'7237e845-206d-41aa-b0e0-2ec281139a26', 5              , N'Прокуратура'                                                                   , N'Prosecution'                                                         , NULL     , 4      ),
    (37                  , N'6ACC2A03-F8F0-4926-8F59-893D3CA6E2B6', 5              , N'Висш съдебен съвет'                                                            , N'Supreme judicial court'                                              , NULL     , 1      ),
    (38                  , N'21C812BE-35AD-467E-B152-D56939E207A3', 5              , N'Инспекторат към ВСС'                                                           , N'SJC''s Inspectorate'                                                 , NULL     , 2      ),

    --6, Други
    (29                  , N'fe7b3663-56f8-4bc2-bef2-443fd4e7670e', 6              , N'Обединение на физически и/или юридически лица'                                 , N'Association of natural persons and/or legal entities'                , NULL     , 3      ),
    (31                  , N'3e8a3a2a-b211-4d82-89de-5c074e5deb94', 6              , N'Физическо лице'                                                                , N'Natural person'                                                      , N'person', 2      ),
    (36                  , N'1f77c5ea-969f-4115-921c-1482df5dffb3', 6              , N'Чуждестранен'                                                                  , N'Foreign'                                                             , NULL     , 4      ),
    (33                  , N'8de784b8-89eb-4d67-ae83-70d9731d21af', 6              , N'Друго юридическо лице'                                                         , N'Other legal entity'                                                  , NULL     , 1      ),

    --7, Медицински заведения
    (34                  , N'24cbf079-15ba-4502-bc35-0f8f90a41264', 7              , N'Болница/медицинско заведение'                                                  , N'Hospital/ Medical establishment'                                     , NULL     , 1      )

SET IDENTITY_INSERT [CompanyLegalTypes] OFF
GO
