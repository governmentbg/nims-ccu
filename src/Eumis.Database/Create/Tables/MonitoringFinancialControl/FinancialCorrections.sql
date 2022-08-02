PRINT 'FinancialCorrections'
GO

CREATE TABLE [dbo].[FinancialCorrections] (
    [FinancialCorrectionId]         INT           NOT NULL IDENTITY,
    [OrderNum]                      INT           NOT NULL,
    [Status]                        INT           NOT NULL,
    [ImpositionDate]                DATETIME2     NOT NULL,
    [ContractId]                    INT           NOT NULL,
    [ContractContractId]            INT           NULL,
    [ContractBudgetLevel3AmountId]  INT           NULL,

    [DeleteNote]                    NVARCHAR(MAX) NULL,
    [CreateDate]                    DATETIME2     NOT NULL,
    [ModifyDate]                    DATETIME2     NOT NULL,
    [Version]                       ROWVERSION    NOT NULL,

    CONSTRAINT [PK_FinancialCorrections]                             PRIMARY KEY ([FinancialCorrectionId]),
    CONSTRAINT [FK_FinancialCorrections_Contracts]                   FOREIGN KEY ([ContractId])                   REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_FinancialCorrections_ContractContracts]           FOREIGN KEY ([ContractContractId])           REFERENCES [dbo].[ContractContracts] ([ContractContractId]),
    CONSTRAINT [FK_FinancialCorrections_ContractBudgetLevel3Amounts] FOREIGN KEY ([ContractBudgetLevel3AmountId]) REFERENCES [dbo].[ContractBudgetLevel3Amounts] ([ContractBudgetLevel3AmountId]),
    CONSTRAINT [CHK_FinancialCorrections_Status]                     CHECK ([Status]          IN (1, 2, 3))
);
GO

exec spDescTable  N'FinancialCorrections', N'Финансови корекции.'
exec spDescColumn N'FinancialCorrections', N'FinancialCorrectionId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'FinancialCorrections', N'OrderNum'                    , N'Пореден номер.'
exec spDescColumn N'FinancialCorrections', N'Status'                      , N'Статус на въвеждане: 1 - Нов; 2 - Въведен; 3 - Анулиран.'
exec spDescColumn N'FinancialCorrections', N'ImpositionDate'              , N'Дата на налагане.'
exec spDescColumn N'FinancialCorrections', N'ContractId'                  , N'Идентификатор на договор.'
exec spDescColumn N'FinancialCorrections', N'ContractContractId'          , N'Идентификатор на договор с изпълнител.'
exec spDescColumn N'FinancialCorrections', N'ContractBudgetLevel3AmountId', N'Идентификатор на бюджетен ред към договор.'

exec spDescColumn N'FinancialCorrections', N'DeleteNote'                  , N'Причина за анулиране.'
exec spDescColumn N'FinancialCorrections', N'CreateDate'                  , N'Дата на създаване на записа.'
exec spDescColumn N'FinancialCorrections', N'ModifyDate'                  , N'Дата на последно редактиране на записа.'
exec spDescColumn N'FinancialCorrections', N'Version'                     , N'Версия.'

GO