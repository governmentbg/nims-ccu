PRINT 'MessageRecipients'
GO

CREATE TABLE [dbo].[MessageRecipients] (
    [MessageRecipientId]    INT                NOT NULL IDENTITY,
    [MessageId]             INT                NOT NULL,
    [RecipientId]           INT                NOT NULL,
    [RecieveDate]           DATETIME2          NULL,
    [IsArchived]            BIT                NOT NULL,

    CONSTRAINT [PK_MessageRecipients]            PRIMARY KEY ([MessageRecipientId]),
    CONSTRAINT [FK_MessageRecipients_Messages]   FOREIGN KEY ([MessageId])       REFERENCES [dbo].[Messages] ([MessageId]),
    CONSTRAINT [FK_MessageRecipients_Users]      FOREIGN KEY ([RecipientId])    REFERENCES [dbo].[Users] ([UserId])
);
GO

exec spDescTable  N'MessageRecipients', N'Получатели на съобщение.'
exec spDescColumn N'MessageRecipients', N'MessageRecipientId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'MessageRecipients', N'MessageId'            , N'Идентификатор на съобщение.'
exec spDescColumn N'MessageRecipients', N'RecipientId'          , N'Идентификатор на потребител - получател.'
exec spDescColumn N'MessageRecipients', N'RecieveDate'          , N'Дата на получаване.'
exec spDescColumn N'MessageRecipients', N'IsArchived'           , N'Маркер дали съобщението е архивирано.'
GO
