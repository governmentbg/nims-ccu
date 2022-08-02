PRINT 'ContractReportFinancialChecks'
GO

CREATE TABLE [dbo].[ContractReportFinancialChecks] (
    [ContractReportFinancialCheckId]        INT               NOT NULL IDENTITY,
    [ContractReportFinancialId]             INT               NOT NULL,
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

    CONSTRAINT [PK_ContractReportFinancialChecks]                             PRIMARY KEY ([ContractReportFinancialCheckId]),
    CONSTRAINT [FK_ContractReportFinancialChecks_ContractReportFinancials]    FOREIGN KEY ([ContractReportFinancialId])         REFERENCES [dbo].[ContractReportFinancials] ([ContractReportFinancialId]),
    CONSTRAINT [FK_ContractReportFinancialChecks_ContractReports]             FOREIGN KEY ([ContractReportId])                  REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportFinancialChecks_Contracts]                   FOREIGN KEY ([ContractId])                        REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportFinancialChecks_Blobs]                       FOREIGN KEY ([BlobKey])                           REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [FK_ContractReportFinancialChecks_CheckedByUser]               FOREIGN KEY ([CheckedByUserId])                   REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractReportFinancialChecks_Status]                     CHECK ([Status]         IN (1, 2, 3))
);
GO

exec spDescTable  N'ContractReportFinancialChecks', N'Проверка на финансов отчет.'
exec spDescColumn N'ContractReportFinancialChecks', N'ContractReportFinancialCheckId'   , N'Уникален системно генериран идентификатор'
exec spDescColumn N'ContractReportFinancialChecks', N'ContractReportFinancialId'        , N'Идентификатор на финансов отчет'
exec spDescColumn N'ContractReportFinancialChecks', N'ContractReportId'                 , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportFinancialChecks', N'ContractId'                       , N'Идентификатор на договор'
exec spDescColumn N'ContractReportFinancialChecks', N'Gid'                              , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportFinancialChecks', N'OrderNum'                         , N'Пореден номер.'
exec spDescColumn N'ContractReportFinancialChecks', N'Status'                           , N'Статус: 1 - Чернова, 2 - Актуален, 3 - Архивиран.'
exec spDescColumn N'ContractReportFinancialChecks', N'Approval'                         , N'Одобрение: 1- Одобрен; 2 - Неодобрен.'
exec spDescColumn N'ContractReportFinancialChecks', N'BlobKey'                          , N'Идентификатор на файл.'
exec spDescColumn N'ContractReportFinancialChecks', N'CheckedByUserId'                  , N'Проверено от.'
exec spDescColumn N'ContractReportFinancialChecks', N'CheckedDate'                      , N'Дата на проверка.'

exec spDescColumn N'ContractReportFinancialChecks', N'CreateDate'                       , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportFinancialChecks', N'ModifyDate'                       , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportFinancialChecks', N'Version'                          , N'Версия.'
GO
