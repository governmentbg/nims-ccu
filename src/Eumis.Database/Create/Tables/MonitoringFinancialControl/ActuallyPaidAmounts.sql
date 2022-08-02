PRINT 'ActuallyPaidAmounts'
GO

CREATE TABLE [dbo].[ActuallyPaidAmounts] (
    [ActuallyPaidAmountId]    INT           NOT NULL,
    [SapFileId]               INT           NULL,
    [ProgrammeId]             INT           NOT NULL,
    [ProgrammePriorityId]     INT           NOT NULL,
    [FinanceSource]           INT           NOT NULL,
    [ContractId]              INT           NOT NULL,
    [ContractReportPaymentId] INT           NULL,
    [RegNumber]               NVARCHAR(200) NULL,
    [Status]                  INT           NOT NULL,
    [PaymentReason]           INT           NOT NULL,
    [PaymentDate]             DATETIME2     NULL,
    [Comment]                 NVARCHAR(MAX) NULL,

    [PaidBfpEuAmount]         MONEY         NULL,
    [PaidBfpBgAmount]         MONEY         NULL,
    [PaidBfpTotalAmount]      MONEY         NULL,
    [PaidSelfAmount]          MONEY         NULL,
    [PaidTotalAmount]         MONEY         NULL,
    [PaidBfpCrossAmount]      MONEY         NULL,

    [IsActivated]             BIT           NOT NULL,
    [IsDeletedNote]           NVARCHAR(MAX) NULL,
    [CreateDate]              DATETIME2     NOT NULL,
    [ModifyDate]              DATETIME2     NOT NULL,
    [Version]                 ROWVERSION    NOT NULL,

    CONSTRAINT [PK_ActuallyPaidAmounts]                        PRIMARY KEY ([ActuallyPaidAmountId]),
    CONSTRAINT [FK_ActuallyPaidAmounts_SapFiles]               FOREIGN KEY ([SapFileId])               REFERENCES [dbo].[SapFiles]      ([SapFileId]),
    CONSTRAINT [FK_ActuallyPaidAmounts_Programmes]             FOREIGN KEY ([ProgrammeId])             REFERENCES [dbo].[MapNodes]               ([MapNodeId]),
    CONSTRAINT [FK_ActuallyPaidAmounts_ProgrammePriorities]    FOREIGN KEY ([ProgrammePriorityId])     REFERENCES [dbo].[MapNodes]               ([MapNodeId]),
    CONSTRAINT [FK_ActuallyPaidAmounts_Contracts]              FOREIGN KEY ([ContractId])              REFERENCES [dbo].[Contracts]              ([ContractId]),
    CONSTRAINT [FK_ActuallyPaidAmounts_ContractReportPayments] FOREIGN KEY ([ContractReportPaymentId]) REFERENCES [dbo].[ContractReportPayments] ([ContractReportPaymentId]),
    CONSTRAINT [CHK_ActuallyPaidAmounts_FinanceSource]         CHECK       ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)),
    CONSTRAINT [CHK_ActuallyPaidAmounts_Status]                CHECK       ([Status]        IN (1, 2, 3)),
    CONSTRAINT [CHK_ActuallyPaidAmounts_PaymentReason]         CHECK       ([PaymentReason] IN (1, 2, 3, 4)),
);
GO

CREATE UNIQUE INDEX [UQ_ActuallyPaidAmounts_RegNumber]
ON [ActuallyPaidAmounts]([RegNumber])
WHERE [RegNumber] IS NOT NULL;

exec spDescTable  N'ActuallyPaidAmounts', N'Реално изплатени суми.'
exec spDescColumn N'ActuallyPaidAmounts', N'ActuallyPaidAmountId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ActuallyPaidAmounts', N'SapFileId'              , N'Идентификатор на файл от САП.'
exec spDescColumn N'ActuallyPaidAmounts', N'ProgrammeId'            , N'Идентификатор на програма.'
exec spDescColumn N'ActuallyPaidAmounts', N'ProgrammePriorityId'    , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'ActuallyPaidAmounts', N'FinanceSource'          , N'Фонд.'
exec spDescColumn N'ActuallyPaidAmounts', N'ContractId'             , N'Идентификатор на договор.'
exec spDescColumn N'ActuallyPaidAmounts', N'ContractReportPaymentId', N'Идентификатор на искане за плащане.'
exec spDescColumn N'ActuallyPaidAmounts', N'RegNumber'              , N'Регистрационен номер.'
exec spDescColumn N'ActuallyPaidAmounts', N'Status'                 , N'Статус: 1 - Чернова;  2 - Въведена; 3 - Анулирана.'
exec spDescColumn N'ActuallyPaidAmounts', N'PaymentReason'          , N'Основание за плащането: 1 – Авансово плащане, 2 – Верифицирани разходи по ново искане за плащане, 3 – Възстановени суми, 4 – Доплащане.'
exec spDescColumn N'ActuallyPaidAmounts', N'PaymentDate'            , N'Дата.'
exec spDescColumn N'ActuallyPaidAmounts', N'Comment'                , N'Коментар.'

exec spDescColumn N'ActuallyPaidAmounts', N'PaidBfpEuAmount'        , N'Изплатена сума БФП - Финансиране от ЕС.'
exec spDescColumn N'ActuallyPaidAmounts', N'PaidBfpBgAmount'        , N'Изплатена сума БФП - Финансиране от НФ.'
exec spDescColumn N'ActuallyPaidAmounts', N'PaidBfpTotalAmount'     , N'Изплатена сума БФП.'
exec spDescColumn N'ActuallyPaidAmounts', N'PaidSelfAmount'         , N'Изплатена сума - Собствено финансиране.'
exec spDescColumn N'ActuallyPaidAmounts', N'PaidTotalAmount'        , N'Изплатена сума - Общо.'
exec spDescColumn N'ActuallyPaidAmounts', N'PaidBfpCrossAmount'     , N'Кръстосано съфинансиране.'

exec spDescColumn N'ActuallyPaidAmounts', N'IsActivated'            , N'Маркер дали записът е бил активиран.'
exec spDescColumn N'ActuallyPaidAmounts', N'IsDeletedNote'          , N'Причина за изтриване.'
exec spDescColumn N'ActuallyPaidAmounts', N'CreateDate'             , N'Дата на създаване на записа.'
exec spDescColumn N'ActuallyPaidAmounts', N'ModifyDate'             , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ActuallyPaidAmounts', N'Version'                , N'Версия.'
GO