PRINT 'EvalSessionDistributionUsers'
GO

CREATE TABLE [dbo].[EvalSessionDistributionUsers] (
    [EvalSessionId]                 INT             NOT NULL,
    [EvalSessionDistributionId]     INT             NOT NULL,
    [EvalSessionUserId]             INT             NOT NULL,
    [IsDeleted]                     BIT             NOT NULL,
    [IsDeletedNote]                 NVARCHAR(MAX)   NULL,

    CONSTRAINT [PK_EvalSessionDistributionUsers]                              PRIMARY KEY ([EvalSessionId], [EvalSessionDistributionId], [EvalSessionUserId]),
    CONSTRAINT [FK_EvalSessionDistributionUsers_EvalSessionDistributions]     FOREIGN KEY ([EvalSessionId], [EvalSessionDistributionId]) REFERENCES [dbo].[EvalSessionDistributions] ([EvalSessionId], [EvalSessionDistributionId]),
    CONSTRAINT [FK_EvalSessionDistributionUsers_Users]                        FOREIGN KEY ([EvalSessionUserId])                          REFERENCES [dbo].[EvalSessionUsers] ([EvalSessionUserId])
);
GO

exec spDescTable  N'EvalSessionDistributionUsers', N'Потребители към разпределение.'
exec spDescColumn N'EvalSessionDistributionUsers', N'EvalSessionId'                      , N'Идентификатор на оценителна сесия.'
exec spDescColumn N'EvalSessionDistributionUsers', N'EvalSessionDistributionId'          , N'Идентификатор на разпределение.'
exec spDescColumn N'EvalSessionDistributionUsers', N'EvalSessionUserId'                  , N'Идентификатор на потребител към оценителна сесия.'
exec spDescColumn N'EvalSessionDistributionUsers', N'IsDeleted'                          , N'Маркер, дали потребителят е изключен от разпределението.'
exec spDescColumn N'EvalSessionDistributionUsers', N'IsDeletedNote'                      , N'Причина за изключване.'

GO

