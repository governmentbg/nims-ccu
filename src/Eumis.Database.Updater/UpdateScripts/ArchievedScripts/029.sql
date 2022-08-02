GO

CREATE TABLE [dbo].[CheckBlankTopics] (
    [CheckBlankTopicId]    INT                 NOT NULL IDENTITY,
    [Gid]                  UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [Name]                 NVARCHAR(100)       NOT NULL,
    [CreateDate]           DATETIME2           NOT NULL,
    [ModifyDate]           DATETIME2           NOT NULL,
    [Version]              ROWVERSION          NOT NULL,

    CONSTRAINT [PK_CheckBlankTopics] PRIMARY KEY ([CheckBlankTopicId]),
);
GO

exec spDescTable  N'CheckBlankTopics', N'Теми за формуляр за провеждане на проверки на място.'
exec spDescColumn N'CheckBlankTopics', N'CheckBlankTopicId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CheckBlankTopics', N'Gid'                     , N'Глобален уникален идентификатор.'
exec spDescColumn N'CheckBlankTopics', N'Name'                    , N'Наименование.'
exec spDescColumn N'CheckBlankTopics', N'CreateDate'              , N'Дата на създаване на записа.'
exec spDescColumn N'CheckBlankTopics', N'ModifyDate'              , N'Дата на последно редактиране на записа.'
exec spDescColumn N'CheckBlankTopics', N'Version'                 , N'Версия.'
GO
