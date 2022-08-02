PRINT 'News'
GO

CREATE TABLE [dbo].[News] (
    [NewsId]               INT                 NOT NULL IDENTITY,
    [Type]                 INT                 NOT NULL,
    [Status]               INT                 NOT NULL,
    [DateFrom]             DATETIME2           NULL,
    [DateTo]               DATETIME2           NULL,
    [Title]                NVARCHAR(200)       NOT NULL,
    [TitleAlt]             NVARCHAR(200)       NULL,
    [Content]              NVARCHAR(MAX)       NOT NULL,
    [ContentAlt]           NVARCHAR(MAX)       NULL,
    [Author]               NVARCHAR(200)       NULL,
    [AuthorAlt]            NVARCHAR(200)       NULL,
    [EmailNotification]    BIT                 NOT NULL,

    [CreatedByUserId]      INT                 NOT NULL,
    [CreateDate]           DATETIME2           NOT NULL,
    [ModifyDate]           DATETIME2           NOT NULL,
    [Version]              ROWVERSION          NOT NULL

    CONSTRAINT [PK_News]            PRIMARY KEY ([NewsId]),
    CONSTRAINT [FK_News_Users]      FOREIGN KEY ([CreatedByUserId])         REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_News_Type]      CHECK       ([Type] IN (1, 2)),
    CONSTRAINT [CHK_News_Status]    CHECK       ([Status] IN (1, 2, 3))
);
GO

exec spDescTable  N'News', N'Новини.'
exec spDescColumn N'News', N'NewsId'           , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'News', N'Type'             , N'Тип: 1 - Вътрешна с-ма; 2 - Портал.'
exec spDescColumn N'News', N'Status'           , N'Статус: 1 - Чернова; 2 - Публикувана; 3 - Архивирана.'
exec spDescColumn N'News', N'DateFrom'         , N'Дата от.'
exec spDescColumn N'News', N'DateTo'           , N'Дата до.'
exec spDescColumn N'News', N'Title'            , N'Заглавие.'
exec spDescColumn N'News', N'TitleAlt'         , N'Заглавие на английски.'
exec spDescColumn N'News', N'Content'          , N'Съдържание.'
exec spDescColumn N'News', N'ContentAlt'       , N'Съдържание на английски.'
exec spDescColumn N'News', N'Author'           , N'Автор.'
exec spDescColumn N'News', N'AuthorAlt'        , N'Автор на английски.'
exec spDescColumn N'News', N'EmailNotification', N'Нотификация по email.'
exec spDescColumn N'News', N'CreatedByUserId'  , N'Създадено от.'
exec spDescColumn N'News', N'CreateDate'       , N'Дата на създаване на записа.'
exec spDescColumn N'News', N'ModifyDate'       , N'Дата на последно редактиране на записа.'
exec spDescColumn N'News', N'Version'          , N'Версия.'
GO
