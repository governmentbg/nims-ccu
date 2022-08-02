PRINT 'ContractReportCertAuthorityFinancialCorrections'
GO

CREATE TABLE [dbo].[ContractReportCertAuthorityFinancialCorrections] (
    [ContractReportCertAuthorityFinancialCorrectionId]      INT                 NOT NULL IDENTITY,
    [ContractReportFinancialId]                             INT                 NOT NULL,
    [ContractReportId]                                      INT                 NOT NULL,
    [ContractId]                                            INT                 NOT NULL,
    [Gid]                                                   UNIQUEIDENTIFIER    NOT NULL UNIQUE,

    [OrderNum]                                              INT                 NOT NULL,
    [Status]                                                INT                 NOT NULL,
    [CertCorrectionDate]                                    DATETIME2           NULL,
    [BlobKey]                                               UNIQUEIDENTIFIER    NULL,
    [Notes]                                                 NVARCHAR(MAX)       NULL,
    [CheckedByUserId]                                       INT                 NULL,
    [CheckedDate]                                           DATETIME2           NULL,

    [CreateDate]                                            DATETIME2           NOT NULL,
    [ModifyDate]                                            DATETIME2           NOT NULL,
    [Version]                                               ROWVERSION          NOT NULL,

    CONSTRAINT [PK_ContractReportCertAuthorityFinancialCorrections]                             PRIMARY KEY ([ContractReportCertAuthorityFinancialCorrectionId]),
    CONSTRAINT [FK_ContractReportCertAuthorityFinancialCorrections_ContractReportFinancials]    FOREIGN KEY ([ContractReportFinancialId])   REFERENCES [dbo].[ContractReportFinancials] ([ContractReportFinancialId]),
    CONSTRAINT [FK_ContractReportCertAuthorityFinancialCorrections_ContractReports]             FOREIGN KEY ([ContractReportId])            REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportCertAuthorityFinancialCorrections_Contracts]                   FOREIGN KEY ([ContractId])                  REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportCertAuthorityFinancialCorrections_Blobs]                       FOREIGN KEY ([BlobKey])                     REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [FK_ContractReportCertAuthorityFinancialCorrections_CheckedByUser]               FOREIGN KEY ([CheckedByUserId])             REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractReportCertAuthorityFinancialCorrections_Status]                     CHECK       ([Status]         IN (1, 2))
);
GO

exec spDescTable  N'ContractReportCertAuthorityFinancialCorrections', N'Корекции от СО на сертифицриани суми на ниво РОД - ПОД.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'ContractReportCertAuthorityFinancialCorrectionId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'ContractReportFinancialId'                          , N'Идентификатор на финансов отчет към пакет отчетни документи'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'ContractReportId'                                   , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'ContractId'                                         , N'Идентификатор на договор'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'Gid'                                                , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'OrderNum'                                           , N'Пореден номер.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'Status'                                             , N'Статус: 1- Чернова; 2 - Приключен.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'CertCorrectionDate'                                 , N'Дата на препотвърждаване.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'BlobKey'                                            , N'Идентификатор на файл.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'Notes'                                              , N'Бележки.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'CheckedByUserId'                                    , N'Проверено от.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'CheckedDate'                                        , N'Дата на проверка.'

exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'CreateDate'                                         , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'ModifyDate'                                         , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'Version'                                            , N'Версия.'
GO
