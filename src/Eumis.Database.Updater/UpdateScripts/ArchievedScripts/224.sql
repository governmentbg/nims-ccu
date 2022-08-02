GO
ALTER TABLE [dbo].[RegProjectXmls]          ADD     [ProjectNameAlt]        NVARCHAR(MAX)   NULL
GO

ALTER TABLE [dbo].[RegProjectXmls]          ADD     [CompanyNameAlt]        NVARCHAR(200)   NULL
GO

ALTER TABLE [dbo].[ExpenseTypes]            ADD     [NameAlt]               NVARCHAR(MAX)   NULL
GO

ALTER TABLE [dbo].[ExpenseSubTypes]         ADD     [NameAlt]               NVARCHAR(MAX)   NULL
GO

ALTER TABLE [dbo].[ProcedureBudgetLevel2]   ADD     [NameAlt]               NVARCHAR(MAX)   NULL
GO

ALTER TABLE [dbo].[ProcedureSpecFields]     ADD     [TitleAlt]              NVARCHAR(MAX)   NULL
GO

ALTER TABLE [dbo].[ProcedureSpecFields]     ADD     [DescriptionAlt]        NVARCHAR(MAX)   NULL
GO

ALTER TABLE [dbo].[Projects]                ADD     [NameAlt]               NVARCHAR(MAX)   NULL
GO

ALTER TABLE [dbo].[Projects]                ADD     [CompanyNameAlt]        NVARCHAR(200)   NULL
GO

ALTER TABLE [dbo].[ProjectVersionXmls]      ADD     [CreateNoteAlt]         NVARCHAR(MAX)   NULL
GO

ALTER TABLE [dbo].[ProjectTypes]            ADD     [NameAlt]               NVARCHAR(MAX)   NOT NULL CONSTRAINT [DEFAULT_ProjectTypes] DEFAULT ''
GO

UPDATE [ProjectTypes] SET
    NameAlt = ListData.NameAlt
FROM (VALUES
    (1, 'Project proposal'),
    (2, 'Proposal for preliminary selection'),
    (3, 'Project fiche')) AS ListData(ProjectTypeId, NameAlt)
WHERE
    ListData.ProjectTypeId = ProjectTypes.ProjectTypeId
GO

ALTER TABLE [dbo].[ProjectTypes] DROP CONSTRAINT [DEFAULT_ProjectTypes]
GO