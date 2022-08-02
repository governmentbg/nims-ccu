PRINT 'ProjectMonitorstatResponses'
GO

CREATE TABLE [dbo].[ProjectMonitorstatResponses] (
    [ProjectMonitorstatResponseId]                       INT                 NOT NULL IDENTITY,
    [ProjectMonitorstatRequestId]                        INT                 NOT NULL,
    [FileName]                                           NVARCHAR(MAX)       NOT NULL,
    [FileKey]                                            UNIQUEIDENTIFIER    NOT NULL,
    [ModifyDate]                                         DATETIME2           NOT NULL,
    
    CONSTRAINT [PK_ProjectMonitorstatResponses]                               PRIMARY KEY ([ProjectMonitorstatResponseId]),
    CONSTRAINT [FK_ProjectMonitorstatResponses_ProjectMonitorstatRequests]    FOREIGN KEY ([ProjectMonitorstatRequestId])   REFERENCES [dbo].[ProjectMonitorstatRequests] ([ProjectMonitorstatRequestId]),
    CONSTRAINT [FK_ProjectMonitorstatResponses_Blobs]                         FOREIGN KEY ([FileKey])                       REFERENCES [dbo].[Blobs] ([Key]),
);
GO
