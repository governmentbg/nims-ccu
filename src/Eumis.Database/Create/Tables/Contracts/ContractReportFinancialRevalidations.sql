PRINT 'ContractReportFinancialRevalidations'
GO

CREATE TABLE [dbo].[ContractReportFinancialRevalidations] (
    [ContractReportFinancialRevalidationId]     INT                  NOT NULL IDENTITY,
    [ContractReportFinancialId]                 INT                  NOT NULL,
    [ContractReportId]                          INT                  NOT NULL,
    [ContractId]                                INT                  NOT NULL,
    [Gid]                                       UNIQUEIDENTIFIER     NOT NULL UNIQUE,

    [OrderNum]                                  INT                  NOT NULL,
    [Status]                                    INT                  NOT NULL,
    [RevalidationDate]                          DATETIME2            NULL,
    [BlobKey]                                   UNIQUEIDENTIFIER     NULL,
    [Notes]                                     NVARCHAR(MAX)        NULL,
    [CheckedByUserId]                           INT                  NULL,
    [CheckedDate]                               DATETIME2            NULL,

    [CreateDate]                                DATETIME2            NOT NULL,
    [ModifyDate]                                DATETIME2            NOT NULL,
    [Version]                                   ROWVERSION           NOT NULL,

    CONSTRAINT [PK_ContractReportFinancialRevalidations]                              PRIMARY KEY ([ContractReportFinancialRevalidationId]),
    CONSTRAINT [FK_ContractReportFinancialRevalidations_ContractReportFinancials]     FOREIGN KEY ([ContractReportFinancialId]) REFERENCES [dbo].[ContractReportFinancials] ([ContractReportFinancialId]),
    CONSTRAINT [FK_ContractReportFinancialRevalidations_ContractReports]              FOREIGN KEY ([ContractReportId])          REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportFinancialRevalidations_Contracts]                    FOREIGN KEY ([ContractId])                REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportFinancialRevalidations_Blobs]                        FOREIGN KEY ([BlobKey])                   REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [FK_ContractReportFinancialRevalidations_CheckedByUser]                FOREIGN KEY ([CheckedByUserId])           REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractReportFinancialRevalidations_Status]                      CHECK       ([Status]         IN (1, 2))
);
GO

exec spDescTable  N'ContractReportFinancialRevalidations', N'Препотвърждаване на верифицирани суми на ниво РОД - ПОД.'
exec spDescColumn N'ContractReportFinancialRevalidations', N'ContractReportFinancialRevalidationId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportFinancialRevalidations', N'ContractReportFinancialId'                  , N'Идентификатор на финансов отчет към пакет отчетни документи'
exec spDescColumn N'ContractReportFinancialRevalidations', N'ContractReportId'                           , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportFinancialRevalidations', N'ContractId'                                 , N'Идентификатор на договор'
exec spDescColumn N'ContractReportFinancialRevalidations', N'Gid'                                        , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportFinancialRevalidations', N'OrderNum'                                   , N'Пореден номер.'
exec spDescColumn N'ContractReportFinancialRevalidations', N'Status'                                     , N'Статус: 1- Чернова; 2 - Приключен.'
exec spDescColumn N'ContractReportFinancialRevalidations', N'RevalidationDate'                           , N'Дата на pрепотвърждаване.'
exec spDescColumn N'ContractReportFinancialRevalidations', N'BlobKey'                                    , N'Идентификатор на файл.'
exec spDescColumn N'ContractReportFinancialRevalidations', N'Notes'                                      , N'Бележки.'
exec spDescColumn N'ContractReportFinancialRevalidations', N'CheckedByUserId'                            , N'Проверено от.'
exec spDescColumn N'ContractReportFinancialRevalidations', N'CheckedDate'                                , N'Дата на проверка.'

exec spDescColumn N'ContractReportFinancialRevalidations', N'CreateDate'                                 , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportFinancialRevalidations', N'ModifyDate'                                 , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportFinancialRevalidations', N'Version'                                    , N'Версия.'
GO
