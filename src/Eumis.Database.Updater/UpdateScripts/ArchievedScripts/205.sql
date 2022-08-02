ALTER TABLE [dbo].[ContractReportMicrosType2Items] DROP CONSTRAINT [CHK_ContractReportMicrosType2Items_Occupation]
GO
ALTER TABLE [dbo].[ContractReportMicrosType2Items] ADD CONSTRAINT [CHK_ContractReportMicrosType2Items_Occupation] CHECK ([Occupation] IN (1, 2, 3, 4, 5, 6, 7, 8))
GO

ALTER TABLE [dbo].[ContractReportMicrosType2Items] DROP CONSTRAINT [CHK_ContractReportMicrosType2Items_ParticipationState]
GO
ALTER TABLE [dbo].[ContractReportMicrosType2Items] ADD CONSTRAINT [CHK_ContractReportMicrosType2Items_ParticipationState] CHECK ([ParticipationState] IN (1, 2, 3, 4, 5))
GO
