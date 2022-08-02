PRINT 'ProcedureDiscussions'
GO

CREATE TABLE [dbo].[ProcedureDiscussions] (
    [ProcedureDiscussionId]        INT              NOT NULL IDENTITY,
    [Gid]                          UNIQUEIDENTIFIER NOT NULL UNIQUE,
    [ProcedureId]                  INT              NOT NULL,
    [Status]                       INT              NOT NULL,
    [Type]                         INT              NOT NULL,
    [Question]                     NVARCHAR(4000)   NOT NULL,
    [UserId]                       INT              NULL,
    [RegistrationId]               INT              NULL,
    [SenderEmail]                  NVARCHAR (100)   NOT NULL,
    [EditQuestion]                 NVARCHAR(4000)   NULL,
    [Answer]                       NVARCHAR(4000)   NULL,
    [AnswerUserId]                 INT              NULL,
    [AnswerDate]                   DATETIME2        NULL,
    [PublicationDate]              DATETIME2        NULL,

    [CreateDate]                   DATETIME2        NOT NULL,
    [ModifyDate]                   DATETIME2        NOT NULL,
    [Version]                      ROWVERSION       NOT NULL,

    CONSTRAINT [PK_ProcedureDiscussions]                        PRIMARY KEY ([ProcedureDiscussionId]),
    CONSTRAINT [FK_ProcedureDiscussions_Procedures]             FOREIGN KEY ([ProcedureId])         REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureDiscussions_RegistrationId]         FOREIGN KEY ([RegistrationId])      REFERENCES [dbo].[Registrations] ([RegistrationId]),
    CONSTRAINT [FK_ProcedureDiscussions_UserId]                 FOREIGN KEY ([UserId])              REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_ProcedureDiscussions_AnswerUserId]           FOREIGN KEY ([AnswerUserId])        REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ProcedureDiscussions_Status]                CHECK       ([Status] IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_ProcedureDiscussions_Type]                  CHECK       ([Type] IN (1, 2)),
);
GO
