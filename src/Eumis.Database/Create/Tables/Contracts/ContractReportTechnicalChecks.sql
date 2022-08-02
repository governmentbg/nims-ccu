PRINT 'ContractReportTechnicalChecks'
GO

CREATE TABLE [dbo].[ContractReportTechnicalChecks] (
    [ContractReportTechnicalCheckId]        INT               NOT NULL IDENTITY,
    [ContractReportTechnicalId]             INT               NOT NULL,
    [ContractReportId]                      INT               NOT NULL,
    [ContractId]                            INT               NOT NULL,
    [Gid]                                   UNIQUEIDENTIFIER  NOT NULL UNIQUE,

    [OrderNum]                              INT               NOT NULL,
    [Status]                                INT               NOT NULL,
    [Approval]                              INT               NULL,
    [BlobKey]                               UNIQUEIDENTIFIER  NULL,
    [CheckedByUserId]                       INT               NULL,
    [CheckedDate]                           DATETIME2         NULL,

    [CreateDate]                            DATETIME2         NOT NULL,
    [ModifyDate]                            DATETIME2         NOT NULL,
    [Version]                               ROWVERSION        NOT NULL,

    CONSTRAINT [PK_ContractReportTechnicalChecks]                             PRIMARY KEY ([ContractReportTechnicalCheckId]),
    CONSTRAINT [FK_ContractReportTechnicalChecks_ContractReportTechnicals]    FOREIGN KEY ([ContractReportTechnicalId])         REFERENCES [dbo].[ContractReportTechnicals] ([ContractReportTechnicalId]),
    CONSTRAINT [FK_ContractReportTechnicalChecks_ContractReports]             FOREIGN KEY ([ContractReportId])                  REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportTechnicalChecks_Contracts]                   FOREIGN KEY ([ContractId])                        REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportTechnicalChecks_Blobs]                       FOREIGN KEY ([BlobKey])                           REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [FK_ContractReportTechnicalChecks_CheckedByUser]               FOREIGN KEY ([CheckedByUserId])                   REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractReportTechnicalChecks_Status]                     CHECK       ([Status]         IN (1, 2, 3))
);
GO

exec spDescTable  N'ContractReportTechnicalChecks', N'Проверка на технически отчет.'
exec spDescColumn N'ContractReportTechnicalChecks', N'ContractReportTechnicalCheckId'   , N'Уникален системно генериран идентификатор'
exec spDescColumn N'ContractReportTechnicalChecks', N'ContractReportTechnicalId'        , N'Идентификатор на технически отчет'
exec spDescColumn N'ContractReportTechnicalChecks', N'ContractReportId'                 , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportTechnicalChecks', N'ContractId'                       , N'Идентификатор на договор'
exec spDescColumn N'ContractReportTechnicalChecks', N'Gid'                              , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportTechnicalChecks', N'OrderNum'                         , N'Пореден номер.'
exec spDescColumn N'ContractReportTechnicalChecks', N'Status'                           , N'Статус: 1 - Чернова, 2 - Актуален, 3 - Архивиран'
exec spDescColumn N'ContractReportTechnicalChecks', N'Approval'                         , N'Одобрение: 1- Одобрен; 2 - Неодобрен.'
exec spDescColumn N'ContractReportTechnicalChecks', N'BlobKey'                          , N'Идентификатор на файл.'
exec spDescColumn N'ContractReportTechnicalChecks', N'CheckedByUserId'                  , N'Проверено от.'
exec spDescColumn N'ContractReportTechnicalChecks', N'CheckedDate'                      , N'Дата на проверка.'

exec spDescColumn N'ContractReportTechnicalChecks', N'CreateDate'                       , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportTechnicalChecks', N'ModifyDate'                       , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportTechnicalChecks', N'Version'                          , N'Версия.'
GO
