PRINT 'ProcedureEvalTableXmls'
GO

CREATE TABLE [dbo].[ProcedureEvalTableXmls] (
    [ProcedureEvalTableXmlId]           INT                 NOT NULL IDENTITY,
    [Gid]                               UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [ProcedureId]                       INT                 NOT NULL,
    [ProcedureEvalTableId]              INT                 NOT NULL,
    [Xml]                               XML                 NOT NULL,
    [Hash]                              NVARCHAR(10)        NOT NULL UNIQUE,
    [CreateDate]                        DATETIME2           NOT NULL,
    [ModifyDate]                        DATETIME2           NOT NULL,
    [Version]                           ROWVERSION          NOT NULL,

    CONSTRAINT [PK_ProcedureEvalTableXmls]                          PRIMARY KEY ([ProcedureEvalTableXmlId]),
    CONSTRAINT [FK_ProcedureEvalTableXmls_Procedures]               FOREIGN KEY ([ProcedureId])                REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureEvalTableXmls_ProcedureEvalTables]      FOREIGN KEY ([ProcedureEvalTableId])       REFERENCES [dbo].[ProcedureEvalTables] ([ProcedureEvalTableId])
);
GO

exec spDescTable  N'ProcedureEvalTableXmls', N'Xml за оценителна таблица.'
exec spDescColumn N'ProcedureEvalTableXmls', N'ProcedureEvalTableXmlId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureEvalTableXmls', N'Gid'                         , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ProcedureEvalTableXmls', N'ProcedureId'                 , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureEvalTableXmls', N'ProcedureEvalTableId'        , N'Идентификатор на оценителна таблица.'
exec spDescColumn N'ProcedureEvalTableXmls', N'Xml'                         , N'Xml съдържание.'
exec spDescColumn N'ProcedureEvalTableXmls', N'CreateDate'                  , N'Дата на създаване на записа.'
exec spDescColumn N'ProcedureEvalTableXmls', N'ModifyDate'                  , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProcedureEvalTableXmls', N'Version'                     , N'Версия.'

GO
