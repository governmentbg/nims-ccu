PRINT 'InterestRates'
GO

CREATE TABLE [dbo].[InterestRates] (
    [InterestRateId]       INT                 NOT NULL IDENTITY,
    [BasicInterestRateId]  INT                 NOT NULL,
    [Date]                 DATETIME2           NOT NULL,
    [Rate]                 DECIMAL(15,3)       NOT NULL,

    CONSTRAINT [PK_InterestRates]                       PRIMARY KEY ([InterestRateId]),
    CONSTRAINT [FK_InterestRates_BasicInterestRates]    FOREIGN KEY ([BasicInterestRateId])   REFERENCES [dbo].[BasicInterestRates] ([BasicInterestRateId])
);
GO

exec spDescTable  N'InterestRates', N'Проценти към основен лихвен процент.'
exec spDescColumn N'InterestRates', N'InterestRateId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'InterestRates', N'BasicInterestRateId', N'Идентификатор на основен лихвен процент.'
exec spDescColumn N'InterestRates', N'Date'               , N'Дата.'
exec spDescColumn N'InterestRates', N'Rate'               , N'Процент.'
GO
