GO
DROP VIEW vwMonitoringMapNodeIndicators;
GO
ALTER TABLE [dbo].[Indicators] DROP CONSTRAINT 
    [UQ_Indicators_Code_Type_Kind],
    [CHK_Indicators_Type],
    [CHK_Indicators_Kind],
    [CHK_Indicators_Trend],
    [CHK_Indicators_AggregatedReport],
    [CHK_Indicators_AggregatedTarget],
    [DF__Indicator__HasSF__282DF8C2],
    [CHK_Indicators_ReportingType]
GO
ALTER TABLE [dbo].[Indicators] DROP COLUMN 
[Code], 
[Type],
[Kind],
[Trend],
[AggregatedReport],
[AggregatedTarget],
[HasQualitativeTarget],
[HasSF],
[ReportingType];
GO
DROP TABLE [dbo].[MapNodeIndicators];
GO
ALTER TABLE [dbo].[ContractReportIndicators] DROP COLUMN
[Type],
[Kind],
[Trend],
[AggregatedReport],
[AggregatedTarget];
GO

GO
ALTER TABLE [dbo].[PRocedures] DROP CONSTRAINT [CHK_Procedures_AllowedRegistrationType];
GO
ALTER TABLE [dbo].[PRocedures] DROP CONSTRAINT [CHK_Procedures_ProjectDuration];
GO
ALTER TABLE [dbo].[Procedures] ALTER COLUMN [AllowedRegistrationType] INT   NULL;
GO
ALTER TABLE [dbo].[Procedures] ALTER COLUMN [ProjectMinAmount]        MONEY NULL;
GO
ALTER TABLE [dbo].[Procedures] ALTER COLUMN [ProjectMaxAmount]        MONEY NULL;
GO
ALTER TABLE [dbo].[Procedures] ALTER COLUMN [ProjectDuration]         INT   NULL;
GO

GO
CREATE TABLE [dbo].[Procurements] (
    [ProcurementId]                INT           NOT NULL IDENTITY,
    [Name]                         NVARCHAR(200) NOT NULL,
    [Status]                       INT           NOT NULL,
    [ErrandAreaId]                 INT           NULL,
    [ErrandLegalActId]             INT           NULL,
    [ErrandTypeId]                 INT           NULL,  
    [PrognosysAmount]              MONEY         NULL,
    [PlanDate]                     DATETIME2     NULL,
    [Description]                  NVARCHAR(MAX) NULL,
    [PPANumber]                    NVARCHAR(100) NULL,
    [InternetAddress]              NVARCHAR(500) NULL,
    [ExpectedAmount]               MONEY         NULL,
    [OffersDeadlineDate]           DATETIME2     NULL,
    [AnnouncedDate]                DATETIME2     NULL,

    [CreateDate]                   DATETIME2     NOT NULL,
    [ModifyDate]                   DATETIME2     NOT NULL,
    [Version]                      ROWVERSION    NOT NULL,

    CONSTRAINT [PK_Procurements]                         PRIMARY KEY ([ProcurementId]),
    CONSTRAINT [FK_Procurements_ErrandAreas]             FOREIGN KEY ([ErrandAreaId])         REFERENCES [dbo].[ErrandAreas]       ([ErrandAreaId]),
    CONSTRAINT [FK_Procurements_ErrandLegalActs]         FOREIGN KEY ([ErrandLegalActId])     REFERENCES [dbo].[ErrandLegalActs]   ([ErrandLegalActId]),
    CONSTRAINT [FK_Procurements_ErrandTypes]             FOREIGN KEY ([ErrandTypeId])         REFERENCES [dbo].[ErrandTypes]       ([ErrandTypeId]),
    CONSTRAINT [CHK_Procurements_Status]                 CHECK       ([Status] IN (1, 2, 3)),
);
GO
CREATE TABLE [dbo].[ProcurementDifferentiatedPositions] (
    [ProcurementDifferentiatedPositionId]                INT           NOT NULL IDENTITY,
    [ProcurementId]                                      INT           NOT NULL,
    [Name]                                               NVARCHAR(MAX) NOT NULL,
    [Comment]                                            NVARCHAR(MAX) NULL,
    [CompanyId]                                          INT           NULL,
    
    CONSTRAINT [PK_ProcurementDifferentiatedPositions]                         PRIMARY KEY ([ProcurementDifferentiatedPositionId]),
    CONSTRAINT [FK_ProcurementDifferentiatedPositions_Procurements]            FOREIGN KEY ([ProcurementId])         REFERENCES [dbo].[Procurements]       ([ProcurementId]),
    CONSTRAINT [FK_ProcurementDifferentiatedPositions_Company]                 FOREIGN KEY ([CompanyId])             REFERENCES [dbo].[Companies]          ([CompanyId]),
);
GO
CREATE TABLE [dbo].[ProcurementDocuments] (
    [ProcurementDocumentId]                     INT                 NOT NULL IDENTITY,
    [ProcurementId]                             INT                 NOT NULL,
    [Name]                                      NVARCHAR(200)       NOT NULL,
    [Description]                               NVARCHAR(MAX)       NULL,
    [BlobKey]                                   UNIQUEIDENTIFIER    NULL

    CONSTRAINT [PK_ProcurementDocuments]                           PRIMARY KEY ([ProcurementDocumentId]),
    CONSTRAINT [FK_ProcurementDocuments_Procurements]              FOREIGN KEY ([ProcurementId])   REFERENCES [dbo].[Procurements] ([ProcurementId]),
    CONSTRAINT [FK_ProcurementDocuments_Blobs]                     FOREIGN KEY ([BlobKey])         REFERENCES [dbo].[Blobs] ([Key]),
);
GO

ALTER TABLE [dbo].[Companies]
DROP CONSTRAINT
    [FK_Companies_CompanySizeType],
    [FK_Companies_KidCodes],
    [CHK_Companies_CompanyLegalStatuses];
GO
ALTER TABLE [dbo].[Companies]
DROP COLUMN 
    [CompanySizeTypeId],
    [KidCodeId],
    [CompanyLegalStatus];
GO
ALTER TABLE [dbo].[Contracts]
DROP CONSTRAINT
    [FK_Contracts_CompanySizeType],
    [FK_Contracts_CompanyKidCodes],
    [CHK_Contracts_CompanyLegalStatuses];
GO
ALTER TABLE [dbo].[Contracts]
DROP COLUMN 
    [CompanySizeTypeId],
    [CompanyKidCodeId],
    [CompanyLegalStatus];
GO
ALTER TABLE [dbo].[Projects]
DROP CONSTRAINT
    [FK_Projects_CompanySizeType],
    [FK_Projects_CompanyKidCodes];
GO
ALTER TABLE [dbo].[Projects]
DROP COLUMN 
    [CompanySizeTypeId],
    [CompanyKidCodeId];
GO

ALTER TABLE [dbo].[ContractBudgetLevel3Amounts] DROP CONSTRAINT
   [FK_ContractBudgetLevel3Amounts_InterventionField],
   [FK_ContractBudgetLevel3Amounts_FormOfFinance],
   [FK_ContractBudgetLevel3Amounts_TerritorialDimension],
   [FK_ContractBudgetLevel3Amounts_TerritorialDeliveryMechanism],
   [FK_ContractBudgetLevel3Amounts_ThematicObjective],
   [FK_ContractBudgetLevel3Amounts_ESFSecondaryTheme],
   [FK_ContractBudgetLevel3Amounts_EconomicDimension];
GO
ALTER TABLE [dbo].[ContractBudgetLevel3Amounts] DROP COLUMN 
    [InterventionFieldId],
    [FormOfFinanceId],
    [TerritorialDimensionId],
    [TerritorialDeliveryMechanismId],
    [ThematicObjectiveId],
    [ESFSecondaryThemeId],
    [EconomicDimensionId];
GO
ALTER TABLE [dbo].[ContractVersionXmlAmounts] DROP CONSTRAINT
   [FK_ContractVersionXmlAmounts_InterventionField],
   [FK_ContractVersionXmlAmounts_FormOfFinance],
   [FK_ContractVersionXmlAmounts_TerritorialDimension],
   [FK_ContractVersionXmlAmounts_TerritorialDeliveryMechanism],
   [FK_ContractVersionXmlAmounts_ThematicObjective],
   [FK_ContractVersionXmlAmounts_ESFSecondaryTheme],
   [FK_ContractVersionXmlAmounts_EconomicDimension];
GO
ALTER TABLE [dbo].[ContractVersionXmlAmounts] DROP COLUMN 
    [InterventionFieldId],
    [FormOfFinanceId],
    [TerritorialDimensionId],
    [TerritorialDeliveryMechanismId],
    [ThematicObjectiveId],
    [ESFSecondaryThemeId],
    [EconomicDimensionId];
GO
ALTER TABLE [dbo].[contracts] DROP CONSTRAINT 
    [CHK_Contracts_ContractType];
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [CHK_Contracts_ContractType] CHECK  (([ContractType]=(8) OR [ContractType]=(3)));
GO
ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [CHK_Contracts_ContractType];
GO
ALTER TABLE [dbo].[directions] ADD [Gid] UNIQUEIDENTIFIER;
GO
ALTER TABLE [dbo].[subdirections] ADD [Gid] UNIQUEIDENTIFIER;
GO
ALTER TABLE [dbo].[directions] ALTER COLUMN [Gid] UNIQUEIDENTIFIER NOT NULL;
GO
ALTER TABLE [dbo].[subdirections] ALTER COLUMN [Gid] UNIQUEIDENTIFIER NOT NULL;
GO
