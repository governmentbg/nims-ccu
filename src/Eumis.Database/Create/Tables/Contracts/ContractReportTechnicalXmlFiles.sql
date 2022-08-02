PRINT 'ContractReportTechnicalXmlFiles'
GO

CREATE TABLE [dbo].[ContractReportTechnicalXmlFiles] (
    [ContractReportTechnicalXmlFileId]  INT                 NOT NULL IDENTITY,
    [ContractReportTechnicalId]         INT                 NOT NULL,
    [Type]                              INT                 NOT NULL,
    [BlobKey]                           UNIQUEIDENTIFIER    NOT NULL,
    [Name]                              NVARCHAR(200)       NOT NULL,
    [Description]                       NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ContractReportTechnicalXmlFiles]                            PRIMARY KEY ([ContractReportTechnicalXmlFileId]),
    CONSTRAINT [FK_ContractReportTechnicalXmlFiles_ContractReportTechnicals]   FOREIGN KEY ([ContractReportTechnicalId])    REFERENCES [dbo].[ContractReportTechnicals] ([ContractReportTechnicalId]),
    CONSTRAINT [FK_ContractReportTechnicalXmlFiles_Blobs]                      FOREIGN KEY ([BlobKey])                      REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_ContractReportTechnicalXmlFiles_Type]                      CHECK ([Type] IN (1, 2, 3))
);
GO

exec spDescTable  N'ContractReportTechnicalXmlFiles', N'Файлове към xml за технически отчет.'
exec spDescColumn N'ContractReportTechnicalXmlFiles', N'ContractReportTechnicalXmlFileId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportTechnicalXmlFiles', N'ContractReportTechnicalId'       , N'Идентификатор на xml за технически отчет.'
exec spDescColumn N'ContractReportTechnicalXmlFiles', N'Type'                            , N'Тип на файла: 1 - Прикачен документ; 2 - Документ към процедура за избор на изпълнител и сключени договори; 3 - Публичен документ към процедура за избор на изпълнител и сключени договори.'
exec spDescColumn N'ContractReportTechnicalXmlFiles', N'BlobKey'                         , N'Идентификатор на файл.'
exec spDescColumn N'ContractReportTechnicalXmlFiles', N'Name'                            , N'Име на файл.'
exec spDescColumn N'ContractReportTechnicalXmlFiles', N'Description'                     , N'Описание.'
GO
