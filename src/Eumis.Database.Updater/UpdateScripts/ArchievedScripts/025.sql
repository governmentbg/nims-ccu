-- 4b5eb06 Add IsSignatureRequired property to ProcedureAppDocs
GO

ALTER TABLE [ProcedureApplicationDocs]
ADD [IsSignatureRequired]         BIT       NOT NULL DEFAULT 0;
GO
