PRINT 'ProgrammeProcedureManuals'
GO

CREATE TABLE [dbo].[ProgrammeProcedureManuals] (
    [ProgrammeProcedureManualId]                INT                 NOT NULL IDENTITY,
    [MapNodeId]                                 INT                 NOT NULL,
    [Name]                                      NVARCHAR(MAX)       NOT NULL,
    [Description]                               NVARCHAR(MAX)       NOT NULL,
    [OrderNum]                                  INT                 NOT NULL,
    [Status]                                    INT                 NOT NULL,
    [ActivationDate]                            DATETIME2           NULL,
    [ActivatedByUserId]                         INT                 NULL,
    [BlobKey]                                   UNIQUEIDENTIFIER    NULL,

    CONSTRAINT [PK_ProgrammeProcedureManuals]                          PRIMARY KEY ([ProgrammeProcedureManualId]),
    CONSTRAINT [FK_ProgrammeProcedureManuals_MapNodes]                 FOREIGN KEY ([MapNodeId])       REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_ProgrammeProcedureManuals_Blobs]                    FOREIGN KEY ([BlobKey])         REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_ProgrammeProcedureManuals_Status]                  CHECK       ([Status] IN (1, 2, 3))
);
GO

exec spDescTable  N'ProgrammeProcedureManuals', N'Процедурен наръчник.'
exec spDescColumn N'ProgrammeProcedureManuals', N'ProgrammeProcedureManualId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProgrammeProcedureManuals', N'MapNodeId'                         , N'Идентификатор на оперативна програма.'
exec spDescColumn N'ProgrammeProcedureManuals', N'Name'                              , N'Наименование.'
exec spDescColumn N'ProgrammeProcedureManuals', N'Description'                       , N'Описание.'
exec spDescColumn N'ProgrammeProcedureManuals', N'OrderNum'                          , N'Пореден номер.'
exec spDescColumn N'ProgrammeProcedureManuals', N'Status'                            , N'Статус на процедурния наръчник: 1 - Чернова, 2 - Актуален, 3 - Архивиран.'
exec spDescColumn N'ProgrammeProcedureManuals', N'ActivationDate'                    , N'Дата на активиране.'
exec spDescColumn N'ProgrammeProcedureManuals', N'ActivatedByUserId'                 , N'Идентификатор на потребител активирал процедурния наръчник.'
exec spDescColumn N'ProgrammeProcedureManuals', N'BlobKey'                           , N'Идентификатор на файл.'

GO

PRINT 'ProgrammeCheckLists'
GO

CREATE TABLE [dbo].[ProgrammeCheckLists] (
    [ProgrammeCheckListId]                      INT                 NOT NULL IDENTITY,
    [MapNodeId]                                 INT                 NOT NULL,
    [Name]                                      NVARCHAR(MAX)       NOT NULL,
    [ProgrammeName]                             NVARCHAR(MAX)       NOT NULL,
    [Description]                               NVARCHAR(MAX)       NOT NULL,
    [Type]                                      INT                 NOT NULL,
    [Status]                                    INT                 NOT NULL,
    [ActivationDate]                            DATETIME2           NULL,
    [ActivatedByUserId]                         INT                 NULL,
    [CreateDate]                                DATETIME2           NOT NULL,
    [ModifyDate]                                DATETIME2           NOT NULL,
    [Version]                                   ROWVERSION          NOT NULL,

    CONSTRAINT [PK_ProgrammeCheckLists]                          PRIMARY KEY ([ProgrammeCheckListId]),
    CONSTRAINT [FK_ProgrammeCheckLists_MapNodes]                 FOREIGN KEY ([MapNodeId])       REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [CHK_ProgrammeCheckLists_Type]                    CHECK       ([Type] IN (1, 2, 3, 4, 5, 6, 7)),
    CONSTRAINT [CHK_ProgrammeCheckLists_Status]                  CHECK       ([Status] IN (1, 2, 3))
);
GO

exec spDescTable  N'ProgrammeCheckLists', N'Контролен лист.'
exec spDescColumn N'ProgrammeCheckLists', N'ProgrammeCheckListId'              , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProgrammeCheckLists', N'MapNodeId'                         , N'Идентификатор на оперативна програма.'
exec spDescColumn N'ProgrammeCheckLists', N'Name'                              , N'Наименование.'
exec spDescColumn N'ProgrammeCheckLists', N'ProgrammeName'                     , N'Наименование на оперативна програма.'
exec spDescColumn N'ProgrammeCheckLists', N'Description'                       , N'Описание.'
exec spDescColumn N'ProgrammeCheckLists', N'Type'                              , N'Тип на контролния лист: 1 - Процедура, 2 - Договор, 3 - Пакет отчетни документи, 4 - Процедура за избор на изпълнител, 5 - Доклад по сертификация, 6 - Проверка на място, 7 - Оперативна програма.'
exec spDescColumn N'ProgrammeCheckLists', N'Status'                            , N'Статус на контролния лист: 1 - Неактивиран, 2 - Активен, 3 - Неактивен.'
exec spDescColumn N'ProgrammeCheckLists', N'ActivationDate'                    , N'Дата на активиране.'
exec spDescColumn N'ProgrammeCheckLists', N'ActivatedByUserId'                 , N'Идентификатор на потребител активирал контролния лист.'
exec spDescColumn N'ProgrammeCheckLists', N'CreateDate'                        , N'Дата на създаване на записа.'
exec spDescColumn N'ProgrammeCheckLists', N'ModifyDate'                        , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProgrammeCheckLists', N'Version'                           , N'Версия.'
GO

PRINT 'ProgrammeCheckListVersionXmls'
GO

CREATE TABLE [dbo].[ProgrammeCheckListVersionXmls] (
    [ProgrammeCheckListVersionXmlId]            INT                 NOT NULL IDENTITY,
    [Gid]                                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [ProgrammeCheckListId]                      INT                 NOT NULL,
    [Xml]                                       XML                 NOT NULL,
    [Hash]                                      NVARCHAR(10)        NOT NULL UNIQUE,
    [VersionNum]                                INT                 NOT NULL,
    [Status]                                    INT                 NOT NULL,
    [ActivationDate]                            DATETIME2           NULL,
    [ActivatedByUserId]                         INT                 NULL,
    [CreateDate]                                DATETIME2           NOT NULL,
    [ModifyDate]                                DATETIME2           NOT NULL,
    [Version]                                   ROWVERSION          NOT NULL,

    CONSTRAINT [PK_ProgrammeCheckListVersionXmls]                          PRIMARY KEY ([ProgrammeCheckListVersionXmlId]),
    CONSTRAINT [FK_ProgrammeCheckListVersionXmls_ProgrammeCheckLists]      FOREIGN KEY ([ProgrammeCheckListId])       REFERENCES [dbo].[ProgrammeCheckLists] ([ProgrammeCheckListId]),
    CONSTRAINT [CHK_ProgrammeCheckListVersionXmls_Status]                  CHECK       ([Status] IN (1, 2, 3, 4))
);
GO

exec spDescTable  N'ProgrammeCheckListVersionXmls', N'Xml за контролен лист.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'ProgrammeCheckListVersionXmlId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'Gid'                               , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'ProgrammeCheckListId'              , N'Идентификатор на контролен лист.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'Xml'                               , N'Xml съдържание.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'Hash'                              , N'Уникален идентификатор на съдържанието на Xml-а.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'VersionNum'                        , N'Пореден номер на версия.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'Status'                            , N'Статус: 1 - Чернова, 2 - Въведен, 3 - Актуален, 4 - Архивиран.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'ActivationDate'                    , N'Дата на активиране.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'ActivatedByUserId'                 , N'Идентификатор на потребител активирал версията.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'CreateDate'                        , N'Дата на създаване на записа.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'ModifyDate'                        , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'Version'                           , N'Версия.'

GO

PRINT 'ProgrammeCheckListVersionXmlFiles'
GO

CREATE TABLE [dbo].[ProgrammeCheckListVersionXmlFiles] (
    [ProgrammeCheckListVersionXmlFileId]       INT                 NOT NULL IDENTITY,
    [ProgrammeCheckListVersionXmlId]           INT                 NOT NULL,
    [BlobKey]                                  UNIQUEIDENTIFIER    NOT NULL,
    [Name]                                     NVARCHAR(200)       NOT NULL,
    [Description]                              NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ProgrammeCheckListVersionXmlFiles]                                 PRIMARY KEY ([ProgrammeCheckListVersionXmlFileId]),
    CONSTRAINT [FK_ProgrammeCheckListVersionXmlFiles_ProgrammeCheckListVersionXmls]   FOREIGN KEY ([ProgrammeCheckListVersionXmlId])    REFERENCES [dbo].[ProgrammeCheckListVersionXmls] ([ProgrammeCheckListVersionXmlId]),
    CONSTRAINT [FK_ProgrammeCheckListVersionXmlFiles_Blobs]                           FOREIGN KEY ([BlobKey])                           REFERENCES [dbo].[Blobs] ([Key])
);
GO

exec spDescTable  N'ProgrammeCheckListVersionXmlFiles', N'Файлове към xml за контролен лист.'
exec spDescColumn N'ProgrammeCheckListVersionXmlFiles', N'ProgrammeCheckListVersionXmlFileId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProgrammeCheckListVersionXmlFiles', N'ProgrammeCheckListVersionXmlId'           , N'Идентификатор на xml за контролен лист.'
exec spDescColumn N'ProgrammeCheckListVersionXmlFiles', N'BlobKey'                                  , N'Идентификатор на файл.'
exec spDescColumn N'ProgrammeCheckListVersionXmlFiles', N'Name'                                     , N'Име на файл.'
exec spDescColumn N'ProgrammeCheckListVersionXmlFiles', N'Description'                              , N'Описание.'
GO
