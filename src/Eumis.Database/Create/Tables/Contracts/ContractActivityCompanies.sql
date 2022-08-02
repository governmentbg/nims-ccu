PRINT 'ContractActivityCompanies'
GO

CREATE TABLE [dbo].[ContractActivityCompanies] (
    [ContractActivityCompanyId]     INT             NOT NULL IDENTITY,
    [ContractActivityId]            INT             NOT NULL,

    [CompanyUin]                    NVARCHAR(200)   NOT NULL,
    [CompanyUinType]                INT             NOT NULL,
    [CompanyName]                   NVARCHAR(MAX)   NOT NULL,

    CONSTRAINT [PK_ContractActivityCompanies]                       PRIMARY KEY ([ContractActivityCompanyId]),
    CONSTRAINT [FK_ContractActivityCompanies_ContractActivities]    FOREIGN KEY ([ContractActivityId])            REFERENCES [dbo].[ContractActivities] ([ContractActivityId])
);
GO

exec spDescTable  N'ContractActivityCompanies', N'Организации към дейност към договор.'
exec spDescColumn N'ContractActivityCompanies', N'ContractActivityCompanyId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractActivityCompanies', N'ContractActivityId'           , N'Идентификатор на дейност към договор.'

exec spDescColumn N'ContractActivityCompanies', N'CompanyUin'                   , N'Уникален идентификационен номер.'
exec spDescColumn N'ContractActivityCompanies', N'CompanyUinType'               , N'0-ЕИК, 1-булстат, 2 - булстат за свободни професии (ЕГН), 3 - Чуждестранни фирми.'
exec spDescColumn N'ContractActivityCompanies', N'CompanyName'                  , N'Организация отговорна за изпълнението на дейността.'
GO
