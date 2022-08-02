ALTER TABLE [dbo].[EvalSessionUsers]
ADD [Status] INT;
GO

UPDATE [dbo].[EvalSessionUsers]
SET [Status] = (
    CASE
        WHEN es.EvalSessionStatus = 1 THEN 1
        ELSE 2
    END)
FROM [dbo].[EvalSessionUsers] esu
INNER JOIN [dbo].[EvalSessions] es ON esu.EvalSessionId = es.EvalSessionId;

ALTER TABLE [EvalSessionUsers]
ALTER COLUMN [Status] INT NOT NULL;
