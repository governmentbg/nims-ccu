GO

IF EXISTS (SELECT [ContractVersionXmlId] FROM [dbo].[ContractVersionXmls] WHERE [Status] NOT IN (1, 3, 4) /*Active, Archived*/)
BEGIN
    THROW 50000,'Cannot update database. All ContractVersionXmls should be with status Active or Archived',1;
END
GO

ALTER TABLE [dbo].[ContractPartners] ADD
    [Gid]                   UNIQUEIDENTIFIER    NULL,
    [IsActive]              BIT                 NULL;
GO

BEGIN TRANSACTION

DECLARE @cID int
DECLARE @cXMLData xml
DECLARE @cStatus int
DECLARE @gid nvarchar(50)
DECLARE @isActivated nvarchar(50)
DECLARE xml_cursor CURSOR FOR
SELECT ContractVersionXmlId, Xml, [Status] FROM ContractVersionXmls WHERE Xml.exist('declare namespace c="http://ereg.egov.bg/segment/R-10040"; /BFPContract/c:Partners/c:Partner') = 1

OPEN xml_cursor
FETCH NEXT FROM xml_cursor INTO @cID, @cXMLData, @cStatus

WHILE @@FETCH_STATUS = 0
BEGIN
    DECLARE @seed int = 0
    WHILE @seed < @cXMLData.value('declare namespace c="http://ereg.egov.bg/segment/R-10040"; count(/BFPContract/c:Partners/c:Partner)', 'int')
    BEGIN
        SET @seed = @seed + 1
        SET @gid = CONVERT(NVARCHAR(200), NEWID())

        IF @cStatus = 1
            SET @isActivated = 'false'
        ELSE
            SET @isActivated = 'true'

        SET @cXMLData.modify('declare namespace c="http://ereg.egov.bg/segment/R-10040";
            insert attribute isActivated {sql:variable("@isActivated")} as last into
            (/BFPContract/c:Partners/c:Partner)[sql:variable("@seed")][1]')
        SET @cXMLData.modify('declare namespace c="http://ereg.egov.bg/segment/R-10040";
            insert attribute isActive {"true"} as last into
            (/BFPContract/c:Partners/c:Partner)[sql:variable("@seed")][1]')
        SET @cXMLData.modify('declare namespace c="http://ereg.egov.bg/segment/R-10040";
            insert attribute gid {sql:variable("@gid")} as last into
            (/BFPContract/c:Partners/c:Partner)[sql:variable("@seed")][1]')
    END
    UPDATE ContractVersionXmls SET Xml = @cXMLData WHERE ContractVersionXmlId = @cID
    FETCH NEXT FROM xml_cursor INTO @cID, @cXMLData
END
CLOSE xml_cursor;
DEALLOCATE xml_cursor;

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10040' as c,
    N'http://ereg.egov.bg/segment/R-10004' as p,
    N'http://ereg.egov.bg/segment/R-10000' as ut
),
ContractPartnerValues as
(
    SELECT
        c.ContractId,
        n.p.value('(@gid)[1]', 'NVARCHAR(MAX)') as Gid,
        n.p.value('(p:Uin/text())[1]', 'NVARCHAR(MAX)') as Uin,
        UinType =
            CASE n.p.value('(p:UinType/ut:Id/text())[1]', 'NVARCHAR(MAX)')
                WHEN 'eik' THEN 0
                WHEN 'bulstat' THEN 1
                WHEN 'personalBulstat' THEN 2
                WHEN 'personalbulstat' THEN 2
                WHEN 'foreign' THEN 3
                ELSE 0
            END
    FROM ContractVersionXmls x
    JOIN Contracts c ON x.ContractId = c.ContractId
    OUTER APPLY x.Xml.nodes('(/BFPContract/c:Partners/c:Partner)') n(p)
    WHERE x.Status = 3
)
UPDATE cp
    SET Gid = cpv.Gid, IsActive = 1
FROM ContractPartners cp
JOIN ContractPartnerValues cpv ON cp.ContractId = cpv.ContractId and cp.Uin = cpv.Uin and cp.UinType = cp.UinType;

IF EXISTS (SELECT [ContractPartnerId] FROM [dbo].[ContractPartners] WHERE [Gid] IS NULL)
BEGIN
    ROLLBACK TRANSACTION;
    THROW 50000,'Cannot update database. Cannot identify partners.',1;
END

COMMIT TRANSACTION
GO

ALTER TABLE [dbo].[ContractPartners] ALTER COLUMN [Gid]                   UNIQUEIDENTIFIER    NOT NULL;
ALTER TABLE [dbo].[ContractPartners] ALTER COLUMN [IsActive]              BIT                 NOT NULL;

ALTER TABLE [dbo].[ContractPartners] ADD CONSTRAINT [UQ_ContractPartners_Gid] UNIQUE ([Gid]);
