PRINT 'MapNodeDocuments'
GO

CREATE TABLE [dbo].[MapNodeDocuments] (
    
    [MapNodeDocumentId]                       INT                 NOT NULL IDENTITY,
    [MapNodeId]                               INT                 NOT NULL,
    [Name]                                    NVARCHAR(200)       NOT NULL,
    [Description]                             NVARCHAR(MAX)       NULL,
    [BlobKey]                                 UNIQUEIDENTIFIER    NULL

    CONSTRAINT [PK_MapNodeDocuments]              PRIMARY KEY       ([MapNodeDocumentId]),
    CONSTRAINT [FK_MapNodeDocuments_MapNodes]     FOREIGN KEY       ([MapNodeId])       REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_MapNodeDocuments_Blobs]        FOREIGN KEY       ([BlobKey])         REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'MapNodeDocuments', N'Документи към елемент на оперативна карта.'
exec spDescColumn N'MapNodeDocuments', N'MapNodeDocumentId'                 , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'MapNodeDocuments', N'MapNodeId'                         , N'Идентификатор на елемент на оперативна карта.'
exec spDescColumn N'MapNodeDocuments', N'Name'                              , N'Наименование.'
exec spDescColumn N'MapNodeDocuments', N'Description'                       , N'Описание.'
exec spDescColumn N'MapNodeDocuments', N'BlobKey'                           , N'Идентификатор на файл.'

GO


