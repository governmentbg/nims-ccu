PRINT 'ContractReportPaymentChecks'
GO

CREATE TABLE [dbo].[ContractReportPaymentChecks] (
    [ContractReportPaymentCheckId]          INT               NOT NULL IDENTITY,
    [ContractReportPaymentId]               INT               NOT NULL,
    [ContractReportId]                      INT               NOT NULL,
    [ContractId]                            INT               NOT NULL,
    [Gid]                                   UNIQUEIDENTIFIER  NOT NULL UNIQUE,

    [OrderNum]                              INT               NOT NULL,
    [Status]                                INT               NOT NULL,
    [Approval]                              INT               NULL,
    [PaymentType]                           INT               NOT NULL,
    [BlobKey]                               UNIQUEIDENTIFIER  NULL,
    [CheckedByUserId]                       INT               NULL,
    [CheckedDate]                           DATETIME2         NULL,

    [CreateDate]                            DATETIME2         NOT NULL,
    [ModifyDate]                            DATETIME2         NOT NULL,
    [Version]                               ROWVERSION        NOT NULL,

    CONSTRAINT [PK_ContractReportPaymentChecks]                           PRIMARY KEY ([ContractReportPaymentCheckId]),
    CONSTRAINT [FK_ContractReportPaymentChecks_ContractReportPayments]    FOREIGN KEY ([ContractReportPaymentId])   REFERENCES [dbo].[ContractReportPayments] ([ContractReportPaymentId]),
    CONSTRAINT [FK_ContractReportPaymentChecks_ContractReports]           FOREIGN KEY ([ContractReportId])          REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportPaymentChecks_Contracts]                 FOREIGN KEY ([ContractId])                REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportPaymentChecks_CheckedByUser]             FOREIGN KEY ([CheckedByUserId])           REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_ContractReportPaymentChecks_Blobs]                     FOREIGN KEY ([BlobKey])                   REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_ContractReportPaymentChecks_Status]                   CHECK ([Status]          IN (1, 2, 3)),
    CONSTRAINT [CHK_ContractReportPaymentChecks_PaymentType]              CHECK ([PaymentType]     IN (1, 2, 3, 4)),
);
GO

exec spDescTable  N'ContractReportPaymentChecks', N'Проверка на искане за плащане.'
exec spDescColumn N'ContractReportPaymentChecks', N'ContractReportPaymentCheckId'     , N'Уникален системно генериран идентификатор'
exec spDescColumn N'ContractReportPaymentChecks', N'ContractReportPaymentId'          , N'Идентификатор на искане за плащане'
exec spDescColumn N'ContractReportPaymentChecks', N'ContractReportId'                 , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportPaymentChecks', N'ContractId'                       , N'Идентификатор на договор'
exec spDescColumn N'ContractReportPaymentChecks', N'Gid'                              , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportPaymentChecks', N'OrderNum'                         , N'Пореден номер.'
exec spDescColumn N'ContractReportPaymentChecks', N'Status'                           , N'Статус: 1 - Чернова, 2 - Актуален, 3 - Архивиран.'
exec spDescColumn N'ContractReportPaymentChecks', N'Approval'                         , N'Одобрение: 1- Одобрен; 2 - Неодобрен.'
exec spDescColumn N'ContractReportPaymentChecks', N'PaymentType'                      , N'Тип на искането за плащане 1 - Авансово, 2 – Авансово, подлежащо на верификация съгласно чл. 131 от Регламент ЕС 1303/2013, 3 - Междинно, 4 - Окончателно.'
exec spDescColumn N'ContractReportPaymentChecks', N'BlobKey'                          , N'Идентификатор на файл.'
exec spDescColumn N'ContractReportPaymentChecks', N'CheckedByUserId'                  , N'Проверено от.'
exec spDescColumn N'ContractReportPaymentChecks', N'CheckedDate'                      , N'Дата на проверка.'

exec spDescColumn N'ContractReportPaymentChecks', N'CreateDate'                       , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportPaymentChecks', N'ModifyDate'                       , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportPaymentChecks', N'Version'                          , N'Версия.'
GO