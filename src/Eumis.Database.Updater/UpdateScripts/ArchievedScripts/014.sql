-- b280586 Add Procedure questions

GO
CREATE TABLE [dbo].[ProcedureQuestions] (
    [ProcedureQuestionId]                       INT                 NOT NULL IDENTITY,
    [ProcedureId]                               INT                 NOT NULL,
    [CreatedByUserId]                           INT                 NOT NULL,
    [CreateDate]                                DATETIME2           NOT NULL,
    [BlobKey]                                   UNIQUEIDENTIFIER    NOT NULL,
    [IsActivated]                               BIT                 NOT NULL

    CONSTRAINT [PK_ProcedureQuestions]                          PRIMARY KEY ([ProcedureQuestionId]),
    CONSTRAINT [FK_ProcedureQuestions_Procedures]               FOREIGN KEY ([ProcedureId])     REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureQuestions_Blobs]                    FOREIGN KEY ([BlobKey])         REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'ProcedureQuestions', N'Въпроси и отговори.'
exec spDescColumn N'ProcedureQuestions', N'ProcedureQuestionId'               , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureQuestions', N'ProcedureId'                       , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureQuestions', N'CreatedByUserId'                   , N'Идентификатор на създаващия потребител.'
exec spDescColumn N'ProcedureQuestions', N'CreateDate'                        , N'Дата на създаване.'
exec spDescColumn N'ProcedureQuestions', N'BlobKey'                           , N'Идентификатор на файл.'

GO
