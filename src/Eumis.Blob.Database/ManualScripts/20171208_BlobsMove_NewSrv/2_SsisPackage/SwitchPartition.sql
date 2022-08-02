USE [$(dbName)]
GO

PRINT 'SWITCH BlobContents_$(partitionId)'
GO

ALTER TABLE [dbo].[BlobContents_$(partitionId)]
SWITCH TO [dbo].[BlobContents]
PARTITION $PARTITION.pfBlobContents($(partitionId))

PRINT 'DROP BlobContents_$(partitionId)'
GO

DROP TABLE [dbo].[BlobContents_$(partitionId)];
GO
