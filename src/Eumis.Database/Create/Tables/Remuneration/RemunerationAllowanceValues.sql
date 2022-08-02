PRINT 'RemunerationAllowanceValues'
GO

CREATE TABLE [dbo].[RemunerationAllowanceValues] (
    [RemunerationAllowanceValueId]              INT             NOT NULL IDENTITY,
    [RemunerationAllowanceId]                   INT             NOT NULL,
    [FromDate]                                  DATETIME2       NOT NULL,
    [Percent]                                   DECIMAL(15,3)   NOT NULL,
    CONSTRAINT [PK_RemunerationAllowanceValues]                         PRIMARY KEY ([RemunerationAllowanceValueId]),
    CONSTRAINT [FK_RemunerationAllowanceValues_RemunerationAllowances]  FOREIGN KEY ([RemunerationAllowanceId]) REFERENCES [dbo].[RemunerationAllowances] ([RemunerationAllowanceId])
);
GO

exec spDescTable  N'RemunerationAllowanceValues', N'Надбавка - проценти.'
exec spDescColumn N'RemunerationAllowanceValues', N'RemunerationAllowanceValueId'         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'RemunerationAllowanceValues', N'RemunerationAllowanceId'              , N'Идентификатор на надбавка.'
exec spDescColumn N'RemunerationAllowanceValues', N'FromDate'                             , N'Начална дата на валидност.'
exec spDescColumn N'RemunerationAllowanceValues', N'Percent'                              , N'Процент.'

GO
