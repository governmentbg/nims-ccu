PRINT 'ExpenseTypes'
GO

CREATE TABLE [dbo].[ExpenseTypes] (
    [ExpenseTypeId]         INT                 NOT NULL IDENTITY,
    [Gid]                   UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [Name]                  NVARCHAR(MAX)       NOT NULL,
    [NameAlt]               NVARCHAR(MAX)       NULL,
    [IsActive]              BIT                 NOT NULL,
    [CreateDate]            DATETIME2           NOT NULL,
    [ModifyDate]            DATETIME2           NOT NULL,
    [Version]               ROWVERSION          NOT NULL,

    CONSTRAINT [PK_ExpenseTypes]                PRIMARY KEY ([ExpenseTypeId])
);
GO

exec spDescTable  N'ExpenseTypes', N'Тип на разходи по елемент от бюджета(редактират се на централно ниво).'
exec spDescColumn N'ExpenseTypes', N'ExpenseTypeId'         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ExpenseTypes', N'Name'                  , N'Наименование'
exec spDescColumn N'ExpenseTypes', N'NameAlt'               , N'Наименование на друг език'
exec spDescColumn N'ExpenseTypes', N'IsActive'              , N'Маркер за активност.'
exec spDescColumn N'ExpenseTypes', N'CreateDate'            , N'Дата на създаване на записа.'
exec spDescColumn N'ExpenseTypes', N'ModifyDate'            , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ExpenseTypes', N'Version'               , N'Версия.'
GO
