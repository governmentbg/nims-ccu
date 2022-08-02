PRINT 'ContractReportFinancialCertCorrections'
GO

CREATE TABLE [dbo].[ContractReportFinancialCertCorrections] (
    [ContractReportFinancialCertCorrectionId]   INT               NOT NULL IDENTITY,
    [ContractReportFinancialId]                 INT               NOT NULL,
    [ContractReportId]                          INT               NOT NULL,
    [ContractId]                                INT               NOT NULL,
    [Gid]                                       UNIQUEIDENTIFIER  NOT NULL UNIQUE,

    [OrderNum]                                  INT               NOT NULL,
    [Status]                                    INT               NOT NULL,
    [CertCorrectionDate]                        DATETIME2         NULL,
    [BlobKey]                                   UNIQUEIDENTIFIER  NULL,
    [Notes]                                     NVARCHAR(MAX)     NULL,
    [CheckedByUserId]                           INT               NULL,
    [CheckedDate]                               DATETIME2         NULL,

    [CreateDate]                                DATETIME2         NOT NULL,
    [ModifyDate]                                DATETIME2         NOT NULL,
    [Version]                                   ROWVERSION        NOT NULL,

    CONSTRAINT [PK_ContractReportFinancialCertCorrections]                              PRIMARY KEY ([ContractReportFinancialCertCorrectionId]),
    CONSTRAINT [FK_ContractReportFinancialCertCorrections_ContractReportFinancials]     FOREIGN KEY ([ContractReportFinancialId]) REFERENCES [dbo].[ContractReportFinancials] ([ContractReportFinancialId]),
    CONSTRAINT [FK_ContractReportFinancialCertCorrections_ContractReports]              FOREIGN KEY ([ContractReportId])          REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportFinancialCertCorrections_Contracts]                    FOREIGN KEY ([ContractId])                REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportFinancialCertCorrections_Blobs]                        FOREIGN KEY ([BlobKey])                   REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [FK_ContractReportFinancialCertCorrections_CheckedByUser]                FOREIGN KEY ([CheckedByUserId])           REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractReportFinancialCertCorrections_Status]                      CHECK       ([Status]         IN (1, 2))
);
GO

exec spDescTable  N'ContractReportFinancialCertCorrections', N'Корекции на сертифицриани суми на ниво РОД - ПОД.'
exec spDescColumn N'ContractReportFinancialCertCorrections', N'ContractReportFinancialCertCorrectionId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportFinancialCertCorrections', N'ContractReportFinancialId'                  , N'Идентификатор на финансов отчет към пакет отчетни документи'
exec spDescColumn N'ContractReportFinancialCertCorrections', N'ContractReportId'                           , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportFinancialCertCorrections', N'ContractId'                                 , N'Идентификатор на договор'
exec spDescColumn N'ContractReportFinancialCertCorrections', N'Gid'                                        , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportFinancialCertCorrections', N'OrderNum'                                   , N'Пореден номер.'
exec spDescColumn N'ContractReportFinancialCertCorrections', N'Status'                                     , N'Статус: 1- Чернова; 2 - Приключен.'
exec spDescColumn N'ContractReportFinancialCertCorrections', N'CertCorrectionDate'                           , N'Дата на pрепотвърждаване.'
exec spDescColumn N'ContractReportFinancialCertCorrections', N'BlobKey'                                    , N'Идентификатор на файл.'
exec spDescColumn N'ContractReportFinancialCertCorrections', N'Notes'                                      , N'Бележки.'
exec spDescColumn N'ContractReportFinancialCertCorrections', N'CheckedByUserId'                            , N'Проверено от.'
exec spDescColumn N'ContractReportFinancialCertCorrections', N'CheckedDate'                                , N'Дата на проверка.'

exec spDescColumn N'ContractReportFinancialCertCorrections', N'CreateDate'                                 , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportFinancialCertCorrections', N'ModifyDate'                                 , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportFinancialCertCorrections', N'Version'                                    , N'Версия.'
GO
