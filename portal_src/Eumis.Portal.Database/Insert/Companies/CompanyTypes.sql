PRINT 'Insert CompanyTypes'
GO

SET IDENTITY_INSERT [CompanyTypes] ON

INSERT INTO [CompanyTypes]
    ([CompanyTypeId], [Gid]                                  , [Name]                    , [NameAlt]                    ,   [Alias]  , [Order])
VALUES                                                                                                                
    (1              , N'fb2c5186-b19b-4c76-9c5a-645c152558c9', N'Нестопанска организация', N'Non-profit organization'   ,   NULL      , 1      ),
    (2              , N'cc242a85-c307-490f-b908-5fb229694bbc', N'Учебно заведение'       , N'Educational establishment' ,   NULL      , 2      ),
    (3              , N'fb9e8997-5238-4346-babc-9140d6187f98', N'Държавна администрация' , N'State administration'      ,   NULL      , 3      ),
    (4              , N'a7f5e889-deca-4772-846b-10f357a6f953', N'Компания'               , N'Company'                   ,   N'company', 4      ),
    (5              , N'debf3139-9a31-4114-bff1-9e0b668ee396', N'Съдебна система'        , N'Judiciary'                 ,   NULL      , 5      ),
    (6              , N'4589c47c-dac6-41f3-a0ff-f616906bd5b5', N'Друга'                  , N'Other'                     ,   NULL      , 7      ),
    (7              , N'3ab609da-3df1-4a5b-acbc-f2252658ad87', N'Медицинско заведение'   , N'Medical establishment'     ,   NULL      , 6      )

SET IDENTITY_INSERT [CompanyTypes] OFF
GO
