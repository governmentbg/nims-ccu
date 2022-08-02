DECLARE @command  NVARCHAR(1000)
DECLARE @name NVARCHAR(1000)

-- Return the name of unique constraint.
SELECT @name = name
FROM sys.objects
WHERE type = 'UQ' AND OBJECT_NAME(parent_object_id) = N'ContractBudgetLevel3Amounts';

-- Delete the unique constraint.
select @command = 'ALTER TABLE [dbo].[ContractBudgetLevel3Amounts] DROP CONSTRAINT ' + @name

EXECUTE (@command)

ALTER TABLE [dbo].[ContractBudgetLevel3Amounts] ADD
    CONSTRAINT [UQ_ContractBudgetLevel3Amounts_ContractId_Gid]         UNIQUE ([ContractId], [Gid]);
GO