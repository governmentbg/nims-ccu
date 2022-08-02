PRINT 'ContractActivities'
GO

CREATE TABLE [dbo].[ContractActivities] (
    [ContractActivityId]        INT                 NOT NULL IDENTITY,
    [ContractId]                INT                 NOT NULL,
    [Gid]                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [IsActive]                  BIT                 NOT NULL,

    [Code]                      NVARCHAR(MAX)       NOT NULL,
    [Name]                      NVARCHAR(MAX)       NOT NULL,
    [ExecutionMethod]           NVARCHAR(MAX)       NOT NULL,
    [Result]                    NVARCHAR(MAX)       NOT NULL,
    [StartMonth]                INT                 NOT NULL,
    [Duration]                  INT                 NOT NULL,
    [Amount]                    MONEY               NOT NULL,

    CONSTRAINT [PK_ContractActivities]                        PRIMARY KEY ([ContractActivityId]),
    CONSTRAINT [FK_ContractActivities_Contracts]              FOREIGN KEY ([ContractId])               REFERENCES [dbo].[Contracts] ([ContractId])
);
GO

exec spDescTable  N'ContractActivities', N'Дейности към договор.'
exec spDescColumn N'ContractActivities', N'ContractActivityId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractActivities', N'ContractId'          , N'Идентификатор на договор.'
exec spDescColumn N'ContractActivities', N'Gid'                 , N'Публичен идентификатор.'
exec spDescColumn N'ContractActivities', N'IsActive'            , N'Маркер за активност.'

exec spDescColumn N'ContractActivities', N'Code'                , N'Дейност.'
exec spDescColumn N'ContractActivities', N'Name'                , N'Описание.'
exec spDescColumn N'ContractActivities', N'ExecutionMethod'     , N'Начин на изпълнение.'
exec spDescColumn N'ContractActivities', N'Result'              , N'Резултат.'
exec spDescColumn N'ContractActivities', N'StartMonth'          , N'Месец за стартиране на дейността (месеци).'
exec spDescColumn N'ContractActivities', N'Duration'            , N'Продължителност на дейността.'
exec spDescColumn N'ContractActivities', N'Amount'              , N'Стойност.'
GO
