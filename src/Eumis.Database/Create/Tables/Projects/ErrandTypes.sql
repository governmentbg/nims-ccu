PRINT 'ErrandTypes'
GO

CREATE TABLE [dbo].[ErrandTypes] (
    [ErrandTypeId]      INT             NOT NULL IDENTITY,
    [ErrandLegalActId]  INT             NOT NULL,
    [Code]              NVARCHAR(50)    NOT NULL UNIQUE,
    [Name]              NVARCHAR(MAX)   NOT NULL,

    CONSTRAINT [PK_ErrandTypes]                 PRIMARY KEY ([ErrandTypeId]),
    CONSTRAINT [FK_ErrandTypes_ErrandLegalActs] FOREIGN KEY ([ErrandLegalActId]) REFERENCES [dbo].[ErrandLegalActs] ([ErrandLegalActId])
);
GO

exec spDescTable  N'ErrandTypes', N'Тип на процедурата за външно възлагане.'
exec spDescColumn N'ErrandTypes', N'ErrandTypeId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ErrandTypes', N'Code'           , N'Код.'
exec spDescColumn N'ErrandTypes', N'Name'           , N'Наименование.'
/* Name exams
ЗОП
01 - Открита процедура
02 - Ограничена процедура
03 - Конкурс за проект
04 - Процедура на Договаряне с обявление
05 - Процедура на Договаряне без обявление
06 - Състезателен Диалог
07 - Друго (По указание на УО)

ПМС
11 - Избор с публична покана
12 - Избор без провеждане на процедура (по указание на УО)

ЗОП - допълнение
20 - Състезателна процедура с договаряне
21 - Договаряне с предварителна покана за участие
22 - Партньорство за иновации
23 - Договаряне без предварителна покана за участие
24 - Договаряне без публикуване на обявление за поръчка
25 - Публично състезание
26 - Пряко договаряне
27 - Събиране на оферти с обява
*/
GO
