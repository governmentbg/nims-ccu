PRINT 'ProjectSpecFields'
GO

CREATE TABLE [dbo].[ProjectSpecFields] (
    [ProjectSpecFieldId]           INT             NOT NULL IDENTITY,
    [ProjectId]                    INT             NOT NULL,
    [ProcedureSpecFieldId]         INT             NOT NULL,
    [Description]                  NVARCHAR(MAX)   NOT NULL,


    CONSTRAINT [PK_ProjectSpecFields]                       PRIMARY KEY ([ProjectSpecFieldId]),
    CONSTRAINT [FK_ProjectSpecFields_Projects]              FOREIGN KEY ([ProjectId])              REFERENCES [dbo].[Projects] ([ProjectId]),
    CONSTRAINT [FK_ProjectSpecFields_ProcedureSpecFields]   FOREIGN KEY ([ProcedureSpecFieldId])   REFERENCES [dbo].[ProcedureSpecFields] ([ProcedureSpecFieldId])
);
GO

exec spDescTable  N'ProjectSpecFields', N'Допълнителна информация необходима за оценка на проектното предложение.'
exec spDescColumn N'ProjectSpecFields', N'ProjectSpecFieldId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectSpecFields', N'ProjectId'               , N'Идентификатор на проектно предложние'
exec spDescColumn N'ProjectSpecFields', N'ProcedureSpecFieldId'    , N'Идентификатор на Специфично поле по процедура (Темплейти към апликационна форма).'
exec spDescColumn N'ProjectSpecFields', N'Description'             , N'Описание от кандидата.'

GO
