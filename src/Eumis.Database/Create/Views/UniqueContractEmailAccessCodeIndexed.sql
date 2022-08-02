PRINT 'Create View [UniqueContractEmailAccessCodeIndexed]'
GO

IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'vwUniqueContractEmailAccessCodeIndexed'))
DROP VIEW vwUniqueContractEmailAccessCodeIndexed
GO

CREATE VIEW vwUniqueContractEmailAccessCodeIndexed WITH SCHEMABINDING
AS

SELECT
    ac.[Code],
    ac.[Email],
    ac.[ContractAccessCodeId],
    ac.[IsActive],
    c.[ContractId],
    c.[Gid] AS ContractGid
FROM [dbo].[ContractAccessCodes] ac
JOIN [dbo].[Contracts] c ON ac.[ContractId] = c.[ContractId]

GO

GRANT SELECT ON vwUniqueContractEmailAccessCodeIndexed TO PUBLIC
GO

CREATE UNIQUE CLUSTERED INDEX [vwUniqueContractEmailAccessCodeIndexed_PK]
 ON [dbo].[vwUniqueContractEmailAccessCodeIndexed] 
(
    [Email] ASC,
    [ContractId] ASC
)
