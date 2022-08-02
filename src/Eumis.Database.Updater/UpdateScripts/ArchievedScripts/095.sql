GO

DECLARE @DropContractIndicatorsGidUniqueCmd NVARCHAR(MAX);
SELECT @DropContractIndicatorsGidUniqueCmd = 'ALTER TABLE ContractIndicators DROP CONSTRAINT ' + d.name
    FROM sys.tables t 
    JOIN sys.indexes d on d.object_id = t.object_id  and d.type=2 and d.is_unique=1
    JOIN sys.index_columns ic on d.index_id=ic.index_id and ic.object_id=t.object_id
    JOIN sys.columns c on ic.column_id = c.column_id  and c.object_id=t.object_id
    WHERE t.name = 'ContractIndicators' and c.name= 'Gid';
EXECUTE (@DropContractIndicatorsGidUniqueCmd);

GO

ALTER TABLE ContractIndicators ADD CONSTRAINT [UQ_ContractIndicators_ContractId_Gid] UNIQUE ([ContractId], [Gid]);

GO
