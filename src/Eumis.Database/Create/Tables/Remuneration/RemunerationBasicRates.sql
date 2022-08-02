PRINT 'RemunerationBasicRates'
GO

CREATE TABLE [dbo].[RemunerationBasicRates] (
    [RemunerationBasicRateId]           INT           NOT NULL IDENTITY,
    [Name]                              NVARCHAR(MAX) NOT NULL,
    CONSTRAINT [PK_RemunerationBasicRates]                     PRIMARY KEY ([RemunerationBasicRateId])
);
GO

exec spDescTable  N'RemunerationBasicRates', N'Основни лихвени проценти.'
exec spDescColumn N'RemunerationBasicRates', N'RemunerationBasicRateId'         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'RemunerationBasicRates', N'Name'                            , N'Наименование'
GO
