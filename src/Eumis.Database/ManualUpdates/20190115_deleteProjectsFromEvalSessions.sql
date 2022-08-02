--executed on 2019/01/15 ~ 15:00

GO
BEGIN TRANSACTION
GO

DECLARE @Input TABLE (ProjectRegNum NVARCHAR(200), SessionNum NVARCHAR(50), EvalSessionId int, ProjectId int);
INSERT INTO @Input
    (ProjectRegNum, SessionNum)
VALUES
    ('BG16RFOP002-2.024-0446', 'BG16RFOP002-2.024-S1'),
    ('BG16RFOP002-2.024-0743', 'BG16RFOP002-2.024-S1'),
	('BG16RFOP002-2.024-0235', 'BG16RFOP002-2.024-S2');

UPDATE @Input
    SET EvalSessionId = es.EvalSessionId
FROM EvalSessions es
JOIN @Input i ON es.SessionNum = i.SessionNum

UPDATE @Input
    SET ProjectId = p.ProjectId
FROM Projects p
JOIN @Input i ON p.RegNumber = i.ProjectRegNum

DECLARE @EvalSessionSheets TABLE (EvalSessionSheetId INT);
INSERT INTO @EvalSessionSheets
SELECT EvalSessionSheetId 
FROM EvalSessionSheets ess
JOIN @Input i ON ess.ProjectId = i.ProjectId AND ess.EvalSessionId = i.EvalSessionId 

DECLARE @EvalSessionSheetXmls TABLE (EvalSessionSheetXmlId INT);
INSERT INTO @EvalSessionSheetXmls
SELECT EvalSessionSheetXmlId FROM EvalSessionSheetXmls 
WHERE EvalSessionSheetId IN (SELECT EvalSessionSheetId FROM @EvalSessionSheets)

--Протоколи/Доклади/Решения
DELETE esrp 
FROM EvalSessionReportProjects esrp
JOIN @Input i ON esrp.ProjectId = i.ProjectId AND esrp.EvalSessionId = i.EvalSessionId 

--Оценителни листове
DELETE esxf 
FROM EvalSessionSheetXmlFiles esxf
WHERE EvalSessionSheetXmlId IN (SELECT EvalSessionSheetXmlId FROM @EvalSessionSheetXmls)

DELETE esx 
FROM EvalSessionSheetXmls esx
WHERE EvalSessionSheetId IN (SELECT EvalSessionSheetId FROM @EvalSessionSheets)

DELETE ess 
FROM EvalSessionSheets ess
JOIN @Input i ON ess.ProjectId = i.ProjectId AND ess.EvalSessionId = i.EvalSessionId

--Разпределение
DELETE esdp 
FROM EvalSessionDistributionProjects esdp
JOIN @Input i ON esdp.ProjectId = i.ProjectId AND esdp.EvalSessionId = i.EvalSessionId 

--ПП
DELETE esp 
FROM EvalSessionProjects esp
JOIN @Input i ON esp.ProjectId = i.ProjectId AND esp.EvalSessionId = i.EvalSessionId
	
COMMIT TRANSACTION
GO
