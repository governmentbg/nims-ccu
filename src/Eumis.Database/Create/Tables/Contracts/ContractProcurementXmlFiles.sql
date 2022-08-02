PRINT 'ContractProcurementXmlFiles'
GO

CREATE TABLE [dbo].[ContractProcurementXmlFiles] (
    [ContractProcurementXmlFileId]  INT                 NOT NULL IDENTITY,
    [ContractProcurementXmlId]      INT                 NOT NULL,
    [Type]                          INT                 NOT NULL,
    [BlobKey]                       UNIQUEIDENTIFIER    NOT NULL,
    [Name]                          NVARCHAR(200)       NOT NULL,
    [Description]                   NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ContractProcurementXmlFiles]                           PRIMARY KEY ([ContractProcurementXmlFileId]),
    CONSTRAINT [FK_ContractProcurementXmlFiles_ContractProcurementXmls]   FOREIGN KEY ([ContractProcurementXmlId])    REFERENCES [dbo].[ContractProcurementXmls] ([ContractProcurementXmlId]),
    CONSTRAINT [FK_ContractProcurementXmlFiles_Blobs]                     FOREIGN KEY ([BlobKey])                     REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_ContractProcurementXmlFiles_Type]                     CHECK ([Type] IN (1, 2, 3))
);
GO

exec spDescTable  N'ContractProcurementXmlFiles', N'Файлове към xml за процедурa за избор на изпълнител и сключени договори на договор за БФП.'
exec spDescColumn N'ContractProcurementXmlFiles', N'ContractProcurementXmlFileId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractProcurementXmlFiles', N'ContractProcurementXmlId'    , N'Идентификатор на xml за процедурa за избор на изпълнител и сключени договори на договор за БФП.'
exec spDescColumn N'ContractProcurementXmlFiles', N'Type'                        , N'Тип на файла: 1 - Документ към договор с изпълнители; 2 - Документ към процедура за избор на изпълнител и сключени договори; 3 - Публичен документ към процедура за избор на изпълнител и сключени договори.'
exec spDescColumn N'ContractProcurementXmlFiles', N'BlobKey'                     , N'Идентификатор на файл.'
exec spDescColumn N'ContractProcurementXmlFiles', N'Name'                        , N'Име на файл.'
exec spDescColumn N'ContractProcurementXmlFiles', N'Description'                 , N'Описание.'
GO
