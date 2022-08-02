PRINT 'ContractReportFinancialXmlFiles'
GO

CREATE TABLE [dbo].[ContractReportFinancialXmlFiles] (
    [ContractReportFinancialXmlFileId]  INT                 NOT NULL IDENTITY,
    [ContractReportFinancialId]         INT                 NOT NULL,
    [BlobKey]                           UNIQUEIDENTIFIER    NOT NULL,
    [Name]                              NVARCHAR(200)       NOT NULL,
    [Description]                       NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ContractReportFinancialXmlFiles]                            PRIMARY KEY ([ContractReportFinancialXmlFileId]),
    CONSTRAINT [FK_ContractReportFinancialXmlFiles_ContractReportFinancials]   FOREIGN KEY ([ContractReportFinancialId])    REFERENCES [dbo].[ContractReportFinancials] ([ContractReportFinancialId]),
    CONSTRAINT [FK_ContractReportFinancialXmlFiles_Blobs]                      FOREIGN KEY ([BlobKey])                      REFERENCES [dbo].[Blobs] ([Key])
);
GO

exec spDescTable  N'ContractReportFinancialXmlFiles', N'Файлове към xml за финансов отчет.'
exec spDescColumn N'ContractReportFinancialXmlFiles', N'ContractReportFinancialXmlFileId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportFinancialXmlFiles', N'ContractReportFinancialId'       , N'Идентификатор на xml за финансов отчет.'
exec spDescColumn N'ContractReportFinancialXmlFiles', N'BlobKey'                         , N'Идентификатор на файл.'
exec spDescColumn N'ContractReportFinancialXmlFiles', N'Name'                            , N'Име на файл.'
exec spDescColumn N'ContractReportFinancialXmlFiles', N'Description'                     , N'Описание.'
GO
