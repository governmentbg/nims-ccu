PRINT 'ActionLogs'
GO

CREATE TABLE [dbo].[ActionLogs](
    [LogDate]                   DATETIME2          NOT NULL,
    [ActionLogId]               INT                NOT NULL IDENTITY,
    [ActionLogType]             INT                NOT NULL,
    [Action]                    NVARCHAR (100)     NOT NULL,
    [AggregateRootId]           INT                NULL,
    [ChildRootId]               INT                NULL,
    [Username]                  NVARCHAR (200)     NULL,
    [RegistrationEmail]         NVARCHAR (200)     NULL,
    [ContractRegistrationEmail] NVARCHAR (200)     NULL,
    [ContractAccessCodeEmail]   NVARCHAR (200)     NULL,
    [PostData]                  NVARCHAR (MAX)     NULL,
    [ResponseData]              NVARCHAR (MAX)     NULL,
    [RawUrl]                    NVARCHAR (MAX)     NOT NULL,
    [RequestId]                 UNIQUEIDENTIFIER   NULL,
    [RemoteIpAddress]           NVARCHAR (50)      NOT NULL,
    CONSTRAINT [PK_ActionLogs]                  PRIMARY KEY ([LogDate] ASC, [ActionLogId] ASC),
    CONSTRAINT [CHK_ActionLogs_ActionLogType]   CHECK       ([ActionLogType] IN (1, 2, 3)),
)
ON psActionLogs([LogDate]);
GO

exec spDescTable  N'ActionLogs', N'Лог на действията.'
exec spDescColumn N'ActionLogs', N'LogDate'                    , N'Дата на лог.'
exec spDescColumn N'ActionLogs', N'ActionLogId'                , N'Идентификатор на лог.'
exec spDescColumn N'ActionLogs', N'ActionLogType'              , N'Вид: 1 - Вътрешна система; 2 - Портал; 3 - Неуспешен вход в системата.'
exec spDescColumn N'ActionLogs', N'Action'                     , N'Действие.'
exec spDescColumn N'ActionLogs', N'AggregateRootId'            , N'Идентификатор на основен обект.'
exec spDescColumn N'ActionLogs', N'ChildRootId'                , N'Идентификатор на подобект.'
exec spDescColumn N'ActionLogs', N'Username'                   , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ActionLogs', N'RegistrationEmail'          , N'Имейл на потребител на модула за електронно кандидатстване.'
exec spDescColumn N'ActionLogs', N'ContractRegistrationEmail'  , N'Имейл на потребител с парола на модула за електронно отчитане.'
exec spDescColumn N'ActionLogs', N'ContractAccessCodeEmail'    , N'Имейл на потребител с код за достъп на модула за електронно отчитане.'
exec spDescColumn N'ActionLogs', N'PostData'                   , N'Данни от заявката.'
exec spDescColumn N'ActionLogs', N'ResponseData'               , N'Данни от отговора.'
exec spDescColumn N'ActionLogs', N'RawUrl'                     , N'URL на заявката.'
exec spDescColumn N'ActionLogs', N'RequestId'                  , N'Идентификатор на заявката.'
exec spDescColumn N'ActionLogs', N'RemoteIpAddress'            , N'ИП адрес.'
