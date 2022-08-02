GO
ALTER TABLE [dbo].[ContractContractors]
  DROP CONSTRAINT
    [CHK_ContractContractors_CompanyLegalStatuses];
GO
ALTER TABLE [dbo].[ContractContractors]
  DROP COLUMN
    [RepresentativeNames],
    [RepresentativeIDNumber],
    [CompanyLegalStatus];

GO
ALTER TABLE [dbo].[ContractPartners] DROP CONSTRAINT [CHK_ContractPartners_CompanyLegalStatuses];
GO
ALTER TABLE [dbo].[ContractPartners] DROP COLUMN [CompanyLegalStatus];
GO
ALTER TABLE [dbo].[ContractPartners] DROP CONSTRAINT [FK_ContractPartners_CompanySizeType];
GO
ALTER TABLE [dbo].[ContractPartners] DROP COLUMN [CompanySizeTypeId];

GO
ALTER TABLE [dbo].[ContractContracts]
  DROP COLUMN
    [ContractAmountWithoutVAT],
    [BudgetDifference];

GO
ALTER TABLE [dbo].[ContractProcurementPlans]
	DROP CONSTRAINT
	[CHK_ContractProcurementPlans_MAPreliminaryControl],
    [CHK_ContractProcurementPlans_PPAPreliminaryControl];
GO
ALTER TABLE [dbo].[ContractProcurementPlans]
  DROP COLUMN
    [Amount],
    [PlanDate],
    [MAPreliminaryControl],
    [PPAPreliminaryControl];
GO
DROP TABLE [dbo].[ContractProcurementPlanPublicDocuments];
GO
DROP TABLE [dbo].[ContractProcurementPlanAdditionalDocuments];
GO

ALTER TABLE [dbo].[Procurements] ADD 
    [Gid] UNIQUEIDENTIFIER NULL,
    [ShortName] NVARCHAR(100) NULL;
GO
ALTER TABLE [dbo].[Procurements] ALTER COLUMN [Gid] UNIQUEIDENTIFIER NOT NULL;
GO
ALTER TABLE [dbo].[Procurements] ALTER COLUMN [ShortName] NVARCHAR(100) NOT NULL;
GO
ALTER TABLE [dbo].[ProcurementDifferentiatedPositions] ADD [Gid] UNIQUEIDENTIFIER NULL;
GO
ALTER TABLE [dbo].[ProcurementDifferentiatedPositions] ALTER COLUMN [Gid] UNIQUEIDENTIFIER NOT NULL;
GO
ALTER TABLE [dbo].[ProcurementDocuments] ADD [Gid] UNIQUEIDENTIFIER NULL;
GO
ALTER TABLE [dbo].[ProcurementDocuments] ALTER COLUMN [Gid] UNIQUEIDENTIFIER NOT NULL;
GO

ALTER TABLE [dbo].[ContractReports] DROP CONSTRAINT 
    [CHK_ContractReports_ReportType];
GO
ALTER TABLE [dbo].[ContractReports]  WITH CHECK ADD  CONSTRAINT [CHK_ContractReports_ReportType] CHECK  ([ReportType] IN (1, 2, 3, 4, 5));
GO

GO
ALTER TABLE [dbo].[ProcedureBudgetLevel2] Drop
	[DF__Procedure__IsSta__762C88DA],
	[DF__Procedure__IsOne__7720AD13],
	[DF__Procedure__IsFla__7814D14C],
	[DF__Procedure__IsLan__7908F585],
	[DF__Procedure__IsEuA__7AF13DF7];
GO

GO
ALTER TABLE [dbo].[ProcedureBudgetLevel2] Drop COLUMN 
	[IsEligibleCost], 
	[IsStandardTablesExpense],
	[IsOneTimeExpense],
	[IsFlatRateExpense],
	[IsLandExpense],
	[IsEuApprovedOneTimeExpense],
	[IsEuApprovedStandardTablesExpense];
GO

GO
ALTER TABLE [dbo].[ContractBudgetLevel3Amounts] ADD [DirectionId] INT NULL;
GO
ALTER TABLE [dbo].[ContractBudgetLevel3Amounts] ADD
	CONSTRAINT [FK_ContractBudgetLevel3Amounts_Directions]            FOREIGN KEY ([DirectionId])   REFERENCES [dbo].[Directions] ([DirectionId]);
GO
GO
ALTER TABLE [dbo].[ContractBudgetLevel3Amounts] ADD [SubDirectionId] INT NULL;
GO
ALTER TABLE [dbo].[ContractBudgetLevel3Amounts] ADD
	CONSTRAINT [FK_ContractBudgetLevel3Amounts_SubDirections]            FOREIGN KEY ([SubDirectionId])   REFERENCES [dbo].[SubDirections] ([SubDirectionId]);
GO
