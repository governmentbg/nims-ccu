PRINT 'RequestPackages'
GO

CREATE TABLE [dbo].[RequestPackages] (
    [RequestPackageId]           INT                NOT NULL IDENTITY,
    [Type]                       INT                NOT NULL,
    [Code]                       NVARCHAR(200)      NOT NULL,
    [Status]                     INT                NOT NULL,
    [EnteredByUserId]            INT                NULL,
    [CheckedByUserId]            INT                NULL,
    [EndedByUserId]              INT                NULL,
    [UserOrganizationId]         INT                NULL,
    [PackageDescription]         NVARCHAR(MAX)      NULL,
    [BlobKey1]                   UNIQUEIDENTIFIER   NULL,
    [Description1]               NVARCHAR(MAX)      NULL,
    [BlobKey2]                   UNIQUEIDENTIFIER   NULL,
    [Description2]               NVARCHAR(MAX)      NULL,
    [BlobKey3]                   UNIQUEIDENTIFIER   NULL,
    [Description3]               NVARCHAR(MAX)      NULL,
    [BlobKey4]                   UNIQUEIDENTIFIER   NULL,
    [Description4]               NVARCHAR(MAX)      NULL,
    [BlobKey5]                   UNIQUEIDENTIFIER   NULL,
    [Description5]               NVARCHAR(MAX)      NULL,
    [EndedMessage]               NVARCHAR(MAX)      NULL,

    [CreateDate]                 DATETIME2          NOT NULL,
    [ModifyDate]                 DATETIME2          NOT NULL,
    [Version]                    ROWVERSION         NOT NULL,


    CONSTRAINT [PK_RequestPackages]                      PRIMARY KEY    ([RequestPackageId]),
    CONSTRAINT [CHK_RequestPackages_Type]                CHECK          ([Type]   IN (1, 2)),
    CONSTRAINT [CHK_RequestPackages_Status]              CHECK          ([Status] IN (1, 2, 3, 4, 5)),
    CONSTRAINT [FK_RequestPackages_EnteredByUser]        FOREIGN KEY ([EnteredByUserId])      REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_RequestPackages_CheckedByUser]        FOREIGN KEY ([CheckedByUserId])      REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_RequestPackages_EndedByUser]          FOREIGN KEY ([EndedByUserId])        REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_RequestPackages_UserOrganization]     FOREIGN KEY ([UserOrganizationId])   REFERENCES [dbo].[UserOrganizations] ([UserOrganizationId]),
    CONSTRAINT [FK_RequestPackages_Blobs1]               FOREIGN KEY ([BlobKey1])             REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [FK_RequestPackages_Blobs2]               FOREIGN KEY ([BlobKey2])             REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [FK_RequestPackages_Blobs3]               FOREIGN KEY ([BlobKey3])             REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [FK_RequestPackages_Blobs4]               FOREIGN KEY ([BlobKey4])             REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [FK_RequestPackages_Blobs5]               FOREIGN KEY ([BlobKey5])             REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'RequestPackages', N'Пакети заявки'
exec spDescColumn N'RequestPackages', N'RequestPackageId'         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'RequestPackages', N'Type'                     , N'Тип на пакета : 1 - Заявка, 2 - Директна промяна.'
exec spDescColumn N'RequestPackages', N'Code'                     , N'Системно генериран код/номер на пакета.'
exec spDescColumn N'RequestPackages', N'CreateDate'               , N'Дата на създаване на пакета.'
exec spDescColumn N'RequestPackages', N'Status'                   , N'Статус на пакета.'
exec spDescColumn N'RequestPackages', N'EnteredByUserId'          , N'Идентификатор на потребителя, въвел пакета.'
exec spDescColumn N'RequestPackages', N'CheckedByUserId'          , N'Идентификатор на потребителя, проверил пакета.'
exec spDescColumn N'RequestPackages', N'EndedByUserId'            , N'Идентификатор на потребителя, приключил пакета.'
exec spDescColumn N'RequestPackages', N'UserOrganizationId'       , N'Идентификатор на организация към група потребители.'
exec spDescColumn N'RequestPackages', N'PackageDescription'       , N'Описание на пакета със заявки.'
exec spDescColumn N'RequestPackages', N'BlobKey1'                 , N'Идентификатор на файл 1.'
exec spDescColumn N'RequestPackages', N'Description1'             , N'Описание към файл 1.'
exec spDescColumn N'RequestPackages', N'BlobKey2'                 , N'Идентификатор на файл 2.'
exec spDescColumn N'RequestPackages', N'Description2'             , N'Описание към файл 2.'
exec spDescColumn N'RequestPackages', N'BlobKey3'                 , N'Идентификатор на файл 3.'
exec spDescColumn N'RequestPackages', N'Description3'             , N'Описание към файл 3.'
exec spDescColumn N'RequestPackages', N'BlobKey4'                 , N'Идентификатор на файл 4.'
exec spDescColumn N'RequestPackages', N'Description4'             , N'Описание към файл 4.'
exec spDescColumn N'RequestPackages', N'BlobKey5'                 , N'Идентификатор на файл 5.'
exec spDescColumn N'RequestPackages', N'Description5'             , N'Описание към файл 5.'
exec spDescColumn N'RequestPackages', N'EndedMessage'             , N'Съобщение при приключване на пакета.'

GO


