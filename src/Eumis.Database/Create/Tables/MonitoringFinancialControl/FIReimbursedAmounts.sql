PRINT 'FIReimbursedAmounts'
GO

CREATE TABLE [dbo].[FIReimbursedAmounts] (
    [FIReimbursedAmountId]        INT           NOT NULL IDENTITY,
    [ProgrammeId]                 INT           NOT NULL,
    [ProgrammePriorityId]         INT           NULL,
    [FinanceSource]               INT           NULL,
    [ContractId]                  INT           NOT NULL,
    [Status]                      INT           NOT NULL,
    [ReimbursementDate]           DATETIME2     NOT NULL,
    [Type]                        INT           NOT NULL,
    [Reimbursement]               INT           NOT NULL,
    [RegNumber]                   NVARCHAR(200) NULL,

    [PrincipalBfpEuAmount]        MONEY         NULL,
    [PrincipalBfpBgAmount]        MONEY         NULL,
    [PrincipalBfpTotalAmount]     MONEY         NULL,
    [InterestBfpEuAmount]         MONEY         NULL,
    [InterestBfpBgAmount]         MONEY         NULL,
    [InterestBfpTotalAmount]      MONEY         NULL,

    [Comment]                     NVARCHAR(MAX) NULL,
    [ShouldInfluencePaidAmounts]  BIT           NOT NULL,

    [CertReportId]                INT           NULL,

    [IsActivated]                 BIT           NOT NULL,
    [IsDeletedNote]               NVARCHAR(MAX) NULL,
    [CreateDate]                  DATETIME2     NOT NULL,
    [ModifyDate]                  DATETIME2     NOT NULL,
    [Version]                     ROWVERSION    NOT NULL,

    CONSTRAINT [PK_FIReimbursedAmounts]                        PRIMARY KEY ([FIReimbursedAmountId]),
    CONSTRAINT [FK_FIReimbursedAmounts_Programmes]             FOREIGN KEY ([ProgrammeId])    REFERENCES [dbo].[MapNodes]      ([MapNodeId]),
    CONSTRAINT [FK_FIReimbursedAmounts_Contracts]              FOREIGN KEY ([ContractId])     REFERENCES [dbo].[Contracts]     ([ContractId]),
    CONSTRAINT [FK_FIReimbursedAmounts_CertReports]            FOREIGN KEY ([CertReportId])   REFERENCES [dbo].[CertReports]   ([CertReportId]),
    CONSTRAINT [CHK_FIReimbursedAmounts_Status]                CHECK       ([Status]         IN (1, 2, 3)),
    CONSTRAINT [CHK_FIReimbursedAmounts_Type]                  CHECK       ([Type]           IN (1, 2, 3)),
    CONSTRAINT [CHK_FIReimbursedAmounts_Reimbursement]         CHECK       ([Reimbursement]  IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_FIReimbursedAmounts_FinanceSource]         CHECK       ([FinanceSource]  IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12))
);
GO

CREATE UNIQUE INDEX [UQ_FIReimbursedAmounts_RegNumber]
ON [FIReimbursedAmounts]([RegNumber])
WHERE [RegNumber] IS NOT NULL;

exec spDescTable  N'FIReimbursedAmounts', N'Възстановени суми по ФИ.'
exec spDescColumn N'FIReimbursedAmounts', N'FIReimbursedAmountId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'FIReimbursedAmounts', N'ProgrammeId'               , N'Идентификатор на програма.'
exec spDescColumn N'FIReimbursedAmounts', N'ContractId'                , N'Идентификатор на договор.'
exec spDescColumn N'FIReimbursedAmounts', N'Status'                    , N'Статус: 1 - Чернова;  2 - Въведена; 3 - Анулирана.'
exec spDescColumn N'FIReimbursedAmounts', N'ReimbursementDate'         , N'Дата на възстановяване.'
exec spDescColumn N'FIReimbursedAmounts', N'Type'                      , N'Суми, възстановени на финансовия инструмент: 1 - Главница, 2 - Печалби, 3 - Други приходи и доходи.'
exec spDescColumn N'FIReimbursedAmounts', N'Reimbursement'             , N'Начин на възстановяване: 1 – По банков път, 2 – Чрез прихващане, 3 - Отписани..'
exec spDescColumn N'FIReimbursedAmounts', N'RegNumber'                 , N'Регистрационен номер.'

exec spDescColumn N'FIReimbursedAmounts', N'PrincipalBfpEuAmount'      , N'Главница - Финансиране от ЕС.'
exec spDescColumn N'FIReimbursedAmounts', N'PrincipalBfpBgAmount'      , N'Главница - Финансиране от НФ.'
exec spDescColumn N'FIReimbursedAmounts', N'PrincipalBfpTotalAmount'   , N'Главница - Общо.'
exec spDescColumn N'FIReimbursedAmounts', N'InterestBfpEuAmount'       , N'Лихва - Финансиране от ЕС.'
exec spDescColumn N'FIReimbursedAmounts', N'InterestBfpBgAmount'       , N'Лихва - Финансиране от НФ.'
exec spDescColumn N'FIReimbursedAmounts', N'InterestBfpTotalAmount'    , N'Лихва - Общо.'

exec spDescColumn N'FIReimbursedAmounts', N'Comment'                   , N'Коментар.'
exec spDescColumn N'FIReimbursedAmounts', N'ShouldInfluencePaidAmounts', N'Отразява се на реално изплатените суми.'

exec spDescColumn N'FIReimbursedAmounts', N'IsActivated'            , N'Маркер дали записът е бил активиран.'
exec spDescColumn N'FIReimbursedAmounts', N'IsDeletedNote'          , N'Причина за изтриване.'
exec spDescColumn N'FIReimbursedAmounts', N'CreateDate'             , N'Дата на създаване на записа.'
exec spDescColumn N'FIReimbursedAmounts', N'ModifyDate'             , N'Дата на последно редактиране на записа.'
exec spDescColumn N'FIReimbursedAmounts', N'Version'                , N'Версия.'
GO