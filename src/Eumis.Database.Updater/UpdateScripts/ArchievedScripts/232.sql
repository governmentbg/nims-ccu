GO

CREATE TABLE [dbo].[Declarations] (
    [DeclarationId]        INT                 NOT NULL IDENTITY,
    [Status]               INT                 NOT NULL,
    [Name]                 NVARCHAR(200)       NOT NULL,
    [NameAlt]              NVARCHAR(200)       NOT NULL,
    [Content]              NVARCHAR(MAX)       NOT NULL,
    [ContentAlt]           NVARCHAR(MAX)       NOT NULL,
    [ActivationDate]       DATETIME2           NULL,

    [CreatedByUserId]      INT                 NOT NULL,
    [CreateDate]           DATETIME2           NOT NULL,
    [ModifyDate]           DATETIME2           NOT NULL,
    [Version]              ROWVERSION          NOT NULL,

    CONSTRAINT [PK_Declarations]            PRIMARY KEY ([DeclarationId]),
    CONSTRAINT [FK_Declarations_Users]      FOREIGN KEY ([CreatedByUserId])         REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_Declarations_Status]    CHECK       ([Status] IN (1, 2, 3))
);
GO

GO

CREATE TABLE [dbo].[DeclarationFiles] (
    [DeclarationFileId]    INT                 NOT NULL IDENTITY,
    [DeclarationId]        INT                 NOT NULL,
    [BlobKey]              UNIQUEIDENTIFIER    NOT NULL,
    [Name]                 NVARCHAR(200)       NOT NULL,
    [Description]          NVARCHAR(200)       NOT NULL,

    CONSTRAINT [PK_DeclarationFiles]                PRIMARY KEY ([DeclarationFileId]),
    CONSTRAINT [FK_DeclarationFiles_Declarations]   FOREIGN KEY ([DeclarationId])         REFERENCES [dbo].[Declarations] ([DeclarationId]),
    CONSTRAINT [FK_DeclarationFiles_Blobs]          FOREIGN KEY ([BlobKey])               REFERENCES [dbo].[Blobs] ([Key])
);
GO

GO

CREATE TABLE [dbo].[UserDeclarations] (
    [UserId]               INT                 NOT NULL,
    [DeclarationId]        INT                 NOT NULL,
    [AcceptDate]           DATETIME2           NOT NULL,

    CONSTRAINT [PK_UserDeclarations]                    PRIMARY KEY ([UserId], [DeclarationId]),
    CONSTRAINT [FK_UserDeclarations_Users]              FOREIGN KEY ([UserId])           REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_UserDeclarations_Declartions]        FOREIGN KEY ([DeclarationId])    REFERENCES [dbo].[Declarations] ([DeclarationId])
);
GO
