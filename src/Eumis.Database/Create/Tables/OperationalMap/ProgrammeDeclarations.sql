PRINT 'ProgrammeDeclarations'
GO

CREATE TABLE [dbo].[ProgrammeDeclarations] (
    [ProgrammeDeclarationId]        INT                 NOT NULL IDENTITY,
    [ProgrammeId]                   INT                 NOT NULL,
    [OrderNum]                      INT                 NOT NULL,
    [Name]                          NVARCHAR(500)       NOT NULL,
    [NameAlt]                       NVARCHAR(500)       NULL,
    [Content]                       NVARCHAR(MAX)       NOT NULL,
    [ContentAlt]                    NVARCHAR(MAX)       NULL,
    [IsActive]                      BIT                 NOT NULL,
    [FieldType]                     INT                 NOT NULL,
    [Type]                          BIT                 NOT NULL,
    [CreateDate]                    DATETIME2           NOT NULL,
    [ModifyDate]                    DATETIME2           NOT NULL,
    [IsConsentForNSIDataProviding]  BIT                 NOT NULL,
    [Version]                       ROWVERSION          NOT NULL,

    CONSTRAINT [PK_ProgrammeDeclarations]                      PRIMARY KEY ([ProgrammeDeclarationId]),
    CONSTRAINT [FK_ProgrammeDeclarations_Programmes]           FOREIGN KEY ([ProgrammeId])             REFERENCES [dbo].[MapNodes]   ([MapNodeId]),
    CONSTRAINT [CHK_ProgrammeDeclarations_FieldType]           CHECK       ([FieldType] IN (1, 2, 3, 4, 5, 6, 7)),
    CONSTRAINT [CHK_ProgrammeDeclarations_Type]                CHECK       ([Type]      IN (1)),
    CONSTRAINT [UQ_ProgrammeDeclarations_ProgrammeId_OrderNum] UNIQUE      ([ProgrammeId], [OrderNum])
);
GO

exec spDescTable  N'ProgrammeDeclarations', N'Декларации към ОП.'
exec spDescColumn N'ProgrammeDeclarations', N'ProgrammeDeclarationId'            , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProgrammeDeclarations', N'ProgrammeId'                       , N'Идентификатор на оперативна програма.'
exec spDescColumn N'ProgrammeDeclarations', N'OrderNum'                          , N'Пореден номер.'
exec spDescColumn N'ProgrammeDeclarations', N'Name'                              , N'Наименование.'
exec spDescColumn N'ProgrammeDeclarations', N'NameAlt'                           , N'Наименование на английски.'
exec spDescColumn N'ProgrammeDeclarations', N'Content'                           , N'Съдържание.'
exec spDescColumn N'ProgrammeDeclarations', N'ContentAlt'                        , N'Съдържание на английски.'
exec spDescColumn N'ProgrammeDeclarations', N'IsActive'                          , N'Маркер за активност.'
exec spDescColumn N'ProgrammeDeclarations', N'FieldType'                         , N'Тип на полето за приемане на декларацията: 1 - Чекбокс, 2 - Числова стойност, 3 - Текст, 4 - Парична стойност (лв.), 5 - Падащо меню, 6 - Дата, 7 - Период.'
exec spDescColumn N'ProgrammeDeclarations', N'Type'                              , N'Тип: 1 - Декларацията към формуляр за кандидатстване.'
exec spDescColumn N'ProgrammeDeclarations', N'CreateDate'                        , N'Дата на създаване на записа.'
exec spDescColumn N'ProgrammeDeclarations', N'ModifyDate'                        , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProgrammeDeclarations', N'IsConsentForNSIDataProviding'      , N'Маркер - "Декларацията е за съгласие данните на кандидата да бъдат предоставени от НСИ".'
exec spDescColumn N'ProgrammeDeclarations', N'Version'                           , N'Версия.'
GO
