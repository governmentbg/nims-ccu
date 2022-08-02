PRINT 'ProgrammeDeclarationItems'
GO

CREATE TABLE [dbo].[ProgrammeDeclarationItems] (
    [ProgrammeDeclarationItemId]    INT                 NOT NULL IDENTITY,
    [ProgrammeDeclarationId]        INT                 NOT NULL,
    [Gid]                           UNIQUEIDENTIFIER    NOT NULL,
    [OrderNum]                      INT                 NOT NULL,
    [Content]                       NVARCHAR(100)       NOT NULL,
    [IsActive]                      BIT                 NOT NULL,

    CONSTRAINT [PK_ProgrammeDeclarationItems]                                   PRIMARY KEY ([ProgrammeDeclarationItemId]),
    CONSTRAINT [FK_ProgrammeDeclarationItems_ProgrammeDeclarations]             FOREIGN KEY ([ProgrammeDeclarationId])             REFERENCES [dbo].[ProgrammeDeclarations] ([ProgrammeDeclarationId])
);
GO

exec spDescTable  N'ProgrammeDeclarationItems', N'Елементи от номенклатура към декларация към ОП.'
exec spDescColumn N'ProgrammeDeclarationItems', N'ProgrammeDeclarationItemId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProgrammeDeclarationItems', N'ProgrammeDeclarationId'            , N'Идентификатор на декларация към ОП.'
exec spDescColumn N'ProgrammeDeclarationItems', N'Gid'                               , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ProgrammeDeclarationItems', N'OrderNum'                          , N'Пореден номер.'
exec spDescColumn N'ProgrammeDeclarationItems', N'Content'                           , N'Съдържание.'
exec spDescColumn N'ProgrammeDeclarationItems', N'IsActive'                          , N'Маркер за активност.'
GO
