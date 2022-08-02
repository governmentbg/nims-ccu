PRINT 'EvalSessionDistributionProjects'
GO

CREATE TABLE [dbo].[EvalSessionDistributionProjects] (
    [EvalSessionId]                 INT             NOT NULL,
    [EvalSessionDistributionId]     INT             NOT NULL,
    [ProjectId]                     INT             NOT NULL,
    [IsDeleted]                     BIT             NOT NULL,
    [IsDeletedNote]                 NVARCHAR(MAX)   NULL,

    CONSTRAINT [PK_EvalSessionDistributionProjects]                              PRIMARY KEY ([EvalSessionId], [EvalSessionDistributionId], [ProjectId]),
    CONSTRAINT [FK_EvalSessionDistributionProjects_EvalSessionDistributions]     FOREIGN KEY ([EvalSessionId], [EvalSessionDistributionId])   REFERENCES [dbo].[EvalSessionDistributions] ([EvalSessionId], [EvalSessionDistributionId]),
    CONSTRAINT [FK_EvalSessionDistributionProjects_Projects]                     FOREIGN KEY ([EvalSessionId], [ProjectId])                   REFERENCES [dbo].[EvalSessionProjects] ([EvalSessionId], [ProjectId])
);
GO

exec spDescTable  N'EvalSessionDistributionProjects', N'Проектни предложения към разпределение.'
exec spDescColumn N'EvalSessionDistributionProjects', N'EvalSessionId'                      , N'Идентификатор на оценителна сесия.'
exec spDescColumn N'EvalSessionDistributionProjects', N'EvalSessionDistributionId'          , N'Идентификатор на разпределение.'
exec spDescColumn N'EvalSessionDistributionProjects', N'ProjectId'                          , N'Идентификатор на проектно предложние към оценителна сесия.'
exec spDescColumn N'EvalSessionDistributionProjects', N'IsDeleted'                          , N'Маркер, дали проектното предложение е изключено от разпределението.'
exec spDescColumn N'EvalSessionDistributionProjects', N'IsDeletedNote'                      , N'Причина за изключване.'

GO

