GO

ALTER TABLE [Companies]
ADD [CompanyLegalStatus]    INT                 NOT NULL DEFAULT 1,
    [NameAlt]               NVARCHAR(200)       NOT NULL DEFAULT N'empty',
    CONSTRAINT [CHK_Companies_CompanyLegalStatuses]     CHECK       ([CompanyLegalStatus] IN (1, 2));