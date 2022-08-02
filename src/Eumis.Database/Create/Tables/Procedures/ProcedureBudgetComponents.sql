PRINT 'ProcedureBudgetComponents'
GO

CREATE TABLE [dbo].[ProcedureBudgetComponents] (
    [ProcedureBudgetComponentId]    INT                NOT NULL    IDENTITY,
    [ProcedureId]                   INT                NOT NULL,
    [Name]                          NVARCHAR(1000)     NULL,
    [Amount]                        MONEY              NULL,
    [Notes]                         NVARCHAR(MAX)      NULL,
    [Status]                        INT                NOT NULL,

    [CreateDate]                    DATETIME2          NOT NULL,
    [ModifyDate]                    DATETIME2          NOT NULL,
    [Version]                       ROWVERSION         NOT NULL,

    CONSTRAINT [PK_ProcedureBudgetComponents]                      PRIMARY KEY ([ProcedureBudgetComponentId]),
    CONSTRAINT [FK_ProcedureBudgetComponents_Procedures]           FOREIGN KEY ([ProcedureId])      REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [CHK_ProcedureBudgetComponents_Status]              CHECK       ([Status] IN (1, 2, 3))
)
GO

exec spDescTable  N'ProcedureBudgetComponents', N'Компоненти от бюджет на процедура.'
exec spDescColumn N'ProcedureBudgetComponents', N'ProcedureBudgetComponentId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureBudgetComponents', N'ProcedureId'                   , N'Идентификатор на процедура'
exec spDescColumn N'ProcedureBudgetComponents', N'Name'                          , N'Наименование на компонент'
exec spDescColumn N'ProcedureBudgetComponents', N'Amount'                        , N'Сума на компонент.'
exec spDescColumn N'ProcedureBudgetComponents', N'Notes'                         , N'Бележки'
exec spDescColumn N'ProcedureBudgetComponents', N'Status'                        , N'Статус на компонент: 1 - Неактивиран, 2 - Активиран, 3 - Неактивен.'
exec spDescColumn N'ProcedureBudgetComponents', N'CreateDate'                    , N'Дата на създаване на записа.'
exec spDescColumn N'ProcedureBudgetComponents', N'ModifyDate'                    , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProcedureBudgetComponents', N'Version'                       , N'Версия.'

GO
