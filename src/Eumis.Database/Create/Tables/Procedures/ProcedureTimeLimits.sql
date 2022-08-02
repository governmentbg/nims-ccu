PRINT 'ProcedureTimeLimits'
GO

CREATE TABLE [dbo].[ProcedureTimeLimits] (
    [ProcedureTimeLimitId]      INT             NOT NULL IDENTITY,
    [ProcedureId]               INT             NOT NULL,
    [EndDate]                   DATETIME2       NULL,
    [Notes]                     NVARCHAR(MAX)   NULL,
    CONSTRAINT [PK_ProcedureTimeLimits]             PRIMARY KEY ([ProcedureTimeLimitId]),
    CONSTRAINT [FK_ProcedureTimeLimits_Procedures]  FOREIGN KEY ([ProcedureId])  REFERENCES [dbo].[Procedures] ([ProcedureId]),
);
GO

exec spDescTable  N'ProcedureTimeLimits', N'Срокове за подаване на предложения по процедура.'
exec spDescColumn N'ProcedureTimeLimits', N'ProcedureTimeLimitId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureTimeLimits', N'ProcedureId'             , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureTimeLimits', N'EndDate'                 , N'Дата на изтичане на срока.'
exec spDescColumn N'ProcedureTimeLimits', N'Notes'                   , N'Информация.'
GO
