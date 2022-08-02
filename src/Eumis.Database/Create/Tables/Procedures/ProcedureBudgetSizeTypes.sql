PRINT 'ProcedureBudgetSizeTypes'
GO

CREATE TABLE [dbo].[ProcedureBudgetSizeTypes] (
    [ProcedureBudgetSizeTypeId]     INT                NOT NULL    IDENTITY,
    [ProcedureBudgetComponentId]    INT                NOT NULL,
    [CompanySizeTypeId]             INT                NOT NULL,

    CONSTRAINT [PK_ProcedureBudgetSizeTypes]                                     PRIMARY KEY ([ProcedureBudgetSizeTypeId]),
    CONSTRAINT [FK_ProcedureBudgetSizeTypes_ProcedureBudgetComponents]           FOREIGN KEY ([ProcedureBudgetComponentId])      REFERENCES [dbo].[ProcedureBudgetComponents] ([ProcedureBudgetComponentId]),
    CONSTRAINT [FK_ProcedureBudgetSizeTypes_CompanySizeTypes]                    FOREIGN KEY ([CompanySizeTypeId])               REFERENCES [dbo].[CompanySizeTypes] ([CompanySizeTypeId]),
)

GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ProcedureBudgetSizeTypes_ProcedureBudgetComponentId_CompanySizeTypeId]
ON [ProcedureBudgetSizeTypes]([ProcedureBudgetComponentId], [CompanySizeTypeId])
GO

exec spDescTable  N'ProcedureBudgetSizeTypes', N'Класификация спрямо „Закона на малки и средни предприятия“ към компоненти от бюджет на процедура.'
exec spDescColumn N'ProcedureBudgetSizeTypes', N'ProcedureBudgetSizeTypeId'                    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureBudgetSizeTypes', N'ProcedureBudgetComponentId'                   , N'Компонент от бюджет по процедура'
exec spDescColumn N'ProcedureBudgetSizeTypes', N'CompanySizeTypeId'                            , N'Идентификатор от класификатор спрямо „Закона на малки и средни предприятия“'

GO
