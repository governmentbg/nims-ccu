PRINT 'ContractDebts'
GO

CREATE TABLE [dbo].[ContractDebts] (
    [ContractDebtId]        INT             NOT NULL IDENTITY,
    [ContractId]            INT             NOT NULL,
    [ProgrammePriorityId]   INT             NOT NULL,
    [FinanceSource]         INT             NOT NULL,
    [DebtStartDate]         DATETIME2       NOT NULL,
    [InterestStartDate]     DATETIME2       NOT NULL,
    [Status]                INT             NOT NULL,
    [ExecutionStatus]       INT             NULL,
    [IrregularityId]        INT             NULL,
    [FinancialCorrectionId] INT             NULL,
    [RegNumber]             NVARCHAR(200)   NULL,
    [RegDate]               DATETIME2       NOT NULL,
    [Comment]               NVARCHAR(MAX)   NULL,

    [CertReportId]          INT             NULL,

    [IsDeletedNote]         NVARCHAR(MAX)   NULL,
    [CreateDate]            DATETIME2       NOT NULL,
    [ModifyDate]            DATETIME2       NOT NULL,
    [Version]               ROWVERSION      NOT NULL,

    CONSTRAINT [PK_ContractDebts]                      PRIMARY KEY ([ContractDebtId]),
    CONSTRAINT [FK_ContractDebts_Contracts]            FOREIGN KEY ([ContractId])              REFERENCES [dbo].[Contracts]            ([ContractId]),
    CONSTRAINT [FK_ContractDebts_ProgrammePriorities]  FOREIGN KEY ([ProgrammePriorityId])     REFERENCES [dbo].[MapNodes]             ([MapNodeId]),
    CONSTRAINT [FK_ContractDebts_Irregularities]       FOREIGN KEY ([IrregularityId])          REFERENCES [dbo].[Irregularities]       ([IrregularityId]),
    CONSTRAINT [FK_ContractDebts_FinancialCorrections] FOREIGN KEY ([FinancialCorrectionId])   REFERENCES [dbo].[FinancialCorrections] ([FinancialCorrectionId]),
    CONSTRAINT [FK_ContractDebts_CertReports]          FOREIGN KEY ([CertReportId])            REFERENCES [dbo].[CertReports]          ([CertReportId]),
    CONSTRAINT [CHK_ContractDebts_Status]              CHECK ([Status]          IN (1, 2, 3)),
    CONSTRAINT [CHK_ContractDebts_ExecutionStatus]     CHECK ([ExecutionStatus] IN (1, 2, 3, 4, 5, 6, 7, 8)),
    CONSTRAINT [CHK_ContractDebts_FinanceSource]       CHECK ([FinanceSource]   IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12))
);
GO

CREATE UNIQUE INDEX [UQ_ContractDebts_RegNumber]
ON [ContractDebts]([RegNumber])
WHERE [Status] <> 1;
GO

exec spDescTable  N'ContractDebts', N'Дълг към договор за БФП.'
exec spDescColumn N'ContractDebts', N'ContractDebtId'               , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractDebts', N'ContractId'                   , N'Идентификатор на договор.'
exec spDescColumn N'ContractDebts', N'ProgrammePriorityId'          , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'ContractDebts', N'FinanceSource'                , N'Фонд.'
exec spDescColumn N'ContractDebts', N'DebtStartDate'                , N'Дата, от която дългът е дължим.'
exec spDescColumn N'ContractDebts', N'InterestStartDate'            , N'Дата, от която се начислява лихва.'
exec spDescColumn N'ContractDebts', N'Status'                       , N'Статус на въвеждане: 1 - Нов; 2 - Въведен; 3 - Анулиран.'
exec spDescColumn N'ContractDebts', N'ExecutionStatus'              , N'Статус: 1 – за дълга тече 14 дневен срок за доброволно възстановяване, 2 – дългът е в процес на принудително възстановяване, 3 - дългът е в процес на принудително възстановяване от НАП, 4 – дългът е напълно възстановен, 5 – дългът е закрит, поради наличие на други основания, 6 – дългът е невъзстановим; 7 – дългът е разсрочен, 8 - друго.'
exec spDescColumn N'ContractDebts', N'IrregularityId'               , N'Идентификатор на нередност.'
exec spDescColumn N'ContractDebts', N'FinancialCorrectionId'        , N'Идентификатор на финансова корекция.'
exec spDescColumn N'ContractDebts', N'RegNumber'                    , N'Номер на дълга.'
exec spDescColumn N'ContractDebts', N'RegDate'                      , N'Дата на регистрация.'
exec spDescColumn N'ContractDebts', N'Comment'                      , N'Коментар.'

exec spDescColumn N'ContractDebts', N'IsDeletedNote'                , N'Причина за изтриване.'
exec spDescColumn N'ContractDebts', N'CreateDate'                   , N'Дата на създаване на записа.'
exec spDescColumn N'ContractDebts', N'ModifyDate'                   , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractDebts', N'Version'                      , N'Версия.'

GO
