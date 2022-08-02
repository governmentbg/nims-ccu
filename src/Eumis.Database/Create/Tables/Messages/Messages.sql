PRINT 'Messages'
GO

CREATE TABLE [dbo].[Messages] (
    [MessageId]             INT                NOT NULL IDENTITY,
    [Status]                INT                NOT NULL,
    [Title]                 NVARCHAR(200)      NOT NULL,
    [Content]               NVARCHAR(MAX)      NOT NULL,

    [SenderId]              INT                NOT NULL,
    [SentDate]              DATETIME2          NULL,
    [Number]                INT                NULL,

    [CreateDate]            DATETIME2          NOT NULL,
    [ModifyDate]            DATETIME2          NOT NULL,
    [Version]               ROWVERSION         NOT NULL

    CONSTRAINT [PK_Messages]            PRIMARY KEY ([MessageId]),
    CONSTRAINT [FK_Messages_Users]      FOREIGN KEY ([SenderId])       REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_Messages_Status]    CHECK ([Status] IN (1, 2))
);
GO

exec spDescTable  N'Messages', N'Съобщения.'
exec spDescColumn N'Messages', N'MessageId'            , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Messages', N'Status'               , N'Статус: 1 - Чернова; 2 - Изпратено.'
exec spDescColumn N'Messages', N'Title'                , N'Заглавие.'
exec spDescColumn N'Messages', N'Content'              , N'Съдържание.'
exec spDescColumn N'Messages', N'SenderId'             , N'Идентификатор на изпращача.'
exec spDescColumn N'Messages', N'SentDate'             , N'Дата на изпращане.'
exec spDescColumn N'Messages', N'Number'               , N'Пореден номер на изпращане.'
exec spDescColumn N'Messages', N'CreateDate'           , N'Дата на създаване на записа.'
exec spDescColumn N'Messages', N'ModifyDate'           , N'Дата на последно редактиране на записа.'
exec spDescColumn N'Messages', N'Version'              , N'Версия.'
GO
