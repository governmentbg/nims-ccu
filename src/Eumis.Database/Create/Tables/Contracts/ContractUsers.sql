PRINT 'ContractUsers'
GO

CREATE TABLE [dbo].[ContractUsers] (
    [ContractUserId]        INT             NOT NULL IDENTITY,
    [ContractId]            INT             NOT NULL,
    [UserId]                INT             NOT NULL,

    CONSTRAINT [PK_ContractUsers]                        PRIMARY KEY ([ContractUserId]),
    CONSTRAINT [FK_ContractUsers_Contracts]              FOREIGN KEY ([ContractId])       REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractUsers_Users]                  FOREIGN KEY ([UserId])           REFERENCES [dbo].[Users]     ([UserId])
);
GO

exec spDescTable  N'ContractUsers', N'Външни верификатори към договор.'
exec spDescColumn N'ContractUsers', N'ContractUserId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractUsers', N'ContractId'         , N'Идентификатор на договор.'
exec spDescColumn N'ContractUsers', N'UserId'             , N'Идентификатор на потребител.'
GO

