GO

ALTER TABLE [dbo].[EvalSessionProjectStandings] ADD 
    [ProjectVersionXmlId]                                INT                                      NOT NULL;
GO

ALTER TABLE [dbo].[EvalSessionProjectStandings] ADD CONSTRAINT
    [FK_EvalSessionProjectStandings_ProjectVersions]     FOREIGN KEY ([ProjectVersionXmlId])      REFERENCES [dbo].[ProjectVersionXmls] ([ProjectVersionXmlId])
GO
