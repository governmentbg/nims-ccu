PRINT 'ContractIndicators'
GO

CREATE TABLE [dbo].[ContractIndicators] (
    [ContractIndicatorId]       INT                 NOT NULL IDENTITY,
    [ContractId]                INT                 NOT NULL,
    [IndicatorId]               INT                 NOT NULL,
    [Gid]                       UNIQUEIDENTIFIER    NOT NULL,
    [IsActive]                  BIT                 NOT NULL,

    [BaseTotalValue]            DECIMAL(15,3)      NULL,
    [BaseMenValue]              DECIMAL(15,3)      NULL,
    [BaseWomenValue]            DECIMAL(15,3)      NULL,
    [TargetTotalValue]          DECIMAL(15,3)      NULL,
    [TargetMenValue]            DECIMAL(15,3)      NULL,
    [TargetWomenValue]          DECIMAL(15,3)      NULL,
    [Description]               NVARCHAR(MAX)      NULL,
    [ProgrammePriorityId]       INT                NULL,
    [InvestmentPriorityId]      INT                NULL,
    [SpecificTargetId]          INT                NULL,
    [FinanceSource]             INT                NULL,

    CONSTRAINT [PK_ContractIndicators]                          PRIMARY KEY ([ContractIndicatorId]),
    CONSTRAINT [UQ_ContractIndicators_ContractId_Gid]           UNIQUE ([ContractId], [Gid]),
    CONSTRAINT [FK_ContractIndicators_Contracts]                FOREIGN KEY ([ContractId])               REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractIndicators_Countries_Indicators]     FOREIGN KEY ([IndicatorId])              REFERENCES [dbo].[Indicators] ([IndicatorId]),
    CONSTRAINT [FK_ContractIndicators_Countries_ProgrammePriorities]   FOREIGN KEY ([ProgrammePriorityId])        REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_ContractIndicators_Countries_InvestmentPriorities]  FOREIGN KEY ([InvestmentPriorityId])       REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_ContractIndicators_Countries_SpecificTargets]       FOREIGN KEY ([SpecificTargetId])           REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [CHK_ContractIndicators_FinanceSource]                  CHECK       ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12))
);
GO

exec spDescTable  N'ContractIndicators', N'Изпълнители към договор.'
exec spDescColumn N'ContractIndicators', N'ContractIndicatorId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractIndicators', N'ContractId'              , N'Идентификатор на договор.'
exec spDescColumn N'ContractIndicators', N'Gid'                     , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ContractIndicators', N'IsActive'                , N'Маркер за активност.'

exec spDescColumn N'ContractIndicators', N'BaseTotalValue'          , N'Базова стойност.'
exec spDescColumn N'ContractIndicators', N'BaseMenValue'            , N'Базова стойност - Мъже.'
exec spDescColumn N'ContractIndicators', N'BaseWomenValue'          , N'Базова стойност - Жени.'
exec spDescColumn N'ContractIndicators', N'TargetTotalValue'        , N'Целева стойност. (2023г.)'
exec spDescColumn N'ContractIndicators', N'TargetMenValue'          , N'Целева стойност (2023г.) - Мъже'
exec spDescColumn N'ContractIndicators', N'TargetWomenValue'        , N'Целева стойност (2023г.) - Жени'
exec spDescColumn N'ContractIndicators', N'Description'             , N'Обяснение за значението на индикатора.'
exec spDescColumn N'ContractIndicators', N'ProgrammePriorityId'     , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'ContractIndicators', N'InvestmentPriorityId'    , N'Идентификатор на инвестиционен приоритет.'
exec spDescColumn N'ContractIndicators', N'SpecificTargetId'        , N'Идентификатор на специфична цел.'
exec spDescColumn N'ContractIndicators', N'FinanceSource'           , N'Фонд.'
GO
