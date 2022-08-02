PRINT 'ContractReportFinancialCSDFiles'
GO

CREATE TABLE [dbo].[ContractReportFinancialCSDFiles] (
    [ContractReportFinancialCSDFileId]       INT                 NOT NULL,
    [ContractReportFinancialCSDId]           INT                 NOT NULL,
    [ContractReportFinancialId]              INT                 NOT NULL,
    [BlobKey]                                UNIQUEIDENTIFIER    NOT NULL,
    [Name]                                   NVARCHAR(200)       NOT NULL,
    [Description]                            NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ContractReportFinancialCSDFiles]                             PRIMARY KEY ([ContractReportFinancialCSDFileId]),
    CONSTRAINT [FK_ContractReportFinancialCSDFiles_ContractReportFinancialCSDs] FOREIGN KEY ([ContractReportFinancialCSDId])  REFERENCES [dbo].[ContractReportFinancialCSDs] ([ContractReportFinancialCSDId]),
    CONSTRAINT [FK_ContractReportFinancialCSDFiles_ContractReportFinancials]    FOREIGN KEY ([ContractReportFinancialId])     REFERENCES [dbo].[ContractReportFinancials] ([ContractReportFinancialId]),
    CONSTRAINT [FK_ContractReportFinancialCSDFiles_Blobs]                       FOREIGN KEY ([BlobKey])                       REFERENCES [dbo].[Blobs] ([Key])
);
GO

exec spDescTable  N'ContractReportFinancialCSDFiles', N'Файлове към разходооправдателен документ.'
exec spDescColumn N'ContractReportFinancialCSDFiles', N'ContractReportFinancialCSDFileId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportFinancialCSDFiles', N'ContractReportFinancialCSDId'     , N'Идентификатор на разходооправдателен документ.'
exec spDescColumn N'ContractReportFinancialCSDFiles', N'BlobKey'                          , N'Идентификатор на файл.'
exec spDescColumn N'ContractReportFinancialCSDFiles', N'Name'                             , N'Име на файл.'
exec spDescColumn N'ContractReportFinancialCSDFiles', N'Description'                      , N'Описание.'
GO
