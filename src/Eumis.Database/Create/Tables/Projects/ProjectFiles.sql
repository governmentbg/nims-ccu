PRINT 'ProjectFiles'
GO

CREATE TABLE [dbo].[ProjectFiles] (
    [ProjectFileId]        INT             NOT NULL IDENTITY,
    [ProjectVersionXmlId]  INT             NOT NULL,
    [File]                 VARBINARY(MAX)  NOT NULL,
    [FileName]             NVARCHAR(200)   NOT NULL,
    [CreateDate]           DATETIME2       NOT NULL,
    [ModifyDate]           DATETIME2       NOT NULL,
    [Version]              ROWVERSION      NOT NULL

    CONSTRAINT [PK_ProjectFiles]                     PRIMARY KEY ([ProjectFileId]),
    CONSTRAINT [FK_ProjectFiles_ProjectVersionXmls]  FOREIGN KEY ([ProjectVersionXmlId])   REFERENCES [dbo].[ProjectVersionXmls] ([ProjectVersionXmlId])
);
GO

exec spDescTable  N'ProjectFiles', N'Файл на проектно предложение.'
exec spDescColumn N'ProjectFiles', N'ProjectFileId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectFiles', N'ProjectVersionXmlId'  , N'Идентификатор на версия на проектно предложние.'
exec spDescColumn N'ProjectFiles', N'File'                 , N'Файл.'
exec spDescColumn N'ProjectFiles', N'FileName'             , N'Име на файл.'
exec spDescColumn N'ProjectFiles', N'CreateDate'           , N'Дата на създаване на записа.'
exec spDescColumn N'ProjectFiles', N'ModifyDate'           , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProjectFiles', N'Version'              , N'Версия.'

GO
