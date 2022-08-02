PRINT 'RemunerationAllowances'
GO

CREATE TABLE [dbo].[RemunerationAllowances] (
    [RemunerationAllowanceId]           INT           NOT NULL IDENTITY,
    [Name]                              NVARCHAR(MAX) NOT NULL,
    CONSTRAINT [PK_RemunerationAllowances]                     PRIMARY KEY ([RemunerationAllowanceId])
);
GO

exec spDescTable  N'RemunerationAllowances', N'Надбавки.'
exec spDescColumn N'RemunerationAllowances', N'RemunerationAllowanceId'         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'RemunerationAllowances', N'Name'                            , N'Наименование'
GO
