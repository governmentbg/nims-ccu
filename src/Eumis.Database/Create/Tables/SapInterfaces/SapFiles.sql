PRINT 'SapFiles'
GO

CREATE TABLE [dbo].[SapFiles] (
    [SapFileId]        INT               NOT NULL IDENTITY,
    [SapSchemaId]      INT               NOT NULL,
    [Status]           INT               NOT NULL,
    [Type]             INT               NOT NULL,
    [FileKey]          UNIQUEIDENTIFIER  NOT NULL,
    [FileName]         NVARCHAR(500)     NOT NULL,
    [SapKey]           NVARCHAR(200)     NOT NULL,
    [SapDate]          DATETIME2         NOT NULL,
    [SapUser]          NVARCHAR(200)     NOT NULL,
    [Xml]              XML               NOT NULL,
    [CreatedByUserId]  INT               NOT NULL,
    [CreateDate]       DATETIME2         NOT NULL,
    [ModifyDate]       DATETIME2         NOT NULL,
    [Version]          ROWVERSION        NOT NULL

    CONSTRAINT [PK_SapFiles]            PRIMARY KEY ([SapFileId]),
    CONSTRAINT [FK_SapFiles_SapSchemas] FOREIGN KEY ([SapSchemaId])     REFERENCES [dbo].[SapSchemas] ([SapSchemaId]),
    CONSTRAINT [FK_SapFiles_Blobs]      FOREIGN KEY ([FileKey])         REFERENCES [dbo].[Blobs]      ([Key]),
    CONSTRAINT [FK_SapFiles_Users]      FOREIGN KEY ([CreatedByUserId]) REFERENCES [dbo].[Users]      ([UserId]),
    CONSTRAINT [CHK_SapFiles_Status]    CHECK       ([Status] IN (1, 2)),
    CONSTRAINT [CHK_SapFiles_Type]      CHECK       ([Type]   IN (1, 2))
);
GO

exec spDescTable  N'SapFiles', N'Файлове от SAP.'
exec spDescColumn N'SapFiles', N'SapFileId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'SapFiles', N'SapSchemaId'    , N'Идентификатор на схема.'
exec spDescColumn N'SapFiles', N'Status'         , N'Статус: 1 - Новосъздаден; 2 - Импортиран.'
exec spDescColumn N'SapFiles', N'Type'           , N'Тип: 1 - Банков трансфер; 2 - Зададен лимит.'
exec spDescColumn N'SapFiles', N'FileKey'        , N'Идентификатор на файл.'
exec spDescColumn N'SapFiles', N'FileName'       , N'Име на файла.'
exec spDescColumn N'SapFiles', N'SapKey'         , N'Идентификатор от файла.'
exec spDescColumn N'SapFiles', N'SapDate'        , N'Дата от файла.'
exec spDescColumn N'SapFiles', N'SapUser'        , N'Потребител от файла.'
exec spDescColumn N'SapFiles', N'Xml'            , N'Xml съдържание.'
exec spDescColumn N'SapFiles', N'CreatedByUserId', N'Създадено от.'
exec spDescColumn N'SapFiles', N'CreateDate'     , N'Дата на създаване на записа.'
exec spDescColumn N'SapFiles', N'ModifyDate'     , N'Дата на последно редактиране на записа.'
exec spDescColumn N'SapFiles', N'Version'        , N'Версия.'
GO
