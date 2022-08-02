PRINT 'ProgrammeCheckListVersionXmlFiles'
GO

CREATE TABLE [dbo].[ProgrammeCheckListVersionXmlFiles] (
    [ProgrammeCheckListVersionXmlFileId]       INT                 NOT NULL IDENTITY,
    [ProgrammeCheckListVersionXmlId]           INT                 NOT NULL,
    [BlobKey]                                  UNIQUEIDENTIFIER    NOT NULL,
    [Name]                                     NVARCHAR(200)       NOT NULL,
    [Description]                              NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ProgrammeCheckListVersionXmlFiles]                                 PRIMARY KEY ([ProgrammeCheckListVersionXmlFileId]),
    CONSTRAINT [FK_ProgrammeCheckListVersionXmlFiles_ProgrammeCheckListVersionXmls]   FOREIGN KEY ([ProgrammeCheckListVersionXmlId])    REFERENCES [dbo].[ProgrammeCheckListVersionXmls] ([ProgrammeCheckListVersionXmlId]),
    CONSTRAINT [FK_ProgrammeCheckListVersionXmlFiles_Blobs]                           FOREIGN KEY ([BlobKey])                           REFERENCES [dbo].[Blobs] ([Key])
);
GO

exec spDescTable  N'ProgrammeCheckListVersionXmlFiles', N'Файлове към xml за контролен лист.'
exec spDescColumn N'ProgrammeCheckListVersionXmlFiles', N'ProgrammeCheckListVersionXmlFileId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProgrammeCheckListVersionXmlFiles', N'ProgrammeCheckListVersionXmlId'           , N'Идентификатор на xml за контролен лист.'
exec spDescColumn N'ProgrammeCheckListVersionXmlFiles', N'BlobKey'                                  , N'Идентификатор на файл.'
exec spDescColumn N'ProgrammeCheckListVersionXmlFiles', N'Name'                                     , N'Име на файл.'
exec spDescColumn N'ProgrammeCheckListVersionXmlFiles', N'Description'                              , N'Описание.'
GO
