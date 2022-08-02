PRINT 'EvalSessionCorrespondence'
GO

CREATE TABLE [dbo].[EvalSessionCorrespondence] (
    [EvalSessionCorrespondenceId]       INT             NOT NULL IDENTITY,
    [EvalSessionId]                     INT             NOT NULL,
    [ProjectId]                         INT             NOT NULL,
    [Type]                              INT             NOT NULL,
    [Content]                           NVARCHAR(MAX)   NOT NULL,

    CONSTRAINT [PK_EvalSessionCorrespondence]               PRIMARY KEY ([EvalSessionCorrespondenceId]),
    CONSTRAINT [FK_EvalSessionCorrespondence_EvalSessions]  FOREIGN KEY ([EvalSessionId])     REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionCorrespondence_Projects]      FOREIGN KEY ([ProjectId])         REFERENCES [dbo].[Projects] ([ProjectId])
    
);
GO

exec spDescTable  N'EvalSessionCorrespondence', N'Кореспонденция към оценителна сесия.'
exec spDescColumn N'EvalSessionCorrespondence', N'EvalSessionCorrespondenceId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EvalSessionCorrespondence', N'EvalSessionId'                    , N'Идентификатор на оценителна сесия.'
exec spDescColumn N'EvalSessionCorrespondence', N'ProjectId'                        , N'Идентификатор на проектно предложние'
exec spDescColumn N'EvalSessionCorrespondence', N'Type'                             , N'Тип : 1-изходяща,2-входяща'

GO

