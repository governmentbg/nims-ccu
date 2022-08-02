PRINT 'ReimbursedAmounts'
GO

CREATE TABLE [dbo].[ReimbursedAmounts] (
    [ReimbursedAmountId]          INT           NOT NULL,
    [SapFileId]                   INT           NULL,
    [ProgrammeId]                 INT           NOT NULL,
    [ProgrammePriorityId]         INT           NULL,
    [FinanceSource]               INT           NULL,
    [ContractId]                  INT           NOT NULL,
    [ContractDebtId]              INT           NULL,
    [Discriminator]               INT           NOT NULL,
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

    CONSTRAINT [PK_ReimbursedAmounts]                        PRIMARY KEY ([ReimbursedAmountId]),
    CONSTRAINT [FK_ReimbursedAmounts_SapFiles]               FOREIGN KEY ([SapFileId])           REFERENCES [dbo].[SapFiles]      ([SapFileId]),
    CONSTRAINT [FK_ReimbursedAmounts_Programmes]             FOREIGN KEY ([ProgrammeId])         REFERENCES [dbo].[MapNodes]      ([MapNodeId]),
    CONSTRAINT [FK_ReimbursedAmounts_ProgrammePriorities]    FOREIGN KEY ([ProgrammePriorityId]) REFERENCES [dbo].[MapNodes]      ([MapNodeId]),
    CONSTRAINT [FK_ReimbursedAmounts_Contracts]              FOREIGN KEY ([ContractId])          REFERENCES [dbo].[Contracts]     ([ContractId]),
    CONSTRAINT [FK_ReimbursedAmounts_ContractDebts]          FOREIGN KEY ([ContractDebtId])      REFERENCES [dbo].[ContractDebts] ([ContractDebtId]),
    CONSTRAINT [FK_ReimbursedAmounts_CertReports]            FOREIGN KEY ([CertReportId])        REFERENCES [dbo].[CertReports]   ([CertReportId]),
    CONSTRAINT [CHK_ReimbursedAmounts_Discriminator]         CHECK       ([Discriminator]  IN (1, 2)),
    CONSTRAINT [CHK_ReimbursedAmounts_Status]                CHECK       ([Status]         IN (1, 2, 3)),
    CONSTRAINT [CHK_ReimbursedAmounts_Type]                  CHECK       ([Type]           IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_ReimbursedAmounts_Reimbursement]         CHECK       ([Reimbursement]  IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_ReimbursedAmounts_FinanceSource]         CHECK       ([FinanceSource]  IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)),
    CONSTRAINT [CHK_ReimbursedAmounts_DebtDiscriminator]     CHECK       ([Discriminator] = 1 OR [ContractDebtId] IS NOT NULL)
);
GO

CREATE UNIQUE INDEX [UQ_ReimbursedAmounts_RegNumber]
ON [ReimbursedAmounts]([RegNumber])
WHERE [RegNumber] IS NOT NULL;

exec spDescTable  N'ReimbursedAmounts', N'Възстановени суми.'
exec spDescColumn N'ReimbursedAmounts', N'ReimbursedAmountId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ReimbursedAmounts', N'SapFileId'                 , N'Идентификатор на файл от SAP.'
exec spDescColumn N'ReimbursedAmounts', N'ProgrammeId'               , N'Идентификатор на програма.'
exec spDescColumn N'ReimbursedAmounts', N'ProgrammePriorityId'       , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'ReimbursedAmounts', N'FinanceSource'             , N'Фонд.'
exec spDescColumn N'ReimbursedAmounts', N'ContractId'                , N'Идентификатор на договор.'
exec spDescColumn N'ReimbursedAmounts', N'ContractDebtId'            , N'Идентификатор на дълг към договор за БФП.'
exec spDescColumn N'ReimbursedAmounts', N'Discriminator'             , N'Вид: 1 - сума към договор; 2 - сума към дълг.'
exec spDescColumn N'ReimbursedAmounts', N'Status'                    , N'Статус: 1 - Чернова;  2 - Въведена; 3 - Анулирана.'
exec spDescColumn N'ReimbursedAmounts', N'ReimbursementDate'         , N'Дата на възстановяване.'
exec spDescColumn N'ReimbursedAmounts', N'Type'                      , N'Вид: 1 - Доброволно възстановяване, 2 - Събиране чрез НАП, 3 - Събиране чрез активиране на обезпечения (банкова гаранция), 4 - Друго.'
exec spDescColumn N'ReimbursedAmounts', N'Reimbursement'             , N'Начин на възстановяване: 1 – По банков път, 2 – Чрез прихващане, 3 - Отписани.'
exec spDescColumn N'ReimbursedAmounts', N'RegNumber'                 , N'Регистрационен номер.'

exec spDescColumn N'ReimbursedAmounts', N'PrincipalBfpEuAmount'      , N'Главница - Финансиране от ЕС.'
exec spDescColumn N'ReimbursedAmounts', N'PrincipalBfpBgAmount'      , N'Главница - Финансиране от НФ.'
exec spDescColumn N'ReimbursedAmounts', N'PrincipalBfpTotalAmount'   , N'Главница - Общо.'
exec spDescColumn N'ReimbursedAmounts', N'InterestBfpEuAmount'       , N'Лихва - Финансиране от ЕС.'
exec spDescColumn N'ReimbursedAmounts', N'InterestBfpBgAmount'       , N'Лихва - Финансиране от НФ.'
exec spDescColumn N'ReimbursedAmounts', N'InterestBfpTotalAmount'    , N'Лихва - Общо.'

exec spDescColumn N'ReimbursedAmounts', N'Comment'                   , N'Коментар.'
exec spDescColumn N'ReimbursedAmounts', N'ShouldInfluencePaidAmounts', N'Отразява се на реално изплатените суми.'

exec spDescColumn N'ReimbursedAmounts', N'IsActivated'            , N'Маркер дали записът е бил активиран.'
exec spDescColumn N'ReimbursedAmounts', N'IsDeletedNote'          , N'Причина за изтриване.'
exec spDescColumn N'ReimbursedAmounts', N'CreateDate'             , N'Дата на създаване на записа.'
exec spDescColumn N'ReimbursedAmounts', N'ModifyDate'             , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ReimbursedAmounts', N'Version'                , N'Версия.'
GO