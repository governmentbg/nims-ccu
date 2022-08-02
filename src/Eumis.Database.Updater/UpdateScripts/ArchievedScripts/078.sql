GO

ALTER TABLE [dbo].[Projects]
DROP CONSTRAINT [CHK_Projects_EvalStatus];
GO

ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [CHK_Projects_EvalStatus] CHECK ([EvalStatus] IN (1, 2, 3));
GO

UPDATE p
SET p.EvalStatus = 2
FROM [dbo].[Projects] p
WHERE p.RegistrationStatus != 3 AND EXISTS(SELECT *
                                           FROM [dbo].[EvalSessionProjects] esp
                                           INNER JOIN [dbo].[EvalSessions] es ON esp.EvalSessionId = es.EvalSessionId
                                           WHERE esp.ProjectId = p.ProjectId AND esp.IsDeleted = 0 AND es.EvalSessionStatus = 3);
GO;
