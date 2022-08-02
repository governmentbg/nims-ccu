GO

IF EXISTS (SELECT [CompensationDocumentId] FROM [dbo].[CompensationDocuments])
BEGIN
    THROW 50000,'Cannot update database. There must be no CompensationDocuments',1;
END
GO

ALTER TABLE [dbo].[CompensationDocuments]
DROP CONSTRAINT [CHK_CompensationDocuments_FinanceSource];
GO

ALTER TABLE [dbo].[CompensationDocuments]
DROP COLUMN [FinanceSource];
GO

ALTER TABLE [dbo].[CompensationDocuments] ADD
    [ProgrammePriorityId]   INT  NOT NULL CONSTRAINT DEFAULT_ProgrammePriorityId DEFAULT 0,
    [FinanceSource]         INT  NOT NULL CONSTRAINT DEFAULT_FinanceSource       DEFAULT 0,
    CONSTRAINT [FK_CompensationDocuments_ProgrammePriorities]    FOREIGN KEY ([ProgrammePriorityId])     REFERENCES [dbo].[MapNodes]   ([MapNodeId]),
    CONSTRAINT [CHK_CompensationDocuments_FinanceSource]         CHECK       ([FinanceSource]     IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10));
GO

ALTER TABLE [dbo].[CompensationDocuments]
DROP CONSTRAINT DEFAULT_ProgrammePriorityId,
     CONSTRAINT DEFAULT_FinanceSource;
GO