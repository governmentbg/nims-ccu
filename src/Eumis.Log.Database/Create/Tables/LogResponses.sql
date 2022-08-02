PRINT 'LogResponses'
GO

CREATE TABLE [dbo].[LogResponses](
    [LogDate]                 DATETIME2          NOT NULL,
    [LogResponseId]           INT                NOT NULL IDENTITY,
    [RequestId]               UNIQUEIDENTIFIER   NOT NULL,
    [ResponseBody]            NVARCHAR (MAX)     NOT NULL,
    CONSTRAINT [PK_LogResponses] PRIMARY KEY ([LogDate] ASC, [LogResponseId] ASC)
)
ON psLogs([LogDate]);
GO
