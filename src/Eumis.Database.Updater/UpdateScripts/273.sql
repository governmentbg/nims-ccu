GO

ALTER TABLE [dbo].[CheckSheets]
ADD [ProjectId]               INT     NULL,
    [IrregularitySignalId]    INT     NULL,
CONSTRAINT [FK_CheckSheets_Projects]            FOREIGN KEY ([ProjectId])            REFERENCES [dbo].[Projects] ([ProjectId]),
CONSTRAINT [FK_CheckSheets_IrregularitySignals] FOREIGN KEY ([IrregularitySignalId]) REFERENCES [dbo].[IrregularitySignals] ([IrregularitySignalId]);
GO

ALTER TABLE [dbo].[CheckSheets] DROP CONSTRAINT [CHK_CheckSheets_Type]
GO
ALTER TABLE [dbo].[CheckSheets] WITH CHECK ADD CONSTRAINT [CHK_CheckSheets_Type] CHECK ([Type] IN (1, 2, 3, 4, 5, 6, 7, 8))
GO

ALTER TABLE [dbo].[ProgrammeCheckLists] DROP CONSTRAINT [CHK_ProgrammeCheckLists_Type]
GO
ALTER TABLE [dbo].[ProgrammeCheckLists] WITH CHECK ADD CONSTRAINT [CHK_ProgrammeCheckLists_Type] CHECK ([Type] IN (1, 2, 3, 4, 5, 6, 7, 8))
GO
