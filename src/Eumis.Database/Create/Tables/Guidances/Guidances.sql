PRINT 'Guidances'
GO

CREATE TABLE [dbo].[Guidances] (
    [GuidanceId]           INT                 NOT NULL IDENTITY,
    [BlobKey]              UNIQUEIDENTIFIER    NOT NULL,
    [FileName]             NVARCHAR(200)       NOT NULL,
    [Description]          NVARCHAR(200)       NOT NULL,
    [Module]               INT                 NOT NULL,

    [CreatedByUserId]      INT                 NOT NULL,
    [CreateDate]           DATETIME2           NOT NULL,
    [ModifyDate]           DATETIME2           NOT NULL,
    [Version]              ROWVERSION          NOT NULL

    CONSTRAINT [PK_Guidances]          PRIMARY KEY ([GuidanceId]),
    CONSTRAINT [FK_Guidances_Users]    FOREIGN KEY ([CreatedByUserId])    REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_Guidancess_Blobs]   FOREIGN KEY ([BlobKey])            REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_Guidances_Module]  CHECK       ([Module] IN (1, 2, 3, 4))
);
GO

exec spDescTable  N'Guidances', N'Ръководства.'
exec spDescColumn N'Guidances', N'GuidanceId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Guidances', N'BlobKey'        , N'Идентификатор на файл.'
exec spDescColumn N'Guidances', N'FileName'       , N'Име на файл.'
exec spDescColumn N'Guidances', N'Description'    , N'Описание.'
exec spDescColumn N'Guidances', N'Module'         , N'Модул - 1 - Вътрешна система; 2 - Портал за електронно кандидатстване; 3 - Портал за електронно отчитане; 4 - Публичен портал.'
exec spDescColumn N'Guidances', N'CreatedByUserId', N'Създал.'
exec spDescColumn N'Guidances', N'CreateDate'     , N'Дата на създаване на записа.'
exec spDescColumn N'Guidances', N'ModifyDate'     , N'Дата на последно редактиране на записа.'
exec spDescColumn N'Guidances', N'Version'        , N'Версия.'
GO
