GO
DECLARE @updateId INT;

SET @updateId = (
	SELECT  [SapSchemaId]
    FROM [dbo].[SapSchemas]
    WHERE [Type] = 1 AND [IsActive] = 1
)

UPDATE [dbo].[SapSchemas] 
SET
	[IsActive] = 0
WHERE SapSchemaId = @updateId

DECLARE @xml XML;
SELECT @xml = Content FROM [dbo].[SapSchemas] WHERE SapSchemaId = @updateId;

SET @xml.modify('
    declare namespace xs="http://www.w3.org/2001/XMLSchema";
    insert <xs:enumeration value="PF" /> as last into (/xs:schema/xs:simpleType[@name="_FinanceSource"]/xs:restriction[@base="xs:string"])[1]');

INSERT INTO [dbo].[SapSchemas]
    ([Content], [IsActive], [CreateDate], [ModifyDate], [Type])
VALUES
    (@xml     , 1         , GETDATE()   , GETDATE()   , 1  );
GO
