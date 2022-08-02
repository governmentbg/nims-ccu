GO

ALTER TABLE [dbo].[Logs] ADD [UserId] INT NULL;
ALTER TABLE [dbo].[Logs] ADD [RegistrationId] INT NULL;

GO

CREATE TABLE [dbo].[ActionLogs](
    [ActionLogId]       INT              NOT NULL IDENTITY,
    [ActionLogType]     INT              NOT NULL,
    [Action]            NVARCHAR (100)   NOT NULL,
    [AggregateRootId]   INT              NULL,
    [ChildRootId]       INT              NULL,
    [UserId]            INT              NULL,
    [RegistrationId]    INT              NULL,
    [PostData]          NVARCHAR (MAX)   NULL,
    [ResponseData]      NVARCHAR (MAX)   NULL,
    [RawUrl]            NVARCHAR (MAX)   NOT NULL,
    [RequestId]         UNIQUEIDENTIFIER NULL,
    [RemoteIpAddress]   NVARCHAR (50)    NOT NULL,
    [LogDate]           DATETIME         NOT NULL,
    CONSTRAINT [PK_ActionLogs]                  PRIMARY KEY ([ActionLogId]),
    CONSTRAINT [CHK_ActionLogs_ActionLogType]   CHECK       ([ActionLogType] IN (1, 2, 3)),
);

GO
