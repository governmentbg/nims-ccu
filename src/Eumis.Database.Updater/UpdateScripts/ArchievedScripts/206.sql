DECLARE @xmlDeclaration NVARCHAR(MAX) = N'<?xml version="1.0" encoding="utf-8"?>';

-- remove xml declaration
UPDATE [dbo].[SapSchemas]
SET
    Content = SUBSTRING(Content, LEN(@xmlDeclaration) + 1, LEN(Content) - LEN(@xmlDeclaration))
WHERE
    SapSchemaId = 1 AND
    CHARINDEX(@xmlDeclaration, Content) = 1
GO

ALTER TABLE [dbo].[SapSchemas] ALTER COLUMN [Content] XML NOT NULL
GO

UPDATE [dbo].[SapSchemas]
SET
    IsActive = 0,
    ModifyDate = GETDATE()
WHERE
    SapSchemaId = 1
GO

DECLARE @xml XML;
SELECT @xml = Content FROM [dbo].[SapSchemas] WHERE SapSchemaId = 1;

SET @xml.modify('
    declare namespace xs="http://www.w3.org/2001/XMLSchema";
    insert <xs:enumeration value="FEAD" /> as last into (/xs:schema/xs:simpleType[@name="_EuFund"]/xs:restriction[@base="xs:string"])[1]')

INSERT INTO [SapSchemas]
    ([Content], [IsActive], [CreateDate], [ModifyDate])
VALUES
    (@xml     , 1         , GETDATE()   , GETDATE()   );
GO

ALTER TABLE [dbo].[SapPaidAmounts] DROP CONSTRAINT [CHK_SapPaidAmounts_Fund]
GO
ALTER TABLE [dbo].[SapPaidAmounts] ADD CONSTRAINT [CHK_SapPaidAmounts_Fund] CHECK ([Fund] IN (1, 2, 3, 5))
GO
