PRINT 'Declarations'
GO

CREATE TABLE [dbo].[Declarations] (
    [DeclarationId]        INT                 NOT NULL IDENTITY,
    [Status]               INT                 NOT NULL,
    [Name]                 NVARCHAR(200)       NOT NULL,
    [NameAlt]              NVARCHAR(200)       NOT NULL,
    [Content]              NVARCHAR(MAX)       NOT NULL,
    [ContentAlt]           NVARCHAR(MAX)       NOT NULL,
    [ActivationDate]       DATETIME2           NULL,

    [CreatedByUserId]      INT                 NOT NULL,
    [CreateDate]           DATETIME2           NOT NULL,
    [ModifyDate]           DATETIME2           NOT NULL,
    [Version]              ROWVERSION          NOT NULL,

    CONSTRAINT [PK_Declarations]            PRIMARY KEY ([DeclarationId]),
    CONSTRAINT [FK_Declarations_Users]      FOREIGN KEY ([CreatedByUserId])         REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_Declarations_Status]    CHECK       ([Status] IN (1, 2, 3))
);
GO

exec spDescTable  N'Declarations', N'Декларации.'
exec spDescColumn N'Declarations', N'DeclarationId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Declarations', N'Status'             , N'Статус: 1 - Чернова; 2 - Публикувана; 3 - Архивирана.'
exec spDescColumn N'Declarations', N'Name'               , N'Наименование.'
exec spDescColumn N'Declarations', N'NameAlt'            , N'Наименование на друг език.'
exec spDescColumn N'Declarations', N'Content'            , N'Съдържание.'
exec spDescColumn N'Declarations', N'ContentAlt'         , N'Съдържание на друг език.'
exec spDescColumn N'Declarations', N'ActivationDate'     , N'Дата на влизане в сила.'
exec spDescColumn N'Declarations', N'CreatedByUserId'    , N'Създадено от.'
exec spDescColumn N'Declarations', N'CreateDate'         , N'Дата на създаване на записа.'
exec spDescColumn N'Declarations', N'ModifyDate'         , N'Дата на последно редактиране на записа.'
exec spDescColumn N'Declarations', N'Version'            , N'Версия.'
GO
