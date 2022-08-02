UPDATE
    c
SET
    c.ContractDate = xmls.ContractDate
FROM 
    [dbo].[Contracts] AS c
    INNER JOIN [dbo].[ContractVersionXmls] AS xmls 
    ON c.ContractId = xmls.ContractId 
        AND (c.ContractDate <> xmls.ContractDate OR (c.ContractDate IS NULL AND xmls.ContractDate IS NOT NULL))
        AND xmls.VersionType = 1 --Нов договор
        AND c.ContractStatus = 2 --Въведен

GO
