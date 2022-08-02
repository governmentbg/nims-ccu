PRINT 'ProjectVersionXmls'
GO

CREATE TABLE [dbo].[ProjectVersionXmls] (
    [ProjectVersionXmlId]   INT                 NOT NULL IDENTITY,
    [Gid]                   UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [ProjectId]             INT                 NOT NULL,
    [Xml]                   XML                 NOT NULL,
    [Hash]                  NVARCHAR(10)        NOT NULL UNIQUE,
    [OrderNum]              INT                 NOT NULL,
    [Status]                INT                 NOT NULL,
    [CreatedByUserId]       INT                 NULL,
    [CreateNote]            NVARCHAR(MAX)       NOT NULL,
    [CreateNoteAlt]         NVARCHAR(MAX)       NULL,
    [TotalBfpAmount]        MONEY               NULL,
    [CoFinancingAmount]     MONEY               NULL,
    [CreateDate]            DATETIME2           NOT NULL,
    [ModifyDate]            DATETIME2           NOT NULL,
    [Version]               ROWVERSION          NOT NULL

    CONSTRAINT [PK_ProjectVersionXmls]                 PRIMARY KEY ([ProjectVersionXmlId]),
    CONSTRAINT [FK_ProjectVersionXmls_Projects]        FOREIGN KEY ([ProjectId])       REFERENCES [dbo].[Projects] ([ProjectId]),
    CONSTRAINT [FK_ProjectVersionXmls_Users]           FOREIGN KEY ([CreatedByUserId]) REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ProjectXmls]                       CHECK ([Status] IN (1, 2, 3))
);
GO

exec spDescTable  N'ProjectVersionXmls', N'Xml за проектно предложение.'
exec spDescColumn N'ProjectVersionXmls', N'ProjectVersionXmlId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectVersionXmls', N'Gid'                     , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ProjectVersionXmls', N'ProjectId'               , N'Идентификатор на проектно предложение.'
exec spDescColumn N'ProjectVersionXmls', N'Xml'                     , N'Xml съдържание.'
exec spDescColumn N'ProjectVersionXmls', N'Hash'                    , N'Уникален идентификатор на съдържанието на Xml-а.'
exec spDescColumn N'ProjectVersionXmls', N'OrderNum'                , N'Пореден номер.'
exec spDescColumn N'ProjectVersionXmls', N'Status'                  , N'Статус, 1 - Чернова, 2 - Актуален, 3 - Архивиран.'
exec spDescColumn N'ProjectVersionXmls', N'CreatedByUserId'         , N'Създадено от.'
exec spDescColumn N'ProjectVersionXmls', N'CreateNote'              , N'Бележка.'
exec spDescColumn N'ProjectVersionXmls', N'CreateNoteAlt'           , N'Бележка на друг език.'
exec spDescColumn N'ProjectVersionXmls', N'TotalBfpAmount'          , N'Общ размер на безвъзмездната финансова помощ.'
exec spDescColumn N'ProjectVersionXmls', N'CoFinancingAmount'       , N'Общ размер на собствено съфинансиране.'
exec spDescColumn N'ProjectVersionXmls', N'CreateDate'              , N'Дата на създаване на записа.'
exec spDescColumn N'ProjectVersionXmls', N'ModifyDate'              , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProjectVersionXmls', N'Version'                 , N'Версия.'

GO
