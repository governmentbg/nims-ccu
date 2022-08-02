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
