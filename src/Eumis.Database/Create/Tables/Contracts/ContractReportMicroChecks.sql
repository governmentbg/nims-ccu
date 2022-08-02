PRINT 'ContractReportMicroChecks'
GO

CREATE TABLE [dbo].[ContractReportMicroChecks] (
    [ContractReportMicroCheckId] INT               NOT NULL IDENTITY,
    [ContractReportMicroId]      INT               NOT NULL,
    [ContractReportId]           INT               NOT NULL,
    [ContractId]                 INT               NOT NULL,
    [Gid]                        UNIQUEIDENTIFIER  NOT NULL UNIQUE,

    [OrderNum]                   INT               NOT NULL,
    [Status]                     INT               NOT NULL,
    [Approval]                   INT               NULL,
    [BlobKey]                    UNIQUEIDENTIFIER  NULL,
    [CheckedByUserId]            INT               NULL,
    [CheckedDate]                DATETIME2         NULL,

    [CreateDate]                 DATETIME2         NOT NULL,
    [ModifyDate]                 DATETIME2         NOT NULL,
    [Version]                    ROWVERSION        NOT NULL,

    CONSTRAINT [PK_ContractReportMicroChecks]                      PRIMARY KEY ([ContractReportMicroCheckId]),
    CONSTRAINT [FK_ContractReportMicroChecks_ContractReportMicros] FOREIGN KEY ([ContractReportMicroId])         REFERENCES [dbo].[ContractReportMicros] ([ContractReportMicroId]),
    CONSTRAINT [FK_ContractReportMicroChecks_ContractReports]      FOREIGN KEY ([ContractReportId])              REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportMicroChecks_Contracts]            FOREIGN KEY ([ContractId])                    REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportMicroChecks_Blobs]                FOREIGN KEY ([BlobKey])                       REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [FK_ContractReportMicroChecks_CheckedByUser]        FOREIGN KEY ([CheckedByUserId])               REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractReportMicroChecks_Status]              CHECK ([Status]         IN (1, 2, 3))
);
GO

exec spDescTable  N'ContractReportMicroChecks', N'Проверка на микроданни.'
exec spDescColumn N'ContractReportMicroChecks', N'ContractReportMicroCheckId', N'Уникален системно генериран идентификатор'
exec spDescColumn N'ContractReportMicroChecks', N'ContractReportMicroId'     , N'Идентификатор на микроданни'
exec spDescColumn N'ContractReportMicroChecks', N'ContractReportId'          , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportMicroChecks', N'ContractId'                , N'Идентификатор на договор'
exec spDescColumn N'ContractReportMicroChecks', N'Gid'                       , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportMicroChecks', N'OrderNum'                  , N'Пореден номер.'
exec spDescColumn N'ContractReportMicroChecks', N'Status'                    , N'Статус: 1 - Чернова, 2 - Актуален, 3 - Архивиран.'
exec spDescColumn N'ContractReportMicroChecks', N'Approval'                  , N'Одобрение: 1- Одобрен; 2 - Неодобрен.'
exec spDescColumn N'ContractReportMicroChecks', N'BlobKey'                   , N'Идентификатор на файл.'
exec spDescColumn N'ContractReportMicroChecks', N'CheckedByUserId'           , N'Проверено от.'
exec spDescColumn N'ContractReportMicroChecks', N'CheckedDate'               , N'Дата на проверка.'

exec spDescColumn N'ContractReportMicroChecks', N'CreateDate'                , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportMicroChecks', N'ModifyDate'                , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportMicroChecks', N'Version'                   , N'Версия.'
GO
