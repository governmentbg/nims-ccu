PRINT 'ContractDebtInterests'
GO

CREATE TABLE [dbo].[ContractDebtInterests] (
    [ContractDebtInterestId]  INT             NOT NULL IDENTITY,
    [ContractDebtId]          INT             NOT NULL,
    [InterestSchemeId]        INT             NOT NULL,
    [OrderNum]                INT             NOT NULL,
    [DateFrom]                DATETIME2       NOT NULL,
    [DateTo]                  DATETIME2       NOT NULL,
    [EuInterestAmount]        MONEY           NOT NULL,
    [BgInterestAmount]        MONEY           NOT NULL,
    [TotalInterestAmount]     MONEY           NOT NULL,
    [EuAmount]                MONEY           NOT NULL,
    [BgAmount]                MONEY           NOT NULL,
    [TotalAmount]             MONEY           NOT NULL,

    CONSTRAINT [PK_ContractDebtInterests]                         PRIMARY KEY ([ContractDebtInterestId]),
    CONSTRAINT [FK_ContractDebtInterests_ContractDebts]           FOREIGN KEY ([ContractDebtId])         REFERENCES [dbo].[ContractDebts] ([ContractDebtId]),
    CONSTRAINT [FK_ContractDebtInterests_InterestSchemes]         FOREIGN KEY ([InterestSchemeId])  REFERENCES [dbo].[InterestSchemes] ([InterestSchemeId])
);
GO

exec spDescTable  N'ContractDebtInterests', N'Лихви към на дълг.'
exec spDescColumn N'ContractDebtInterests', N'ContractDebtInterestId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractDebtInterests', N'ContractDebtId'               , N'Идентификатор на дълг.'
exec spDescColumn N'ContractDebtInterests', N'OrderNum'                     , N'Пореден номер.'
exec spDescColumn N'ContractDebtInterests', N'DateFrom'                     , N'Дата от.'
exec spDescColumn N'ContractDebtInterests', N'DateTo'                       , N'Дата до.'
exec spDescColumn N'ContractDebtInterests', N'EuInterestAmount'             , N'Изчислена лихва на БФП от ЕС.'
exec spDescColumn N'ContractDebtInterests', N'BgInterestAmount'             , N'Изчислена лихва на БФП от НФ.'
exec spDescColumn N'ContractDebtInterests', N'TotalInterestAmount'          , N'Изчислена лихва на БФП общо.'
exec spDescColumn N'ContractDebtInterests', N'EuAmount'                     , N'Главница на БФП от ЕС.'
exec spDescColumn N'ContractDebtInterests', N'BgAmount'                     , N'Главница на БФП от НФ.'
exec spDescColumn N'ContractDebtInterests', N'TotalAmount'                  , N'Главница на БФП общо.'
GO
