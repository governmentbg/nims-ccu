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
exec spDescColumn N'ProgrammeDeclarations', N'Version'                           , N'Версия.'
GO

PRINT 'ProgrammeDeclarationItems'
GO

CREATE TABLE [dbo].[ProgrammeDeclarationItems] (
    [ProgrammeDeclarationItemId]    INT                 NOT NULL IDENTITY,
    [ProgrammeDeclarationId]        INT                 NOT NULL,
    [Gid]                           UNIQUEIDENTIFIER    NOT NULL,
    [OrderNum]                      INT                 NOT NULL,
    [Content]                       NVARCHAR(100)       NOT NULL,
    [IsActive]                      BIT                 NOT NULL,

    CONSTRAINT [PK_ProgrammeDeclarationItems]                                   PRIMARY KEY ([ProgrammeDeclarationItemId]),
    CONSTRAINT [FK_ProgrammeDeclarationItems_ProgrammeDeclarations]             FOREIGN KEY ([ProgrammeDeclarationId])             REFERENCES [dbo].[ProgrammeDeclarations] ([ProgrammeDeclarationId])
);
GO

exec spDescTable  N'ProgrammeDeclarationItems', N'Елементи от номенклатура към декларация към ОП.'
exec spDescColumn N'ProgrammeDeclarationItems', N'ProgrammeDeclarationItemId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProgrammeDeclarationItems', N'ProgrammeDeclarationId'            , N'Идентификатор на декларация към ОП.'
exec spDescColumn N'ProgrammeDeclarationItems', N'Gid'                               , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ProgrammeDeclarationItems', N'OrderNum'                          , N'Пореден номер.'
exec spDescColumn N'ProgrammeDeclarationItems', N'Content'                           , N'Съдържание.'
exec spDescColumn N'ProgrammeDeclarationItems', N'IsActive'                          , N'Маркер за активност.'
GO

PRINT 'ProcedureDeclarations'
GO

CREATE TABLE [dbo].[ProcedureDeclarations] (
    [ProcedureDeclarationId]                INT                 NOT NULL IDENTITY,
    [Gid]                                   UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [ProcedureId]                           INT                 NOT NULL,
    [ProgrammeDeclarationId]                INT                 NOT NULL,
    [IsActive]                              BIT                 NOT NULL,
    [IsActivated]                           BIT                 NOT NULL,
    [IsRequired]                            BIT                 NOT NULL,
    [Type]                                  INT                 NOT NULL,
    [CreateDate]                            DATETIME2           NOT NULL,
    [ModifyDate]                            DATETIME2           NOT NULL,
    [Version]                               ROWVERSION          NOT NULL,

    CONSTRAINT [PK_ProcedureDeclarations]                           PRIMARY KEY ([ProcedureDeclarationId]),
    CONSTRAINT [FK_ProcedureDeclarations_Procedures]                FOREIGN KEY ([ProcedureId])             REFERENCES [dbo].[Procedures]   ([ProcedureId]),
    CONSTRAINT [FK_ProcedureDeclarations_ProgrammeDeclarations]     FOREIGN KEY ([ProgrammeDeclarationId])  REFERENCES [dbo].[ProgrammeDeclarations]   ([ProgrammeDeclarationId])
);
GO

exec spDescTable  N'ProcedureDeclarations', N'Декларации към процедура.'
exec spDescColumn N'ProcedureDeclarations', N'ProcedureDeclarationId'          , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureDeclarations', N'Gid'                             , N'Публичен системно генериран идентификатор.'
exec spDescColumn N'ProcedureDeclarations', N'ProgrammeDeclarationId'          , N'Идентификатор на декларация към ОП.'
exec spDescColumn N'ProcedureDeclarations', N'ProcedureId'                     , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureDeclarations', N'IsActivated'                     , N'Маркер, дали документът е активиран.'
exec spDescColumn N'ProcedureDeclarations', N'IsActive'                        , N'Маркер, дали документът е активен.'
exec spDescColumn N'ProcedureDeclarations', N'IsRequired'                      , N'Маркер, дали документът е задължителен.'
exec spDescColumn N'ProcedureDeclarations', N'CreateDate'                      , N'Дата на създаване на записа.'
exec spDescColumn N'ProcedureDeclarations', N'ModifyDate'                      , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProcedureDeclarations', N'Version'                         , N'Версия.'

GO

ALTER TABLE [dbo].[ProcedureApplicationSections] DROP CONSTRAINT [CHK_ProcedureApplicationSections_Section]
GO
ALTER TABLE [dbo].[ProcedureApplicationSections] WITH CHECK ADD CONSTRAINT [CHK_ProcedureApplicationSections_Section] CHECK ([Section] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15))
GO

GO
INSERT INTO [dbo].[ProcedureApplicationSections]
SELECT
    ProcedureId,
    15 AS Section,
    Max(OrderNum) + 1 AS OrderNum,
    0 AS IsSelected
FROM [dbo].[ProcedureApplicationSections]
GROUP BY [ProcedureId]

GO
