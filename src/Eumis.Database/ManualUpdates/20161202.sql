--executed on 2016/12/02 ~ 11.30

UPDATE cr
SET cr.[Status] = 3
FROM [Eumis].[dbo].[CertReports] cr
JOIN [Eumis].[dbo].[MapNodes] mn on cr.[ProgrammeId] = mn.[MapNodeId]
WHERE [OrderNum] = 3 and [OrderVersionNum] = 2 and mn.[Name] = N'Околна среда' and mn.[Code] = N'2014BG16M1OP002'
GO