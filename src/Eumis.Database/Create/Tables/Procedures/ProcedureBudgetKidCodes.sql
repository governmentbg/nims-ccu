PRINT 'ProcedureBudgetKidCodes'
GO

CREATE TABLE [dbo].[ProcedureBudgetKidCodes] (
    [ProcedureBudgetKidCodeId]      INT                NOT NULL    IDENTITY,
    [ProcedureBudgetComponentId]    INT                NOT NULL,
    [KidCodeId]                     INT                NOT NULL,

    CONSTRAINT [PK_ProcedureBudgetKidCodes]                                      PRIMARY KEY ([ProcedureBudgetKidCodeId]),
    CONSTRAINT [FK_ProcedureBudgetKidCodes_ProcedureBudgetComponents]            FOREIGN KEY ([ProcedureBudgetComponentId])      REFERENCES [dbo].[ProcedureBudgetComponents] ([ProcedureBudgetComponentId]),
    CONSTRAINT [FK_ProcedureBudgetKidCodes_KidCodes]                             FOREIGN KEY ([KidCodeId])                       REFERENCES [dbo].[KidCodes] ([KidCodeId]),
)
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ProcedureBudgetKidCodes_ProcedureBudgetComponentId_KidCodeId]
ON [ProcedureBudgetKidCodes]([ProcedureBudgetComponentId], [KidCodeId])
GO

exec spDescTable  N'ProcedureBudgetKidCodes', N'Кодове на икономически дейности към компоненти от бюджет на процедура.'
exec spDescColumn N'ProcedureBudgetKidCodes', N'ProcedureBudgetKidCodeId'                    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureBudgetKidCodes', N'ProcedureBudgetComponentId'                  , N'Компонент от бюджет по процедура'
exec spDescColumn N'ProcedureBudgetKidCodes', N'KidCodeId'                                   , N'Идентификатор код на икономическа дейност“'
GO
