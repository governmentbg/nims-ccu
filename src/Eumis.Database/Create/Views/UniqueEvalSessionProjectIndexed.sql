PRINT 'Create View [UniqueEvalSessionProjectIndexed]'
GO

IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'vwUniqueEvalSessionProjectIndexed'))
DROP VIEW vwUniqueEvalSessionProjectIndexed
GO

CREATE VIEW vwUniqueEvalSessionProjectIndexed WITH SCHEMABINDING
AS

SELECT esp.ProjectId
FROM [dbo].[EvalSessionProjects] esp
JOIN [dbo].[EvalSessions] es ON esp.EvalSessionId = es.EvalSessionId
WHERE es.EvalSessionStatus IN (1, 2, 3) AND esp.IsDeleted = 0

GO

GRANT SELECT ON vwUniqueEvalSessionProjectIndexed TO PUBLIC
GO

CREATE UNIQUE CLUSTERED INDEX [vwUniqueEvalSessionProjectIndexed_PK]
 ON [dbo].[vwUniqueEvalSessionProjectIndexed] 
(
 ProjectId ASC
)