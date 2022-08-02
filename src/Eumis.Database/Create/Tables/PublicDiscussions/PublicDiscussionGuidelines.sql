PRINT 'PublicDiscussionGuidelines'
GO

CREATE TABLE [dbo].[PublicDiscussionGuidelines] (
    [PublicDiscussionGuidelineId]   INT                 NOT NULL IDENTITY,
    [Gid]                           UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [PublicDiscussionId]            INT                 NOT NULL,
    [FileName]                      NVARCHAR(MAX)       NULL,
    [Description]                   NVARCHAR(MAX)       NULL,
    [BlobKey]                       UNIQUEIDENTIFIER    NULL,
    [Status]                        INT                 NOT NULL,
    [StatusDate]                    DATETIME2           NOT NULL,

    CONSTRAINT [PK_PublicDiscussionGuidelines]                      PRIMARY KEY ([PublicDiscussionGuidelineId]),
    CONSTRAINT [FK_PublicDiscussionGuidelines_PublicDiscussions]    FOREIGN KEY ([PublicDiscussionId])      REFERENCES [dbo].[PublicDiscussions] ([PublicDiscussionId]),
    CONSTRAINT [FK_PublicDiscussionGuidelines_Blobs]                FOREIGN KEY ([BlobKey])                 REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_PublicDiscussionGuidelines_Status]              CHECK   ([Status] IN (1, 2, 3)),
);
GO

exec spDescTable  N'PublicDiscussionGuidelines', N'Насоки към обществено обсъждане на процедура.'
exec spDescColumn N'PublicDiscussionGuidelines', N'PublicDiscussionGuidelineId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'PublicDiscussionGuidelines', N'Gid'                         , N'Публичен системно генериран идентификатор.'
exec spDescColumn N'PublicDiscussionGuidelines', N'PublicDiscussionId'          , N'Идентификатор на обществено обсъждане на процедура.'
exec spDescColumn N'PublicDiscussionGuidelines', N'FileName'                    , N'Име на файл.'
exec spDescColumn N'PublicDiscussionGuidelines', N'Description'                 , N'Описание.'
exec spDescColumn N'PublicDiscussionGuidelines', N'BlobKey'                     , N'Идентификатор на файл.'
exec spDescColumn N'PublicDiscussionGuidelines', N'Status'                      , N'Статус: 1 - Неактивиран, 2 - Активиран, 3 - Неактивен.'
exec spDescColumn N'PublicDiscussionGuidelines', N'StatusDate'                  , N'Дата на промяна на статус.'
GO
