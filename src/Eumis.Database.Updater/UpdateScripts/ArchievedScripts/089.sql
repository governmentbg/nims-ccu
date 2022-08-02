GO

ALTER TABLE [dbo].[ErrandAreas] ADD [Code] NVARCHAR(50) NOT NULL UNIQUE;

ALTER TABLE [dbo].[ErrandTypes] ADD
    [ErrandLegalActId]  INT             NOT NULL,
    [Code]              NVARCHAR(50)    NOT NULL UNIQUE;

ALTER TABLE [dbo].[ErrandTypes] ADD
    CONSTRAINT [FK_ErrandTypes_ErrandLegalActs] FOREIGN KEY ([ErrandLegalActId]) REFERENCES [dbo].[ErrandLegalActs] ([ErrandLegalActId]);

CREATE TABLE [dbo].[ContractProcurementPlans] (
    [ContractProcurementPlanId] INT                 NOT NULL IDENTITY,
    [ContractId]                INT                 NOT NULL,
    [Gid]                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,

    [Name]                      NVARCHAR(MAX)       NOT NULL,
    [ErrandAreaId]              INT                 NOT NULL,
    [ErrandLegalActId]          INT                 NOT NULL,
    [ErrandTypeId]              INT                 NOT NULL,
    [Amount]                    MONEY               NOT NULL,
    [Description]               NVARCHAR(MAX)       NOT NULL,
    [MAPreliminaryControl]      INT                 NOT NULL,
    [PPAPreliminaryControl]     INT                 NOT NULL,
    [InternetAddress]           NVARCHAR(MAX)       NOT NULL,
    [ExpectedAmount]            MONEY               NOT NULL,
    [PPANumber]                 NVARCHAR(MAX)       NULL,
    [PlanDate]                  DATETIME2           NULL,
    [NoticeDate]                DATETIME2           NULL,

    CONSTRAINT [PK_ContractProcurementPlans]                        PRIMARY KEY ([ContractProcurementPlanId]),
    CONSTRAINT [FK_ContractProcurementPlans_Contracts]              FOREIGN KEY ([ContractId])                  REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractProcurementPlans_ErrandAreas]            FOREIGN KEY ([ErrandAreaId])                REFERENCES [dbo].[ErrandAreas] ([ErrandAreaId]),
    CONSTRAINT [FK_ContractProcurementPlans_ErrandLegalActs]        FOREIGN KEY ([ErrandLegalActId])            REFERENCES [dbo].[ErrandLegalActs] ([ErrandLegalActId]),
    CONSTRAINT [FK_ContractProcurementPlans_ErrandTypes]            FOREIGN KEY ([ErrandTypeId])                REFERENCES [dbo].[ErrandTypes] ([ErrandTypeId]),
    CONSTRAINT [CHK_ContractProcurementPlans_MAPreliminaryControl]  CHECK       ([MAPreliminaryControl] IN (1, 2, 3)),
    CONSTRAINT [CHK_ContractProcurementPlans_PPAPreliminaryControl] CHECK       ([PPAPreliminaryControl] IN (1, 2, 3))
);
GO

CREATE TABLE [dbo].[ContractDifferentiatedPositions] (
    [ContractDifferentiatedPositionId]  INT                 NOT NULL IDENTITY,
    [ContractProcurementPlanId]         INT                 NOT NULL,
    [ContractContractId]                INT                 NULL,
    [Gid]                               UNIQUEIDENTIFIER    NOT NULL UNIQUE,

    [Name]                              NVARCHAR(MAX)       NOT NULL,
    [SubmittedOffersCount]              INT                 NULL,
    [RankedOffersCount]                 INT                 NULL,
    [Comment]                           NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ContractDifferentiatedPositions]                             PRIMARY KEY ([ContractDifferentiatedPositionId]),
    CONSTRAINT [FK_ContractDifferentiatedPositions_ContractProcurementPlans]    FOREIGN KEY ([ContractProcurementPlanId])   REFERENCES [dbo].[ContractProcurementPlans] ([ContractProcurementPlanId]),
    CONSTRAINT [FK_ContractDifferentiatedPositions_ContractContracts]           FOREIGN KEY ([ContractContractId])          REFERENCES [dbo].[ContractContracts] ([ContractContractId])
);
GO

SET IDENTITY_INSERT [ErrandAreas] ON
INSERT INTO [ErrandAreas]
    ([ErrandAreaId], [Code], [Name]         )
VALUES
    (1             , N'0'  , N'Доставка'    ),
    (2             , N'1'  , N'Услуга'      ),
    (3             , N'2'  , N'Строителство');
SET IDENTITY_INSERT [ErrandAreas] OFF
GO

SET IDENTITY_INSERT [ErrandTypes] ON
INSERT INTO [ErrandTypes]
    ([ErrandTypeId], [ErrandLegalActId], [Code], [Name])
VALUES
    -- ЗОП
    (1             , 1                 , N'01' , N'Открита процедура'),
    (2             , 1                 , N'02' , N'Ограничена процедура'),
    (3             , 1                 , N'03' , N'Конкурс за проект'),
    (4             , 1                 , N'04' , N'Процедура на Договаряне с обявление'),
    (5             , 1                 , N'05' , N'Процедура на Договаряне без обявление'),
    (6             , 1                 , N'06' , N'Състезателен Диалог'),
    (7             , 1                 , N'07' , N'Друго (По указание на УО)'),

    -- ПМС
    (8             , 2                 , N'11' , N'Избор с публична покана'),
    (9             , 2                 , N'12' , N'Избор без провеждане на процедура (по указание на УО)');
SET IDENTITY_INSERT [ErrandTypes] OFF
GO

