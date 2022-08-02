GO

ALTER TABLE [dbo].[ProgrammeCheckLists] ADD CONSTRAINT [FK_CheckLists_Users]
FOREIGN KEY ([ActivatedByUserId]) REFERENCES [dbo].[Users] ([UserId]);

ALTER TABLE [dbo].[ProgrammeCheckListVersionXmls] ADD CONSTRAINT [FK_CheckListVersionXmls_Users]
FOREIGN KEY ([ActivatedByUserId]) REFERENCES [dbo].[Users] ([UserId]);

ALTER TABLE [dbo].[ProgrammeProcedureManuals] ADD CONSTRAINT [FK_ProgrammeProcedureManuals_Users]
FOREIGN KEY ([ActivatedByUserId]) REFERENCES [dbo].[Users] ([UserId]);

GO

PRINT 'CheckSheets'
GO

CREATE TABLE [dbo].[CheckSheets] (
    [CheckSheetId]                              INT                 NOT NULL IDENTITY,
    [MapNodeId]                                 INT                 NOT NULL,
    [ProgrammeCheckListId]                      INT                 NOT NULL,
    [ProcedureId]                               INT                 NULL,
    [ContractId]                                INT                 NULL,
    [CompanyId]                                 INT                 NULL,
    [ContractProcurementXmlId]                  INT                 NULL,
    [ContractReportId]                          INT                 NULL,
    [CertReportId]                              INT                 NULL,
    [SpotCheckId]                               INT                 NULL,
    [CheckListVersionNum]                       INT                 NOT NULL,
    [CheckListName]                             NVARCHAR(MAX)       NOT NULL,
    [Notes]                                     NVARCHAR(MAX)       NULL,
    [Type]                                      INT                 NOT NULL,
    [Status]                                    INT                 NOT NULL,
    [CreatedByUserId]                           INT                 NOT NULL,
    [CreateDate]                                DATETIME2           NOT NULL,
    [ModifyDate]                                DATETIME2           NOT NULL,
    [Version]                                   ROWVERSION          NOT NULL,

    CONSTRAINT [PK_CheckSheets]                          PRIMARY KEY ([CheckSheetId]),
    CONSTRAINT [FK_CheckSheets_MapNodes]                 FOREIGN KEY ([MapNodeId])                        REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_CheckSheets_ProgrammeCheckLists]      FOREIGN KEY ([ProgrammeCheckListId])             REFERENCES [dbo].[ProgrammeCheckLists] ([ProgrammeCheckListId]),
    CONSTRAINT [FK_CheckSheets_Users]                    FOREIGN KEY ([CreatedByUserId])                  REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_CheckSheets_Procedures]               FOREIGN KEY ([ProcedureId])                      REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_CheckSheets_Contracts]                FOREIGN KEY ([ContractId])                       REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_CheckSheets_Companies]                FOREIGN KEY ([CompanyId])                        REFERENCES [dbo].[Companies] ([CompanyId]),
    CONSTRAINT [FK_CheckSheets_ContractProcurementXmls]  FOREIGN KEY ([ContractProcurementXmlId])         REFERENCES [dbo].[ContractProcurementXmls] ([ContractProcurementXmlId]),
    CONSTRAINT [FK_CheckSheets_ContractReports]          FOREIGN KEY ([ContractReportId])                 REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_CheckSheets_CertReports]              FOREIGN KEY ([CertReportId])                     REFERENCES [dbo].[CertReports] ([CertReportId]),
    CONSTRAINT [FK_CheckSheets_SpotChecks]               FOREIGN KEY ([SpotCheckId])                      REFERENCES [dbo].[SpotChecks] ([SpotCheckId]),
    CONSTRAINT [CHK_CheckSheets_Type]                    CHECK       ([Type] IN (1, 2, 3, 4, 5, 6, 7)),
    CONSTRAINT [CHK_CheckSheets_Status]                  CHECK       ([Status] IN (1, 2, 3, 4))
);
GO

exec spDescTable  N'CheckSheets', N'Контролен лист.'
exec spDescColumn N'CheckSheets', N'CheckSheetId'                      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CheckSheets', N'MapNodeId'                         , N'Идентификатор на оперативна програма.'
exec spDescColumn N'CheckSheets', N'ProgrammeCheckListId'              , N'Идентификатор на шаблон за контролен лист.'
exec spDescColumn N'CheckSheets', N'ProcedureId'                       , N'Идентификатор на процедура.'
exec spDescColumn N'CheckSheets', N'ContractId'                        , N'Идентификатор на договор.'
exec spDescColumn N'CheckSheets', N'CompanyId'                         , N'Идентификатор на бенефициент.'
exec spDescColumn N'CheckSheets', N'ContractProcurementXmlId'          , N'Идентификатор на процедура за избор на изпълнител.'
exec spDescColumn N'CheckSheets', N'ContractReportId'                  , N'Идентификатор на пакет отчетни документи.'
exec spDescColumn N'CheckSheets', N'CertReportId'                      , N'Идентификатор на доклад по сертификация.'
exec spDescColumn N'CheckSheets', N'SpotCheckId'                       , N'Идентификатор на проверка на място.'
exec spDescColumn N'CheckSheets', N'CheckListVersionNum'               , N'Номер на текущата версия.'
exec spDescColumn N'CheckSheets', N'CheckListName'                     , N'Наименование.'
exec spDescColumn N'CheckSheets', N'Notes'                             , N'Бележки.'
exec spDescColumn N'CheckSheets', N'Type'                              , N'Тип на контролен лист: 1 - Процедура, 2 - Договор, 3 - Пакет отчетни документи, 4 - Процедура за избор на изпълнител, 5 - Доклад по сертификация, 6 - Проверка на място, 7 - Оперативна програма.'
exec spDescColumn N'CheckSheets', N'Status'                            , N'Статус на контролен лист: 1 - Чернова, 2 - В изпълнение, 3 - Приключен, 4 - Анулиран.'
exec spDescColumn N'CheckSheets', N'CreatedByUserId'                   , N'Идентификатор на потребител създал контролен лист.'
exec spDescColumn N'CheckSheets', N'CreateDate'                        , N'Дата на създаване на записа.'
exec spDescColumn N'CheckSheets', N'ModifyDate'                        , N'Дата на последно редактиране на записа.'
exec spDescColumn N'CheckSheets', N'Version'                           , N'Версия.'

GO

PRINT 'CheckSheetVersionXmls'
GO

CREATE TABLE [dbo].[CheckSheetVersionXmls] (
    [CheckSheetVersionXmlId]                    INT                 NOT NULL IDENTITY,
    [Gid]                                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [CheckSheetId]                              INT                 NOT NULL,
    [VersionNum]                                INT                 NOT NULL,
    [Xml]                                       XML                 NOT NULL,
    [Hash]                                      NVARCHAR(10)        NOT NULL UNIQUE,
    [UserId]                                    INT                 NOT NULL,
    [CreateDate]                                DATETIME2           NOT NULL,
    [ModifyDate]                                DATETIME2           NOT NULL,
    [Version]                                   ROWVERSION          NOT NULL,

    CONSTRAINT [PK_CheckSheetVersionXmls]                          PRIMARY KEY ([CheckSheetVersionXmlId]),
    CONSTRAINT [FK_CheckSheetVersionXmls_CheckSheets]              FOREIGN KEY ([CheckSheetId])            REFERENCES [dbo].[CheckSheets] ([CheckSheetId]),
    CONSTRAINT [FK_CheckSheetVersionXmls_Users]                    FOREIGN KEY ([UserId])                  REFERENCES [dbo].[Users] ([UserId])
);
GO

exec spDescTable  N'CheckSheetVersionXmls', N'Xml за контролен лист.'
exec spDescColumn N'CheckSheetVersionXmls', N'CheckSheetVersionXmlId'            , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CheckSheetVersionXmls', N'Gid'                               , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'CheckSheetVersionXmls', N'CheckSheetId'                      , N'Идентификатор на контролен лист.'
exec spDescColumn N'CheckSheetVersionXmls', N'VersionNum'                        , N'Пореден номер на версия.'
exec spDescColumn N'CheckSheetVersionXmls', N'Xml'                               , N'Xml съдържание.'
exec spDescColumn N'CheckSheetVersionXmls', N'Hash'                              , N'Уникален идентификатор на съдържанието на Xml-а.'
exec spDescColumn N'CheckSheetVersionXmls', N'UserId'                            , N'Идентификатор на потребител създал версията.'
exec spDescColumn N'CheckSheetVersionXmls', N'CreateDate'                        , N'Дата на създаване на записа.'
exec spDescColumn N'CheckSheetVersionXmls', N'ModifyDate'                        , N'Дата на последно редактиране на записа.'
exec spDescColumn N'CheckSheetVersionXmls', N'Version'                           , N'Версия.'

GO

PRINT 'CheckSheetVersionXmlFiles'
GO

CREATE TABLE [dbo].[CheckSheetVersionXmlFiles] (
    [CheckSheetVersionXmlFileId]       INT                 NOT NULL IDENTITY,
    [CheckSheetVersionXmlId]           INT                 NOT NULL,
    [BlobKey]                          UNIQUEIDENTIFIER    NOT NULL,
    [Name]                             NVARCHAR(200)       NOT NULL,
    [Description]                      NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_CheckSheetVersionXmlFiles]                                 PRIMARY KEY ([CheckSheetVersionXmlFileId]),
    CONSTRAINT [FK_CheckSheetVersionXmlFiles_CheckSheetVersionXmls]           FOREIGN KEY ([CheckSheetVersionXmlId])    REFERENCES [dbo].[CheckSheetVersionXmls] ([CheckSheetVersionXmlId]),
    CONSTRAINT [FK_CheckSheetVersionXmlFiles_Blobs]                           FOREIGN KEY ([BlobKey])                   REFERENCES [dbo].[Blobs] ([Key])
);
GO

exec spDescTable  N'CheckSheetVersionXmlFiles', N'Файлове към xml за контролен лист.'
exec spDescColumn N'CheckSheetVersionXmlFiles', N'CheckSheetVersionXmlFileId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CheckSheetVersionXmlFiles', N'CheckSheetVersionXmlId'           , N'Идентификатор на xml за оценителна таблица.'
exec spDescColumn N'CheckSheetVersionXmlFiles', N'BlobKey'                          , N'Идентификатор на файл.'
exec spDescColumn N'CheckSheetVersionXmlFiles', N'Name'                             , N'Име на файл.'
exec spDescColumn N'CheckSheetVersionXmlFiles', N'Description'                      , N'Описание.'
GO

PRINT 'CheckSheetActionLogs'
GO

CREATE TABLE [dbo].[CheckSheetActionLogs] (
    [CheckSheetActionLogId]                     INT                 NOT NULL IDENTITY,
    [CheckSheetVersionXmlId]                    INT                 NULL,
    [CheckSheetId]                              INT                 NOT NULL,
    [VersionNum]                                INT                 NOT NULL,
    [UserId]                                    INT                 NOT NULL,
    [Role]                                      NVARCHAR(MAX)       NULL,
    [Action]                                    INT                 NOT NULL,
    [CreateDate]                                DATETIME2           NOT NULL,

    CONSTRAINT [PK_CheckSheetActionLogs]                          PRIMARY KEY ([CheckSheetActionLogId]),
    CONSTRAINT [FK_CheckSheetActionLogs_CheckSheets]              FOREIGN KEY ([CheckSheetId])            REFERENCES [dbo].[CheckSheets] ([CheckSheetId]),
    CONSTRAINT [FK_CheckSheetActionLogs_CheckSheetVersionXmls]    FOREIGN KEY ([CheckSheetVersionXmlId])  REFERENCES [dbo].[CheckSheetVersionXmls] ([CheckSheetVersionXmlId]),
    CONSTRAINT [FK_CheckSheetActionLogs_Users]                    FOREIGN KEY ([UserId])                  REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_CheckSheetActionLogs_Action]                  CHECK       ([Action] IN (1, 2, 3, 4))
);
GO

exec spDescTable  N'CheckSheetActionLogs', N'Действие извършено върху контролен лист.'
exec spDescColumn N'CheckSheetActionLogs', N'CheckSheetActionLogId'             , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CheckSheetActionLogs', N'CheckSheetVersionXmlId'            , N'Идентификатор на Xml версия на контролен лист.'
exec spDescColumn N'CheckSheetActionLogs', N'CheckSheetId'                      , N'Идентификатор на контролен лист.'
exec spDescColumn N'CheckSheetActionLogs', N'VersionNum'                        , N'Пореден номер на версия.'
exec spDescColumn N'CheckSheetActionLogs', N'UserId'                            , N'Идентификатор на потребител извършил действието.'
exec spDescColumn N'CheckSheetActionLogs', N'Role'                              , N'Роля на потребител извършил действието.'
exec spDescColumn N'CheckSheetActionLogs', N'Action'                            , N'Тип на действието: 1 - Създаване, 2 - Приключване, 3 - Връщане, 4 - Анулиране.'
exec spDescColumn N'CheckSheetActionLogs', N'CreateDate'                        , N'Дата на създаване на записа.'

GO
