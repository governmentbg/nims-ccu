GO

ALTER TABLE [dbo].[EvalSessionReportProjects]
ADD [ProjectVersionId]              INT                NULL,
    [Correspondence]                NVARCHAR(MAX)      NULL,
    [ProjectPlace]                  NVARCHAR(MAX)      NULL,
CONSTRAINT [FK_EvalSessionReportProjects_ProjectVersionXmls]           FOREIGN KEY ([ProjectVersionId])                       REFERENCES [dbo].[ProjectVersionXmls]  ([ProjectVersionXmlId]);
GO
