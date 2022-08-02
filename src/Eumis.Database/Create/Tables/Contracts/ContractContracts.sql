PRINT 'ContractContracts'
GO

CREATE TABLE [dbo].[ContractContracts] (
    [ContractContractId]        INT                 NOT NULL IDENTITY,
    [ContractId]                INT                 NOT NULL,
    [Gid]                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [IsActive]                  BIT                 NOT NULL,
    [ContractContractorId]      INT                 NOT NULL,

    [SignDate]                  DATETIME2           NOT NULL,
    [Number]                    NVARCHAR(MAX)       NOT NULL,
    [TotalAmountExcludingVAT]   MONEY               NOT NULL,
    [ContractAmountWithoutVAT]  MONEY               NOT NULL,
    [VATAmountIfEligible]       MONEY               NOT NULL,
    [TotalFundedValue]          MONEY               NOT NULL,
    [BudgetDifference]          MONEY               NOT NULL,
    [NumberAnnexes]             INT                 NOT NULL,
    [CurrentAnnexTotalAmount]   MONEY               NOT NULL,
    [Comment]                   NVARCHAR(MAX)       NULL,
    [StartDate]                 DATETIME2           NOT NULL,
    [EndDate]                   DATETIME2           NOT NULL,
    [HasSubcontractorMember]    BIT                 NOT NULL,

    CONSTRAINT [PK_ContractContracts]                           PRIMARY KEY ([ContractContractId]),
    CONSTRAINT [FK_ContractContracts_Contracts]                 FOREIGN KEY ([ContractId])                  REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractContracts_ContractContractors]       FOREIGN KEY ([ContractContractorId])        REFERENCES [dbo].[ContractContractors] ([ContractContractorId])
);
GO

exec spDescTable  N'ContractContracts', N'Договори с изпълнители към договор.'
exec spDescColumn N'ContractContracts', N'ContractContractId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractContracts', N'ContractId'               , N'Идентификатор на договор.'
exec spDescColumn N'ContractContracts', N'Gid'                      , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ContractContracts', N'IsActive'                 , N'Маркер за активност.'
exec spDescColumn N'ContractContracts', N'ContractContractorId'     , N'Идентификатор на изпълнител.'

exec spDescColumn N'ContractContracts', N'SignDate'                 , N'Дата на сключване на договор.'
exec spDescColumn N'ContractContracts', N'Number'                   , N'Номер на договора'
exec spDescColumn N'ContractContracts', N'TotalAmountExcludingVAT'  , N'Обща сума на договора без ДДС.'
exec spDescColumn N'ContractContracts', N'ContractAmountWithoutVAT' , N'Сума на договора, финансирана по проекта без ДДС.'
exec spDescColumn N'ContractContracts', N'VATAmountIfEligible'      , N'Сума на ДДС, по договора, финансирана по проекта, ако е допустим разход.'
exec spDescColumn N'ContractContracts', N'TotalFundedValue'         , N'Обща стойност на договора, финансирана по проекта.'
exec spDescColumn N'ContractContracts', N'BudgetDifference'         , N'Разлика от одобрения бюджет.'
exec spDescColumn N'ContractContracts', N'NumberAnnexes'            , N'Брой анекси към договор.'
exec spDescColumn N'ContractContracts', N'CurrentAnnexTotalAmount'  , N'Обща стойност на актуалния анекс.'
exec spDescColumn N'ContractContracts', N'Comment'                  , N'Коментар.'
exec spDescColumn N'ContractContracts', N'StartDate'                , N'Начална дата за изпълнение.'
exec spDescColumn N'ContractContracts', N'EndDate'                  , N'Крайна дата за изпълнение.'
exec spDescColumn N'ContractContracts', N'HasSubcontractorMember'   , N'Има подизпълнители или членове на обединението.'
GO
