CREATE TABLE [dbo].[ActuallyPaidAmountDocuments] (
    [ActuallyPaidAmountDocumentId]      INT                 NOT NULL IDENTITY,
    [ActuallyPaidAmountId]              INT                 NOT NULL,
    [Name]                              NVARCHAR(200)       NOT NULL,
    [Description]                       NVARCHAR(MAX)       NULL,
    [BlobKey]                           UNIQUEIDENTIFIER    NULL

    CONSTRAINT [PK_ActuallyPaidAmountDocuments]                     PRIMARY KEY ([ActuallyPaidAmountDocumentId]),
    CONSTRAINT [FK_ActuallyPaidAmountDocuments_ActuallyPaidAmount]  FOREIGN KEY ([ActuallyPaidAmountId])    REFERENCES [dbo].[ActuallyPaidAmounts] ([ActuallyPaidAmountId]),
    CONSTRAINT [FK_ActuallyPaidAmountDocuments_Blobs]               FOREIGN KEY ([BlobKey])                 REFERENCES [dbo].[Blobs] ([Key]),
);
GO
