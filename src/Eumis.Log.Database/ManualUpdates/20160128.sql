--executed on 2016/01/28 ~ 15:00
UPDATE al
SET al.[AggregateRootId] = pc.[ProjectId],   
    al.PostData = replace(SubString([PostData],0 ,20), Left(SubString([PostData], PatIndex('%[0-9.-]%', [PostData]), 8000), PatIndex('%[^0-9.-]%', SubString([PostData], PatIndex('%[0-9.-]%', [PostData]), 8000) + 'X')-1), pc.[ProjectId]) + SubString([PostData], 20, 9999999)
  FROM [EumisLogs1].[dbo].[ActionLogs] al
  JOIN [Eumis].[dbo].[ProjectCommunications] pc on al.[ChildRootId] = pc.[ProjectCommunicationId]
  WHERE [Action] = 'EvalSessions.Edit.Projects.Communications.Edit'
GO