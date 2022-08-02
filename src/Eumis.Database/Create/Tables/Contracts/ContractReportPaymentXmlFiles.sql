PRINT 'ContractReportPaymentXmlFiles'
GO

CREATE TABLE [dbo].[ContractReportPaymentXmlFiles] (
    [ContractReportPaymentXmlFileId]  INT                 NOT NULL IDENTITY,
    [ContractReportPaymentId]         INT                 NOT NULL,
    [BlobKey]                         UNIQUEIDENTIFIER    NOT NULL,
    [Name]                            NVARCHAR(200)       NOT NULL,
    [Description]                     NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ContractReportPaymentXmlFiles]                          PRIMARY KEY ([ContractReportPaymentXmlFileId]),
    CONSTRAINT [FK_ContractReportPaymentXmlFiles_ContractReportPayments]   FOREIGN KEY ([ContractReportPaymentId])    REFERENCES [dbo].[ContractReportPayments] ([ContractReportPaymentId]),
    CONSTRAINT [FK_ContractReportPaymentXmlFiles_Blobs]                    FOREIGN KEY ([BlobKey])                    REFERENCES [dbo].[Blobs] ([Key])
);
GO

exec spDescTable  N'ContractReportPaymentXmlFiles', N'Файлове към xml за искане за плащане.'
exec spDescColumn N'ContractReportPaymentXmlFiles', N'ContractReportPaymentXmlFileId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportPaymentXmlFiles', N'ContractReportPaymentId'       , N'Идентификатор на xml за искане за плащане.'
exec spDescColumn N'ContractReportPaymentXmlFiles', N'BlobKey'                       , N'Идентификатор на файл.'
exec spDescColumn N'ContractReportPaymentXmlFiles', N'Name'                          , N'Име на файл.'
exec spDescColumn N'ContractReportPaymentXmlFiles', N'Description'                   , N'Описание.'
GO
