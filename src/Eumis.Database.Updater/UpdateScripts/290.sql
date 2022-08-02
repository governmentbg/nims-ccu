GO
ALTER TABLE [dbo].[Procedures] ADD [HasBudgetComponents] BIT NOT NULL DEFAULT 0;

GO
PRINT 'ProcedureBudgetComponents'
CREATE TABLE [dbo].[ProcedureBudgetComponents] (
    [ProcedureBudgetComponentId]    INT                NOT NULL    IDENTITY,
    [ProcedureId]                   INT                NOT NULL,
    [Name]                          NVARCHAR(1000)     NULL,
    [Amount]                        MONEY              NULL,
    [Notes]                         NVARCHAR(MAX)      NULL,
    [Status]                        INT                NOT NULL,

    [CreateDate]                    DATETIME2          NOT NULL,
    [ModifyDate]                    DATETIME2          NOT NULL,
    [Version]                       ROWVERSION         NOT NULL,

    CONSTRAINT [PK_ProcedureBudgetComponents]                      PRIMARY KEY ([ProcedureBudgetComponentId]),
    CONSTRAINT [FK_ProcedureBudgetComponents_Procedures]           FOREIGN KEY ([ProcedureId])      REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [CHK_ProcedureBudgetComponents_Status]              CHECK       ([Status] IN (1, 2, 3))
)

GO
PRINT 'ProcedureBudgetSizeTypes'
CREATE TABLE [dbo].[ProcedureBudgetSizeTypes] (
    [ProcedureBudgetSizeTypeId]     INT                NOT NULL    IDENTITY,
    [ProcedureBudgetComponentId]    INT                NOT NULL,
    [CompanySizeTypeId]             INT                NOT NULL,

    CONSTRAINT [PK_ProcedureBudgetSizeTypes]                                     PRIMARY KEY ([ProcedureBudgetSizeTypeId]),
    CONSTRAINT [FK_ProcedureBudgetSizeTypes_ProcedureBudgetComponents]           FOREIGN KEY ([ProcedureBudgetComponentId])      REFERENCES [dbo].[ProcedureBudgetComponents] ([ProcedureBudgetComponentId]),
    CONSTRAINT [FK_ProcedureBudgetSizeTypes_CompanySizeTypes]                    FOREIGN KEY ([CompanySizeTypeId])               REFERENCES [dbo].[CompanySizeTypes] ([CompanySizeTypeId]),
)

CREATE UNIQUE NONCLUSTERED INDEX [UQ_ProcedureBudgetSizeTypes_ProcedureBudgetComponentId_CompanySizeTypeId]
ON [ProcedureBudgetSizeTypes]([ProcedureBudgetComponentId], [CompanySizeTypeId])

GO
PRINT 'ProcedureBudgetKidCodes'
CREATE TABLE [dbo].[ProcedureBudgetKidCodes] (
    [ProcedureBudgetKidCodeId]      INT                NOT NULL    IDENTITY,
    [ProcedureBudgetComponentId]    INT                NOT NULL,
    [KidCodeId]                     INT                NOT NULL,

    CONSTRAINT [PK_ProcedureBudgetKidCodes]                                      PRIMARY KEY ([ProcedureBudgetKidCodeId]),
    CONSTRAINT [FK_ProcedureBudgetKidCodes_ProcedureBudgetComponents]            FOREIGN KEY ([ProcedureBudgetComponentId])      REFERENCES [dbo].[ProcedureBudgetComponents] ([ProcedureBudgetComponentId]),
    CONSTRAINT [FK_ProcedureBudgetKidCodes_KidCodes]                             FOREIGN KEY ([KidCodeId])                       REFERENCES [dbo].[KidCodes] ([KidCodeId])
)
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ProcedureBudgetKidCodes_ProcedureBudgetComponentId_KidCodeId]
ON [ProcedureBudgetKidCodes]([ProcedureBudgetComponentId], [KidCodeId])

IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'vwUniqueProcedureBudgetSizeTypeKidCodeIndexed'))
DROP VIEW vwUniqueProcedureBudgetSizeTypeKidCodeIndexed
GO

CREATE VIEW vwUniqueProcedureBudgetSizeTypeKidCodeIndexed WITH SCHEMABINDING
AS

SELECT
    pbc.[ProcedureId],
    pbc.[ProcedureBudgetComponentId],
    bkc.[KidCodeId],
    bst.[CompanySizeTypeId],
    pbc.[Status]
FROM [dbo].[ProcedureBudgetComponents] pbc
JOIN [dbo].[ProcedureBudgetKidCodes] bkc on pbc.ProcedureBudgetComponentId = bkc.ProcedureBudgetComponentId
JOIN [dbo].[ProcedureBudgetSizeTypes] bst on pbc.ProcedureBudgetComponentId = bst.ProcedureBudgetComponentId  
WHERE pbc.[Status] < 3
GO

GRANT SELECT ON vwUniqueContractEmailAccessCodeIndexed TO PUBLIC
GO

CREATE UNIQUE CLUSTERED INDEX [vwUniqueProcedureBudgetSizeTypeKidCodeIndexed_PK]
 ON [dbo].vwUniqueProcedureBudgetSizeTypeKidCodeIndexed 
(
    [ProcedureId] ASC,
    [KidCodeId] ASC,
    [CompanySizeTypeId] ASC
)

GO
ALTER TABLE [dbo].[evalSessionProjectStandings] 
    ADD [ProcedureBudgetComponentId] INT NULL
    Constraint [FK_evalSessionProjectStandings_ProcedureBudgetComponents] FOREIGN KEY([ProcedureBudgetComponentId]) REFERENCES [dbo].[ProcedureBudgetComponents] ([ProcedureBudgetComponentId]);

GO
DROP INDEX [UQ_EvalSessionProjectStandings_EvalSessionId_IsPreliminary_OrderNum] ON [dbo].[EvalSessionProjectStandings]

GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_EvalSessionProjectStandings_EvalSessionId_IsPreliminary_OrderNum] ON [dbo].[EvalSessionProjectStandings]
(
    [EvalSessionId] ASC,
    [IsPreliminary] ASC,
    [ProcedureBudgetComponentId] ASC,
    [OrderNum] ASC
)
WHERE ([OrderNum] IS NOT NULL AND [IsDeleted]=(0))
GO
