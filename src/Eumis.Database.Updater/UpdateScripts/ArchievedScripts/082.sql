GO

UPDATE [dbo].[MapNodes]
SET [Code] = SUBSTRING([Code], 5, LEN([Code]) - 4)
WHERE [Type] = 'ProgrammePriority' AND [Code] LIKE '2014%';
GO

UPDATE [dbo].[RegProjectXmls]
SET [Xml].modify('
    declare namespace bd="http://ereg.egov.bg/segment/R-10019";
    declare namespace proc="http://ereg.egov.bg/segment/R-10002";
    declare namespace c="http://ereg.egov.bg/segment/R-10001";
    replace value of (//Project/bd:ProjectBasicData/proc:Procedure/c:Code/text())[1]
            with substring((//Project/bd:ProjectBasicData/proc:Procedure/c:Code)[1], 5, string-length((//Project/bd:ProjectBasicData/proc:Procedure/c:Code)[1]) - 4)')
WHERE [Status] = 1 AND [ProcedureId] IN (SELECT p.[ProcedureId]
                                         FROM [dbo].[Procedures] p
                                         WHERE p.[Code] LIKE '2014%');
GO

-- updating procedure code
UPDATE pv
SET [ProcedureText] = REPLACE([ProcedureText], '"code":"' + p.Code + '"', '"code":"' + SUBSTRING(p.Code, 5, LEN(p.Code) - 4) + '"')
FROM [dbo].[ProcedureVersions] pv
JOIN [dbo].[Procedures] p ON pv.[ProcedureId] = p.[ProcedureId]
WHERE CHARINDEX('.', p.Code) != 0 AND p.[Code] LIKE '2014%';
GO

-- updating codes in programmePriorities array
UPDATE pv
SET [ProcedureText] = REPLACE([ProcedureText], '"code":"2014BG16RFOP001-', '"code":"BG16RFOP001-')
FROM [dbo].[ProcedureVersions] pv
JOIN [dbo].[Procedures] p ON pv.[ProcedureId] = p.[ProcedureId]
WHERE CHARINDEX('.', p.Code) != 0 AND p.[Code] LIKE '2014%';
GO

-- updating programme priorities codes expenses array
UPDATE pv
SET [ProcedureText] = REPLACE([ProcedureText], '"programmePriorityCode":"2014BG16RFOP001-', '"programmePriorityCode":"BG16RFOP001-')
FROM [dbo].[ProcedureVersions] pv
JOIN [dbo].[Procedures] p ON pv.[ProcedureId] = p.[ProcedureId]
WHERE CHARINDEX('.', p.Code) != 0 AND p.[Code] LIKE '2014%';
GO

UPDATE [dbo].[Procedures]
SET [Code] = SUBSTRING([Code], 5, LEN([Code]) - 4)
WHERE [Code] LIKE '2014%';
GO
