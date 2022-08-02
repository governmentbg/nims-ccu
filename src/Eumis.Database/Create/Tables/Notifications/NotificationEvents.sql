PRINT 'NotificationEvents'

CREATE TABLE [dbo].[NotificationEvents] (
    [NotificationEventId]       INT             NOT NULL IDENTITY,
    [Name]                      NVARCHAR(200)   NOT NULL,
    [NameAlt]                   NVARCHAR(200)   NULL,
    [IsProgrammeDependent]      BIT             NOT NULL,
    [ModifyDate]                DATETIME2       NOT NULL
    CONSTRAINT [PK_NotificationEvents]          PRIMARY KEY ([NotificationEventId])
);

exec spDescTable  N'NotificationEvents', N'Събития за нотификация.'
exec spDescColumn N'NotificationEvents', N'NotificationEventId'             , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'NotificationEvents', N'Name'                            , N'Наименование на събитие.'
exec spDescColumn N'NotificationEvents', N'IsProgrammeDependent'            , N'Зависи от оперативната програма: 0 - Не, 1 - Да.'
exec spDescColumn N'NotificationEvents', N'ModifyDate'                      , N'Дата на модифициране.'
