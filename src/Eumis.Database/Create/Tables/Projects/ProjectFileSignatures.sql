PRINT 'ProjectFileSignatures'
GO

CREATE TABLE [dbo].[ProjectFileSignatures] (
    [ProjectFileSignatureId]    INT             NOT NULL IDENTITY,
    [ProjectFileId]             INT             NOT NULL,
    [Signature]                 VARBINARY(MAX)  NOT NULL,
    [FileName]                  NVARCHAR(200)   NOT NULL,

    CONSTRAINT [PK_ProjectFileSignatures]               PRIMARY KEY ([ProjectFileSignatureId]),
    CONSTRAINT [FK_ProjectFileSignatures_ProjectFiles]  FOREIGN KEY ([ProjectFileId])   REFERENCES [dbo].[ProjectFiles] ([ProjectFileId])
);
GO

exec spDescTable  N'ProjectFileSignatures', N'Подпис на файл на проектно предложение.'
exec spDescColumn N'ProjectFileSignatures', N'ProjectFileSignatureId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectFileSignatures', N'ProjectFileId'            , N'Идентификатор на файл проектно предложние.'
exec spDescColumn N'ProjectFileSignatures', N'Signature'                , N'Подпис.'
exec spDescColumn N'ProjectFileSignatures', N'FileName'                 , N'Име на подпис.'

GO
