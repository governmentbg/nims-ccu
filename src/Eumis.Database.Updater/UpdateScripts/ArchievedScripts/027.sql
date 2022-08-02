GO

ALTER TABLE [Procedures]
ADD [AllowedRegistrationType]   INT                 NOT NULL DEFAULT 3,
    CONSTRAINT [CHK_Procedures_AllowedRegistrationType]   CHECK       ([AllowedRegistrationType] IN (1, 2, 3));
GO
