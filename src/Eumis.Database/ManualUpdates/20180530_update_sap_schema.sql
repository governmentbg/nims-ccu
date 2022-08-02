--executed on 2018/05/30 ~ 15:25

DECLARE @activeSchemaId INT = 4;

UPDATE [dbo].[SapSchemas]
SET
    IsActive = 0,
    ModifyDate = GETDATE()
WHERE
    SapSchemaId = @activeSchemaId;

DECLARE @xml XML;
SELECT @xml = Content FROM [dbo].[SapSchemas] WHERE SapSchemaId = @activeSchemaId;

SET @xml.modify('
    declare namespace xs="http://www.w3.org/2001/XMLSchema";
    insert <xs:enumeration value="" /> as last into (/xs:schema/xs:simpleType[@name="_FinanceSource"]/xs:restriction[@base="xs:string"])[1]');

INSERT INTO [dbo].[SapSchemas]
    ([Content], [IsActive], [CreateDate], [ModifyDate])
VALUES
    (@xml     , 1         , GETDATE()   , GETDATE()   );
GO
