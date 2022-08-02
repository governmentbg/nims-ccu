PRINT 'RemunerationBasicRateValues'
GO

CREATE TABLE [dbo].[RemunerationBasicRateValues] (
    [RemunerationBasicRateValueId]              INT             NOT NULL IDENTITY,
    [RemunerationBasicRateId]                   INT             NOT NULL,
    [FromDate]                                  DATETIME2       NOT NULL,
    [Percent]                                   DECIMAL(15,3)   NOT NULL,
    CONSTRAINT [PK_RemunerationBasicRateValues]                         PRIMARY KEY ([RemunerationBasicRateValueId]),
    CONSTRAINT [FK_RemunerationBasicRateValues_RemunerationBasicRates]  FOREIGN KEY ([RemunerationBasicRateId]) REFERENCES [dbo].[RemunerationBasicRates] ([RemunerationBasicRateId])
);
GO

exec spDescTable  N'RemunerationBasicRateValues', N'Основен лихвен процент - проценти.'
exec spDescColumn N'RemunerationBasicRateValues', N'RemunerationBasicRateValueId'         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'RemunerationBasicRateValues', N'RemunerationBasicRateId'              , N'Идентификатор на основен лихвен процент.'
exec spDescColumn N'RemunerationBasicRateValues', N'FromDate'                             , N'Начална дата на валидност.'
exec spDescColumn N'RemunerationBasicRateValues', N'Percent'                              , N'Процент.'

GO
