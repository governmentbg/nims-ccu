PRINT 'AllowanceRates'
GO

CREATE TABLE [dbo].[AllowanceRates] (
    [AllowanceRateId]      INT                 NOT NULL IDENTITY,
    [AllowanceId]          INT                 NOT NULL,
    [Date]                 DATETIME2           NOT NULL,
    [Rate]                 DECIMAL(15,3)       NOT NULL,

    CONSTRAINT [PK_AllowanceRates]               PRIMARY KEY ([AllowanceRateId]),
    CONSTRAINT [FK_AllowanceRates_Allowances]    FOREIGN KEY ([AllowanceId])      REFERENCES [dbo].[Allowances] ([AllowanceId])
);
GO

exec spDescTable  N'AllowanceRates', N'Проценти към надбавка.'
exec spDescColumn N'AllowanceRates', N'AllowanceRateId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'AllowanceRates', N'AllowanceId'        , N'Идентификатор на надбавка.'
exec spDescColumn N'AllowanceRates', N'Date'               , N'Дата.'
exec spDescColumn N'AllowanceRates', N'Rate'               , N'Процент.'
GO
