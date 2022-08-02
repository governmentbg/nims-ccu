PRINT 'ExpenseBudgetItems'
GO

CREATE TABLE [dbo].[ExpenseBudgetItems] (
    [ExpenseBudgetItemId] INT           NOT NULL IDENTITY,
    [ItemLevel]           INT           NOT NULL,
    [SectionCode]         NVARCHAR(50)  NOT NULL,
    [SubsectionCode]      NVARCHAR(50)  NOT NULL,
    [ParagraphCode]       NVARCHAR(50)  NOT NULL,
    [Code]                NVARCHAR(50)  NOT NULL,
    [Name]                NVARCHAR(MAX) NOT NULL,
    [IsVisible]           bit           NOT NULL,
    [IsTotalVisible]      bit           NOT NULL,
    [IsInsteadParent]     bit           NOT NULL,
    CONSTRAINT [PK_ExpenseBudgetItems] PRIMARY KEY ([ExpenseBudgetItemId])
);
GO

exec spDescTable  N'ExpenseBudgetItems', N'Елементи на бюджет.'
exec spDescColumn N'ExpenseBudgetItems', N'ExpenseBudgetItemId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ExpenseBudgetItems', N'ItemLevel'            , N'Ниво на елемента 1(Раздел:Разходи, Приходи), 2(подраздел:Общо допустими разходи, Допълващи разходи), 3(параграф:Кръстосано финансиране, Разходи направени от партньори)'
exec spDescColumn N'ExpenseBudgetItems', N'SectionCode'          , N'Код на раздел:1.Разходи, 2.Приходи'
exec spDescColumn N'ExpenseBudgetItems', N'SubsectionCode'       , N'Код на подраздел:1.Общо допустими разходи, 2.Допълващи разходи'
exec spDescColumn N'ExpenseBudgetItems', N'ParagraphCode'        , N'Код на параграф:1.Кръстосано финансиране, 2.Разходи направени от партньори'
exec spDescColumn N'ExpenseBudgetItems', N'Code'                 , N'Код (I.)(1)(1.1)'
exec spDescColumn N'ExpenseBudgetItems', N'Name'                 , N'Наименование'
exec spDescColumn N'ExpenseBudgetItems', N'IsVisible'            , N'Маркер дали се показва'
exec spDescColumn N'ExpenseBudgetItems', N'IsTotalVisible'       , N'Маркер дали се показва показва тотал на подчинените елементи'
exec spDescColumn N'ExpenseBudgetItems', N'IsInsteadParent'      , N'Маркер дали се показва вместо родителското ниво (при пози случай IsVisible на родителя е 0)'
GO

