PRINT 'ContractSubcontracts'
GO

CREATE TABLE [dbo].[ContractSubcontracts] (
    [ContractSubcontractId]     INT                 NOT NULL IDENTITY,
    [ContractContractId]        INT                 NOT NULL,
    [ContractContractorId]      INT                 NOT NULL,

    [Type]                      INT                 NOT NULL,
    [Date]                      DATETIME2           NOT NULL,
    [Number]                    NVARCHAR(MAX)       NOT NULL,
    [Amount]                    MONEY               NOT NULL,

    CONSTRAINT [PK_ContractSubcontracts]                        PRIMARY KEY ([ContractSubcontractId]),
    CONSTRAINT [CHK_ContractSubcontracts_Type]                  CHECK ([Type] IN (1,2)),
    CONSTRAINT [FK_ContractSubcontracts_ContractContracts]      FOREIGN KEY ([ContractContractId])          REFERENCES [dbo].[ContractContracts] ([ContractContractId]),
    CONSTRAINT [FK_ContractSubcontracts_ContractContractors]    FOREIGN KEY ([ContractContractorId])        REFERENCES [dbo].[ContractContractors] ([ContractContractorId])
);
GO

exec spDescTable  N'ContractSubcontracts', N'Договори с подизпълнители към договор.'
exec spDescColumn N'ContractSubcontracts', N'ContractSubcontractId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractSubcontracts', N'ContractContractId'        , N'Идентификатор на договор с изпълнител.'
exec spDescColumn N'ContractSubcontracts', N'ContractContractorId'      , N'Идентификатор на изпълнител.'

exec spDescColumn N'ContractSubcontracts', N'Type'                      , N'Тип. 1 - Подизпълнител, 2 - Член на обединението.'
exec spDescColumn N'ContractSubcontracts', N'Date'                      , N'Дата на сключване.'
exec spDescColumn N'ContractSubcontracts', N'Number'                    , N'Номер.'
exec spDescColumn N'ContractSubcontracts', N'Amount'                    , N'Стойност.'
GO
