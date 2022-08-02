--executed on 2016/01/07 ~ 18:10
ALTER TABLE [dbo].[ActionLogs] ADD
    [ContractRegistrationEmail] NVARCHAR (200)     NULL,
    [ContractAccessCodeEmail]   NVARCHAR (200)     NULL
GO

ALTER TABLE [dbo].[Logs] ADD
    [ContractRegistrationId]  INT                NULL,
    [ContractAccessCodeId]    INT                NULL
GO