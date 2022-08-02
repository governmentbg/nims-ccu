PRINT 'EvalSessionDecisions'
GO

CREATE TABLE [dbo].[EvalSessionDecisions] (
    [EvalSessionDecisionId]       INT               NOT NULL IDENTITY,
    [EvalSessionId]               INT               NOT NULL,
    [ProjectId]                   INT               NOT NULL,
    [Admin]                       BIT               NOT NULL,
    [AdminDesc]                   NVARCHAR(MAX)     NULL,
    [Eligibility]                 BIT               NULL,
    [EligibilityDesc]             NVARCHAR(MAX)     NULL,
    [Finance]                     DECIMAL(15,3)     NULL,
    [FinanceDesc]                 NVARCHAR(MAX)     NULL,
    [Final]                       INT               NULL,
    [FinalDesc]                   NVARCHAR(MAX)     NULL,
    [HelpSum]                     MONEY             NULL,

    CONSTRAINT [PK_EvalSessionDecisions]               PRIMARY KEY ([EvalSessionDecisionId]),
    CONSTRAINT [FK_EvalSessionDecisions_EvalSessions]  FOREIGN KEY ([EvalSessionId])     REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionDecisions_Projects]      FOREIGN KEY ([ProjectId])         REFERENCES [dbo].[Projects] ([ProjectId])

);
GO

exec spDescTable  N'EvalSessionDecisions', N'Решения към оценителна сесия.'
exec spDescColumn N'EvalSessionDecisions', N'EvalSessionDecisionId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EvalSessionDecisions', N'EvalSessionId'         , N'Идентификатор на оценителна сесия.'
exec spDescColumn N'EvalSessionDecisions', N'ProjectId'             , N'Идентификатор на проектно предложние'
exec spDescColumn N'EvalSessionDecisions', N'Admin'                 , N'Административна оценка'
exec spDescColumn N'EvalSessionDecisions', N'AdminDesc'             , N'Административна оценка - коментар'
exec spDescColumn N'EvalSessionDecisions', N'Eligibility'           , N'Оценка за допустимост'
exec spDescColumn N'EvalSessionDecisions', N'EligibilityDesc'       , N'Оценка за допустимост - коментар'
exec spDescColumn N'EvalSessionDecisions', N'Finance'               , N'Техническа и финансова оценка'
exec spDescColumn N'EvalSessionDecisions', N'FinanceDesc'           , N'Техническа и финансова оценка - коментар'
exec spDescColumn N'EvalSessionDecisions', N'Final'                 , N'Обща оценка: 0 - Отхвърлено, 1 - Одобрено, 2 - Одобрено без финансиране, 3 - Оттеглено, 4 - Отказ на финансиране'
exec spDescColumn N'EvalSessionDecisions', N'FinalDesc'             , N'Обща оценка - коментар'
exec spDescColumn N'EvalSessionDecisions', N'HelpSum'               , N'Предложено финансиране'

GO

