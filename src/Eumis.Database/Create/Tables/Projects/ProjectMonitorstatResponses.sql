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

exec spDescTable  N'ProjectMonitorstatResponses', N'Получени документи от Мониторстат.'
exec spDescColumn N'ProjectMonitorstatResponses', N'ProjectMonitorstatResponseId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectMonitorstatResponses', N'ProjectMonitorstatRequestId'         , N'Идентификатор на заявка.'
exec spDescColumn N'ProjectMonitorstatResponses', N'FileName'                            , N'Наименование на файл.'
exec spDescColumn N'ProjectMonitorstatResponses', N'FileKey'                             , N'Идентификатор на файл от Мониторстат.'
exec spDescColumn N'ProjectMonitorstatResponses', N'ModifyDate'                          , N'Дата на модифициране.'
GO
