PRINT 'PublicDiscussionComments'
GO

CREATE TABLE [dbo].[PublicDiscussionComments] (
    [PublicDiscussionCommentId]    INT              NOT NULL IDENTITY,
    [Gid]                          UNIQUEIDENTIFIER NOT NULL UNIQUE,
    [PublicDiscussionId]           INT              NOT NULL,
    [Status]                       INT              NOT NULL,
    [Type]                         INT              NOT NULL,
    [RegistrationId]               INT              NULL,
    [SenderEmail]                  NVARCHAR (100)   NOT NULL,
    [UserId]                       INT              NULL,
    [CreateDate]                   DATETIME2        NOT NULL,
    [Comment]                      NVARCHAR(4000)   NOT NULL,
    [EditComment]                  NVARCHAR(4000)   NULL,
    [Standpoint]                   NVARCHAR(4000)   NULL,
    [StandpointDate]               DATETIME2        NULL,
    [StandpointUserId]             INT              NULL,
    [BlobKey]                      UNIQUEIDENTIFIER NULL,

    CONSTRAINT [PK_PublicDiscussionComments]                        PRIMARY KEY ([PublicDiscussionCommentId]),
    CONSTRAINT [FK_PublicDiscussionComments_PublicDiscussions]      FOREIGN KEY ([PublicDiscussionId])  REFERENCES [dbo].[PublicDiscussions] ([PublicDiscussionId]),
    CONSTRAINT [FK_PublicDiscussionComments_Blobs]                  FOREIGN KEY ([BlobKey])             REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [FK_PublicDiscussionComments_RegistrationId]         FOREIGN KEY ([RegistrationId])      REFERENCES [dbo].[Registrations] ([RegistrationId]),
    CONSTRAINT [FK_PublicDiscussionComments_UserId]                 FOREIGN KEY ([UserId])              REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_PublicDiscussionComments_StandpointUserId]       FOREIGN KEY ([StandpointUserId])    REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_PublicDiscussionComments_Status]                CHECK   ([Status] IN (1, 2, 3)),
    CONSTRAINT [CHK_PublicDiscussionComments_Type]                  CHECK   ([Type] IN (1, 2)),
);
GO

exec spDescTable  N'PublicDiscussionComments', N'Насоки към обществено обсъждане на процедура.'
exec spDescColumn N'PublicDiscussionComments', N'PublicDiscussionCommentId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'PublicDiscussionComments', N'Gid'                         , N'Публичен системно генериран идентификатор.'
exec spDescColumn N'PublicDiscussionComments', N'PublicDiscussionId'          , N'Идентификатор на обществено обсъждане на процедура.'
exec spDescColumn N'PublicDiscussionComments', N'Status'                      , N'Статус: 1 - Неактивиран, 2 - Активиран, 3 - Неактивен.'
exec spDescColumn N'PublicDiscussionComments', N'Type'                        , N'Статус: 1 - Кандидат, 2 - УО/МИГ.'
exec spDescColumn N'PublicDiscussionComments', N'RegistrationId'              , N'Идентификатор на регистрация (подател).'
exec spDescColumn N'PublicDiscussionComments', N'SenderEmail'                 , N'E-mail на подател.'
exec spDescColumn N'PublicDiscussionComments', N'UserId'                      , N'Идентификатор на потребител (подател).'
exec spDescColumn N'PublicDiscussionComments', N'CreateDate'                  , N'Дата на подаване.'
exec spDescColumn N'PublicDiscussionComments', N'Comment'                     , N'Коментар/предложение.'
exec spDescColumn N'PublicDiscussionComments', N'EditComment'                 , N'Коригиран коментар/предложение.'
exec spDescColumn N'PublicDiscussionComments', N'Standpoint'                  , N'Становище на УО/МИГ.'
exec spDescColumn N'PublicDiscussionComments', N'StandpointDate'              , N'Дата на становище.'
exec spDescColumn N'PublicDiscussionComments', N'StandpointUserId'            , N'Идентификатор на потребител (становище).'
exec spDescColumn N'PublicDiscussionComments', N'BlobKey'                     , N'Идентификатор на файл.'
GO
