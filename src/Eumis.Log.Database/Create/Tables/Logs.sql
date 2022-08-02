PRINT 'Logs'
GO

CREATE TABLE [dbo].[Logs](
    [LogDate]                 DATETIME2          NOT NULL,
    [LogId]                   INT                NOT NULL IDENTITY,
    [Level]                   NVARCHAR (50)      NOT NULL,
    [Application]             NVARCHAR (50)      NOT NULL,
    [Message]                 NVARCHAR (MAX)     NULL,
    [IP]                      NVARCHAR (50)      NULL,
    [Method]                  NVARCHAR (10)      NULL,
    [RawUrl]                  NVARCHAR (MAX)     NULL,
    [UserAgent]               NVARCHAR (MAX)     NULL,
    [SessionId]               UNIQUEIDENTIFIER   NULL,
    [RequestId]               UNIQUEIDENTIFIER   NULL,
    [UserId]                  INT                NULL,
    [RegistrationId]          INT                NULL,
    [ContractRegistrationId]  INT                NULL,
    [ContractAccessCodeId]    INT                NULL,
    [ElapsedMilliseconds]     BIGINT             NULL,
    [Status]                  NVARCHAR (MAX)     NULL,
    CONSTRAINT [PK_Logs] PRIMARY KEY ([LogDate] ASC, [LogId] ASC)
)
ON psLogs([LogDate]);
GO
