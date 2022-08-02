PRINT 'ContractReportTechnicals'
GO

CREATE TABLE [dbo].[ContractReportTechnicals] (
    [ContractReportTechnicalId]             INT               NOT NULL IDENTITY,
    [ContractReportId]                      INT               NOT NULL,
    [ContractId]                            INT               NOT NULL,
    [Gid]                                   UNIQUEIDENTIFIER  NOT NULL UNIQUE,
    [Xml]                                   XML               NOT NULL,
    [Hash]                                  NVARCHAR(10)      NOT NULL UNIQUE,
    [VersionNum]                            INT               NOT NULL,
    [VersionSubNum]                         INT               NOT NULL,
    [Status]                                INT               NOT NULL,
    [StatusNote]                            NVARCHAR(MAX)     NULL,

    [Type]                                  INT               NULL,
    [RegDate]                               DATETIME2         NULL,
    [SubmitDate]                            DATETIME2         NULL,
    [DateFrom]                              DATETIME2         NULL,
    [DateTo]                                DATETIME2         NULL,

    [SenderContractRegistrationId]          INT               NULL,

    [CreateDate]                            DATETIME2         NOT NULL,
    [ModifyDate]                            DATETIME2         NOT NULL,
    [Version]                               ROWVERSION        NOT NULL,

    CONSTRAINT [PK_ContractReportTechnicals]                        PRIMARY KEY ([ContractReportTechnicalId]),
    CONSTRAINT [FK_ContractReportTechnicals_Contracts]              FOREIGN KEY ([ContractId])         REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportTechnicals_ContractReports]        FOREIGN KEY ([ContractReportId])   REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportTechnicals_ContractRegistrations]  FOREIGN KEY ([SenderContractRegistrationId])   REFERENCES [dbo].[ContractRegistrations] ([ContractRegistrationId]),
    CONSTRAINT [CHK_ContractReportTechnicals_Type]                  CHECK ([Type]     IN (1, 2)),
    CONSTRAINT [CHK_ContractReportTechnicals_Status]                CHECK ([Status]   IN (1, 2, 3, 4))
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_ContractReportTechnicals_ContractReportId_Status]
ON [ContractReportTechnicals]([ContractReportId], [Status])
WHERE [Status] = 3;

GO

exec spDescTable  N'ContractReportTechnicals', N'Технически отчет.'
exec spDescColumn N'ContractReportTechnicals', N'ContractReportTechnicalId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportTechnicals', N'ContractReportId'           , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportTechnicals', N'ContractId'                 , N'Идентификатор на договор'
exec spDescColumn N'ContractReportTechnicals', N'Gid'                        , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ContractReportTechnicals', N'Xml'                        , N'Xml съдържание.'
exec spDescColumn N'ContractReportTechnicals', N'Hash'                       , N'Уникален идентификатор на съдържанието на Xml-а.'
exec spDescColumn N'ContractReportTechnicals', N'VersionNum'                 , N'Номер на отчета.'
exec spDescColumn N'ContractReportTechnicals', N'VersionSubNum'              , N'Пореден номер на версията за отчет.'
exec spDescColumn N'ContractReportTechnicals', N'Status'                     , N'Статус: 1 - Чернова, 2 - Въведен, 3 - Актуален/Изпратен, 4 - Върнат.'
exec spDescColumn N'ContractReportTechnicals', N'StatusNote'                 , N'Бележка.'

exec spDescColumn N'ContractReportTechnicals', N'Type'                       , N'Тип на отчета: 1 – Междинен, 2 - Окончателен.'
exec spDescColumn N'ContractReportTechnicals', N'RegDate'                    , N'Дата на регистриране.'
exec spDescColumn N'ContractReportTechnicals', N'SubmitDate'                 , N'Дата на представяне.'
exec spDescColumn N'ContractReportTechnicals', N'DateFrom'                   , N'Период на отчитане - от.'
exec spDescColumn N'ContractReportTechnicals', N'DateTo'                     , N'Период на отчитане - до.'

exec spDescColumn N'ContractReportTechnicals', N'CreateDate'                 , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportTechnicals', N'ModifyDate'                 , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportTechnicals', N'Version'                    , N'Версия.'
GO
