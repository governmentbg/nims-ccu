PRINT 'ContractReportPayments'
GO

CREATE TABLE [dbo].[ContractReportPayments] (
    [ContractReportPaymentId]               INT               NOT NULL IDENTITY,
    [ContractReportId]                      INT               NOT NULL,
    [ContractId]                            INT               NOT NULL,
    [Gid]                                   UNIQUEIDENTIFIER  NOT NULL UNIQUE,
    [Xml]                                   XML               NOT NULL,
    [Hash]                                  NVARCHAR(10)      NOT NULL UNIQUE,
    [VersionNum]                            INT               NOT NULL,
    [VersionSubNum]                         INT               NOT NULL,
    [Status]                                INT               NOT NULL,
    [StatusNote]                            NVARCHAR(MAX)     NULL,

    [PaymentType]                           INT               NULL,
    [RegDate]                               DATETIME2         NULL,
    [OtherRegistration]                     NVARCHAR(200)     NULL,
    [SubmitDate]                            DATETIME2         NULL,
    [SubmitDeadline]                        DATETIME2         NULL,
    [DateFrom]                              DATETIME2         NULL,
    [DateTo]                                DATETIME2         NULL,

    [AdditionalIncome]                      MONEY             NULL,
    [TotalAmount]                           MONEY             NULL,
    [RequestedAmount]                       MONEY             NULL,

    [SenderContractRegistrationId]          INT               NULL,

    [CreateDate]                            DATETIME2         NOT NULL,
    [ModifyDate]                            DATETIME2         NOT NULL,
    [Version]                               ROWVERSION        NOT NULL,

    CONSTRAINT [PK_ContractReportPayments]                          PRIMARY KEY ([ContractReportPaymentId]),
    CONSTRAINT [FK_ContractReportPayments_Contracts]                FOREIGN KEY ([ContractId])         REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportPayments_ContractReports]          FOREIGN KEY ([ContractReportId])   REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportPayments_ContractRegistrations]    FOREIGN KEY ([SenderContractRegistrationId])   REFERENCES [dbo].[ContractRegistrations] ([ContractRegistrationId]),
    CONSTRAINT [CHK_ContractReportPayments_PaymentType]             CHECK ([PaymentType]     IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_ContractReportPayments_Status]                  CHECK ([Status]         IN (1, 2, 3, 4))
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_ContractReportPayments_ContractReportId_Status]
ON [ContractReportPayments]([ContractReportId], [Status])
WHERE [Status] = 3;

GO

exec spDescTable  N'ContractReportPayments', N'Искане за плащане.'
exec spDescColumn N'ContractReportPayments', N'ContractReportPaymentId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportPayments', N'ContractReportId'           , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportPayments', N'ContractId'                 , N'Идентификатор на договор'
exec spDescColumn N'ContractReportPayments', N'Gid'                        , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ContractReportPayments', N'Xml'                        , N'Xml съдържание.'
exec spDescColumn N'ContractReportPayments', N'Hash'                       , N'Уникален идентификатор на съдържанието на Xml-а.'
exec spDescColumn N'ContractReportPayments', N'VersionNum'                 , N'Номер на отчета.'
exec spDescColumn N'ContractReportPayments', N'VersionSubNum'              , N'Пореден номер на версията за отчет.'
exec spDescColumn N'ContractReportPayments', N'Status'                     , N'Статус: 1- Чернова; 2 - Въведен, 3 - Актуален/Изпратен, 4 - Върнат.'
exec spDescColumn N'ContractReportPayments', N'StatusNote'                 , N'Бележка.'

exec spDescColumn N'ContractReportPayments', N'PaymentType'                , N'Тип на искането за плащане 1 - Авансово, 2 – Авансово, подлежащо на верификация съгласно чл. 131 от Регламент ЕС 1303/2013, 3 - Междинно, 4 - Окончателно.'
exec spDescColumn N'ContractReportPayments', N'RegDate'                    , N'Дата на регистриране.'
exec spDescColumn N'ContractReportPayments', N'OtherRegistration'          , N'Друга регистрация.'
exec spDescColumn N'ContractReportPayments', N'SubmitDate'                 , N'Дата на представяне.'
exec spDescColumn N'ContractReportPayments', N'SubmitDeadline'             , N'Срок за представяне.'
exec spDescColumn N'ContractReportPayments', N'DateFrom'                   , N'Дата на отчитане - от.'
exec spDescColumn N'ContractReportPayments', N'DateTo'                     , N'Дата на отчитане - до.'

exec spDescColumn N'ContractReportPayments', N'AdditionalIncome'           , N'Допълнителни нетни приходи, които не са заложени в договора/заповедта за БФП/споразумението.'
exec spDescColumn N'ContractReportPayments', N'TotalAmount'                , N'Сума на искането за плащане.'
exec spDescColumn N'ContractReportPayments', N'RequestedAmount'            , N'Стойност на исканите средства за плащане без допълнителните нетни приходиs.'

exec spDescColumn N'ContractReportPayments', N'CreateDate'                 , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportPayments', N'ModifyDate'                 , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportPayments', N'Version'                    , N'Версия.'
GO
