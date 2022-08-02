PRINT 'RemunerationSchemes'
GO

CREATE TABLE [dbo].[RemunerationSchemes] (
    [RemunerationSchemeId]          INT           NOT NULL IDENTITY,
    [Name]                          NVARCHAR(MAX) NOT NULL,
    [RemunerationBasicRateId]       INT           NOT NULL,
    [RemunerationAllowanceId]       INT           NOT NULL,
    [YearBase]                      INT           NOT NULL,
    CONSTRAINT [PK_RemunerationSchemes]                     PRIMARY KEY ([RemunerationSchemeId]),
    CONSTRAINT [FK_RemunerationSchemes_RemunerationAllowances]  FOREIGN KEY ([RemunerationAllowanceId]) REFERENCES [dbo].[RemunerationAllowances] ([RemunerationAllowanceId]),
    CONSTRAINT [FK_RemunerationSchemes_RemunerationBasicRates]  FOREIGN KEY ([RemunerationBasicRateId]) REFERENCES [dbo].[RemunerationBasicRates] ([RemunerationBasicRateId])
);
GO

exec spDescTable  N'RemunerationSchemes', N'Схеми за олихвяване.'
exec spDescColumn N'RemunerationSchemes', N'RemunerationSchemeId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'RemunerationSchemes', N'Name'                       , N'Наименование'
exec spDescColumn N'RemunerationSchemes', N'RemunerationBasicRateId'    , N'Идентификатор на основен лихвен процент.'
exec spDescColumn N'RemunerationSchemes', N'RemunerationAllowanceId'    , N'Идентификатор на надбавка.'
exec spDescColumn N'RemunerationSchemes', N'YearBase'                   , N'Годишна база.'
GO
