PRINT 'FlatFinancialCorrections'
GO

CREATE TABLE [dbo].[FlatFinancialCorrections] (
    [FlatFinancialCorrectionId]             INT                    NOT NULL IDENTITY,
    [ProgrammeId]                           INT                    NOT NULL,
    [Name]                                  NVARCHAR(MAX)          NULL,
    [OrderNum]                              INT                    NOT NULL,
    [Level]                                 INT                    NOT NULL,
    [Type]                                  INT                    NOT NULL,
    [Status]                                INT                    NOT NULL,
    [ImpositionDate]                        DATETIME2              NULL,
    [ImpositionNumber]                      NVARCHAR(200)          NULL,
    [Description]                           NVARCHAR(MAX)          NULL,
    [Base]                                  NVARCHAR(MAX)          NULL,
    [BlobKey]                               UNIQUEIDENTIFIER       NULL,
    [ContractId]                            INT                    NULL,

    [CreateDate]                            DATETIME2              NOT NULL,
    [ModifyDate]                            DATETIME2              NOT NULL,
    [Version]                               ROWVERSION             NOT NULL,


    CONSTRAINT [PK_FlatFinancialCorrections]                PRIMARY KEY ([FlatFinancialCorrectionId]),
    CONSTRAINT [FK_FlatFinancialCorrections_Programmes]     FOREIGN KEY ([ProgrammeId])        REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_FlatFinancialCorrections_Blobs]          FOREIGN KEY ([BlobKey])            REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_FlatFinancialCorrections_Level]         CHECK       ([Level]   IN (1, 2, 3, 4, 5)),
    CONSTRAINT [CHK_FlatFinancialCorrections_Type]          CHECK       ([Type]    IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_FlatFinancialCorrections_Status]        CHECK       ([Status]  IN (1, 2))
);
GO

exec spDescTable  N'FlatFinancialCorrections', N'Финансови корекции за системни пропуски.'
exec spDescColumn N'FlatFinancialCorrections', N'FlatFinancialCorrectionId'           , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'FlatFinancialCorrections', N'ProgrammeId'                         , N'Идентификатор на програма.'
exec spDescColumn N'FlatFinancialCorrections', N'Name'                                , N'Наименование.'
exec spDescColumn N'FlatFinancialCorrections', N'OrderNum'                            , N'Пореден номер.'
exec spDescColumn N'FlatFinancialCorrections', N'Level'                               , N'Ниво: 1 – Оперативна програма, 2 – Приоритетна ос, 3 – Процедура, 4 - Договор за БФП, 5 - Договор с изпълнител.'
exec spDescColumn N'FlatFinancialCorrections', N'Type'                                , N'Тип: 1 – ФКСП, наложена от УО, 2 – ФКСП, наложена във връзка с препоръка на СО, 3 – ФКСП, наложена във връзка с препоръка на ОО, 4 – ФКСП, наложена във връзка с препоръка на ЕК.'
exec spDescColumn N'FlatFinancialCorrections', N'Status'                              , N'Статус: 1 – Чернова, 2 – Актуална.'
exec spDescColumn N'FlatFinancialCorrections', N'ImpositionDate'                      , N'Дата на налагане.'
exec spDescColumn N'FlatFinancialCorrections', N'ImpositionNumber'                    , N'Номер на решението за налагане.'
exec spDescColumn N'FlatFinancialCorrections', N'Description'                         , N'Описание.'
exec spDescColumn N'FlatFinancialCorrections', N'Base'                                , N'База за определяне на финансовата корекция за системни пропуски.'
exec spDescColumn N'FlatFinancialCorrections', N'BlobKey'                             , N'Идентификатор на файл.'

exec spDescColumn N'FlatFinancialCorrections', N'CreateDate'                          , N'Дата на създаване на записа.'
exec spDescColumn N'FlatFinancialCorrections', N'ModifyDate'                          , N'Дата на последно редактиране на записа.'
exec spDescColumn N'FlatFinancialCorrections', N'Version'                             , N'Версия.'

GO
