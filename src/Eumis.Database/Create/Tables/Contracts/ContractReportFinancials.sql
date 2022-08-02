PRINT 'ContractReportFinancials'
GO

CREATE TABLE [dbo].[ContractReportFinancials] (
    [ContractReportFinancialId]             INT               NOT NULL IDENTITY,
    [ContractReportId]                      INT               NOT NULL,
    [ContractId]                            INT               NOT NULL,
    [Gid]                                   UNIQUEIDENTIFIER  NOT NULL UNIQUE,
    [Xml]                                   XML               NOT NULL,
    [Hash]                                  NVARCHAR(10)      NOT NULL UNIQUE,
    [VersionNum]                            INT               NOT NULL,
    [VersionSubNum]                         INT               NOT NULL,
    [Status]                                INT               NOT NULL,
    [StatusNote]                            NVARCHAR(MAX)     NULL,
    
    [SubmitDate]                            DATETIME2         NULL,
    [StartDate]                             DATETIME2         NULL,
    [EndDate]                               DATETIME2         NULL,
    [PaymentsFinalRecipientsAmount]         MONEY             NOT NULL,
    [CommitmentsGuaranteeAmount]            MONEY             NOT NULL,
    [ExpenseManagementAmount]               MONEY             NOT NULL,
    [ManagementFeesAmount]                  MONEY             NOT NULL,
    [CorrespondingPublicSpendingAmount]     MONEY             NOT NULL,

    [SenderContractRegistrationId]          INT               NULL,

    [CreateDate]                            DATETIME2         NOT NULL,
    [ModifyDate]                            DATETIME2         NOT NULL,
    [Version]                               ROWVERSION        NOT NULL,

    CONSTRAINT [PK_ContractReportFinancials]                        PRIMARY KEY ([ContractReportFinancialId]),
    CONSTRAINT [FK_ContractReportFinancials_Contracts]              FOREIGN KEY ([ContractId])         REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportFinancials_ContractReports]        FOREIGN KEY ([ContractReportId])   REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportFinancials_ContractRegistrations]  FOREIGN KEY ([SenderContractRegistrationId])   REFERENCES [dbo].[ContractRegistrations] ([ContractRegistrationId]),
    CONSTRAINT [CHK_ContractReportFinancials_Status]                CHECK ([Status]         IN (1, 2, 3, 4))
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_ContractReportFinancials_ContractReportId_Status]
ON [ContractReportFinancials]([ContractReportId], [Status])
WHERE [Status] = 3;

GO

exec spDescTable  N'ContractReportFinancials', N'Финансов отчет.'
exec spDescColumn N'ContractReportFinancials', N'ContractReportFinancialId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportFinancials', N'ContractReportId'           , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportFinancials', N'ContractId'                 , N'Идентификатор на договор'
exec spDescColumn N'ContractReportFinancials', N'Gid'                        , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ContractReportFinancials', N'Xml'                        , N'Xml съдържание.'
exec spDescColumn N'ContractReportFinancials', N'Hash'                       , N'Уникален идентификатор на съдържанието на Xml-а.'
exec spDescColumn N'ContractReportFinancials', N'VersionNum'                 , N'Номер на отчета.'
exec spDescColumn N'ContractReportFinancials', N'VersionSubNum'              , N'Пореден номер на версията за отчет.'
exec spDescColumn N'ContractReportFinancials', N'Status'                     , N'Статус: 1- Чернова; 2 - Въведен, 3 - Актуален/Изпратен, 4 - Върнат.'
exec spDescColumn N'ContractReportFinancials', N'StatusNote'                 , N'Бележка.'

exec spDescColumn N'ContractReportFinancials', N'StartDate'                  , N'Начална дата.'
exec spDescColumn N'ContractReportFinancials', N'EndDate'                    , N'Крайна дата.'

exec spDescColumn N'ContractReportFinancials', N'CreateDate'                 , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportFinancials', N'ModifyDate'                 , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportFinancials', N'Version'                    , N'Версия.'
GO
