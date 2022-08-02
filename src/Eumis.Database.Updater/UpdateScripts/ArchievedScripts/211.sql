UPDATE [dbo].[SapSchemas]
SET
    IsActive = 0,
    ModifyDate = GETDATE()
WHERE
    SapSchemaId = 3
GO

DECLARE @xml XML;
SELECT @xml = Content FROM [dbo].[SapSchemas] WHERE SapSchemaId = 3;

SET @xml.modify('
    declare namespace xs="http://www.w3.org/2001/XMLSchema";
    insert <xs:enumeration value="MZ" /> as last into (/xs:schema/xs:simpleType[@name="_FinanceSource"]/xs:restriction[@base="xs:string"])[1]');
SET @xml.modify('
    declare namespace xs="http://www.w3.org/2001/XMLSchema";
    insert <xs:enumeration value="SF" /> as last into (/xs:schema/xs:simpleType[@name="_FinanceSource"]/xs:restriction[@base="xs:string"])[1]');
SET @xml.modify('
    declare namespace xs="http://www.w3.org/2001/XMLSchema";
    insert <xs:enumeration value="IF" /> as last into (/xs:schema/xs:simpleType[@name="_FinanceSource"]/xs:restriction[@base="xs:string"])[1]');

INSERT INTO [dbo].[SapSchemas]
    ([Content], [IsActive], [CreateDate], [ModifyDate])
VALUES
    (@xml     , 1         , GETDATE()   , GETDATE()   );
GO
