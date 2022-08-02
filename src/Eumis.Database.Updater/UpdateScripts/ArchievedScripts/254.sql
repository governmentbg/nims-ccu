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

PRINT 'PublicDiscussionGuidelines'
GO

CREATE TABLE [dbo].[PublicDiscussionGuidelines] (
    [PublicDiscussionGuidelineId]   INT                 NOT NULL IDENTITY,
    [Gid]                           UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [PublicDiscussionId]            INT                 NOT NULL,
    [FileName]                      NVARCHAR(MAX)       NULL,
    [Description]                   NVARCHAR(MAX)       NULL,
    [BlobKey]                       UNIQUEIDENTIFIER    NULL,
    [Status]                        INT                 NOT NULL

    CONSTRAINT [PK_PublicDiscussionGuidelines]                      PRIMARY KEY ([PublicDiscussionGuidelineId]),
    CONSTRAINT [FK_PublicDiscussionGuidelines_PublicDiscussions]    FOREIGN KEY ([PublicDiscussionId])      REFERENCES [dbo].[PublicDiscussions] ([PublicDiscussionId]),
    CONSTRAINT [FK_PublicDiscussionGuidelines_Blobs]                FOREIGN KEY ([BlobKey])                 REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_PublicDiscussionGuidelines_Status]              CHECK   ([Status] IN (1, 2, 3)),
);
GO

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
