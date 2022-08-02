GO
CREATE UNIQUE INDEX [UQ_RegProjectXmls_ProjectId]
    ON [dbo].[RegProjectXmls]([ProjectId]) WHERE [ProjectId] IS NOT NULL
GO
