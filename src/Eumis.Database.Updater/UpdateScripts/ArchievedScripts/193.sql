ALTER TABLE [dbo].[ContractReportMicrosType2Items] DROP CONSTRAINT [CHK_ContractReportMicrosType2Items_CancelationReason];
GO

ALTER TABLE [dbo].[ContractReportMicrosType2Items] ADD CONSTRAINT [CHK_ContractReportMicrosType2Items_CancelationReason] CHECK ([CancelationReason] IN (1, 2, 3, 4, 5, 6));
GO

ALTER TABLE [dbo].[ContractReportMicrosType2Items] DROP CONSTRAINT [CHK_ContractReportMicrosType2Items_LeavingState];
GO

ALTER TABLE [dbo].[ContractReportMicrosType2Items] ADD CONSTRAINT [CHK_ContractReportMicrosType2Items_LeavingState] CHECK ([LeavingState] IN (1, 2, 3, 4, 5, 6, 7, 8));
GO
