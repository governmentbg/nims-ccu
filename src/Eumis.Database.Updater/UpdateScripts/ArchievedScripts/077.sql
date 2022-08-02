GO

--This script will remove the identity constraint from [dbo].[Companies] table) and
--add a new sequence that will be used to populate ids in [dbo].[Companies] instead of the identity constraint

--Drop constraints from tables that have FK to Companies
ALTER TABLE [dbo].[ProjectActivities]
DROP CONSTRAINT [FK_ProjectActivities_Companies]
GO

ALTER TABLE [dbo].[ProjectPartners]
DROP CONSTRAINT [FK_ProjectPartners_Companies]
GO

ALTER TABLE [dbo].[Projects]
DROP CONSTRAINT [FK_Projects_Companies]
GO

ALTER TABLE [dbo].[CompanyPersons]
DROP CONSTRAINT [FK_CompanyPersons_Companies]
GO

ALTER TABLE [dbo].[Contracts]
DROP CONSTRAINT [FK_Contracts_Companies]
GO

ALTER TABLE [dbo].[ContractActivities]
DROP CONSTRAINT [FK_ContractActivities_Companies]
GO

ALTER TABLE [dbo].[ContractPartners]
DROP CONSTRAINT [FK_ContractPartners_Companies]
GO

ALTER TABLE [dbo].[ProcedureSpecificBeneficiaries]
DROP CONSTRAINT [FK_ProcedureSpecificBeneficiaries_Companies]
GO



--Change Companies accordingly - add new column, copy identity columns values to the new column, remove PK constraint,
--remove old identity column, rename new column, add new PK constraint
ALTER TABLE [dbo].[Companies]
DROP CONSTRAINT [PK_Companies]
GO

ALTER TABLE [dbo].[Companies]
ADD [CompanyIdNew] INT NOT NULL CONSTRAINT DEFAULT_Companies DEFAULT 0;
GO

ALTER TABLE [dbo].[Companies]
DROP CONSTRAINT DEFAULT_Companies
GO

UPDATE [dbo].[Companies]
SET [CompanyIdNew] = [CompanyId]
GO

ALTER TABLE [dbo].[Companies] DROP COLUMN [CompanyId];
GO

EXEC sp_rename '[Companies].CompanyIdNew', 'CompanyId', 'COLUMN'
GO

ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [PK_Companies] PRIMARY KEY ([CompanyId]);
GO


--Add new constraints in tables that have FK to Companies
ALTER TABLE [dbo].[ProcedureSpecificBeneficiaries]
ADD CONSTRAINT [FK_ProcedureSpecificBeneficiaries_Companies]    FOREIGN KEY ([CompanyId])   REFERENCES [dbo].[Companies] ([CompanyId]);
GO

ALTER TABLE [dbo].[ContractPartners]
ADD CONSTRAINT [FK_ContractPartners_Companies]   FOREIGN KEY ([CompanyId])           REFERENCES [dbo].[Companies] ([CompanyId]);
GO

ALTER TABLE [dbo].[ContractActivities]
ADD CONSTRAINT [FK_ContractActivities_Companies]     FOREIGN KEY ([CompanyId])       REFERENCES [dbo].[Companies] ([CompanyId]);
GO

ALTER TABLE [dbo].[Contracts]
ADD CONSTRAINT [FK_Contracts_Companies]          FOREIGN KEY ([CompanyId])           REFERENCES [dbo].[Companies] ([CompanyId]);
GO

ALTER TABLE [dbo].[CompanyPersons]
ADD CONSTRAINT [FK_CompanyPersons_Companies] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Companies] ([CompanyId]);
GO

ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [FK_Projects_Companies]  FOREIGN KEY ([CompanyId])           REFERENCES [dbo].[Companies] ([CompanyId]);
GO

ALTER TABLE [dbo].[ProjectPartners]
ADD CONSTRAINT [FK_ProjectPartners_Companies]   FOREIGN KEY ([CompanyId])           REFERENCES [dbo].[Companies] ([CompanyId]);
GO

ALTER TABLE [dbo].[ProjectActivities]
ADD CONSTRAINT [FK_ProjectActivities_Companies]     FOREIGN KEY ([CompanyId])       REFERENCES [dbo].[Companies] ([CompanyId]);
GO


-- Add CompanySequence
DECLARE @max int;
SELECT @max = ISNULL(MAX(CompanyId), 0) + 10 FROM Companies;

exec('CREATE SEQUENCE [dbo].[CompanySequence] START WITH ' + @max)
