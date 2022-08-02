PRINT 'ContractReportTechnicalCorrections'
GO

CREATE TABLE [dbo].[ContractReportTechnicalCorrections] (
    [ContractReportTechnicalCorrectionId]       INT               NOT NULL IDENTITY,
    [ContractReportTechnicalId]                 INT               NOT NULL,
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

    CONSTRAINT [PK_ContractReportTechnicalCorrections]                              PRIMARY KEY ([ContractReportTechnicalCorrectionId]),
    CONSTRAINT [FK_ContractReportTechnicalCorrections_ContractReportTechnicals]     FOREIGN KEY ([ContractReportTechnicalId]) REFERENCES [dbo].[ContractReportTechnicals] ([ContractReportTechnicalId]),
    CONSTRAINT [FK_ContractReportTechnicalCorrections_ContractReports]              FOREIGN KEY ([ContractReportId])          REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportTechnicalCorrections_Contracts]                    FOREIGN KEY ([ContractId])                REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportTechnicalCorrections_Blobs]                        FOREIGN KEY ([BlobKey])                   REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [FK_ContractReportTechnicalCorrections_CheckedByUser]                FOREIGN KEY ([CheckedByUserId])           REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractReportTechnicalCorrections_Status]                      CHECK       ([Status]         IN (1, 2, 3))
);
GO

exec spDescTable  N'ContractReportTechnicalCorrections', N'Корекции на технически отчет към пакет отчетни документи.'
exec spDescColumn N'ContractReportTechnicalCorrections', N'ContractReportTechnicalCorrectionId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportTechnicalCorrections', N'ContractReportTechnicalId'                  , N'Идентификатор на технически отчет към пакет отчетни документи'
exec spDescColumn N'ContractReportTechnicalCorrections', N'ContractReportId'                           , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportTechnicalCorrections', N'ContractId'                                 , N'Идентификатор на договор'
exec spDescColumn N'ContractReportTechnicalCorrections', N'Gid'                                        , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportTechnicalCorrections', N'OrderNum'                                   , N'Пореден номер.'
exec spDescColumn N'ContractReportTechnicalCorrections', N'Status'                                     , N'Статус: 1- Чернова; 2 - Приключен.; 3 - Архивиран'
exec spDescColumn N'ContractReportTechnicalCorrections', N'CorrectionDate'                             , N'Дата на корекцията.'
exec spDescColumn N'ContractReportTechnicalCorrections', N'BlobKey'                                    , N'Идентификатор на файл.'
exec spDescColumn N'ContractReportTechnicalCorrections', N'Notes'                                      , N'Бележки.'
exec spDescColumn N'ContractReportTechnicalCorrections', N'CheckedByUserId'                            , N'Проверено от.'
exec spDescColumn N'ContractReportTechnicalCorrections', N'CheckedDate'                                , N'Дата на проверка.'

exec spDescColumn N'ContractReportTechnicalCorrections', N'CreateDate'                                 , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportTechnicalCorrections', N'ModifyDate'                                 , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportTechnicalCorrections', N'Version'                                    , N'Версия.'
GO
