PRINT 'Emails'
GO

CREATE TABLE [dbo].[Emails](
    [EmailId]               INT               NOT NULL IDENTITY,
    [Recipient]             NVARCHAR(100)     NOT NULL,
    [MailTemplateName]      NVARCHAR(100)     NOT NULL,
    [Context]               NVARCHAR(MAX)     NULL,
    [Status]                INT               NOT NULL,
    [FailedAttempts]        INT               NOT NULL,
    [FailedAttemptsErrors]  NVARCHAR(MAX)     NULL,
    [CreateDate]            DATETIME2         NOT NULL,
    [ModifyDate]            DATETIME2         NOT NULL,
    [Version]               ROWVERSION        NOT NULL,

    CONSTRAINT [PK_Emails]          PRIMARY KEY ([EmailId]),
    CONSTRAINT [CHK_Emails_Status]  CHECK       ([Status] IN (1, 2, 3))
);

GO

exec spDescTable  N'Emails', N'Опашка за електронна поща.'
exec spDescColumn N'Emails', N'EmailId'             , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Emails', N'Recipient'           , N'Получател.'
exec spDescColumn N'Emails', N'MailTemplateName'    , N'Име на шаблон.'
exec spDescColumn N'Emails', N'Context'             , N'Контекст на email-a.'
exec spDescColumn N'Emails', N'Status'              , N'Статус. 1 - Предстоящо изпращане, 2 - Изпратен, 3 - Грешка'
exec spDescColumn N'Emails', N'FailedAttempts'      , N'Брой неуспешни опити за изпращане'
exec spDescColumn N'Emails', N'CreateDate'          , N'Дата на създаване на записа.'
exec spDescColumn N'Emails', N'ModifyDate'          , N'Дата на последно редактиране на записа.'
exec spDescColumn N'Emails', N'Version'             , N'Версия на записа.'

GO
