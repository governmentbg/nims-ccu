PRINT 'ContractReportFinancialCorrections'
GO

CREATE TABLE [dbo].[ContractReportFinancialCorrections] (
    [ContractReportFinancialCorrectionId]       INT               NOT NULL IDENTITY,
    [ContractReportFinancialId]                 INT               NOT NULL,
    [ContractReportId]                          INT               NOT NULL,
    [ContractId]                                INT               NOT NULL,
    [Gid]                                       UNIQUEIDENTIFIER  NOT NULL UNIQUE,

    [OrderNum]                                  INT               NOT NULL,
    [Status]                                    INT               NOT NULL,
    [CorrectionDate]                            DATETIME2         NULL,
    [BlobKey]                                   UNIQUEIDENTIFIER  NULL,
    [Notes]                                     NVARCHAR(MAX)     NULL,
    [CheckedByUserId]                           INT               NULL,
    [CheckedDate]                               DATETIME2         NULL,

    [CreateDate]                                DATETIME2         NOT NULL,
    [ModifyDate]                                DATETIME2         NOT NULL,
    [Version]                                   ROWVERSION        NOT NULL,

    CONSTRAINT [PK_ContractReportFinancialCorrections]                              PRIMARY KEY ([ContractReportFinancialCorrectionId]),
    CONSTRAINT [FK_ContractReportFinancialCorrections_ContractReportFinancials]     FOREIGN KEY ([ContractReportFinancialId]) REFERENCES [dbo].[ContractReportFinancials] ([ContractReportFinancialId]),
    CONSTRAINT [FK_ContractReportFinancialCorrections_ContractReports]              FOREIGN KEY ([ContractReportId])          REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportFinancialCorrections_Contracts]                    FOREIGN KEY ([ContractId])                REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportFinancialCorrections_Blobs]                        FOREIGN KEY ([BlobKey])                   REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [FK_ContractReportFinancialCorrections_CheckedByUser]                FOREIGN KEY ([CheckedByUserId])           REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractReportFinancialCorrections_Status]                      CHECK       ([Status]         IN (1, 2))
);
GO

exec spDescTable  N'ContractReportFinancialCorrections', N'Корекции на верифицирани суми на ниво РОД - ПОД.'
exec spDescColumn N'ContractReportFinancialCorrections', N'ContractReportFinancialCorrectionId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportFinancialCorrections', N'ContractReportFinancialId'                  , N'Идентификатор на финансов отчет към пакет отчетни документи'
exec spDescColumn N'ContractReportFinancialCorrections', N'ContractReportId'                           , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportFinancialCorrections', N'ContractId'                                 , N'Идентификатор на договор'
exec spDescColumn N'ContractReportFinancialCorrections', N'Gid'                                        , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportFinancialCorrections', N'OrderNum'                                   , N'Пореден номер.'
exec spDescColumn N'ContractReportFinancialCorrections', N'Status'                                     , N'Статус: 1- Чернова; 2 - Приключен.'
exec spDescColumn N'ContractReportFinancialCorrections', N'CorrectionDate'                             , N'Дата на корекцията.'
exec spDescColumn N'ContractReportFinancialCorrections', N'BlobKey'                                    , N'Идентификатор на файл.'
exec spDescColumn N'ContractReportFinancialCorrections', N'Notes'                                      , N'Бележки.'
exec spDescColumn N'ContractReportFinancialCorrections', N'CheckedByUserId'                            , N'Проверено от.'
exec spDescColumn N'ContractReportFinancialCorrections', N'CheckedDate'                                , N'Дата на проверка.'

exec spDescColumn N'ContractReportFinancialCorrections', N'CreateDate'                                 , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportFinancialCorrections', N'ModifyDate'                                 , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportFinancialCorrections', N'Version'                                    , N'Версия.'
GO
