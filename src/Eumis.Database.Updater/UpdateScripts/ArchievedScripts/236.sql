ALTER TABLE [dbo].[ReimbursedAmounts] DROP CONSTRAINT [CHK_ReimbursedAmounts_Reimbursement]
GO

ALTER TABLE [dbo].[ReimbursedAmounts] WITH CHECK ADD CONSTRAINT [CHK_ReimbursedAmounts_Reimbursement] CHECK ([Reimbursement] IN (1, 2, 3, 4))
GO

ALTER TABLE [dbo].[FIReimbursedAmounts] DROP CONSTRAINT [CHK_FIReimbursedAmounts_Reimbursement]
GO

ALTER TABLE [dbo].[FIReimbursedAmounts] WITH CHECK ADD CONSTRAINT [CHK_FIReimbursedAmounts_Reimbursement] CHECK ([Reimbursement] IN (1, 2, 3, 4))
GO
