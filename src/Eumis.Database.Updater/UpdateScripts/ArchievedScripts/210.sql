GO
BEGIN TRANSACTION
GO

DECLARE @NewXML xml
DECLARE @pID int
DECLARE @pXMLData xml
DECLARE @CurrentDate date = GETDATE();
DECLARE @CurrentDateStr char(10) = CONVERT(CHAR(10), @CurrentDate, 126);

CREATE TABLE #XmlData(
	ContractProcurementXmlId int, 
	Gid nvarchar(50),
	IsDraft int,
	MeetsCriteria int,
	EverAnnounced int);

WITH
    XMLNAMESPACES (
		N'http://ereg.egov.bg/segment/R-10041' as R10041,
		N'http://ereg.egov.bg/segment/R-10048' as R10048,
		N'http://ereg.egov.bg/segment/R-10070' as R10070,
		N'http://ereg.egov.bg/segment/R-10000' as R10000),
    ContractProcurementValues (ContractProcurementXmlId, Gid, IsDraft, MeetsCriteria, EverAnnounced)
AS
(
    SELECT
       cpxml.ContractProcurementXmlId,
	   n.nSpace.value('(@gid)[1]', 'NVARCHAR(50)') as Gid,
	   IIF(cpxml.Status = 1, 1, 0) as IsDraft,
	   IIF(n.nSpace.value('(R10048:BFPContractPlan/R10070:ErrandLegalAct/R10000:Id/text())[1]', 'NVARCHAR(MAX)') = '7e9b44e8-742b-45e5-b967-7b7feec6e18a'
			AND n.nSpace.value('count(R10048:DifferentiatedPosition)', 'INT') > 0
			AND n.nSpace.value('count(R10048:PublicAttachedDocument)', 'INT') > 0
			AND n.nSpace.value('(R10048:OffersDeadlineDate/text())[1]', 'NVARCHAR(MAX)') IS NOT NULL, 1, 0) as MeetsCriteria,
		NULL as EverAnnounced
    FROM ContractProcurementXmls cpxml
    OUTER APPLY cpxml.Xml.nodes('(/Procurements/R10041:ProcurementPlans/R10041:ProcurementPlan)') n(nSpace)
	WHERE cpxml.Xml.exist('declare namespace R10041="http://ereg.egov.bg/segment/R-10041"; /Procurements/R10041:ProcurementPlans/*:ProcurementPlan') = 1
)
INSERT INTO #XmlData
SELECT ContractProcurementXmlId, Gid, IsDraft, MeetsCriteria, EverAnnounced  FROM ContractProcurementValues

UPDATE xd1 SET EverAnnounced = xd2.MeetsCriteria
FROM #XmlData xd1 JOIN (SELECT Gid, MAX(MeetsCriteria) as MeetsCriteria
						FROM #XmlData where IsDraft = 0
						GROUP BY Gid) xd2 ON xd1.Gid = xd2.Gid

DECLARE xml_cursor CURSOR FOR
SELECT ContractProcurementXmlId, Xml FROM ContractProcurementXmls
WHERE Xml.exist('declare namespace R10041="http://ereg.egov.bg/segment/R-10041"; /Procurements/R10041:ProcurementPlans/*:ProcurementPlan') = 1

OPEN xml_cursor
FETCH NEXT FROM xml_cursor INTO @pID, @pXMLData
WHILE @@FETCH_STATUS = 0
BEGIN
    DECLARE @seed int = 0
    WHILE @seed < @pXMLData.value('declare namespace R10041 = "http://ereg.egov.bg/segment/R-10041";count(/Procurements/R10041:ProcurementPlans/*:ProcurementPlan)', 'int')
    BEGIN
        SET @seed = @seed + 1;
		
		SELECT @NewXML = CASE 
							WHEN (IsDraft = 0 AND MeetsCriteria = 1) OR (IsDraft = 1 AND MeetsCriteria = 1 AND EverAnnounced = 1) 
							THEN
								N'<IsAnnounced xmlns="http://ereg.egov.bg/segment/R-10048">true</IsAnnounced>
								<AnnouncedDate xmlns="http://ereg.egov.bg/segment/R-10048">' + @CurrentDateStr + '</AnnouncedDate>
								<IsTerminated xmlns="http://ereg.egov.bg/segment/R-10048">false</IsTerminated>'	
							ELSE 
								N'<IsAnnounced xmlns="http://ereg.egov.bg/segment/R-10048">false</IsAnnounced>
								<IsTerminated xmlns="http://ereg.egov.bg/segment/R-10048">false</IsTerminated>'
						END
		FROM #XmlData xdata
		WHERE ContractProcurementXmlId = @pID
				AND Gid = (SELECT @pXMLData.value('declare namespace R10041 = "http://ereg.egov.bg/segment/R-10041";
											(/Procurements/R10041:ProcurementPlans/*:ProcurementPlan/@gid)[sql:variable("@seed")][1]', 'nvarchar(50)'))
			
		SET @pXMLData.modify('
				declare namespace R10041 = "http://ereg.egov.bg/segment/R-10041";
				declare namespace R10048 = "http://ereg.egov.bg/segment/R-10048";
				declare namespace R10070 = "http://ereg.egov.bg/segment/R-10070";
				declare namespace R10000 = "http://ereg.egov.bg/segment/R-10000";
				insert sql:variable("@NewXML") as last
				into (/Procurements/R10041:ProcurementPlans/*:ProcurementPlan)[sql:variable("@seed")][1]')

	END
	UPDATE ContractProcurementXmls SET Xml = @pXMLData WHERE ContractProcurementXmlId = @pID
    FETCH NEXT FROM xml_cursor INTO @pID, @pXMLData
END
CLOSE xml_cursor;
DEALLOCATE xml_cursor;

UPDATE cpp
    SET AnnouncedDate = @CurrentDate
FROM ContractProcurementPlans cpp
JOIN (SELECT DISTINCT Gid FROM #XmlData 
		WHERE (IsDraft = 0 AND MeetsCriteria = 1) 
		OR (IsDraft = 1 AND MeetsCriteria = 1 AND EverAnnounced = 1)) xdata 
	on cpp.Gid = xdata.Gid

DROP TABLE #XmlData
GO

COMMIT TRANSACTION
GO
