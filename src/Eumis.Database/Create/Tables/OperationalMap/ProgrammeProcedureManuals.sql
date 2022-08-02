PRINT 'ProgrammeProcedureManuals'
GO

CREATE TABLE [dbo].[ProgrammeProcedureManuals] (
    [ProgrammeProcedureManualId]                INT                 NOT NULL IDENTITY,
    [MapNodeId]                                 INT                 NOT NULL,
    [Name]                                      NVARCHAR(MAX)       NOT NULL,
    [Description]                               NVARCHAR(MAX)       NOT NULL,
    [OrderNum]                                  INT                 NOT NULL,
    [Status]                                    INT                 NOT NULL,
    [ActivationDate]                            DATETIME2           NULL,
    [ActivatedByUserId]                         INT                 NULL,
    [BlobKey]                                   UNIQUEIDENTIFIER    NULL,

    CONSTRAINT [PK_ProgrammeProcedureManuals]                          PRIMARY KEY ([ProgrammeProcedureManualId]),
    CONSTRAINT [FK_ProgrammeProcedureManuals_MapNodes]                 FOREIGN KEY ([MapNodeId])          REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_ProgrammeProcedureManuals_Blobs]                    FOREIGN KEY ([BlobKey])            REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [FK_ProgrammeProcedureManuals_Users]                    FOREIGN KEY ([ActivatedByUserId])  REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ProgrammeProcedureManuals_Status]                  CHECK       ([Status] IN (1, 2, 3))
);
GO

exec spDescTable  N'ProgrammeProcedureManuals', N'Процедурен наръчник.'
exec spDescColumn N'ProgrammeProcedureManuals', N'ProgrammeProcedureManualId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProgrammeProcedureManuals', N'MapNodeId'                         , N'Идентификатор на оперативна програма.'
exec spDescColumn N'ProgrammeProcedureManuals', N'Name'                              , N'Наименование.'
exec spDescColumn N'ProgrammeProcedureManuals', N'Description'                       , N'Описание.'
exec spDescColumn N'ProgrammeProcedureManuals', N'OrderNum'                          , N'Пореден номер.'
exec spDescColumn N'ProgrammeProcedureManuals', N'Status'                            , N'Статус на процедурния наръчник: 1 - Чернова, 2 - Актуален, 3 - Архивиран.'
exec spDescColumn N'ProgrammeProcedureManuals', N'ActivationDate'                    , N'Дата на активиране.'
exec spDescColumn N'ProgrammeProcedureManuals', N'ActivatedByUserId'                 , N'Идентификатор на потребител активирал процедурния наръчник.'
exec spDescColumn N'ProgrammeProcedureManuals', N'BlobKey'                           , N'Идентификатор на файл.'

GO
