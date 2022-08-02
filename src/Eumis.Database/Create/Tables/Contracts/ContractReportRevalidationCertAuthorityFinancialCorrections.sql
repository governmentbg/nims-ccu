PRINT 'ContractReportRevalidationCertAuthorityFinancialCorrections'
GO

CREATE TABLE [dbo].[ContractReportRevalidationCertAuthorityFinancialCorrections] (
    [ContractReportRevalidationCertAuthorityFinancialCorrectionId]  INT                 NOT NULL IDENTITY,
    [ContractReportFinancialId]                                     INT                 NOT NULL,
    [ContractReportId]                                              INT                 NOT NULL,
    [ContractId]                                                    INT                 NOT NULL,
    [Gid]                                                           UNIQUEIDENTIFIER    NOT NULL UNIQUE,

    [OrderNum]                                                      INT                 NOT NULL,
    [Status]                                                        INT                 NOT NULL,
    [CertCorrectionDate]                                            DATETIME2           NULL,
    [BlobKey]                                                       UNIQUEIDENTIFIER    NULL,
    [Notes]                                                         NVARCHAR(MAX)       NULL,
    [CheckedByUserId]                                               INT                 NULL,
    [CheckedDate]                                                   DATETIME2           NULL,

    [CreateDate]                                                    DATETIME2           NOT NULL,
    [ModifyDate]                                                    DATETIME2           NOT NULL,
    [Version]                                                       ROWVERSION          NOT NULL,

    CONSTRAINT [PK_ContractReportRevalidationCertAuthorityFinancialCorrections]                             PRIMARY KEY ([ContractReportRevalidationCertAuthorityFinancialCorrectionId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityFinancialCorrections_ContractReportFinancials]    FOREIGN KEY ([ContractReportFinancialId])   REFERENCES [dbo].[ContractReportFinancials] ([ContractReportFinancialId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityFinancialCorrections_ContractReports]             FOREIGN KEY ([ContractReportId])            REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityFinancialCorrections_Contracts]                   FOREIGN KEY ([ContractId])                  REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityFinancialCorrections_Blobs]                       FOREIGN KEY ([BlobKey])                     REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityFinancialCorrections_CheckedByUser]               FOREIGN KEY ([CheckedByUserId])             REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractReportRevalidationCertAuthorityFinancialCorrections_Status]                     CHECK       ([Status]         IN (1, 2))
);
GO

exec spDescTable  N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'Корекции от СО на препотвърдени сертифицриани суми на ниво РОД - ПОД.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'ContractReportRevalidationCertAuthorityFinancialCorrectionId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'ContractReportFinancialId'                                   , N'Идентификатор на финансов отчет към пакет отчетни документи'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'ContractReportId'                                            , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'ContractId'                                                  , N'Идентификатор на договор'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'Gid'                                                         , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'OrderNum'                                                    , N'Пореден номер.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'Status'                                                      , N'Статус: 1- Чернова; 2 - Приключен.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'CertCorrectionDate'                                          , N'Дата на препотвърждаване.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'BlobKey'                                                     , N'Идентификатор на файл.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'Notes'                                                       , N'Бележки.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'CheckedByUserId'                                             , N'Проверено от.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'CheckedDate'                                                 , N'Дата на проверка.'

exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'CreateDate'                                                  , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'ModifyDate'                                                  , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'Version'                                                     , N'Версия.'
GO
