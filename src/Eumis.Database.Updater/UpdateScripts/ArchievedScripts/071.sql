GO

--This script will add a new column named [EvalSessionUserId] in [dbo].[EvalSessionUsers] table (ESU),
--which requires to drop constraints from tables that have FK to ESU, then change ESU accordingly
--and then add the new columns and constraints in the tables that have FK to ESU

--Drop constraints from tables that have FK to ESU
----EvalSessionDistributionUsers
ALTER TABLE [dbo].[EvalSessionDistributionUsers]
DROP
  CONSTRAINT [PK_EvalSessionDistributionUsers]
GO

ALTER TABLE [dbo].[EvalSessionDistributionUsers]
DROP
  CONSTRAINT [FK_EvalSessionDistributionUsers_Users]
GO



----EvalSessionSheets
ALTER TABLE [dbo].[EvalSessionSheets]
DROP
  CONSTRAINT [FK_EvalSessionSheets_Users]
GO


--Change ESU accordingly - drop PK constraint, add new column, add new PK constraint
----EvalSessionUsers
ALTER TABLE [dbo].[EvalSessionUsers]
DROP
  CONSTRAINT [PK_EvalSessionUsers]
GO

ALTER TABLE [dbo].[EvalSessionUsers] ADD
    [EvalSessionUserId] INT NOT NULL IDENTITY;
GO

ALTER TABLE [dbo].[EvalSessionUsers] ADD
    CONSTRAINT [PK_EvalSessionUsers] PRIMARY KEY ([EvalSessionUserId]);
GO

ALTER TABLE [dbo].[EvalSessionUsers] ADD
    CONSTRAINT [UQ_EvalSessionUsers_EvalSeesuin_User_Type]  UNIQUE      ([EvalSessionId], [UserId], [Type]);
GO



--Add new columns and constraints, remove old columns to tables that have FK to ESU
----EvalSessionDistributionUsers
ALTER TABLE [dbo].[EvalSessionDistributionUsers] ADD
    [EvalSessionUserId] INT NOT NULL CONSTRAINT DEFAULT_EvalSessionUser DEFAULT 0;
GO

ALTER TABLE [dbo].[EvalSessionDistributionUsers]
DROP
  CONSTRAINT DEFAULT_EvalSessionUser
GO

UPDATE [dbo].[EvalSessionDistributionUsers] SET [EvalSessionUserId] = (SELECT esu.[EvalSessionUserId] FROM [dbo].[EvalSessionUsers] esu WHERE esu.[UserId] = [EvalSessionDistributionUsers].[UserId] and esu.[EvalSessionId] = [EvalSessionDistributionUsers].[EvalSessionId])
WHERE [EvalSessionUserId] = 0
GO

ALTER TABLE [EvalSessionDistributionUsers] DROP COLUMN [UserId];
GO

ALTER TABLE [dbo].[EvalSessionDistributionUsers] ADD
    CONSTRAINT [PK_EvalSessionDistributionUsers] PRIMARY KEY ([EvalSessionId], [EvalSessionDistributionId], [EvalSessionUserId]);
GO

ALTER TABLE [dbo].[EvalSessionDistributionUsers] ADD
    CONSTRAINT [FK_EvalSessionDistributionUsers_Users]     FOREIGN KEY ([EvalSessionUserId])        REFERENCES [dbo].[EvalSessionUsers] ([EvalSessionUserId]);
GO




----EvalSessionSheets
ALTER TABLE [dbo].[EvalSessionSheets] ADD
    [EvalSessionUserId] INT NOT NULL CONSTRAINT DEFAULT_EvalSessionUser DEFAULT 0;
GO

ALTER TABLE [dbo].[EvalSessionSheets]
DROP
  CONSTRAINT DEFAULT_EvalSessionUser
GO

UPDATE [dbo].[EvalSessionSheets] SET [EvalSessionUserId] = (SELECT esu.[EvalSessionUserId] FROM [dbo].[EvalSessionUsers] esu WHERE esu.[UserId] = [EvalSessionSheets].[UserId] and esu.[EvalSessionId] = [EvalSessionSheets].[EvalSessionId])
WHERE [EvalSessionUserId] = 0
GO

ALTER TABLE [EvalSessionSheets] DROP COLUMN [UserId];
GO

ALTER TABLE [dbo].[EvalSessionSheets] ADD
    CONSTRAINT [FK_EvalSessionSheets_Users]      FOREIGN KEY ([EvalSessionUserId])        REFERENCES [dbo].[EvalSessionUsers] ([EvalSessionUserId]);
GO
