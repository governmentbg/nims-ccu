GO

DECLARE @cID int
DECLARE @cXMLData xml

DECLARE xml_cursor CURSOR FOR
SELECT [ContractVersionXmlId], [Xml] FROM [dbo].[ContractVersionXmls]

OPEN xml_cursor
FETCH NEXT FROM xml_cursor INTO @cID, @cXMLData

WHILE @@FETCH_STATUS = 0
BEGIN
    DECLARE @node xml
    SELECT @node = CONVERT(XML, REPLACE(CONVERT(NVARCHAR(MAX), pv.[Xml].query('declare namespace p="http://ereg.egov.bg/segment/R-10019"; (/Project/p:ProjectSpecFields)[1]')), N'xmlns="http://ereg.egov.bg/segment/R-10019"', N'xmlns="http://ereg.egov.bg/segment/R-10040"'))
                    FROM [dbo].[ContractVersionXmls] cv
                    JOIN [dbo].[Contracts] c on cv.[ContractId] = c.[ContractId]
                    JOIN [dbo].[ProjectVersionXmls] pv ON c.[ProjectId] = pv.[ProjectId]
                    WHERE cv.[ContractVersionXmlId] = @cID and pv.[Status] = 2 and (cv.[Xml].exist('declare namespace p="http://ereg.egov.bg/segment/R-10040"; (/BFPContract/p:ProjectSpecFields)[1]') = 0)
    SET @cXMLData.modify('insert sql:variable("@node") as last into (/BFPContract)[1]')

    UPDATE ContractVersionXmls SET [Xml] = @cXMLData WHERE [ContractVersionXmlId] = @cID
    FETCH NEXT FROM xml_cursor INTO @cID, @cXMLData
END
CLOSE xml_cursor;
DEALLOCATE xml_cursor;
