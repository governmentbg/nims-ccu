PRINT 'ExpenseSubTypes'
GO

CREATE TABLE [dbo].[ExpenseSubTypes] (
    [ExpenseSubTypeId]      INT             NOT NULL IDENTITY,
    [ExpenseTypeId]         INT             NOT NULL,
    [Name]                  NVARCHAR(MAX)   NOT NULL,
    [NameAlt]               NVARCHAR(MAX)   NULL,
    CONSTRAINT [PK_ExpenseSubTypes]              PRIMARY KEY ([ExpenseSubTypeId]),
    CONSTRAINT [FK_ExpenseSubTypes_ExpenseTypes] FOREIGN KEY ([ExpenseTypeId]) REFERENCES [dbo].[ExpenseTypes] ([ExpenseTypeId])
);
GO

exec spDescTable  N'ExpenseSubTypes', N'Подтип на разход по елемент от бюджета(редактират се от УО).'
exec spDescColumn N'ExpenseSubTypes', N'ExpenseSubTypeId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ExpenseSubTypes', N'ExpenseTypeId'      , N'Идентификатор на тип разход по елемент от бюджета.'
exec spDescColumn N'ExpenseSubTypes', N'Name'               , N'Наименование'
exec spDescColumn N'ExpenseSubTypes', N'NameAlt'            , N'Наименование на друг език'
GO

