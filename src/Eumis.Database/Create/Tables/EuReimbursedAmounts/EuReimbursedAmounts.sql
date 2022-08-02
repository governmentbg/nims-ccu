PRINT 'EuReimbursedAmounts'
GO

CREATE TABLE [dbo].[EuReimbursedAmounts] (
    [EuReimbursedAmountId]       INT               NOT NULL IDENTITY,
    [ProgrammeId]                INT               NOT NULL,
    [Status]                     INT               NOT NULL,
    [FinanceSource]              INT               NOT NULL,
    [PaymentType]                INT               NULL,
    [Date]                       DATETIME2         NULL,

    [PaymentAppNum]              NVARCHAR(50)      NULL,
    [PaymentAppSentDate]         DATETIME2         NULL,
    [PaymentAppDateFrom]         DATETIME2         NULL,
    [PaymentAppDateTo]           DATETIME2         NULL,

    [CertBfpEuAmountLv]          MONEY             NULL,
    [CertBfpBgAmountLv]          MONEY             NULL,
    [CertBfpTotalAmountLv]       MONEY             NULL,
    [CertSelfAmountLv]           MONEY             NULL,
    [CertTotalAmountLv]          MONEY             NULL,

    [CertBfpEuAmountEuro]        MONEY             NULL,
    [CertBfpBgAmountEuro]        MONEY             NULL,
    [CertBfpTotalAmountEuro]     MONEY             NULL,
    [CertSelfAmountEuro]         MONEY             NULL,
    [CertTotalAmountEuro]        MONEY             NULL,

    [EuTranche]                  MONEY             NULL,
    [Note]                       NVARCHAR(MAX)     NULL,

    [IsActivated]                BIT               NOT NULL,
    [DeleteNote]                 NVARCHAR(MAX)     NULL,
    [CreateDate]                 DATETIME2         NOT NULL,
    [ModifyDate]                 DATETIME2         NOT NULL,
    [Version]                    ROWVERSION        NOT NULL

    CONSTRAINT [PK_EuReimbursedAmounts]                   PRIMARY KEY ([EuReimbursedAmountId]),
    CONSTRAINT [FK_EuReimbursedAmounts_Programmes]        FOREIGN KEY ([ProgrammeId])                REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [CHK_EuReimbursedAmounts_Status]           CHECK ([Status]         IN (1, 2, 3)),
    CONSTRAINT [CHK_EuReimbursedAmounts_FinanceSource]    CHECK ([FinanceSource]  IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)),
    CONSTRAINT [CHK_EuReimbursedAmounts_PaymentType]      CHECK ([PaymentType]    IN (1, 2, 3))
);
GO

exec spDescTable  N'EuReimbursedAmounts', N'Възстановени от ЕК суми.'
exec spDescColumn N'EuReimbursedAmounts', N'EuReimbursedAmountId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EuReimbursedAmounts', N'ProgrammeId'           , N'Идентификатор на програма.'
exec spDescColumn N'EuReimbursedAmounts', N'Status'                , N'Статус на проверката: 1 - Чернова, 2 - Въведена, 3 - Анулирана.'
exec spDescColumn N'EuReimbursedAmounts', N'FinanceSource'         , N'Приложим фонд: 1 – ЕСФ, 2 – ЕФРР, 3- КФ, 4 – ИМЗ, 5 – ФЕПНЛ.'
exec spDescColumn N'EuReimbursedAmounts', N'PaymentType'           , N'Тип на плащането: 1 - Авансово финансиране, 2 - Междинно плащане, 3 - Плащане на окончателно салдо.'
exec spDescColumn N'EuReimbursedAmounts', N'Date'                  , N'Дата (swift).'

exec spDescColumn N'EuReimbursedAmounts', N'PaymentAppNum'         , N'Пореден номер на Заявление за плащане.'
exec spDescColumn N'EuReimbursedAmounts', N'PaymentAppSentDate'    , N'Дата на изпращане на Заявлението за плащане.'
exec spDescColumn N'EuReimbursedAmounts', N'PaymentAppDateFrom'    , N'Обхванат период от Заявлението за плащане - от.'
exec spDescColumn N'EuReimbursedAmounts', N'PaymentAppDateTo'      , N'Обхванат период от Заявлението за плащане - до.'

exec spDescColumn N'EuReimbursedAmounts', N'CertBfpEuAmountLv'     , N'Обща сума на сертифицираните разходи - Финансиране от ЕС (лева).'
exec spDescColumn N'EuReimbursedAmounts', N'CertBfpBgAmountLv'     , N'Обща сума на сертифицираните разходи - Финансиране от НФ (лева).'
exec spDescColumn N'EuReimbursedAmounts', N'CertBfpTotalAmountLv'  , N'Обща сума на сертифицираните разходи - БФП (лева).'
exec spDescColumn N'EuReimbursedAmounts', N'CertSelfAmountLv'      , N'Обща сума на сертифицираните разходи - собствено съфинансиране от бенефициента (лева).'
exec spDescColumn N'EuReimbursedAmounts', N'CertTotalAmountLv'     , N'Обща сума на сертифицираните разходите (лева).'

exec spDescColumn N'EuReimbursedAmounts', N'CertBfpEuAmountEuro'   , N'Обща сума на сертифицираните разходи – Финансиране от ЕС (евро).'
exec spDescColumn N'EuReimbursedAmounts', N'CertBfpBgAmountEuro'   , N'Обща сума на сертифицираните разходи - Финансиране от НФ(евро).'
exec spDescColumn N'EuReimbursedAmounts', N'CertBfpTotalAmountEuro', N'Обща сума на сертифицираните разходи - БФП (евро).'
exec spDescColumn N'EuReimbursedAmounts', N'CertSelfAmountEuro'    , N'Обща сума на сертифицираните разходи - собствено съфинансиране от бенефициента (евро).'
exec spDescColumn N'EuReimbursedAmounts', N'CertTotalAmountEuro'   , N'Обща сума на сертифицираните разходи (евро).'

exec spDescColumn N'EuReimbursedAmounts', N'EuTranche'             , N'Преведен транш от ЕК.'
exec spDescColumn N'EuReimbursedAmounts', N'Note'                  , N'Забележка.'

exec spDescColumn N'EuReimbursedAmounts', N'IsActivated'           , N'Маркер дали записът е бил активиран.'
exec spDescColumn N'EuReimbursedAmounts', N'DeleteNote'            , N'Причина за анулиране.'
exec spDescColumn N'EuReimbursedAmounts', N'CreateDate'            , N'Дата на създаване на записа.'
exec spDescColumn N'EuReimbursedAmounts', N'ModifyDate'            , N'Дата на последно редактиране на записа.'
exec spDescColumn N'EuReimbursedAmounts', N'Version'               , N'Версия.'
GO
