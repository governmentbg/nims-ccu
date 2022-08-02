PRINT 'ContractAccessCodes'
GO

CREATE TABLE [dbo].[ContractAccessCodes] (
    [ContractAccessCodeId]      INT                 NOT NULL IDENTITY,
    [ContractId]                INT                 NOT NULL,
    [Gid]                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [Code]                      NVARCHAR (50)       NOT NULL,
    [FirstName]                 NVARCHAR (100)      NOT NULL,
    [LastName]                  NVARCHAR (100)      NOT NULL,
    [Position]                  NVARCHAR (100)      NULL,
    [Email]                     NVARCHAR (200)      NOT NULL,
    [Identifier]                NVARCHAR (MAX)      NULL,
    [IsActive]                  BIT                 NOT NULL,
    [CanReadContracts]          BIT                 NOT NULL,
    [CanReadProcurements]       BIT                 NOT NULL,
    [CanWriteProcurements]      BIT                 NOT NULL,
    [CanReadTechnicalPlan]      BIT                 NOT NULL,
    [CanWriteTechnicalPlan]     BIT                 NOT NULL,
    [CanReadFinancialPlan]      BIT                 NOT NULL,
    [CanWriteFinancialPlan]     BIT                 NOT NULL,
    [CanReadMicrodata]          BIT                 NOT NULL,
    [CanWriteMicrodata]         BIT                 NOT NULL,
    [CanReadPaymentRequest]     BIT                 NOT NULL,
    [CanWritePaymentRequest]    BIT                 NOT NULL,
    [CanReadCommunication]      BIT                 NOT NULL,
    [CanReadSpendingPlan]       BIT                 NOT NULL,
    [CanWriteSpendingPlan]      BIT                 NOT NULL,
    [CreateDate]                DATETIME2           NOT NULL,
    [ModifyDate]                DATETIME2           NOT NULL,
    [Version]                   ROWVERSION          NOT NULL

    CONSTRAINT [PK_ContractAccessCodes]                 PRIMARY KEY ([ContractAccessCodeId]),
    CONSTRAINT [FK_ContractAccessCodes_Contracts]       FOREIGN KEY ([ContractId])              REFERENCES [dbo].[Contracts] ([ContractId]),
);
GO

exec spDescTable  N'ContractAccessCodes', N'Кодове за достъп към договор за БФП.'
exec spDescColumn N'ContractAccessCodes', N'ContractAccessCodeId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractAccessCodes', N'ContractId'                        , N'Идентификатор на договор за БФП.'
exec spDescColumn N'ContractAccessCodes', N'Gid'                               , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ContractAccessCodes', N'Code'                              , N'Код.'
exec spDescColumn N'ContractAccessCodes', N'FirstName'                         , N'Собствено име.'
exec spDescColumn N'ContractAccessCodes', N'LastName'                          , N'Фамилно име.'
exec spDescColumn N'ContractAccessCodes', N'Position'                          , N'Телефон.'
exec spDescColumn N'ContractAccessCodes', N'Email'                             , N'Ел. адрес.'
exec spDescColumn N'ContractAccessCodes', N'Identifier'                        , N'Идентификатор.'
exec spDescColumn N'ContractAccessCodes', N'IsActive'                          , N'Маркер за активност.'
exec spDescColumn N'ContractAccessCodes', N'CreateDate'                        , N'Дата на създаване на записа.'
exec spDescColumn N'ContractAccessCodes', N'ModifyDate'                        , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractAccessCodes', N'Version'                           , N'Версия.'
GO
