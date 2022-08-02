PRINT 'ActuallyPaidAmountDocuments'
GO

CREATE TABLE [dbo].[ActuallyPaidAmountDocuments] (
    [ActuallyPaidAmountDocumentId]          INT                 NOT NULL    IDENTITY,
    [ActuallyPaidAmountId]                  INT                 NOT NULL,
    [Name]                                  NVARCHAR(200)       NOT NULL,
    [Description]                           NVARCHAR(MAX)       NULL,
    [BlobKey]                               UNIQUEIDENTIFIER    NULL

    CONSTRAINT [PK_ActuallyPaidAmountDocuments]                     PRIMARY KEY       ([ActuallyPaidAmountDocumentId]),
    CONSTRAINT [FK_ActuallyPaidAmountDocuments_ActuallyPaidAmount]  FOREIGN KEY       ([ActuallyPaidAmountId])  REFERENCES [dbo].[ActuallyPaidAmounts] ([ActuallyPaidAmountId]),
    CONSTRAINT [FK_ActuallyPaidAmountDocuments_Blobs]               FOREIGN KEY       ([BlobKey])               REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'ActuallyPaidAmountDocuments', N'Документи към реално изплатени суми.'
exec spDescColumn N'ActuallyPaidAmountDocuments', N'ActuallyPaidAmountDocumentId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ActuallyPaidAmountDocuments', N'ActuallyPaidAmountId'			   , N'Идентификатор на реално изплатена сума.'
exec spDescColumn N'ActuallyPaidAmountDocuments', N'Name'                              , N'Наименование.'
exec spDescColumn N'ActuallyPaidAmountDocuments', N'Description'                       , N'Описание.'
exec spDescColumn N'ActuallyPaidAmountDocuments', N'BlobKey'                           , N'Идентификатор на файл.'

GO