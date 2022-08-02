UPDATE [dbo].[SapSchemas]
SET
    IsActive = 0,
    ModifyDate = GETDATE()
WHERE
    SapSchemaId = 2
GO

DECLARE @xml XML;
SELECT @xml = Content FROM [dbo].[SapSchemas] WHERE SapSchemaId = 2;

SET @xml.modify('
    declare namespace xs="http://www.w3.org/2001/XMLSchema";
    delete /xs:schema/xs:simpleType[@name="_EuFund"]/xs:restriction[@base="xs:string"]/*');

SET @xml.modify('
    declare namespace xs="http://www.w3.org/2001/XMLSchema";
    insert <xs:pattern value="([A-Z0-9])+" /> as last into (/xs:schema/xs:simpleType[@name="_EuFund"]/xs:restriction[@base="xs:string"])[1]');

SET @xml.modify('
    declare namespace xs="http://www.w3.org/2001/XMLSchema";
    insert <xs:enumeration value="EUR" /> as last into (/xs:schema/xs:simpleType[@name="_Currency"]/xs:restriction[@base="xs:string"])[1]');

INSERT INTO [dbo].[SapSchemas]
    ([Content], [IsActive], [CreateDate], [ModifyDate])
VALUES
    (@xml     , 1         , GETDATE()   , GETDATE()   );
GO

ALTER TABLE [dbo].[SapSchemas]
    ADD [IsActiveCheck] AS
    CASE [IsActive]
        WHEN 1 THEN -1
        WHEN 0 THEN [SapSchemaId]
    END

ALTER TABLE [dbo].[SapSchemas] ADD CONSTRAINT [UQ_SapSchemas_SingleIsActive] UNIQUE ([IsActiveCheck])
GO
