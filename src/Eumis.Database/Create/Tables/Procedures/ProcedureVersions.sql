PRINT 'ProcedureVersions'
GO

CREATE TABLE [dbo].[ProcedureVersions] (
    [ProcedureId]               INT                 NOT NULL,
    [ProcedureVersionId]        INT                 NOT NULL,
    [ProcedureGid]              UNIQUEIDENTIFIER    NOT NULL,
    [ProcedureText]             NVARCHAR(MAX)       NOT NULL,
    [IsActive]                  BIT                 NOT NULL,
    [CreateDate]                DATETIME2           NOT NULL,
    [ModifyDate]                DATETIME2           NOT NULL,
    [Version]                   ROWVERSION          NOT NULL,

    CONSTRAINT [PK_ProcedureVersions]                            PRIMARY KEY ([ProcedureId], [ProcedureVersionId]),
    CONSTRAINT [FK_ProcedureVersions_Procedures]                 FOREIGN KEY ([ProcedureId])    REFERENCES [dbo].[Procedures] ([ProcedureId])
);
GO

exec spDescTable  N'ProcedureVersions', N'Версия на процедура по инвестиционен приоритет.'
exec spDescColumn N'ProcedureVersions', N'ProcedureVersionId'      , N'Идентификатор на версия на процедура.'
exec spDescColumn N'ProcedureVersions', N'ProcedureId'             , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureVersions', N'ProcedureGid'            , N'Публичен идентификатор на процедура.'
exec spDescColumn N'ProcedureVersions', N'ProcedureText'           , N'Json на процедурата.'
exec spDescColumn N'ProcedureVersions', N'IsActive'                , N'Маркер за активност.'
exec spDescColumn N'ProcedureVersions', N'CreateDate'              , N'Дата на създаване на записа.'
exec spDescColumn N'ProcedureVersions', N'ModifyDate'              , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProcedureVersions', N'Version'                 , N'Версия.'
GO
