PRINT 'PublicDiscussions'
GO

CREATE TABLE [dbo].[PublicDiscussions] (
    [PublicDiscussionId]            INT             NOT NULL IDENTITY,
    [ProcedureId]                   INT             NOT NULL,
    [Status]                        INT             NOT NULL,
    [CommentsSectionStatus]         INT             NOT NULL,
    [FirstPublicatedByUserId]       INT             NULL,
    [FirstPublicationDate]          DATETIME2       NULL,
    [LastPublicatedByUserId]        INT             NULL,
    [LastPublicationDate]           DATETIME2       NULL,
    [EndDate]                       DATETIME2       NULL,
    [CreateDate]                    DATETIME2       NOT NULL,
    [ModifyDate]                    DATETIME2       NOT NULL,
    [Version]                       ROWVERSION      NOT NULL,

    CONSTRAINT [PK_PublicDiscussions]                               PRIMARY KEY ([PublicDiscussionId]),
    CONSTRAINT [FK_PublicDiscussions_Procedures]                    FOREIGN KEY ([ProcedureId])             REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_PublicDiscussions_FirstPublicatedByUser]         FOREIGN KEY ([FirstPublicatedByUserId]) REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_PublicDiscussions_LastPublicatedByUser]          FOREIGN KEY ([LastPublicatedByUserId])  REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_PublicDiscussions_Status]                       CHECK   ([Status] IN (1, 2)),
    CONSTRAINT [CHK_PublicDiscussions_CommentsSectionStatus]        CHECK   ([CommentsSectionStatus] IN (1, 2)),
);
GO

exec spDescTable  N'PublicDiscussions', N'Обществено обсъждане на процедура.'
exec spDescColumn N'PublicDiscussions', N'PublicDiscussionId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'PublicDiscussions', N'ProcedureId'              , N'Идентификатор на процедура.'
exec spDescColumn N'PublicDiscussions', N'Status'                   , N'Статус: 1 - Чернова, 2 - Публикуван.'
exec spDescColumn N'PublicDiscussions', N'CommentsSectionStatus'    , N'Статус на секция "Коментари и предложения": 1 - Чернова, 2 - Публикуван.'
exec spDescColumn N'PublicDiscussions', N'FirstPublicatedByUserId'  , N'Публикувано за първи път от.'
exec spDescColumn N'PublicDiscussions', N'FirstPublicationDate'     , N'Дата на първо публикуване.'
exec spDescColumn N'PublicDiscussions', N'LastPublicatedByUserId'   , N'Публикувано за последен път от.'
exec spDescColumn N'PublicDiscussions', N'LastPublicationDate'      , N'Дата на последно публикуване.'
exec spDescColumn N'PublicDiscussions', N'EndDate'                  , N'Крайна дата.'
exec spDescColumn N'PublicDiscussions', N'CreateDate'               , N'Дата на създаване на записа.'
exec spDescColumn N'PublicDiscussions', N'ModifyDate'               , N'Дата на последно редактиране на записа.'
exec spDescColumn N'PublicDiscussions', N'Version'                  , N'Версия.'
GO
