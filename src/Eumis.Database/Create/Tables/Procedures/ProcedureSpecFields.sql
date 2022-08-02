PRINT 'ProcedureSpecFields'
GO

CREATE TABLE [dbo].[ProcedureSpecFields] (
    [ProcedureSpecFieldId]      INT                 NOT NULL IDENTITY,
    [Gid]                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [ProcedureId]               INT                 NOT NULL,
    [Title]                     NVARCHAR(MAX)       NOT NULL,
    [TitleAlt]                  NVARCHAR(MAX)       NULL,
    [Description]               NVARCHAR(MAX)       NULL,
    [DescriptionAlt]            NVARCHAR(MAX)       NULL,
    [IsRequired]                BIT                 NOT NULL,
    [MaxLength]                 INT                 NOT NULL,
    [IsActivated]               BIT                 NOT NULL,
    [IsActive]                  BIT                 NOT NULL,

    CONSTRAINT [PK_ProcedureSpecFields]            PRIMARY KEY ([ProcedureSpecFieldId]),
    CONSTRAINT [FK_ProcedureSpecFields_Procedures] FOREIGN KEY ([ProcedureId]) REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [CHK_ProcedureSpecFields_MaxLength] CHECK ([MaxLength] IN (1000, 3000, 5000, 10000, 0)),
);
GO

exec spDescTable  N'ProcedureSpecFields', N'Специфични полета по процедура (Темплейти към апликационна форма).'
exec spDescColumn N'ProcedureSpecFields', N'ProcedureSpecFieldId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureSpecFields', N'Gid'                        , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ProcedureSpecFields', N'ProcedureId'                , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureSpecFields', N'Title'                      , N'Етикет на полето(темплейта) при показване във форма.'
exec spDescColumn N'ProcedureSpecFields', N'TitleAlt'                   , N'Етикет на полето(темплейта) на друг език.'
exec spDescColumn N'ProcedureSpecFields', N'Description'                , N'Описание'
exec spDescColumn N'ProcedureSpecFields', N'DescriptionAlt'             , N'Описание на друг език'
exec spDescColumn N'ProcedureSpecFields', N'IsRequired'                 , N'Маркер, дали полето е задължително'
exec spDescColumn N'ProcedureSpecFields', N'MaxLength'                  , N'Максимална допустима дължина на полето. Стойност 0 определя записа като поле за попълване на IBAN'
GO
