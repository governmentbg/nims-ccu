CREATE TABLE [dbo].[ContractExtensions] (
    [ContractExtensionId]       INT             NOT NULL IDENTITY,
    [ContractId]                INT             NOT NULL,
    [Discriminator]             INT             NOT NULL,
    [Value]                     NVARCHAR(MAX)   NOT NULL,

    CONSTRAINT [PK_ContractExtensions]                  PRIMARY KEY ([ContractExtensionId]),
    CONSTRAINT [FK_ContractExtensions_Contracts]        FOREIGN KEY ([ContractId]) REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [CHK_ContractExtensions_Discriminator]   CHECK ([Discriminator] IN (1)),
);
GO

exec spDescTable  N'ContractExtensions', N'Специфични полета към договор'
exec spDescColumn N'ContractExtensions', N'ContractExtensionId'     , N'Уникален системно генериран идентификатор'
exec spDescColumn N'ContractExtensions', N'ContractId'              , N'Идентификатор на договор'
exec spDescColumn N'ContractExtensions', N'Discriminator'           , N'Вид: 1 - Единен идентификационен номер на риболовен кораб'
exec spDescColumn N'ContractExtensions', N'Value'                   , N'Стойност на полето'
GO
