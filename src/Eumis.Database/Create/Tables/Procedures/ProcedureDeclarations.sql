PRINT 'ProcedureDeclarations'
GO

CREATE TABLE [dbo].[ProcedureDeclarations] (
    [ProcedureDeclarationId]                INT                 NOT NULL IDENTITY,
    [Gid]                                   UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [ProcedureId]                           INT                 NOT NULL,
    [ProgrammeDeclarationId]                INT                 NULL,
    [IsActive]                              BIT                 NOT NULL,
    [IsActivated]                           BIT                 NOT NULL,
    [IsRequired]                            BIT                 NOT NULL,
    [Type]                                  INT                 NOT NULL,
    [CreateDate]                            DATETIME2           NOT NULL,
    [ModifyDate]                            DATETIME2           NOT NULL,
    [Version]                               ROWVERSION          NOT NULL,

    CONSTRAINT [PK_ProcedureDeclarations]                           PRIMARY KEY ([ProcedureDeclarationId]),
    CONSTRAINT [FK_ProcedureDeclarations_Procedures]                FOREIGN KEY ([ProcedureId])             REFERENCES [dbo].[Procedures]   ([ProcedureId]),
    CONSTRAINT [FK_ProcedureDeclarations_ProgrammeDeclarations]     FOREIGN KEY ([ProgrammeDeclarationId])  REFERENCES [dbo].[ProgrammeDeclarations]   ([ProgrammeDeclarationId])
);
GO

exec spDescTable  N'ProcedureDeclarations', N'Декларации към процедура.'
exec spDescColumn N'ProcedureDeclarations', N'ProcedureDeclarationId'          , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureDeclarations', N'Gid'                             , N'Публичен системно генериран идентификатор.'
exec spDescColumn N'ProcedureDeclarations', N'ProgrammeDeclarationId'          , N'Идентификатор на декларация към ОП.'
exec spDescColumn N'ProcedureDeclarations', N'ProcedureId'                     , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureDeclarations', N'IsActivated'                     , N'Маркер, дали документът е активиран.'
exec spDescColumn N'ProcedureDeclarations', N'IsActive'                        , N'Маркер, дали документът е активен.'
exec spDescColumn N'ProcedureDeclarations', N'IsRequired'                      , N'Маркер, дали документът е задължителен.'
exec spDescColumn N'ProcedureDeclarations', N'CreateDate'                      , N'Дата на създаване на записа.'
exec spDescColumn N'ProcedureDeclarations', N'ModifyDate'                      , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProcedureDeclarations', N'Version'                         , N'Версия.'

GO
