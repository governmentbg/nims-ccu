GO
ALTER TABLE [dbo].[IrregularityVersions] DROP CONSTRAINT [CHK_IrregularityVersions_Rapporteur];
GO
ALTER TABLE [dbo].[IrregularityVersions] ADD CONSTRAINT [CHK_IrregularityVersions_Rapporteur] CHECK ([Rapporteur] IN (1, 2, 3, 4, 5, 6, 7, 8, 9));
GO
