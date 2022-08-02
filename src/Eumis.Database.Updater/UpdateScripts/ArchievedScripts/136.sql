GO

BEGIN TRANSACTION

DECLARE @cID int
DECLARE @cXMLData xml
DECLARE @cStatus int
DECLARE @gid nvarchar(50)
DECLARE xml_cursor CURSOR FOR
SELECT ContractVersionXmlId, Xml, [Status] FROM ContractVersionXmls WHERE (Xml.exist('declare namespace c="http://ereg.egov.bg/segment/R-10040"; /BFPContract/c:Partners/c:Partner') = 1 AND Xml.exist('declare namespace c="http://ereg.egov.bg/segment/R-10040"; /BFPContract/c:Partners/c:Partner/@gid') = 0)

OPEN xml_cursor
FETCH NEXT FROM xml_cursor INTO @cID, @cXMLData, @cStatus

WHILE @@FETCH_STATUS = 0
BEGIN
    DECLARE @seed int = 0
    WHILE @seed < @cXMLData.value('declare namespace c="http://ereg.egov.bg/segment/R-10040"; count(/BFPContract/c:Partners/c:Partner)', 'int')
    BEGIN
        SET @seed = @seed + 1
        SET @gid = CONVERT(NVARCHAR(200), NEWID())

        SET @cXMLData.modify('declare namespace c="http://ereg.egov.bg/segment/R-10040";
            insert attribute isActive {"true"} as last into
            (/BFPContract/c:Partners/c:Partner)[sql:variable("@seed")][1]')
        SET @cXMLData.modify('declare namespace c="http://ereg.egov.bg/segment/R-10040";
            insert attribute gid {sql:variable("@gid")} as last into
            (/BFPContract/c:Partners/c:Partner)[sql:variable("@seed")][1]')
    END
    UPDATE ContractVersionXmls SET Xml = @cXMLData WHERE ContractVersionXmlId = @cID
    FETCH NEXT FROM xml_cursor INTO @cID, @cXMLData, @cStatus
END
CLOSE xml_cursor;
DEALLOCATE xml_cursor;

COMMIT TRANSACTION
GO