GO

ALTER TABLE [dbo].[Contracts] ADD
    [CompanyTypeId]                         INT               NULL,
    [CompanyLegalStatus]                    INT               NULL,
    [CompanyLegalTypeId]                    INT               NULL,
    CONSTRAINT [FK_Contracts_CompanyLegalType]      FOREIGN KEY ([CompanyLegalTypeId])          REFERENCES [dbo].[CompanyLegalTypes] ([CompanyLegalTypeId]),
    CONSTRAINT [FK_Contracts_CompanyType]           FOREIGN KEY ([CompanyTypeId])               REFERENCES [dbo].[CompanyTypes] ([CompanyTypeId]),
    CONSTRAINT [CHK_Contracts_CompanyLegalStatuses] CHECK       ([CompanyLegalStatus] IN (1, 2));

GO
